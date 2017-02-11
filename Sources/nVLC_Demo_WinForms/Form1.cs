//    nVLC
//
//    Author:  Roman Ginzburg
//
//    nVLC is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    nVLC is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU General Public License for more details.
//
// ========================================================================
using System;
using System.ComponentModel;
using System.Windows.Forms;
using nVLC;
using nVLC.Events;
using nVLC.Media;
using nVLC.Players;

namespace nVLC_Demo_WinForms
{
    public partial class Form1 : Form
    {
        IMediaPlayerFactory m_factory;
        IDiskPlayer m_player;
        IMedia m_media;

        public Form1()
        {
            InitializeComponent();

            #if !__MonoCS__
            bool findLibvlc = true;
            #else
            bool findLibvlc = false;
            #endif

            m_factory = new MediaPlayerFactory(findLibvlc);
            m_player = m_factory.CreatePlayer<IDiskPlayer>();

            m_player.Events.PlayerPositionChanged += Events_PlayerPositionChanged;
            m_player.Events.TimeChanged += Events_TimeChanged;
            m_player.Events.MediaEnded += Events_MediaEnded;
            m_player.Events.PlayerStopped += Events_PlayerStopped;

            m_player.WindowHandle = panel1.Handle;
            trackBar2.Value = m_player.Volume > 0 ? m_player.Volume : 0;

            UISync.Init(this);
        }

        void Events_PlayerStopped(object sender, EventArgs e)
        {
            UISync.Execute(() => InitControls());
        }

        void Events_MediaEnded(object sender, EventArgs e)
        {
            UISync.Execute(() => InitControls());
        }

        private void InitControls()
        {
            trackBar1.Value = 0;
            lblTime.Text = @"00:00:00";
            lblDuration.Text = @"00:00:00";
        }

        void Events_TimeChanged(object sender, MediaPlayerTimeChanged e)
        {
            UISync.Execute(() => lblTime.Text = TimeSpan.FromMilliseconds(e.NewTime).ToString().Substring(0, 8));
        }

        void Events_PlayerPositionChanged(object sender, MediaPlayerPositionChanged e)
        {
            UISync.Execute(() => trackBar1.Value = (int)(e.NewPosition * 100));
        }

        private void LoadMedia()
        {
            using (var ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = ofd.FileName;
                }
            }
        }

        void Events_StateChanged(object sender, MediaStateChange e)
        {
            UISync.Execute(() => label1.Text = e.NewState.ToString());
        }

        void Events_DurationChanged(object sender, MediaDurationChange e)
        {
            UISync.Execute(() => lblDuration.Text = TimeSpan.FromMilliseconds(e.NewDuration).ToString().Substring(0, 8));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadMedia();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                m_media = m_factory.CreateMedia<IMedia>(textBox1.Text);
                m_media.Events.DurationChanged += Events_DurationChanged;
                m_media.Events.StateChanged += Events_StateChanged;
                m_media.Events.ParsedChanged += Events_ParsedChanged;

                m_player.Open(m_media);
                m_media.Parse(true);

                m_player.Play();
            }
            else
            {
                errorProvider1.SetError(textBox1, "Please select media path first !");
            }
        }

        void Events_ParsedChanged(object sender, MediaParseChange e)
        {
            Console.WriteLine(e.Parsed);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            m_player.Volume = trackBar2.Value;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            m_player.Position = (float)trackBar1.Value / 100;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            m_player.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            m_player.Pause();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_player.ToggleMute();

            button1.Text = m_player.Mute ? "Unmute" : "Mute";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private class UISync
        {
            private static ISynchronizeInvoke Sync;

            public static void Init(ISynchronizeInvoke sync)
            {
                Sync = sync;
            }

            public static void Execute(Action action)
            {
                Sync.BeginInvoke(action, null);
            }
        }
    }
}

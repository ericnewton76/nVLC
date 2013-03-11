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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Declarations;
using LibVlcWrapper;

namespace Implementation
{
    internal class Log : DisposableBase
    {
        Thread m_reader;
        IntPtr m_hLog = IntPtr.Zero;
        volatile bool doRun;
        ILogger m_logger;
        bool m_enabled;
        LogIterator m_logIterator;

        public Log(IntPtr hLib, ILogger logger)
        {           
            m_logger = logger;

            LibVlcMethods.libvlc_set_log_verbosity(hLib, 2);
            m_hLog = LibVlcMethods.libvlc_log_open(hLib);
            m_logIterator = new LogIterator(m_hLog);
            m_reader = new Thread(Retreive);
            m_reader.IsBackground = true;
            m_reader.Name = "Log Thread";

            WriteTimeout = 500;
        }

        public int WriteTimeout { get; set; }

        private void Retreive()
        {
            while (doRun)
            {
                foreach (var item in m_logIterator)
                {
                    switch (item.Severity)
                    {
                        case libvlc_log_messate_t_severity.INFO:
                            m_logger.Info(item.Message);
                            break;

                        case libvlc_log_messate_t_severity.WARN:
                            m_logger.Warning(item.Message);
                            break;

                        case libvlc_log_messate_t_severity.DBG:
                            m_logger.Debug(item.Message);
                            break;

                        case libvlc_log_messate_t_severity.ERR:

                        default:
                            m_logger.Error(item.Message);
                            break;
                    }
                }

                Thread.Sleep(WriteTimeout);
            }
        }

        private void Start()
        {
            doRun = true;
            m_reader.Start();
        }

        private void Stop()
        {
            doRun = false;
        }

        public bool Enabled
        {
            get
            {
                return m_enabled;
            }
            set
            {
                if (m_enabled == value)
                {
                    return;
                }

                m_enabled = value;
                if (m_enabled)
                {
                    Start();
                }
                else
                {
                    Stop();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            LibVlcMethods.libvlc_log_close(m_hLog);
        }

        private class LogIterator : IEnumerable<LogMessage>
        {
            IntPtr m_hLog;

            internal LogIterator(IntPtr hLog)
            {
                m_hLog = hLog;
            }

            #region IEnumerable<string> Members

            public IEnumerator<LogMessage> GetEnumerator()
            {
                IntPtr i = LibVlcMethods.libvlc_log_get_iterator(m_hLog);

                while (LibVlcMethods.libvlc_log_iterator_has_next(i) != 0)
                {
                    libvlc_log_message_t msg = new libvlc_log_message_t();
                    msg.sizeof_msg = (uint)Marshal.SizeOf(msg);
                    LibVlcMethods.libvlc_log_iterator_next(i, ref msg);

                    yield return GetMessage(msg);
                }

                LibVlcMethods.libvlc_log_iterator_free(i);
                LibVlcMethods.libvlc_log_clear(m_hLog);
            }

            private LogMessage GetMessage(libvlc_log_message_t msg)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0} ", Marshal.PtrToStringAnsi(msg.psz_header));
                sb.AppendFormat("{0} ", Marshal.PtrToStringAnsi(msg.psz_message));
                sb.AppendFormat("{0} ", Marshal.PtrToStringAnsi(msg.psz_name));
                sb.Append(Marshal.PtrToStringAnsi(msg.psz_type));

                return new LogMessage() { Message = sb.ToString(), Severity = (libvlc_log_messate_t_severity)msg.i_severity };
            }

            #endregion

            #region IEnumerable Members

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion
        }

        private struct LogMessage
        {
            public libvlc_log_messate_t_severity Severity;
            public string Message;
        }
    }
}

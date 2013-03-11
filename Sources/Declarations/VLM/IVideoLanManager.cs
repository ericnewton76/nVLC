using System;
using System.Collections.Generic;

namespace Declarations.VLM
{
    /// <summary>
    /// 
    /// </summary>
    public interface IVideoLanManager : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <param name="options"></param>
        /// <param name="bEnabled"></param>
        /// <param name="bLoop"></param>
        void AddBroadcast(string name, string input, string output, IEnumerable<string> options = null, bool bEnabled = true, bool bLoop = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="input"></param>
        void AddInput(string name, string input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="input"></param>
        /// <param name="options"></param>
        /// <param name="bEnabled"></param>
        /// <param name="mux"></param>
        void AddVod(string name, string input, IEnumerable<string> options = null, bool bEnabled = true, string mux = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <param name="options"></param>
        /// <param name="bEnabled"></param>
        /// <param name="bLoop"></param>
        void ChangeMedia(string name, string input, string output, IEnumerable<string> options, bool bEnabled, bool bLoop);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        void DeleteMedia(string name);

        /// <summary>
        /// 
        /// </summary>
        IVlmEventManager Events { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        int GetMediaLength(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        float GetMediaPosition(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        int GetMediaRate(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        int GetMediaTime(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        int GetMediaTitle(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        int GetMediaChapter(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool IsMediaSeekable(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        void Pause(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        void Play(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="percentage"></param>
        void Seek(string name, float percentage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bEnabled"></param>
        void SetEnabled(string name, bool bEnabled);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="input"></param>
        void SetInput(string name, string input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bLoop"></param>
        void SetLoop(string name, bool bLoop);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mux"></param>
        void SetMux(string name, string mux);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="output"></param>
        void SetOutput(string name, string output);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        void Stop(string name);
    }
}

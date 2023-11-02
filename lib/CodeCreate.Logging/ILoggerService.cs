using System;

namespace CodeCreate.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILoggerService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public LogEntry LogError(string message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public LogEntry LogError(string message, object data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        /// <param name="appEventId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public LogEntry LogError(Exception ex, string message,
            int? appEventId = null, object? data = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="appEventId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public LogEntry LogError(string message, int appEventId,
            object? data = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public LogEntry LogInformation(string message, object? data = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="appEventId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public LogEntry LogInformation(string message, int appEventId,
            object? data = null);
    }
}

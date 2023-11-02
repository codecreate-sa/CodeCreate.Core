using System;

namespace CodeCreate.Logging
{
    /// <summary>
    /// The ILoggerservice interface
    /// </summary>
    public interface ILoggerService
    {
        /// <summary>
        /// An overload of the LogError method
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public LogEntry LogError(string message);

        /// <summary>
        /// An overload of the LogError method
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public LogEntry LogError(string message, object data);

        /// <summary>
        /// An overload of the LogError method
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        /// <param name="appEventId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public LogEntry LogError(
            Exception ex, 
            string message,
            int? appEventId = null, 
            object? data = null
        );

        /// <summary>
        /// An overload of the LogError method
        /// </summary>
        /// <param name="message"></param>
        /// <param name="appEventId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public LogEntry LogError(
            string message, 
            int appEventId,
            object? data = null
        );

        /// <summary>
        /// An overload of the LogInformation method
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public LogEntry LogInformation(string message, object? data = null);

        /// <summary>
        /// An overload of the LogInformation method
        /// </summary>
        /// <param name="message"></param>
        /// <param name="appEventId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public LogEntry LogInformation(
            string message, 
            int appEventId,
            object? data = null
        );
    }
}

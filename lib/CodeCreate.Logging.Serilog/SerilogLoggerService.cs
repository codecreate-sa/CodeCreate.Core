using Serilog;
using System;
using CodeCreate.Logging.Serilog.Constants;

namespace CodeCreate.Logging.Serilog
{
    /// <summary>
    /// 
    /// </summary>
    public class SerilogLoggerService : ILoggerService
    {
        private readonly ICorrelationIdProvider _correlationIdProvider;

        private const string LogMessageTemplate = LogMessage.Template;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="correlationIdProvider"></param>
        public SerilogLoggerService(ICorrelationIdProvider correlationIdProvider)
        {
            _correlationIdProvider = correlationIdProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public LogEntry LogError(string message)
        {
            return LogErrorInternal(null, message, null, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public LogEntry LogError(string message, object data)
        {
            return LogErrorInternal(null, message, null, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="appEventId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public LogEntry LogError(string message, int appEventId, 
            object? data = null)
        {
            return LogErrorInternal(null, message, appEventId, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        /// <param name="appEventId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public LogEntry LogError(Exception ex, string message, 
            int? appEventId = null, object? data = null)
        {
            return LogErrorInternal(ex, message, appEventId, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public LogEntry LogInformation(string message, object? data = null)
        {
            return LogInformationInternal(message, null, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="appEventId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public LogEntry LogInformation(string message, int appEventId, 
            object? data = null)
        {
            return LogInformationInternal(message, appEventId, data);
        }

        private LogEntry LogErrorInternal(Exception? ex, string message, 
            int? appEventId, object? data)
        {
            var correlationId = _correlationIdProvider.GetCorrelationId();

            if (ex == null) 
            {
                Log.Error(LogMessageTemplate, message, 
                    data, appEventId, 
                    correlationId);
            } 
            else 
            {
                Log.Error(ex, LogMessageTemplate,
                    message, data, 
                    appEventId, correlationId);
            }

            return new LogEntry 
            {
                Message = message,
                AppEventId = appEventId,
                CorrelationId = correlationId
            };
        }

        private LogEntry LogInformationInternal(string message, int? appEventId, 
            object? data)
        {
            var correlationId = _correlationIdProvider.GetCorrelationId();

            Log.Information(LogMessageTemplate, message, 
                data, appEventId, 
                correlationId);

            return new LogEntry 
            {
                Message = message,
                AppEventId = appEventId,
                CorrelationId = correlationId
            };
        }
    }
}

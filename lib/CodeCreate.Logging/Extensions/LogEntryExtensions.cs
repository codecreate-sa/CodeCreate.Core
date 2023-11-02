using CodeCreate.Core;

namespace CodeCreate.Logging.Extensions
{
    /// <summary>
    ///
    /// </summary>
    public static class LogEntryExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="resultCode"></param>
        /// <returns></returns>
        public static ApiResult<T> CreateFailedResult<T>(this LogEntry @this, int resultCode)
        {
            return ApiResult<T>.CreateFailed(
                resultCode,
                @this.Message, 
                @this.AppEventId ?? 0, 
                @this.CorrelationId
            );
        }
    }
}

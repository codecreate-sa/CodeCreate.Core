using System;

namespace CodeCreate.Core
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string? ErrorText { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string? CorrelationId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool Successful => Code == ResultCode.Success;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="errorText"></param>
        /// <param name="eventId"></param>
        /// <param name="correlationId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static ApiResult<T> CreateFailed(int code, string? errorText,
            int eventId, string? correlationId)
        {
            if (code == ResultCode.Success) 
            {
                throw new ArgumentOutOfRangeException(nameof(code));
            }

            return new ApiResult<T> 
            {
                Code = code,
                EventId = eventId,
                ErrorText = errorText,
                CorrelationId = correlationId
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="code"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static ApiResult<T> CreateFailed(int code, string errorText)
        {
            return CreateFailed(code, errorText, 0, null);
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="result"></param>
        /// <typeparam name="Y"></typeparam>
        /// <returns></returns>
        public static ApiResult<T> CreateFailed<Y>(ApiResult<Y> result)
        {
            return CreateFailed(result.Code, result.ErrorText,
                result.EventId, result.CorrelationId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiResult<T> CreateSuccessful(T? data)
        {
            return new ApiResult<T>()
            {
                Data = data,
                Code = ResultCode.Success
            };
        }
    }
}

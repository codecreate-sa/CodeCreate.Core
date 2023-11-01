using System;

namespace CodeCreate.Core
{
    /// <summary>
    /// The ApiResult construct
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T>
    {
        /// <summary>
        /// The data of type <T>
        /// </summary>
        public T Data { get; set; } = default!;

        /// <summary>
        /// The code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// The event id
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// The error text
        /// </summary>
        public string ErrorText { get; set; } = default!;

        /// <summary>
        /// The correlation id
        /// </summary>
        public string? CorrelationId { get; set; }

        /// <summary>
        /// Whether the result code is successful or not
        /// </summary>
        public bool Successful => Code == ResultCode.Success;

        /// <summary>
        /// CreateFailed helper method
        /// </summary>
        /// <param name="code"></param>
        /// <param name="errorText"></param>
        /// <param name="eventId"></param>
        /// <param name="correlationId"></param>
        /// <returns></returns>
        public static ApiResult<T> CreateFailed(
            int code, 
            string errorText, 
            int eventId, 
            string? correlationId)
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
        /// CreateFailed helper method
        /// </summary>
        /// <param name="code"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static ApiResult<T> CreateFailed(int code, string errorText)
        {
            return CreateFailed(code, errorText, 0, null);
        }

        /// <summary>
        /// CreateFailed helper method
        /// </summary>
        /// <typeparam name="Y"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static ApiResult<T> CreateFailed<Y>(ApiResult<Y> result)
        {
            return CreateFailed(
                result.Code, 
                result.ErrorText,
                result.EventId, 
                result.CorrelationId
            );
        }

        /// <summary>
        /// CreateSuccessful helper method
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiResult<T> CreateSuccessful(T data)
        {
            return new ApiResult<T>
            {
                Data = data,
                Code = ResultCode.Success
            };
        }
    }
}

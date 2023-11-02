using CodeCreate.Core;

namespace CodeCreate.AspNetCore
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApiResultExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static ApiObjectResult<T> AsActionResult<T>(this ApiResult<T> result)
        {
            return new ApiObjectResult<T>(result);
        }
    }
}

using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CodeCreate.Core;

namespace CodeCreate.AspNetCore
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiObjectResult<T> : ObjectResult
    {
        private readonly ApiResult<T> _result;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        public ApiObjectResult(ApiResult<T> result) : base(result)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            if (result.Successful) 
            {
                Value = result.Data;
                StatusCode = StatusCodes.Status200OK;
            }
            else
            {
                StatusCode = result.Code;

                Value = new 
                {
                    status = StatusCode,
                    eventId = result.EventId,
                    message = result.ErrorText
                };
            }

            _result = result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnFormatting(ActionContext context)
        {
            if (context == null) 
            {
                throw new ArgumentNullException(nameof(context));
            }

            base.OnFormatting(context);

            context
                .HttpContext
                .Response
                .Headers[Constants.AppEventIdHeaderName] = $"{_result.EventId}";
        }
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CodeCreate.Logging.AspNetCore.Constants;

namespace CodeCreate.Logging.AspNetCore
{
    /// <summary>
    ///  
    /// </summary>
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, ICorrelationIdProvider provider)
        {
            var correlationId = CreateCorrelationId(context);

            provider.SetCorrelationId(correlationId);

            context.TraceIdentifier = correlationId;

            context.Response.OnStarting(() => 
            {
                if (!context.Response.Headers.ContainsKey(CorrelationId.DefaultHeader)) 
                {
                    context.Response.Headers.Add(
                        CorrelationId.DefaultHeader, 
                        correlationId
                    );
                }

                return Task.CompletedTask;
            });

            await _next(context);
        }

        private static string CreateCorrelationId(HttpContext context)
        {
            var headerValue = context?
                .Request?
                .Headers[CorrelationId.DefaultHeader];

            if (!string.IsNullOrEmpty(headerValue)) 
            {
                return headerValue!;
            }

            return $"{Guid.NewGuid()}";
        }
    }
}

using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace CodeCreate.Logging.AspNetCore
{
    /// <summary>
    /// The middleware for fetching the correlation id
    /// </summary>
    public class CorrelationIdMiddleware
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// The CorrelationIdMiddleware constructor
        /// </summary>
        /// <param name="next"></param>
        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        /// <summary>
        /// The Invoke method
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
                if (!context.Response.Headers.ContainsKey(Constants.CorrelationId.DefaultHeader)) 
                {
                    context.Response.Headers.Add(
                        Constants.CorrelationId.DefaultHeader, 
                        correlationId
                    );
                }

                return Task.CompletedTask;
            });

            await _next(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static string CreateCorrelationId(HttpContext context)
        {
            var headerValue = context?
                .Request?
                .Headers[Constants.CorrelationId.DefaultHeader];

            return !string.IsNullOrWhiteSpace(headerValue) ? 
                headerValue :
                $"{Guid.NewGuid()}";
        }
    }
}

using System;
using System.Collections.Generic;

namespace CodeCreate.Messaging
{
    using System.Threading.Tasks;
    using CodeCreate.Events;

    /// <summary>
    /// 
    /// </summary>
    public interface IMessagePump
    {
        /// <summary>
        /// 
        /// </summary>
        TimeSpan DefaultEventRetryDelay { get; }

        /// <summary>
        ///
        /// </summary>
        void Start();

        /// <summary>
        ///
        /// </summary>
        void Stop();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Task<bool> DispatchAsync(IEvent @event);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        TimeSpan GetRetryDelay(IEvent @event);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<SubscriberConfig> GetSubscriberConfig();
    }
}

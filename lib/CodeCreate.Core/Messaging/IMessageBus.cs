namespace CodeCreate.Messaging
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    ///
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="delay"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Publish<T>(T message, TimeSpan? delay = null)
            where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="topic"></param>
        /// <param name="delay"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Publish<T>(T message, string topic, TimeSpan? delay = null)
            where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="delay"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> PublishAsync<T>(T message, TimeSpan? delay = null)
            where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="topic"></param>
        /// <param name="delay"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> PublishAsync<T>(T message, string topic, TimeSpan? delay = null)
            where T : class;

        /// <summary>
        ///
        /// </summary>
        /// <param name="onMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IDisposable Subscribe<T>(Func<T, Task> onMessage)
            where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="onMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IDisposable Subscribe<T>(string subscriptionId, Func<T, Task> onMessage)
            where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="topic"></param>
        /// <param name="onMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IDisposable Subscribe<T>(string subscriptionId, string topic, Func<T, Task> onMessage)
            where T : class;
    }
}

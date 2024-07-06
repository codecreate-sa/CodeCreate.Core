using EasyNetQ;

namespace CodeCreate.Messaging
{
    /// <summary>
    /// 
    /// </summary>
    public class RabbitMqMessageBus : IMessageBus
    {
        /// <summary>
        ///
        /// </summary>
        private readonly IBus _bus;

        /// <summary>
        ///
        /// </summary>
        /// <param name="bus"></param>
        public RabbitMqMessageBus(IBus bus)
        {
            _bus = bus;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="delay"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Publish<T>(T message, TimeSpan? delay = null)
            where T : class
        {
            ArgumentNullException.ThrowIfNull(message);

            try
            {
                if (delay is null)
                {
                    _bus.PubSub.Publish(message);
                }
                else
                {
                    _bus.Scheduler.FuturePublish(message, delay.Value);
                }
            }
            catch (Exception e)
            {
                // log?
                return false;
            }

            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="delay"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<bool> PublishAsync<T>(T message, TimeSpan? delay = null)
            where T : class
        {
            ArgumentNullException.ThrowIfNull(message);

            try
            {
                if (delay is null)
                {
                    await _bus.PubSub.PublishAsync(message);
                }
                else
                {
                    await _bus.Scheduler.FuturePublishAsync(message, delay.Value);
                }
            }
            catch (Exception e)
            {
                // log
                return false;
            }

            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="onMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IDisposable Subscribe<T>(Func<T, Task> onMessage)
            where T : class
        {
            ArgumentNullException.ThrowIfNull(onMessage);

            return _bus.PubSub.Subscribe("dev", onMessage);
        }
    }
}

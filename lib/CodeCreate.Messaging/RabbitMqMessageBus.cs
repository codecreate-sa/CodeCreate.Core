namespace CodeCreate.Messaging
{
    using System;
    using System.Threading.Tasks;

    using EasyNetQ;

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
            return PublishInternal(message, null, delay);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="topic"></param>
        /// <param name="delay"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Publish<T>(T message, string? topic, TimeSpan? delay = null)
            where T : class
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(topic);

            return PublishInternal(message, topic, delay);
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
            return await PublishInternalAsync(message, null, delay);
        }

        public async Task<bool> PublishAsync<T>(T message, string? topic, TimeSpan? delay = null)
            where T : class
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(topic);

            return await PublishInternalAsync(message, topic, delay);
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
            return SubscribeInternal("default", null, onMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscriberId"></param>
        /// <param name="onMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IDisposable Subscribe<T>(string subscriberId, Func<T, Task> onMessage)
            where T : class
        {
            return SubscribeInternal(subscriberId, null, onMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscriberId"></param>
        /// <param name="topic"></param>
        /// <param name="onMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IDisposable Subscribe<T>(string subscriberId, string? topic, Func<T, Task> onMessage)
            where T : class
        {
            return SubscribeInternal(subscriberId, topic, onMessage);
        }

        private IDisposable SubscribeInternal<T>(string subscriberId, string? topic, Func<T, Task> onMessage)
            where T : class
        {
            ArgumentNullException.ThrowIfNull(onMessage);
            ArgumentException.ThrowIfNullOrWhiteSpace(subscriberId);

            if (string.IsNullOrWhiteSpace(topic))
            {
                return _bus.PubSub.Subscribe(subscriberId, onMessage);
            }

            return _bus.PubSub.Subscribe<T>(subscriberId, (x, _) => onMessage(x), x => x.WithTopic(topic));
        }

        private bool PublishInternal<T>(T message, string? topic, TimeSpan? delay = null)
        {
            ArgumentNullException.ThrowIfNull(message);

            try
            {
                if (delay is null)
                {
                    if (string.IsNullOrWhiteSpace(topic))
                    {
                        _bus.PubSub.Publish(message);
                    }
                    else
                    {
                        _bus.PubSub.Publish(message, topic);
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(topic))
                    {
                        _bus.Scheduler.FuturePublish(message, delay.Value);
                    }
                    else
                    {
                        _bus.Scheduler.FuturePublish(message, delay.Value, topic);
                    }
                }
            }
            catch (Exception e)
            {
                // log?
                return false;
            }

            return true;
        }

        private async Task<bool> PublishInternalAsync<T>(T message, string? topic, TimeSpan? delay = null)
            where T : class
        {
            ArgumentNullException.ThrowIfNull(message);

            try
            {
                if (delay is null)
                {
                    if (string.IsNullOrWhiteSpace(topic))
                    {
                        await _bus.PubSub.PublishAsync(message);
                    }
                    else
                    {
                        await _bus.PubSub.PublishAsync(message, topic);
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(topic))
                    {
                        await _bus.Scheduler.FuturePublishAsync(message, delay.Value);
                    }
                    else
                    {
                        await _bus.Scheduler.FuturePublishAsync(message, delay.Value, c => c.WithTopic(topic));
                    }
                }
            }
            catch (Exception e)
            {
                // log
                return false;
            }

            return true;
        }
    }
}

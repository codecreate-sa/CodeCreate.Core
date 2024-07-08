namespace CodeCreate.Messaging
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using CodeCreate.Events;

    /// <summary>
    ///
    /// </summary>
    public abstract class MessagePumpBase : IMessagePump, IDisposable
    {
        /// <summary>
        ///
        /// </summary>
        public virtual TimeSpan DefaultEventRetryDelay => TimeSpan.FromHours(1);

        /// <summary>
        ///
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public virtual TimeSpan GetRetryDelay(IEvent @event) =>
            @event.EventTypeId switch
            {
                _ => DefaultEventRetryDelay
            };

        /// <summary>
        ///
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        ///
        /// </summary>
        private readonly IMessageBus _bus;

        /// <summary>
        ///
        /// </summary>
        public bool Started { get; private set; }

        /// <summary>
        ///
        /// </summary>
        protected MessagePumpBase(IMessageBus bus)
        {
            _bus = bus;
        }

        /// <summary>
        ///
        /// </summary>
        private List<IDisposable?>? _subscribers = [];

        /// <summary>
        ///
        /// </summary>
        /// <exception cref="ObjectDisposedException"></exception>
        public void Start()
        {
            ObjectDisposedException.ThrowIf(_disposed, this);

            if (Started)
            {
                return;
            }

            Started = true;
            
            _subscribers ??= [];

            foreach (var subscriber in GetSubscriberConfig())
            {
                _subscribers.Add(_bus.Subscribe<IEvent>(
                    subscriber.SubscriberId, subscriber.Topic,
                    async @event =>
                    {
                        if (!await DispatchAsync(@event))
                        {
                            await _bus.PublishAsync(@event, @event.Topic, GetRetryDelay(@event));
                        }
                    }));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<SubscriberConfig> GetSubscriberConfig();

        /// <summary>
        ///
        /// </summary>
        public void Stop()
        {
            Dispose();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public abstract Task<bool> DispatchAsync(IEvent @event);

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                if (_subscribers is not null)
                {
                    for (var i = 0; i < _subscribers.Count; ++i)
                    {
                        if (_subscribers[i] is null)
                        {
                            continue;
                        }

                        _subscribers[i]!.Dispose();
                        _subscribers[i] = null;
                    }

                    _subscribers.Clear();
                    _subscribers = null;
                }
            }

            _disposed = true;
        }
    }
}

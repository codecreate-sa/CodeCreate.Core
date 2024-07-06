namespace CodeCreate.Messaging
{
    using CodeCreate.Events;

    /// <summary>
    ///
    /// </summary>
    public class MessagePump : IMessagePump, IDisposable
    {
        /// <summary>
        ///
        /// </summary>
        private readonly static TimeSpan EventLifetime = TimeSpan.FromDays(1); //tbd

        /// <summary>
        ///
        /// </summary>
        private readonly static TimeSpan EventRetryDelay = TimeSpan.FromHours(1); //tbd

        /// <summary>
        ///
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        private static TimeSpan GetRetryDelay(IEvent @event) =>
            @event.EventTypeId switch
            {
                _ => EventRetryDelay
            };

        /// <summary>
        ///
        /// </summary>
        private readonly static IReadOnlySet<int> _shouldAlertByMail =
            new HashSet<int>();

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
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        ///
        /// </summary>
        public bool Started { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public MessagePump(IMessageBus bus,
            IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
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
            _subscribers.Add(_bus.Subscribe<IEvent>(async @event =>
            {
                if (!await DispatchAsync(@event))
                {
                    await _bus.PublishAsync(@event, GetRetryDelay(@event));
                }
            }));
        }

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
        public Task<bool> DispatchAsync(IEvent @event)
        {
            ArgumentNullException.ThrowIfNull(@event);

            if (DateTime.UtcNow.Subtract(@event.CreatedUtc) > EventLifetime)
            {
                //log timeout
                return Task.FromResult(true);
            }

            if (@event.EventTypeId == EventTypeId.Invalid)
            {
                // log
                return Task.FromResult(true);
            }

            return @event.EventTypeId switch
            {
                _ => Task.FromResult(true)
            };
        }

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

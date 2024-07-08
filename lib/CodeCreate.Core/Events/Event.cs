namespace CodeCreate.Events
{
    using System;

    public class Event<T> : IEvent<T> where T : IEventData
    {
        public T? EventData { get; set; }

        public string? Topic { get; set; } //tbd

        public DateTime CreatedUtc { get; set; }


        public string? CorrelationId { get; set; }


        public EventTypeId EventTypeId { get; set; }


        public Event()
        {
            CreatedUtc = DateTime.UtcNow;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="data"></param>
        /// <param name="correlationId"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Event(EventTypeId typeId, T data, string? correlationId = null)
            : this()
        {
            ArgumentNullException.ThrowIfNull(data);

            if (typeId == EventTypeId.Invalid)
            {
                throw new ArgumentOutOfRangeException(nameof(typeId));
            }

            EventData = data;
            EventTypeId = typeId;
            CorrelationId = correlationId;
        }
    }

    public class Event : Event<IEventData>, IEvent
    {
        public Event() : base()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="data"></param>
        /// <param name="correlationId"></param>
        public Event(EventTypeId typeId, IEventData data, string? correlationId = null)
            : base(typeId, data, correlationId)
        {
        }
    }
}

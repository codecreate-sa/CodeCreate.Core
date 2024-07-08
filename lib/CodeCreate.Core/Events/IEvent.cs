namespace CodeCreate.Events
{
    using System;

    public interface IEvent<T>
        where T : IEventData
    {
        T? EventData { get; set; }

        string? Topic { get; set; }

        DateTime CreatedUtc { get; set; }

        string? CorrelationId { get; set; }

        EventTypeId EventTypeId { get; set; }
    }

    public interface IEvent : IEvent<IEventData>
    {
    }
}

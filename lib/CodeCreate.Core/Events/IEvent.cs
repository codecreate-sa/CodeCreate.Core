namespace CodeCreate.Events
{
    using System;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEvent<T>
        where T : IEventData
    {
        /// <summary>
        ///
        /// </summary>
        T? EventData { get; set; }

        /// <summary>
        ///
        /// </summary>
        DateTime CreatedUtc { get; set; }

        /// <summary>
        ///
        /// </summary>
        string? CorrelationId { get; set; }

        /// <summary>
        ///
        /// </summary>
        EventTypeId EventTypeId { get; set; }
    }

    public interface IEvent : IEvent<IEventData>
    {
    }
}

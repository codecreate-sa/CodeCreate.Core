namespace CodeCreate.Logging
{
    /// <summary>
    /// The LogEntry class
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// The message of the log entry
        /// </summary>
        public string Message { get; set; } = default!;

        /// <summary>
        /// The app event id of the log entry
        /// </summary>
        public int? AppEventId { get; set; } = default!;

        /// <summary>
        /// The correlation id of the log entry
        /// </summary>
        public string CorrelationId { get; set; } = default!;
    }
}

namespace CodeCreate.Logging.Serilog.Constants
{
    /// <summary>
    /// 
    /// </summary>
    public static class MsSqlServerSink
    {
        /// <summary>
        ///
        /// </summary>
        public const string TableName = "Logs";

        /// <summary>
        ///
        /// </summary>
        public const string SchemaName = "diagnostics";

        /// <summary>
        ///
        /// </summary>
        public const int BatchPostingLimit = 10;

        /// <summary>
        ///
        /// </summary>
        public const string CorrelationIdColumn = "CorrelationId";

        /// <summary>
        ///
        /// </summary>
        public const string AppEventIdColumn = "AppEventId";
    }
}

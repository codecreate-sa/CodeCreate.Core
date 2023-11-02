namespace CodeCreate.Logging.Serilog
{
    /// <summary>
    /// The CodeCreate.Logging.Serilog library constants
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The LogMessageTemplate constant
        /// </summary>
        public const string LogMessageTemplate = "{message} {@data} {AppEventId} {CorrelationId}";

        /// <summary>
        /// The MSSqlServerSinkOptions constants
        /// </summary>
        public static class MSSqlServerSink
        {
            /// <summary>
            /// The TableName constant
            /// </summary>
            public const string TableName = "Logs";

            /// <summary>
            /// The SchemaName constant
            /// </summary>
            public const string SchemaName = "diagnostics";

            /// <summary>
            /// The BatchPostingLimit constant
            /// </summary>
            public const int BatchPostingLimit = 10;
        }

        /// <summary>
        /// The SqlColumn name constants
        /// </summary>
        public static class SqlColumnName
        {
            /// <summary>
            /// The CorrelationId constant
            /// </summary>
            public const string CorrelationId = "CorrelationId";

            /// <summary>
            /// The AppEventId constant
            /// </summary>
            public const string AppEventId = "AppEventId";
        }
    }
}

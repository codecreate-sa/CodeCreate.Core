using System;
using System.Data;
using System.Collections.Generic;

using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace CodeCreate.Logging.Serilog
{
    /// <summary>
    /// The MsSqlServerSinkFactory class
    /// </summary>
    public static class MsSqlServerSinkFactory
    {
        /// <summary>
        /// The CreateDefaultDatabaseLogger method
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static LoggerConfiguration CreateDefaultDatabaseLogger(string connectionString)
        {
            if (connectionString == null) 
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            var sinkOptions = new MSSqlServerSinkOptions 
            {
                TableName = Constants.MsSqlServerSink.TableName,
                SchemaName = Constants.MsSqlServerSink.SchemaName,
                BatchPostingLimit = Constants.MsSqlServerSink.BatchPostingLimit
            };

            var columnOpts = new ColumnOptions();
            columnOpts.TimeStamp.DataType = SqlDbType.DateTimeOffset;

            // LogEvent is in JSON format
            columnOpts.Store.Add(StandardColumn.LogEvent);
            columnOpts.Store.Remove(StandardColumn.Properties);
            columnOpts.Store.Remove(StandardColumn.MessageTemplate);

            columnOpts.AdditionalColumns = new List<SqlColumn>
            {
                new SqlColumn
                {
                    AllowNull = true,
                    ColumnName = Constants.MsSqlServerSink.CorrelationIdColumn,
                    DataType = SqlDbType.NVarChar
                },
                new SqlColumn
                {
                    AllowNull = true,
                    ColumnName = Constants.MsSqlServerSink.AppEventIdColumn,
                    DataType = SqlDbType.Int
                }
            };

            return new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .Enrich.FromLogContext()
                .WriteTo.MSSqlServer(
                    connectionString: connectionString,
                    sinkOptions: sinkOptions,
                    columnOptions: columnOpts
                );
        }
    }
}

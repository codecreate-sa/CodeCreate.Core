using System;
using System.Collections.Generic;
using System.Data;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog;

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
                TableName = Constants.MSSqlServerSink.TableName,
                SchemaName = Constants.MSSqlServerSink.SchemaName,
                BatchPostingLimit = Constants.MSSqlServerSink.BatchPostingLimit
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
                    ColumnName = Constants.SqlColumnName.CorrelationId,
                    DataType = SqlDbType.NVarChar
                },
                new SqlColumn
                {
                    AllowNull = true,
                    ColumnName = Constants.SqlColumnName.AppEventId,
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

using System;
using System.Collections.Generic;
using System.Data;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using CodeCreate.Logging.Serilog.Constants;

namespace CodeCreate.Logging.Serilog
{
    /// <summary>
    ///  
    /// </summary>
    public static class MsSqlServerSinkFactory
    {
        /// <summary>
        /// 
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
                TableName = MsSqlServerSink.TableName,
                SchemaName = MsSqlServerSink.SchemaName,
                BatchPostingLimit = MsSqlServerSink.BatchPostingLimit
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
                    ColumnName = MsSqlServerSink.CorrelationIdColumn,
                    DataType = SqlDbType.NVarChar
                },
                new SqlColumn
                {
                    AllowNull = true,
                    ColumnName = MsSqlServerSink.AppEventIdColumn,
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

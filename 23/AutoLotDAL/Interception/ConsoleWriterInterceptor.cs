using System;
using static System.Console;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLotDAL.Interception
{
    //c Add ConsoleWriterInterceptor type providing Interception showing SQL queries sent to DB and register Interceptor in the constructor of my context class.
    //Interceptor code can be located either in code or config file.
    //It has more advantages such as convinient in config file.
    public class ConsoleWriterInterceptor : IDbCommandInterceptor
    {
        public void NonQueryExecuting(DbCommand command,
          DbCommandInterceptionContext<int> interceptionContext)
        {
            WriteInfo(interceptionContext.IsAsync, command.CommandText);
        }

        public void NonQueryExecuted(DbCommand command,
          DbCommandInterceptionContext<int> interceptionContext)
        {
            WriteInfo(interceptionContext.IsAsync, command.CommandText);
        }

        public void ReaderExecuting(DbCommand command,
          DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            WriteInfo(interceptionContext.IsAsync, command.CommandText);
        }

        public void ReaderExecuted(DbCommand command,
          DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            WriteInfo(interceptionContext.IsAsync, command.CommandText);
        }

        public void ScalarExecuting(DbCommand command,
          DbCommandInterceptionContext<object> interceptionContext)
        {
            WriteInfo(interceptionContext.IsAsync, command.CommandText);
        }
        public void ScalarExecuted(DbCommand command,
          DbCommandInterceptionContext<object> interceptionContext)
        {
            WriteInfo(interceptionContext.IsAsync, command.CommandText);
        }

        private void WriteInfo(bool isAsync, string commandText)
        {
            WriteLine($"IsAsync: {isAsync}, Command Text: {commandText}");
        }
    }
}

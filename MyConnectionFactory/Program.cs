using System;
using static System.Console;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConnectionFactory
{
    // A list of possible providers.
    enum DataProvider
    { SqlServer, OleDb, Odbc, None }

    class Program
    {

        static void Main(string[] args)
        {
            WriteLine("**** Very Simple Connection Factory *****\n");
            // Get a specific connection
            Console.WriteLine("Write your choice by integer among 4 DataProviders : 0.SqlServer 1.OleDb 2.Odbc 3.None");
            string ChosenDataProvider = Console.ReadLine();
            DataProvider ChosenDataProviderInEnum = (DataProvider)Enum.Parse(typeof(DataProvider), ChosenDataProvider, true);
            IDbConnection myConnection = GetConnection(ChosenDataProviderInEnum);
            WriteLine($"Your connection is a {myConnection.GetType().Name}");
            // Open, use and close connection
            ReadLine();
        }


        // I can choose a specific connection object by passing enum value of DataProvider
        static IDbConnection GetConnection(DataProvider dataProvider)
        {
            IDbConnection connection = null;
            switch (dataProvider)
            {
                case DataProvider.SqlServer:
                    // Get a SqlConnection object via switch statement
                    connection = new SqlConnection();
                    break;
                case DataProvider.OleDb:
                    connection = new OleDbConnection();
                    break;
                case DataProvider.Odbc:
                    connection = new OdbcConnection();
                    break;
            }
            return connection;
        }
    }
}

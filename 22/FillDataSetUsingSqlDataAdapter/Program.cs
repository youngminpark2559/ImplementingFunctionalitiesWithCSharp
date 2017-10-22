using System;
using static System.Console;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Added a console project FillDataSetUsingSqlDataAdapter using DataAdapter to set DataSet by putting DataTables in DataSet.
namespace FillDataSetUsingSqlDataAdapter
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("***** Fun with Data Adapters *****\n");

            // Hard-coded connection string.
            string connectionString = "Integrated Security = SSPI;Initial Catalog=AutoLot;" +
              @"Data Source=.\SQLEXPRESS";

            //Create the DataSet object with passing AutoLot literal.
            DataSet ds = new DataSet("AutoLot");

            //I use DataAdapter class in SQL version which I'm using.
            //Inform adapter of the Select command query text and connection information.
            SqlDataAdapter adapter = new SqlDataAdapter("Select * From Inventory", connectionString);

            // Fill our DataSet(ds) with a new table named Inventory.
            adapter.Fill(ds, "Inventory");

            // Display contents of DataSet.
            //PrintDataSet(ds);
        }
    }
}

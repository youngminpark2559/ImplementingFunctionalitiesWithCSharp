using System;
using static System.Console;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

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
            //Inform adapter of the Select command query text which is mapped to SQL statement and connection information.
            //
            SqlDataAdapter adapter = new SqlDataAdapter("Select * From Inventory", connectionString);

            // Fill our DataSet(ds) with a new table named Inventory.
            adapter.Fill(ds, "Inventory");

            // Display contents of DataSet.
            PrintDataSet(ds);
        }



        //Added a PrintDataSet(DataSet ds) to show all records of each DataTable from DataSet which PrintDataSet() caller put.
        static void PrintDataSet(DataSet ds)
        {
            // Print out DataSet name which PrintDataSet caller passed in.
            WriteLine($"DataSet is named: {ds.DataSetName}");

            //Print out all extended properties.
            foreach (DictionaryEntry de in ds.ExtendedProperties)
            {
                WriteLine($"Key = {de.Key}, Value = {de.Value}");
            }

            WriteLine();

            //Get all tables from DataSet.
            foreach (DataTable dt in ds.Tables)
            {
                WriteLine($"=> {dt.TableName} Table:");

                // Print out the column names.
                for (int curCol = 0; curCol < dt.Columns.Count; curCol++)
                {
                    Write(dt.Columns[curCol].ColumnName + "\t");
                }

                WriteLine("\n----------------------------------");

                // Print the DataTable.
                for (int curRow = 0; curRow < dt.Rows.Count; curRow++)
                {
                    for (int curCol = 0; curCol < dt.Columns.Count; curCol++)
                    {
                        Write(dt.Rows[curRow][curCol].ToString().Trim() + "\t");
                    }
                    WriteLine();
                }
            }
        }
    }
}

using DataGridViewDataDesigner.DataSets;
using DataGridViewDataDesigner.DataSets.AutoLotDataSetTableAdapters;
using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StronglyTypedDataSetConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Strongly Typed DataSets *****\n");

            // Caller creates the DataSet object.
            var table = new AutoLotDataSet.InventoryDataTable();

            // Inform adapter of the Select command text and connection.
            var adapter = new InventoryTableAdapter();

            // Fill our DataSet with a new table, named Inventory.
            adapter.Fill(table);

            PrintInventory(table); Console.ReadLine();
        }
        static void PrintInventory(AutoLotDataSet.InventoryDataTable dt)
        {
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
                    Write(dt.Rows[curRow][curCol] + "\t");
                }
                WriteLine();
            }
        }
    }
}

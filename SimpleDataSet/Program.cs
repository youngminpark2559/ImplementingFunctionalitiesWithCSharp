using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SimpleDataSet
{
    class Program
    {
        static void FillDataSet(DataSet ds)
        {
            // Create data columns that map to the
            // "real" columns in the Inventory table
            // of the AutoLot database.
            var carIDColumn = new DataColumn("CarID", typeof(int))
            {
                //Set the settings of CarIDCoumn object and it affects the real CarID colmun in DB
                Caption = "Car ID",
                ReadOnly = true,
                AllowDBNull = false,
                Unique = true,
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1
            };

            var carMakeColumn = new DataColumn("Make", typeof(string));
            var carColorColumn = new DataColumn("Color", typeof(string));
            var carPetNameColumn = new DataColumn("PetName", typeof(string))
            { Caption = "Pet Name" };

            // Now add DataColumns to a Inventory DataTable which resides in memory.
            var inventoryTable = new DataTable("Inventory");
            // After this, Inventory DataTable object contains four DataColumn objects
            // such as carIDColumn, carMakeColumn, carColorColumn, carPetNameColumn
            // which are all still in memory
            inventoryTable.Columns.AddRange(new[]
              {carIDColumn, carMakeColumn, carColorColumn, carPetNameColumn});


        }


        //Method to inspect RowState of Temp DataTable
        private static void ManipulateDataRowState()
        {
            // Create a temp DataTable for testing in memory
            var temp = new DataTable("Temp");
            temp.Columns.Add(new DataColumn("TempColumn", typeof(int)));

            // RowState = Detached.
            var row = temp.NewRow();
            WriteLine($"After calling NewRow(): {row.RowState}");

            // RowState = Added.
            temp.Rows.Add(row);
            WriteLine($"After calling Rows.Add(): {row.RowState}");

            // RowState = Added.
            row["TempColumn"] = 10;
            WriteLine($"After first assignment: {row.RowState}");

            // RowState = Unchanged.
            temp.AcceptChanges();
            WriteLine($"After calling AcceptChanges: {row.RowState}");

            // RowState = Modified.
            row["TempColumn"] = 11;
            WriteLine($"After first assignment: {row.RowState}");

            // RowState = Deleted.
            temp.Rows[0].Delete();
            WriteLine($"After calling Delete: {row.RowState}");
        }

        static void Main(string[] args)
        {
            WriteLine("***** Fun with DataSets *****\n");

            // Create the DataSet object named Car Inventory and add a few properties to that DataSet
            var carsInventoryDS = new DataSet("Car Inventory");

            carsInventoryDS.ExtendedProperties["TimeStamp"] = DateTime.Now;
            carsInventoryDS.ExtendedProperties["DataSetID"] = Guid.NewGuid();
            carsInventoryDS.ExtendedProperties["Company"] =
              "Mikko’s Hot Tub Super Store";

            FillDataSet(carsInventoryDS);
            PrintDataSet(carsInventoryDS);

            ReadLine();
        }
    }
}

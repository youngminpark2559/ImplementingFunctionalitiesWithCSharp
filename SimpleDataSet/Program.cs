using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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






            // Added the functionality to deal with process of setting row of DataTable
            // Now add some "rows" to the Inventory Table in memory.
            DataRow carRow = inventoryTable.NewRow();
            // Using indexer to set values, and if I put an invalid column name or ordinal position, I get a runtime exception
            carRow["Make"] = "BMW";
            carRow["Color"] = "Black";
            carRow["PetName"] = "Hamlet";
            //Put one row which I set above
            inventoryTable.Rows.Add(carRow);

            //Creating a new row
            carRow = inventoryTable.NewRow();
            //This time, using array to set row values
            // Column 0 is the autoincremented ID field,
            // so start at 1.
            carRow[1] = "Saab";
            carRow[2] = "Red";
            carRow[3] = "Sea Breeze";
            inventoryTable.Rows.Add(carRow);
            //After above, I have a single DataTable containing two rows

            // Set the Column[0] of Inventory DataTable as a primary
            inventoryTable.PrimaryKey = new[] { inventoryTable.Columns[0] };


            //Added a statement to insert Inventory DataTable to a specific DataSet
            // Finally, add our table to the DataSet.
            //DataSet is consists of DataTables
            //I'm adding a Inventory DataTable to DataSet
            //by invoking FillDataSet(DataSet ds), with passing DataSet type object
            //which will be a specific DataSet in which I'm going to insert Inventory DataTable
            ds.Tables.Add(inventoryTable);
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



        ////Added a PrintDataSet(DataSet ds) to show DataSet by using DataSet properties
        ////This method can be replaced with below PrintDataSet(DataSet ds) for better performance and simplicity by using DataTableReader object in retrieving data from DataTable
        //static void PrintDataSet(DataSet ds)
        //{
        //    // Print out the DataSet name and any extended properties.
        //    WriteLine($"DataSet is named: {ds.DataSetName}");
        //    foreach (DictionaryEntry de in ds.ExtendedProperties)
        //    {
        //        WriteLine($"Key = {de.Key}, Value = {de.Value}");
        //    }
        //    WriteLine();

        //    // Print out each table using rows and columns.
        //    //Now, I only have "Inventory" DataTable in "Car Inventory" DataSet.
        //    foreach (DataTable dt in ds.Tables)
        //    {
        //        WriteLine($"=> {dt.TableName} Table:");

        //        // Print out the column names.
        //        for (var curCol = 0; curCol < dt.Columns.Count; curCol++)
        //        {
        //            //Show Columns horizontally.
        //            //Columns[0].ColumnName \t Columns[1].ColumnName
        //            //from each DataTable.
        //            //Now, I only have "Inventory" DataTable in "Car Inventory" DataSet.
        //            Write($"{dt.Columns[curCol].ColumnName}\t");
        //        }
        //        WriteLine("\n----------------------------------");

        //        // Print the DataTable.
        //        //This logic is inside of foreach (DataTable dt in ds.Tables),
        //        //so that this logic shows row values of a specific one DataTable.
        //        //Row contains a set of values like  BMW, Black, Hamlet
        //        //I'm going to show each value in the row
        //        for (var curRow = 0; curRow < dt.Rows.Count; curRow++)
        //        {
        //            for (var curCol = 0; curCol < dt.Columns.Count; curCol++)
        //            {
        //                //dr.Rows[0][0] = BMW \t dr.Rows[0][1] = Black \t dr.Rows[0][2] = Hamlet
        //                //dt.Columns.Count=3
        //                //dr.Rows[1][0] = Saab \t dr.Rows[1][1] = Red \t dr.Rows[1][2] = Sea Breeze
        //                Write($"{dt.Rows[curRow][curCol]}\t");
        //            }
        //            WriteLine();
        //        }
        //    }
        //}


        //This is updated version of PrintDataSet() invoking PrintTable() helper method using DataTableReader type for retrieving data from DataTable.
        //Result is identical with above pre-updated PrintDataSet()
        static void PrintDataSet(DataSet ds)
        {
            // Print out any name and extended properties.
            WriteLine($"DataSet is named: {ds.DataSetName}");
            foreach (DictionaryEntry de in ds.ExtendedProperties)
            {
                WriteLine($"Key = {de.Key}, Value = {de.Value}");
            }
            WriteLine();

            //Print out each table using data reader
            foreach (DataTable dt in ds.Tables)
            {
                WriteLine($"=> {dt.TableName} Table:");
                // Print out the column names.
                for (int curCol = 0; curCol < dt.Columns.Count; curCol++)
                {
                    Write($"{dt.Columns[curCol].ColumnName.Trim()}\t");
                }
                WriteLine("\n----------------------------------");

                // Call our new helper method.
                PrintTable(dt);
            }
        }




        //Added PrintTable(DataTable dt) to retrieve data from DataTable by using DataTableReader object and updated PrintDataSet(DataSet ds) using DataTableReader

        //Processing DataTable data by using DataTableReader object.
        //It's different between retrieving data from connected layer and disconnected layer.
        //For example, connection to the DB is needed for connected layer.
        //But the process or result is identical.
        //This method retrieves data from DataTable from disconnected layer.
        static void PrintTable(DataTable dt)
        {
            // Get the DataTableReader type.
            DataTableReader dtReader = dt.CreateDataReader();

            // The DataTableReader works just like the DataReader of data provider in connected layer.
            // DataTableReader is good choice when I retrieve data from DataTable without iteration of row and column.
            //Read record until there is no record.
            while (dtReader.Read())
            {
                for (var i = 0; i < dtReader.FieldCount; i++)
                {
                    //dtReader.GetValue(0) \t dtReader.GetValue(1) \t
                    Write($"{dtReader.GetValue(i).ToString().Trim()}\t");
                }
                WriteLine();
            }
            dtReader.Close();
        }




        //Added SaveAndLoadAsXml(DataSet carsInventoryDS) to serialize DataTable and DataSet objects to XML file.
        //This is method to serialize DataTable and DataSet object to XML.
        //I'm passing in DataSet object to this method.
        static void SaveAndLoadAsXml(DataSet carsInventoryDS)
        {
            //Save this carsInventoryDS DataSet to carsDataSet.xml file in bin\debug.
            carsInventoryDS.WriteXml("carsDataSet.xml");
            //Save this carsInventoryDS DataSet to carsDataSet.xsd file in bin\debug.
            carsInventoryDS.WriteXmlSchema("carsDataSet.xsd");

            // Clear out carsInventoryDS DataSet.
            carsInventoryDS.Clear();

            // Load carsInventoryDS DataSet from XML file.
            carsInventoryDS.ReadXml("carsDataSet.xml");
        }




        //Added SaveAndLoadAsBinary(DataSet carsInventoryDS) to serialize carsInventoryDS DataSet object to binary type and save it to BinaryCars.bin file.

        //Serializing object to XML format can cause overhead due to its descriptive nature.
        //Serializing in the binary format is good alternative for XML format in terms of overhead aspect and the case that needs cross machine boundary.
        static void SaveAndLoadAsBinary(DataSet carsInventoryDS)
        {
            // Set binary serialization flag(SerializationFormat.Binary) 
            //and assign it to carsInventoryDS.RemotingFormat(Car Inventory DataSet's property).
            carsInventoryDS.RemotingFormat = SerializationFormat.Binary;

            // Set the FileStream defining it's going to create a BinaryCars.bin file
            var fs = new FileStream("BinaryCars.bin", FileMode.Create);

            //Get BinaryFormatter object.
            var bFormat = new BinaryFormatter();

            //Serialize carsInventoryDS object and save serialized data to BinaryCars.bin file
            bFormat.Serialize(fs, carsInventoryDS);

            //Close FileStream
            fs.Close();

            // Clear out DataSet.
            carsInventoryDS.Clear();

            // Load DataSet from binary file.
            fs = new FileStream("BinaryCars.bin", FileMode.Open);

            //By using BinaryFormatter object, I deserialize BinaryCars.bin, and construct carsInventoryDS object in object type, and then explicitly type cast to DataSet.
            var data = (DataSet)bFormat.Deserialize(fs);
        }


        static void Main(string[] args)
        {
            WriteLine("***** Fun with DataSets *****\n");

            // Create the DataSet object named Car Inventory and add a few properties to that DataSet
            //which resides in memory
            var carsInventoryDS = new DataSet("Car Inventory");

            carsInventoryDS.ExtendedProperties["TimeStamp"] = DateTime.Now;
            carsInventoryDS.ExtendedProperties["DataSetID"] = Guid.NewGuid();
            carsInventoryDS.ExtendedProperties["Company"] =
              "Mikko’s Hot Tub Super Store";

            //Added comments for FillDataSet(carsInventoryDS), var carsInventoryDS = new DataSet("Car Inventory")
            //I'm invoking FillDataSet(carsInventoryDS), with passing in carsInventoryDS which will be a specific DataSet which will contain Inventory DataTable after I call ds.Tables.Add(inventoryTable)
            FillDataSet(carsInventoryDS);
            PrintDataSet(carsInventoryDS);

            SaveAndLoadAsXml(carsInventoryDS);
            //Output
            //<?xml version="1.0" standalone="yes"?>
            //<Car_x0020_Inventory>
            //  <Inventory>
            //    <CarID>1</CarID>
            //    <Make>BMW</Make>
            //    <Color>Black</Color>
            //    <PetName>Hamlet</PetName>
            //  </Inventory>
            //  <Inventory>
            //    <CarID>2</CarID>
            //    <Make>Saab</Make>
            //    <Color>Red</Color>
            //    <PetName>Sea Breeze</PetName>
            //  </Inventory>
            //</Car_x0020_Inventory>


            SaveAndLoadAsBinary(carsInventoryDS);
        }
    }
}
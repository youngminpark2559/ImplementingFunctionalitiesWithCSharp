using AutoLotConsoleApp.EF;
using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AutoLotConsoleApp
{
    class Program
    {
        //Added a helper AddNewRecord() to insert a new record by using EF.
        private static int AddNewRecord()
        {
            // Add record to the Inventory table of the AutoLot database.
            using (var context = new AutoLotEntities())
            {
                try
                {
                    // Hard-code data for a new record, for testing.
                    var car = new Car() { Make = "Yugo", Color = "Brown", CarNickName = "Brownie" };

                    //DbSet<TEntity> has Add().
                    //With this, I add car object which is created above to Cars property.
                    //And it is stored in memory in DbSet<Car> type.
                    //In memory, DbSet<Car> = car1, car2, ..
                    context.Cars.Add(car);

                    //I put DbSet<Car> which is put by new car just above, when I SaveChanges() of DbContext, EF does that task on behalf of myself.
                    //After finish this task, DB is changed.
                    context.SaveChanges();

                    // On a successful save, EF populates the database generated identity field.
                    //The CarId of the new record is showing.
                    //EF executes a SELECT statement on behalf of myself to get CarId value, even if I don't do anything to get that value from DB
                    return car.CarId;
                }
                catch (Exception ex)
                {
                    WriteLine(ex.InnerException.Message);
                    return 0;
                }
            }
        }


        ////Added a helper PrintAllInventory() to show all records in Inventory table with being helped by ToString() of Car class.
        //private static void PrintAllInventory()
        //{
        //    // Select all items from the Inventory table of AutoLot,
        //    // and print out the data using our custom ToString()
        //    // of the Car entity class.
        //    using (var context = new AutoLotEntities())
        //    {
        //        //Car class is decorated with [Table("Inventory")]
        //        //One Inventory object means one row of records corresponding to each columns,
        //        //for example, Inventory object 1 = Id=1, Name=Bim, Color=White, Make=BMW
        //        //DbSet<Inventory> is consists of multiple Inventory objects.
        //        foreach (Car c in context.Cars)
        //        {
        //            //Iterating record by Cars property sends SELECT query implicitly to the ADO.NET data provider in behind scene.
        //            //In other words, EF creates DataReader to retrieve records from DB, and converts the records from DataReader type to Car type.
        //            WriteLine(c);
        //        }
        //    }
        //}

        ////Added an updated helper PrintAllInventory() to fill DbSet with SQL query with using EF.
        ////It can be implemented either in inline or stored procedures.
        //private static void PrintAllInventory()
        //{
        //    using (var context = new AutoLotEntities())
        //    {
        //        //Uses inline SQL query
        //        foreach (Car c in context.Cars.SqlQuery("Select CarId,Make,Color,PetName as CarNickName from Inventory where Make = @p0", "BMW"))
        //        {
        //            WriteLine(c);
        //        }
        //    }
        //}


        //Updated a PrintAllInventory() with using LINQ to submit query in EF.
        //It's powerful and convinient when LINQ is used in EF when doing task.
        //LINQ is converted into SQL query which creates a DataReader, and returns records.
        private static void PrintAllInventory()
        {
            using (var context = new AutoLotEntities())
            {
                foreach (Car c in context.Cars.Where(c => c.Make == "BMW"))
                {
                    WriteLine(c);
                }
            }

        }


        //private static void FunWithLinqQueries()
        //{
        //    using (var context = new AutoLotEntities())
        //    {
        //        // Get a projection of new data by using anonymous type object.
        //        // a`[] = {{Blue, BMW}, {White, VW}, .. }
        //        var colorsMakes = from item in context.Cars
        //                          select new { item.Color, item.Make };

        //        foreach (var item in colorsMakes)
        //        {
        //            WriteLine(item);
        //        }

        //        // Get only items where Color == "Black"
        //        var blackCars = from item in context.Cars where item.Color == "Black" select item;
        //        foreach (var item in blackCars)
        //        {
        //            WriteLine(item);
        //        }
        //    }
        //}

        private static void FunWithLinqQueries()
        {
            using (var context = new AutoLotEntities())
            {
                // Get all data from the Inventory table.
                // Could also write:
                // var allData = (from item in context.Cars select item).ToArray();
                var allData = context.Cars.ToArray();

                // Get a projection of new data by using anonymous type object.
                // a`[] = {{Blue, BMW}, {White, VW}, .. }
                var colorsMakes = from item in context.Cars
                                  select new { item.Color, item.Make };

                foreach (var item in colorsMakes)
                {
                    WriteLine(item);
                }

                // Get only items where Color == "Black"
                var blackCars = from item in context.Cars where item.Color == "Black" select item;
                foreach (var item in blackCars)
                {
                    WriteLine(item);
                }
            }
        }




        //Added a helper RemoveRecord(int carId) to delete record in DB by marking "Deleted" to EntityState on an object from DbSet<Car> and then call context.SaveChanges()
        private static void RemoveRecord(int carId)
        {
            // Find a car to delete by primary key.
            using (var context = new AutoLotEntities())
            {
                // See if we have it, and if we have it, assign that Car object to carToDelete.
                Car carToDelete = context.Cars.Find(carId);

                if (carToDelete != null)
                {
                    //Delete a Car object which I found above with carId from DbSet<Cars>
                    context.Cars.Remove(carToDelete);

                    //Try to delete records corresponding to above object in DB.
                    context.SaveChanges();
                }
            }
        }




        //Added a helper RemoveRecordUsingEntityState(int carId) to delete records in DB by marking EntityState on an object which I want to delete, is found by carId.
        private static void RemoveRecordUsingEntityState(int carId)
        {
            using (var context = new AutoLotEntities())
            {
                Car carToDelete = new Car() { CarId = carId };
                context.Entry(carToDelete).State = EntityState.Deleted;
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    WriteLine(ex);
                }
            }
        }



        static void Main(string[] args)
        {
            WriteLine("***** Fun with ADO.NET EF *****\n");
            int carId = AddNewRecord();
            RemoveRecord(carId);
            //WriteLine(carId);
            //PrintAllInventory();
            //FunWithLinqQueries();
        }
    }
}

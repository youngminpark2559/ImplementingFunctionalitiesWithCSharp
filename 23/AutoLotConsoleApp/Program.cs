using AutoLotConsoleApp.EF;
using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        //Added a helper PrintAllInventory() to show all records in Inventory table with being helped by ToString() of Car class.
        private static void PrintAllInventory()
        {
            // Select all items from the Inventory table of AutoLot,
            // and print out the data using our custom ToString()
            // of the Car entity class.
            using (var context = new AutoLotEntities())
            {
                //Car class is decorated with [Table("Inventory")]
                //One Inventory object means one row of records corresponding to each columns,
                //for example, Inventory object 1 = Id=1, Name=Bim, Color=White, Make=BMW
                //DbSet<Inventory> is consists of multiple Inventory objects.
                foreach (Car c in context.Cars)
                {
                    WriteLine(c);
                }
            }
        }


        static void Main(string[] args)
        {
            WriteLine("***** Fun with ADO.NET EF *****\n");
            //int carId = AddNewRecord();
            //WriteLIne(carId);
            PrintAllInventory();
        }
    }
}

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
                    return car.CarId;
                }
                catch (Exception ex)
                {
                    WriteLine(ex.InnerException.Message);
                    return 0;
                }
            }
        }


        static void Main(string[] args)
        {
            WriteLine("***** Fun with ADO.NET EF *****\n");
            int carId = AddNewRecord();
            WriteLine(carId);
        }
    }
}

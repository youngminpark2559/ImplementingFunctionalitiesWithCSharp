using AutoLotDAL.EF;
using System;
using static System.Console;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLotDAL.Repos;
using AutoLotDAL.Models;

namespace AutoLotTestDrive
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DataInitializer());
            WriteLine("***** Fun with ADO.NET EF Code First *****\n");
            var car1 = new Inventory() { Make = "Yugo", Color = "Brown", PetName = "Brownie" };
            var car2 = new Inventory() { Make = "SmartCar", Color = "Brown", PetName = "Shorty" };
            AddNewRecord(car1);
            AddNewRecord(car2);
            AddNewRecords(new List<Inventory> { car1, car2 });
            PrintAllInventory();
        }



        private static void PrintAllInventory()
        {
            using (var repo = new InventoryRepo())
            {
                foreach (Inventory c in repo.GetAll())
                {
                    WriteLine(c);
                }
            }
        }






        //c Added a helper AddNewRecord(Inventory car) to insert new records, using disconnected layer(InventoryReop -> (IRepo) -> BaseRepo -> my context DbSet<Inventory> in memory) -> connected layer(DbContext -> Database)
        // Add record to the Inventory table of the AutoLot database.
        private static void AddNewRecord(Inventory car)
        {
            //First, add car object passed in by user by invoking Add() in BaseRepo,
            //and BaseRepo inserts car object into DbSet<Inventory> property storing values in memory,
            //and finally, within this scope of method, it calls SaveChanges() of DbContext which will do the database job. 
            using (var repo = new InventoryRepo())
            {
                repo.Add(car);
            }
        }
        private static void AddNewRecords(IList<Inventory> cars)
        {
            using (var repo = new InventoryRepo())
            {
                repo.AddRange(cars);
            }
        }
    }
}

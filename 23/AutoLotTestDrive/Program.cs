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
using System.Data.Entity.Infrastructure;

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
            UpdateRecord(car1.CarId);
            PrintAllInventory();
            ShowAllOrders();
            ShowAllOrdersEagerlyFetched();
            ReadLine();
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




        //Added a UpdateRecord(int carId) updating record in DB, using EF and repository pattern.
        private static void UpdateRecord(int carId)
        {
            using (var repo = new InventoryRepo())
            {
                //InventoryRepo object -> call GetOne() in IRepo? -> call GetOne() in BaseRepo -> get an object which was found by carId, is to be updated.
                var carToUpdate = repo.GetOne(carId);

                if (carToUpdate != null)
                {
                    WriteLine("Before change: " + repo.Context.Entry(carToUpdate).State);
                    carToUpdate.Color = "Blue";
                    WriteLine("After change: " + repo.Context.Entry(carToUpdate).State);
                    repo.Save(carToUpdate);
                    WriteLine("After save: " + repo.Context.Entry(carToUpdate).State);
                }
            }
        }

        //c Added ShowAllOrders() retrieving data from tables(Order, Inventory) which are related to Order table by using navigation property.
        //Use navigation property to retrieve data from tables(Order, Inventory) which are related to Order table.
        //In other word, Order class has navigation properties whose types are DbSet<Order>, DbSet<Inventory>
        private static void ShowAllOrders()
        {
            using (var repo = new OrderRepo())
            {
                WriteLine("*********** Pending Orders ***********");
                foreach (var itm in repo.GetAll())
                {
                    WriteLine($"->{itm.Customer.FullName} is waiting on {itm.Car.PetName}");
                }
            }
        }

        //Add ShowAllOrdersEagerlyFetched() to retrieve data including data with which are related to Order table(Customer, Car) by using Eager loading(All related data with one query).
        private static void ShowAllOrdersEagerlyFetched()
        {
            using (var context = new AutoLotEntities())
            {
                WriteLine("*********** Pending Orders ***********");
                //AutoLotEntities Orders property
                var orders = context.Orders
                  //Order class Customer property
                  .Include(x => x.Customer)
                  //Order class Car property
                  .Include(y => y.Car)
                  //Submit query to DB and get all data and assign them to orders.
                  .ToList();

                foreach (var itm in orders)
                {
                    //Order->Customer property->Customer class->FullName property.
                    WriteLine($"->{itm.Customer.FullName} is waiting on {itm.Car.PetName}");
                }
            }
        }








        //c Add MakeCustomerARisk(Customer customer) to move an customer object to CreditRisk table and remove from Customer table.
        //This is for transaction.
        //An customer object is moved to CreditRisk table and removed from Customer table
        private static CreditRisk MakeCustomerARisk(Customer customer)
        {
            using (var context = new AutoLotEntities())
            {
                context.Customers.Attach(customer);
                context.Customers.Remove(customer);
                var creditRisk = new CreditRisk()
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName
                };
                context.CreditRisks.Add(creditRisk);
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    WriteLine(ex);
                }
                catch (Exception ex)
                {
                    WriteLine(ex);
                }
                return creditRisk;
            }
        }




    }
}

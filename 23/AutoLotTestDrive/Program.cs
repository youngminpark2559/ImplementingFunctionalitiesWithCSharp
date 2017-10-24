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


//The manner EF checks and syncs betwenn entities and database.
//1. EF inspects _MigrationHitory
//2. EF compares hashes of current models to the most recent hash in tables.



namespace AutoLotTestDrive
{
    class Program
    {
        static void Main(string[] args)
        {
            //Database.SetInitializer(new DataInitializer());
            //WriteLine("***** Fun with ADO.NET EF Code First *****\n");
            //var car1 = new Inventory() { Make = "Yugo", Color = "Brown", PetName = "Brownie" };
            //var car2 = new Inventory() { Make = "SmartCar", Color = "Brown", PetName = "Shorty" };
            //AddNewRecord(car1);
            //AddNewRecord(car2);
            //AddNewRecords(new List<Inventory> { car1, car2 });
            //UpdateRecord(car1.CarId);
            //PrintAllInventory();
            //ShowAllOrders();
            //ShowAllOrdersEagerlyFetched();

            ////c Add invoking MakeCustomerARisk(), PrintAllCustomersAndCreditRisks() in Main() to test transaction.
            //WriteLine("***** Fun with ADO.NET EF Code First Transaction *****\n");
            //PrintAllCustomersAndCreditRisks();

            ////Get one CustomerRepo object.
            //var customerRepo = new CustomerRepo();

            ////Get a customer whose Id is 4.
            //var customer = customerRepo.GetOne(4);

            ////Set EntityState of this object to Detached.
            //customerRepo.Context.Entry(customer).State = EntityState.Detached;

            ////Move this customer to CreditRisk table with removing it from Customer table.
            //var risk = MakeCustomerARisk(customer);

            ////Check the change.
            //PrintAllCustomersAndCreditRisks();

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
                var creditRiskDupe = new CreditRisk()
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName
                };
                context.CreditRisks.Add(creditRiskDupe);
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    WriteLine(ex);
                }

                return creditRisk;
            }
        }




        private static void PrintAllCustomersAndCreditRisks()
        {
            WriteLine("*********** Customers ***********");
            using (var repo = new CustomerRepo())
            {
                foreach (var cust in repo.GetAll())
                {
                    WriteLine($"->{cust.FirstName} {cust.LastName} is a Customer.");
                }
            }
            WriteLine("*********** Credit Risks ***********");
            using (var repo = new CreditRiskRepo())
            {
                foreach (var risk in repo.GetAll())
                {
                    WriteLine($"->{risk.FirstName} {risk.LastName} is a Credit Risk!");
                }
            }
        }





        private static void UpdateRecordWithConcurrency()
        {
            var car = new Inventory()
            { Make = "Yugo", Color = "Brown", PetName = "Brownie" };
            AddNewRecord(car);
            var repo1 = new InventoryRepo();
            var car1 = repo1.GetOne(car.CarId);
            car1.PetName = "Updated";

            var repo2 = new InventoryRepo();
            var car2 = repo2.GetOne(car.CarId);
            car2.Make = "Nissan";

            repo1.Save(car1);
            try
            {
                repo2.Save(car2);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                WriteLine(ex);
            }
            RemoveRecordById(car1.CarId, car1.Timestamp);
        }






        private static void RemoveRecordById(int carId, byte[] timeStamp)
        {
            using (var repo = new InventoryRepo())
            {
                repo.Delete(carId, timeStamp);
            }
        }

    }
}

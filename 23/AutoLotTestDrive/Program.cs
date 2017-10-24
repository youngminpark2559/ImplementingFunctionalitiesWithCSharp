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
    }
}

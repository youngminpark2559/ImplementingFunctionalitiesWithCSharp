using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADONETTest;

namespace TransactionTestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("***** Simple Transaction Example *****\n");

            // A simple way to allow the tx to succeed or not.
            bool throwEx = true;


            // If you press Y, situation like error happening occurs, and then task for Database is rolled back, transaction failed.
            // If you press N, situation like normal process without errors occurs, and then task for Database is processed
            Write("Do you want to throw an exception (Y or N): ");
            var userAnswer = ReadLine();
            if (userAnswer?.ToLower() == "n")
            {
                throwEx = false;
            }

            var dal = new InventoryDAL();
            dal.OpenConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=SSPI;" +
              "Initial Catalog=AutoLot");

            // Process customer 5 – enter the id for Homer Simpson in the next line.
            dal.ProcessCreditRisk(throwEx, 6);
            WriteLine("Check CreditRisk table for results");
            ReadLine();
        }
    }
}

namespace AutoLotDAL.EF
{
    using AutoLotDAL.Interception;
    using AutoLotDAL.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure.Interception;
    using System.Linq;

    public class AutoLotEntities : DbContext
    {
        //c Add DatabaseLogger
        //1st parameter is the file name which I want to name for log file.
        //2ns parameter is the optional option deciding whether the log is appended.
        static readonly DatabaseLogger DatabaseLogger = new DatabaseLogger("sqllog.txt", true);

        public AutoLotEntities() : base("name=AutoLotConnection")
        {
            ////Register the Interception.
            //DbInterception.Add(new ConsoleWriterInterceptor());

            //Use static DatabaseLogger field containing DatabaseLogger object, to invoking StartLogging().
            DatabaseLogger.StartLogging();

            //Register the DatabaseLogger.
            DbInterception.Add(DatabaseLogger);
        }




        public virtual DbSet<CreditRisk> CreditRisks { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
    }


}
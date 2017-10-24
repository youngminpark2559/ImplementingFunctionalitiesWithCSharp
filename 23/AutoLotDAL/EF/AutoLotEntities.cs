namespace AutoLotDAL.EF
{
    using AutoLotDAL.Interception;
    using AutoLotDAL.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Infrastructure.Interception;
    using System.Linq;

    public class AutoLotEntities : DbContext
    {
        //c Add DatabaseLogger.
        //c Add ObjectMaterialized, SavingChanges events. 


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




            //Interceptor code
            var context = (this as IObjectContextAdapter).ObjectContext;
            context.ObjectMaterialized += OnObjectMaterialized;

            //SavingChanges fires after SaveChanges() is called(on the DbContext) but before the DB is updated.
            context.SavingChanges += OnSavingChanges;
        }

        private void OnSavingChanges(object sender, EventArgs eventArgs)
        {
            //Sender is of type ObjectContext.
            //Can get current and original values, and cancel/modify the save operation as desired.
            var context = sender as ObjectContext;
            if (context == null) return;
            foreach (ObjectStateEntry item in
              context.ObjectStateManager.GetObjectStateEntries(EntityState.Modified | EntityState.Added))
            {
                //Do something important here
                if ((item.Entity as Inventory) != null)
                {
                    var entity = (Inventory)item.Entity;
                    if (entity.Color == "Red")
                    {
                        item.RejectPropertyChanges(nameof(entity.Color));
                    }
                }
            }
        }

        private void OnObjectMaterialized(object sender,
        System.Data.Entity.Core.Objects.ObjectMaterializedEventArgs e)
        {
        }



        public virtual DbSet<CreditRisk> CreditRisks { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void Dispose(bool disposing)
        {
            DbInterception.Remove(DatabaseLogger);
            DatabaseLogger.StopLogging();
            base.Dispose(disposing);
        }
    }
}
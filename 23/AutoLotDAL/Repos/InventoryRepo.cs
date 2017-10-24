using AutoLotDAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLotDAL.Repos
{
    public class InventoryRepo : BaseRepo<Inventory>, IRepo<Inventory>
    {
        public InventoryRepo()
        {
            Table = Context.Inventory;
        }



        //c Update Delete(), DeleteAsync() in each *Repo.cs to accept byte[] timeStamp as a parameter,
        //which will be added to conditional SQL query statement such as WHERE clause.
        //For delete record from Inventory table, based on CarId which client put to delete.
        public int Delete(int id, byte[] timeStamp)
        {
            Context.Entry(new Inventory()
            {
                CarId = id,
                Timestamp = timeStamp
            }).State = EntityState.Deleted;
            return SaveChanges();
        }
        public Task<int> DeleteAsync(int id, byte[] timeStamp)
        {
            Context.Entry(new Inventory()
            {
                CarId = id,
                Timestamp = timeStamp
            }).State = EntityState.Deleted;
            return SaveChangesAsync();
        }
    }
}

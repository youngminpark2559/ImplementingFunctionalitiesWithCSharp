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




        //For delete record from Inventory table, based on CarId which client put to delete.
        public int Delete(int id)
        {
            Context.Entry(new Inventory() { CarId = id }).State = EntityState.Deleted;
            return SaveChanges();
        }

        public Task<int> DeleteAsync(int id)
        {
            Context.Entry(new Inventory() { CarId = id }).State = EntityState.Deleted;
            return SaveChangesAsync();
        }
    }
}

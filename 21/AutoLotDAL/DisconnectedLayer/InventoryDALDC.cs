using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLotDAL.DisconnectedLayer
{
    public class InventoryDALDC
    {
        private string _connectionString;
        private SqlDataAdapter _adapter = null;

        //I pass in connectionString when I instantiate InventoryDALDC object.
        public InventoryDALDC(string connectionString)
        {
            _connectionString = connectionString;

            // Configure the SqlDataAdapter.
            ConfigureAdapter(out _adapter);
        }



        private void ConfigureAdapter(out SqlDataAdapter adapter)
        {
            // Create the adapter(new SqlDataAdapter()) and set up the SelectCommand object
            //which contains query and connection information.
            adapter = new SqlDataAdapter("Select * From Inventory", _connectionString);

            // Obtain the remaining command objects dynamically at runtime
            // using the SqlCommandBuilder.
            //
            var builder = new SqlCommandBuilder(adapter);
        }


        public DataTable GetAllInventory()
        {
            //Create a new DataTable named Inventory in memory.
            DataTable inv = new DataTable("Inventory");

            //Fill DataSet with DataTable which is created above.
            _adapter.Fill(inv);

            return inv;
        }

        //Update() of data adapter object(_adapter) examines the RowState value of each row of modifiedTable DataTable whether they're marked as Added, Deleted, Modified, etc
        //and after finishing examination, command objects which is fit to RowState of DataTable are used to process DB task.
        public void UpdateInventory(DataTable modifiedTable)
        {
            _adapter.Update(modifiedTable);
        }
    }
}

using AutoLotDAL.DisconnectedLayer;
using System;
using System.Data;
using System.Windows.Forms;

//Added a Winforms project InventoryDALDisconnectedGUI to manipulate data of Inventory DataTable by using InventoryDALDC type of AutoLotDAL.dll in the disconnected manner.
namespace InventoryDALDisconnectedGUI
{
    public partial class MainForm : Form
    {
        //_dal stores an object of InventoryDALDC
        InventoryDALDC _dal = null;

        public MainForm()
        {
            InitializeComponent();

            string cnStr =
            @"Data Source=.\SQLEXPRESS;Initial Catalog=AutoLot;" +
             "Integrated Security=True;Pooling=False";

            //Create data access object by instantiating InventoryDALDC type with passing in connection information.
            _dal = new InventoryDALDC(cnStr);

            // Fill up our grid by Inventory DataTable records.
            inventoryGrid.DataSource = _dal.GetAllInventory();
        }

        private void btnUpdateInventory_Click(object sender, EventArgs e)
        {
            // Get modified data from the grid, and assign it to changedDT.
            DataTable changedDT = (DataTable)inventoryGrid.DataSource;

            try
            {
                // Commit our changes.
                _dal.UpdateInventory(changedDT);

                //Retrieve modified data and bind to DataGridView.
                inventoryGrid.DataSource = _dal.GetAllInventory();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
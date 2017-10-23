using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Added a Winforms project MultitabledDataSetApp to do tasks multi DataTables in DataSet and bind them to DataGridView.
namespace MultitabledDataSetApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            _connectionString =
            ConfigurationManager.ConnectionStrings["AutoLotSqlProvider"].ConnectionString;

            // Create adapters which will be passed in SqlCommandBuilder constructor parameter.
            _invTableAdapter =
            new SqlDataAdapter("Select * from Inventory", _connectionString);
            _custTableAdapter =
            new SqlDataAdapter("Select * from Customers", _connectionString);
            _ordersTableAdapter =
            new SqlDataAdapter("Select * from Orders", _connectionString);

            // Autogenerate commands.
            _sqlCbInventory = new SqlCommandBuilder(_invTableAdapter);
            _sqlCbOrders = new SqlCommandBuilder(_ordersTableAdapter);
            _sqlCbCustomers = new SqlCommandBuilder(_custTableAdapter);

            // Fill tables in DataSet.
            _invTableAdapter.Fill(_autoLotDs, "Inventory");
            _custTableAdapter.Fill(_autoLotDs, "Customers");
            _ordersTableAdapter.Fill(_autoLotDs, "Orders");

            // Build relations between tables.
            //BuildTableRelationship();

            // Bind to grids
            dataGridViewInventory.DataSource = _autoLotDs.Tables["Inventory"];
            dataGridViewCustomers.DataSource = _autoLotDs.Tables["Customers"];
            dataGridViewOrders.DataSource = _autoLotDs.Tables["Orders"];
        }

        // Create DataSet named AutoLot in disconnected manner.
        private DataSet _autoLotDs = new DataSet("AutoLot");

        // Form wide connection string.
        private string _connectionString;

        // Make use of command builders to simplify data adapter configuration.
        //SqlCommandBuilder for Inventory.
        private SqlCommandBuilder _sqlCbInventory;
        private SqlCommandBuilder _sqlCbCustomers;
        private SqlCommandBuilder _sqlCbOrders;

        // Our data adapters (for each table).
        //SqlDataAdapter for Inventory DataTable.
        private SqlDataAdapter _invTableAdapter;
        private SqlDataAdapter _custTableAdapter;
        private SqlDataAdapter _ordersTableAdapter;


        //Added a BuildTableRelationship()
        private void BuildTableRelationship()
        {
            //Create a DataRelation object CustomerOrder defining Customer-Order data relation.
            DataRelation dr = new DataRelation("CustomerOrder",
                _autoLotDs.Tables["Customers"].Columns["CustID"],
                _autoLotDs.Tables["Orders"].Columns["CustID"]);
            _autoLotDs.Relations.Add(dr);

            //Create a DataRelation object InventoryOrder defining Inventory-Order data relation.
            dr = new DataRelation("InventoryOrder",
                _autoLotDs.Tables["Inventory"].Columns["CarID"],
                _autoLotDs.Tables["Orders"].Columns["CarID"]);
            _autoLotDs.Relations.Add(dr);
        }


    }
}

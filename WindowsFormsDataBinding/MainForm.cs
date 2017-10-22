


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//Added a winforms project "WindowsFormsDataBinding" to bind data from DataTable to DataGridView control
namespace WindowsFormsDataBinding
{


    public partial class MainForm : Form
    {
        //Declare listCars field(=global variable, member variable) which will contains Car objects. 
        List<Car> listCars = null;


        //Get a DataTable object, and assign it to inventoryTable field.
        DataTable inventoryTable = new DataTable();






        //Add a helper method.
        void CreateDataTable()
        {
            // Create table schema by getting DataColumn object by instantiating it with passing values.
            //I don't need to set Id record, if I set Id column as a primary key and auto-incrementing behavior
            var carIDColumn = new DataColumn("Id", typeof(int));
            var carMakeColumn = new DataColumn("Make", typeof(string));
            var carColorColumn = new DataColumn("Color", typeof(string));
            var carPetNameColumn = new DataColumn("PetName", typeof(string))
            { Caption = "Pet Name" };

            //Add columns which are set above code into inventoryTable DataTable,
            //passing in an anonymous array which contains each column object
            inventoryTable.Columns.AddRange(
              new[] { carIDColumn, carMakeColumn, carColorColumn, carPetNameColumn });


            //Iterate over the List<Car> and make rows.
            //I can use foreach functionality because listCar is generic List type,
            //so that it implements IEnumerable.
            foreach (var c in listCars)
            {
                //Generated one row example with column.
                //Id = 1, PetName = "Chucky", Make = "BMW", Color = "Green" 
                var newRow = inventoryTable.NewRow();
                newRow["Id"] = c.Id;
                newRow["Make"] = c.Make;
                newRow["Color"] = c.Color;
                newRow["PetName"] = c.PetName;
                inventoryTable.Rows.Add(newRow);
            }
            //With those code, finished to set DataTable, column&schema, row&data


            // Bind the inventoryTable DataTable to the DataSource of carInventoryGridView control.
            carInventoryGridView.DataSource = inventoryTable;
        }




        public MainForm()
        {
            InitializeComponent();

            // Fill the List with car objects, and then I assign it to the listCars.
            listCars = new List<Car>
            {
                new Car { Id = 1, PetName = "Chucky", Make = "BMW", Color = "Green" },
                new Car { Id = 2, PetName = "Tiny", Make = "Yugo", Color = "White" },
                new Car { Id = 3, PetName = "Ami", Make = "Jeep", Color = "Tan" },
                new Car { Id = 4, PetName = "Pain Inducer", Make = "Caravan", Color = "Pink" },
                new Car { Id = 5, PetName = "Fred", Make = "BMW", Color = "Green" },
                new Car { Id = 6, PetName = "Sidd", Make = "BMW", Color = "Black" },
                new Car { Id = 7, PetName = "Mel", Make = "Firebird", Color = "Red" },
                new Car { Id = 8, PetName = "Sarah", Make = "Colt", Color = "Black" }
            };

            CreateDataTable();
        }


        //Added a btnRemoveCar_Click() to delete a row, based on Id value which user puts.
        // Remove this row from the DataRowCollection of DataTable in memory.
        private void btnRemoveCar_Click(object sender, EventArgs e)
        {
            try
            {
                // Find the correct row to delete based on value which user put in txtCarToRemove textbox.
                //Invoke Select method of inventoryTable DataTable object by passing string value like Id=2
                //and I assign selected row to the rowToDelete local variable which is DataRow array type.
                DataRow[] rowToDelete = inventoryTable.Select($"Id={int.Parse(txtCarToRemove.Text)}");

                // Delete 1st row from DataTable in memory by invoking Delete() of DataRow type
                rowToDelete[0].Delete();


                //Invoke AcceptChanges() of inventoryTable DataTable to apply change to the DataTable. 
                inventoryTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}


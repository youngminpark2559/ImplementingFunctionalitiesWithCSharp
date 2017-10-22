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

        // View of the DataTable.
        DataView yugosOnlyView;




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


        //Added a CreateDataView() to configure DataView by getting DataTable, setting RowFilter, and bind data to DataView control.
        private void CreateDataView()
        {
            // Set the table(inventoryTable) that is used to construct this view(DataView yugosOnlyView).
            yugosOnlyView = new DataView(inventoryTable);

            //Configure the RowFilter of yugosOnlyView DataView to Make = 'Yugo'
            yugosOnlyView.RowFilter = "Make = 'Yugo'";

            // Bind to the new grid.
            //Completed setting of yugosOnlyView DataView is bound to DataSource of dataGridYugosView control.
            dataGridYugosView.DataSource = yugosOnlyView;
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

            // Make a data table.
            CreateDataTable();

            // Make a view.
            CreateDataView();
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


        //Added a btnDisplayMakes_Click() to select and retrieve data, based on string value in Make like BMW
        private void btnDisplayMakes_Click(object sender, EventArgs e)
        {
            //Get a string from txtMakeToView and make string like Make='BMW"
            //and assign it to filterStr local variable.
            string filterStr = $"Make='{txtMakeToView.Text}'";

            // Find all rows matching the filter which is "I want all rows which contain BMW in their Make column,
            //and then, assign row(s) to makes local variable of DataRow array type.
            //If I call Select() of DataTable object, it's process like it's based on standard SQL syntax.
            //DataRow[] makes = inventoryTable.Select(filterStr);

            // Sort by PetName in ascending order.
            //DataRow[] makes = inventoryTable.Select(filterStr, "PetName");

            // Sort by PetName in descending order.
            DataRow[] makes = inventoryTable.Select(filterStr, "PetName DESC");

            // Show what we got.
            //This is the case that the lenght of makes array is 0 which means that Select() didn't find any row which contains BMW in their column.
            if (makes.Length == 0)
                MessageBox.Show("Sorry, no cars...", "Selection error!");
            else
            {
                //Declare strMake local variable to store.
                string strMake = null;


                for (var i = 0; i < makes.Length; i++)
                {
                    //Append to strMake by iterating.
                    //makes[0]["PetName] means that it's using indexer, retrieving data which is in PetName column from 1st row.
                    //As a result, 
                    //Chucky
                    //Fred
                    //Sidd
                    strMake += makes[i]["PetName"] + "\n";
                }
                // Now show all matches in a message box.
                MessageBox.Show(strMake, $"We have {txtMakeToView.Text}s named:");
            }
        }

        //Added a helper method ShowCarsWithIdGreaterThanFive().
        //Add a helper method ShowCarsWithIdGreaterThanFive().
        private void ShowCarsWithIdGreaterThanFive()
        {
            // Now show the petnames of all cars with ID greater than 5.
            DataRow[] properIDs;
            string newFilterStr = "ID > 5";

            //Retrieve DataRow objects which have Id greater than 5 from inventoryTable DataTable,
            //and assign them to DataRow[].
            properIDs = inventoryTable.Select(newFilterStr);

            string strIDs = null;

            for (int i = 0; i < properIDs.Length; i++)
            {
                //Retrieve 1st row object(properIDs[0]), and assign it to temp.
                DataRow temp = properIDs[i];

                //In 1st row object, search and set the value assigned to PetName, and ID.
                strIDs += $"{temp["PetName"]} is ID {temp["ID"]}\n";
            }
            MessageBox.Show(strIDs, "Pet names of cars where ID > 5");
        }





        //Added a btnChangeMakes_Click() to find all rows which have BMW in their Make column, and change the value from BMW to Yugo.
        // Find the rows you want to edit with a filter.
        private void btnChangeMakes_Click(object sender, EventArgs e)
        {
            // If user click No from MessageBoxButtons, process stops by being returned.
            if (DialogResult.Yes !=
              MessageBox.Show("Are you sure?? BMWs are much nicer than Yugos!",
              "Please Confirm!", MessageBoxButtons.YesNo)) return;

            //If user click Yes from MessageBoxButtons, change process is going.
            //Set a filter to be used as condition in SQL syntax query by being used in Select(). 
            string filterStr = "Make='BMW'";

            //Find all rows matching the filter from inventoryTable DataTable,
            //and assign row(s) to makes DataRow[].
            DataRow[] makes = inventoryTable.Select(filterStr);

            // Change all Beemers to Yugos!
            for (int i = 0; i < makes.Length; i++)
            {
                //Make column of 1st row is set by Yugo.
                makes[i]["Make"] = "Yugo";
            }
        }
    }
}

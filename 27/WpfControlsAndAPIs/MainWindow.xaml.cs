using AutoLotDAL.Repos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Annotations;
using System.Windows.Annotations.Storage;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//c Add codes to implement TabControl by using <TabControl>, <TabItem> etc.

//c Add event handler method RadioButtonClicked(), ColorChanged() for event Click, SelectionChanged of RadioButton, ComboBox.

//c Add InkCanvas in ToolBox.

//c Update code for RadioButtonClicked() event handler method which plays roles for InkMode, EraseMode, SelectMode, based on client's selection on RadioButton.

//c Update MainWindow() to set default for myInkCanvas.EditingMode, inkRadio, comboColors.SelectedIndex.

//c Update ColorChanged() event handler method which sets the drawing pen color a color what user put in ComboBox.

//c Update ComboBox in xaml, to make selection(RGB) visually good with showing colored ellipses, and update ColorChanged() event handler method to assign value into colorToUse by Tag type.

//c Add funtionality for saving, loading, clearing the drawing.

//c Update funtionality for document editor which is in the 2nd TabItem by using FlowDocumentReader class and its lower members such as FlowDocument, Section, List, Paragraph..

//c Add <List>, <Paragraph> in <FlowDocument> in xaml to implement UI , and add PopulateDocument() to add data.

//c Add XML namespace with tag prefix "a" (xmlns:a="clr-namespace:System.Windows.Annotations;assembly=PresentationFramework"). This namespace enables me to use command objects which are used with Documents API, those command objects are packaged in System.Windows.Annotations namespace of PresentationFramework.dll.

//c Update <Button> by adding Command attribute containing AnnotationService as a value. This time, I use sticky note, delete sticky note, highlight functionalities of AnnotationService.

//c Add EnableAnnotations() to use AnnotationService

//c Add btnSaveDoc, btnLoadDoc in xaml, and add event handler for each button click event, by using lambda expression. They save myDocumentReader.Document content to documentData.xaml and read it from file and bind to the myDocumentReader.Document.

//c Add configuration code for 3rd tab(Data Binding) layout in xaml file.

//c Add codes to test Data binding. I create a scrollbar and a label. As I move the scrollbar which has minimum value 1 at start point, maximum value 100 at end point, increased or decreased value dynamically binds to the lable in the double data type.

//c Add MyDoubleConverter class which implements IValueConverter, which converts the number around scrollbar and label.

//c Update 4th DataGrid tap to show data from database which is defined in AutoLotDAL.dll in xaml file. And add ConfigureGrid() to retrieve data from database by using LINQ.

namespace WpfControlsAndAPIs
{


    class MyDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            // Convert the double to an int.
            double v = (double)value;
            return (int)v;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            // You won't worry about "two-way" bindings here, so just return the value.
            return value;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            // Insert code required on object creation below this point.

            //Set Ink mode default in myInkCanvas.EditingMode
            //and set inkRadio checked by default in RadioButton
            //and set comboColors default to SelectedIndex=0.
            this.myInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            this.inkRadio.IsChecked = true;
            this.comboColors.SelectedIndex = 0;

            PopulateDocument();
            EnableAnnotations();


            // Rig up some Click handlers for the save/load of the flow doc.
            btnSaveDoc.Click += (o, s) =>
            {
                using (FileStream fStream = File.Open("documentData.xaml", FileMode.Create))
                {
                    XamlWriter.Save(this.myDocumentReader.Document, fStream);
                }
            };

            btnLoadDoc.Click += (o, s) =>
            {
                using (FileStream fStream = File.Open("documentData.xaml", FileMode.Open))
                {
                    try
                    {
                        FlowDocument doc = XamlReader.Load(fStream) as FlowDocument;
                        this.myDocumentReader.Document = doc;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error Loading Doc!");
                    }
                }
            };

            SetBindings();

            ConfigureGrid();
        }


        //This is for RadioButtons(InkMode, EraseMode, SelectMode).
        private void RadioButtonClicked(object sender, RoutedEventArgs e)
        {
            // Based on which button sent the event, place the InkCanvas in a unique
            // mode of operation.
            switch ((sender as RadioButton)?.Content.ToString())
            {
                // These strings must be the same as the Content values for each
                // RadioButton.
                case "Ink Mode!":
                    this.myInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                    break;

                case "Erase Mode!":
                    this.myInkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                    break;

                case "Select Mode!":
                    this.myInkCanvas.EditingMode = InkCanvasEditingMode.Select;
                    break;
            }
        }



        ////This method sets the drawing pen color a color what user put in ComboBox.
        //private void ColorChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    // Get the selected value in the combo box.
        //    string colorToUse =
        //      (this.comboColors.SelectedItem as ComboBoxItem)?.Content.ToString();

        //    // Change the color used to render the strokes.
        //    this.myInkCanvas.DefaultDrawingAttributes.Color =
        //      (Color)ColorConverter.ConvertFromString(colorToUse);
        //}




        //This method sets the drawing pen color a color what user put in ComboBox.
        private void ColorChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the Tag of the selected StackPanel.
            string colorToUse = (this.comboColors.SelectedItem
                as StackPanel).Tag.ToString();

            // Change the color used to render the strokes.
            this.myInkCanvas.DefaultDrawingAttributes.Color =
              (Color)ColorConverter.ConvertFromString(colorToUse);
        }


        private void SaveData(object sender, RoutedEventArgs e)
        {
            // Save all data on the InkCanvas to a local file.
            using (FileStream fs = new FileStream("StrokeData.bin", FileMode.Create))
            {
                this.myInkCanvas.Strokes.Save(fs);
                fs.Close();
            }
        }

        private void LoadData(object sender, RoutedEventArgs e)
        {
            // Fill StrokeCollection from file.
            using (FileStream fs = new FileStream("StrokeData.bin",
              FileMode.Open, FileAccess.Read))
            {
                StrokeCollection strokes = new StrokeCollection(fs);
                this.myInkCanvas.Strokes = strokes;
            }
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            // Clear all strokes.
            this.myInkCanvas.Strokes.Clear();
        }

        private void PopulateDocument()
        {
            // Add some data to the List item.
            this.listOfFunFacts.FontSize = 14;
            this.listOfFunFacts.MarkerStyle = TextMarkerStyle.Box;
            this.listOfFunFacts.ListItems.Add(new ListItem(new
              Paragraph(new Run("Fixed documents are for WYSIWYG print ready docs!"))));
            this.listOfFunFacts.ListItems.Add(new ListItem(
    new Paragraph(new Run("The API supports tables and embedded figures!"))));
            this.listOfFunFacts.ListItems.Add(new ListItem(
              new Paragraph(new Run("Flow documents are read only!"))));
            this.listOfFunFacts.ListItems.Add(new ListItem(new Paragraph(new Run
              ("BlockUIContainer allows you to embed WPF controls in the document!")
              )));

            // Now add some data to the Paragraph.
            // First part of sentence.
            Run prefix = new Run("This paragraph was generated ");

            // Middle of paragraph.
            Bold b = new Bold();
            Run infix = new Run("dynamically");
            infix.Foreground = Brushes.Red;
            infix.FontSize = 30;
            b.Inlines.Add(infix);

            // Last part of paragraph.
            Run suffix = new Run(" at runtime!");

            // Now add each piece to the collection of inline elements
            // of the Paragraph.
            this.paraBodyText.Inlines.Add(prefix);
            this.paraBodyText.Inlines.Add(infix);
            this.paraBodyText.Inlines.Add(suffix);
        }



        private void EnableAnnotations()
        {
            // Create the AnnotationService object that works
            // with our FlowDocumentReader.
            AnnotationService anoService = new AnnotationService(myDocumentReader);

            // Create a MemoryStream that will hold the annotations.
            MemoryStream anoStream = new MemoryStream();

            // Now, create an XML-based store based on the MemoryStream.
            // You could use this object to programmatically add, delete,
            // or find annotations.
            AnnotationStore store = new XmlStreamStore(anoStream);

            // Enable the annotation services.
            anoService.Enable(store);
        }

        private void SetBindings()
        {
            // Create a Binding object.
            Binding b = new Binding();

            // Register the converter, source, and path.
            b.Converter = new MyDoubleConverter();
            b.Source = this.mySB;
            b.Path = new PropertyPath("Value");

            // Call the SetBinding method on the Label.
            this.labelSBThumb.SetBinding(Label.ContentProperty, b);
        }

        private void ConfigureGrid()
        {
            using (var repo = new InventoryRepo())
            {
                // Build a LINQ query that gets back some data from the Inventory table.
                gridInventory.ItemsSource =
                  repo.GetAll().Select(x => new { x.CarId, x.Make, x.Color, x.PetName });
            }
        }

        
    }
}

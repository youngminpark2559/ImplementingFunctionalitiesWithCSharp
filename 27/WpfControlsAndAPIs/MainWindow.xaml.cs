using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
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

namespace WpfControlsAndAPIs
{
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
    }
}

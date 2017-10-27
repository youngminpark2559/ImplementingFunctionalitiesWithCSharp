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


        private void ColorChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO: Add event handler implementation here.
        }



    }
}

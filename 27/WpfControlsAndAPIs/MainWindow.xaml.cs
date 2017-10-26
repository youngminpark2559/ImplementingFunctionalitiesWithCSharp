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
        }



        private void RadioButtonClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Add event handler implementation here.
        }


        private void ColorChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO: Add event handler implementation here.
        }

    }
}

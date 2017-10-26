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

//c Add WPF App project WPFRoutedEvents.

//c Add codes for bubbling events, by using button, outter ellipse, inner ellipse.

//c Add code to stop routed bubbling event from MouseDown event of outerEllipse, so that I can only see title change, not click event for button.

namespace WPFRoutedEvents
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void btnClickMe_Clicked(object sender, RoutedEventArgs e)
        {
            // Do something when button is clicked.
            MessageBox.Show("Clicked the button");
        }

        //Click outter one -> Event fired and change title -> Event bubbled up to Button element's Click event 
        public void outerEllipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Change title of window.
            this.Title = "You clicked the outer ellipse!";

            // Stop bubbling!
            e.Handled = true;
        }
    }
}

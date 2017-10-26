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

//c Add code for routed tunneling event which goes down to fire event as opposed to routed bubbling event. When both exists, tunneling event is fired, tunneling and fire event, bubbling event is fired, bubbling and fire event.

namespace WPFRoutedEvents
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string _mouseActivity = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void btnClickMe_Clicked(object sender, RoutedEventArgs e)
        {
            AddEventInfo(sender, e);
            MessageBox.Show(_mouseActivity, "Your Event Info");

            // Clear string for next round.
            _mouseActivity = "";
        }

        private void AddEventInfo(object sender, RoutedEventArgs e)
        {
            _mouseActivity += string.Format(
              "{0} sent a {1} event named {2}.\n", sender,
              e.RoutedEvent.RoutingStrategy,
              e.RoutedEvent.Name);
        }


        ////Click outter one -> Event fired and change title -> Event bubbled up to Button element's Click event 
        //public void outerEllipse_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    // Change title of window.
        //    this.Title = "You clicked the outer ellipse!";

        //    // Stop bubbling!
        //    e.Handled = true;
        //}


        //Test for routed bubbling event and routed tunneling event.
        //1.PreviewMouseDown fired -> no down element for routed tunneling event -> 2.MouseDown fired -> routed bubbling event up to Click event of Button.
        private void outerEllipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddEventInfo(sender, e);
        }

        private void outerEllipse_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            AddEventInfo(sender, e);
        }
    }
}

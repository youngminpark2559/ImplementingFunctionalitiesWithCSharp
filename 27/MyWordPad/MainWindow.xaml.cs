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

//c Add a WPF project MyWordPad for testing nested panels to build a window's frame.

//c Add a menu system by Menu, MenuItem element inside of DockPanel in xaml file, with setting some events.

//c Add a ToolBar system which can be alternatively used for a menu system.
namespace MyWordPad
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

        protected void FileExit_Click(object sender, RoutedEventArgs args)
        {
            // Close this window.
            this.Close();
        }
        protected void ToolsSpellingHints_Click(object sender, RoutedEventArgs args)
        {
        }
        protected void MouseEnterExitArea(object sender, RoutedEventArgs args)
        {
        }
        protected void MouseEnterToolsHintsArea(object sender, RoutedEventArgs args)
        {
        }
        protected void MouseLeaveArea(object sender, RoutedEventArgs args)
        {
        }
    }
}

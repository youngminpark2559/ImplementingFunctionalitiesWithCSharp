using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

//c Add MainWindow class to move codes which creates an object of the Window type
//(Window mainWindow = new Window()) in AppStartUp() of Program.cs 
//into MainWindow.cs, to encapsulate its interior values for configuring appearance or functionality, 
//so that I can merely create MainWindow object with passing parameters.

//c Add Closing, Closed events of Window object, 
//and their event handlers(MainWindow_Closing, MainWindow_Closed).

namespace WpfAppAllCode
{
    //This class can be used in strongly typed type.
    class MainWindow : Window
    {
        // Our UI element.
        private Button btnExitApp = new Button();

        public MainWindow(string windowTitle, int height, int width)
        {

            // Configure button and set the child control.
            btnExitApp.Click += new RoutedEventHandler(btnExitApp_Clicked);
            btnExitApp.Content = "Exit Application";
            btnExitApp.Height = 25;
            btnExitApp.Width = 100;

            // Set the content of this window to a single button.
            this.Content = btnExitApp;

            //Configure window properties.
            this.Title = windowTitle;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Height = height;
            this.Width = width;
            this.Show();

            //This event is fired when just before the program is closed, and corresponding event handler is invoked.
            //The use case of this is asking you if you really want to close the program, providing Yes or No button.
            this.Closing += MainWindow_Closing;

            //This event is fired when just after the program is closed, and corresponding event handler is invoked.
            this.Closed += MainWindow_Closed;
        }



        private void btnExitApp_Clicked(object sender, RoutedEventArgs e)
        {
            // Did user enable /godmode?
            if ((bool)Application.Current.Properties["GodMode"])
            {
                //
                MessageBox.Show("Cheater!");
            }
            this.Close();
        }



        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // See if the user really wants to shut down this window.
            string msg = "Do you want to close without saving?";
            MessageBoxResult result = MessageBox.Show(msg,
              "My App", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.No)
            {
                // If user doesn't want to close, cancel closure with keeping the app in the memory.
                e.Cancel = true;
            }
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            MessageBox.Show("See ya!");
        }
    }
}

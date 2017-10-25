using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

//c Add a Console project WpfAppAllCode to make a simple WPF application, written without XAML.
//c Add explanation comment for [STAThread].

namespace WpfAppAllCode
{
    // In this first example, you are defining a single class type
    // to represent the application itself and the main window.
    class Program : Application
    {
        //The Main() of WPF app must be decorated with this.
        //This makes sure that any legacy COM objects used by my application are thread safe, 
        //otherwise, I'll get a runtime exception.
        [STAThread]
        static void Main(string[] args)
        {
            Program app = new Program();

            // Handle the Startup and Exit events by each event handler(AppStartUp, AppExit),
            // and then run the application.
            app.Startup += AppStartUp;
            app.Exit += AppExit;

            // Fires the Startup event.
            app.Run();
        }

        //Event handler.
        static void AppStartUp(object sender, StartupEventArgs e)
        {
            // Create a Window object.
            Window mainWindow = new Window();

            // Set some basic properties by Window object.
            mainWindow.Title = "My First WPF App!";
            mainWindow.Height = 200;
            mainWindow.Width = 300;
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            mainWindow.Show();
        }


        static void AppExit(object sender, ExitEventArgs e)
        {
            MessageBox.Show("App has exited");
        }
    }
}

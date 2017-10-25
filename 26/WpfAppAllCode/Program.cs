using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

//c Add a Console project WpfAppAllCode to make a simple WPF application, written without XAML.
//c Add explanation comment for [STAThread].
//c Add codes using the manner of "specifying the underlying delegates by names" which can be used alternatively with the manner of "method group conversion syntax".

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

            // Handle the events(Startup, Exit) by each event handler(AppStartUp, AppExit),
            // with using method group conversion syntax which is shorthand notation removed the task of specifying the underlying delegates used by a specific event. 
            app.Startup += AppStartUp;
            app.Exit += AppExit;

            // Run the application by firing the Startup event.
            app.Run();






            //// This way is that I specify the underlying delegates by names.
            //Program app = new Program();

            ////Startup event works with StartupEventHandler delegate 
            ////which is defined in System.Windows namespace of PresentationFramework.dll,
            ////and can only point to method 
            ////which takes 1st parameter as Object type, 2nd parameter as StartupEventArgs type.
            //app.Startup += new StartupEventHandler(AppStartUp);
            //app.Exit += new ExitEventHandler(AppExit);
            //app.Run();





        }

        //Event handler.
        static void AppStartUp(object sender, StartupEventArgs e)
        {
            //// Create a Window object.
            //Window mainWindow = new Window();

            //// Set some basic properties by Window object.
            //mainWindow.Title = "My First WPF App!";
            //mainWindow.Height = 200;
            //mainWindow.Width = 300;
            //mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //mainWindow.Show();



            // Create a MainWindow object.
            var main = new MainWindow("My better WPF App!", 200, 300);
            main.Show();
        }


        static void AppExit(object sender, ExitEventArgs e)
        {
            MessageBox.Show("App has exited");
        }
    }
}

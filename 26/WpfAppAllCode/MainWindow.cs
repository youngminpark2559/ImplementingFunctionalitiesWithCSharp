using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

//c Add MainWindow class to move codes which creates an object of the Window type
//(Window mainWindow = new Window()) in AppStartUp() of Program.cs 
//into MainWindow.cs, to encapsulate its interior values for configuring appearance or functionality, 
//so that I can merely create MainWindow object with passing parameters.

namespace WpfAppAllCode
{
    class MainWindow : Window
    {
        public MainWindow(string windowTitle, int height, int width)
        {
            this.Title = windowTitle;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Height = height;
            this.Width = width;
        }
    }
}

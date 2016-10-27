using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

///
///
/// THIS SOFTWARE WAS DEVELOPED BY WILLIAM BRIGGS FOR DEMONSTRATION PURPOSES ONLY
/// @2016
///




namespace flood
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var init = this.Resources["SettingViewModel"]; //Doesn't seem to get created until the first access from settings, which is breaking the view model mediators.
            Window window = new MainWindow();
            window.Title = "Download";
            window.Show();
        }
    }
}

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

namespace flood
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var box = sender as CheckBox;

            if(box != null && box.Command != null && string.IsNullOrEmpty(interval.Text))
            {
                box.Command.Execute(new List<object>() { false, interval.Text });
            }
        }

        private void interval_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (autoRefreshBox != null && autoRefreshBox.Command != null && autoRefreshBox.CommandParameter != null &&  !string.IsNullOrEmpty(interval.Text))
            {
                autoRefreshBox.Command.Execute(autoRefreshBox.CommandParameter);
            }
        }
    }
}

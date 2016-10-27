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
using System.Windows.Shapes;

namespace flood
{
    /// <summary>
    /// Interaction logic for _3dMessAbout.xaml
    /// </summary>
    public partial class _3dMessAbout : Window
    {
        private Dictionary<Type, Type> Values = new Dictionary<Type, Type>();

        public _3dMessAbout()
        {
            InitializeComponent();
            Register<object, Random>();
        }

        public void Register<TKey, TValue>()
        {
            var type = typeof(TValue);
            var typeOfKey = typeof(TKey);

            typeOfKey.IsAssignableFrom(typeOfKey);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace flood
{
    public class PropertyToPropertyString
    {

        public Dictionary<string, ReflectiveItem> Values { get; set; }

        public PropertyToPropertyString()
        {
            Values = new Dictionary<string, ReflectiveItem>();
        }

    }

    public class ReflectiveItem : DependencyObject
    {
        public object Item
        {
            get { return (object)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Item.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register("Item", typeof(object), typeof(ReflectiveItem), new PropertyMetadata(null, 
                (x, y)=>
                {

                }));

    }
}

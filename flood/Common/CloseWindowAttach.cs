using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace flood
{
    public static class CloseWindowAttach
    {
        public static bool GetShouldClose(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShouldCloseProperty);
        }

        public static void SetShouldClose(DependencyObject obj, bool value)
        {
            obj.SetValue(ShouldCloseProperty, value);
        }

        /// <summary>
        /// Setting to true will attempt to close the window assosicated with the dependency object
        /// </summary>
        public static readonly DependencyProperty ShouldCloseProperty =
            DependencyProperty.RegisterAttached("ShouldClose", typeof(bool), typeof(CloseWindowAttach), new PropertyMetadata(false, ShouldCloseChanged));

        public static void ShouldCloseChanged (DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            //Returns the parent window of element
            var wind = Recursion.ParentOfType<Window>(obj);

            if(wind != null && (bool)args.NewValue)
            {
                wind.Close();
            }
        }

        
    }
}

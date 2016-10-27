using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace flood
{
    class MainWindowFactory : IWindowFactory
    {
        public void CreateAndShowWindow(Type type, double? width, double? height, string title, ResizeMode mode, SizeToContent stc)
        {
            Window wind = null;

            if(type.IsSubclassOf(typeof(Window)))
            {
                wind = (Activator.CreateInstance(type) as Window);
            }

            else if(type.IsSubclassOf(typeof(UserControl)))
            {
                var cont = (Activator.CreateInstance(type) as UserControl);
                wind = new Window() { Content = cont };
            }

            else
            {
                throw new InvalidOperationException("Could not resolve type to window or user control");
            }

            wind.Height = height ?? wind.Height;
            wind.Width = width ?? wind.Width;
            wind.Title = title;
            wind.ResizeMode = mode;
            wind.SizeToContent = stc;
            wind.ShowDialog();
        }
    }
}

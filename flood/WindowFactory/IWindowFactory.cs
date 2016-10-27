using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace flood
{
    public interface IWindowFactory
    {
        void CreateAndShowWindow(Type type, double? width, double? height, string title, ResizeMode mode, SizeToContent stc);
    }
}

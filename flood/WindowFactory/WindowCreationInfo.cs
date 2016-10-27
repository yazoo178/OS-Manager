using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace flood
{
    public class WindowCreationInfo
    {
        public Type Type { get; set; }

        public double? Width { get; set; }

        public double? Height { get; set; }

        public string Title { get; set; }

        public ResizeMode ResizeMode { get; set; }

        public SizeToContent SizeToContent { get; set; }
    }
}

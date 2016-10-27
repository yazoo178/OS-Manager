using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flood
{
    public class BaseNotifyingElement : INotifyPropertyChanged
    {
        protected void OnPropertyChanged(string arg1)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(arg1));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

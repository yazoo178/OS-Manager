using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flood
{
    public static class ApplicationMediator
    {
        public static event Action<string, object> OnPropertyChanged;

        public static void Notify(Func<string> propName, object obj)
        {
            if(OnPropertyChanged != null)
            {
                OnPropertyChanged(propName(), obj);
            }
        }
    }
}

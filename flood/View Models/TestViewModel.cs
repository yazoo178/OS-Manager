using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flood
{
    public class TestViewModel : BaseViewModel
    {
        private object exampleObject;
        public object ExampleObject
        {
            get
            {
                return exampleObject;
            }

            set
            {
                exampleObject = value;
                base.OnPropertyChanged("ExampleObject");
            }

        }
    }
}

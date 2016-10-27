using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flood
{
    public class CompressionViewModel : BaseViewModel
    {
        public CompressionFactory CompressionFactory { get; set; }

        private CompressionType _compString;
        public CompressionType CompressionCreatorString
        {
            get
            {
                return _compString;
            }
            set
            {

                _compString = value;
                base.OnPropertyChanged("CompressionCreatorString");
                this.CurrentCompression = CompressionFactory.CreateCompressionFromType(value);

            }
        }

        private ICompression _cuComp;
        public ICompression CurrentCompression
        {
            get
            {
                return _cuComp;
            }

            set
            {
                _cuComp = value;
                base.OnPropertyChanged("CurrentCompression");
            }
        }
    }
}

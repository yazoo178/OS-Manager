using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flood
{
    public class EnumTypeList<T> where T : struct, IConvertible
    {
        public IEnumerable<T> CompressionTypes { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            return CompressionTypes.GetEnumerator();
        }

    }
}

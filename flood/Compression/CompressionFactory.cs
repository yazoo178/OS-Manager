using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flood
{
    public class CompressionFactory : BaseNotifyingElement
    {
        public virtual ICompression CreateCompressionFromType(CompressionType type)
        {
            switch(type)
            {
                case CompressionType.Zip:
                    return new Zip();
                case CompressionType.Rar:
                    return new Rar();
                case CompressionType._7Zip:
                    return new _7Zip();
            }

            return null;
        }

    }

    public interface ICompression { }

    public class Zip : ICompression { }

    public class Rar : ICompression { }

    public class _7Zip : ICompression { }

    public enum CompressionType {Non, Zip, Rar, _7Zip}
}

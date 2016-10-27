using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flood
{
    interface IProcessCommunication<T>
    {
        bool CanHandleProcessRequest(T argument);
    }
}

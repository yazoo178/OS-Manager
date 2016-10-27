using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flood
{
    public class StandardProcessCommunication : IProcessCommunication<IList<object>>
    {

        public bool CanHandleProcessRequest(IList<object> x)
        {
            return (x != null) ? (x[0] is Process && x[1] is ResetToken) ? !(x[0] as Process).HasExited : false : false;
        }
    }
}

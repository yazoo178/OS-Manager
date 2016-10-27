using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flood
{
    public interface IProcessCommunicationFactory
    {
        StandardProcessCommunication Create();
        string Type { get; set; }
    }
}

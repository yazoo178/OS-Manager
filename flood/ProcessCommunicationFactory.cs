using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace flood
{
    public class ProcessCommunicationFactory : IProcessCommunicationFactory
    {
        public StandardProcessCommunication Create()
        {
            return Activator.CreateInstance(Assembly.GetExecutingAssembly().FullName, "flood.Processes." + Type.ToString()).Unwrap() as StandardProcessCommunication;
        }

        public string Type { get; set; }
        
    }
}

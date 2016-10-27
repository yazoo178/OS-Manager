using flood.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flood
{
    public interface IProgressRunner : IRunner
    {
        event EventHandler<double> ProgressChanged;
        ReportingFormat Format { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace flood
{
    public interface IRunner
    {
        Task Run();
        CancellationTokenSource SourceCancel { get; set; }

        event EventHandler<CompletedEventArgs> Completed;
    }
}

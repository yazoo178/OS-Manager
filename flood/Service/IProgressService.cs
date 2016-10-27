using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace flood
{
    public interface IProgressService : IService
    {
        double Progress { get; }

        void EnqueueProgressRunner(IProgressRunner runner);

        void EnqueueProgressRunners(IEnumerable<IProgressRunner> runners);

        Task<IEnumerable<CompletionResult>> StartProgressRunnerOnQueue();

        event EventHandler PreviewStartNextTask;

        bool UseMultiBar { get; set; }


    }
}

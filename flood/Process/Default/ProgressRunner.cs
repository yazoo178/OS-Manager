using flood.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace flood
{
    public class ProgressRunner : IProgressRunner
    {
        public Task<CompletionResult> CurrentTask { get; private set; }
        public event EventHandler<double> ProgressChanged;
        public event EventHandler<CompletedEventArgs> Completed;
        public CancellationTokenSource SourceCancel { get; set; }
        public ReportingFormat Format { get; set; }

        public ProgressRunner(Func<IProgress<double>, CompletionResult> act)
        {
            Progress<double> reporter = new Progress<double>(reporter_ProgressChanged);

            CurrentTask = new Task<CompletionResult>(() => act(reporter));
        }

        public ProgressRunner(Func<IProgress<double>, CancellationToken, CompletionResult> act)
        {
            Progress<double> reporter = new Progress<double>(reporter_ProgressChanged);
            SourceCancel = new CancellationTokenSource();

            CurrentTask = new Task<CompletionResult>(() => act(reporter, SourceCancel.Token));
        }

        private void OnComplete(CompletionResult act)
        {
            if (Completed != null)
            {
                Completed(this, new CompletedEventArgs() { CompletionResult = act });
            }
        }

        public async Task Run()
        {
            CurrentTask.Start();
            OnComplete(await CurrentTask);
        }

        void reporter_ProgressChanged(double e)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(this, e);
            }
        }
    }
}

using flood.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace flood
{
    public class DefaultNotifyingService : BaseNotifyingElement, IProgressService
    {
        public bool Running { get; set; }
        private Queue<IRunner> Runners = new Queue<IRunner>();
        private IRunner CurrentRunner;
        private IList<CompletionResult> results = new List<CompletionResult>();
        public event EventHandler<CompletedEventArgs> TaskCompleted;

        public event EventHandler PreviewStartNextTask;

        private bool? _endwithsu = null;
        public bool? EndedWithSuccess
        {
            get
            {
                return _endwithsu;
            }

            set
            {
                _endwithsu = value;
                base.OnPropertyChanged("EndedWithSuccess");
            }
        }

        private double _progress;

        public double Progress
        {
            get
            {
                return _progress;
            }

            protected set
            {
                _progress = value;
                base.OnPropertyChanged("Progress");
            }
        }

        private object _content;
        public object Content
        {
            get
            {
                return _content;
            }

            protected set
            {
                _content = value;
                base.OnPropertyChanged("Content");
            }
        }

        public int RunnersCount
        {
            get
            {
                return Runners.Count;
            }
        }

        public void EnqueueProgressRunner(IProgressRunner runner)
        {
           Runners.Enqueue(runner);
           base.OnPropertyChanged("RunnersCount");
        }

        public void EnqueueProgressRunners(IEnumerable<IProgressRunner> runners)
        {
            runners.ToList().ForEach(Runners.Enqueue);
        }

        public void DisplayContent(object content)
        {
            this.Content = content;
        }

        public async Task<IEnumerable<CompletionResult>> StartProgressRunnerOnQueue()
        {
            if (CurrentRunner != null)
            {
                throw new InvalidOperationException("Queue Runner already started with tasks running");
            }

            EndedWithSuccess = null;
            Running = true;
            await RunNext();
            Interlocked.Exchange(ref CurrentRunner, null);
            this.Runners.Clear();
            Running = false;

            var returnResults = new List<CompletionResult>(results);
            results.Clear();

            return returnResults;
        }

        private async Task RunNext()
        {
            while (Runners.Count > 0)
            {
                Interlocked.Exchange(ref CurrentRunner, Runners.Dequeue());
                base.OnPropertyChanged("RunnersCount");

                if (CurrentRunner is IProgressRunner)
                {
                    (CurrentRunner as IProgressRunner).ProgressChanged += runner_ProgressChanged;
                }

                OnPreviewStartNextTask(this, new EventArgs());

                await Run(CurrentRunner);
            }
        }

        private async Task Run(IRunner runner)
        {
            runner.Completed += runner_Completed;
            await runner.Run();
        }

        void runner_Completed(object sender, CompletedEventArgs e)
        {
            Interlocked.Exchange(ref CurrentRunner, null);

            if (!e.CompletionResult.RanToCompletion)
            {
                Progress = 100;
                
            }
            else
            {
                OnTaskCompleted(e);
            }

            this.results.Add(e.CompletionResult);
        }

        protected virtual void OnTaskCompleted(CompletedEventArgs result)
        {
            if (this.Runners.Count == 0)
            {
                this.EndedWithSuccess = true;
            }

            if (TaskCompleted != null)
            {
                TaskCompleted(this, result);
            }
        }

        protected virtual void OnPreviewStartNextTask(object sender, EventArgs e)
        {
            if(this.PreviewStartNextTask != null)
            {
                PreviewStartNextTask(this, e);
            }
        }

        void runner_ProgressChanged(object sender, double e)
        {
            var reporter = sender as IProgressRunner;

            if (reporter.Format == ReportingFormat.Value)
            {
                Progress += e;
            }

            else
            {
                Progress = e;
            }
        }


        private bool _useM;
        public bool UseMultiBar
        {
            get
            {
                return _useM;
            }
            set
            {
                _useM = value;
                this.OnPropertyChanged("UseMultiBar");
            }
        }
        
    }
}

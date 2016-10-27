using flood.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace flood
{
    /// <summary>
    /// Interaction logic for ParallelProgressWindow.xaml
    /// </summary>
    public partial class ParallelProgressWindow : UserControl
    {
        private IDictionary<IProgressRunner, Tuple<ProgressBar, RowDefinition>> RunnerDict = new Dictionary<IProgressRunner, Tuple<ProgressBar, RowDefinition>>();

        public ParallelProgressWindow()
        {
            InitializeComponent();
        }

        public async Task AddRunnerAndRun(IProgressRunner runner)
        {
            ProgressBar bar = new ProgressBar() { Style = Application.Current.Resources["ProgressBarStyle"] as Style };
            bar.MinHeight = 25;
            var row = new RowDefinition() { Height = new GridLength(0, GridUnitType.Star) };
            RunnerDict.Add(runner, new Tuple<ProgressBar, RowDefinition>(bar, row));

            Grid.SetRow(bar, RunnerDict.Count - 1);

            (this.Template.FindName("William", this) as Grid).RowDefinitions.Add(row);
            (this.Template.FindName("William", this) as Grid).Children.Add(bar);
           // runner.Completed += runner_Completed;
            Start(runner);
        }

        private async Task Start(IProgressRunner runner)
        {
            runner.ProgressChanged += runner_ProgressChanged;
            runner.Run();
        }

        void runner_ProgressChanged(object sender, double e)
        {
            var reporter = sender as IProgressRunner;

            if (reporter.Format == ReportingFormat.Value)
            {
                RunnerDict[reporter].Item1.Value += e;
            }

            else
            {
                RunnerDict[reporter].Item1.Value = e;
            }
        }

        void runner_Completed(object sender, CompletedEventArgs e)
        {
            (this.Template.FindName("William", this) as Grid).Children.Remove(RunnerDict[sender as IProgressRunner].Item1);
            (this.Template.FindName("William", this) as Grid).RowDefinitions.Remove(RunnerDict[sender as IProgressRunner].Item2);
            RunnerDict.Remove(sender as IProgressRunner);
        }

        
    }
}

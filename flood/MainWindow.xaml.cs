using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WindowsInput;
using System.Windows.Forms;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using TwitchCSharp.Clients;

namespace flood
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

         //   Run();

            //string app = @"C:\Users\Will\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Skills Funding Agency\Data Collection and Funding Transformation\Funding Information System.appref-ms";
           // Process.Start(app, @"/c Hello");
                
        }

        async void Run()
        {
            while (await RunIt())
            {
                System.Windows.MessageBox.Show("Here");

                
            }

        }

        async Task<bool> RunIt()
        {
            Thread.Sleep(1000);

            var runner = new Func<Task<int>>(async () =>
            {
                await Task.Delay(5000);
                
                return 0;
            });

            await new Func<Task>(async () =>
                {

                }).Invoke();

            return true;
        }

        void grid_ItemsSourceUpdated(object sender, ItemsSourceUpdatedEventArgs e)
        {
            var grid = sender as BaseDataGrid;

            if (grid.SortDescriptions != null && e.New is ICollectionView)
            {
                var listCollectionView = CollectionViewSource.GetDefaultView(e.New);

                grid.SortDescriptions.ForEach(listCollectionView.SortDescriptions.Add);
            }
        }

    }

    class WilliamRunnerTwo
    {
        public Task RunningTask;
        private Action<int> ProgressChange;

        public WilliamRunnerTwo(Action<IProgress<int>> ToRun)
        {
            IProgress<int> reporter = new Progress<int>(Report);

            RunningTask = new Task(() =>
                {
                    ToRun(reporter);
                });
            
        }

        private void Report(int value)
        {
            OnProgressChanged(value);
        }

        public void Start()
        {
            RunningTask.Start();
        }

        protected virtual void OnProgressChanged(int prog)
        {
            if (ProgressChange != null)
            {
                Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() =>
                    {
                        ProgressChange(prog);
                    }), null);
            }
        }


    }
    
}

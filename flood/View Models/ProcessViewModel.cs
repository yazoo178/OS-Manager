using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interop;

namespace flood
{
    /// <summary>
    /// View model for application processes
    /// </summary>
    public class ProcessViewModel : BaseViewModel
    {                
        
        #region Private Fields
        private NativeCommandProcess _applicationProcessCmd;
        private IEnumerable<SortDescription> _sortDescriptions;
        private ICollectionView _proccess;
        private List<string> _ignoreList;
        private string _status = string.Empty;
        private string _messageText = string.Empty;
        #endregion

        #region Private Methods

        /// <summary>
        /// Filters an item from the collection view. Ignores items in the IgnoreList
        /// </summary>
        /// <param name="o">Current Item</param>
        /// <returns>false if the item is in the list, otherwise true</returns>
        private bool Filter(object o)
        {
            Process p = o as Process;
            return IgnoreList != null ? !IgnoreList.Contains(p.ProcessName) : true;
        }

        private void view_CurrentChanged(object sender, EventArgs e)
        {

            if (ViewModelMediator.Return<SettingsViewModel>().SyncSelectedGridItemWithCmd && Processes.CurrentItem != null)
            {
                try
                {
                    var current = Processes.CurrentPosition;
                    var cmdModel = ViewModelMediator.Return<CommandsViewModel>();
                    var path = (Processes.CurrentItem as Process).MainModule.FileName;
                    path = path.Remove(path.LastIndexOf(@"\"));
                    cmdModel.MessageSend.SendMessage("cd " + path, ApplicationProcessCmd.Process.MainWindowHandle, (x) => this.Status = "Process Successfully Navigated to", true);
                }

                catch (Exception)
                {
                    this.Status = "Unable to navigate to process";
                }
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Command Window for the current application
        /// </summary>
        public NativeCommandProcess ApplicationProcessCmd
        {
            get
            {
                return _applicationProcessCmd;
            }

            set
            {
                _applicationProcessCmd = value;
                this.OnPropertyChanged("ApplicationProcessCmd");
            }
        }

        /// <summary>
        /// A view of all of the current services running on the system. Lazy and will get created on first call
        /// </summary>
        public ICollectionView Processes
        {
            get
            {
                if (_proccess == null)
                {
                    ReCreateProcessView();
                    
                }

                return _proccess;
            }

            private set
            {
                _proccess = value;
                this.OnPropertyChanged(Common.Properties.Processes);
            }

        }


        /// <summary>
        /// We need to expose the Sort Descriptions so the our custom datagrid can bind to it.
        /// There exists a bug where the sort descriptions are not getting set when refreshing
        /// So instead we set them when the itemssource changes in the grid
        /// </summary>
        public IEnumerable<SortDescription> SortDescriptions
        {
            get
            {
                return _sortDescriptions;
            }

            private set
            {
                _sortDescriptions = value;
                this.OnPropertyChanged(Common.Properties.SortDescriptions);
            }
        }

        /// <summary>
        /// The processes to ignore when filtering the grid
        /// </summary>
        public List<string> IgnoreList
        {
            get
            {
                _ignoreList = _ignoreList ?? new List<string>();
                return _ignoreList;
            }
        }

        /// <summary>
        /// Text of the message box for sending input on screen
        /// </summary>
        public string MessageText
        {
            get
            {
                return _messageText;
            }

            set
            {
                _messageText = value;
                this.OnPropertyChanged(Common.Properties.MessageText);
            }
        }


        /// <summary>
        /// Status of the application, used by label
        /// </summary>
        public string Status
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;
                this.OnPropertyChanged(Common.Properties.Status);

            }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Recreate the process view along with Sort Descriptions, bindings for sort descriptions will get updated
        /// and filters will get re-applied 
        /// </summary>
        public void ReCreateProcessView()
        {

            var view = CollectionViewSource.GetDefaultView(new ObservableCollection<Process>(Process.GetProcesses()));

            this.SortDescriptions = new List<SortDescription>() { new SortDescription() { Direction = ListSortDirection.Descending, PropertyName = "MainWindowTitle"}
                , new SortDescription() { Direction = ListSortDirection.Ascending, PropertyName = "ProcessName"}};

            this.Processes = view;

            view.Filter += Filter;
            view.CurrentChanged += view_CurrentChanged;
        }

        /// <summary>
        /// Switches the killed process with the newly created one to simulate a process revive
        /// </summary>
        /// <param name="toSwitch"></param>
        public void SwitchRevivedProcessWithKilled(Process toSwitch)
        {
            var processToSwitch = Processes.CurrentItem as Process;

            (Processes.SourceCollection as ObservableCollection<Process>).Remove(processToSwitch);
            this.Processes.Refresh();
            (Processes.SourceCollection as ObservableCollection<Process>).Add(toSwitch);
            this.Processes.MoveCurrentTo(toSwitch);
        }

        #endregion

    }
}

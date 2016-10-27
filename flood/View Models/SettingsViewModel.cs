using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace flood
{
    public class SettingsViewModel : BaseViewModel
    {
        private bool _useDefaultProcess;
        public bool UseDefaultProcess
        {
            get
            {
                return _useDefaultProcess;
            }

            set
            {
                _useDefaultProcess = value;
                this.OnPropertyChanged("UseDefaultProcess");
            }
        }

        private bool _appendCarigeReturn;
        public bool AppendCarriageReturn
        {
            get
            {
                return _appendCarigeReturn;
            }

            set
            {
                _appendCarigeReturn = value;
                this.OnPropertyChanged("AppendCarriageReturn");
            }
        }
        private bool _syncSelectedGridItemWithCmd;
        public bool SyncSelectedGridItemWithCmd
        {
            get
            {
                return _syncSelectedGridItemWithCmd;
            }

            set
            {
                _syncSelectedGridItemWithCmd = value;
                this.OnPropertyChanged("SyncSelectedGridItemWithCmd");
            }
        }
        private string _interval = "30";
        public string Interval
        {
            get
            {
                return _interval;
            }

            set
            {
                _interval = value;
                this.OnPropertyChanged("Interval");
            }
        }


        public ICommand AutoRefreshCommand
        {
            get
            {
                return ViewModelMediator.Return<CommandsViewModel>().AutoRefreshCommand;
            }
        }
    }
}

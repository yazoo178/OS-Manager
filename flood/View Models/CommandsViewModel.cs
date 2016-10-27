using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;

namespace flood
{
    public class CommandsViewModel : BaseViewModel
    {
        public IMessageSender MessageSend { get; set; }

        public ITextLoaderFactory LoaderFactory { get; set; }
        private ITextLoader _textLoader;
        public ITextLoader TextLoader
        {
            get
            {
                if(_textLoader == null)
                {
                    _textLoader = new TxtFileLoader();
                }
                return _textLoader;
            }

            private set
            {
                _textLoader = value;
                this.OnPropertyChanged("TextLoader");
            }
        }

        public ICommand SendMessageCommand
        {
            get
            {
                var processModel = ViewModelMediator.Return<ProcessViewModel>();
                var settingsModel = ViewModelMediator.Return<SettingsViewModel>();

                return new RelayCommand<IList<object>, Task>(async x => await MessageSend.SendMessageAsync(TextLoader.IsTextActive ? TextLoader.Text : processModel.MessageText,
                    settingsModel != null && settingsModel.UseDefaultProcess ? processModel.ApplicationProcessCmd.Process.MainWindowHandle : (x[0] as Process).MainWindowHandle
                    , y => processModel.Status = y, settingsModel.AppendCarriageReturn || TextLoader.IsTextActive), y => CanHandleProcessRequest(y) && !string.IsNullOrEmpty(processModel.MessageText));
            }
        }

        public ICommand ClearLoadedTextCommand
        {
            get
            {
                var processModel = ViewModelMediator.Return<ProcessViewModel>();

                return new RelayCommand<object, object>((x) =>
                    {
                        TextLoader.ClearText();
                        processModel.Status = "Stored Text cleared";
                        return null;
                    }, x => TextLoader.IsTextActive);
            }
        }

        public ICommand LoadFileStringCommand
        {
            get
            {
                var processModel = ViewModelMediator.Return<ProcessViewModel>();

                return new RelayCommand<object, Task>(async (o) =>
                    {
                        var dialog = new OpenFileDialog() { Filter = "Text Files (.txt)|*.txt|Batch Files (.bat)|*.bat" };
                        var result = dialog.ShowDialog();

                        if (result != DialogResult.Cancel)
                        {
                            await Task.Run(() =>
                                {
                                    TextLoader = LoaderFactory.CreateLoader(Path.GetExtension(dialog.FileName));
                                    TextLoader.LoadScript(dialog.FileName);
                                });
                            processModel.Status = "Text loaded";
                        }

                        else
                        {
                            processModel.Status = "Text browse cancelled";
                        }

                    });
            }
        }

        public ICommand KillProcessCommand
        {
            get
            {
                var processModel = ViewModelMediator.Return<ProcessViewModel>();

                return new RelayCommand<IList<object>, Task>(async (x) =>
                {
                    await Task.Run(() => (x[0] as Process).Kill());
                    processModel.Processes.Refresh();
                },
                CanHandleProcessRequest);
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                var processModel = ViewModelMediator.Return<ProcessViewModel>();

                return new RelayCommand<object, Task>(async (x) =>
                {
                    processModel.Status = "Refreshing";
                    await Task.Delay(1000);
                    await Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() =>  processModel.ReCreateProcessView()), DispatcherPriority.Send);
                    processModel.Status = "Refresh Complete";
                });
            }
        }

        public ICommand ReviveCommand
        {
            get
            {
                var processModel = ViewModelMediator.Return<ProcessViewModel>();

                return new RelayCommand<IList<object>, Task>(async (x) =>
                {
                    try
                    {
                        processModel.Status = "Reviving Process";
                        var process = await Task.Run(() => Process.Start(new ProcessStartInfo((x[0] as Process).ProcessName)));
                        processModel.SwitchRevivedProcessWithKilled(process);
                        processModel.Status = "Revive Complete";
                    }
                    catch(Exception)
                    {
                       System.Windows.MessageBox.Show("Could not revive process");
                    }

                },
                CanReviveProcessRequest);
            }
        }

        public ICommand AutoRefreshCommand
        {
            get
            {
                 var processModel = ViewModelMediator.Return<ProcessViewModel>();

                 return new RelayCommand<IList<object>,object>((x) =>
                 {
                    if((bool)x[0])
                    {
                        var timer = RefreshDispatchTimer.Instance.Timer;
                        string inter = x[1] as string;
                        int value = 0;
                        int.TryParse(inter, out value);

                        if (value >= 8)
                        {
                            timer.Interval = TimeSpan.FromSeconds(value);
                            timer.Tick += (o, e) => processModel.ReCreateProcessView();
                            timer.Start();
                        }

                        else
                        {
                            timer.Stop();
                            System.Windows.MessageBox.Show("The interval must be a number greater than 8", "Invalid Interval", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                    else
                    {
                        var timer = RefreshDispatchTimer.Instance.Timer;
                        timer.Stop();
                    }

                    return null;
                });
            }
        }

        private bool CanHandleProcessRequest(IList<object> x)
        {
            var processModel = ViewModelMediator.Return<ProcessViewModel>();
            var settingsModel = ViewModelMediator.Return<SettingsViewModel>();

            return  settingsModel.UseDefaultProcess ? !processModel.ApplicationProcessCmd.Process.HasExited :
                (x != null) && ((x[0] is Process) && !((Process) x[0]).HasExited);
        }

        private bool CanReviveProcessRequest(IList<object> x)
        {
            return (x != null) && ((x[0] is Process) && ((Process) x[0]).HasExited);
        }
    }
}

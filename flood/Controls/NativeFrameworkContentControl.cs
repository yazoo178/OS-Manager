using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Interop;
using System.Windows.Markup;


namespace flood
{
    class NativeFrameworkContentControl : UserControl
    {
        /// <summary>
        /// Reference to function pointer
        /// </summary>
        private ExternalFunctions.LowLevelKeyboardProcDelegate _lowLevelKeyBoardReference;

        /// <summary>
        /// //Pointer to the process window
        /// </summary>
        public IntPtr Window { get; set; } 

        /// <summary>
        /// //DP Process reference for the current window
        /// </summary>
        public Process Process
        {
            get { return (Process)GetValue(ProcessProperty); }
            set { SetValue(ProcessProperty, value); }
        }

        public static DependencyProperty ProcessProperty;

        public NativeFrameworkContentControl()
        {
            this.Initialized += NativeFrameworkContentControl_Initialized;
        }

        public int LowLevelKeyboardProc(int nCode, int wParam, ref flood.ExternalFunctions.KBDLLHOOKSTRUCT lParam)
       {
            switch(wParam)
            {
                case 260:
                    return 1;
                default:
                    return 0;
            }
        }

        void NativeFrameworkContentControl_Initialized(object sender, EventArgs e)
        {
            if (this.ApplicationString != null)
            {
                CreateNativeProcess();
            }

            else
            {
                throw new InvalidOperationException("The ApplicationString property must be set");
            }
        }

        private void CreateNativeProcess()
        {
            this.Process = ProcessCreator.CreateProcess(ApplicationString);
            Application.Current.Exit += (x, y) => Process.Kill();
            Process.EnableRaisingEvents = true;
            Process.Exited += Process_Exited;

            Thread.Sleep(100); //BAD, need a better way to wait for signal

            //Gets the current window style, removes the border from it (~0x00450000)
            ExternalFunctions.SetWindowLong(Process.MainWindowHandle, -16, (int)(ExternalFunctions.GetWindowLong(Process.MainWindowHandle, -16) & ~0x00450000));

            System.Windows.Forms.ContainerControl panel1 = new System.Windows.Forms.ContainerControl();

            IntPtr value = ExternalFunctions.SetParent(Process.MainWindowHandle, panel1.Handle);

            this.Window = Process.MainWindowHandle;

            //HookNativeKeyboardEvent();

            this.Content = new System.Windows.Forms.Integration.WindowsFormsHost() { Child = panel1 };
        }

        /// <summary>
        /// Occurs if the intergrated process has recieved an attempt to close
        /// If it has, questions if the user wants to take the main application down with it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Process_Exited(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() =>
                {
                    if (
                    MessageBox.Show("Attempting to kill this command prompt will take the main application down with it. Do you wish to proceed?", "Attempting to close command prompt", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        Application.Current.Shutdown();
                        return;
                    }

                    CreateNativeProcess();
                    FrameworkElement.HeightProperty.GetMetadata(this).PropertyChangedCallback(this, new DependencyPropertyChangedEventArgs(NativeFrameworkContentControl.HeightProperty, 0, this.Height));
                });
        }


        private void HookNativeKeyboardEvent()
        {
            _lowLevelKeyBoardReference = LowLevelKeyboardProc; //Store a reference to this function pointer, as it gets invoked from unmanaged code
            ExternalFunctions.SetWindowsHookEx(13, _lowLevelKeyBoardReference, ExternalFunctions.GetWindowLong(Process.MainWindowHandle, -6), 0);
        }

        static NativeFrameworkContentControl()
        {
            FrameworkElement.WidthProperty.OverrideMetadata(typeof(NativeFrameworkContentControl), new FrameworkPropertyMetadata(0.0, (x, y) =>
            {
                if (y.NewValue != y.OldValue)
                {
                    NativeFrameworkContentControl element = x as NativeFrameworkContentControl;

                    if (element != null && element.Window != IntPtr.Zero)
                    {
                        ExternalFunctions.MoveWindow(element.Window, 0, 0, Convert.ToInt32(element.Width), Convert.ToInt32(element.Height), false);
                    }
                }
            }));

            FrameworkElement.HeightProperty.OverrideMetadata(typeof(NativeFrameworkContentControl), new FrameworkPropertyMetadata(0.0, (x, y) =>
            {
                if (y.NewValue != y.OldValue)
                {
                    NativeFrameworkContentControl element = x as NativeFrameworkContentControl;
                    if (element != null && element.Window != IntPtr.Zero)
                    {
                        ExternalFunctions.MoveWindow(element.Window, 0, 0, Convert.ToInt32(element.Width), Convert.ToInt32(element.Height), false);
                    }
                }
            }));


            ProcessProperty = DependencyProperty.Register("Process", typeof(Process), typeof(NativeFrameworkContentControl), new FrameworkPropertyMetadata(null, (x, y) =>
            {

            }) { DefaultUpdateSourceTrigger = System.Windows.Data.UpdateSourceTrigger.PropertyChanged });

        }

        //The process string, i.e cmd.exe
        public string ApplicationString
        {
            get { return (string)GetValue(ApplicationStringProperty); }
            set { SetValue(ApplicationStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ApplicationString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ApplicationStringProperty =
            DependencyProperty.Register("ApplicationString", typeof(string), typeof(NativeFrameworkContentControl), new PropertyMetadata(null));

    }
}

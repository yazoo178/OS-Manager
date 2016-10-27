using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Runtime.InteropServices;
using WindowsInput;
using System.Windows.Interop;
using System.Windows;

namespace flood
{
    class AsyncMessageSender : IMessageSender
    {
        private System.Threading.CancellationToken token;

        public AsyncMessageSender(System.Threading.CancellationToken _token)
        {
            this.token = _token;
        }

        public AsyncMessageSender() { }

        public async Task SendMessageAsync(string message, IntPtr pro, int interval)
        {
            await Task.Run(() =>
            {
                Timer timer = new Timer(1000);
                timer.Elapsed += ((x, y) =>
                {
                    if (!token.IsCancellationRequested)
                    {
                        ExternalFunctions.SetForegroundWindow(pro);
                        InputSimulator.SimulateTextEntry(message);
                    }
                    else
                    {
                        timer.Stop();
                        timer.Dispose();
                    }
                });

                timer.Start();

            });

            await Task.Delay(-1, token);
        }

        /// <summary>
        /// Sends a one off message to a process
        /// </summary>
        /// <param name="message">Message Text</param>
        /// <param name="pro">A handle to the process</param>
        /// <param name="ActCallBack">Progress report</param>
        /// <returns></returns>
        public async Task SendMessageAsync(string message, IntPtr pro, Action<string> ActCallBack, bool includeCarrigeReturn)
        {
            ActCallBack("Sending Message.........");

            await Task.Run(() =>
            {
                ExternalFunctions.SetForegroundWindow(pro);
                InputSimulator.SimulateTextEntry(message);

                if (includeCarrigeReturn)
                {
                    InputSimulator.SimulateKeyPress(VirtualKeyCode.RETURN);
                }
                
            });

            ActCallBack("Message Sent Successfully"); //Maybe pass future report back
            ExternalFunctions.SetForegroundWindow(new WindowInteropHelper(Application.Current.MainWindow).Handle); //Return focus back to main app
        }



        public void SendMessage(string message, IntPtr pro, Action<string> ActCallBack, bool includeReturn)
        {
            ActCallBack("Sending Message.........");

            ExternalFunctions.SetForegroundWindow(pro);
            InputSimulator.SimulateTextEntry(message);

            if (includeReturn)
            {
                InputSimulator.SimulateKeyPress(VirtualKeyCode.RETURN);
            }

            ActCallBack("Message Sent Successfully"); //Maybe pass future report back
            ExternalFunctions.SetForegroundWindow(new WindowInteropHelper(Application.Current.MainWindow).Handle); //Return focus back to main app
        }
        
    }
}

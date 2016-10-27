using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flood
{
    public interface IMessageSender
    {
        Task SendMessageAsync(string message, IntPtr pro, Action<string> ActCallBack, bool includeReturn);
        void SendMessage(string message, IntPtr pro, Action<string> ActCallBack, bool includeReturn);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace flood
{
    public class WindowViewModel : BaseViewModel
    {
        public IWindowFactory Factory { get; set; }

        public ICommand ShowWindowCommand
        {
            get
            {
                return new RelayCommand<WindowCreationInfo, object>((x) =>
                    {
                        Factory.CreateAndShowWindow(x.Type, x.Width, x.Height, x.Title, x.ResizeMode, x.SizeToContent);
                        return null;
                    });
            }
                
        }

        public WindowViewModel Testing()
        {
            return this;
        }
    }
}

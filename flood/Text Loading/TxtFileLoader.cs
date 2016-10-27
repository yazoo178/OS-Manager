using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flood
{
    public class TxtFileLoader : BaseNotifyingElement, ITextLoader
    {
        public bool IsTextActive
        {
            get
            {
                return !string.IsNullOrEmpty(_text);
            }
        }
        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                this.OnPropertyChanged("Text");
                this.OnPropertyChanged("IsTextActive");
            }
        }

        public virtual void LoadScript(string path)
        {
            Text = File.ReadAllText(path);
        }

        public void ClearText()
        {
            Text = null;
        }

    }
}

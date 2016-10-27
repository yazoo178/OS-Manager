using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flood
{
    public interface ITextLoader
    {
        bool IsTextActive { get; }
        string Text { get; set; }

        void LoadScript(string path);

        void ClearText();
    }
}

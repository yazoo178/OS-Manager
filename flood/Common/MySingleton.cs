using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace flood
{
    class MySingleton
    {
        private static MySingleton _instance;

        private IList<string> IgnoreList = new List<string>() { "svchost", "conhost", "MSBuild", "nvvsvc"};

        public static MySingleton Instance
        {
            get
            {
                _instance = _instance ?? new MySingleton();
                return _instance;
            }
        }

    }
}

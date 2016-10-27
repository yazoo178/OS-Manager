using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flood
{
    public class BatFileLoader : TxtFileLoader
    {
        /// <summary>
        /// Batch file loader loads the path itself rather than the contents
        /// </summary>
        /// <param name="path"></param>
        public override void LoadScript(string path)
        {
            base.Text = path;
        }

    }
}

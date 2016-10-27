using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace flood
{
    static class UserEnviroment
    {
        static UserEnviroment()
        {
            CalcScreenRes();
        }

        private static void CalcScreenRes()
        {
            var ptrToDesk = ExternalFunctions.GetDesktopWindow();
            _RECT r = new _RECT();
            ExternalFunctions.GetWindowRect(ptrToDesk, out r);
            ScreenWidth = r.right / 1.5;
            ScreenHeight = r.bottom / 2;

            Encoding.Unicode.GetBytes("hello");
        }

        public static double ScreenWidth { get; private set; }

        public static double ScreenHeight { get; private set; }

        public static string VideoPath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) + @"\";
            }
        }

    }

    struct _RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }
}

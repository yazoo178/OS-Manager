using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flood
{
    static class Common
    {
        public struct Properties
        {
            public static readonly string Status = "Status";
            public static readonly string Process = "Process";
            public static readonly string MessageText = "MessageText";
            public static readonly string Processes = "Processes";
            public static readonly string SortDescriptions = "SortDescriptions";
        }

        public struct Reflection
        {
            public static readonly string DefaultNamespace = "flood";
        }

        public struct Regex
        {
            public static readonly string YoutubeRegex = @"(?:https?:\/\/)?(?:(?:(?:www\.?)?youtube\.com(?:\/(?:(?:watch\?.*?(v=[^&\s]+).*)|(?:v(\/.*))|(channel\/.+)|(?:user\/(.+))|(?:results\?(search_query=.+))))?)|(?:youtu\.be(\/.*)?))";
        }
    }
}

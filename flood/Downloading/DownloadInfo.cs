using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;

namespace flood.Downloading
{
    public class DownloadInfo
    {
        public DownloadInfo(string _path, string _link, IProgress<double> _prog)
        {
            OutputPath = _path;
            DownloadLink = _link;
            DownloadReporter = _prog;
        }

        public string OutputPath { get; private set; }

        public string DownloadLink { get; private set; }

        public IProgress<double> DownloadReporter { get; private set; }

        public VideoInfo SelectedVideoInfo { get; set; }

        public string Extension { get; set; }

       

    }
}

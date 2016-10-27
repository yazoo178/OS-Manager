using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace flood.Downloading
{
    public abstract class Downloader : IDownload
    {
        protected IProgress<double> Reporter { get; private set; }

        /// <exception cref="System.InvalidOperationException">Thrown when either Download Link or Output path is empty</exception>
        public CompletionResult Download(DownloadInfo info)
        {
            Reporter = info.DownloadReporter;

            if (string.IsNullOrEmpty(info.DownloadLink))
            {
                throw new InvalidOperationException("Download Link cannot be empty");
            }

            if(string.IsNullOrEmpty(info.OutputPath))
            {
                throw new InvalidOperationException("Output Path cannot be empty");
            }

            return DownloadImpDefault(info);
        }

        protected abstract CompletionResult DownloadImpDefault(DownloadInfo info);
    }
}

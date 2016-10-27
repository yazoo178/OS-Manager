using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flood.Downloading
{
    public interface IDownload
    {
        CompletionResult Download(DownloadInfo info);
    }


}

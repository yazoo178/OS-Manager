using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;

namespace flood.Downloading.Youtube
{
    public class YoutubeVideoDownload : Downloader
    {
        protected override CompletionResult DownloadImpDefault(DownloadInfo info)
        {
            if (info.SelectedVideoInfo.RequiresDecryption)
            {
                DownloadUrlResolver.DecryptDownloadUrl(info.SelectedVideoInfo);
            }
            var forMatTitle = new string(info.SelectedVideoInfo.Title.Where(x => !(Path.GetInvalidPathChars().Contains(x) || Path.GetInvalidFileNameChars().Contains(x))).ToArray()) + info.Extension;

            VideoDownloader downloader = new VideoDownloader(info.SelectedVideoInfo, info.OutputPath + forMatTitle);

            downloader.DownloadProgressChanged += (obj, e)=>
                {
                    Reporter.Report(e.ProgressPercentage);
                };

            try
            {
                downloader.Execute();
                return new CompletionResult("Download Successful:" + Environment.NewLine + info.OutputPath + forMatTitle);
            }

            catch(Exception e)
            {
                return new CompletionResult(string.Format("Download Failed :( - {0} : {1}", e.GetType().Name, e.Message), e) { RanToCompletion = false };
            }
        }

    }
}

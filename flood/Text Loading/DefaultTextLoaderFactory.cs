using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;

namespace flood
{
    public class DefaultTextLoaderFactory : ITextLoaderFactory
    {
        public ITextLoader CreateLoader(string type)
        {
            var loader = CreateTextLoaderImp(type);
            return loader;
        }

        protected virtual ITextLoader CreateTextLoaderImp(string type)
        {
            var ToCreate = type.Trim(new[] { '.' });
            var firstUpper = ToCreate[0].ToString().ToUpper();
            ToCreate = ToCreate.Remove(0, 1);
            ToCreate = firstUpper + ToCreate + "FileLoader";
           // var testing = Activator.CreateInstanceFrom(Assembly.GetEntryAssembly().CodeBase, Common.Reflection.DefaultNamespace + "." + ToCreate); //Also works, keeping here for reference

            return Activator.CreateInstance(Assembly.GetExecutingAssembly().FullName, Common.Reflection.DefaultNamespace +  "." + ToCreate).Unwrap() as ITextLoader;
        }

        public async Task FileWriteTest(string path)
        {
           // var mm = DownloadUrlResolver.GetDownloadUrls("https://www.youtube.com/watch?v=2K7GsMr86QA");

           // VideoDownloader downloader = new VideoDownloader(mm.FirstOrDefault(), @"C:\Users\Will\Desktop\www.flv");
           // downloader.Execute();
        }
    }
}

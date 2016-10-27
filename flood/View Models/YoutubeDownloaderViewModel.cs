using flood.Downloading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using YoutubeExtractor;

namespace flood
{
    public class YoutubeDownloaderViewModel : BaseViewModel
    {
        public YoutubeDownloaderViewModel()
        {
            All = true;
        }

        private string _url;

        public string Url
        {
            get { return _url; }
            set 
            {
                _url = value;

                if (!string.IsNullOrEmpty(value))
                {
                    UpdateVideoInfoView();
                }

                base.OnPropertyChanged("Url");
            }
        }

        private string _outputFolder = UserEnviroment.VideoPath;

        public string OutputFolder
        {
            get { return _outputFolder; }
            set { _outputFolder = value.EndsWith(@"\") ? value : value + @"\"; base.OnPropertyChanged("OutputFolder"); }
        }

        private string _status;

        public string Status
        {
            get { return _status; }
            set { _status = value; base.OnPropertyChanged("Status"); }
        }

        private IDownload _downloader;

        public IDownload Downloader
        {
            get { return _downloader; }
            set { _downloader = value; base.OnPropertyChanged("Downloader"); }
        }

        private string _lastDownloadString;
        public string LastDownloadString
        {
            get
            {
                return _lastDownloadString;
            }

            private set
            {
                _lastDownloadString = value;
                base.OnPropertyChanged("LastDownloadString");
                CommandManager.InvalidateRequerySuggested();
            }
        }


        public ICommand Download
        {
            get
            {
                return new RelayCommand<DefaultNotifyingService, Task>(async (ProgressDownloader) =>
                    {
                        var runner = new ProgressRunner((p, token) =>
                        {
                            var currentItem = VideoTypeView.CurrentItem as VideoInfo;
                            return _downloader.Download(new DownloadInfo(_outputFolder, _url, p) { SelectedVideoInfo = currentItem, Extension = (currentItem.AdaptiveType == AdaptiveType.Audio) ? currentItem.AudioExtension : currentItem.VideoExtension });
                        });

                        runner.Format = Controls.ReportingFormat.PercentageToCompletion;
                        ProgressDownloader.EnqueueProgressRunner(runner);

                        if (!ProgressDownloader.Running)
                        {
                           ProgressDownloader.DisplayContent("Downloading Video.." + Environment.NewLine + (VideoTypeView.CurrentItem as VideoInfo).Title);
                           var results = ProgressDownloader.StartProgressRunnerOnQueue();
                           var result = (await results).LastOrDefault();
                           LastDownloadString = (result.Result as string).Split(new string[] { Environment.NewLine }, StringSplitOptions.None)[1];

                           ProgressDownloader.DisplayContent(result.Result);
                        }
                        

                    }, CanExecuteDownloadVideo);
            }
        }

        public ICommand PlayLastVideo
        {
            get
            {
                return new RelayCommand<ProgressWindow, Task>(async (ProgressDownloader) =>
                {
                    await ProcessCreator.CreateProcessAsync(new System.Diagnostics.ProcessStartInfo() { FileName = LastDownloadString });

                }, CanPlayLastVideo);
            }
        }

        private ICollectionView _videoTypeView;
        public ICollectionView VideoTypeView
        {
            get
            {
                if(_videoTypeView == null)
                {
                    UpdateVideoInfoView();
                }

                return _videoTypeView ;
            }

            private set
            {
                if (_videoTypeView != value)
                {
                    _videoTypeView = value;
                    this.OnPropertyChanged("VideoTypeView");
                }
            }
        }

      
        public string Error
        {
            get { return null; }
        }

        private bool CanExecuteDownloadVideo(BaseNotifyingElement window)
        {
            if (Directory.Exists(OutputFolder) && Regex.IsMatch(Url, Common.Regex.YoutubeRegex) && VideoTypeView.CurrentItem != null)
            {
                return true;
            }

            return false;
        }

        private bool CanPlayLastVideo(object obj)
        {
            return !string.IsNullOrEmpty(LastDownloadString);
        }

        private void UpdateVideoInfoView()
        {
            var tmpView = !string.IsNullOrEmpty(Url) ? CollectionViewSource.GetDefaultView(DownloadUrlResolver.GetDownloadUrls(Url, false))
                : CollectionViewSource.GetDefaultView(new List<VideoInfo>());

            VideoTypeView = tmpView;

            ReasessFilter();
        }

        private void ReasessFilter()
        {
            if (VideoTypeView != null)
            {
                VideoTypeView.Filter = !All ? (VideoOnly ? new Predicate<object>(VideoOnlyFilter)
                    : new Predicate<object>(AudioOnlyFilter)) : new Predicate<object>((o) => true);
            }
        }

        private bool VideoOnlyFilter(object link)
        {
            var vid = link as VideoInfo;
            return vid.AdaptiveType != AdaptiveType.Video;
        }

        private bool AudioOnlyFilter(object link)
        {
            var vid = link as VideoInfo;
            return vid.AdaptiveType == AdaptiveType.Audio;
        }

        private bool _audioOnly;

        public bool AudioOnly
        {
            get
            {
                return _audioOnly;
            }

            set
            {
                _audioOnly = value;
                base.OnPropertyChanged("AudioOnly");
                ReasessFilter();
            }
        }

        private bool _videoOnly;
        public bool VideoOnly
        {
            get
            {
                return _videoOnly;
            }

            set
            {
                _videoOnly = value;
                base.OnPropertyChanged("VideoOnly");
                ReasessFilter();
            }
        }

        private bool _all;
        public bool All
        {
            get
            {
                return _all;
            }

            set
            {
                _all = value;
                base.OnPropertyChanged("All");
                ReasessFilter();
            }
        }
    }
}

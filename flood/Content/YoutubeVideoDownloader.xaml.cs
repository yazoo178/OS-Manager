using flood.Downloading;
using flood.Downloading.Youtube;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using YoutubeExtractor;

namespace flood
{
    /// <summary>
    /// Interaction logic for YoutubeVideoDownloader.xaml
    /// </summary>
    public partial class YoutubeVideoDownloader : UserControl
    {
        public IDownload Downloader
        {
            get { return (IDownload)GetValue(DownloaderProperty); }
            set { SetValue(DownloaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DownloaderProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DownloaderProperty =
            DependencyProperty.Register("Downloader", typeof(IDownload), typeof(YoutubeVideoDownloader), new PropertyMetadata(null));


        public YoutubeVideoDownloader()
        {
            string will = "hello how are you doing today, are you doing well?";
            var mm = will.Messing().ToList();

            InitializeComponent();
            this.Loaded += YoutubeVideoDownloader_Loaded;
        }

        void YoutubeVideoDownloader_Loaded(object sender, RoutedEventArgs e)
        {
            grid_ItemsSourceUpdated(null, null);
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Regex.IsMatch(url.Text, Common.Regex.YoutubeRegex) && Directory.Exists(outputFolder.Text);
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Capture these dp fields now as the delegate below is invoked on a seperate thread
            var outputPath = outputFolder.Text;
            var link = url.Text;
            var downloader = this.Downloader;

            var runner = new ProgressRunner((p, token) => downloader.Download(new DownloadInfo(outputPath, link, p)))
            {
                Format = Controls.ReportingFormat.PercentageToCompletion
            };


            // ProgressDownloader.TaskCompleted += ProgressDownloader_TaskCompleted;
          //  ProgressDownloader.EnqueueProgressRunner(runner);
           // ProgressDownloader.StartProgressRunnerOnQueue();
                
        }

        void grid_ItemsSourceUpdated(object sender, ItemsSourceUpdatedEventArgs e)
        {
            try
            {
                youtubeGrid.Columns.FirstOrDefault(x => x.Header.ToString() == "DownloadUrl").Visibility = System.Windows.Visibility.Hidden;
                youtubeGrid.SelectedItem = null;
            }
            catch (Exception) { }
        }

        void ProgressDownloader_TaskCompleted(object sender, CompletedEventArgs e)
        {
            
        }

    }
}

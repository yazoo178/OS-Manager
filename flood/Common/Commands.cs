using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace flood
{
    public static class Commands
    {
        public static RoutedCommand RequestCancelCommand = new RoutedCommand("RequestCancelCommand", typeof(MainWindow));

        public static RoutedCommand SendMessageCommand = new RoutedCommand("SendMessageCommand", typeof(MainWindow));

        public static RoutedCommand KillProcessCommand = new RoutedCommand("KillProcessCommand", typeof(MainWindow));

        public static RoutedCommand ShowSettingsCommand = new RoutedCommand("ShowSettingsCommand", typeof(MainWindow));

        public static RoutedCommand DownloadYouTubeVideoCommand = new RoutedCommand("DownloadYouTubeVideoCommand", typeof(YoutubeVideoDownloader));
    }
}
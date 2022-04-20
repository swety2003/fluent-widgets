using MyNewApp.Common;
using MyWidget;
using MyWidget2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MyNewApp.Widgets
{

    /// <summary>
    /// MediaControl.xaml 的交互逻辑
    /// </summary>
    public partial class MediaControl : WidgetBase
    {
        private DispatcherTimer dt;
        private MediaControlVm vm;
        public MediaControl()
        {
            InitializeComponent();
            this.WidgetWidth = 230;
            this.WidgetHeight = 110;

            this.Icon = "MusicNote224";
            this.WidgetName = "媒体控制器";
            this.Description = "仅支持 win32 网易云音乐";
            this.GUID = "base.music";
        }

        private void WidgetBase_Loaded(object sender, RoutedEventArgs e)
        {
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(2);
            dt.Tick += dt_Tick;
            dt.Start();
            vm = new MediaControlVm();
            this.DataContext = vm;
        }

        private string sName;

        public string SName
        {
            get { return sName; }
            set
            {
                if (sName != value)
                {
                    UpdateCover(value);

                }
                sName = value;
            }
        }
        public async void UpdateCover(string s)
        {
            if (s == null)
            {
                return;
            }
            string coverUrl = await NeteaseCloudMusic.FindAvator(s);
            if (coverUrl == null)
            {
                return;
            }
            vm.ImgSrc = BitmapFrame.Create(new Uri(coverUrl));
        }
        public void dt_Tick(object sender, EventArgs e)
        {
            SName = NeteaseCloudMusic.FindName();
            string QwQ = SName == "" ? "网易云音乐-未播放" : SName;
            if (QwQ.Split('-').Count() >= 2)
            {
                vm.Singer = QwQ.Split('-')[1];

                vm.MusicName = QwQ.Split('-')[0];

            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BasicMethods.runcmd("start Assets\\Scripts\\previous.vbs");

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BasicMethods.runcmd("start Assets\\Scripts\\play.vbs");

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BasicMethods.runcmd("start Assets\\Scripts\\next.vbs");

        }
    }

    public class MediaControlVm : NotifyBase
    {
        private string _musicName;

        public string MusicName
        {
            get { return _musicName; }
            set { _musicName = value; this.DoNotify(); }
        }

        private ImageSource _imgSrc;

        public ImageSource ImgSrc
        {
            get { return _imgSrc; }
            set { _imgSrc = value; this.DoNotify(); }
        }
        private string _singer;

        public string Singer
        {
            get { return _singer; }
            set { _singer = value; this.DoNotify(); }
        }
    }
}


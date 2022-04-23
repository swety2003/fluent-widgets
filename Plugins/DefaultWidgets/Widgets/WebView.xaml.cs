using MyNewApp;
using MyNewApp.Common;
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

namespace DefaultWidgets.Widgets
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class WebView :IWidgetBase
    {
        public int WidgetHeight { get; set; }
        public int WidgetWidth { get; set; }
        public string WidgetName { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string GUID { get; set; }
        public bool WCanResize { get; set; } = false;
        public Action action { get; set; } = null;



        FlyoutWindow wd=null;
        private double wh;
        private double ww;
        public WebView()
        {
            InitializeComponent();
            this.WidgetWidth = 230;
            this.WidgetHeight = 230;
            this.WidgetName = "WebView2";
            this.Description = "一个基于Webview2的浏览器";
            this.Icon = "Globe24";
            this.GUID = "base.webview2";
            this.WCanResize = true;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            pop1.IsOpen = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pop1.IsOpen = false;
            try
            {
                webview.CoreWebView2.Navigate(tb.Text);

            }catch (Exception ex)
            {
                MessageBox.Show("请输入正确的网址！(以 http:// 或者 https:// 开头）");
            }
        }

        private void WidgetBase_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                wd=((root.Parent as Grid).Parent as Border).Parent as FlyoutWindow;
            }catch (Exception ex)
            {

            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            wd.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            ww = wd.Width;
            wh = wd.Height;
            wd.Width = 128;
            wd.Height = 32;
            normal.Visibility = Visibility.Collapsed;
            mined.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            wd.Height = wh;
            wd.Width = ww;
            normal.Visibility = Visibility.Visible;
            mined.Visibility = Visibility.Collapsed ;
        }
    }
}

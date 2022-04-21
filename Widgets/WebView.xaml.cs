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

namespace MyNewApp.Widgets
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class WebView : WidgetBase
    {
        FlyoutWindow wd=null;
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
            webview.NavigateToString(tb.Text);
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
            wd.Width = 64;
            wd.Height = 64;
        }
    }
}

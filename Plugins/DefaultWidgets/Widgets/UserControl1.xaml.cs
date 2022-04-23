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
    public partial class UserControl1 : UserControl, IWidgetBase
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        public int WidgetHeight { get; set; } = 230;
        public int WidgetWidth { get; set; } = 230;
        public string WidgetName { get; set; } = "测试";
        public string Description { get; set; } = "description";
        public string Icon { get; set; } = "";
        public string GUID { get; set; } = "w.sd";
        public bool WCanResize { get; set; } = false;
        public Action action { get; set; } = null;
    }
}

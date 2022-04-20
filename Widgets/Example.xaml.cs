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
    public partial class Example : WidgetBase
    {
        public Example()
        {
            InitializeComponent();
            this.WidgetWidth = 230;
            this.WidgetHeight = 230;
            this.WidgetName = "开发示例";
            this.Description = "空白小部件";
            this.Icon = "CalendarDay24";
            this.GUID = "base.aichedule";
        }

        private void WidgetBase_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}

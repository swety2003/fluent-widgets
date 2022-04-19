using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyNewApp.Common
{
    public class WidgetBase:UserControl
    {

        public int WidgetHeight { get; set; } = 210;
        public int WidgetWidth { get; set; } = 210;

        public string WidgetName { get; set; } = "未命名组件";
        public string Description { get; set; } = "这里是介绍";

        public string Icon { get; set; } = "\uE783";

        public string GUID { get; set; } = "";

        public bool WCanResize { get; set; }=false;


        public Action action;
    }
}

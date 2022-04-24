using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyNewApp.Common
{
    public interface IWidgetBase
    {

        int WidgetHeight => 230;
        int WidgetWidth => 230;

        string WidgetName => "未命名";
        string Description => "描述";


        string Icon => "";

        string GUID => "example.empty";

        bool WCanResize => false;

        Action action { get; set; }
    }
}

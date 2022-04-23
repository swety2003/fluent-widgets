using System;
using static MyNewApp.Common.PluginBase;

namespace DefaultWidgets
{
    public class Class1: IPlugin
    {
        public string Name => "基础插件";
        public string Description => "自带的小部件集合";
        public string Author => "SwetyCore";
    }
}

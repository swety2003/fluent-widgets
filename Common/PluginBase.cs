using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyNewApp.Common
{
    public class PluginBase
    {
        public interface IPlugin
        {
            public string Name => "插件名称";
            public string Description => "插件描述";
            public string Author => "Author";

            public string guid => "{931FC2E6-FCCB-40C8-9DEB-DE4AF8349DFE}";
            //[Obsolete] public bool IsEnabled { get; set; }
        }

        public class PluglingLoader
        {

            public Dictionary<IPlugin,List<UserControl>> LoadedPlugins { get; set; } = new Dictionary<IPlugin, List<UserControl>>();

            
            public Dictionary<IPlugin, List<UserControl>> Load()
            {
                var url = Environment.CurrentDirectory + "\\Plugins";
                if (!Directory.Exists(url)) Directory.CreateDirectory(url);//创建该文件夹　　


                string[] files = Directory.GetFiles(url);

                //将所有plugin文件夹下面的文件路径读取到files里面

                foreach (string file in files)
                {

                    if (file.ToUpper().EndsWith(".DLL"))//将文件名转换为大写，如果后缀为".DLL"则为插件,执行下面语句
                    {

                        Assembly ab = Assembly.LoadFile(file);//可以将assembly视为一个程序集的类型，通过loadfile将其实例化

                        Type[] types = ab.GetTypes();//获取程序集中程序的类型

                        IPlugin plugin = null;

                        foreach (Type t in types)
                        {

                            if (t.GetInterface("IPlugin") != null)//搜索带有指定接口的插件
                            {
                                plugin=ab.CreateInstance(t.FullName) as IPlugin;//将插件装入plugins集合
                                Console.WriteLine($"检测到插件:{t.FullName}");
                                break;
                            }

                        }
                        if(plugin == null)
                        {
                            continue;
                        }
                        else
                        {
                            LoadedPlugins.Add(plugin, new List<UserControl>());

                        }
                        foreach (Type t in types)
                        {
                            if (t.GetInterface("IWidgetBase") != null)
                            {
                                LoadedPlugins[plugin].Add(ab.CreateInstance(t.FullName) as UserControl);
                                Console.WriteLine($"检测到小部件:{t.FullName}");
                            }
                        }

                    }

                }
                return LoadedPlugins;
            }

        }
    }
}

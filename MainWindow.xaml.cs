using MyNewApp.Common;
using MyNewApp.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using WpfWidgetDesktop.Utils;

namespace MyNewApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly string guid = "core.main";

        public Dictionary<string, bool> cfg = new Dictionary<string, bool>();

        public void SaveWidgetStatue()
        {
            foreach (var a in WidgetsList.widgetInstances)
            {

                cfg[a.widget.GUID] = a.Enabled;

            }
            SettingProvider.SetNoSave(guid, cfg);
        }

        private WidgetBase CreateInstance(Type t)
        {
            return Activator.CreateInstance(t) as WidgetBase;

        }

        public void RefreshWidgets()
        {

            SaveWidgetStatue();



            WidgetsList.widgetInstances.Where(a =>
            {

                var w = a.widget.Parent as StackPanel;
                if (w == null)
                {
                    return a.Enabled;
                }
                else
                {

                    return w.Children == null && a.Enabled;
                }
            }).ToList().ForEach(a =>
            {
                var w = new FlyoutWindow(a.widget);
                w.Show();
            });


        }



        private void ScanWidgets()
        {

            WidgetsList.MainWindow = this;
            WidgetsList.widgetInstances = new List<WidgetsList.WidgetInstance>();

            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            currentAssembly.ExportedTypes.Where(t => t.BaseType == typeof(WidgetBase)).ToList().ForEach(t =>
            {
                Console.WriteLine(t.Name);
                WidgetsList.widgetInstances.Add(new WidgetsList.WidgetInstance()
                {
                    widget = CreateInstance(t),
                });
            });
        }


        public MainWindow()
        {

            InitializeComponent();
            ScanWidgets();
            WPFUI.Appearance.Background.Apply(this, WPFUI.Appearance.BackgroundType.Mica);
        }
    }
}

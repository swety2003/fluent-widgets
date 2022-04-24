using MyNewApp.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace DefaultWidgets.Widgets
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class MemMonitor : IWidgetBase
    {
        public int WidgetHeight { get; set; }
        public int WidgetWidth { get; set; }
        public string WidgetName { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string GUID { get; set; }
        public bool WCanResize { get; set; } = false;
        public Action action { get; set; } = null;


        private PerformanceCounter MEMCommitedPerc;
        private DispatcherTimer dt;
        private MemMonitorVM vm;
        public MemMonitor()
        {
            InitializeComponent();
            this.WidgetHeight = 50;
            this.WidgetWidth = 230;

            this.Icon = "DeveloperBoardLightning20";
            this.WidgetName = "内存监视";
            this.Description = "内存占用率查看";

            this.GUID = "base.memmonitor";
        }


        private void WidgetBase_Loaded(object sender, RoutedEventArgs e)
        {
            vm = new MemMonitorVM();
            this.DataContext = vm;
            MEMCommitedPerc = new PerformanceCounter("Memory", "% Committed Bytes In Use", null);
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += dt_Tick;
            dt.Start();
        }
        public void getprocessorUtility()
        {
            vm.MEMAvailable = MEMCommitedPerc.NextValue();
            vm.Text = Math.Round(vm.MEMAvailable, 1).ToString() ;
            
        }
        void dt_Tick(object sender, EventArgs e)
        {
            getprocessorUtility();
        }
    }


    internal class MemMonitorVM : NotifyBase
    {
        private float _memAvailable;

        public float MEMAvailable
        {
            get { return _memAvailable; }
            set { _memAvailable = value; DoNotify(); }
        }

        private string _text;

        public string Text
        {
            get { return $"Mem已用:{_text}%"; }
            set { _text = value; DoNotify(); }
        }


    }
}

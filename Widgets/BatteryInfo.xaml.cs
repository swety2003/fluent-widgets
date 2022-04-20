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
using System.Windows.Threading;
using WpfApp1.Utils;
using static WpfApp1.Utils.SysBatteryInfo;

namespace MyNewApp.Widgets
{
    /// <summary>
    /// SearchBox.xaml 的交互逻辑
    /// </summary>
    public partial class BatteryInfo : WidgetBase
    {
        private SysBatteryInfo batteryInfo;
        private DispatcherTimer dt;
        private BatteryInfoVM vm;
        public BatteryInfo()
        {
            InitializeComponent();
            this.WidgetHeight = 50;
            this.WidgetWidth = 230;

            this.Icon = "BatteryCharge24";
            this.WidgetName = "电池信息";
            this.Description = "电池信息查看";
            this.GUID = "base.batteryinfo";
        }

        private void WidgetBase_Loaded(object sender, RoutedEventArgs e)
        {
            batteryInfo = new SysBatteryInfo();
            vm = new BatteryInfoVM();
            this.DataContext = vm;
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(60);
            dt.Tick += dt_Tick;
            dt.Start();
            dt_Tick(null, null);
        }

        void dt_Tick(object sender, EventArgs e)
        {
            SystemPowerStatus systemPowerStatus = batteryInfo.Get();
            vm.Value = systemPowerStatus.BatteryLifePercent;
            vm.Icon = systemPowerStatus.ACLineStatus == ACLineStatus_.Online ? "\uea93" : "\ue83f";
            vm.Text = vm.Value.ToString();
        }
    }
    internal class BatteryInfoVM : NotifyBase
    {
        private int _value;

        public int Value
        {
            get { return _value; }
            set { _value = value; DoNotify(); }
        }

        private string _icon;

        public string Icon
        {
            get { return _icon; }
            set { _icon = value; DoNotify(); }
        }

        private string _text;

        public string Text
        {
            get { return $"电量剩余:{_text}%"; }
            set { _text = value; DoNotify(); }
        }

    }
}

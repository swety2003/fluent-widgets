using MyNewApp.Common;
using MyWidget2.Utils;
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

namespace MyNewApp.Widgets
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class DigitalClock : WidgetBase
    {
            DigitalClockModel vm;
            private DispatcherTimer dt;
            public DigitalClock()
            {
                InitializeComponent();

                this.WidgetWidth = 230;
                this.WidgetHeight = 230;

                this.Icon = "Clock24";
;
                this.WidgetName = "时钟";
                this.Description = "简单的时钟";
                this.GUID = "base.clock";
            }
            private string GetNow()
            {
                int a = DateTime.Now.Hour;
                if (a < 6)
                {
                    return "夜深了,";
                }
                else if (a < 8)
                {
                    return "早上好,";
                }
                else if (a < 12)
                {
                    return "上午好,";
                }
                else if (a < 13) { return "中午好,"; }
                else if (a < 18)
                {
                    return "下午好,";
                }
                else
                {
                    return "晚上好,";
                }
            }
            public string Week()
            {
                string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
                string week = weekdays[Convert.ToInt32(DateTime.Now.DayOfWeek)];

                return week;
            }

            void dt_Tick(object sender, EventArgs e)
            {
                DateTime now = DateTime.Now;
                vm.Date = now.ToString("D");
                vm.HourAndMinute = now.ToString("t");
                vm.Second = $":{now.ToString("ss")}";
                vm.Week = Week();
                vm.Hello = $"{GetNow()}{System.Environment.UserName}";
                TimeSpan m_WorkTimeTemp = new TimeSpan(Convert.ToInt64(Environment.TickCount) * 10000);
                vm.StartTime = $"已开机 {m_WorkTimeTemp.Days}天 {m_WorkTimeTemp.Hours}时 {m_WorkTimeTemp.Minutes}分";
            }

            private void Button_Click(object sender, RoutedEventArgs e)
            {
                var a = MessageBox.Show("确认关机?", "提示", MessageBoxButton.OKCancel);
                if (a == MessageBoxResult.OK)
                {
                    BasicMethods.runcmd("shutdown -p");

                }
            }

            private void Button_Click_1(object sender, RoutedEventArgs e)
            {
                var a = MessageBox.Show("确认关机?", "提示", MessageBoxButton.OKCancel);
                if (a == MessageBoxResult.OK)
                {
                    BasicMethods.runcmd("shutdown -p");

                }
            }

            private void Button_Click_2(object sender, RoutedEventArgs e)
            {
                BasicMethods.runcmd("rundll32.exe user32.dll LockWorkStation");

            }

            private void Button_Click_3(object sender, RoutedEventArgs e)
            {
                var a = MessageBox.Show("确认重启?", "提示", MessageBoxButton.OKCancel);
                if (a == MessageBoxResult.OK)
                {
                    BasicMethods.runcmd("shutdown -r now");

                }
            }

            private void WidgetBase_Loaded(object sender, RoutedEventArgs e)
            {

                vm = new DigitalClockModel();
                this.DataContext = vm;

                dt = new DispatcherTimer();

                dt.Interval = TimeSpan.FromSeconds(0.1);
                dt.Tick += dt_Tick;
                dt.Start();
            }
        }
        public class DigitalClockModel : NotifyBase
        {
            private string _hourAndMinute;

            public string HourAndMinute
            {
                get { return _hourAndMinute; }
                set { _hourAndMinute = value; this.DoNotify(); }
            }

            private string _startTime;

            public string StartTime
            {
                get { return _startTime; }
                set { _startTime = value; DoNotify(); }
            }


            private string _hello;

            public string Hello
            {
                get { return _hello; }
                set { _hello = value; DoNotify(); }
            }


            private string _second;

            public string Second
            {
                get { return _second; }
                set { _second = value; this.DoNotify(); }
            }

            private string _week;

            public string Week
            {
                get { return _week; }
                set { _week = value; this.DoNotify(); }
            }

            private string _date;

            public string Date
            {
                get { return _date; }
                set { _date = value; this.DoNotify(); }
            }



        }

    
}

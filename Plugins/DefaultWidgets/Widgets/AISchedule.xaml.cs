using MyNewApp.Common;
using MyWidget2.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
using static DefaultWidgets.Widgets.AIScheduleObj;

namespace DefaultWidgets.Widgets
{
    /// <summary>
    /// AISchedule.xaml 的交互逻辑
    /// </summary>
    public partial class AISchedule : UserControl, IWidgetBase
    {
        public int WidgetHeight { get; set; }
        public int WidgetWidth { get; set; }
        public string WidgetName { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; } 
        public string GUID { get; set; }
        public bool WCanResize { get; set; } = false;

        private AIScheduleVM vm;
        private AIScheduleObj.Setting setting { get; set; }
        private DateTime targetTime;

        public AISchedule()
        {
            InitializeComponent();
            this.WidgetWidth = 230;
            this.WidgetHeight = 230;
            this.WidgetName = "课程表";
            this.Description = "小爱课程表支持";
            this.Icon = "Apps24";
            this.GUID = "base.aichedule";
            targetTime = DateTime.Now;

        }

        public Action action { get; set; } = null;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            targetTime = DateTime.Now;

            LoadTable(true);

        }

        private void WidgetBase_Loaded(object sender, RoutedEventArgs e)
        {
            vm = new AIScheduleVM();
            LoadTable(false);
            this.DataContext = vm;

        }

        private bool IsIn(string[] a, string b)
        {
            Console.WriteLine(a);
            foreach (string a2 in a)
            {
                if (a2 == b) { return true; }
            }
            return false;
        }

        public int Week2Int(DayOfWeek w)
        {
            var a = Convert.ToInt32(w);
            if (a == 0)
            {
                return 7;
            }
            else
            {
                return a;
            }
        }

        public async void LoadTable(bool tip = true)
        {
            try
            {
                string table = await File.ReadAllTextAsync("./Config/table.json");
                TableRoot tr = JsonConvert.DeserializeObject<TableRoot>(table);
                setting = tr.data.setting;
                var week = Update();

                vm.CI = tr.data.courses.Where(x => IsIn(x.weeks.Split(','), week) && x.day == Week2Int(targetTime.DayOfWeek)).ToList();

            }
            catch (Exception ex)
            {
                if (tip)
                {
                    var a = MessageBox.Show("还未设置课程表,是否打开课表文件夹?", "提示", MessageBoxButton.OKCancel);
                    if (a == MessageBoxResult.OK)
                    {
                        BasicMethods.runcmd("explorer Config");

                    }
                }

            }

        }

        public string Week2CN()
        {
            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = weekdays[Convert.ToInt32(targetTime.DayOfWeek)];

            return week;
        }

        private string Update()
        {
            string Start = setting.startSemester;
            DateTime start = BasicMethods.StampToDateTime(Start);
            TimeSpan ts = targetTime - start;
            int week = (int)Math.Floor((double)ts.Days / 7) + 1;
            vm.Week = $"第 {week} 周 ";
            vm.Day = $"{Week2CN()}";
            return week.ToString();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            targetTime=targetTime.AddDays(-1);
            LoadTable(true);

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            targetTime = targetTime.AddDays(1);
            LoadTable(true);


        }
    }

    public class AIScheduleObj
    {
        public class CoursesItem
        {
            /// <summary>
            /// 
            /// </summary>
            public int attendTime { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int createTime { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int ctId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int day { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string extend { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 农学基础
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 三江楼0402
            /// </summary>
            public string position { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> sectionList { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string sections { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string style { get; set; }
            /// <summary>
            /// 赵玉国/倪纪恒
            /// </summary>
            public string teacher { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int updateTime { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string weeks { get; set; }
        }

        public class Setting
        {
            /// <summary>
            /// 
            /// </summary>
            public int afternoonNum { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int confirm { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int createTime { get; set; }
            /// <summary>
            /// {"startSemester":1645372800000,"degree":"本科/专科","showNotInWeek":true,"bgSetting":{"name":"default","opacity":1}}
            /// </summary>
            public string extend { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int isWeekend { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int morningNum { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int nightNum { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int presentWeek { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string school { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string sectionTimes { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int speak { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string startSemester { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int totalWeek { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int updateTime { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int weekStart { get; set; }
        }

        public class CourseData
        {
            /// <summary>
            /// 
            /// </summary>
            public List<CoursesItem> courses { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int current { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 大二下
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Setting setting { get; set; }
        }

        public class TableRoot
        {
            /// <summary>
            /// 
            /// </summary>
            public int code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public CourseData data { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string desc { get; set; }
        }
    }

    public class AIScheduleVM : NotifyBase
    {
        private List<CoursesItem> _ci;

        public List<CoursesItem> CI
        {
            get { return _ci; }
            set { _ci = value; this.DoNotify(); }
        }

        private string _week;

        public string Week
        {
            get { return _week; }
            set { _week = value; this.DoNotify(); }
        }

        private string _day;

        public string Day
        {
            get { return _day; }
            set { _day = value; this.DoNotify(); }
        }
    }
}

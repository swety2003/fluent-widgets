using DGP.Genshin.GamebarWidget.MiHoYoAPI;
using DGP.Genshin.GamebarWidget.Model;
using MyNewApp.Common;
using Newtonsoft.Json;
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
using WpfWidgetDesktop.Utils;

namespace MyNewApp.Widgets
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class GenshinDailyNote : WidgetBase
    {
        private class Cfg
        {
            public string Cookie { get; set; }

        }
        private Cfg cfg;
        private DispatcherTimer dt = new DispatcherTimer();

        private GenshinModel vm;

        public GenshinDailyNote()
        {
            InitializeComponent();
            this.WidgetHeight = 170;
            this.WidgetWidth = 230;

            this.Icon = "Channel24";
            this.WidgetName = "原神实时便笺";
            this.Description = "显示原神树脂等信息的工具";
            this.GUID = "base.dailynote";
        }

        private void WidgetBase_Loaded(object sender, RoutedEventArgs e)
        {
            vm = new GenshinModel();
            cfg = JsonConvert.DeserializeObject<Cfg>(SettingProvider.Get(GUID));
            if (cfg == null)
            {
                cfg = new Cfg();
                cfg.Cookie = "";
            }
            dt.Interval = TimeSpan.FromMinutes(8);
            dt.Tick += dt_Tick;
            dt.Start();
            this.DataContext = vm;
            Refresh();

        }
        private void dt_Tick(object o, EventArgs e)
        {
            if (cfg.Cookie == "" | cfg.Cookie == null)
            {

                return;
            }

            Refresh();

        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            pop1.IsOpen = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pop1.IsOpen = false;
            cfg.Cookie = tb.Text;
            SettingProvider.SetNoSave(GUID, cfg);
            Refresh();


        }
        public async Task RefreshDailyNotePoolAsync(string mycookie)
        {

            if (mycookie == "" | mycookie == null)
            {
                MessageBox.Show("未设置cookie!", "提示");

                return;
            }

            Cookie cookie = new Cookie(mycookie);


            List<UserGameRole> roles = await new UserGameRoleProvider(cookie.CookieValue).GetUserGameRolesAsync();
            //roleAndNotes.Clear();
            if (roles.Count < 1)
            {
                MessageBox.Show("无效的Cookie或者无网络!", "提示");
                return;
            }
            foreach (UserGameRole role in roles)
            {
                DailyNote note = await new DailyNoteProvider(cookie.CookieValue).GetDailyNoteAsync(role.Region, role.GameUid);
                //roleAndNotes.Add(new RoleAndNote { Role = role, Note = note });
                this.vm.RoleAndNote = new RoleAndNote { Role = role, Note = note };
            }


        }
        public void Refresh()
        {
            RefreshDailyNotePoolAsync(cfg.Cookie);
        }

    }

    public class GenshinModel : NotifyBase
    {
        private string _cookie;

        public string Cookie
        {
            get { return _cookie; }
            set { _cookie = value; this.DoNotify(); }
        }
        private string _edited;

        public string Edited
        {
            get { return _edited; }
            set { _edited = value; this.DoNotify(); }
        }
        private RoleAndNote _roleAndNote;

        public RoleAndNote RoleAndNote
        {
            get { return _roleAndNote; }
            set { _roleAndNote = value; this.DoNotify(); }
        }

    }
}

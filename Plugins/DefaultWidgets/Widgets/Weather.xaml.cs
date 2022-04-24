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
using DefaultWidgets.Utils;
using static DefaultWidgets.Utils.Weather.DataType;
using Newtonsoft.Json;

namespace DefaultWidgets.Widgets
{
    /// <summary>
    /// Weather.xaml 的交互逻辑
    /// </summary>
    public partial class Weather : UserControl,IWidgetBase
    {
        public int WidgetHeight { get; set; }
        public int WidgetWidth { get; set; }
        public string WidgetName { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string GUID { get; set; }
        public bool WCanResize { get; set; } = false;
        public Action action { get; set; } = null;

        WeatherVM vm = new WeatherVM();
        public Weather()
        {
            InitializeComponent();

            this.WidgetHeight = 230;
            this.WidgetWidth = 230;

            this.Icon = "Board24";


            this.WidgetName = "天气";
            this.GUID = "base.qs";
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = vm;
            RefreshAsync();

        }


        private async Task RefreshAsync()
        {

            try
            {
                Utils.Weather.API api = new Utils.Weather.API();
                var resp = await api.GetCurrent();

                vm.Weather = JsonConvert.DeserializeObject<WeatherCurrent.Root>(resp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            RefreshAsync();

        }
    }

    public class WeatherVM:NotifyBase
    {
        private WeatherCurrent.Root _weather;

        public WeatherCurrent.Root Weather
        {
            get { return _weather; }
            set { _weather = value; DoNotify(); }
        }

    }
}

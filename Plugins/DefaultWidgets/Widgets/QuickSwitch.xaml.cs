using DefaultWidgets.Utils;
using MyNewApp.Common;
using MyWidget2.Utils;
using NativeWifi;
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

namespace DefaultWidgets.Widgets
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class QuickSwitch : IWidgetBase
    {
        public int WidgetHeight { get; set; }
        public int WidgetWidth { get; set; }
        public string WidgetName { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string GUID { get; set; }
        public bool WCanResize { get; set; } = false;
        public Action action { get; set; } = null;



        private WlanClient.WlanInterface WlanIface;
        private QuickSwitchVM vm;
        private VolumeControl volumeControl;
        private bool isUserChangeVolume = true;

        public QuickSwitch()
        {
            InitializeComponent();
            this.WidgetHeight = 170;
            this.WidgetWidth = 230;

            this.Icon = "Board24";


            this.WidgetName = "快捷开关";
            this.GUID = "base.qs";
        }

        private void WidgetBase_Loaded(object sender, RoutedEventArgs e)
        {
            vm = new QuickSwitchVM();
            this.DataContext = vm;
            WlanClient client = new WlanClient();
            WlanIface = client.Interfaces[0];//一般就一个网卡，有2个没试过。

            WlanIface.WlanConnectionNotification += WlanIface_WlanConnectionNotification;
            slider.Value = int.Parse(BasicMethods.GetBrightness());
            InitializeAudioControl();
            try
            {

                LoadNet();
            }
            catch (Exception ex)
            {
                //不支持
            }

        }
        void WlanIface_WlanConnectionNotification(Wlan.WlanNotificationData notifyData, Wlan.WlanConnectionNotificationData connNotifyData)
        {
            if (notifyData.notificationSource == NativeWifi.Wlan.WlanNotificationSource.MSM)
            {
                //这里是完成连接
                if ((NativeWifi.Wlan.WlanNotificationCodeMsm)notifyData.NotificationCode == NativeWifi.Wlan.WlanNotificationCodeMsm.Connected)
                {
                    vm.IsEnabled = true;
                    vm.SSID = connNotifyData.profileName;
                }
            }
            else if (notifyData.notificationSource == NativeWifi.Wlan.WlanNotificationSource.ACM)
            {
                //连接失败
                if ((NativeWifi.Wlan.WlanNotificationCodeAcm)notifyData.NotificationCode == NativeWifi.Wlan.WlanNotificationCodeAcm.ConnectionAttemptFail)
                {
                    vm.IsEnabled = true;

                    vm.SSID = "未连接";
                    LoadNet();

                    //WlanIface.DeleteProfile(connNotifyData.profileName);
                }
                if ((NativeWifi.Wlan.WlanNotificationCodeAcm)notifyData.NotificationCode == NativeWifi.Wlan.WlanNotificationCodeAcm.Disconnected)
                {
                    vm.IsEnabled = true;

                    vm.SSID = "未连接";
                    LoadNet();

                }
                if ((NativeWifi.Wlan.WlanNotificationCodeAcm)notifyData.NotificationCode == NativeWifi.Wlan.WlanNotificationCodeAcm.Disconnecting)
                {
                    vm.IsEnabled = true;

                    vm.SSID = "断开连接";
                    LoadNet();
                }
                if ((NativeWifi.Wlan.WlanNotificationCodeAcm)notifyData.NotificationCode == NativeWifi.Wlan.WlanNotificationCodeAcm.ConnectionStart)
                {
                    vm.IsEnabled = true;

                    vm.SSID = "连接中…";
                }
            }

        }

        private void LoadNet()
        {
            Wlan.WlanAvailableNetwork[] networks;
            System.Int32 dwFlag = new Int32();
            try
            {
                networks = WlanIface.GetAvailableNetworkList(0);
            }
            catch (Exception ex)
            {
                vm.IsEnabled = false;
                vm.SSID = "已关闭";
                return;
            }
            foreach (Wlan.WlanAvailableNetwork network in networks)
            {
                vm.IsEnabled = true;

                //string SSID = WlanHelper.GetStringForSSID(network.dot11Ssid);
                string SSID = network.profileName;
                if (network.flags.HasFlag(Wlan.WlanAvailableNetworkFlags.Connected))
                {

                    vm.SSID = SSID;
                }
            }
        }

        private void InitializeAudioControl()
        {
            volumeControl = VolumeControl.Instance;
            volumeControl.OnAudioNotification += volumeControl_OnAudioNotification;
            volumeControl_OnAudioNotification(null, new AudioNotificationEventArgs() { MasterVolume = volumeControl.MasterVolume });

        }

        void volumeControl_OnAudioNotification(object sender, AudioNotificationEventArgs e)
        {
            this.isUserChangeVolume = false;
            try
            {
                this.Dispatcher.Invoke(new Action(() => { volume.Value = e.MasterVolume; }));
            }
            catch { }
            this.isUserChangeVolume = true;
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BasicMethods.SetBrightness(int.Parse(e.NewValue.ToString()));

        }

        private void Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (isUserChangeVolume)
            {
                volumeControl.MasterVolume = volume.Value;
            }

        }
    }

    public class QuickSwitchVM : NotifyBase
    {
        private string _ssid;

        public string SSID
        {
            get { return _ssid; }
            set { _ssid = value; DoNotify(); }
        }

        private bool _isEnabled;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; DoNotify(); }
        }


    }
}

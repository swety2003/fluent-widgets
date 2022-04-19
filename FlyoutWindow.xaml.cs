using MyNewApp.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Utils;
using WpfWidgetDesktop.Utils;

namespace MyNewApp
{
    /// <summary>
    /// FlyoutWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FlyoutWindow : Window
    {
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        public readonly string guid = "core.flyoutwindow";

        public class CFG
        {
            public string guid;
            public double left;
            public double top;
        }

        private CFG cfg;

        public static Dictionary<string, CFG> CFGS;

        private WidgetBase widget;

        public FlyoutWindow(WidgetBase widget)
        {
            InitializeComponent();

            this.widget = widget;

            IntPtr hWnd = new WindowInteropHelper(GetWindow(this)).EnsureHandle();

            //WPFUI.Appearance.Background.Apply(this, WPFUI.Appearance.BackgroundType.Acrylic);


            Height = widget.WidgetHeight + 2;
            Width = widget.WidgetWidth + 2;
            widget.action = CloseWidget;

            if (widget.WCanResize)
            {
                this.ResizeMode = ResizeMode.CanResizeWithGrip;
            }
            this.LocationChanged += LocationChangedHandler;

            this.Loaded += LoadWidget;

            this.Closed += Window_Closed;

            SetWindowLong(hWnd, (-20), 0x80);

        }
        private void Window_Closed(object sender, EventArgs e)
        {
            WindowPos.LX.Remove(this);
            var a = this.widget.Parent as Grid;
            a.Children.Clear();
        }

        private void LocationChangedHandler(object s, EventArgs e)
        {
            WindowPos.GetNearBy(this);
        }

        private void LoadWidget(object sender, EventArgs e)
        {
            WindowPos.LX.Add(this);
            try
            {
                this.cfg = new CFG
                {
                    guid = widget.GUID,
                };
                CFGS = JsonConvert.DeserializeObject<Dictionary<string, CFG>>(SettingProvider.Get(guid));
                if (CFGS == null)
                {
                    CFGS = new Dictionary<string, CFG>();
                }
                if (CFGS.ContainsKey(cfg.guid))
                {

                    cfg = CFGS[cfg.guid];
                    this.Left = cfg.left;
                    this.Top = cfg.top;
                }

                Container.Children.Add(widget);
            }
            catch (Exception ex)
            {
                this.Close();
            }
        }

        public void CloseWidget()
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            CloseWidget();
        }
    }
}

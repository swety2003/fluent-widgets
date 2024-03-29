﻿using MyNewApp.Common;
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

namespace MyNewApp.Widgets
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class CPUMonitor : WidgetBase
    {
        private PerformanceCounter cpuProcessorUtility;
        private DispatcherTimer dt;
        private CPUMonitorVm vm;
        public CPUMonitor()
        {
            InitializeComponent();
            this.WidgetHeight = 50;
            this.WidgetWidth = 230;

            this.Icon = "PulseSquare24";
            this.WidgetName = "CPU性能监视";
            this.Description = "CPU占用率查看";
            this.GUID = "base.cpumonitor";
        }


        private void WidgetBase_Loaded(object sender, RoutedEventArgs e)
        {
            vm = new CPUMonitorVm();
            this.DataContext = vm;
            cpuProcessorUtility = new PerformanceCounter("Processor Information", "% Processor Utility", "_Total");
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += dt_Tick;
            dt.Start();
        }
        public void getprocessorUtility()
        {
            float tmp = cpuProcessorUtility.NextValue();

            vm.ProcessorUtility = (float)(Math.Round((double)tmp, 1));
            vm.Text = vm.ProcessorUtility.ToString();
        }
        void dt_Tick(object sender, EventArgs e)
        {
            getprocessorUtility();
        }
    }


    internal class CPUMonitorVm : NotifyBase
    {
        private float _processorUtility;

        public float ProcessorUtility
        {
            get { return _processorUtility; }
            set { _processorUtility = value; DoNotify(); }
        }

        private string _text;

        public string Text
        {
            get { return $"CPU已用:{_text}%"; }
            set { _text = value; DoNotify(); }
        }


    }
}

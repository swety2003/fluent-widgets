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

namespace MyNewApp.Controls
{
    /// <summary>
    /// TableViewItem.xaml 的交互逻辑
    /// </summary>
    public partial class TableViewItem : UserControl
    {
        public TableViewItem()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static readonly DependencyProperty CurrentProperty =
            DependencyProperty.Register("Current", typeof(string), typeof(TableViewItem), new PropertyMetadata("0",null, ChangeCurrent));
        public static readonly DependencyProperty MaxProperty =
            DependencyProperty.Register("Max", typeof(string), typeof(TableViewItem), new PropertyMetadata("0",null,ChangeMax));
        public static readonly DependencyProperty TNameProperty =
            DependencyProperty.Register("TName", typeof(string), typeof(TableViewItem), new PropertyMetadata("0",null,  ChangeName));

        private static object ChangeName(DependencyObject obj, object baseValue)
        {
            var control = obj as TableViewItem;
            control.tname.Text = baseValue as string;
            return baseValue;

        }
        private static object ChangeCurrent(DependencyObject obj, object baseValue)
        {
            var control = obj as TableViewItem;
            control.tcurrent.Text = baseValue as string;
            return (object)baseValue;

        }
        private static object ChangeMax(DependencyObject obj, object baseValue)
        {
            var control = obj as TableViewItem;
            control.tmax.Text = baseValue as string;
            return baseValue;
        }

        public string TName
        {
            get { return (string)GetValue(TNameProperty); }
            set { SetValue(TNameProperty, value);  }
        }


        public string Current
        {
            get { return (string)GetValue(CurrentProperty); }
            set { SetValue(CurrentProperty, value);  }
        }

        public string Max
        {
            get { return (String)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value);  }
        }

        private string _tip;

        public string Tip
        {
            get { return _tip; }
            set { _tip = value; }
        }


    }
}

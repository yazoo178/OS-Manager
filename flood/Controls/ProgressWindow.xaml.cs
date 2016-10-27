using flood.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace flood
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ProgressWindow : UserControl
    {
        public ProgressWindow()
        {
            InitializeComponent();
        }

        public Visibility CancelButtonVisibility
        {
            get { return (Visibility)GetValue(CancelButtonVisibilityProperty); }
            set { SetValue(CancelButtonVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CancelButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CancelButtonVisibilityProperty =
            DependencyProperty.Register("CancelButtonVisibility", typeof(Visibility), typeof(ProgressWindow), new PropertyMetadata(Visibility.Visible));



        public IProgressService ProgressService
        {
            get { return (IProgressService)GetValue(ProgressServiceProperty); }
            set { SetValue(ProgressServiceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProgressService.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressServiceProperty =
            DependencyProperty.Register("ProgressService", typeof(IProgressService), typeof(ProgressWindow), new PropertyMetadata((x, y) =>
                {
                    var element = y.NewValue as IProgressService;
                    var dp = x as ProgressWindow;
                    element.PreviewStartNextTask += dp.ResetBarStyle;
                }));


        public bool UseMultiBar
        {
            get { return (bool)GetValue(UseMultiBarProperty); }
            set { SetValue(UseMultiBarProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UseMultiBar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseMultiBarProperty =
            DependencyProperty.Register("UseMultiBar", typeof(bool), typeof(ProgressWindow), new PropertyMetadata(false,
                (x, y)=>
                {
                    if((bool)y.NewValue)
                    {
                        (x as ProgressWindow).Loaded += (e, o) =>
                        {
                           // ((x as ProgressWindow).Template.FindName("TotalProgressBar", x as ProgressWindow) as ProgressBar).Visibility = Visibility.Visible;
                        };
                            
                    }
                }));


        public void ResetBarStyle(object sender, EventArgs e)
        {
            ProgressBar Bar = this.Template.FindName("Bar", this) as ProgressBar;
            Bar.Style = null;
            Bar.Style = (this.Template.FindName("William", this) as Grid).Resources["AnimatingBarStyle"] as Style;
        }

       
    }

}

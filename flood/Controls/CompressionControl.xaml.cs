using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace flood
{
    /// <summary>
    /// Interaction logic for CompressionControl.xaml
    /// </summary>
    public partial class CompressionControl : UserControl
    {
        /// <summary>
        /// The current selected compression object from the dropdown
        /// </summary>
        public ICompression SelectedCompression
        {
            get { return (ICompression)GetValue(SelectedCompressionProperty); }
            set { SetValue(SelectedCompressionProperty, value); }
        }

        public static readonly DependencyProperty SelectedCompressionProperty =
            DependencyProperty.Register("SelectedCompression", typeof(ICompression), typeof(CompressionControl), new PropertyMetadata(null));

        /// <summary>
        /// The default compression type to be displayed in the dropdown
        /// </summary>
        public CompressionType DefaultCompressionSelection
        {
            get { return (CompressionType)GetValue(DefaultCompressionSelectionProperty); }
            set { SetValue(DefaultCompressionSelectionProperty, value); }
        }

        public static readonly DependencyProperty DefaultCompressionSelectionProperty =
            DependencyProperty.Register("DefaultCompressionSelection", typeof(CompressionType), typeof(CompressionControl), new PropertyMetadata(CompressionType.Non,
                (x, y) =>
                {
                    (x as CompressionControl).Loaded += (e, o) =>
                        {
                            //After the control has loaded check to see if the defaultcompressionselection is contained within the all the avaliable compression types
                            if (DefaultCompressionTypes.Contains((CompressionType)y.NewValue))
                            {
                                //Select the item if it is.
                                //NOTE: This will get cleared out later if UseDefaultCOmpressionSelections is set to false
                                ((x as CompressionControl).Template.FindName("PART_box", x as CompressionControl) as ComboBox).SelectedItem = y.NewValue;
                            }
                        };

                    if ((x as CompressionControl).IsLoaded)
                    {
                        throw new InvalidOperationException("DefaultCompressionSelection cannot be changed after the control has loaded");
                    }
                })

                );

        /// <summary>
        /// Should the dropdown use the default compression types
        /// </summary>
        public bool UseDefaultCompressionSelections
        {
            get { return (bool)GetValue(UseDefaultCompressionSelectionsProperty); }
            set { SetValue(UseDefaultCompressionSelectionsProperty, value); }
        }

        public static readonly DependencyProperty UseDefaultCompressionSelectionsProperty =
            DependencyProperty.Register("UseDefaultCompressionSelections", typeof(bool), typeof(CompressionControl), new PropertyMetadata(true
                ,(x, y) =>
                {
                    if (!(bool)y.NewValue)
                    {
                        var control = x as CompressionControl;

                        //Clear out the current compression box
                        //Set the source to an empty collection
                        Action act = () =>
                            {
                                var box = control.Template.FindName("PART_box", control) as ComboBox;
                                box.ItemsSource = new ObservableCollection<CompressionType>();

                            };

                        if (control.IsLoaded)
                        {
                            act();
                        }

                        else
                        {
                            control.Loaded += (e, o) => act();
                        }
                    }
                })
                

                );

        /// <summary>
        /// The Compression Types to Display
        /// </summary>
        [TypeConverter(typeof(CommaSeperatedStringToListOfEnumConverter<CompressionType>))]
        public EnumTypeList<CompressionType> CompressionTypes
        {
            get { return (EnumTypeList<CompressionType>)GetValue(CompressionTypesProperty); }
            set { SetValue(CompressionTypesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CompressionTypes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CompressionTypesProperty =
            DependencyProperty.Register("CompressionTypes", typeof(EnumTypeList<CompressionType>), typeof(CompressionControl), new PropertyMetadata(null,
                (x, y)=>
                {
                    var control = x as CompressionControl;

                    if (control.UseDefaultCompressionSelections)
                    {
                        throw new InvalidOperationException("Cannot set custom compression types when use default compression is true");
                    }
                    
                    Action act = ()=>
                        {
                            var box = control.Template.FindName("PART_box", control) as ComboBox;

                            var value = y.NewValue as EnumTypeList<CompressionType>;

                            value.CompressionTypes.ToList().ForEach((box.Items.SourceCollection as IList<CompressionType>).Add);
                            box.SelectedItem = control.DefaultCompressionSelection;
                        };

                    if(control.IsLoaded)
                    {
                        act();
                    }
                    else
                    {
                        control.Loaded += (e, o) => act();
                    }

                }
                ));

        

        
        public CompressionControl()
        {
            InitializeComponent();
        }

        private static Lazy<IEnumerable<CompressionType>> defaultCompTypes = new Lazy<IEnumerable<CompressionType>>(()
            => new ObservableCollection<CompressionType>() { CompressionType.Zip, CompressionType.Rar, CompressionType._7Zip });
        public static IEnumerable<CompressionType> DefaultCompressionTypes
        {
            get
            {
                return defaultCompTypes.Value;
            }
        }


    }
}

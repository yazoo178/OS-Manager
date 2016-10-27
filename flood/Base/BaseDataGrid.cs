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

namespace flood
{
   public class BaseDataGrid : DataGrid
    {
        public static readonly DependencyProperty SortDescriptionsProperty = DependencyProperty.Register("SortDescriptions", typeof(List<SortDescription>), typeof(BaseDataGrid), new PropertyMetadata(null));
        public static readonly RoutedEvent ItemsSourceUpdatedEvent; 

        public event RoutedItemsSourceEventHandler ItemsSourceUpdated
        {
            add
            {
                this.AddHandler(ItemsSourceUpdatedEvent, value);
            }

            remove
            {
                this.RemoveHandler(ItemsSourceUpdatedEvent, value);
            }
        }

        static BaseDataGrid ()
        {
            ItemsSourceUpdatedEvent = EventManager.RegisterRoutedEvent("ItemsSourceUpdated", RoutingStrategy.Bubble, typeof(RoutedItemsSourceEventHandler), typeof(BaseDataGrid));
        }



        public Type EntityType
        {
            get { return (Type)GetValue(EntityTypeProperty); }
            set { SetValue(EntityTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EntityType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EntityTypeProperty =
            DependencyProperty.Register("EntityType", typeof(Type), typeof(BaseDataGrid), new PropertyMetadata(null, (x, y) =>
                {
                    BaseDataGrid grid = x as BaseDataGrid;

                    if(y.NewValue != null)
                    {
                        var typ = y.NewValue as Type;
                        var props = typ.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                        var view = (Activator.CreateInstance(typeof(ObservableCollection<>).MakeGenericType(typ)));
                        grid.ItemsSource = view as IEnumerable;

                        ICollectionView source = CollectionViewSource.GetDefaultView(null);
 
                    }
                }));



        public List<SortDescription> SortDescriptions
        {
            get { return (List<SortDescription>)GetValue(SortDescriptionsProperty); }
            set { SetValue(SortDescriptionsProperty, value); }
        }


        protected override void OnItemsSourceChanged(System.Collections.IEnumerable oldValue, System.Collections.IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);
            RaiseEvent(new ItemsSourceUpdatedEventArgs(ItemsSourceUpdatedEvent, this) { New = newValue, Old = oldValue });
        }
    }

    public class ItemsSourceUpdatedEventArgs : RoutedEventArgs
    {
        public ItemsSourceUpdatedEventArgs(RoutedEvent eve, object source) : base(eve, source) { }

        public IEnumerable Old { get; set; }
        public IEnumerable New { get; set; }
    }

    public delegate void RoutedItemsSourceEventHandler(object obj, ItemsSourceUpdatedEventArgs args);
}

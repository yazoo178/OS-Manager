using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace flood
{
    public static class UpdateViewModelOnVisibileAttach
    {
        public static PropertyToPropertyString GetMapper(DependencyObject obj)
        {
            return (PropertyToPropertyString)obj.GetValue(MapperProperty);
        }

        public static void SetMapper(DependencyObject obj, PropertyToPropertyString value)
        {
            obj.SetValue(MapperProperty, value);
        }

        // Using a DependencyProperty as the backing store for Mapper.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MapperProperty =
            DependencyProperty.RegisterAttached("Mapper", typeof(PropertyToPropertyString), typeof(UpdateViewModelOnVisibileAttach), new PropertyMetadata(null));

        
        public static object GetModelToUpdate(DependencyObject obj)
        {
            return (object)obj.GetValue(ModelToUpdateProperty);
        }

        public static void SetModelToUpdate(DependencyObject obj, object value)
        {
            obj.SetValue(ModelToUpdateProperty, value);
        }

        // Using a DependencyProperty as the backing store for ModelToUpdate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModelToUpdateProperty =
            DependencyProperty.RegisterAttached("ModelToUpdate", typeof(object), typeof(UpdateViewModelOnVisibileAttach), new PropertyMetadata(null));

        

        public static bool GetUpdateViewModel(DependencyObject obj)
        {
            return (bool)obj.GetValue(UpdateViewModelProperty);
        }

        public static void SetUpdateViewModel(DependencyObject obj, bool value)
        {
            obj.SetValue(UpdateViewModelProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UpdateViewModelProperty =
            DependencyProperty.RegisterAttached("UpdateViewModel", typeof(bool), typeof(UpdateViewModelOnVisibileAttach), new PropertyMetadata(false,
                (x, y)=>
                {
                    var obj = x as FrameworkElement;

                    if ((bool)y.NewValue)
                    {
                        obj.IsVisibleChanged += (e, o) =>
                            {
                                if((bool)o.NewValue)
                                {
                                    Update(obj);
                                }
                            };
                    }
                }
                ));

        private static void Update(FrameworkElement element)
        {
            var toUpdate = GetModelToUpdate(element);
            var typ = toUpdate.GetType();
            var mapper = GetMapper(element);

            if(toUpdate != null && mapper != null)
            {
                foreach(var value in mapper.Values)
                {
                    var prop = typ.GetProperty(value.Key);

                    if(prop != null)
                    {
                        prop.SetValue(toUpdate, value.Value.Item);
                    }
                }
            }
        }

        

        
    }
}

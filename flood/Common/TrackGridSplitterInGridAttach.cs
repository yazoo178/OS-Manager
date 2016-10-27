using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace flood
{
    public static class TrackGridSplitterInGridAttach
    {


        public static int GetMinVisableArea(DependencyObject obj)
        {
            return (int)obj.GetValue(MinVisableAreaProperty);
        }

        public static void SetMinVisableArea(DependencyObject obj, int value)
        {
            obj.SetValue(MinVisableAreaProperty, value);
        }

        // Using a DependencyProperty as the backing store for MinVisableArea.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinVisableAreaProperty =
            DependencyProperty.RegisterAttached("MinVisableArea", typeof(int), typeof(TrackGridSplitterInGridAttach), new PropertyMetadata(0));

        

        public static bool GetTrackGridSplitterInGrid(DependencyObject obj)
        {
            return (bool)obj.GetValue(TrackGridSplitterInGridProperty);
        }

        public static void SetTrackGridSplitterInGrid(DependencyObject obj, bool value)
        {
            obj.SetValue(TrackGridSplitterInGridProperty, value);
        }

        // Using a DependencyProperty as the backing store for TrackGridSplitterInGrid.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TrackGridSplitterInGridProperty =
            DependencyProperty.RegisterAttached("TrackGridSplitterInGrid", typeof(bool), typeof(TrackGridSplitterInGridAttach), new PropertyMetadata(false));
            
            

        public static FrameworkElement GetElementToTrack(DependencyObject obj)
        {
            return (FrameworkElement)obj.GetValue(ElementToTrackProperty);
        }

        public static void SetElementToTrack(DependencyObject obj, FrameworkElement value)
        {
            obj.SetValue(ElementToTrackProperty, value);
        }

        // Using a DependencyProperty as the backing store for ElementToTrack.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ElementToTrackProperty =
            DependencyProperty.RegisterAttached("ElementToTrack", typeof(FrameworkElement), typeof(TrackGridSplitterInGridAttach), new PropertyMetadata(null, (x, y) =>
            {
                if (GetTrackGridSplitterInGrid(x))
                {
                    (x as FrameworkElement).Loaded += TrackGridSplitterInGridAttach_Loaded;
                }
                else
                {
                    (x as FrameworkElement).Loaded -= TrackGridSplitterInGridAttach_Loaded;
                }
            }));

        static void TrackGridSplitterInGridAttach_Loaded(object sender, RoutedEventArgs e)
        {
            var splitter = sender as GridSplitter;
            splitter.DragDelta += splitter_DragDelta;
        }

        static void splitter_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            var splitter = sender as GridSplitter;
            var ele = GetElementToTrack(splitter);

            if (ele.Height + -(e.VerticalChange) > GetMinVisableArea(ele))
            {
                ele.Height += -(e.VerticalChange);
            }
        }
            

        

        
    }
}

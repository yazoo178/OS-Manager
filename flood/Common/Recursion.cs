using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace flood
{
    public static class Recursion
    {
        /// <summary>
        /// Returns all depenendy objects of a specified type. Transverses down the tree
        /// </summary>
        /// <typeparam name="T">Type to look for</typeparam>
        /// <param name="root">The root of the search</param>
        /// <returns>A list of elements</returns>
        public static IEnumerable<T> ElementsOfType<T> (DependencyObject root) where T : DependencyObject
        {
            var list = new List<T>();
            LoopTree(list, root);
            return list;
        }

        private static void LoopTree<T> (IList<T> items, DependencyObject current) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(current); i++)
            {
                if(VisualTreeHelper.GetChild(current, i) is T)
                {
                    items.Add(VisualTreeHelper.GetChild(current, i) as T);
                }

                LoopTree<T>(items, VisualTreeHelper.GetChild(current, i));
            }
        }

        public static double TotalWidthOfEverythingOfType<T>(DependencyObject startPoint) where T : FrameworkElement
        {
            return ElementsOfType<T>(startPoint).Sum(x => (x as FrameworkElement).ActualWidth);
        }

        /// <summary>
        /// Transverses up the visual tree until a dependeny object of a specified type is found
        /// </summary>
        /// <typeparam name="T">Type to look for</typeparam>
        /// <param name="root">The root of the searc</param>
        /// <returns>First Element of type</returns>
        public static T ParentOfType<T>(DependencyObject root) where T : DependencyObject
        {
            if(root is T)
            {
                return root as T;
            }

            return GoUpTree<T>(root);
        }

        public static IEnumerable<T> ParentsOfType<T>(DependencyObject root) where T : DependencyObject
        {
            var list = new List<T>();
            GoUpTreeRecur(root, list);
            return list;
        }

        private static void GoUpTreeRecur<T>(DependencyObject current, IList<T> elements) where T : DependencyObject
        {
            if (current != null)
            {
                current = VisualTreeHelper.GetParent(current);

                if (current is T)
                {
                    elements.Add(current as T);
                }

                GoUpTreeRecur(current, elements);
            }
            
        }

        private static T GoUpTree<T> (DependencyObject current) where T : DependencyObject
        {
            while(VisualTreeHelper.GetParent(current) != null)
            {
                current = VisualTreeHelper.GetParent(current);

                if (current is T)
                {
                    return current as T;
                }
            }

            return null;
        }


    }
}

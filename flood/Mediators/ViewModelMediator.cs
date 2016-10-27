using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flood
{
    public static class ViewModelMediator
    {
        private static IList<IList<BaseViewModel>> ViewModels = new List<IList<BaseViewModel>>();

        /// <summary>
        /// Returns the first instance of the specified value model
        /// </summary>
        /// <typeparam name="T">Type of viewmodel</typeparam>
        /// <returns>view model of type</returns>
        public static T Return<T>() where T : BaseViewModel
        {
            var result = ViewModels.FirstOrDefault(x => x.Any(y => y.GetType() == typeof(T))).FirstOrDefault() as T;

            if (result != null)
            {
                return result;
            }
            throw new InvalidOperationException("Type not found");
        }

        /// <summary>
        /// Returns all instances of the specified value model
        /// </summary>
        /// <typeparam name="T">Type of viewmodel</typeparam>
        /// <returns>A collection of view models</returns>
        public static IEnumerable<T> ReturnAll<T>() where T : BaseViewModel
        {
            var collection = ViewModels.FirstOrDefault(x => x.Any(y => y.GetType() == typeof(T))).OfType<T>();

            if (collection != null)
            {
                return collection;
            }

            throw new InvalidOperationException("Collection of type not found");
        }

        /// <summary>
        /// Registers a view model against this mediator
        /// </summary>
        /// <param name="source"></param>
        public static void Register(BaseViewModel source)
        {
           // var typeofSource = source.GetType();
            var collection = ViewModels.FirstOrDefault(x => x.Any(y => y.GetType() == source.GetType()));

            if (collection != null)
            {
                collection.Add(source);
                return;
            }

            ViewModels.Add(new List<BaseViewModel>() { source });
            
        }
    }
}

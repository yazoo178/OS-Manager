using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flood
{
    /// <summary>
    /// Base view model, all view models should dervive from this for notifaction functionality
    /// </summary>
    public class BaseViewModel : BaseNotifyingElement
    {
        #region Constructors

        /// <summary>
        /// Default Constructor, registers with the ViewModel medaitor to provide inter-view-model communication
        /// Hooks up to the ApplicationMediator property change to provide centralized notifactions
        /// </summary>
        public BaseViewModel()
        {
            ViewModelMediator.Register(this);
        }

        #endregion

        private bool _close;
        /// <summary>
        /// Setting to true will attempt to close the dependant window.
        /// </summary>
        public bool Close
        {
            get
            {
                return _close;
            }

            set
            {
                _close = value;
                base.OnPropertyChanged("Close");
            }
        }
    }
}

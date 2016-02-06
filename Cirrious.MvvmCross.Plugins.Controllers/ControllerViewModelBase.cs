
using MvvmCross.Core.ViewModels;
using System;

namespace Cirrious.MvvmCross.Plugins.Controllers
{
    /// <summary>
    /// Basic implementation of view model that attaches a controller
    /// </summary>
    public abstract class ControllerViewModelBase : MvxViewModel, IControllerAware
    {
        /// <summary>
        /// Gets the controller associated with the view model.
        /// </summary>
        public IMvxController Controller { get; private set; }

        public void AttachController(IMvxController controller)
        {
            if (Controller != null)
            {
                throw new InvalidOperationException("Controller has already been associated with this view model.");
            }

            Controller = controller;
		}

		public void DetachController()
		{
			Controller = null;
		}

        
    }
}
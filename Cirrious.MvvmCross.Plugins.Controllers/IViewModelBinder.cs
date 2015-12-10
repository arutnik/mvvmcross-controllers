using Cirrious.MvvmCross.ViewModels;
using System;

namespace Cirrious.MvvmCross.Plugins.Controllers
{
    /// <summary>
    /// Supports creating a controller for a View Model
    /// and attaching the controller to it.
    /// </summary>
    public interface IViewModelBinder
    {
        /// <summary>
        /// Creates a controller for a view model, attaches
        /// it to the view model, and optionally initializes the
        /// controller.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="parameterValues"></param>
        /// <param name="savedState"></param>
        /// <exception cref="ArgumentException">If the controller cannot be found.</exception>
        void Bind(IMvxViewModel viewModel, IMvxBundle parameterValues, IMvxBundle savedState);

        /// <summary>
        /// Reloads the controller for a view model, re-running
        /// its lifecycle methods
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="parameterValues"></param>
        /// <param name="savedState"></param>
        /// <exception cref="ArgumentException">If the controller cannot be found.</exception>
        void Reload(IMvxViewModel viewModel, IMvxBundle parameterValues, IMvxBundle savedState);
    }
}

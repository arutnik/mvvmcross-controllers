using MvvmCross.Core.ViewModels;

namespace Cirrious.MvvmCross.Plugins.Controllers
{
    /// <summary>
    /// Represents an entity that can attach a View Model
    /// </summary>
    public interface IViewModelAware
    {
        /// <summary>
        /// Attaches a view model.
        /// </summary>
        /// <param name="viewModel"></param>
        void AttachViewModel(IMvxViewModel viewModel);
    }
}
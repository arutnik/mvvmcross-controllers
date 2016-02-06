using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System;
using System.Threading.Tasks;

namespace Cirrious.MvvmCross.Plugins.Controllers
{
    /// <summary>
    /// A default implementation of IMvXcontroller
    /// that acts as a base class for a controller in MVVMC.
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public abstract class MvxController<TViewModel> : MvxNavigatingObject,
        IMvxController,
        IMvxActivatable,
        IViewModelAware,
        IDisposable
        where TViewModel : class, IMvxViewModel 
    {
        private Task _oneTimeInitTask;

        /// <summary>
        /// Gets the View Model that this is attached to.
        /// </summary>
        protected TViewModel ViewModel { get; private set; }

        /// <summary>
        /// Gets whether initialization is complete
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// Gets whether the controller has been disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        public void AttachViewModel(IMvxViewModel viewModel)
        {
            if (ViewModel != null)
            {
                throw new InvalidOperationException("A view model has already been attached to this controller.");
            }

            ViewModel = (TViewModel) viewModel;
        }

        public Task WaitForInitialize()
        {
            if (_oneTimeInitTask == null)
            {
                _oneTimeInitTask = InitializeInternal();
            }

            return _oneTimeInitTask;
        }

        public async Task Activate(ActivateEvent kind)
        {
            await OnActivate(kind);
        }

        public async Task Deactivate(DeactivateEvent kind)
        {
            await OnDeactivate(kind);
        }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                try
                {
                    Dispose(true);
                }
                finally
                {
                    IsDisposed = true;
                }
            }
        }

        /// <summary>
        /// Closes the attached View Model
        /// </summary>
        protected void Close()
        {
            if (ViewModel != null)
            {
                Close(ViewModel);
            }
        }

		protected async Task InitializeInternal()
		{
			await OnControllerInitialize();

			IsInitialized = true;
		}

        protected virtual async Task OnControllerInitialize()
        { 
            await Task.Yield();
        }

        protected virtual async Task OnActivate(ActivateEvent kind)
        {
            await Task.Yield();
        }

        protected virtual async Task OnDeactivate(DeactivateEvent kind)
        { 
            await Task.Yield();
        }

        /// <summary>
        /// Disposes the controller, releasing any resources it may hold.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            
        }

        public virtual void Recreate()
        {
            Mvx.Trace("MvxController: Recreate (usually via ViewModelReload)");
            _oneTimeInitTask = null;
            IsDisposed = false;
            IsInitialized = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using MvvmCross.Plugins.Controllers;
using MvvmCross.WindowsUWP.Views;
using MvvmCross.Core.ViewModels;

namespace MvvmCross.Plugins.Controllers.SampleUwp.Views
{
    /// <summary>
    /// This is an example of how to use a common base class for your views
    /// to add Save/Restore, Activation/Deactivation and Disposal
    /// to controllers
    /// </summary>
    public class WindowsPageBase : MvxWindowsPage
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            // Try activate the viewModel/controller
            var navigationKind = GetActivateEvent(e.NavigationMode);
            ViewModel.ActivateAttachedController(navigationKind);
        }

        private ActivateEvent GetActivateEvent(NavigationMode mode)
        {
            if (mode == NavigationMode.Forward) return ActivateEvent.Forward;
            if (mode == NavigationMode.Back) return ActivateEvent.Backwards;
            if (mode == NavigationMode.Refresh) return ActivateEvent.Refresh;

            return ActivateEvent.Default;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            var deactivateEvent = GetDeactivateEvent(e.NavigationMode);
            //TODO: Determine if app is suspending and use the right event.

            // Try save existing state
            if (ViewModel != null)
            {
                IMvxBundle saveStateBundle = ViewModel.GetAttachedControllerSavedState();
                if (saveStateBundle != null)
                {
                    //Call mvx method to save controller's state.
                    //It gets restored during controller binding.
                    SaveStateBundle(e, saveStateBundle);
                }
            }

            // Deactivate viewmodel/controller
            ViewModel.DeactivateAttachedController(deactivateEvent);


            //If you are not suspending, you should dispose the controller
            ViewModel.DisposeAttachedController();
        }

        private DeactivateEvent GetDeactivateEvent(NavigationMode mode)
        {
            if (mode == NavigationMode.Back) return DeactivateEvent.Back;
            if (mode == NavigationMode.Forward) return DeactivateEvent.Forward;

            return DeactivateEvent.Default;
        }
    }
}
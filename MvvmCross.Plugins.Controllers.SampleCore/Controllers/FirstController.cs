using Cirrious.MvvmCross.Plugins.Controllers.SampleCore.ViewModels;
using MvvmCross.Core.ViewModels;
using System.Threading.Tasks;

namespace Cirrious.MvvmCross.Plugins.Controllers.SampleCore.Controllers
{
    public class FirstController : MvxController<FirstViewModel>
    {

        public FirstControllerState SaveState()
        {
            return new FirstControllerState() { NumberState = ViewModel.NumberState };
        }

        public void ReloadState(FirstControllerState state)
        {
            ViewModel.NumberState = state.NumberState;
        }

        protected override async Task OnControllerInitialize()
        {
            //This is called when the controller is attached

            ViewModel.GoNextWithArguments = new MvxCommand(OnGoNextWithArgs);
            ViewModel.GoNextNoArgs = new MvxCommand(OnGoNextWithoutArgs);

            await Task.Delay(3000);

            ViewModel.Hello = "Hello MvvmCross with Controllers.";
        }

        private void OnGoNextWithArgs()
        {
            var navArgs = new SecondViewModelNavigationArguments()
            {
                Message = "The number was: ",
                Number = ViewModel.NumberState,
            };

            ShowViewModel<SecondViewModel>(navArgs);
        }

        private void OnGoNextWithoutArgs()
        {
            ShowViewModel<SecondViewModel>();
        }

        protected override async Task OnActivate(ActivateEvent kind)
        {
            await base.OnActivate(kind);

            Mvx.Trace("Activate {0} kind {1}", GetType().Name, kind);
        }

        protected override async Task OnDeactivate(DeactivateEvent kind)
        {
            await base.OnDeactivate(kind);

            Mvx.Trace("Deactivate {0} kind {1}", GetType().Name, kind);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Mvx.Trace("Disposing {0}", GetType().Name);
        }
    }

    public class FirstControllerState
    {
        public int NumberState { get; set; }
    }
}

using Cirrious.MvvmCross.ViewModels;
using System.Windows.Input;

namespace Cirrious.MvvmCross.Plugins.Controllers.SampleCore.ViewModels
{
    public class FirstViewModel 
		: ControllerViewModelBase
    {
		private string _hello = "Hello MvvmCross";
        public string Hello
		{ 
			get { return _hello; }
			set { _hello = value; RaisePropertyChanged(() => Hello); }
		}

        private int _numberState;
        public int NumberState
        {
            get { return _numberState; }
            set { SetProperty(ref _numberState, value); }
        }

        ICommand _goNextWithArgs;
        public ICommand GoNextWithArguments
        {
            get { return _goNextWithArgs; }
            set { SetProperty(ref _goNextWithArgs, value); }
        }

        ICommand _goNextNoArgs;
        public ICommand GoNextNoArgs
        {
            get { return _goNextNoArgs; }
            set { SetProperty(ref _goNextNoArgs, value); }
        }
    }
}

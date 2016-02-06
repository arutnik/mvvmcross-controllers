using System.Windows.Input;

namespace MvvmCross.Plugins.Controllers.SampleCore.ViewModels
{
    public class SecondViewModel 
		: ControllerViewModelBase
    {
		private string _hello = string.Empty;
        public string Hello
		{ 
			get { return _hello; }
			set { _hello = value; RaisePropertyChanged(() => Hello); }
		}

        ICommand _closeMe;
        public ICommand CloseMe
        {
            get { return _closeMe; }
            set { SetProperty(ref _closeMe, value); }
        }
    }

    public class SecondViewModelNavigationArguments
    {
        public string Message { get; set; }

        public int Number { get; set; }
    }
}

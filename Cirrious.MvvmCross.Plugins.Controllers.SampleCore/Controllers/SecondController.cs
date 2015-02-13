using Cirrious.MvvmCross.Plugins.Controllers.SampleCore.ViewModels;
using Cirrious.MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cirrious.MvvmCross.Plugins.Controllers.SampleCore.Controllers
{
    public class SecondController : MvxController<SecondViewModel>
    {
        public void Init(SecondViewModelNavigationArguments navArgs)
        {
            ViewModel.Hello = string.Format("{0} : {1}", navArgs.Message, navArgs.Number);
        }

        protected override async Task OnControllerInitialize()
        {
            if (string.IsNullOrEmpty(ViewModel.Hello))
            {
                ViewModel.Hello = "No data received";
            }

            ViewModel.CloseMe = new MvxCommand(Close);
        }
 
    }
}

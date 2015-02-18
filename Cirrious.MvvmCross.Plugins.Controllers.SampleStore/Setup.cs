using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsCommon.Platform;
using Windows.UI.Xaml.Controls;
using Cirrious.MvvmCross.Plugins.Controllers.SampleCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cirrious.MvvmCross.Plugins.Controllers.SampleStore
{
    public class Setup : MvxWindowsSetup
    {
        public Setup(Frame rootFrame) : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            var app = new SampleCore.App();
            return app;
        }
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}
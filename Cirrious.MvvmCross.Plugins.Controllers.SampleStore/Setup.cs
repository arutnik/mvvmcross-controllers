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
            //Createable types function should be only evaluated once if it never changes.
            var lazyCreateableTypes = new Lazy<Type[]>(() => CreatableTypes().ToArray());

            var app = new SampleCore.App();
            app.GetCreateableTypes = () => lazyCreateableTypes.Value;

            return app;
        }
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}
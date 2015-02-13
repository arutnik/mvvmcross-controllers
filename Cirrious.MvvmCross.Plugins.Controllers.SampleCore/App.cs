using Cirrious.CrossCore.IoC;
using System.Linq;
using System;
using System.Reflection;
using System.Collections.Generic;
using Cirrious.CrossCore;

namespace Cirrious.MvvmCross.Plugins.Controllers.SampleCore
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public Func<IEnumerable<Type>> GetCreateableTypes { get; set; }

        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
				
            RegisterAppStart<ViewModels.FirstViewModel>();
        }

        protected override MvvmCross.ViewModels.IMvxViewModelLocator CreateDefaultViewModelLocator()
        {
            var defaultLocator = base.CreateDefaultViewModelLocator();
            
            var binder = new ViewModelBinder(CreatableTypes);

            // register default view model binder
            Mvx.RegisterSingleton<IViewModelBinder>(binder);

            return new BindingViewModelLocator(defaultLocator, binder);
        }
    }
}
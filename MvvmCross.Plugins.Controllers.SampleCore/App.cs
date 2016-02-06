
using MvvmCross.Platform.IoC;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace MvvmCross.Plugins.Controllers.SampleCore
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
				
            RegisterAppStart<ViewModels.FirstViewModel>();
        }

        protected override IMvxViewModelLocator CreateDefaultViewModelLocator()
        {
            var defaultLocator = base.CreateDefaultViewModelLocator();
            
            var binder = new ViewModelBinder(CreatableTypes);

            // register default view model binder
            Mvx.RegisterSingleton<IViewModelBinder>(binder);

            return new BindingViewModelLocator(defaultLocator, binder);
        }
    }
}
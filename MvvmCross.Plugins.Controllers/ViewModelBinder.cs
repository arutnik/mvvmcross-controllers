using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cirrious.MvvmCross.Plugins.Controllers
{

    /// <summary>
    /// A default View Model binder that will call
    /// paramater initialization, re-load state
    /// and initialize methods on the created controller.
    /// </summary>
    public class ViewModelBinder : IViewModelBinder
    {
        private Func<IEnumerable<Type>> _getCreateableTypes;

        /// <summary>
        /// Creates a new ViewModelBinder
        /// </summary>
        /// <param name="getCreateableTypes">A function that returns all createable 
        /// types in the app</param>
        public ViewModelBinder(Func<IEnumerable<Type>> getCreateableTypes)
        {
            _getCreateableTypes = getCreateableTypes;
        }

        public void Bind(IMvxViewModel viewModel, IMvxBundle parameterValues = null, IMvxBundle savedState = null)
        {
            var controllerAware = viewModel as IControllerAware;
            if (controllerAware != null)
            {
                var viewModelType = viewModel.GetType();
                var name = viewModelType.FullName.Replace("ViewModel", "Controller");

                Type controllerType = GetControllerTypeForViewModel(_getCreateableTypes(), viewModel);

                if (controllerType == null)
                {
                    throw new ArgumentException(string.Format("Controller for view model {0} cannot be found.", viewModelType));
                }

                try
                {
                    var controller = (IMvxController) Mvx.IocConstruct(controllerType);
                    controllerAware.AttachController(controller);

                    var viewModelAware = controller as IViewModelAware;
                    if (viewModelAware != null)
                    {
                        viewModelAware.AttachViewModel(viewModel);
                    }

                    RunControllerLifecycleMethods(controllerAware, controller, parameterValues, savedState);
                }
                catch (Exception ex)
                {
                    MvxTrace.Error("MvxControllers: Problem creating controller of type {0} - problem {1}",
                        controllerType,
                        ex.ToLongString());

                    throw;
                }
            }
        }


        public void Reload(IMvxViewModel viewModel, IMvxBundle parameterValues, IMvxBundle savedState)
        {
            var controllerAware = viewModel as IControllerAware;
            if (controllerAware != null)
            {
                try
                {
                    var controller = controllerAware.Controller;
                    if (controller != null)
                    {
                        controller.Recreate();
                        RunControllerLifecycleMethods(controllerAware, controller, parameterValues, savedState);
                    }
                    else
                    {
                        Bind(viewModel, parameterValues, savedState);
                    }
                }
                catch (Exception ex)
                {
                    MvxTrace.Error("MvxControllers: Problem reloading ViewModel of type {0} - problem {1}",
                        viewModel.GetType().Name,
                        ex.ToLongString());

                    throw;
                }
            }
        }

        protected void RunControllerLifecycleMethods(IControllerAware controllerAware, IMvxController controller, IMvxBundle parameterValues = null, IMvxBundle savedState = null)
        {
            try
            {
                CallControllerInitMethods(controller, parameterValues);
                if (savedState != null)
                {
                    CallReloadStateMethods(controller, savedState);
                }
            }
            catch (Exception ex)
            {
                MvxTrace.Error("MvxControllers: Problem initialising controller of type {0} - problem {1}",
                    controller.GetType().Name, ex.ToLongString());

                throw;
            }

            controller.WaitForInitialize();
        }

        protected virtual Type GetControllerTypeForViewModel(IEnumerable<Type> createableTypes, IMvxViewModel viewModel)
        {
            var name = viewModel.GetType().FullName.Replace("ViewModel", "Controller");

            var controllerType = createableTypes.FirstOrDefault(t => t.FullName == name);

            if (controllerType == null)
            {
                throw new ArgumentException(string.Format("Controller type {0} cannot be found.", name));
            }

            return controllerType;
        }

        protected virtual void CallControllerInitMethods(IMvxController controller, IMvxBundle parameterValues)
        {
            controller.InvokeMethodsWithBundle("Init", parameterValues);
        }

        protected virtual void CallReloadStateMethods(IMvxController controller, IMvxBundle savedState)
        {
            controller.InvokeMethodsWithBundle("ReloadState", savedState);
        }
    }
}
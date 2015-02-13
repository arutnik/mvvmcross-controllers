using Cirrious.CrossCore.Core;
using Cirrious.MvvmCross.ViewModels;
using System.Linq;
using System.Reflection;

namespace Cirrious.MvvmCross.Plugins.Controllers
{
    /// <summary>
    /// Contains helper methods for Controllers and their View Models
    /// </summary>
    public static class MvxControllerExtensions
    {
        /// <summary>
        /// Attempts to invoke methods matching a given name
        /// on a controller using
        /// a IMvxBundle parameter
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="methodName"></param>
        /// <param name="bundle"></param>
        public static void InvokeMethodsWithBundle(this IMvxController controller, string methodName, IMvxBundle bundle)
        {
            var methods = controller.GetType()
                .GetTypeInfo()
                .DeclaredMethods.Where(m => m.Name == methodName)
                .Where(m => !m.IsAbstract)
                ;

            foreach (MethodInfo methodInfo in methods)
            {
                controller.InvokeMethodWithBundle(methodInfo, bundle);
            }
        }
        
        /// <summary>
        /// Invokes a method using a bundle to find the appropriate
        /// invocation parameters.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="methodInfo"></param>
        /// <param name="bundle"></param>
        public static void InvokeMethodWithBundle(this IMvxController controller, MethodInfo methodInfo, IMvxBundle bundle)
        {
            if (bundle.Data.Count == 0)
            {
                return;
            }

            ParameterInfo[] parameterInfoArray = methodInfo.GetParameters();

            if (parameterInfoArray.Length == 1 && parameterInfoArray[0].ParameterType == typeof(IMvxBundle))
            {
                //Method accepts bundle directly as parameter.
                methodInfo.Invoke(controller, new object[]
                {
                    bundle
                });
            }
            else if (parameterInfoArray.Length == 1 &&
                !MvxSingleton<IMvxSingletonCache>.Instance.Parser.TypeSupported(parameterInfoArray[0].ParameterType))
            {
                //Single parameter is an aggregate type that the bundle can deserialize
                object obj = bundle.Read(parameterInfoArray[0].ParameterType);
                methodInfo.Invoke(controller, new[]
                {
                    obj
                });
            }
            else
            {
                //Bundle is an argument list.
                object[] parameters = bundle.CreateArgumentList(parameterInfoArray, controller.GetType().Name).ToArray();
                methodInfo.Invoke(controller, parameters);
            }
        }

        /// <summary>
        /// Attempts to call the 'SaveState' method
        /// on a controller to create a saved state bundle
        /// </summary>
        /// <param name="controller"></param>
        /// <returns>An IMvxBundle object that contains any saved state collected.</returns>
        public static IMvxBundle GetSavedState(this IMvxController controller)
        {
            var mvxBundle = new MvxBundle();
            var methods =
                controller.GetType()
                    .GetTypeInfo()
                    .DeclaredMethods.Where(m => m.Name == "SaveState")
                    .Where(m => m.ReturnType != typeof (void))
                    .Where(m => !m.GetParameters().Any());

            foreach (MethodInfo methodBase in methods)
            {
                object toStore = methodBase.Invoke(controller, new object[0]);
                if (toStore != null)
                    mvxBundle.Write(toStore);
            }
            return mvxBundle;
        }

        /// <summary>
        /// Attempts to call the 'SaveState' method
        /// on a view model's attached controller to create a saved state bundle
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns>An IMvxBundle object that contains any saved state collected, or
        /// null if no controller is attached.</returns>
        public static IMvxBundle GetAttachedControllerSavedState(this IMvxViewModel viewModel)
        {
            var controllerAware = viewModel as IControllerAware;
            if (controllerAware != null)
            {
                var ctl = controllerAware.Controller;
                if (ctl != null)
                {
                    return GetSavedState(ctl);
                }
            }

            return null;
        }

        /// <summary>
        /// Activates any attached controller with the given activation event.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="kind"></param>
        public static void ActivateAttachedController(this IMvxViewModel viewModel, ActivateEvent kind)
        {
            var controllerAware = viewModel as IControllerAware;
            if (controllerAware != null)
            {
                var ctl = controllerAware.Controller as IMvxActivatable;
                if (ctl != null)
                {
                    ctl.Activate(kind);
                }
            }
        }

        /// <summary>
        /// Deactivates any attached controller with the given deactivation event.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="kind"></param>
        public static void DeactivateAttachedController(this IMvxViewModel viewModel, DeactivateEvent kind)
        {
            var controllerAware = viewModel as IControllerAware;
            if (controllerAware != null)
            {
                var ctl = controllerAware.Controller as IMvxActivatable;
                if (ctl != null)
                {
                    ctl.Deactivate(kind);
                }
            }
        }

        /// <summary>
        /// Disposes any attached controller. Does not detach the controller.
        /// </summary>
        /// <param name="viewModel"></param>
        public static void DisposeAttachedController(this IMvxViewModel viewModel)
        {
            var controllerAware = viewModel as IControllerAware;
            if (controllerAware != null)
            {
                var ctl = controllerAware.Controller as IMvxActivatable;
                if (ctl != null)
                {
                    ctl.DisposeIfDisposable();
                }
            }
        }
    }
}
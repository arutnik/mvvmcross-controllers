using System.Threading.Tasks;
namespace Cirrious.MvvmCross.Plugins.Controllers
{
    /// <summary>
    /// Controllers hold the logic for
    /// manipulating View Models.
    /// </summary>
    public interface IMvxController
    {
		/// <summary>
		/// Returns a handle to the controller's
		/// initialization routine or starts it
		/// if has not already started; repeatedly
		/// awaiting this method will not call repeat
		/// initializations
		/// </summary>
		/// <returns>A task for awaiting initialization.</returns>
		Task WaitForInitialize();

        /// <summary>
        /// Called when a controller is being recycled for
        /// ViewModel reload purposes. After calling this,
        /// calling WaitForInitialize should start a new
        /// intialization routine.
        /// </summary>
        void Recreate();
    }
}

namespace MvvmCross.Plugins.Controllers
{
    /// <summary>
    /// Implemented by view models which are controller enabled.
    /// </summary>
    public interface IControllerAware
    {
        /// <summary>
        /// Gets an associated controller.
        /// </summary>
        IMvxController Controller { get; }

        /// <summary>
        /// Attaches controller to the view model.
        /// </summary>
        /// <param name="controller"></param>
		void AttachController(IMvxController controller);

		/// <summary>
		/// Detaches controller from the view model.
		/// </summary>
		void DetachController();
    }
}
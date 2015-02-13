using System.Threading.Tasks;
namespace Cirrious.MvvmCross.Plugins.Controllers
{
    /// <summary>
    /// An entity that is aware of screen activation
    /// and deactivation events.
    /// </summary>
    public interface IMvxActivatable
    {
        /// <summary>
        /// Called upon an activation event.
        /// </summary>
        Task Activate(ActivateEvent kind);

        /// <summary>
        /// Called upon a deactivation event.
        /// </summary>
        Task Deactivate(DeactivateEvent kind);
    }
}
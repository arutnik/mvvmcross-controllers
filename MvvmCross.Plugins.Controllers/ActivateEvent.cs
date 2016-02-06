namespace MvvmCross.Plugins.Controllers
{
    /// <summary>
    /// Describes types of activations for entities.
    /// </summary>
    public enum ActivateEvent
    {
        /// <summary>
        /// Default or unspecified reason for activation
        /// </summary>
        Default,
        /// <summary>
        /// Activating due to forward navigation
        /// </summary>
        Forward,
        /// <summary>
        /// Activating due to backward navigation
        /// </summary>
        Backwards,
        /// <summary>
        /// Re-activating an existing instance of the entity, possibly due
        /// to suspension.
        /// </summary>
        Refresh,
    }
}
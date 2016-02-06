namespace MvvmCross.Plugins.Controllers
{
    /// <summary>
    /// Describes types of deactivation
    /// </summary>
    public enum DeactivateEvent
    {
        /// <summary>
        /// Default or unspecified kind of deativation
        /// </summary>
        Default,
        /// <summary>
        /// Deactivation due to forward navigation
        /// </summary>
        Forward,
        /// <summary>
        /// Deactivation due to backward navigation
        /// </summary>
        Back,
        /// <summary>
        /// Deactivation due to suspension.
        /// </summary>
        Suspend
    }
}
namespace TBBProject.Core.Navigation
{
    public interface INavigationProviderContext
    {
        /// <summary>
        /// Provides navigation manager
        /// </summary>
        /// <value>The navigation manager.</value>
        INavigationManager NavigationManager { get; }
    }
}

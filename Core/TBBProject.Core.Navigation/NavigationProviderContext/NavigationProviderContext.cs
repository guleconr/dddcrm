namespace TBBProject.Core.Navigation
{
    public class NavigationProviderContext : INavigationProviderContext
    {
        /// <summary>
        /// Gets the navigation manager.
        /// </summary>
        /// <value>The navigation manager.</value>
        public INavigationManager NavigationManager { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Anka.Core.UI.Navigation.NavigationProviderContext"/> class.
        /// </summary>
        /// <param name="navigationManager">Navigation manager.</param>
        public NavigationProviderContext(INavigationManager navigationManager)
        {
            NavigationManager = navigationManager;
        }
    }
}

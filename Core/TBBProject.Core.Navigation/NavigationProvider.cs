namespace TBBProject.Core.Navigation
{
    public abstract class NavigationProvider
    {
        public abstract void CreateNavigation(INavigationProviderContext navigationProviderContext);
    }
}

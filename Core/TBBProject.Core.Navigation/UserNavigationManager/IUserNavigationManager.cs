namespace TBBProject.Core.Navigation
{
    public interface IUserNavigationManager
    {
        /// <summary>
        /// Gets user navigation.
        /// </summary>
        /// <returns>User navigation</returns>
        /// <param name="navigationName">Navigation name.</param>
        UserNavigation GetUserNavigation(string navigationName);
    }
}

using System.Collections.Generic;

namespace TBBProject.Core.Navigation
{
    public class UserNavigationManager : IUserNavigationManager
    {
        private readonly INavigationManager _navigationManager;

        public UserNavigationManager(INavigationManager navigationManager)
        {
            _navigationManager = navigationManager;

            // register on app startup
            _navigationManager.Initialize();
        }

        public UserNavigation GetUserNavigation(string navigationName)
        {
            var navigationDefinition = _navigationManager.NavigationDefinitions.GetValueOrDefault(navigationName);

            var userNavigation = new UserNavigation(navigationDefinition);

            Populate(navigationDefinition.Items, userNavigation.UserNavigationItems);

            return userNavigation;
        }

        private int Populate(IList<NavigationItemDefinition> navigationDefinitions, IList<UserNavigationItem> userNavigationItems)
        {
            var itemCount = 0;
            foreach (var navigationItemDefinition in navigationDefinitions)
            {
                var userNavigationItem = new UserNavigationItem(navigationItemDefinition);
                if (navigationItemDefinition.IsLast || Populate(navigationItemDefinition.Items, userNavigationItem.UserNavigationItems) > 0)
                {
                    userNavigationItems.Add(userNavigationItem);
                    itemCount++;
                }
            }
            return itemCount;
        }
    }
}

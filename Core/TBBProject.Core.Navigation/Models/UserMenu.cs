using System.Collections.Generic;

namespace TBBProject.Core.Navigation
{
    public class UserNavigation
    {
        public string Name { get; set; }
        public IList<UserNavigationItem> UserNavigationItems { get; set; }

        public UserNavigation(NavigationDefinition navigationDefinition)
        {
            Name = navigationDefinition.Name;
            UserNavigationItems = new List<UserNavigationItem>();
        }
    }
}

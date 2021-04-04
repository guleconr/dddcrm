using System.Collections.Generic;

namespace TBBProject.Core.Navigation
{
    public class UserNavigationItem
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string DisplayName { get; set; }
        public int Order { get; set; }
        public string Uri { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsHidden { get; set; }
        public bool IsActive { get; set; }
        public string Clazz { get; set; }
        public UserNavigationItem Parent { get; set; }
        public IList<UserNavigationItem> UserNavigationItems { get; set; }

        public UserNavigationItem(NavigationItemDefinition navigationItemDefinition)
        {
            Name = navigationItemDefinition.Name;
            DisplayName = navigationItemDefinition.DisplayName;
            Icon = navigationItemDefinition.Icon;
            Order = navigationItemDefinition.Order;
            Uri = navigationItemDefinition.Uri;
            IsDisabled = navigationItemDefinition.IsDisabled;
            IsHidden = navigationItemDefinition.IsHidden;
            IsActive = navigationItemDefinition.IsActive;
            Clazz = navigationItemDefinition.Clazz;

            UserNavigationItems = new List<UserNavigationItem>();
        }

    }
}

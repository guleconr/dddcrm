using System.Collections.Generic;
using System.Linq;

namespace TBBProject.Core.Navigation
{
    /// <summary>
    /// Represents an item in navigation
    /// </summary>
    public class NavigationItemDefinition : IProvideNavigationItemDefinitions
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
        public bool AtFooter { get; set; } = false;
        public NavigationItemDefinition Parent { get; set; }
        public List<NavigationItemDefinition> Items { get; set; }

        public NavigationItemDefinition(
            string name,
            string displayName,
            string uri)
        {
            Name = name;
            DisplayName = displayName;
            Uri = uri;
            Items = new List<NavigationItemDefinition>();
        }

        public NavigationItemDefinition AddMore(NavigationItemDefinition menuItemDefinition)
        {
            Items.Add(menuItemDefinition);
            return this;
        }

        public void Remove(NavigationItemDefinition menuItemDefinition)
        {
            Items.Remove(menuItemDefinition);
        }

        public bool IsLast
        {
            get
            {
                if (Items != null && Items.Any())
                {
                    return false;
                }

                return true;
            }
        }
    }
}

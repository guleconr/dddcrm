using System;
using System.Collections.Generic;

namespace TBBProject.Core.Navigation
{
    /// <summary>
    /// Represents a menu
    /// </summary>
    public class NavigationDefinition : IProvideNavigationItemDefinitions
    {

        public string Name { get; set; }
        public List<NavigationItemDefinition> Items { get; set; }

        public NavigationDefinition(string name, string localizedString)
        {
            Name = name;
            Items = new List<NavigationItemDefinition>();
        }

        /// <summary>
        /// Adds menu item definition
        /// </summary>
        /// <returns>The add.</returns>
        /// <param name="navigationItemDefinition">Menu item definition.</param>
        public NavigationDefinition Add(NavigationItemDefinition navigationItemDefinition)
        {
            Items.Add(navigationItemDefinition);
            return this;
        }

        /// <summary>
        /// removes menu item definition
        /// </summary>
        /// <param name="navigationItemDefinition">Menu item definition.</param>
        public void Remove(NavigationItemDefinition navigationItemDefinition)
        {
            Items.Remove(navigationItemDefinition);
        }

    }
}

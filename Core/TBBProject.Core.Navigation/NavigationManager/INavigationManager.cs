using System;
using System.Collections.Generic;

namespace TBBProject.Core.Navigation
{
    public interface INavigationManager
    {
        /// <summary>
        /// Provides navigation definitions
        /// </summary>
        /// <value>The menu definitions.</value>
        IDictionary<string, NavigationDefinition> NavigationDefinitions { get; }

        /// <summary>
        /// Gets the application navigation container. This is the main navigation
        /// </summary>
        /// <value>The application navigation container.</value>
        NavigationDefinition ApplicationNavigationContainer { get; }

        void Initialize();
    }
}

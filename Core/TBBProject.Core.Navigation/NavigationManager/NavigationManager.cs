using System.Collections.Generic;

namespace TBBProject.Core.Navigation
{
    public class NavigationManager : INavigationManager
    {
        private readonly NavigationProvider _navigationProvider;

        public NavigationManager(NavigationProvider navigationProvider)
        {
            _navigationProvider = navigationProvider;

            NavigationDefinitions = new Dictionary<string, NavigationDefinition>
                {
                    {"ApplicationNavigation", new NavigationDefinition("ApplicationNavigation", "Application Navigation")}
                };
        }

        public IDictionary<string, NavigationDefinition> NavigationDefinitions { get; }
        public NavigationDefinition ApplicationNavigationContainer => NavigationDefinitions["ApplicationNavigation"];

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        public void Initialize()
        {
            var context = new NavigationProviderContext(this);

            _navigationProvider.CreateNavigation(context);
        }

    }
}

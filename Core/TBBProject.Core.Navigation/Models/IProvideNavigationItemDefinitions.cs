using System.Collections.Generic;

namespace TBBProject.Core.Navigation
{
    public interface IProvideNavigationItemDefinitions
    {
        List<NavigationItemDefinition> Items { get; set; }
    }
}

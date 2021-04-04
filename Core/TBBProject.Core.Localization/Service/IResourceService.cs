using System.Collections.Generic;
using Microsoft.Extensions.Localization;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.Localization
{
    public interface IResourceService
    {
        Resource GetResourceByKeyAndCulture(string key, string culture);
        IEnumerable<LocalizedString> GetAllStrings(string culture);
    }
}
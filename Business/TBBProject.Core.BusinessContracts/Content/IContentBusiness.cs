using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using TBBProject.Core.BusinessContracts.ViewModels;

namespace TBBProject.Core.BusinessContracts
{
    public interface IContentBusiness
    {
        DataSourceResult GetContentAll([DataSourceRequest] DataSourceRequest request, int? IsRelease, string ReleaseDate, string EndDate, UserVM user = null);
        DataSourceResult GetContentLangAll([DataSourceRequest] DataSourceRequest request, long contentId, UserVM user = null);

        ContentLangVM GetContentLang(long contentId);
        ContentVM GetContent(long contentId);



        DataSourceResult GetContent(long languageId, [DataSourceRequest] DataSourceRequest request);
        void CreateContent(ContentVM content,UserVM user);
        void UpdateContent(ContentVM content);
        void DeleteContent(long Id);
        void AppContent(long Id);


        void CreateContentLang(ContentLangVM content);
        void UpdateContentLang(ContentLangVM content);
        void DeleteContentLang(long Id);
    }
}

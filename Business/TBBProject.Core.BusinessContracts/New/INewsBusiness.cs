using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using TBBProject.Core.BusinessContracts.ViewModels;

namespace TBBProject.Core.BusinessContracts
{
    public interface INewsBusiness
    {
        DataSourceResult GetNewsAll([DataSourceRequest] DataSourceRequest request, int? IsRelease, string ReleaseDate, string EndDate,int?ApprovalStatus, UserVM user = null);
        DataSourceResult GetNewsApprovalAll([DataSourceRequest] DataSourceRequest request, int? IsRelease, string ReleaseDate, string EndDate, UserVM user = null);

        DataSourceResult GetNewsLangAll([DataSourceRequest] DataSourceRequest request, long newsId,UserVM user = null);

        NewsLangVM GetNewsLang(long Id);
        NewsVM GetNews(long Id);



        DataSourceResult GetNews(long languageId, [DataSourceRequest] DataSourceRequest request);
        void CreateNews(NewsVM news, UserVM user);
        void UpdateNews(NewsVM news);
        void DeleteNews(long Id);
        
        void CreateNewsLang(NewsLangVM news);
        void UpdateNewsLang(NewsLangVM news);
        void DeleteNewsLang(long Id);
        void AppNews(long Id);

    }
}

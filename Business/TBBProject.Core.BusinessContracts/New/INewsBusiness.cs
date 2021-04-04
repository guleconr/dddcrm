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
        DataSourceResult GetNewsAllAsync([DataSourceRequest] DataSourceRequest request, int? IsRelease, DateTime ReleaseDate);
        DataSourceResult GetNewsLangAllAsync([DataSourceRequest] DataSourceRequest request, long announcementId);

        NewsLangVM GetNewsLang(long announcementId);
        NewsVM GetNews(long announcementId);



        DataSourceResult GetNews(long languageId, [DataSourceRequest] DataSourceRequest request);
        void CreateNews(NewsVM announcement);
        void UpdateNews(NewsVM announcement);
        void DeleteNews(long Id);

        void CreateNewsLang(NewsLangVM announcement);
        void UpdateNewsLang(NewsLangVM announcement);
        void DeleteNewsLang(NewsLangVM announcement);
    }
}

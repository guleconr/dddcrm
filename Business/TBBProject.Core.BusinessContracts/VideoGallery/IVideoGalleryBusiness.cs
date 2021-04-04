using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using TBBProject.Core.BusinessContracts.ViewModels;

namespace TBBProject.Core.BusinessContracts
{
    public interface IVideoGalleryBusiness
    {
        DataSourceResult GetVideoGalleryAllAsync([DataSourceRequest] DataSourceRequest request, DateTime ReleaseDate);
        DataSourceResult GetVideoGalleryLangAllAsync([DataSourceRequest] DataSourceRequest request, long videoGalleryId);

        VideoGalleryLangVM GetVideoGalleryLang(long videoGalleryId);
        VideoGalleryVM GetVideoGallery(long videoGalleryId);



        DataSourceResult GetVideoGallery(long languageId, [DataSourceRequest] DataSourceRequest request);
        void CreateVideoGallery(VideoGalleryVM videoGallery);
        void UpdateVideoGallery(VideoGalleryVM videoGallery);
        void DeleteVideoGallery(long Id);

        void CreateVideoGalleryLang(VideoGalleryLangVM videoGallery);
        void UpdateVideoGalleryLang(VideoGalleryLangVM videoGallery);
        void DeleteVideoGalleryLang(VideoGalleryLangVM videoGallery);
    }
}

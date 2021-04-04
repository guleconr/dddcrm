using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.DataContracts
{
    public interface IVideoGalleryDataLayer
    {
        IQueryable<VideoGallery> GetVideoGallery(long Id);

        void CreateVideoGallery(VideoGallery model);

        void UpdateVideoGallery(VideoGallery model);

        void DeleteVideoGallery(long Id);
        IQueryable<VideoGallery> GetVideoGalleryAllAsync();
        IQueryable<VideoGalleryLang> GetVideoGalleryLangAllAsync(long VideoGalleryId);

        VideoGalleryLang GetVideoGalleryLang(long VideoGalleryId);

        VideoGallery GetVideoGalleryWithId(long VideoGalleryId);

        void CreateVideoGalleryLang(VideoGalleryLang model);

        void UpdateVideoGalleryLang(VideoGalleryLang model);

        void DeleteVideoGalleryLang(VideoGalleryLang model);
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TBBProject.Core.BusinessContracts;
using TBBProject.Core.BusinessContracts.ViewModels;
using TBBProject.Core.Data.Domain;
using TBBProject.Core.DataContracts;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.Business
{

    public class VideoGalleryBusiness : IVideoGalleryBusiness
    {
        private readonly IVideoGalleryDataLayer _videoGalleryDataLayer;
        private readonly IMapper _mapper;
        public VideoGalleryBusiness(IVideoGalleryDataLayer videoGalleryDataLayer, IMapper mapper)
        {
            _videoGalleryDataLayer = videoGalleryDataLayer;
            _mapper = mapper;
        }

        public DataSourceResult GetVideoGallery(long Id, [DataSourceRequest] DataSourceRequest request)
        {
            var VideoGallery = _videoGalleryDataLayer.GetVideoGallery(Id).ToDataSourceResult(request);
            VideoGallery.Data = _mapper.Map<List<VideoGalleryVM>>(VideoGallery.Data);
            return VideoGallery;
        }

        public void CreateVideoGallery(VideoGalleryVM videoGallery)
        {
            videoGallery.VideoGalleryLang = new List<VideoGalleryLangVM>();
            VideoGalleryLangVM lang = new VideoGalleryLangVM();
            lang.Title = videoGallery.Title;
            lang.Url = videoGallery.Url;
            lang.LanguageId = videoGallery.LanguageId;
            videoGallery.VideoGalleryLang.Add(lang);
            var evideoGallery = _mapper.Map<VideoGallery>(videoGallery);
            _videoGalleryDataLayer.CreateVideoGallery(evideoGallery);

        }

        public void UpdateVideoGallery(VideoGalleryVM videoGallery)
        {
            var evideoGallery = _mapper.Map<VideoGallery>(videoGallery);
            _videoGalleryDataLayer.UpdateVideoGallery(evideoGallery);
        }

        public void DeleteVideoGallery(long Id)
        {
            _videoGalleryDataLayer.DeleteVideoGallery(Id);
        }



        public void CreateVideoGalleryLang(VideoGalleryLangVM videoGallery)
        {
            var evideoGallery = _mapper.Map<VideoGalleryLang>(videoGallery);
            _videoGalleryDataLayer.CreateVideoGalleryLang(evideoGallery);

        }

        public void UpdateVideoGalleryLang(VideoGalleryLangVM videoGallery)
        {
            var evideoGallery = _mapper.Map<VideoGalleryLang>(videoGallery);
            _videoGalleryDataLayer.UpdateVideoGalleryLang(evideoGallery);
        }

        public void DeleteVideoGalleryLang(VideoGalleryLangVM videoGallery)
        {
            var evideoGallery = _mapper.Map<VideoGalleryLang>(videoGallery);
            _videoGalleryDataLayer.DeleteVideoGalleryLang(evideoGallery);
        }

        public DataSourceResult GetVideoGalleryLangAllAsync([DataSourceRequest] DataSourceRequest request, long videoGalleryId)
        {
            var data = _videoGalleryDataLayer.GetVideoGalleryLangAllAsync(videoGalleryId).ToDataSourceResult(request);
            data.Data = _mapper.Map<List<VideoGalleryLangVM>>(data.Data);
            return data;
        }

        public VideoGalleryLangVM GetVideoGalleryLang(long videoGalleryId)
        {
            var data = _videoGalleryDataLayer.GetVideoGalleryLang(videoGalleryId);
            return _mapper.Map<VideoGalleryLangVM>(data);
        }

        public VideoGalleryVM GetVideoGallery(long videoGalleryId)
        {
            var data = _videoGalleryDataLayer.GetVideoGalleryWithId(videoGalleryId);
            return _mapper.Map<VideoGalleryVM>(data);
        }

        public DataSourceResult GetVideoGalleryAllAsync([DataSourceRequest] DataSourceRequest request, DateTime ReleaseDate)
        {
            var data = _videoGalleryDataLayer.GetVideoGalleryAllAsync();
            //if (ReleaseDate.Year > 1)
            //{
            //    data = data.Where(i => i.ReleaseDate == ReleaseDate);
            //}
            var res = data.ToDataSourceResult(request);
            res.Data = _mapper.Map<List<VideoGalleryVM>>(res.Data);
            return res;
        }
    }
}

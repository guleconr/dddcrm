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
using Kendo.Mvc;

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

        public void DeleteVideoGalleryLang(long Id)
        {
            _videoGalleryDataLayer.DeleteVideoGalleryLang(Id);
        }

        public DataSourceResult GetVideoGalleryLangAll([DataSourceRequest] DataSourceRequest request, long videoGalleryId)
        {
            var data = _videoGalleryDataLayer.GetVideoGalleryLangAll(videoGalleryId).ToDataSourceResult(request);
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

        public DataSourceResult GetVideoGalleryAll([DataSourceRequest] DataSourceRequest request, string ReleaseDate, string EndDate)
        {
            var data = _videoGalleryDataLayer.GetVideoGalleryAll();
            if (ReleaseDate != null && EndDate != null)
            {
                data = data.Where(i => i.ReleaseDate >= DateTime.ParseExact(ReleaseDate, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat)
                && i.ReleaseDate <= DateTime.ParseExact(EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat));
            }
            if (request.Filters.Count > 0)
            {
                var filter = ((Kendo.Mvc.FilterDescriptor)( (Kendo.Mvc.CompositeFilterDescriptor)request.Filters[0]).FilterDescriptors[0]).Value;
                data  = data.Where(i => i.VideoGalleryLang.Any(i => i.Title.Contains(filter.ToString())));
                request.Filters.Clear();
            }
            var res = data.ToDataSourceResult(request);
            res.Data = _mapper.Map<List<VideoGalleryVM>>(res.Data);
            return res;
        }
    }
}

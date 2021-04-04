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

    public class NewsBusiness : INewsBusiness
    {
        private readonly INewsDataLayer _newsDataLayer;
        private readonly IMapper _mapper;
        public NewsBusiness(INewsDataLayer newsDataLayer, IMapper mapper)
        {
            _newsDataLayer = newsDataLayer;
            _mapper = mapper;
        }

        public DataSourceResult GetNews(long Id, [DataSourceRequest] DataSourceRequest request)
        {
            var news = _newsDataLayer.GetNews(Id).ToDataSourceResult(request);
            news.Data = _mapper.Map<List<NewsVM>>(news.Data);
            return news;
        }

        public void CreateNews(NewsVM news)
        {
            news.NewsLang = new List<NewsLangVM>();
            NewsLangVM lang = new NewsLangVM();
            lang.Title = news.Title;
            lang.NewsImage = news.NewsImage;
            lang.Content = news.Content;
            lang.LanguageId = news.LanguageId;
            news.NewsLang.Add(lang);
            news.ApprovalStatus = ApprovalStatus.Waiting;
            var enews = _mapper.Map<News>(news);
            _newsDataLayer.CreateNews(enews);

        }

        public void UpdateNews(NewsVM news)
        {
            var enews = _mapper.Map<News>(news);
            _newsDataLayer.UpdateNews(enews);
        }

        public void DeleteNews(long Id)
        {
            _newsDataLayer.DeleteNews(Id);
        }



        public void CreateNewsLang(NewsLangVM news)
        {
            var enews = _mapper.Map<NewsLang>(news);
            _newsDataLayer.CreateNewsLang(enews);

        }

        public void UpdateNewsLang(NewsLangVM news)
        {
            var enews = _mapper.Map<NewsLang>(news);
            _newsDataLayer.UpdateNewsLang(enews);
        }

        public void DeleteNewsLang(NewsLangVM news)
        {
            var enews = _mapper.Map<NewsLang>(news);
            _newsDataLayer.DeleteNewsLang(enews);
        }

        public DataSourceResult GetNewsLangAllAsync([DataSourceRequest] DataSourceRequest request, long newsId)
        {
            var data = _newsDataLayer.GetNewsLangAllAsync(newsId).ToDataSourceResult(request);
            data.Data = _mapper.Map<List<NewsLangVM>>(data.Data);
            return data;
        }

        public NewsLangVM GetNewsLang(long newsId)
        {
            var data = _newsDataLayer.GetNewsLang(newsId);
            return _mapper.Map<NewsLangVM>(data);
        }

        public NewsVM GetNews(long newsId)
        {
            var data = _newsDataLayer.GetNewsWithId(newsId);
            return _mapper.Map<NewsVM>(data);
        }

        public DataSourceResult GetNewsAllAsync([DataSourceRequest] DataSourceRequest request, int? IsRelease, DateTime ReleaseDate)
        {
            var data = _newsDataLayer.GetNewsAllAsync();
            if (IsRelease != null)
            {
                if (IsRelease == 0)
                    data = data.Where(i => i.IsRelease == false);
                else
                    data = data.Where(i => i.IsRelease == true);
            }
            //if (ReleaseDate.Year > 1)
            //{
            //    data = data.Where(i => i.ReleaseDate == ReleaseDate);
            //}
            var res = data.ToDataSourceResult(request);
            res.Data = _mapper.Map<List<NewsVM>>(res.Data);
            return res;
        }
    }
}

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
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace TBBProject.Core.Business
{

    public class NewsBusiness : INewsBusiness
    {
        private readonly INewsDataLayer _newsDataLayer;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public NewsBusiness(INewsDataLayer newsDataLayer, IMapper mapper, IConfiguration config)
        {
            _newsDataLayer = newsDataLayer;
            _mapper = mapper;
            _config = config;
        }

        public DataSourceResult GetNews(long Id, [DataSourceRequest] DataSourceRequest request)
        {
            var news = _newsDataLayer.GetNews(Id).ToDataSourceResult(request);
            news.Data = _mapper.Map<List<NewsVM>>(news.Data);
            return news;
        }

        public void CreateNews(NewsVM news,UserVM user)
        {

            var model = news;
            if (news.NewsImageForm != null)
            {
                var ms = new MemoryStream();
                news.NewsImageForm.CopyTo(ms);
                news.NewsImage = ms.ToArray();
                news.NewsImageName = news.NewsImageForm.FileName;
            }
            if (news.SliderImageForm != null)
            {
                var ms = new MemoryStream();
                news.SliderImageForm.CopyTo(ms);
                news.SliderImage = ms.ToArray();
                news.SliderImageName = news.SliderImageForm.FileName;
            }
            if (news.NewsGalleryForm != null && news.NewsGalleryForm.Count > 0)
            {
                news.NewsGallery = new List<NewsGalleryVM>();
                foreach (var item in news.NewsGalleryForm)
                {
                    NewsGalleryVM arr = new NewsGalleryVM();
                    var ms = new MemoryStream();
                    item.CopyTo(ms);
                    arr.Image = ms.ToArray();
                    arr.Name = item.FileName;
                    news.NewsGallery.Add(arr);
                }
            }
            model.NewsLang = new List<NewsLangVM>();
            NewsLangVM lang = new NewsLangVM();
            lang.Title = model.Title;
            lang.HeadLine = model.HeadLine;
            lang.Content = model.Content;
            lang.LanguageId = model.LanguageId;
            lang.NewsImage = model.NewsImage;
            lang.NewsImageName = model.NewsImageName;
            lang.SliderImage = model.SliderImage;
            lang.SliderImageName = model.SliderImageName;
            lang.NewsGallery = model.NewsGallery;
            model.NewsLang.Add(lang);
            model.ApprovalStatus = ApprovalStatus.Waiting;
            lang.UserId = user.Id;
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

        public void AppNews(long Id)
        {
            _newsDataLayer.AppNews(Id);
        }

        public void CreateNewsLang(NewsLangVM news)
        {
            if (news.NewsImageForm != null)
            {
                var ms = new MemoryStream();
                news.NewsImageForm.CopyTo(ms);
                news.NewsImage = ms.ToArray();
                news.NewsImageName = news.NewsImageForm.FileName;
            }
            if (news.SliderImageForm != null)
            {
                var ms = new MemoryStream();
                news.SliderImageForm.CopyTo(ms);
                news.SliderImage = ms.ToArray();
                news.SliderImageName = news.SliderImageForm.FileName;
            }
            if (news.NewsGalleryForm!=null&&news.NewsGalleryForm.Count>0)
            {
                news.NewsGallery = new List<NewsGalleryVM>();
                foreach (var item in news.NewsGalleryForm)
                {
                    NewsGalleryVM arr = new NewsGalleryVM();
                    var ms = new MemoryStream();
                    item.CopyTo(ms);
                    arr.Image = ms.ToArray();
                    arr.Name = item.FileName;
                    news.NewsGallery.Add(arr);
                }
            }
            var enews = _mapper.Map<NewsLang>(news);
            _newsDataLayer.CreateNewsLang(enews);

        }

        public void UpdateNewsLang(NewsLangVM news)
        {
            if (news.NewsImageForm != null)
            {
                var ms = new MemoryStream();
                news.NewsImageForm.CopyTo(ms);
                news.NewsImage = ms.ToArray();
                news.NewsImageName = news.NewsImageForm.FileName;
            }
            if (news.SliderImageForm != null)
            {
                var ms = new MemoryStream();
                news.SliderImageForm.CopyTo(ms);
                news.SliderImage = ms.ToArray();
                news.SliderImageName = news.SliderImageForm.FileName;
            }
            if (news.NewsGalleryForm!=null && news.NewsGalleryForm.Count > 0)
            {
                news.NewsGallery = new List<NewsGalleryVM>();
                foreach (var item in news.NewsGalleryForm)
                {
                    NewsGalleryVM arr = new NewsGalleryVM();
                    var ms = new MemoryStream();
                    item.CopyTo(ms);
                    arr.Image = ms.ToArray();
                    arr.Name = item.FileName;
                    news.NewsGallery.Add(arr);
                }
            }
            var enews = _mapper.Map<NewsLang>(news);
            _newsDataLayer.UpdateNewsLang(enews);
        }

        public void DeleteNewsLang(long Id)
        {
            _newsDataLayer.DeleteNewsLang(Id);
        }

        public DataSourceResult GetNewsLangAll([DataSourceRequest] DataSourceRequest request, long newsId, UserVM user = null)
        {
            var data = _newsDataLayer.GetNewsLangAll(newsId);
            if (user.UserRoleId != _config.GetValue<long>("ApplicationSettings:PressRelease"))
                data = data.Where(i => i.User.UserRole.Any(y => y.User.UserRole.Any(x => x.RoleId == user.UserRoleId)));

            var res = data.ToDataSourceResult(request);
            res.Data = _mapper.Map<List<NewsLangVM>>(res.Data);
            return res;
        }

        public NewsLangVM GetNewsLang(long newsId)
        {
            var data = _newsDataLayer.GetNewsLang(newsId);
            var result= _mapper.Map<NewsLangVM>(data);
            if (result.NewsImage != null)
            {
                var stream = new MemoryStream(result.NewsImage);
                IFormFile file = new FormFile(stream, 0, result.NewsImage.Length, result.NewsImageName, result.NewsImageName);
                result.NewsImageForm = file;
            }
            if (result.SliderImage != null)
            {
                var stream = new MemoryStream(result.SliderImage);
                IFormFile file = new FormFile(stream, 0, result.SliderImage.Length, result.SliderImageName, result.SliderImageName);
                result.SliderImageForm = file;
            }
            if (result.NewsGallery.Count!= null &&result.NewsGallery.Count >0)
            {
                result.NewsGalleryForm = new List<IFormFile>();
                foreach (var item in result.NewsGallery)
                {
                    var gallerystream = new MemoryStream(item.Image);
                    IFormFile galleryfile = new FormFile(gallerystream, 0, item.Image.Length, item.Name, item.Name);
                    result.NewsGalleryForm.Add(galleryfile);
                }
            }
            return result;
        }

        public NewsVM GetNews(long newsId)
        {
            var data = _newsDataLayer.GetNewsWithId(newsId);
            return _mapper.Map<NewsVM>(data);

        }

        public DataSourceResult GetNewsAll([DataSourceRequest] DataSourceRequest request, int? IsRelease, string ReleaseDate,string EndDate,int?ApprovalStatus, UserVM user = null)
        {
            var data = _newsDataLayer.GetNewsAll();
            if (user.UserRoleId != _config.GetValue<long>("ApplicationSettings:PressRelease"))
                data = data.Where(i => i.NewsLang.Any(y => y.User.UserRole.Any(x => x.RoleId == user.UserRoleId)));

            if (IsRelease != null)
            {
                if (IsRelease == 0)
                    data = data.Where(i => i.IsRelease == false);
                else
                    data = data.Where(i => i.IsRelease == true);
            }
            if (ApprovalStatus != null)
            {
                    data = data.Where(i => i.ApprovalStatus == (ApprovalStatus)ApprovalStatus);

            }
            if (ReleaseDate != null)
            {
                data = data.Where(i => i.ReleaseDate >= DateTime.ParseExact(ReleaseDate, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat));
            }
            if (EndDate != null)
            {
                data = data.Where(i => i.ReleaseDate <= DateTime.ParseExact(EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat));
            }
            if (request.Filters.Count > 0)
            {
                var filter = ( (Kendo.Mvc.FilterDescriptor)( (Kendo.Mvc.CompositeFilterDescriptor)request.Filters[0] ).FilterDescriptors[0] ).Value;
                data = data.Where(i => i.NewsLang.Any(i => i.Title.Contains(filter.ToString())));
                request.Filters.Clear();
            }
            var res = data.ToDataSourceResult(request);
            res.Data = _mapper.Map<List<NewsVM>>(res.Data);
            return res;
        }


        public DataSourceResult GetNewsApprovalAll([DataSourceRequest] DataSourceRequest request, int? IsRelease, string ReleaseDate, string EndDate, UserVM user = null)
        {
            var data = _newsDataLayer.GetNewsAll();

            data = data.Where(i => i.ApprovalStatus == ApprovalStatus.Waiting);

            if (user.UserRoleId != _config.GetValue<long>("ApplicationSettings:PressRelease"))
                data = data.Where(i => i.NewsLang.Any(y => y.User.UserRole.Any(x => x.RoleId == user.UserRoleId)));

            if (IsRelease != null)
            {
                if (IsRelease == 0)
                    data = data.Where(i => i.IsRelease == false);
                else
                    data = data.Where(i => i.IsRelease == true);
            }
            if (ReleaseDate != null)
            {
                data = data.Where(i => i.ReleaseDate >= DateTime.ParseExact(ReleaseDate, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat));
            }
            if (EndDate != null)
            {
                data = data.Where(i => i.ReleaseDate <= DateTime.ParseExact(EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat));
            }
            if (request.Filters.Count > 0)
            {
                var filter = ( (Kendo.Mvc.FilterDescriptor)( (Kendo.Mvc.CompositeFilterDescriptor)request.Filters[0] ).FilterDescriptors[0] ).Value;
                data = data.Where(i => i.NewsLang.Any(i => i.Title.Contains(filter.ToString())));
                request.Filters.Clear();
            }
            var res = data.ToDataSourceResult(request);
            res.Data = _mapper.Map<List<NewsVM>>(res.Data);
            return res;
        }
    }
}

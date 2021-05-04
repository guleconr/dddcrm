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

    public class ContentBusiness : IContentBusiness
    {
        private readonly IContentDataLayer _contentDataLayer;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public ContentBusiness(IContentDataLayer contentDataLayer, IMapper mapper, IConfiguration config)
        {
            _contentDataLayer = contentDataLayer;
            _mapper = mapper;
            _config = config;
        }

        public DataSourceResult GetContent(long Id, [DataSourceRequest] DataSourceRequest request)
        {
            var content = _contentDataLayer.GetContent(Id).ToDataSourceResult(request);
            content.Data = _mapper.Map<List<ContentVM>>(content.Data);
            return content;
        }

        public void CreateContent(ContentVM model,UserVM user)
        {
            if (model.ImageForm != null)
            {
                var ms = new MemoryStream();
                model.ImageForm.CopyTo(ms);
                model.Image = ms.ToArray();
                model.ImageName = model.ImageForm.FileName;
            }
            if (model.ContentFileForm != null && model.ContentFileForm.Count > 0)
            {
                model.ContentFile = new List<ContentFileVM>();
                foreach (var item in model.ContentFileForm)
                {
                    ContentFileVM arr = new ContentFileVM();
                    var ms = new MemoryStream();
                    item.CopyTo(ms);
                    arr.File = ms.ToArray();
                    arr.Name = item.FileName;
                    model.ContentFile.Add(arr);
                }
            }
            if (model.ContentGalleryForm != null && model.ContentGalleryForm.Count > 0)
            {
                model.ContentGallery = new List<ContentGalleryVM>();
                foreach (var item in model.ContentGalleryForm)
                {
                    ContentGalleryVM arr = new ContentGalleryVM();
                    var ms = new MemoryStream();
                    item.CopyTo(ms);
                    arr.Image = ms.ToArray();
                    arr.Name = item.FileName;
                    model.ContentGallery.Add(arr);
                }
            }
            var ann = model;
            ann.ContentLang = new List<ContentLangVM>();
            var lang = new ContentLangVM();
            lang.Title = ann.Title;
            lang.Image = ann.Image;
            lang.ImageName = ann.ImageName;
            lang.ContentGallery = ann.ContentGallery;
            lang.ContentFile = ann.ContentFile;
            lang.Content = ann.Content;
            lang.LanguageId = ann.LanguageId;
            lang.UserId = user.Id;
            ann.ApprovalStatus = ApprovalStatus.Waiting;
            ann.ContentLang.Add(lang);
            var econtent = _mapper.Map<Content>(ann);
            _contentDataLayer.CreateContent(econtent);

        }

        public void UpdateContent(ContentVM content)
        {
            var econtent = _mapper.Map<Content>(content);
            _contentDataLayer.UpdateContent(econtent);
        }

        public void DeleteContent(long Id)
        {
            _contentDataLayer.DeleteContent(Id);
        }

        public void AppContent(long Id)
        {
            _contentDataLayer.AppContent(Id);
        }

        public void CreateContentLang(ContentLangVM content)
        {
            if (content.ImageForm != null)
            {
                var ms = new MemoryStream();
                content.ImageForm.CopyTo(ms);
                content.Image = ms.ToArray();
                content.ImageName = content.ImageForm.FileName;
            }
            if (content.ContentFileForm != null && content.ContentFileForm.Count > 0)
            {
                content.ContentFile = new List<ContentFileVM>();
                foreach (var item in content.ContentFileForm)
                {
                    ContentFileVM arr = new ContentFileVM();
                    var ms = new MemoryStream();
                    item.CopyTo(ms);
                    arr.File = ms.ToArray();
                    arr.Name = item.FileName;
                    content.ContentFile.Add(arr);
                }
            }
            if (content.ContentGalleryForm != null && content.ContentGalleryForm.Count > 0)
            {
                content.ContentGallery = new List<ContentGalleryVM>();
                foreach (var item in content.ContentGalleryForm)
                {
                    ContentGalleryVM arr = new ContentGalleryVM();
                    var ms = new MemoryStream();
                    item.CopyTo(ms);
                    arr.Image = ms.ToArray();
                    arr.Name = item.FileName;
                    content.ContentGallery.Add(arr);
                }
            }
            var econtent = _mapper.Map<ContentLang>(content);
            _contentDataLayer.CreateContentLang(econtent);

        }

        public void UpdateContentLang(ContentLangVM content)
        {
            if (content.ImageForm != null)
            {
                var ms = new MemoryStream();
                content.ImageForm.CopyTo(ms);
                content.Image = ms.ToArray();
                content.ImageName = content.ImageForm.FileName;
            }
            if (content.ContentFileForm != null && content.ContentFileForm.Count > 0)
            {
                content.ContentFile = new List<ContentFileVM>();
                foreach (var item in content.ContentFileForm)
                {
                    ContentFileVM arr = new ContentFileVM();
                    var ms = new MemoryStream();
                    item.CopyTo(ms);
                    arr.File = ms.ToArray();
                    arr.Name = item.FileName;
                    content.ContentFile.Add(arr);
                }
            }
            if (content.ContentGalleryForm != null && content.ContentGalleryForm.Count > 0)
            {
                content.ContentGallery = new List<ContentGalleryVM>();
                foreach (var item in content.ContentGalleryForm)
                {
                    ContentGalleryVM arr = new ContentGalleryVM();
                    var ms = new MemoryStream();
                    item.CopyTo(ms);
                    arr.Image = ms.ToArray();
                    arr.Name = item.FileName;
                    content.ContentGallery.Add(arr);
                }
            }
            var econtent = _mapper.Map<ContentLang>(content);
            _contentDataLayer.UpdateContentLang(econtent);
        }

        public void DeleteContentLang(long Id)
        {
            _contentDataLayer.DeleteContentLang(Id);
        }

        public DataSourceResult GetContentLangAll([DataSourceRequest] DataSourceRequest request, long contentId, UserVM user = null)
        {
            var data = _contentDataLayer.GetContentLangAll(contentId);
            if (user.UserRoleId != _config.GetValue<long>("ApplicationSettings:PressRelease"))
                data = data.Where(i => i.User.UserRole.Any(y => y.User.UserRole.Any(x => x.RoleId == user.UserRoleId)));

            var res = data.ToDataSourceResult(request);
            res.Data = _mapper.Map<List<ContentLangVM>>(res.Data);
            return res;
        }

        public ContentLangVM GetContentLang(long contentId)
        {
            var data = _contentDataLayer.GetContentLang(contentId);
            var result = _mapper.Map<ContentLangVM>(data);
            if (result.Image != null)
            {
                var stream = new MemoryStream(result.Image);
                IFormFile file = new FormFile(stream, 0, result.Image.Length, result.ImageName, result.ImageName);
                result.ImageForm = file;
            }
            if (result.ContentFile.Count != null && result.ContentFile.Count > 0)
            {
                result.ContentFileForm = new List<IFormFile>();
                foreach (var item in result.ContentFile)
                {
                    var gallerystream = new MemoryStream(item.File);
                    IFormFile galleryfile = new FormFile(gallerystream, 0, item.File.Length, item.Name, item.Name);
                    result.ContentFileForm.Add(galleryfile);
                }
            }
            if (result.ContentGallery.Count != null && result.ContentGallery.Count > 0)
            {
                result.ContentGalleryForm = new List<IFormFile>();
                foreach (var item in result.ContentGallery)
                {
                    var gallerystream = new MemoryStream(item.Image);
                    IFormFile galleryfile = new FormFile(gallerystream, 0, item.Image.Length, item.Name, item.Name);
                    result.ContentGalleryForm.Add(galleryfile);
                }
            }
            return result;
        }

        public ContentVM GetContent(long contentId)
        {
            var data = _contentDataLayer.GetContentWithId(contentId);
            return _mapper.Map<ContentVM>(data);
        }

        public DataSourceResult GetContentAll([DataSourceRequest] DataSourceRequest request, int? IsRelease, string ReleaseDate, string EndDate, UserVM user=null)
        {
            var data = _contentDataLayer.GetContentAll();

            if (user.UserRoleId != _config.GetValue<long>("ApplicationSettings:PressRelease"))
                data = data.Where(i => i.ContentLang.Any(y => y.User.UserRole.Any(x => x.RoleId == user.UserRoleId)));

            if (IsRelease != null)
            {
                if (IsRelease == 0)
                    data = data.Where(i => i.IsRelease ==false);
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
                data = data.Where(i => i.ContentLang.Any(i => i.Title.Contains(filter.ToString())));
                request.Filters.Clear();
            }
            var res = data.ToDataSourceResult(request);
            res.Data = _mapper.Map<List<ContentVM>>(res.Data);
            return res;
        }
    }
}

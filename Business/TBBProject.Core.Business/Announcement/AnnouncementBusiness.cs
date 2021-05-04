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

    public class AnnouncementBusiness : IAnnouncementBusiness
    {
        private readonly IAnnouncementDataLayer _announcementDataLayer;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AnnouncementBusiness(IAnnouncementDataLayer announcementDataLayer, IMapper mapper, IConfiguration config)
        {
            _announcementDataLayer = announcementDataLayer;
            _mapper = mapper;
            _config = config;

        }

        public DataSourceResult GetAnnouncement(long Id, [DataSourceRequest] DataSourceRequest request)
        {
            var announcement = _announcementDataLayer.GetAnnouncement(Id).ToDataSourceResult(request);
            announcement.Data = _mapper.Map<List<AnnouncementVM>>(announcement.Data);
            return announcement;
        }

        public void CreateAnnouncement(AnnouncementVM announcement,UserVM user)
        {
            if (announcement.ImageForm != null)
            {
                var ms = new MemoryStream();
                announcement.ImageForm.CopyTo(ms);
                announcement.Image = ms.ToArray();
                announcement.ImageName = announcement.ImageForm.FileName;
            }
            if (announcement.AnnouncementFileForm != null && announcement.AnnouncementFileForm.Count > 0)
            {
                announcement.AnnouncementFile = new List<AnnouncementFileVM>();
                foreach (var item in announcement.AnnouncementFileForm)
                {
                    AnnouncementFileVM arr = new AnnouncementFileVM();
                    var ms = new MemoryStream();
                    item.CopyTo(ms);
                    arr.File = ms.ToArray();
                    arr.Name = item.FileName;
                    announcement.AnnouncementFile.Add(arr);
                }
            }
            var ann = announcement;

            ann.AnnouncementLang = new List<AnnouncementLangVM>();
            var lang = new AnnouncementLangVM();

            lang.Title = ann.Title;
            lang.Image = ann.Image;
            lang.ImageName = ann.ImageName;
            lang.AnnouncementFile = ann.AnnouncementFile;
            lang.Content = ann.Content;
            lang.LanguageId = ann.LanguageId;
            lang.UserId = user.Id;
            ann.AnnouncementLang.Add(lang);
            ann.ApprovalStatus = ApprovalStatus.Waiting;
            if(ann.EndReleaseDate!=null)
                ann.EndReleaseDate = DateTime.ParseExact(ann.EndReleaseDateStr, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat);
            var eannouncement = _mapper.Map<Announcement>(announcement);
            _announcementDataLayer.CreateAnnouncement(eannouncement);

        }

        public void UpdateAnnouncement(AnnouncementVM announcement)
        {
            if (announcement.EndReleaseDate != null)
                announcement.EndReleaseDate = DateTime.ParseExact(announcement.EndReleaseDateStr, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat);
            var eannouncement = _mapper.Map<Announcement>(announcement);
            _announcementDataLayer.UpdateAnnouncement(eannouncement);
        }

        public void DeleteAnnouncement(long Id)
        {
            _announcementDataLayer.DeleteAnnouncement(Id);
        }

        public void AppAnnouncement(long Id)
        {
            _announcementDataLayer.AppAnnouncement(Id);
        }

        public void CreateAnnouncementLang(AnnouncementLangVM announcement)
        {
            if (announcement.ImageForm != null)
            {
                var ms = new MemoryStream();
                announcement.ImageForm.CopyTo(ms);
                announcement.Image = ms.ToArray();
                announcement.ImageName = announcement.ImageForm.FileName;
            }
            if (announcement.AnnouncementFileForm != null && announcement.AnnouncementFileForm.Count > 0)
            {
                announcement.AnnouncementFile = new List<AnnouncementFileVM>();
                foreach (var item in announcement.AnnouncementFileForm)
                {
                    AnnouncementFileVM arr = new AnnouncementFileVM();
                    var ms = new MemoryStream();
                    item.CopyTo(ms);
                    arr.File = ms.ToArray();
                    arr.Name = item.FileName;
                    announcement.AnnouncementFile.Add(arr);
                }
            }
            var eannouncement = _mapper.Map<AnnouncementLang>(announcement);
            _announcementDataLayer.CreateAnnouncementLang(eannouncement);

        }

        public void UpdateAnnouncementLang(AnnouncementLangVM announcement)
        {
            if (announcement.ImageForm != null)
            {
                var ms = new MemoryStream();
                announcement.ImageForm.CopyTo(ms);
                announcement.Image = ms.ToArray();
                announcement.ImageName = announcement.ImageForm.FileName;
            }
            if (announcement.AnnouncementFileForm != null && announcement.AnnouncementFileForm.Count > 0)
            {
                announcement.AnnouncementFile = new List<AnnouncementFileVM>();
                foreach (var item in announcement.AnnouncementFileForm)
                {
                    AnnouncementFileVM arr = new AnnouncementFileVM();
                    var ms = new MemoryStream();
                    item.CopyTo(ms);
                    arr.File = ms.ToArray();
                    arr.Name = item.FileName;
                    announcement.AnnouncementFile.Add(arr);
                }
            }
            var eannouncement = _mapper.Map<AnnouncementLang>(announcement);
            _announcementDataLayer.UpdateAnnouncementLang(eannouncement);
        }

        public void DeleteAnnouncementLang(long Id)
        {
            _announcementDataLayer.DeleteAnnouncementLang(Id);
        }

        public DataSourceResult GetAnnouncementLangAll([DataSourceRequest] DataSourceRequest request, long announcementId,UserVM user)
        {
            var data = _announcementDataLayer.GetAnnouncementLangAll(announcementId);
            if (user.UserRoleId != _config.GetValue<long>("ApplicationSettings:PressRelease"))
                data = data.Where(i => i.User.UserRole.Any(y => y.User.UserRole.Any(x => x.RoleId == user.UserRoleId)));

            var res = data.ToDataSourceResult(request);
            res.Data = _mapper.Map<List<AnnouncementLangVM>>(res.Data);
            return res;
        }

        public AnnouncementLangVM GetAnnouncementLang(long announcementId)
        {
            var data = _announcementDataLayer.GetAnnouncementLang(announcementId);
            var result = _mapper.Map<AnnouncementLangVM>(data);
            if (result.Image != null)
            {
                var stream = new MemoryStream(result.Image);
                IFormFile file = new FormFile(stream, 0, result.Image.Length, result.ImageName, result.ImageName);
                result.ImageForm = file;
            }
            if (result.AnnouncementFile.Count!=null && result.AnnouncementFile.Count > 0)
            {
                result.AnnouncementFileForm = new List<IFormFile>();
                foreach (var item in result.AnnouncementFile)
                {
                    var gallerystream = new MemoryStream(item.File);
                    IFormFile galleryfile = new FormFile(gallerystream, 0, item.File.Length, item.Name, item.Name);
                    result.AnnouncementFileForm.Add(galleryfile);
                }
            }
            return result;
        }

        public AnnouncementVM GetAnnouncement(long announcementId)
        {
            var data = _announcementDataLayer.GetAnnouncementWithId(announcementId);
            return _mapper.Map<AnnouncementVM>(data);
        }

        public DataSourceResult GetAnnouncementAll([DataSourceRequest] DataSourceRequest request, int? IsRelease, string ReleaseDate, string EndDate, int? ApprovalStatus,UserVM user =null)
        {
            var data = _announcementDataLayer.GetAnnouncementAll();
            if (user.UserRoleId != _config.GetValue<long>("ApplicationSettings:PressRelease"))
                data = data.Where(i => i.AnnouncementLang.Any(y => y.User.UserRole.Any(x => x.RoleId == user.UserRoleId)));

            if (IsRelease != null)
            {
                if (IsRelease == 0)
                    data = data.Where(i => i.IsRelease ==false);
                else
                    data = data.Where(i => i.IsRelease == true);
            }
            if (ApprovalStatus != null)
            {
                    data = data.Where(i => i.ApprovalStatus ==  (ApprovalStatus)ApprovalStatus);
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
                data = data.Where(i => i.AnnouncementLang.Any(i => i.Title.Contains(filter.ToString())));
                request.Filters.Clear();
            }
            var res = data.ToDataSourceResult(request);
            res.Data = _mapper.Map<List<AnnouncementVM>>(res.Data);
            return res;
        }


        public DataSourceResult GetAnnouncementApprovalAll([DataSourceRequest] DataSourceRequest request, int? IsRelease, string ReleaseDate, string EndDate, UserVM user = null)
        {
            var data = _announcementDataLayer.GetAnnouncementAll();

            data = data.Where(i => i.ApprovalStatus == ApprovalStatus.Waiting);

            if (user.UserRoleId != _config.GetValue<long>("ApplicationSettings:PressRelease"))
                data = data.Where(i => i.AnnouncementLang.Any(y => y.User.UserRole.Any(x => x.RoleId == user.UserRoleId)));

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
                data = data.Where(i => i.AnnouncementLang.Any(i => i.Title.Contains(filter.ToString())));
                request.Filters.Clear();
            }
            var res = data.ToDataSourceResult(request);
            res.Data = _mapper.Map<List<AnnouncementVM>>(res.Data);
            return res;
        }

    }
}

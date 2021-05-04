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
using System.IO;
using Microsoft.AspNetCore.Http;

namespace TBBProject.Core.Business
{

    public class LegislationAnnouncementBusiness : ILegislationAnnouncementBusiness
    {
        private readonly ILegislationAnnouncementDataLayer _legislationAnnouncementDataLayer;
        private readonly IMapper _mapper;
        public LegislationAnnouncementBusiness(ILegislationAnnouncementDataLayer legislationAnnouncementDataLayer, IMapper mapper)
        {
            _legislationAnnouncementDataLayer = legislationAnnouncementDataLayer;
            _mapper = mapper;
        }

        public DataSourceResult GetLegislationAnnouncement(long Id, [DataSourceRequest] DataSourceRequest request)
        {
            var legislationAnnouncement = _legislationAnnouncementDataLayer.GetLegislationAnnouncement(Id).ToDataSourceResult(request);
            legislationAnnouncement.Data = _mapper.Map<List<LegislationAnnouncementVM>>(legislationAnnouncement.Data);
            return legislationAnnouncement;
        }

        public void CreateLegislationAnnouncement(LegislationAnnouncementVM legislationAnnouncement,UserVM user)
        {
            if (legislationAnnouncement.LegislationAnnouncementFileForm != null && legislationAnnouncement.LegislationAnnouncementFileForm.Count > 0)
            {
                legislationAnnouncement.LegislationAnnouncementFile = new List<LegislationAnnouncementFileVM>();
                foreach (var item in legislationAnnouncement.LegislationAnnouncementFileForm)
                {
                    LegislationAnnouncementFileVM arr = new LegislationAnnouncementFileVM();
                    var ms = new MemoryStream();
                    item.CopyTo(ms);
                    arr.File = ms.ToArray();
                    arr.Name = item.FileName;
                    legislationAnnouncement.LegislationAnnouncementFile.Add(arr);
                }
            }
            var ann = legislationAnnouncement;
            ann.LegislationAnnouncementLang = new List<LegislationAnnouncementLangVM>();
            var lang = new LegislationAnnouncementLangVM();
            lang.Title = ann.Title;
            lang.Link = ann.Link;
            lang.Content = ann.Content;
            lang.LanguageId = ann.LanguageId;
            lang.LegislationAnnouncementFile = ann.LegislationAnnouncementFile;
            lang.UserId = user.Id;
            ann.LegislationAnnouncementLang.Add(lang);
            ann.ApprovalStatus = ApprovalStatus.Waiting;

            var elegislationAnnouncement = _mapper.Map<LegislationAnnouncement>(legislationAnnouncement);
            _legislationAnnouncementDataLayer.CreateLegislationAnnouncement(elegislationAnnouncement);

        }

        public void UpdateLegislationAnnouncement(LegislationAnnouncementVM legislationAnnouncement)
        {
            var elegislationAnnouncement = _mapper.Map<LegislationAnnouncement>(legislationAnnouncement);
            _legislationAnnouncementDataLayer.UpdateLegislationAnnouncement(elegislationAnnouncement);
        }

        public void DeleteLegislationAnnouncement(long Id)
        {
            _legislationAnnouncementDataLayer.DeleteLegislationAnnouncement(Id);
        }

        public void AppLegislationAnnouncement(long Id)
        {
            _legislationAnnouncementDataLayer.AppLegislationAnnouncement(Id);
        }

        public void CreateLegislationAnnouncementLang(LegislationAnnouncementLangVM legislationAnnouncement)
        {
            if (legislationAnnouncement.LegislationAnnouncementFileForm != null && legislationAnnouncement.LegislationAnnouncementFileForm.Count > 0)
            {
                legislationAnnouncement.LegislationAnnouncementFile = new List<LegislationAnnouncementFileVM>();
                foreach (var item in legislationAnnouncement.LegislationAnnouncementFileForm)
                {
                    LegislationAnnouncementFileVM arr = new LegislationAnnouncementFileVM();
                    var ms = new MemoryStream();
                    item.CopyTo(ms);
                    arr.File = ms.ToArray();
                    arr.Name = item.FileName;
                    legislationAnnouncement.LegislationAnnouncementFile.Add(arr);
                }
            }
            var elegislationAnnouncement = _mapper.Map<LegislationAnnouncementLang>(legislationAnnouncement);
            _legislationAnnouncementDataLayer.CreateLegislationAnnouncementLang(elegislationAnnouncement);

        }

        public void UpdateLegislationAnnouncementLang(LegislationAnnouncementLangVM legislationAnnouncement)
        {
            if (legislationAnnouncement.LegislationAnnouncementFileForm != null && legislationAnnouncement.LegislationAnnouncementFileForm.Count > 0)
            {
                legislationAnnouncement.LegislationAnnouncementFile = new List<LegislationAnnouncementFileVM>();
                foreach (var item in legislationAnnouncement.LegislationAnnouncementFileForm)
                {
                    LegislationAnnouncementFileVM arr = new LegislationAnnouncementFileVM();
                    var ms = new MemoryStream();
                    item.CopyTo(ms);
                    arr.File = ms.ToArray();
                    arr.Name = item.FileName;
                    legislationAnnouncement.LegislationAnnouncementFile.Add(arr);
                }
            }
            var elegislationAnnouncement = _mapper.Map<LegislationAnnouncementLang>(legislationAnnouncement);
            _legislationAnnouncementDataLayer.UpdateLegislationAnnouncementLang(elegislationAnnouncement);
        }

        public void DeleteLegislationAnnouncementLang(long Id)
        {
            _legislationAnnouncementDataLayer.DeleteLegislationAnnouncementLang(Id);
        }

        public DataSourceResult GetLegislationAnnouncementLangAll([DataSourceRequest] DataSourceRequest request, long legislationAnnouncementId)
        {
            var data = _legislationAnnouncementDataLayer.GetLegislationAnnouncementLangAll(legislationAnnouncementId).ToDataSourceResult(request);
            data.Data = _mapper.Map<List<LegislationAnnouncementLangVM>>(data.Data);
            return data;
        }

        public LegislationAnnouncementLangVM GetLegislationAnnouncementLang(long legislationAnnouncementId)
        {
            var data = _legislationAnnouncementDataLayer.GetLegislationAnnouncementLang(legislationAnnouncementId);
            var result = _mapper.Map<LegislationAnnouncementLangVM>(data);
            if (result.LegislationAnnouncementFile.Count != null && result.LegislationAnnouncementFile.Count > 0)
            {
                result.LegislationAnnouncementFileForm = new List<IFormFile>();
                foreach (var item in result.LegislationAnnouncementFile)
                {
                    var gallerystream = new MemoryStream(item.File);
                    IFormFile galleryfile = new FormFile(gallerystream, 0, item.File.Length, item.Name, item.Name);
                    result.LegislationAnnouncementFileForm.Add(galleryfile);
                }
            }
            return result;
        }

        public LegislationAnnouncementVM GetLegislationAnnouncement(long legislationAnnouncementId)
        {
            var data = _legislationAnnouncementDataLayer.GetLegislationAnnouncementWithId(legislationAnnouncementId);
            return _mapper.Map<LegislationAnnouncementVM>(data);
        }

        public DataSourceResult GetLegislationAnnouncementAll([DataSourceRequest] DataSourceRequest request, int? IsRelease, string ReleaseDate, string EndDate)
        {
            var data = _legislationAnnouncementDataLayer.GetLegislationAnnouncementAll();
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
                data = data.Where(i => i.LegislationAnnouncementLang.Any(i => i.Title.Contains(filter.ToString())));
                request.Filters.Clear();
            }
            var res = data.ToDataSourceResult(request);
            res.Data = _mapper.Map<List<LegislationAnnouncementVM>>(res.Data);
            return res;
        }
    }
}

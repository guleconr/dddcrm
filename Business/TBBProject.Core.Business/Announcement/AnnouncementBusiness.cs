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

    public class AnnouncementBusiness : IAnnouncementBusiness
    {
        private readonly IAnnouncementDataLayer _announcementDataLayer;
        private readonly IMapper _mapper;
        public AnnouncementBusiness(IAnnouncementDataLayer announcementDataLayer, IMapper mapper)
        {
            _announcementDataLayer = announcementDataLayer;
            _mapper = mapper;
        }

        public DataSourceResult GetAnnouncement(long Id, [DataSourceRequest] DataSourceRequest request)
        {
            var announcement = _announcementDataLayer.GetAnnouncement(Id).ToDataSourceResult(request);
            announcement.Data = _mapper.Map<List<AnnouncementVM>>(announcement.Data);
            return announcement;
        }

        public void CreateAnnouncement(AnnouncementVM announcement)
        {
            announcement.AnnouncementLang = new List<AnnouncementLangVM>();
            AnnouncementLangVM lang = new AnnouncementLangVM();
            lang.Title = announcement.Title;
            lang.Image = announcement.Image;
            lang.Content = announcement.Content;
            lang.LanguageId = announcement.LanguageId;
            announcement.AnnouncementLang.Add(lang);
            announcement.ApprovalStatus = ApprovalStatus.Waiting;
            var eannouncement = _mapper.Map<Announcement>(announcement);
            _announcementDataLayer.CreateAnnouncement(eannouncement);

        }

        public void UpdateAnnouncement(AnnouncementVM announcement)
        {
            var eannouncement = _mapper.Map<Announcement>(announcement);
            _announcementDataLayer.UpdateAnnouncement(eannouncement);
        }

        public void DeleteAnnouncement(long Id)
        {
            _announcementDataLayer.DeleteAnnouncement(Id);
        }



        public void CreateAnnouncementLang(AnnouncementLangVM announcement)
        {
            var eannouncement = _mapper.Map<AnnouncementLang>(announcement);
            _announcementDataLayer.CreateAnnouncementLang(eannouncement);

        }

        public void UpdateAnnouncementLang(AnnouncementLangVM announcement)
        {
            var eannouncement = _mapper.Map<AnnouncementLang>(announcement);
            _announcementDataLayer.UpdateAnnouncementLang(eannouncement);
        }

        public void DeleteAnnouncementLang(AnnouncementLangVM announcement)
        {
            var eannouncement = _mapper.Map<AnnouncementLang>(announcement);
            _announcementDataLayer.DeleteAnnouncementLang(eannouncement);
        }

        public DataSourceResult GetAnnouncementLangAllAsync([DataSourceRequest] DataSourceRequest request, long announcementId)
        {
            var data = _announcementDataLayer.GetAnnouncementLangAllAsync(announcementId).ToDataSourceResult(request);
            data.Data = _mapper.Map<List<AnnouncementLangVM>>(data.Data);
            return data;
        }

        public AnnouncementLangVM GetAnnouncementLang(long announcementId)
        {
            var data = _announcementDataLayer.GetAnnouncementLang(announcementId);
            return _mapper.Map<AnnouncementLangVM>(data);
        }

        public AnnouncementVM GetAnnouncement(long announcementId)
        {
            var data = _announcementDataLayer.GetAnnouncementWithId(announcementId);
            return _mapper.Map<AnnouncementVM>(data);
        }

        public DataSourceResult GetAnnouncementAllAsync([DataSourceRequest] DataSourceRequest request, int? IsRelease, DateTime ReleaseDate, int AnnouncementType)
        {
            var data = _announcementDataLayer.GetAnnouncementAllAsync();
            if (IsRelease != null)
            {
                if (IsRelease == 0)
                    data = data.Where(i => i.IsRelease == false);
                else
                    data = data.Where(i => i.IsRelease == true);
            }
            //if (ReleaseDate.Year >1)
            //{
            //    data = data.Where(i => i.ReleaseDate == ReleaseDate);
            //}
            if (AnnouncementType > 0)
            {
                data = data.Where(i => i.AnnouncementTypeId == AnnouncementType);
            }
            var res = data.ToDataSourceResult(request);
            res.Data = _mapper.Map<List<AnnouncementVM>>(res.Data);
            return res;
        }
    }
}

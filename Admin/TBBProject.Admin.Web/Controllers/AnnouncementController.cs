using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBBProject.Admin.Web.Helpers;
using TBBProject.Core.BusinessContracts;
using TBBProject.Core.BusinessContracts.ViewModels;
using TBBProject.Core.Caching;
using TBBProject.Core.Common.Enums;
using TBBProject.Core.Common.Extensions;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using TBBProject.Admin.Web.Controllers;
using TBBProject.Core.Caching;
using TBBProject.Admin.Web.Helpers;
using TBBProject.Core.Common.Extensions;
using TBBProject.Core.Common.Enums;
using TBBProject.Core.BusinessContracts.ViewModels;
using TBBProject.Core.BusinessContracts;

namespace TBBProject.Admin.Web.Controllers
{
    [Authorize]
    public class AnnouncementController : BaseController
    {
        private readonly IAnnouncementBusiness _announcementBusiness;
        private readonly IStringLocalizer<BaseController> _localizer;
        private readonly IAccountBusiness _accountBusiness;
        public AnnouncementController(IAccountBusiness accountBusiness, IAnnouncementBusiness announcementBusiness, IStringLocalizer<BaseController> localizer)
                          : base(accountBusiness, localizer)
        {
            _announcementBusiness = announcementBusiness;
            _localizer = localizer;
            _accountBusiness = accountBusiness;
        }


        [PageCheckAttribute]
        [Route("Announcement")]
        public async Task<IActionResult> Announcement()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> AnnouncementSearchPost(AnnouncementVM model)
        {
            return PartialView("AnnouncementGridView", model);
        }

        public JsonResult Get_Announcement([DataSourceRequest] DataSourceRequest request, int? IsRelease, DateTime ReleaseDate, int AnnouncementType)
        {
            var result = _announcementBusiness.GetAnnouncementAllAsync(request, IsRelease, ReleaseDate, AnnouncementType);
            return Json(result);
        }

        public JsonResult Get_AnnouncementLang([DataSourceRequest] DataSourceRequest request, long announcementId)
        {
            var result = _announcementBusiness.GetAnnouncementLangAllAsync(request, announcementId);
            return Json(result);
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingAnnouncement_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<AnnouncementVM> announcement)
        {

            _announcementBusiness.CreateAnnouncement(announcement.FirstOrDefault());

            return Content("");

        }

        [AcceptVerbs("Post")]
        public ActionResult EditingAnnouncement_Update(AnnouncementVM announcement)
        {
            if (announcement != null && ModelState.IsValid)
            {
                _announcementBusiness.UpdateAnnouncement(announcement);
            }
            return Content("");

        }

        [AcceptVerbs("Post")]
        public ActionResult EditingAnnouncement_Destroy(int Id)
        {
            _announcementBusiness.DeleteAnnouncement(Id);
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult AnnouncementLang_Create(AnnouncementLangVM announcement)
        {
            _announcementBusiness.CreateAnnouncementLang(announcement);
            return Content("");

        }

        [AcceptVerbs("Post")]
        public ActionResult EditingAnnouncementLang_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<AnnouncementLangVM> announcement)
        {

            _announcementBusiness.CreateAnnouncementLang(announcement.FirstOrDefault());

            return Content("");

        }

        [AcceptVerbs("Post")]
        public ActionResult EditingAnnouncementLang_Update(AnnouncementLangVM model)
        {
            _announcementBusiness.UpdateAnnouncementLang(model);
            return Content("");

        }

        [AcceptVerbs("Post")]
        public ActionResult EditingAnnouncementLang_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<AnnouncementLangVM> announcement)
        {
            _announcementBusiness.DeleteAnnouncementLang(announcement.FirstOrDefault());
            return Content("");
        }

        [Route("AnnouncementLangUpdate/{eventId}")]
        public IActionResult AnnouncementLangUpdate(long eventId)
        {
            var result = _announcementBusiness.GetAnnouncementLang(eventId);
            return PartialView(result);
        }

        [Route("AnnouncementUpdate/{eventId}")]
        public async Task<IActionResult> AnnouncementUpdate(long eventId)
        {
            var result = _announcementBusiness.GetAnnouncement(eventId);
            return PartialView(result);
        }

        [Route("AnnouncementAdd/{eventId}")]
        public async Task<IActionResult> AnnouncementAdd(long eventId)
        {
            AnnouncementLangVM result = new AnnouncementLangVM();
            result.AnnouncementId = eventId;
            return PartialView(result);
        }

    }

}

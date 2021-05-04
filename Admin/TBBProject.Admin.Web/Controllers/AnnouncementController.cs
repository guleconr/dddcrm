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
using System.Web;
using System.IO;
using System.Security.Claims;

namespace TBBProject.Admin.Web.Controllers
{
    [Authorize]
    public class AnnouncementController : BaseController
    {
        private readonly IAnnouncementBusiness _announcementBusiness;
        private readonly IStringLocalizer<BaseController> _localizer;
        private readonly IAccountBusiness _accountBusiness;
        private readonly IDefinitionsBusiness _definitionBusiness;
        public AnnouncementController(IAccountBusiness accountBusiness, IAnnouncementBusiness announcementBusiness, IStringLocalizer<BaseController> localizer, IDefinitionsBusiness definitionBusiness)
                          : base(accountBusiness, localizer)
        {
            _announcementBusiness = announcementBusiness;
            _localizer = localizer;
            _accountBusiness = accountBusiness;
            _definitionBusiness = definitionBusiness;

        }



        [PageCheckAttribute]
        [Route("Announcement")]
        public async Task<IActionResult> Announcement()
        {
            return View();
        }

        [Route("CreateAnn")]
        public async Task<IActionResult> CreateAnn()
        {
            AnnouncementVM result = new AnnouncementVM();
            return PartialView(result);
        }

        [HttpPost]
        public async Task<IActionResult> AnnouncementApprovalSearchPost(AnnouncementVM model)
        {
            return PartialView("~/Views/AnnouncementApproval/AnnouncementApprovalGridView.cshtml", model);
        }

        #region Announcemet Approval View

        [PageCheckAttribute]
        [Route("AnnouncementApproval")]
        public async Task<IActionResult> AnnouncementApproval()
        {
            return View("~/Views/AnnouncementApproval/AnnouncementApproval.cshtml");
        }

        [Route("AnnouncementApprovalLangUpdate/{eventId}")]
        public IActionResult AnnouncementApprovalLangUpdate(long eventId)
        {

            var result = _announcementBusiness.GetAnnouncementLang(eventId);
            return PartialView("~/Views/AnnouncementApproval/AnnouncementApprovalLangUpdate.cshtml", result);
        }

        [Route("AnnouncementApprovalUpdate/{eventId}")]
        public async Task<IActionResult> AnnouncementApprovalUpdate(long eventId)
        {
            var result = _announcementBusiness.GetAnnouncement(eventId);
            return PartialView("~/Views/AnnouncementApproval/AnnouncementApprovalUpdate.cshtml", result);
        }

        [Route("AnnouncementApprovalAdd/{eventId}")]
        public async Task<IActionResult> AnnouncementApprovalAdd(long eventId)
        {
            AnnouncementLangVM result = new AnnouncementLangVM();
            result.AnnouncementId = eventId;
            return PartialView("~/Views/AnnouncementApproval/AnnouncementApprovalAdd.cshtml", result);
        }

        #endregion


        [HttpPost]
        public async Task<IActionResult> AnnouncementSearchPost(AnnouncementVM model)
        {
            return PartialView("AnnouncementGridView", model);
        }

        public JsonResult Get_Announcement([DataSourceRequest] DataSourceRequest request, int? IsRelease, string ReleaseDate, string EndDate, int? ApprovalStatus)
        {
            
            var value = HttpContext.Session.GetString("SessionUser");
            UserVM user = JsonConvert.DeserializeObject<UserVM>(value);
            var result = _announcementBusiness.GetAnnouncementAll(request, IsRelease,ReleaseDate, EndDate, ApprovalStatus, user);
            return Json(result);
        }

        public JsonResult Get_AnnouncementLang([DataSourceRequest] DataSourceRequest request, long announcementId)
        {
            var value = HttpContext.Session.GetString("SessionUser");
            UserVM user = JsonConvert.DeserializeObject<UserVM>(value);
            var result = _announcementBusiness.GetAnnouncementLangAll(request, announcementId, user);
            return Json(result);
        }

        public JsonResult Get_AnnouncementApproval([DataSourceRequest] DataSourceRequest request, int? IsRelease, string ReleaseDate, string EndDate)
        {

            var value = HttpContext.Session.GetString("SessionUser");
            UserVM user = JsonConvert.DeserializeObject<UserVM>(value);
            var result = _announcementBusiness.GetAnnouncementApprovalAll(request, IsRelease, ReleaseDate, EndDate, user);
            return Json(result);
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingAnnouncement_Create(AnnouncementVM announcement)
        {
            var value = HttpContext.Session.GetString("SessionUser");
            UserVM user = JsonConvert.DeserializeObject<UserVM>(value);
            announcement.Content = HttpUtility.HtmlDecode(announcement.Content);
            _announcementBusiness.CreateAnnouncement(announcement,user);
            return Content("");

        }

        [AcceptVerbs("Post")]
        public ActionResult EditingAnnouncement_Update(AnnouncementVM announcement)
        {
            _announcementBusiness.UpdateAnnouncement(announcement);
            return Content("");

        }

        [AcceptVerbs("Post")]
        public ActionResult EditingAnnouncement_Destroy(int Id)
        {
            _announcementBusiness.DeleteAnnouncement(Id);
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult AppAnnouncement(int Id)
        {
            _announcementBusiness.AppAnnouncement(Id);
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult AnnouncementLang_Create(AnnouncementLangVM announcement)
        {
            announcement.Content = HttpUtility.HtmlDecode(announcement.Content);

            var value = HttpContext.Session.GetString("SessionUser");
            UserVM user = JsonConvert.DeserializeObject<UserVM>(value);
            announcement.UserId = user.Id;
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
            model.Content = HttpUtility.HtmlDecode(model.Content);
            _announcementBusiness.UpdateAnnouncementLang(model);
            return Content("");

        }

        [AcceptVerbs("Post")]
        public ActionResult EditingAnnouncementLang_Destroy(int Id)
        {
            _announcementBusiness.DeleteAnnouncementLang(Id);
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


        public JsonResult Get_LanguageList(long announcementId, long announcementLangId)
        {
            var announcement = _announcementBusiness.GetAnnouncement(announcementId);
            var result = _definitionBusiness.GetLanguageList();
            for (int i = 0; i < announcement.AnnouncementLang.Count; i++)
            {
                for (int l = 0; l < result.Count; l++)
                {
                    if (announcement.AnnouncementLang[i].LanguageId == result[l].Id)
                        result.RemoveAt(l);
                }
            }
            return Json(result);
        }

        public JsonResult Get_LanguageListUpdate(long announcementId, long announcementLangId)
        {
            var announcement = _announcementBusiness.GetAnnouncement(announcementId);
            var result = _definitionBusiness.GetLanguageList();
            for (int i = 0; i < announcement.AnnouncementLang.Count; i++)
            {
                for (int l = 0; l < result.Count; l++)
                {
                    if (announcement.AnnouncementLang[i].LanguageId == result[l].Id)
                        result.RemoveAt(l);
                }
            }
            if (result.Count == 1)
            {
                result = _definitionBusiness.GetLanguageList();
            }
            if (result.Count == 0)
            {
                var lang = _announcementBusiness.GetAnnouncementLang(announcementLangId);
                result.Add(lang.Language);
            }

            return Json(result);
        }

    }

}

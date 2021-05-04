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
    public class LegislationAnnouncementController : BaseController
    {
        private readonly ILegislationAnnouncementBusiness _legislationAnnouncementBusiness;
        private readonly IStringLocalizer<BaseController> _localizer;
        private readonly IAccountBusiness _accountBusiness;
        private readonly IDefinitionsBusiness _definitionBusiness;
        public LegislationAnnouncementController(IAccountBusiness accountBusiness, ILegislationAnnouncementBusiness legislationAnnouncementBusiness, IStringLocalizer<BaseController> localizer, IDefinitionsBusiness definitionBusiness)
                          : base(accountBusiness, localizer)
        {
            _legislationAnnouncementBusiness = legislationAnnouncementBusiness;
            _localizer = localizer;
            _accountBusiness = accountBusiness;
            _definitionBusiness = definitionBusiness;

        }


        [PageCheckAttribute]
        [Route("LegislationAnnouncement")]
        public async Task<IActionResult> LegislationAnnouncement()
        {
            return View();
        }


        [Route("CreateLA")]
        public async Task<IActionResult> CreateLA()
        {
            LegislationAnnouncementVM result = new LegislationAnnouncementVM();
            return PartialView(result);
        }
        [HttpPost]
        public async Task<IActionResult> LegislationAnnouncementSearchPost(LegislationAnnouncementVM model)
        {
            return PartialView("LegislationAnnouncementGridView", model);
        }

        public JsonResult Get_LegislationAnnouncement([DataSourceRequest] DataSourceRequest request, int? IsRelease, string ReleaseDate, string EndDate)
        {
            var result = _legislationAnnouncementBusiness.GetLegislationAnnouncementAll(request, IsRelease, ReleaseDate, EndDate);
            return Json(result);
        }

        public JsonResult Get_LegislationAnnouncementLang([DataSourceRequest] DataSourceRequest request, long legislationAnnouncementId)
        {
            var result = _legislationAnnouncementBusiness.GetLegislationAnnouncementLangAll(request, legislationAnnouncementId);
            return Json(result);
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingLegislationAnnouncement_Create(LegislationAnnouncementVM legislationAnnouncement)
        {
            legislationAnnouncement.Content = HttpUtility.HtmlDecode(legislationAnnouncement.Content);

            var value = HttpContext.Session.GetString("SessionUser");
            UserVM user = JsonConvert.DeserializeObject<UserVM>(value);
            _legislationAnnouncementBusiness.CreateLegislationAnnouncement(legislationAnnouncement, user);
            return Content("");

        }

        [AcceptVerbs("Post")]
        public ActionResult EditingLegislationAnnouncement_Update(LegislationAnnouncementVM legislationAnnouncement)
        {
            legislationAnnouncement.Content = HttpUtility.HtmlDecode(legislationAnnouncement.Content);

            _legislationAnnouncementBusiness.UpdateLegislationAnnouncement(legislationAnnouncement);

            return Content("");

        }

        [AcceptVerbs("Post")]
        public ActionResult EditingLegislationAnnouncement_Destroy(int Id)
        {
            _legislationAnnouncementBusiness.DeleteLegislationAnnouncement(Id);
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult AppLegislationAnnouncement(int Id)
        {
            _legislationAnnouncementBusiness.AppLegislationAnnouncement(Id);
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult LegislationAnnouncementLang_Create(LegislationAnnouncementLangVM legislationAnnouncement)
        {
            legislationAnnouncement.Content = HttpUtility.HtmlDecode(legislationAnnouncement.Content);

            var value = HttpContext.Session.GetString("SessionUser");
            UserVM user = JsonConvert.DeserializeObject<UserVM>(value);
            legislationAnnouncement.UserId = user.Id;
            _legislationAnnouncementBusiness.CreateLegislationAnnouncementLang(legislationAnnouncement);
            return Content("");

        }

        [AcceptVerbs("Post")]
        public ActionResult EditingLegislationAnnouncementLang_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<LegislationAnnouncementLangVM> legislationAnnouncement)
        {
            _legislationAnnouncementBusiness.CreateLegislationAnnouncementLang(legislationAnnouncement.FirstOrDefault());
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingLegislationAnnouncementLang_Update(LegislationAnnouncementLangVM model)
        {
            model.Content = HttpUtility.HtmlDecode(model.Content);
            _legislationAnnouncementBusiness.UpdateLegislationAnnouncementLang(model);
            return Content("");

        }

        [AcceptVerbs("Post")]
        public ActionResult EditingLegislationAnnouncementLang_Destroy(int Id)
        {
            _legislationAnnouncementBusiness.DeleteLegislationAnnouncementLang(Id);
            return Content("");
        }

        [Route("LegislationAnnouncementLangUpdate/{eventId}")]
        public IActionResult LegislationAnnouncementLangUpdate(long eventId)
        {

            var result = _legislationAnnouncementBusiness.GetLegislationAnnouncementLang(eventId);
            return PartialView(result);
        }

        [Route("LegislationAnnouncementUpdate/{eventId}")]
        public async Task<IActionResult> LegislationAnnouncementUpdate(long eventId)
        {
            var result = _legislationAnnouncementBusiness.GetLegislationAnnouncement(eventId);
            return PartialView(result);
        }

        [Route("LegislationAnnouncementAdd/{eventId}")]
        public async Task<IActionResult> LegislationAnnouncementAdd(long eventId)
        {
            LegislationAnnouncementLangVM result = new LegislationAnnouncementLangVM();
            result.LegislationAnnouncementId = eventId;
            return PartialView(result);
        }


        public JsonResult Get_LanguageList(long legislationAnnouncementId, long legislationAnnouncemenLangId)
        {
            var legislationAnnouncement = _legislationAnnouncementBusiness.GetLegislationAnnouncement(legislationAnnouncementId);
            var result = _definitionBusiness.GetLanguageList();
            for (int i = 0; i < legislationAnnouncement.LegislationAnnouncementLang.Count; i++)
            {
                for (int l = 0; l < result.Count; l++)
                {
                    if (legislationAnnouncement.LegislationAnnouncementLang[i].LanguageId == result[l].Id)
                        result.RemoveAt(l);
                }
            }
            if (result.Count == 0)
            {
                var lang = _legislationAnnouncementBusiness.GetLegislationAnnouncementLang(legislationAnnouncemenLangId);
                result.Add(lang.Language);
            }
            return Json(result);
        }

        public JsonResult Get_LanguageListUpdate(long legislationAnnouncementId, long legislationAnnouncemenLangId)
        {
            var legislationAnnouncement = _legislationAnnouncementBusiness.GetLegislationAnnouncement(legislationAnnouncementId);
            var result = _definitionBusiness.GetLanguageList();
            for (int i = 0; i < legislationAnnouncement.LegislationAnnouncementLang.Count; i++)
            {
                for (int l = 0; l < result.Count; l++)
                {
                    if (legislationAnnouncement.LegislationAnnouncementLang[i].LanguageId == result[l].Id)
                        result.RemoveAt(l);
                }
            }
            if (result.Count == 1)
            {
                result = _definitionBusiness.GetLanguageList();
            }
            if (result.Count == 0)
            {
                var lang = _legislationAnnouncementBusiness.GetLegislationAnnouncementLang(legislationAnnouncemenLangId);
                result.Add(lang.Language);
            }

            return Json(result);
        }

    }

}

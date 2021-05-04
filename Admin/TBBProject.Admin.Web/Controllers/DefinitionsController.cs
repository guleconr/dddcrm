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
    public class DefinitionsController : BaseController
    {
        private readonly IDefinitionsBusiness _definitionBusiness;
        private readonly IStringLocalizer<BaseController> _localizer;
        private readonly ICacheProvider _cache;
        public DefinitionsController(IAccountBusiness accountBusiness, ICacheProvider cache, IStringLocalizer<BaseController> localizer, IDefinitionsBusiness definitionBusiness)
            : base(accountBusiness, localizer)
        {
            _localizer = localizer;
            _cache = cache;
            _definitionBusiness = definitionBusiness;

        }
        #region Resources
        [Route("Resources")]
        [PageCheckAttribute]
        public async Task<IActionResult> Resources()
        {
            var result = _definitionBusiness.GetLanguagesAsync().Result.FirstOrDefault();
            return View(result.Id);
        }
        public IActionResult Resources2()
        {
            return View();
        }
        public JsonResult Get_Resources([DataSourceRequest] DataSourceRequest request, long languageId)
        {
            var result = _definitionBusiness.GetResources(languageId, request);
            return Json(result);
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingResources_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<ResourcesVM> resources)
        {
            if (resources != null && ModelState.IsValid)
            {
                _definitionBusiness.CreateResources(request, resources);
            }
            return Json(resources.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingResources_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<ResourcesVM> resources)
        {
            if (resources != null && ModelState.IsValid)
            {
                _definitionBusiness.UpdateResources(request, resources);
            }
            return Json(resources.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingResources_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<ResourcesVM> resources)
        {
            if (resources.Any())
            {
                _definitionBusiness.DeleteResources(request, resources);
            }
            return Json(resources.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region Languages
        [Route("Languages")]
        [PageCheckAttribute]
        public async Task<IActionResult> Languages()
        {
            var result = _definitionBusiness.GetLanguagesAsync().Result.FirstOrDefault();
            return View(result.Id);
        }
        public IActionResult Languages2()
        {
            return View();
        }
        public JsonResult Get_LanguagesAsyc()
        {
            var result = _definitionBusiness.GetLanguagesAsync();
            return Json(result);
        }
        public JsonResult Get_Languages([DataSourceRequest] DataSourceRequest request)
        {
            var result = _definitionBusiness.GetLanguages(request);
            return Json(result);
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingLanguages_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<LanguageVM> languages)
        {
            if (languages != null && ModelState.IsValid)
            {
                _definitionBusiness.CreateLanguages(request, languages);
            }
            return Json(languages.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingLanguages_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<LanguageVM> languages)
        {
            if (languages != null && ModelState.IsValid)
            {
                _definitionBusiness.UpdateLanguages(request, languages);
            }
            return Json(languages.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingLanguages_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<LanguageVM> languages)
        {
            if (languages.Any())
            {
                _definitionBusiness.DeleteLanguages(request, languages);
            }
            return Json(languages.ToDataSourceResult(request, ModelState));
        }
        public JsonResult Get_LanguageList()
        {
            var result = _definitionBusiness.GetLanguageList();
            return Json(result);
        }
        #endregion

        #region Announcement
        [PageCheckAttribute]
        [Route("AnnouncementType")]
        public async Task<IActionResult> AnnouncementType()
        {
            return View();
        }

        public JsonResult Get_AnnouncementType([DataSourceRequest] DataSourceRequest request)
        {
            var result = _definitionBusiness.GetAnnouncement(request);
            return Json(result);
        }

        public JsonResult Get_AnnouncementList()
        {
            var result = _definitionBusiness.Get_AnnouncementList();
            return Json(result);
        }

     

        [AcceptVerbs("Post")]
        public ActionResult EditingAnnouncementType_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<AnnouncementTypeVM> announcement)
        {
            if (announcement != null && ModelState.IsValid)
            {
                _definitionBusiness.CreateAnnouncement(request, announcement);
            }
            return Json(announcement.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingAnnouncementType_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<AnnouncementTypeVM> announcement)
        {
            if (announcement != null && ModelState.IsValid)
            {
                _definitionBusiness.UpdateAnnouncement(request, announcement);
            }
            return Json(announcement.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingAnnouncementType_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<AnnouncementTypeVM> announcement)
        {
            if (announcement.Any())
            {
                _definitionBusiness.DeleteAnnouncement(request, announcement);
            }
            return Json(announcement.ToDataSourceResult(request, ModelState));
        }

        public async Task<IActionResult> ResourceGridDiv(long languageId)
        {
            var result = await _definitionBusiness.GetLanguagesAsync();
            ViewData["languages"] = result;
            return PartialView(languageId);
        }

        public async Task<IActionResult> RefreshLanguage()
        {

            var value = HttpContext.Session.GetString("SessionUser");
            var user = JsonConvert.DeserializeObject<UserVM>(value);
            _cache.RemovePatternExist(user.Language);
            return Content("Done");
        }
        #endregion

        #region Icons
        public async Task<JsonResult> Get_IconsAsyc()
        {

            var result = _cache.Get<List<IconVM>>("Icons");
            if (result == null)
            {
                result = await _definitionBusiness.Get_IconsAsyc();
                _cache.Set("Icons", result);
            }
            return Json(result);
        }
        #endregion

        #region Enums
        public JsonResult Get_YesNoEnumList() => Json(EnumExtension.EnumToLocalizedList<YesNoEnum>(_localizer).Select(x => new { Id = x.Value, Name = x.Text }));
        public JsonResult Get_AuthorityTypeEnumList() => Json(EnumExtension.EnumToLocalizedList<AuthorityTypeEnum>(_localizer).Select(x => new { Id = x.Value, Name = x.Text }));
        public JsonResult Get_StatusEnumList() => Json(EnumExtension.EnumToLocalizedList<StatusEnum>(_localizer).Select(x => new { Id = x.Value, Name = x.Text }));
        public JsonResult Get_ApprovalTypeList() => Json(EnumExtension.EnumToLocalizedList<ApprovalStatus>(_localizer).Select(x => new { Id = x.Value, Name = x.Text }));
        public JsonResult Get_MunicipalityList() => Json(EnumExtension.EnumToLocalizedList<MunicipalityEnum>(_localizer).Select(x => new { Id = x.Value, Name = x.Text }));

        public JsonResult Get_MunicipalityCityList() => Json(EnumExtension.EnumToLocalizedList<MunicipalityCityEnum>(_localizer).Select(x => new { Id = x.Value, Name = x.Text }));


        #endregion

    }

}

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
    public class ContentController : BaseController
    {
        private readonly IContentBusiness _contentBusiness;
        private readonly IStringLocalizer<BaseController> _localizer;
        private readonly IAccountBusiness _accountBusiness;
        private readonly IDefinitionsBusiness _definitionBusiness;
        public ContentController(IAccountBusiness accountBusiness, IContentBusiness contentBusiness, IStringLocalizer<BaseController> localizer, IDefinitionsBusiness definitionBusiness)
                          : base(accountBusiness, localizer)
        {
            _contentBusiness = contentBusiness;
            _localizer = localizer;
            _accountBusiness = accountBusiness;
            _definitionBusiness = definitionBusiness;

        }


        [PageCheckAttribute]
        [Route("Content")]
        public async Task<IActionResult> Content()
        {
            return View();
        }

        [Route("CreateContent")]
        public async Task<IActionResult> CreateContent()
        {
            ContentVM result = new ContentVM();
            return PartialView(result);
        }

        [HttpPost]
        public async Task<IActionResult> ContentSearchPost(ContentVM model)
        {
            return PartialView("ContentGridView", model);
        }

        public JsonResult Get_Content([DataSourceRequest] DataSourceRequest request, int? IsRelease, string ReleaseDate, string EndDate)
        {
            var value = HttpContext.Session.GetString("SessionUser");
            UserVM user = JsonConvert.DeserializeObject<UserVM>(value);
            var result = _contentBusiness.GetContentAll(request, IsRelease, ReleaseDate, EndDate, user);
            return Json(result);
        }

        public JsonResult Get_ContentLang([DataSourceRequest] DataSourceRequest request, long contentId)
        {
            var value = HttpContext.Session.GetString("SessionUser");
            UserVM user = JsonConvert.DeserializeObject<UserVM>(value);
            var result = _contentBusiness.GetContentLangAll(request, contentId, user);
            return Json(result);
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingContent_Create(ContentVM model)
        {

            model.Content = HttpUtility.HtmlDecode(model.Content);

            var value = HttpContext.Session.GetString("SessionUser");
            UserVM user = JsonConvert.DeserializeObject<UserVM>(value);
            _contentBusiness.CreateContent(model, user);
            return Content("");

        }

        [AcceptVerbs("Post")]
        public ActionResult EditingContent_Update(ContentVM content)
        {

            _contentBusiness.UpdateContent(content);

            return Content("");

        }

        [AcceptVerbs("Post")]
        public ActionResult EditingContent_Destroy(int Id)
        {
            _contentBusiness.DeleteContent(Id);
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult AppContent(int Id)
        {
            _contentBusiness.AppContent(Id);
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult ContentLang_Create(ContentLangVM model)
        {
            model.Content = HttpUtility.HtmlDecode(model.Content);

            var value = HttpContext.Session.GetString("SessionUser");
            UserVM user = JsonConvert.DeserializeObject<UserVM>(value);
            model.UserId = user.Id;
            _contentBusiness.CreateContentLang(model);
            return Content("");

        }

        [AcceptVerbs("Post")]
        public ActionResult EditingContentLang_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<ContentLangVM> content)
        {
            _contentBusiness.CreateContentLang(content.FirstOrDefault());
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingContentLang_Update(ContentLangVM model)
        {
            model.Content = HttpUtility.HtmlDecode(model.Content);
            _contentBusiness.UpdateContentLang(model);
            return Content("");

        }

        [AcceptVerbs("Post")]
        public ActionResult EditingContentLang_Destroy(int Id)
        {
            _contentBusiness.DeleteContentLang(Id);
            return Content("");
        }

        [Route("ContentLangUpdate/{eventId}")]
        public IActionResult ContentLangUpdate(long eventId)
        {

            var result = _contentBusiness.GetContentLang(eventId);
            return PartialView(result);
        }

        [Route("ContentUpdate/{eventId}")]
        public async Task<IActionResult> ContentUpdate(long eventId)
        {
            var result = _contentBusiness.GetContent(eventId);
            return PartialView(result);
        }

        [Route("ContentAdd/{eventId}")]
        public async Task<IActionResult> ContentAdd(long eventId)
        {
            ContentLangVM result = new ContentLangVM();
            result.ContentId = eventId;
            return PartialView(result);
        }


        public JsonResult Get_LanguageList(long contentId, long contentLangId)
        {
            var content = _contentBusiness.GetContent(contentId);
            var result = _definitionBusiness.GetLanguageList();
            for (int i = 0; i < content.ContentLang.Count; i++)
            {
                for (int l = 0; l < result.Count; l++)
                {
                    if (content.ContentLang[i].LanguageId == result[l].Id)
                        result.RemoveAt(l);
                }
            }
            if (result.Count == 0)
            {
                var lang = _contentBusiness.GetContentLang(contentLangId);
                result.Add(lang.Language);
            }
            return Json(result);
        }

        public JsonResult Get_LanguageListUpdate(long contentId, long contentLangId)
        {
            var content = _contentBusiness.GetContent(contentId);
            var result = _definitionBusiness.GetLanguageList();
            for (int i = 0; i < content.ContentLang.Count; i++)
            {
                for (int l = 0; l < result.Count; l++)
                {
                    if (content.ContentLang[i].LanguageId == result[l].Id)
                        result.RemoveAt(l);
                }
            }
            if (result.Count == 1)
            {
                result = _definitionBusiness.GetLanguageList();
            }
            if (result.Count == 0)
            {
                var lang = _contentBusiness.GetContentLang(contentLangId);
                result.Add(lang.Language);
            }

            return Json(result);
        }

    }

}

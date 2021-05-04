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
using System.Security.Claims;
using System.IO;

namespace TBBProject.Admin.Web.Controllers
{
    [Authorize]
    public class NewsController : BaseController
    {
        private readonly INewsBusiness _newsBusiness;
        private readonly IStringLocalizer<BaseController> _localizer;
        private readonly IAccountBusiness _accountBusiness;
        private readonly IDefinitionsBusiness _definitionBusiness;


        public NewsController(IAccountBusiness accountBusiness, INewsBusiness newsBusiness, IStringLocalizer<BaseController> localizer
            , IDefinitionsBusiness definitionBusiness)
                          : base(accountBusiness, localizer)
        {
            _newsBusiness = newsBusiness;
            _localizer = localizer;
            _accountBusiness = accountBusiness;
            _definitionBusiness = definitionBusiness;

        }


        [PageCheckAttribute]
        [Route("News")]
        public async Task<IActionResult> News()
        {
            return View();
        }

        [Route("CreateNews")]
        public async Task<IActionResult> CreateNews()
        {
            NewsVM result = new NewsVM();
            return PartialView(result);
        }

        [HttpPost]
        public async Task<IActionResult> NewsSearchPost(NewsVM model)
        {
            return PartialView("NewsGridView", model);
        }

        #region News  Approval View
        [PageCheckAttribute]
        [Route("NewsApproval")]
        public async Task<IActionResult> NewsApproval()
        {
            return View("~/Views/NewsApproval/NewsApproval.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> NewsApprovalSearchPost(NewsVM model)
        {
            return PartialView("~/Views/NewsApproval/NewsApprovalGridView.cshtml", model);
        }

        [Route("NewsApprovalLangUpdate/{eventId}")]
        public IActionResult NewsApprovalLangUpdate(long eventId)
        {
            var result = _newsBusiness.GetNewsLang(eventId);
            return PartialView("~/Views/NewsApproval/NewsApprovalLangUpdate.cshtml", result);
        }

        [Route("NewsApprovalUpdate/{eventId}")]
        public async Task<IActionResult> NewsApprovalUpdate(long eventId)
        {
            var result = _newsBusiness.GetNews(eventId);
            return PartialView("~/Views/NewsApproval/NewsApprovalUpdate.cshtml", result);
        }

        [Route("NewsApprovalAdd/{eventId}")]
        public async Task<IActionResult> NewsApprovalAdd(long eventId)
        {
            NewsLangVM result = new NewsLangVM();
            result.NewsId = eventId;
            return PartialView("~/Views/NewsApproval/NewsApprovalAdd.cshtml", result);
        }

        #endregion

        public JsonResult Get_News([DataSourceRequest] DataSourceRequest request, int? IsRelease, string ReleaseDate, string EndDate,int?ApprovalStatus)
        {
            var value = HttpContext.Session.GetString("SessionUser");
            UserVM user = JsonConvert.DeserializeObject<UserVM>(value);
            var result = _newsBusiness.GetNewsAll(request, IsRelease, ReleaseDate, EndDate, ApprovalStatus, user);
            return Json(result);
        }

        public JsonResult Get_NewsApproval([DataSourceRequest] DataSourceRequest request, int? IsRelease, string ReleaseDate, string EndDate)
        {
            var value = HttpContext.Session.GetString("SessionUser");
            UserVM user = JsonConvert.DeserializeObject<UserVM>(value);
            var result = _newsBusiness.GetNewsApprovalAll(request, IsRelease, ReleaseDate, EndDate, user);
            return Json(result);
        }

        public JsonResult Get_NewsLang([DataSourceRequest] DataSourceRequest request, long newsId)
        {
            var value = HttpContext.Session.GetString("SessionUser");
            UserVM user = JsonConvert.DeserializeObject<UserVM>(value);
            var result = _newsBusiness.GetNewsLangAll(request, newsId, user);
            return Json(result);
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingNews_Create(NewsVM news)
        {
            news.Content = HttpUtility.HtmlDecode(news.Content);
            var value = HttpContext.Session.GetString("SessionUser");
            UserVM user = JsonConvert.DeserializeObject<UserVM>(value);
            _newsBusiness.CreateNews(news, user);
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingNews_Update(NewsVM news)
        {

            _newsBusiness.UpdateNews(news);
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingNews_Destroy(long Id)
        {
            _newsBusiness.DeleteNews(Id);
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult NewsLang_Create(NewsLangVM news)
        {
            news.Content = HttpUtility.HtmlDecode(news.Content);
            var value = HttpContext.Session.GetString("SessionUser");
            UserVM user = JsonConvert.DeserializeObject<UserVM>(value);
            news.UserId = user.Id;
            _newsBusiness.CreateNewsLang(news);
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingNewsLang_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<NewsLangVM> news)
        {

            _newsBusiness.CreateNewsLang(news.FirstOrDefault());
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingNewsLang_Update(NewsLangVM model)
        {
            model.Content = HttpUtility.HtmlDecode(model.Content);
            _newsBusiness.UpdateNewsLang(model);
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingNewsLang_Destroy(long Id)
        {
            _newsBusiness.DeleteNewsLang(Id);
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult AppNews(int Id)
        {
            _newsBusiness.AppNews(Id);
            return Content("");
        }

        [Route("NewsLangUpdate/{eventId}")]
        public IActionResult NewsLangUpdate(long eventId)
        {
            var result = _newsBusiness.GetNewsLang(eventId);
            return PartialView(result);
        }

        [Route("NewsUpdate/{eventId}")]
        public async Task<IActionResult> NewsUpdate(long eventId)
        {
            var result = _newsBusiness.GetNews(eventId);
            return PartialView(result);
        }

        [Route("NewsAdd/{eventId}")]
        public async Task<IActionResult> NewsAdd(long eventId)
        {
            NewsLangVM result = new NewsLangVM();
            result.NewsId = eventId;
            return PartialView(result);
        }

        

        public JsonResult Get_LanguageList(long newsId, long newsLangId)
        {
            var news = _newsBusiness.GetNews(newsId);
            var result = _definitionBusiness.GetLanguageList();
            for (int i = 0; i < news.NewsLang.Count; i++)
            {
                for (int l = 0; l < result.Count; l++)
                {
                    if (news.NewsLang[i].LanguageId == result[l].Id)
                        result.RemoveAt(l);
                }
            }
            if (result.Count == 0)
            {
                var lang = _newsBusiness.GetNewsLang(newsLangId);
                result.Add(lang.Language);
            }
            return Json(result);
        }

        public JsonResult Get_LanguageListUpdate(long newsId, long newsLangId)
        {
            var news = _newsBusiness.GetNews(newsId);
            var result = _definitionBusiness.GetLanguageList();
            for (int i = 0; i < news.NewsLang.Count; i++)
            {
                for (int l = 0; l < result.Count; l++)
                {
                    if (news.NewsLang[i].LanguageId == result[l].Id)
                        result.RemoveAt(l);
                }
            }
            if (result.Count == 1)
            {
                result = _definitionBusiness.GetLanguageList();
            }
            if (result.Count == 0)
            {
                var lang = _newsBusiness.GetNewsLang(newsLangId);
                result.Add(lang.Language);
            }

            return Json(result);
        }

    }

}

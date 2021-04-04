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
    public class NewsController : BaseController
    {
        private readonly INewsBusiness _newsBusiness;
        private readonly IStringLocalizer<BaseController> _localizer;
        private readonly IAccountBusiness _accountBusiness;
        public NewsController(IAccountBusiness accountBusiness, INewsBusiness newsBusiness, IStringLocalizer<BaseController> localizer)
                          : base(accountBusiness, localizer)
        {
            _newsBusiness = newsBusiness;
            _localizer = localizer;
            _accountBusiness = accountBusiness;
        }


        [PageCheckAttribute]
        [Route("News")]
        public async Task<IActionResult> News()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> NewsSearchPost(NewsVM model)
        {
            return PartialView("NewsGridView", model);
        }

        public JsonResult Get_News([DataSourceRequest] DataSourceRequest request, int? IsRelease, DateTime ReleaseDate)
        {
            var result = _newsBusiness.GetNewsAllAsync(request, IsRelease, ReleaseDate);
            return Json(result);
        }

        public JsonResult Get_NewsLang([DataSourceRequest] DataSourceRequest request, long newsId)
        {
            var result = _newsBusiness.GetNewsLangAllAsync(request, newsId);
            return Json(result);
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingNews_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<NewsVM> news)
        {
            _newsBusiness.CreateNews(news.FirstOrDefault());
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingNews_Update(NewsVM news)
        {
            if (news != null && ModelState.IsValid)
            {
                _newsBusiness.UpdateNews(news);
            }
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
            _newsBusiness.UpdateNewsLang(model);
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingNewsLang_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<NewsLangVM> news)
        {
            _newsBusiness.DeleteNewsLang(news.FirstOrDefault());
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

    }

}

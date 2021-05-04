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
    public class VideoGalleryController : BaseController
    {
        private readonly IVideoGalleryBusiness _videoGalleryBusiness;
        private readonly IStringLocalizer<BaseController> _localizer;
        private readonly IAccountBusiness _accountBusiness;
        private readonly IDefinitionsBusiness _definitionBusiness;

        public VideoGalleryController(IAccountBusiness accountBusiness, IVideoGalleryBusiness videoGalleryBusiness, IStringLocalizer<BaseController> localizer, IDefinitionsBusiness definitionBusiness)
                          : base(accountBusiness, localizer)
        {
            _videoGalleryBusiness = videoGalleryBusiness;
            _localizer = localizer;
            _accountBusiness = accountBusiness;
            _definitionBusiness = definitionBusiness;
        }


        [PageCheckAttribute]
        [Route("VideoGallery")]
        public async Task<IActionResult> VideoGallery()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> VideoGallerySearchPost(VideoGalleryVM model)
        {
            return PartialView("VideoGalleryGridView", model);
        }

        public JsonResult Get_VideoGallery([DataSourceRequest] DataSourceRequest request, string ReleaseDate, string EndDate)
        {
            var result = _videoGalleryBusiness.GetVideoGalleryAll(request, ReleaseDate, EndDate);
            return Json(result);
        }

        public JsonResult Get_VideoGalleryLang([DataSourceRequest] DataSourceRequest request, long videoGalleryId)
        {
            var result = _videoGalleryBusiness.GetVideoGalleryLangAll(request, videoGalleryId);
            return Json(result);
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingVideoGallery_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<VideoGalleryVM> videoGallery)
        {
            _videoGalleryBusiness.CreateVideoGallery(videoGallery.FirstOrDefault());
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingVideoGallery_Update(VideoGalleryVM videoGallery)
        {

            _videoGalleryBusiness.UpdateVideoGallery(videoGallery);

            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingVideoGallery_Destroy(long Id)
        {
            _videoGalleryBusiness.DeleteVideoGallery(Id);
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult VideoGalleryLang_Create(VideoGalleryLangVM videoGallery)
        {
            _videoGalleryBusiness.CreateVideoGalleryLang(videoGallery);
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingVideoGalleryLang_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<VideoGalleryLangVM> videoGallery)
        {
            _videoGalleryBusiness.CreateVideoGalleryLang(videoGallery.FirstOrDefault());
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingVideoGalleryLang_Update(VideoGalleryLangVM model)
        {
            _videoGalleryBusiness.UpdateVideoGalleryLang(model);
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingVideoGalleryLang_Destroy(long Id)
        {
            _videoGalleryBusiness.DeleteVideoGalleryLang(Id);
            return Content("");
        }

        [Route("VideoGalleryLangUpdate/{eventId}")]
        public IActionResult VideoGalleryLangUpdate(long eventId)
        {
            var result = _videoGalleryBusiness.GetVideoGalleryLang(eventId);
            return PartialView(result);
        }

        [Route("VideoGalleryUpdate/{eventId}")]
        public async Task<IActionResult> VideoGalleryUpdate(long eventId)
        {
            var result = _videoGalleryBusiness.GetVideoGallery(eventId);
            return PartialView(result);
        }

        [Route("VideoGalleryAdd/{eventId}")]
        public async Task<IActionResult> VideoGalleryAdd(long eventId)
        {
            VideoGalleryLangVM result = new VideoGalleryLangVM();
            result.VideoGalleryId = eventId;
            return PartialView(result);
        }


        public JsonResult Get_LanguageList(long videoGalleryId, long videoGalleryLangId)
        {
            var videoGallery = _videoGalleryBusiness.GetVideoGallery(videoGalleryId);
            var result = _definitionBusiness.GetLanguageList();
            for (int i = 0; i < videoGallery.VideoGalleryLang.Count; i++)
            {
                for (int l = 0; l < result.Count; l++)
                {
                    if (videoGallery.VideoGalleryLang[i].LanguageId == result[l].Id)
                        result.RemoveAt(l);
                }
            }
            if (result.Count == 0)
            {
                var lang = _videoGalleryBusiness.GetVideoGalleryLang(videoGalleryLangId);
                result.Add(lang.Language);
            }
            return Json(result);
        }

        public JsonResult Get_LanguageListUpdate(long videoGalleryId, long videoGalleryLangId)
        {
            var vaideoGallery = _videoGalleryBusiness.GetVideoGallery(videoGalleryId);
            var result = _definitionBusiness.GetLanguageList();
            for (int i = 0; i < vaideoGallery.VideoGalleryLang.Count; i++)
            {
                for (int l = 0; l < result.Count; l++)
                {
                    if (vaideoGallery.VideoGalleryLang[i].LanguageId == result[l].Id)
                        result.RemoveAt(l);
                }
            }
            if (result.Count == 1)
            {
                result = _definitionBusiness.GetLanguageList();
            }
            if (result.Count == 0)
            {
                var lang = _videoGalleryBusiness.GetVideoGalleryLang(videoGalleryLangId);
                result.Add(lang.Language);
            }

            return Json(result);
        }

    }

}

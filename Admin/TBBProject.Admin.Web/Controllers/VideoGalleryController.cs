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
        public VideoGalleryController(IAccountBusiness accountBusiness, IVideoGalleryBusiness videoGalleryBusiness, IStringLocalizer<BaseController> localizer)
                          : base(accountBusiness, localizer)
        {
            _videoGalleryBusiness = videoGalleryBusiness;
            _localizer = localizer;
            _accountBusiness = accountBusiness;
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

        public JsonResult Get_VideoGallery([DataSourceRequest] DataSourceRequest request, DateTime ReleaseDate)
        {
            var result = _videoGalleryBusiness.GetVideoGalleryAllAsync(request, ReleaseDate);
            return Json(result);
        }

        public JsonResult Get_VideoGalleryLang([DataSourceRequest] DataSourceRequest request, long videoGalleryId)
        {
            var result = _videoGalleryBusiness.GetVideoGalleryLangAllAsync(request, videoGalleryId);
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
            if (videoGallery != null && ModelState.IsValid)
            {
                _videoGalleryBusiness.UpdateVideoGallery(videoGallery);
            }
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
        public ActionResult EditingVideoGalleryLang_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<VideoGalleryLangVM> videoGallery)
        {
            _videoGalleryBusiness.DeleteVideoGalleryLang(videoGallery.FirstOrDefault());
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

    }

}

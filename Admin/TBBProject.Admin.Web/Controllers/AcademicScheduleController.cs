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
    public class AcademicScheduleController : BaseController
    {
        private readonly IAcademicScheduleBusiness _academicScheduleBusiness;
        private readonly IStringLocalizer<BaseController> _localizer;
        private readonly IAccountBusiness _accountBusiness;
        public AcademicScheduleController(IAccountBusiness accountBusiness, IAcademicScheduleBusiness academicScheduleBusiness, IStringLocalizer<BaseController> localizer)
                          : base(accountBusiness, localizer)
        {
            _academicScheduleBusiness = academicScheduleBusiness;
            _localizer = localizer;
            _accountBusiness = accountBusiness;
        }


        [PageCheckAttribute]
        [Route("AcademicSchedule")]
        public async Task<IActionResult> AcademicSchedule()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> AcademicScheduleSearchPost(AcademicScheduleVM model)
        {
            return PartialView("AcademicScheduleGridView", model);
        }

        public JsonResult Get_AcademicSchedule([DataSourceRequest] DataSourceRequest request, DateTime ReleaseDate)
        {
            var result = _academicScheduleBusiness.GetAcademicScheduleAllAsync(request, ReleaseDate);
            return Json(result);
        }

        public JsonResult Get_AcademicScheduleLang([DataSourceRequest] DataSourceRequest request, long academicScheduleId)
        {
            var result = _academicScheduleBusiness.GetAcademicScheduleLangAllAsync(request, academicScheduleId);
            return Json(result);
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingAcademicSchedule_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<AcademicScheduleVM> academicSchedule)
        {
            _academicScheduleBusiness.CreateAcademicSchedule(academicSchedule.FirstOrDefault());
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingAcademicSchedule_Update(AcademicScheduleVM academicSchedule)
        {
            if (academicSchedule != null && ModelState.IsValid)
            {
                _academicScheduleBusiness.UpdateAcademicSchedule(academicSchedule);
            }
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingAcademicSchedule_Destroy(long Id)
        {
            _academicScheduleBusiness.DeleteAcademicSchedule(Id);
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult AcademicScheduleLang_Create(AcademicScheduleLangVM academicSchedule)
        {
            _academicScheduleBusiness.CreateAcademicScheduleLang(academicSchedule);
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingAcademicScheduleLang_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<AcademicScheduleLangVM> academicSchedule)
        {
            _academicScheduleBusiness.CreateAcademicScheduleLang(academicSchedule.FirstOrDefault());
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingAcademicScheduleLang_Update(AcademicScheduleLangVM model)
        {
            _academicScheduleBusiness.UpdateAcademicScheduleLang(model);
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingAcademicScheduleLang_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<AcademicScheduleLangVM> academicSchedule)
        {
            _academicScheduleBusiness.DeleteAcademicScheduleLang(academicSchedule.FirstOrDefault());
            return Content("");
        }

        [Route("AcademicScheduleLangUpdate/{eventId}")]
        public IActionResult AcademicScheduleLangUpdate(long eventId)
        {
            var result = _academicScheduleBusiness.GetAcademicScheduleLang(eventId);
            return PartialView(result);
        }

        [Route("AcademicScheduleUpdate/{eventId}")]
        public async Task<IActionResult> AcademicScheduleUpdate(long eventId)
        {
            var result = _academicScheduleBusiness.GetAcademicSchedule(eventId);
            return PartialView(result);
        }

        [Route("AcademicScheduleAdd/{eventId}")]
        public async Task<IActionResult> AcademicScheduleAdd(long eventId)
        {
            AcademicScheduleLangVM result = new AcademicScheduleLangVM();
            result.AcademicScheduleId = eventId;
            return PartialView(result);
        }

    }

}

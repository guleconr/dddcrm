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
    public class MunicipalityController : BaseController
    {
        private readonly IMunicipalityBusiness _municipalityBusiness;
        private readonly IStringLocalizer<BaseController> _localizer;
        private readonly IAccountBusiness _accountBusiness;
        private readonly IDefinitionsBusiness _definitionBusiness;
        public MunicipalityController(IAccountBusiness accountBusiness, IMunicipalityBusiness municipalityBusiness, IStringLocalizer<BaseController> localizer, IDefinitionsBusiness definitionBusiness)
                          : base(accountBusiness, localizer)
        {
            _municipalityBusiness = municipalityBusiness;
            _localizer = localizer;
            _accountBusiness = accountBusiness;
            _definitionBusiness = definitionBusiness;

        }


        [PageCheckAttribute]
        [Route("Municipality")]
        public async Task<IActionResult> Municipality()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> MunicipalitySearchPost(MunicipalityVM model)
        {
            return PartialView("MunicipalityGridView", model);
        }

        public JsonResult Get_Municipality([DataSourceRequest] DataSourceRequest request, int? MunicipalityType)
        {
            var result = _municipalityBusiness.GetMunicipalityAll(request, MunicipalityType);
            return Json(result);
        }

        public JsonResult Get_MunicipalityDistrict([DataSourceRequest] DataSourceRequest request, long municipalityId)
        {
            var result = _municipalityBusiness.GetMunicipalityDistrictAll(request, municipalityId);
            return Json(result);
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingMunicipality_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<MunicipalityVM> municipality)
        {

            var ann = municipality.FirstOrDefault();
            _municipalityBusiness.CreateMunicipality(ann);
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingMunicipality_Update(MunicipalityVM municipality)
        {

            _municipalityBusiness.UpdateMunicipality(municipality);
            return Content("");

        }

        [AcceptVerbs("Post")]
        public ActionResult EditingMunicipality_Destroy(int Id)
        {
            _municipalityBusiness.DeleteMunicipality(Id);
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult AppMunicipality(int Id)
        {
            _municipalityBusiness.AppMunicipality(Id);
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult MunicipalityDistrict_Create(DistrictVM municipality)
        {
            _municipalityBusiness.CreateMunicipalityDistrict(municipality);
            return Content("");

        }

        [AcceptVerbs("Post")]
        public ActionResult EditingMunicipalityDistrict_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<DistrictVM> municipality)
        {
            _municipalityBusiness.CreateMunicipalityDistrict(municipality.FirstOrDefault());
            return Content("");
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingMunicipalityDistrict_Update(DistrictVM model)
        {
            _municipalityBusiness.UpdateMunicipalityDistrict(model);
            return Content("");

        }

        [AcceptVerbs("Post")]
        public ActionResult EditingMunicipalityDistrict_Destroy(int Id)
        {
            _municipalityBusiness.DeleteMunicipalityDistrict(Id);
            return Content("");
        }

        [Route("MunicipalityDistrictUpdate/{eventId}")]
        public IActionResult MunicipalityDistrictUpdate(long eventId)
        {

            var result = _municipalityBusiness.GetMunicipalityDistrict(eventId);
            return PartialView(result);
        }

        [Route("MunicipalityUpdate/{eventId}")]
        public async Task<IActionResult> MunicipalityUpdate(long eventId)
        {
            var result = _municipalityBusiness.GetMunicipality(eventId);
            return PartialView(result);
        }

        [Route("MunicipalityAdd/{eventId}")]
        public async Task<IActionResult> MunicipalityAdd(long eventId)
        {
            DistrictVM result = new DistrictVM();
            result.MunicipalityId = eventId;
            return PartialView(result);
        }

    }

}

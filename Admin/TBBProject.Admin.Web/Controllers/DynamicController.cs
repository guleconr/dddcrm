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
    public class DynamicController : BaseController
    {
        private readonly IDynamicQuestionsBusiness _dynamicQuestionsBusiness;
        private readonly IStringLocalizer<BaseController> _localizer;
        private readonly ICacheProvider _cache;
        public DynamicController(IAccountBusiness accountBusiness, ICacheProvider cache, IStringLocalizer<BaseController> localizer, IDynamicQuestionsBusiness dynamicQuestionsBusiness)
            : base(accountBusiness, localizer)
        {
            _localizer = localizer;
            _cache = cache;
            _dynamicQuestionsBusiness = dynamicQuestionsBusiness;

        }
        #region DynamicForms
        [Route("DynamicForms")]
        [PageCheckAttribute]
        public async Task<IActionResult> DynamicForms()
        {
            return View();
        }
        public JsonResult Get_DynamicMenu([DataSourceRequest] DataSourceRequest request)
        {
            var result = _dynamicQuestionsBusiness.ReadDynamicQuestions(request);
            return Json(result);
        }

        //[AcceptVerbs("Post")]
        //public ActionResult EditingDynamicMenu_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<ResourcesVM> resources)
        //{
        //    if (resources != null && ModelState.IsValid)
        //    {
        //        _definitionBusiness.CreateDynamicMenu(request, resources);
        //    }
        //    return Json(resources.ToDataSourceResult(request, ModelState));
        //}

        //[AcceptVerbs("Post")]
        //public ActionResult EditingDynamicMenu_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<ResourcesVM> resources)
        //{
        //    if (resources != null && ModelState.IsValid)
        //    {
        //        _definitionBusiness.UpdateResources(request, resources);
        //    }
        //    return Json(resources.ToDataSourceResult(request, ModelState));
        //}

        //[AcceptVerbs("Post")]
        //public ActionResult EditingDynamicMenu_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<ResourcesVM> resources)
        //{
        //    if (resources.Any())
        //    {
        //        _definitionBusiness.DeleteResources(request, resources);
        //    }
        //    return Json(resources.ToDataSourceResult(request, ModelState));
        //}

        #endregion

        #region DynamicReports
        [Route("DynamicReports")]
        [PageCheckAttribute]
        public async Task<IActionResult> DynamicReports()
        {
            return View();
        }
        #endregion

    }

}

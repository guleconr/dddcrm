using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBBProject.Admin.Web.Helpers;
using TBBProject.Core.BusinessContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TBBProject.Core.Common.Enums;
using TBBProject.Admin.Web.Controllers;

namespace TBBProject.Admin.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(IAccountBusiness accountBusiness, IStringLocalizer<BaseController> localizer)
             : base(accountBusiness, localizer)
        {
        }
        [PageCheckAttribute]
        public IActionResult Index()
        {
            return View();
        }
        [PageCheckAttribute]
        [Route("UserProfile")]
        public async Task<IActionResult> UserProfile()
        {
            return View();
        }

    }
}

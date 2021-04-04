using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using TBBProject.Core.Common;
using TBBProject.Core.Data;
using TBBProject.Core.Data.Domain;
using TBBProject.Core.Localization;
using TBBProject.Core.Services;
using TBBProject.Core.Web;

namespace TBBProject.Web.Controllers
{
    public class AccountController : Controller
    {

        [HttpGet]
        [AllowAnonymous]
        [Route("login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


    }
}

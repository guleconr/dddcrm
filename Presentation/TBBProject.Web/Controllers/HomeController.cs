using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TBBProject.Core.Common.Helper;
using TBBProject.Core.Localization;
using TBBProject.Core.Services;
using TBBProject.Core.Web;
using TBBProject.Web.Models;

namespace TBBProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;
        public static string Language = "";

        public HomeController(
           
           IStringLocalizer<HomeController> localizer)
        {
           
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Index")]
        [DisableAudit]
        public IActionResult Index(string lang)
        {
            if (Language == "")
            {
                HttpContext.Session.SetString("ProjectCulture", "en-US");
                Language = "en-US";
            }
            else if(Language== "tr-TR")
            {
                HttpContext.Session.SetString("ProjectCulture", "en-US");
                Language = "en-US";

            }
            else
            {
                Language = "tr-TR";
                HttpContext.Session.SetString("ProjectCulture", Language);
            }

            return View();
        }


        [Route("{keyword}-*-{selectedId}")]
        public IActionResult Test(LocalizationUrlViewModel model)
        {
            return View();
        }
    }
}
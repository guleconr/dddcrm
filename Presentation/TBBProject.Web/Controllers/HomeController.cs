using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TBBProject.Core.Common.Helper;
using TBBProject.Core.Localization;
using TBBProject.Core.Services;
using TBBProject.Core.Web;
using TBBProject.Web.Extensions;
using TBBProject.Web.Models;

namespace TBBProject.Web.Controllers
{
    [ServiceFilter(typeof(UrlLocalizationFilter))]
    public class HomeController : Controller
    {
        public HomeController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("{keyword}-*-{selectedId}")]
        public IActionResult Test(LocalizationUrlViewModel model)
        {
            return Content("sss");
        }
    }
}
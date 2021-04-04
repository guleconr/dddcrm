using Microsoft.AspNetCore.Mvc;
using TBBProject.Core.Common;

namespace TBBProject.Core.Web
{
    public class ErrorPagesController : BaseController
    {
        private readonly ICorrelationIdAccessor _correlationIdAccessor;
        public ErrorPagesController(ICorrelationIdAccessor correlationIdAccessor) => _correlationIdAccessor = correlationIdAccessor;

        [Route("/errors/page-not-found")]
        public IActionResult PageNotFound() => View("PageNotFound");

        [Route("/errors/forbidden")]
        public IActionResult Forbidden()
        {
            var errorViewModel = new ServerErrorViewModel
            {
                CorrelationId = _correlationIdAccessor.GetCorrelationId()
            };
            return View("Forbidden", errorViewModel);
        }

        [Route("/errors/unauthorised")]
        public IActionResult Unauthorised()
        {
            var errorViewModel = new ServerErrorViewModel
            {
                CorrelationId = _correlationIdAccessor.GetCorrelationId()
            };
            return View("Unauthorised", errorViewModel);
        }

        [Route("/errors/internal-server-error")]
        public IActionResult InternalServerError()
        {
            var errorViewModel = new ServerErrorViewModel
            {
                CorrelationId = _correlationIdAccessor.GetCorrelationId()
            };
            return View("~/Views/ErrorPages/InternalServerError.cshtml", errorViewModel);
        }
    }
}

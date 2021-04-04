using System.Linq;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using TBBProject.Core.Data;
using TBBProject.Core.Data.Domain;
using TBBProject.Core.Web;
using X.PagedList;

namespace TBBProject.Admin.Web
{
    [Authorize(Roles = "admin")]
    public class AuditController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Audit> _auditRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private readonly ILogger<AuditController> _logger;
        private readonly IStringLocalizer<AuditController> _localizer;


        public AuditController(
            IUnitOfWork uow,
            ILogger<AuditController> logger,
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer<AuditController> localizer)
        {
            _uow = uow;
            _auditRepository = uow.Repository<Audit>();
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _localizer = localizer;
        }

        public IActionResult Index() => View();

        [HttpPost]
        [DisableAudit]
        public JsonResult RenderAudits([DataSourceRequest] DataSourceRequest request)
        {
            var audits = _auditRepository.TableNoTracking.OrderByDescending(e => e.Id);
            var result = audits.ToDataSourceResult(request);
            return Json(result);
        }

        public IActionResult Alternate(int? page)
        {
            var pageNumber = page ?? 1;
            var audits = _auditRepository.TableNoTracking.OrderByDescending(e => e.Id).ToPagedList(pageNumber, 25);
            return View(audits);
        }
    }
}

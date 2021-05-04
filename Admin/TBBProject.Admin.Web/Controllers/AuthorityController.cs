using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBBProject.Admin.Web.Helpers;
using TBBProject.Core.BusinessContracts;
using TBBProject.Core.BusinessContracts.ViewModels;
using TBBProject.Core.Common;
using TBBProject.Core.Common.Enums;
using TBBProject.Core.Common.Extensions;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace TBBProject.Admin.Web.Controllers
{
    [Authorize]
    public class AuthorityController : BaseController
    {
        private readonly IAuthorityBusiness _authorityBusiness;
        private readonly IAccountBusiness _accountBusiness;
        private readonly IStringLocalizer<BaseController> _localizer;
        public AuthorityController(IAccountBusiness accountBusiness, IStringLocalizer<BaseController> localizer, IAuthorityBusiness authorityBusiness)
              : base(accountBusiness, localizer)
        {
            _authorityBusiness = authorityBusiness;
            _localizer = localizer;
            _accountBusiness = accountBusiness;
        }
        #region Role
        [Route("Role")]
        [PageCheckAttribute]
        public IActionResult Role()
        {
            return View();
        }
        public JsonResult Get_Roles([DataSourceRequest] DataSourceRequest request)
        {
            var result = _authorityBusiness.GetRoles(request);
            return Json(result);
        }
        public JsonResult Get_RolesDropdown()
        {
            var result = _authorityBusiness.GetRoles();
            return Json(result);
        }
        [AcceptVerbs("Post")]
        // [ProcessLoggerFilterFactory(ProcessLevel = 1, ReferenceTableId = (int)ReferenceTableEnum.Home)]
        public ActionResult EditingRoles_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<RoleVM> roles)
        {
            if (roles != null && ModelState.IsValid)
            {
                _authorityBusiness.CreateRoles(request, roles);
            }
            return Json(roles.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        // [ProcessLoggerFilterFactory(ProcessLevel = 1, ReferenceTableId = (int)ReferenceTableEnum.Home)]
        public ActionResult EditingRoles_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<RoleVM> roles)
        {
            if (roles != null && ModelState.IsValid)
            {
                _authorityBusiness.UpdateRoles(request, roles);
            }
            return Json(roles.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        //[ProcessLoggerFilterFactory(ProcessLevel = 1, ReferenceTableId = (int)ReferenceTableEnum.Home)]
        public ActionResult EditingRoles_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<RoleVM> roles)
        {
            if (roles.Any())
            {
                _authorityBusiness.DeleteRoles(request, roles);
            }
            return Json(roles.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region Authority
        [Route("Authority")]
        [PageCheckAttribute]
        public IActionResult Authority()
        {
            return View();
        }

        public JsonResult GetAuthorityTree(long? id)
        {
            var result = _authorityBusiness.GetAuthorityTree(id);
            return Json(result);
        }
        public JsonResult GetRoleAuthorityTree(long? id, long roleId)
        {
            var result = _authorityBusiness.GetRoleAuthorityTree(id, roleId);
            return Json(result);
        }
        public IActionResult AuthorityGridDiv(long authorityId)
        {
            return PartialView(authorityId);
        }

        public JsonResult Get_Authoritys([DataSourceRequest] DataSourceRequest request, long parentId)
        {
            var result = _authorityBusiness.GetAuthoritys(request, parentId);
            return Json(result);
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingAuthority_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<AuthorityVM> authoritys)
        {
            if (authoritys != null && ModelState.IsValid)
            {
                _authorityBusiness.CreateAuthoritys(request, authoritys);
            }
            return Json(authoritys.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingAuthority_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<AuthorityVM> authoritys)
        {
            if (authoritys != null && ModelState.IsValid)
            {
                _authorityBusiness.UpdateAuthoritys(request, authoritys);
            }
            return Json(authoritys.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingAuthority_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<AuthorityVM> authoritys)
        {
            if (authoritys.Any())
            {
                _authorityBusiness.DeleteAuthoritys(request, authoritys);
            }
            return Json(authoritys.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region RoleAuthority
        [Route("RoleAuthority")]
        [PageCheckAttribute]
        public IActionResult RoleAuthority()
        {
            return View();
        }
        public IActionResult RoleAuthorityTree(long roleId)
        {
            return PartialView(roleId);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> RoleAuthorityPost(string selectedIds, long selectedRoleId)
        {
            if (selectedIds == null)
            {
                FormResultModel result = new FormResultModel
                {
                    IsSuccess = false,
                    ResultText = _localizer[LocalizationCaptions.AuthorizeNothingChange].Value
                };
                return Json(result);
            }
            else
            {
                var result = await _authorityBusiness.UpdateRoleAuthoritysAsync(selectedIds, selectedRoleId);
                return Json(result);
            }
        }

        #endregion

        #region UserRole
        [Route("UserRole")]
        [PageCheckAttribute]
        public IActionResult UserRole()
        {
            return View();
        }
        public async Task<IActionResult> UserRoleListBox(long userId)
        {
            var result = await _authorityBusiness.GetUserRolesAsync(userId);
            ViewBag.Roles = result.SelectableRoles;
            ViewBag.RolesAdded = result.SelectedRoles;
            return PartialView(userId);
        }

        public async Task<JsonResult> Get_UsersDropdown(string text)
        {
            var value = HttpContext.Session.GetString("SessionUser");
            UserVM user = null;
            if (value != null)
            {
                user = JsonConvert.DeserializeObject<UserVM>(value);
            }
            if (user != null)
            {
                var result = await _accountBusiness.GetUsersByMailTextAsync(text);
                return Json(result.ToList());

            }
            else
            {
                var result = await _accountBusiness.GetUsersByMailTextAsync(text);
                return Json(result);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> UserRolePost(string selectedIds, long selectedUserId)
        {

            var result = await _authorityBusiness.UpdateUserRoleAsync(selectedIds, selectedUserId);
            return Json(result);
        }
        #endregion

        #region Users
        [Route("Users")]
        [PageCheckAttribute]
        public IActionResult Users()
        {
            return View();
        }

        public async Task<JsonResult> ServerFiltering_UsersName(string text)
        {
            var result = await _accountBusiness.ServerFiltering_UsersNameAsync(text);
            return Json(result);
        }
        public async Task<JsonResult> ServerFiltering_UsersSurname(string text)
        {
            var result = await _accountBusiness.ServerFiltering_UsersSurnameAsync(text);
            return Json(result);
        }
        public async Task<JsonResult> ServerFiltering_UsersEmail(string text)
        {
            var result = await _accountBusiness.ServerFiltering_UsersEmailAsync(text);
            return Json(result);
        }
        [HttpPost]
        public IActionResult UserSearchPost(UserSearchVM model)
        {
            return PartialView("UserList", model);
        }
        [AcceptVerbs("Post")]
        public ActionResult EditingUsers_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<UserVM> users)
        {
            _authorityBusiness.CreateUsers(request, users);
            return Json(users.ToDataSourceResult(request));
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingUsers_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<UserVM> users)
        {
            _authorityBusiness.UpdateUsers(request, users);
            return Json(users.ToDataSourceResult(request));
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingUsers_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<UserVM> users)
        {
            _authorityBusiness.DeleteUsers(request, users);
            return Json(users.ToDataSourceResult(request));
        }
        public JsonResult Get_Users([DataSourceRequest] DataSourceRequest request, string name, string surname)
        {
            var result = _accountBusiness.GetUsersByFilter(request, name, surname);
            return Json(result);
        }
        #endregion
    }
}

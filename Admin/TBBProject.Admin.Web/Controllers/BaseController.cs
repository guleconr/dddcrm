using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TBBProject.Admin.Web.Helpers;
using TBBProject.Core.BusinessContracts;
using TBBProject.Core.BusinessContracts.ViewModels;
using TBBProject.Core.Common;
using TBBProject.Core.Localization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Admin.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly IAccountBusiness _accountBusiness;
        private readonly IStringLocalizer<BaseController> _localizer;

        public BaseController(IAccountBusiness accountBusiness, IStringLocalizer<BaseController> localizer)
        {
            _accountBusiness = accountBusiness;
            _localizer = localizer;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor.EndpointMetadata.Any(i => i.ToString() == "TBBProject.Admin.Web.Helpers.PageCheckAttribute"))
            {
                var userId = HttpContext.User.Identity.Name;
                if (userId != null)
                {
                    var value = HttpContext.Session.GetString("SessionUser");
                    var request = context.HttpContext.Request;
                    List<BreadCrumbHelper> slcted = new List<BreadCrumbHelper>();
                    UserVM user = new UserVM();
                    if (value != null)
                    {
                        user = JsonConvert.DeserializeObject<UserVM>(value);
                    }
                    else
                    {
                        user = _accountBusiness.GetUserByEmail(userId);
                        var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, user.Email),
                                 new Claim(ClaimTypes.Locality, user.Language),
                            };
                        var roles = user.UserRoles.Split(',').ToList();
                        foreach (var item in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, item));
                        }
                        var userIdentity = new ClaimsIdentity(claims, "login");
                        ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                        HttpContext.SignInAsync(principal);
                        var serialised = JsonConvert.SerializeObject(user);
                        HttpContext.Session.SetString("SessionUser", serialised);
                    }
                    var selectedMenu = user.Authoritys.Where(i => i.Url == request.Path.ToUriComponent()).FirstOrDefault();
                    if (selectedMenu != null)
                    {
                        ViewBag.LeftMenu = MenuHelper.GetYetkiMenu(user.Authoritys, request.Path.ToUriComponent(), _localizer);
                        ViewBag.Title = selectedMenu.Title;
                        ViewBag.Language = user.Language;
                       
                        var selectedId = selectedMenu.Id;
                        var count = 1;
                        if (selectedMenu.Url == "/")
                        {
                            var menuItem = user.Authoritys.Where(i => i.Url == request.Path.ToUriComponent()).FirstOrDefault();
                            slcted.Add(new BreadCrumbHelper { Name ="Anasayfa", Url = menuItem.Url });
                        }
                        else
                        {
                            count++;
                            var menuItem = user.Authoritys.Where(i => i.Url == request.Path.ToUriComponent()).FirstOrDefault();
                            slcted.Add(new BreadCrumbHelper { Name = menuItem.BreadCrumb, Url = menuItem.Url });
                            do
                            {
                                if (selectedId != null)
                                {
                                    var selectedparentId = user.Authoritys.Where(i => i.Id == selectedId).FirstOrDefault().ParentMenu;
                                    if (selectedparentId.HasValue)
                                    {
                                        var prntMenu = user.Authoritys.Where(i => i.Id == selectedparentId).FirstOrDefault();
                                        slcted.Add(new BreadCrumbHelper { Name = prntMenu.BreadCrumb, Url = prntMenu.Url });
                                        selectedId = selectedparentId.Value;
                                        count++;
                                    }
                                    else
                                    {
                                        selectedId = selectedparentId.HasValue ? selectedparentId.Value : 0;
                                    }
                                }
                                else
                                {
                                    selectedId = 0;
                                }
                            } while (selectedId != 0);
                            slcted.Add(new BreadCrumbHelper { Name ="Anasayfa" , Url = "/" });
                        }
                        ViewBag.MenuRenk = "gradient-45deg-indigo-blue";
                        ViewBag.BreadTexts = slcted;
                        ViewBag.BreadCount = count;
                    }
                    else
                    {
                        context.Result = new RedirectResult("/Account/AccessDenied");
                        return;
                    }
                }
            }
        }
    }
}

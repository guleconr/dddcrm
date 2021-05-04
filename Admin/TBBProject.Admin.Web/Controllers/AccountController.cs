using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using TBBProject.Core.Data;
using TBBProject.Core.Data.Domain;
using TBBProject.Core.Localization;
using TBBProject.Core.Services;
using TBBProject.Core.Web;
using TBBProject.Core.BusinessContracts;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using TBBProject.Core.BusinessContracts.ViewModels;
using TBBProject.Core.Common.Extensions;
using TBBProject.Core.Common;

namespace TBBProject.Admin.Web
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IEmailService _emailService;
        private readonly IStringLocalizer<AccountController> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _uow;
        private readonly IAccountBusiness _applicationBusiness;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private readonly IRepository<Users> _usersRepository;
        private readonly IAccountBusiness _accountBusiness;
        private readonly IAuthorityBusiness _authorityBusiness;
        private readonly EncryptionService _encryptionService;

        public AccountController(
            ILogger<AccountController> logger,
            IEmailService emailService, IAccountBusiness applicationBusiness, EncryptionService encryptionService,
            IStringLocalizer<AccountController> localizer, IHttpContextAccessor httpContextAccessor, IUnitOfWork uow, IAccountBusiness accountBusiness
            )
        {
            _uow = uow;
            _logger = logger;
            _emailService = emailService;
            _applicationBusiness = applicationBusiness;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _accountBusiness = accountBusiness;
            _usersRepository = _uow.Repository<Users>();
            _encryptionService = encryptionService;

        }

        [HttpGet]
        [AllowAnonymous]
        [Route("login")]
        [DisableAudit]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (TempData["SendMail"] != null)
                ViewBag.Message = TempData["SendMail"].ToString();
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ChangePassword")]
        [DisableAudit]
        public async Task<IActionResult> ChangePassword(string returnUrl = null)
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("MailChangePassword")]
        [DisableAudit]
        public async Task<IActionResult> MailChangePassword(string returnUrl)
        {
            ChangePasswordVM model = new ChangePasswordVM();

            if (returnUrl != null)
            {
                string email = _encryptionService.Decrypt(returnUrl);
                var user = _accountBusiness.GetUserByEmail(email);
                model.Email = user.Email;
                model.Password = user.Password;
                return View(model);
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("MailChangePassword")]
        [DisableAudit]
        public async Task<IActionResult> MailChangePassword(ChangePasswordVM model)
        {
            var user = _accountBusiness.GetUserByEmail(model.Email);
            if (user != null)
            {
                if (model.NewPassword == model.RepeatNewPassword)
                {
                    Users selectedUser = _usersRepository.TableNoTracking.Where(i => i.Id == user.Id).FirstOrDefault();
                    selectedUser.IsFirstLogin = false;
                    selectedUser.Password = _encryptionService.Encrypt(model.NewPassword);
                    _usersRepository.Update(selectedUser);
                    _uow.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("Email", LocalizationCaptions.EmailExists);
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ForgotPassword")]
        [DisableAudit]
        public async Task<IActionResult> ForgotPassword(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        [DisableAudit]
        public async Task<IActionResult> Login(UserLoginVM model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {

                var user = _accountBusiness.GetUser(model.Email, model.Password);
                #region  Login Süreci
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Email),
                         new Claim(ClaimTypes.Locality, user.Language)
                    };
                    var roles = user.UserRoles.Split(',').ToList();
                    foreach (var item in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item));
                    }
                    var userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    var serialised = JsonConvert.SerializeObject(user);
                    HttpContext.Session.SetString("SessionUser", serialised);
                    HttpContext.Session.SetString("ProjectCulture", user.Language);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    var mailuser = _accountBusiness.GetUserByEmail(model.Email);
                    if (mailuser != null)
                    {
                        Users selectedUser = _usersRepository.TableNoTracking.Where(i => i.Id == mailuser.Id).FirstOrDefault();
                        selectedUser.IncorrectLoginCount = mailuser.IncorrectLoginCount + 1;
                        _usersRepository.Update(selectedUser);
                        _uow.SaveChanges();
                        if (selectedUser.IncorrectLoginCount >= 5)
                        {
                            return RedirectToAction("ForgotPassword", "Account");
                        }
                    }
                    ModelState.AddModelError("Email", "Hatalı Email veya Şifre");
                    return View(model);
                }

                #endregion
            }
            else if (model.Password == null)
            {
                var user = _accountBusiness.GetUserByEmail(model.Email);
                if (user != null && user.IsFirstLogin == true)
                {
                    return RedirectToAction("ForgotPassword", "Account");
                }
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ForgotPassword")]
        [DisableAudit]
        public async Task<IActionResult> ForgotPassword(UserVM model, string returnUrl = null)
        {

            ViewData["ReturnUrl"] = returnUrl;
            string url = "https://localhost:44307/MailChangePassword?returnUrl=";
            var user = _accountBusiness.GetUserByEmail(model.Email);
            if (user != null)
            {
                url += _encryptionService.Encrypt(user.Email);
                TempData["SendMail"] = LocalizationCaptions.CheckEmailResetPassword;
                _emailService.SendEmailAsync(user.Email, "Email Yenileme", url);
                return RedirectToAction("Index", "Home", ViewBag);
            }
            else
            {
                TempData["SendMail"] = "Hatalı Email";
                ViewBag.Message = TempData["SendMail"].ToString();
                ModelState.AddModelError("Email", LocalizationCaptions.EmailExists);
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ChangePassword")]
        [DisableAudit]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            var user = _accountBusiness.GetUserByEmail(model.Email);
            if (user != null)
            {
                if (model.NewPassword == model.RepeatNewPassword)
                {
                    Users selectedUser = _usersRepository.TableNoTracking.Where(i => i.Id == user.Id).FirstOrDefault();
                    selectedUser.IsFirstLogin = false;
                    selectedUser.IncorrectLoginCount = 0;
                    selectedUser.Password = _encryptionService.Encrypt(model.NewPassword);
                    _usersRepository.Update(selectedUser);
                    _uow.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("Email", LocalizationCaptions.EmailExists);
                    return View(model);
                }
            }

            #region  Login Süreci
            if (user != null)
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Email),
                         new Claim(ClaimTypes.Locality, user.Language)
                    };
                var roles = user.UserRoles.Split(',').ToList();
                foreach (var item in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }
                var userIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
                var serialised = JsonConvert.SerializeObject(user);
                HttpContext.Session.SetString("SessionUser", serialised);
                HttpContext.Session.SetString("ProjectCulture", user.Language);

                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("Email", LocalizationCaptions.EmailExists);
                return View(model);
            }

            #endregion

            return View(model);
        }



        [Route("logoff")]
        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> AccessDenied()
        {
            return PartialView();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TBBProject.Core.BusinessContracts;
using TBBProject.Core.Common.Enums;
using TBBProject.Core.Data;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Admin.Web.Controllers
{
    public class RemoteValidationsController : BaseController
    {
        private readonly IUnitOfWork _uow;
        public RemoteValidationsController(IAccountBusiness accountBusiness, IStringLocalizer<BaseController> localizer, IUnitOfWork uow)
            : base(accountBusiness, localizer)
        {
            _uow = uow;


        }
        [AcceptVerbs("Get", "Post")]
        public ActionResult DateCheck(string field0)
        {
            try
            {
                Convert.ToDateTime(field0);
                return Json(true);

            }
            catch (Exception e)
            {
                return Json(false);

            }
        }

        [AcceptVerbs("Get", "Post")]
        public ActionResult DateTimeCheck(string field0, string field1)
        {
            try
            {

                if (DateTime.ParseExact(field0, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat) <=
                    DateTime.ParseExact(field1, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat))
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }

            }
            catch (Exception e)
            {
                return Json(false);

            }
        }
        [AcceptVerbs("Get", "Post")]
        public ActionResult DateTimeCheck2(string field0, string field1)
        {
            try
            {
                if (DateTime.ParseExact(field0, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat) >=
                    DateTime.ParseExact(field1, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat))
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }

            }
            catch (Exception e)
            {
                return Json(false);

            }
        }

        [AcceptVerbs("Get", "Post")]
        public ActionResult ZamanCheck(string field0, string field1)
        {
            try
            {

                if (DateTime.ParseExact(field0, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat) <=
                    DateTime.ParseExact(field1, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat))
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }

            }
            catch (Exception e)
            {
                return Json(true);

            }
        }
        [AcceptVerbs("Get", "Post")]
        public ActionResult ZamanCheck2(string field0, string field1)
        {
            try
            {
                if (DateTime.ParseExact(field0, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat) >=
                    DateTime.ParseExact(field1, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat))
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }

            }
            catch (Exception e)
            {
                return Json(true);

            }
        }

    }
}

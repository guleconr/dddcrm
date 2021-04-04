using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using TBBProject.Core.Common;
using System;
using System.IO;
using System.Text.Encodings.Web;

namespace TBBProject.Core.Web
{
    public abstract class BaseController : Controller
    {
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        //protected virtual string RenderViewComponentToString(string componentName, object arguments = null)
        //{
        //    //original implementation: https://github.com/aspnet/Mvc/blob/dev/src/Microsoft.AspNetCore.Mvc.ViewFeatures/Internal/ViewComponentResultExecutor.cs
        //    //we customized it to allow running from controllers

        //    //TODO add support for parameters (pass ViewComponent as input parameter)
        //    if (string.IsNullOrEmpty(componentName))
        //        throw new ArgumentNullException(nameof(componentName));

        //    var actionContextAccessor = HttpContext.RequestServices.GetService(typeof(IActionContextAccessor)) as IActionContextAccessor;
        //    if (actionContextAccessor == null)
        //        throw new Exception("IActionContextAccessor cannot be resolved");

        //    var context = actionContextAccessor.ActionContext;

        //    var viewComponentResult = ViewComponent(componentName, arguments);

        //    var viewData = this.ViewData;
        //    if (viewData == null)
        //    {
        //        throw new NotImplementedException();
        //        //TODO viewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState);
        //    }

        //    var tempData = this.TempData;
        //    if (tempData == null)
        //    {
        //        throw new NotImplementedException();
        //        //TODO tempData = _tempDataDictionaryFactory.GetTempData(context.HttpContext);
        //    }

        //    //using (var writer = new StringWriter())
        //    //{
        //    //    var viewContext = new ViewContext(
        //    //        context,
        //    //        NullView.Instance,
        //    //        viewData,
        //    //        tempData,
        //    //        writer,
        //    //        new HtmlHelperOptions());

        //    //    var viewComponentHelper = context.HttpContext.RequestServices.GetRequiredService<IViewComponentHelper>();
        //    //    ( viewComponentHelper as IViewContextAware )?.Contextualize(viewContext);

        //    //    var result = viewComponentResult.ViewComponentType == null ?
        //    //        viewComponentHelper.InvokeAsync(viewComponentResult.ViewComponentName, viewComponentResult.Arguments) :
        //    //        viewComponentHelper.InvokeAsync(viewComponentResult.ViewComponentType, viewComponentResult.Arguments);

        //    //    result.Result.WriteTo(writer, HtmlEncoder.Default);
        //    //    return writer.ToString();
        //    //}
        //}

        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <returns>Result</returns>
        protected virtual string RenderPartialViewToString()
        {
            return RenderPartialViewToString(null, null);
        }

        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <returns>Result</returns>
        protected virtual string RenderPartialViewToString(string viewName)
        {
            return RenderPartialViewToString(viewName, null);
        }

        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Result</returns>
        protected virtual string RenderPartialViewToString(object model)
        {
            return RenderPartialViewToString(null, model);
        }

        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <param name="model">Model</param>
        /// <returns>Result</returns>
        protected virtual string RenderPartialViewToString(string viewName, object model)
        {
            //get Razor view engine
            var razorViewEngine = TBBProjectContext.Current.Resolve<IRazorViewEngine>();

            //create action context
            var actionContext = new ActionContext(this.HttpContext, this.RouteData, this.ControllerContext.ActionDescriptor, this.ModelState);

            //set view name as action name in case if not passed
            if (string.IsNullOrEmpty(viewName))
                viewName = this.ControllerContext.ActionDescriptor.ActionName;

            //set model
            ViewData.Model = model;

            //try to get a view by the name
            var viewResult = razorViewEngine.FindView(actionContext, viewName, false);
            if (viewResult.View == null)
            {
                //or try to get a view by the path
                viewResult = razorViewEngine.GetView(null, viewName, false);
                if (viewResult.View == null)
                    throw new ArgumentNullException($"{viewName} view was not found");
            }
            using (var stringWriter = new StringWriter())
            {
                var viewContext = new ViewContext(actionContext, viewResult.View, ViewData, TempData, stringWriter, new HtmlHelperOptions());

                var t = viewResult.View.RenderAsync(viewContext);
                t.Wait();
                return stringWriter.GetStringBuilder().ToString();
            }
        }
    }
}

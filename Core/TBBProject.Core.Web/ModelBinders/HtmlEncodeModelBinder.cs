using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Ganss.XSS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace TBBProject.Core.Web
{
    public class HtmlEncodeRequestModelBinder : IModelBinder
    {
        private readonly IModelBinder _fallbackBinder;

        public HtmlEncodeRequestModelBinder(IModelBinder fallbackBinder) => _fallbackBinder = fallbackBinder ?? throw new ArgumentNullException(nameof(fallbackBinder));

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return _fallbackBinder.BindModelAsync(bindingContext);
            }

            var valueAsString = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(valueAsString))
            {
                return _fallbackBinder.BindModelAsync(bindingContext);
            }

            var sanitizer = new HtmlSanitizer();

            var result = sanitizer.Sanitize(valueAsString);

            bindingContext.Result = ModelBindingResult.Success(result);

            return Task.CompletedTask;
        }
    }

    public class HtmlEncodeRequestModelBinderProvider : IModelBinderProvider
    {
        [Obsolete]
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            //if (!context.Metadata.IsComplexType && context.Metadata.ModelType == typeof(string)) // only encode string types
            //{
            //    return new HtmlEncodeRequestModelBinder(new SimpleTypeModelBinder(context.Metadata.ModelType));
            //}

            return null;
        }
    }

    public static class MvcOptionsExtensions
    {
        public static void UseHtmlEncodeModelBinding(this MvcOptions opts)
        {
            var binderToFind = opts.ModelBinderProviders.FirstOrDefault(x => x.GetType() == typeof(SimpleTypeModelBinderProvider));

            if (binderToFind == null)
                return;

            var index = opts.ModelBinderProviders.IndexOf(binderToFind);
            opts.ModelBinderProviders.Insert(index, new HtmlEncodeRequestModelBinderProvider());
        }
    }
}

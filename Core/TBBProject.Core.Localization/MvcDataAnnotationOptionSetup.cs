using System; 
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization; 
using System.Reflection;

namespace TBBProject.Core.Localization
{
    public class MvcDataAnnotationOptionSetup : IConfigureOptions<MvcOptions>
    {
        private readonly IStringLocalizerFactory _stringLocalizerFactory;
        private readonly IValidationAttributeAdapterProvider _validationAttributeAdapterProvider;
        private readonly IOptions<MvcDataAnnotationsLocalizationOptions> _dataAnnotationLocalizationOptions;

        public MvcDataAnnotationOptionSetup(
            IValidationAttributeAdapterProvider validationAttributeAdapterProvider,
            IOptions<MvcDataAnnotationsLocalizationOptions> dataAnnotationLocalizationOptions)
        {
            _validationAttributeAdapterProvider = validationAttributeAdapterProvider ?? throw new ArgumentNullException(nameof(validationAttributeAdapterProvider));
            _dataAnnotationLocalizationOptions = dataAnnotationLocalizationOptions ?? throw new ArgumentNullException(nameof(dataAnnotationLocalizationOptions));
        }

        public MvcDataAnnotationOptionSetup(
            IValidationAttributeAdapterProvider validationAttributeAdapterProvider,
            IOptions<MvcDataAnnotationsLocalizationOptions> dataAnnotationLocalizationOptions,
            IStringLocalizerFactory stringLocalizerFactory)
            : this(validationAttributeAdapterProvider, dataAnnotationLocalizationOptions)
        {
            _stringLocalizerFactory = stringLocalizerFactory;
        }

        public void Configure(MvcOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var type = typeof(ILocalizationAssemblyMarkerInterface);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);

            var L = _stringLocalizerFactory.Create(nameof(ILocalizationAssemblyMarkerInterface), assemblyName.Name);

            options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => L[Constants.DefaultMessage.ModelBindingMessage.AttemptedValueIsInvalidAccessor, x, y]);
            options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor((x) => L[Constants.DefaultMessage.ModelBindingMessage.MissingBindRequiredValueAccessor, x]);
            options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => L[Constants.DefaultMessage.ModelBindingMessage.MissingKeyOrValueAccessor]);
            options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor((x) => L[Constants.DefaultMessage.ModelBindingMessage.UnknownValueIsInvalidAccessor, x]);
            options.ModelBindingMessageProvider.SetValueIsInvalidAccessor((x) => L[Constants.DefaultMessage.ModelBindingMessage.ValueIsInvalidAccessor]);
            options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor((x) => L[Constants.DefaultMessage.ModelBindingMessage.ValueMustBeANumberAccessor]);
            options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor((x) => L[Constants.DefaultMessage.ModelBindingMessage.ValueMustNotBeNullAccessor, x]);
        }
    }
}

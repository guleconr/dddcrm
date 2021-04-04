using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TBBProject.Core.Web.CustomAttributes
{
    public class RequiredIfOtherNullAttribute : ValidationAttribute
    {
        private string PropertyName { get; set; }

        public RequiredIfOtherNullAttribute(string propertyName) => this.PropertyName = propertyName;

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var instance = context.ObjectInstance;
            var type = instance.GetType();
            var otherProperty = type.GetProperty(PropertyName).GetValue(instance, null);
            var thisProperty = context.ObjectType.GetProperty(context.MemberName).GetValue(instance, null);
            if (( otherProperty == null && thisProperty == null ) || ( otherProperty != null && string.IsNullOrEmpty(otherProperty.ToString()) && thisProperty != null && string.IsNullOrEmpty(thisProperty.ToString()) ))
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}

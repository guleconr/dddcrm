using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace TBBProject.Core.Common.Extensions
{

    public static class EnumExtension
    {
        public static List<SelectListItem> EnumToLocalizedList<T>(IStringLocalizer localizer)
            where T : Enum
        {
            var list = Enum.GetValues(typeof(T)).Cast<T>().Select(v => new SelectListItem
            {
                Text = localizer[v.Description(v.ToString()).ToString()].Value,
                Value = ( (int)Enum.Parse(typeof(T), v.ToString()) ).ToString()
            }).ToList();

            return list;
        }
        public static List<SelectListItem> EnumToLocalizedList<T>()
           where T : Enum
        {
            var list = Enum.GetValues(typeof(T)).Cast<T>().Select(v => new SelectListItem
            {
                Text = v.Description(v.ToString()).ToString(),
                Value = ( (int)Enum.Parse(typeof(T), v.ToString()) ).ToString()
            }).ToList();

            return list;
        }
        public static List<SelectListItem> EnumToList<T>()
           where T : Enum
        {
            var list = Enum.GetValues(typeof(T)).Cast<T>().Select(v => new SelectListItem
            {
                Text = v.Description(v.ToString()).ToString(),
                Value = ( (int)Enum.Parse(typeof(T), v.ToString()) ).ToString()
            }).ToList();

            return list;
        }

        public static string Description(this Enum @enum, string name)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            var description = string.Empty;
            var fields = @enum.GetType().GetFields();
            foreach (var field in fields)
            {
                if (field.Name == name)
                    description = field.CustomAttributes != null && field.CustomAttributes.Count() > 0 ? field.CustomAttributes.Where(i => i.AttributeType.Name == "DisplayAttribute").FirstOrDefault().NamedArguments[0].TypedValue.Value.ToString() : "";
            }
            return description;
        }

    }
}

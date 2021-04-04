using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TBBProject.Core.Common;
using TBBProject.Core.Common.Enums;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Admin.Web
{
    public static class Extensions
    {
        public static string RemoveHtml(this string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
                return "";

            try
            {
                var tagsExpression = new Regex(@"</?.+?>");
                return tagsExpression.Replace(inputString, " ");
            }
            catch
            {
                return inputString;
            }
        }

        public static string RemoveHtmlToTextArea(this string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
                return "";

            return inputString.Decode().Replace("<br>", "\n").Replace("<br/>", "\n").Replace("<br />", "\n").RemoveHtml().Decode();
        }

        public static string ReplaceNewlineToBr(this string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
                return "";

            return inputString.Replace("\n", "<br />");
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

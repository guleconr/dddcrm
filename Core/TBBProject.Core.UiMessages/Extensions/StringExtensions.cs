using Microsoft.AspNetCore.Html;

namespace TBBProject.Core.UiMessages
{
    public static class StringExtensions
    {
        public static HtmlString ToHtmlString(this string str) => new HtmlString(str);
    }
}

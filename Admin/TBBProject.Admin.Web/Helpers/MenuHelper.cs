using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBBProject.Admin.Web.Controllers;
using TBBProject.Core.Common;
using TBBProject.Core.Data.Domain;
using Microsoft.Extensions.Localization;
using TBBProject.Core.Web;
using BaseController = TBBProject.Admin.Web.Controllers.BaseController;

namespace TBBProject.Admin.Web.Helpers
{
    public static class MenuHelper
    {
        public static string GetYetkiMenu(List<Authority> Yetkiler, string Url, IStringLocalizer<BaseController> _localizer)
        {
            string result = "";
            string activeStr = "";
            string openStr = "";
            foreach (var item in Yetkiler.Where(i => i.ParentMenu == null && i.IsMenu == Core.Common.Enums.YesNoEnum.Yes).OrderBy(i => i.MenuOrder))
            {
                activeStr = "";
                openStr = "";

                if (item.Url == Url)
                {
                    activeStr = "active";
                }
                if (Yetkiler.Where(i => i.ParentMenu == item.Id).Count() > 0)
                {
                    if (Yetkiler.Where(i => i.ParentMenu == item.Id).ToList().Any(i=>i.Url==Url))
                    {
                        openStr = " active open";
                    }
                    result += @"<li class='bold " + activeStr + " " + openStr + @"'>
                                <a class='collapsible-header waves-effect waves-cyan ' href='JavaScript:void(0)'>
                                    <i class='material-icons'>
                                     " + item.Icon + @"
                                        </i>
                                    <span class='menu-title' data-i18n='Dashboard'>" + _localizer[item.Text]  + @"</span>
                                </a>
                                <div class='collapsible-body'>
                                   <ul class='collapsible collapsible-sub' data-collapsible='accordion'>
            " + GetYetkiMenuRecursive(Yetkiler.Where(i => i.IsMenu == Core.Common.Enums.YesNoEnum.Yes).ToList(), item, Url, _localizer) +
                                @"
            </ul>
                                </div>
                            </li>";
                }
                else
                {
                    result += @"<li class='bold " + activeStr + @"'>
                                <a class='waves-effect waves-cyan " + activeStr + @" ' href='" + item.Url + @"'>
                                    <i class='material-icons'>
                                     " + item.Icon + @"
                                        </i>
                                    <span class='menu-title' data-i18n='ToDo'>" + _localizer[item.Text] + @"</span>
                                </a>
                            </li>";
                }
            }

            return result;
        }

        public static string GetYetkiMenuRecursive(List<Authority> Yetkiler, Authority selected, string Url, IStringLocalizer<BaseController> _localizer)
        {
            string result = "";
            string activeStr = "";
            foreach (var item in Yetkiler.Where(i => i.ParentMenu == selected.Id).OrderBy(i => i.MenuOrder))
            {
                activeStr = "";

                if (item.Url == Url)
                {
                    activeStr = "class='active'";
                }
                if (Yetkiler.Where(i => i.ParentMenu == item.Id).Count() > 0)
                {
                    result += @"<li class='bold " + activeStr + @"'>
                                <a class='collapsible-header waves-effect waves-cyan " + activeStr + @"' href='JavaScript:void(0)'>
                                     <i class='material-icons'>
                                     " + item.Icon + @"
                                        </i>
                                    <span class='menu-title' data-i18n='Dashboard'>Anasayfa</span>
                                </a>
                                <div class='collapsible-body'>
 <ul class='collapsible collapsible-sub' data-collapsible='accordion'>
            " + GetYetkiMenuRecursive(Yetkiler, item, Url,_localizer) +
                                @"
            </ul>
                                    
                                </div>
                            </li>";
                }
                else
                {
                    result += @"<li><a " + activeStr + " href='" + item.Url + @"'> <i class='material-icons'>
                                     " + item.Icon + @"
                                        </i><span data-i18n='" + item.Name + @"'>" + _localizer[item.Text] + @"</span></a>
              </li>";
                }
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using TBBProject.Core.Caching;
using TBBProject.Core.Data;
using TBBProject.Core.Data.Domain;
using TBBProject.Core.Web;

namespace TBBProject.Web.Controllers
{
    [ResponseCache(Duration = 86400)]
    public class SitemapController : Controller
    {
   
    }
}
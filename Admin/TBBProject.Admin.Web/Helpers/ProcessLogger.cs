using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using TBBProject.Core.Data;
using TBBProject.Core.Data.Domain;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ServiceStack;
using TBBProject.Core.BusinessContracts.ViewModels;

namespace TBBProject.Admin.Web.Helpers
{
    public class ProcessLoggerFilterFactory : Attribute, IFilterFactory
    {
        public int PageId { get; set; }
        public int ProcessLevel { get; set; }
        public int ReferenceTableId { get; set; }
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var filter = serviceProvider.GetService<ProcessLoggerFilter>();

            if (PageId > 0)
            {
                filter.PageId = PageId;
            }
            if (ProcessLevel > 0)
            {
                filter.ProcessLevel = ProcessLevel;
            }
            if (ReferenceTableId > 0)
            {
                filter.ReferenceTableId = ReferenceTableId;
            }
            return filter;
        }
    }
    public class ProcessLoggerFilter : IActionFilter
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<ProcessLogs> _processLogsRepository;

        public int PageId { get; set; }
        public int ProcessLevel { get; set; }
        public int ReferenceTableId { get; set; }

        public ProcessLoggerFilter(IUnitOfWork uow)
        {
            _uow = uow;
            _processLogsRepository = uow.Repository<ProcessLogs>();
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
            var postedData = JSON.stringify(context.HttpContext.Request.Form);
            List<values> ObjOrderList = JsonConvert.DeserializeObject<List<values>>(postedData);
            long.TryParse(ObjOrderList.FirstOrDefault(a => a.Key.ToUpper() == "ID")?.Value, out var ReferenceId);
            var value = context.HttpContext.Session.GetString("SessionUser");
            var request = context.HttpContext.Request;
            List<BreadCrumbHelper> slcted = new List<BreadCrumbHelper>();
            UserVM user = new UserVM();
            if (value != null)
            {
                user = JsonConvert.DeserializeObject<UserVM>(value);
            }
            var newRecord = new ProcessLogs
            {
                UserId = user.Id,
                PageUrl = context.HttpContext.Request.Path,
                PostedData = postedData,
                ProcessDate = DateTime.Now,
                ProcessLevel = ProcessLevel,
                Description = "",
                ReferenceTableId = ReferenceTableId,
                ReferenceId = ReferenceId
            };
            _processLogsRepository.Insert(newRecord);
            _uow.SaveChanges();
        }

        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {

        }
    }

    public class values
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}

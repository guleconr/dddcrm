using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TBBProject.Core.BusinessContracts;
using TBBProject.Core.BusinessContracts.ViewModels;
using TBBProject.Core.Data.Domain;
using TBBProject.Core.DataContracts;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.Business
{

    public class AcademicScheduleBusiness : IAcademicScheduleBusiness
    {
        private readonly IAcademicScheduleDataLayer _academicScheduleDataLayer;
        private readonly IMapper _mapper;
        public AcademicScheduleBusiness(IAcademicScheduleDataLayer academicScheduleDataLayer, IMapper mapper)
        {
            _academicScheduleDataLayer = academicScheduleDataLayer;
            _mapper = mapper;
        }

        public DataSourceResult GetAcademicSchedule(long Id, [DataSourceRequest] DataSourceRequest request)
        {
            var AcademicSchedule = _academicScheduleDataLayer.GetAcademicSchedule(Id).ToDataSourceResult(request);
            AcademicSchedule.Data = _mapper.Map<List<AcademicScheduleVM>>(AcademicSchedule.Data);
            return AcademicSchedule;
        }

        public void CreateAcademicSchedule(AcademicScheduleVM academicSchedule)
        {
            academicSchedule.AcademicScheduleLang = new List<AcademicScheduleLangVM>();
            AcademicScheduleLangVM lang = new AcademicScheduleLangVM();
            lang.Title = academicSchedule.Title;
            lang.Url = academicSchedule.Url;
            lang.LanguageId = academicSchedule.LanguageId;
            academicSchedule.AcademicScheduleLang.Add(lang);
            var eacademicSchedule = _mapper.Map<AcademicSchedule>(academicSchedule);
            _academicScheduleDataLayer.CreateAcademicSchedule(eacademicSchedule);

        }

        public void UpdateAcademicSchedule(AcademicScheduleVM academicSchedule)
        {

            var eacademicSchedule = _mapper.Map<AcademicSchedule>(academicSchedule);
            _academicScheduleDataLayer.UpdateAcademicSchedule(eacademicSchedule);
        }

        public void DeleteAcademicSchedule(long Id)
        {
            _academicScheduleDataLayer.DeleteAcademicSchedule(Id);
        }



        public void CreateAcademicScheduleLang(AcademicScheduleLangVM academicSchedule)
        {
            var eacademicSchedule = _mapper.Map<AcademicScheduleLang>(academicSchedule);
            _academicScheduleDataLayer.CreateAcademicScheduleLang(eacademicSchedule);

        }

        public void UpdateAcademicScheduleLang(AcademicScheduleLangVM academicSchedule)
        {
            var eacademicSchedule = _mapper.Map<AcademicScheduleLang>(academicSchedule);
            _academicScheduleDataLayer.UpdateAcademicScheduleLang(eacademicSchedule);
        }

        public void DeleteAcademicScheduleLang(long Id)
        {
            _academicScheduleDataLayer.DeleteAcademicScheduleLang(Id);
        }

        public DataSourceResult GetAcademicScheduleLangAll([DataSourceRequest] DataSourceRequest request, long academicScheduleId)
        {
            var data = _academicScheduleDataLayer.GetAcademicScheduleLangAll(academicScheduleId).ToDataSourceResult(request);
            data.Data = _mapper.Map<List<AcademicScheduleLangVM>>(data.Data);
            return data;
        }

        public AcademicScheduleLangVM GetAcademicScheduleLang(long academicScheduleId)
        {
            var data = _academicScheduleDataLayer.GetAcademicScheduleLang(academicScheduleId);
            return _mapper.Map<AcademicScheduleLangVM>(data);
        }

        public AcademicScheduleVM GetAcademicSchedule(long academicScheduleId)
        {
            var data = _academicScheduleDataLayer.GetAcademicScheduleWithId(academicScheduleId);
            return _mapper.Map<AcademicScheduleVM>(data);
        }

        public DataSourceResult GetAcademicScheduleAll([DataSourceRequest] DataSourceRequest request, string ReleaseDate, string EndDate)
        {
            var data = _academicScheduleDataLayer.GetAcademicScheduleAll();
            if (ReleaseDate!=null)
            {
                data = data.Where(i => i.StartDate >= DateTime.ParseExact(ReleaseDate, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat)
                &&i.StartDate <= DateTime.ParseExact(EndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat));
            }
            if (request.Filters.Count > 0)
            {
                var filter = ( (Kendo.Mvc.FilterDescriptor)( (Kendo.Mvc.CompositeFilterDescriptor)request.Filters[0] ).FilterDescriptors[0] ).Value;
                data = data.Where(i => i.AcademicScheduleLang.Any(i => i.Title.Contains(filter.ToString())));
                request.Filters.Clear();
            }
            var res = data.ToDataSourceResult(request);
            res.Data = _mapper.Map<List<AcademicScheduleVM>>(res.Data);
            return res;
        }
    }
}

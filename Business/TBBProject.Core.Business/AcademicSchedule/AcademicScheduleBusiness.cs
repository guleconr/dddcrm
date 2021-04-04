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

        public void DeleteAcademicScheduleLang(AcademicScheduleLangVM academicSchedule)
        {
            var eacademicSchedule = _mapper.Map<AcademicScheduleLang>(academicSchedule);
            _academicScheduleDataLayer.DeleteAcademicScheduleLang(eacademicSchedule);
        }

        public DataSourceResult GetAcademicScheduleLangAllAsync([DataSourceRequest] DataSourceRequest request, long academicScheduleId)
        {
            var data = _academicScheduleDataLayer.GetAcademicScheduleLangAllAsync(academicScheduleId).ToDataSourceResult(request);
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

        public DataSourceResult GetAcademicScheduleAllAsync([DataSourceRequest] DataSourceRequest request, DateTime ReleaseDate)
        {
            var data = _academicScheduleDataLayer.GetAcademicScheduleAllAsync();
            //if (ReleaseDate.Year > 1)
            //{
            //    data = data.Where(i => i.ReleaseDate == ReleaseDate);
            //}
            var res = data.ToDataSourceResult(request);
            res.Data = _mapper.Map<List<AcademicScheduleVM>>(res.Data);
            return res;
        }
    }
}

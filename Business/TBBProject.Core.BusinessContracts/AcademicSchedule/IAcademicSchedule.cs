using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using TBBProject.Core.BusinessContracts.ViewModels;

namespace TBBProject.Core.BusinessContracts
{
    public interface IAcademicScheduleBusiness
    {
        DataSourceResult GetAcademicScheduleAllAsync([DataSourceRequest] DataSourceRequest request, DateTime ReleaseDate);
        DataSourceResult GetAcademicScheduleLangAllAsync([DataSourceRequest] DataSourceRequest request, long academicScheduleId);

        AcademicScheduleLangVM GetAcademicScheduleLang(long academicScheduleId);
        AcademicScheduleVM GetAcademicSchedule(long academicScheduleId);



        DataSourceResult GetAcademicSchedule(long languageId, [DataSourceRequest] DataSourceRequest request);
        void CreateAcademicSchedule(AcademicScheduleVM academicSchedule);
        void UpdateAcademicSchedule(AcademicScheduleVM academicSchedule);
        void DeleteAcademicSchedule(long Id);

        void CreateAcademicScheduleLang(AcademicScheduleLangVM academicSchedule);
        void UpdateAcademicScheduleLang(AcademicScheduleLangVM academicSchedule);
        void DeleteAcademicScheduleLang(AcademicScheduleLangVM academicSchedule);
    }
}

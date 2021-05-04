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
        DataSourceResult GetAcademicScheduleAll([DataSourceRequest] DataSourceRequest request, string ReleaseDate, string EndDate);
        DataSourceResult GetAcademicScheduleLangAll([DataSourceRequest] DataSourceRequest request, long academicScheduleId);

        AcademicScheduleLangVM GetAcademicScheduleLang(long academicScheduleId);
        AcademicScheduleVM GetAcademicSchedule(long academicScheduleId);



        DataSourceResult GetAcademicSchedule(long languageId, [DataSourceRequest] DataSourceRequest request);
        void CreateAcademicSchedule(AcademicScheduleVM academicSchedule);
        void UpdateAcademicSchedule(AcademicScheduleVM academicSchedule);
        void DeleteAcademicSchedule(long Id);

        void CreateAcademicScheduleLang(AcademicScheduleLangVM academicSchedule);
        void UpdateAcademicScheduleLang(AcademicScheduleLangVM academicSchedule);
        void DeleteAcademicScheduleLang(long Id);
    }
}

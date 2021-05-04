using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.DataContracts
{
    public interface IAcademicScheduleDataLayer
    {
        IQueryable<AcademicSchedule> GetAcademicSchedule(long Id);

        void CreateAcademicSchedule(AcademicSchedule model);

        void UpdateAcademicSchedule(AcademicSchedule model);

        void DeleteAcademicSchedule(long Id);
        IQueryable<AcademicSchedule> GetAcademicScheduleAll();
        IQueryable<AcademicScheduleLang> GetAcademicScheduleLangAll(long AcademicScheduleId);

        AcademicScheduleLang GetAcademicScheduleLang(long AcademicScheduleId);

        AcademicSchedule GetAcademicScheduleWithId(long AcademicScheduleId);

        void CreateAcademicScheduleLang(AcademicScheduleLang model);

        void UpdateAcademicScheduleLang(AcademicScheduleLang model);

        void DeleteAcademicScheduleLang(long Id);
    }
}

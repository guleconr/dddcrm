using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TBBProject.Core.Data;
using TBBProject.Core.Data.Domain;
using TBBProject.Core.DataContracts;

namespace TBBProject.Core.DataLayer
{
    public class AcademicScheduleDataLayer : IAcademicScheduleDataLayer
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<AcademicSchedule> _academicScheduleRepository;
        private readonly IRepository<AcademicScheduleLang> _academicSchedulelangRepository;
        private readonly IMapper _mapper;


        public AcademicScheduleDataLayer(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _academicScheduleRepository = _uow.Repository<AcademicSchedule>();
            _academicSchedulelangRepository = _uow.Repository<AcademicScheduleLang>();
            _mapper = mapper;

        }
        public IQueryable<AcademicSchedule> GetAcademicSchedule(long Id)
        {
            var result = _academicScheduleRepository.TableNoTracking.Where(i => i.Id == Id);
            return result;
        }

        public void CreateAcademicSchedule(AcademicSchedule model)
        {
            _academicScheduleRepository.Insert(model);
            _uow.SaveChanges();
        }
        public void UpdateAcademicSchedule(AcademicSchedule model)
        {
            var result = _academicScheduleRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            result.StartDate = model.StartDate;
            result.EndDate = model.EndDate;
            _academicScheduleRepository.Update(result);
            _uow.SaveChanges();
        }
        public void DeleteAcademicSchedule(long Id)
        {
            var result = _academicScheduleRepository.TableNoTracking.Include(i => i.AcademicScheduleLang).Where(i => i.Id == Id).FirstOrDefault();
            _academicScheduleRepository.Delete(result);
            _uow.SaveChanges();
        }

        public void CreateAcademicScheduleLang(AcademicScheduleLang model)
        {
            _academicSchedulelangRepository.Insert(model);
            _uow.SaveChanges();
        }
        public void UpdateAcademicScheduleLang(AcademicScheduleLang model)
        {
            var result = _academicSchedulelangRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            result.Title = model.Title;
            result.Url = model.Url;
            result.LanguageId = model.LanguageId;
            _academicSchedulelangRepository.Update(result);
            _uow.SaveChanges();
        }
        public void DeleteAcademicScheduleLang(long Id)
        {
            var result = _academicSchedulelangRepository.TableNoTracking.Where(i => i.Id == Id).FirstOrDefault();
            _academicSchedulelangRepository.Delete(result);
            _uow.SaveChanges();
        }
        public IQueryable<AcademicSchedule> GetAcademicScheduleAll()
        {
            return _academicScheduleRepository.TableNoTracking.Include(i => i.AcademicScheduleLang).ThenInclude(i => i.Language).OrderByDescending(i => i.StartDate);
            
        }
        public IQueryable<AcademicScheduleLang> GetAcademicScheduleLangAll(long academicScheduleId)
        {
            return _academicSchedulelangRepository.TableNoTracking.Include(i => i.Language).Where(i => i.AcademicScheduleId == academicScheduleId);
        }

        public AcademicScheduleLang GetAcademicScheduleLang(long academicScheduleId)
        {
            return _academicSchedulelangRepository.TableNoTracking.Include(i => i.Language).Where(i => i.Id == academicScheduleId).FirstOrDefault();
        }

        public AcademicSchedule GetAcademicScheduleWithId(long academicScheduleId)
        {
            return _academicScheduleRepository.TableNoTracking.Where(i => i.Id == academicScheduleId).Include(i=>i.AcademicScheduleLang).ThenInclude(i=>i.Language).FirstOrDefault();
        }
    }
}

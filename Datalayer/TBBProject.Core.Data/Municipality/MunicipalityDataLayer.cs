using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TBBProject.Core.Common.Enums;
using TBBProject.Core.Data;
using TBBProject.Core.Data.Domain;
using TBBProject.Core.DataContracts;

namespace TBBProject.Core.DataLayer
{
    public class MunicipalityDataLayer : IMunicipalityDataLayer
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Municipality> _municipalityRepository;
        private readonly IRepository<District> _districtRepository;
        private readonly IMapper _mapper;


        public MunicipalityDataLayer(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _municipalityRepository = _uow.Repository<Municipality>();
            _districtRepository = _uow.Repository<District>();
            _mapper = mapper;

        }
        public IQueryable<Municipality> GetMunicipality(long Id)
        {
            var result = _municipalityRepository.TableNoTracking.Where(i => i.Id == Id);
            return result;
        }

        public void CreateMunicipality(Municipality model)
        {
            _municipalityRepository.Insert(model);
            _uow.SaveChanges();
        }
        public void UpdateMunicipality(Municipality model)
        {
            var result = _municipalityRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            result.Email = model.Email;
            result.Name = model.Name;
            result.Phone = model.Phone;
            result.Population = model.Population;
            result.MunicipalityType = model.MunicipalityType;
            _municipalityRepository.Update(result);
            _uow.SaveChanges();
        }
        public void DeleteMunicipality(long Id)
        {
            var result = _municipalityRepository.TableNoTracking.Where(i => i.Id == Id).FirstOrDefault();
            _municipalityRepository.Delete(result);
            _uow.SaveChanges();
        }

        public void AppMunicipality(long Id)
        {
            var result = _municipalityRepository.TableNoTracking.Where(i => i.Id == Id).FirstOrDefault();
            _municipalityRepository.Update(result);
            _uow.SaveChanges();
        }

        public void CreateMunicipalityDistrict(District model)
        {
            var result = _municipalityRepository.TableNoTracking.Where(i => i.Id == model.MunicipalityId).FirstOrDefault();
            if (result.MunicipalityType == MunicipalityEnum.BigCity)
                model.MunicipalityType = MunicipalityEnum.BigDistrict;
            else if(result.MunicipalityType == MunicipalityEnum.City)
                model.MunicipalityType = MunicipalityEnum.District;
            _districtRepository.Insert(model);
            _uow.SaveChanges();
        }
        public void UpdateMunicipalityDistrict(District model)
        {
            var result = _districtRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            result.Email = model.Email;
            result.Name = model.Name;
            result.Phone = model.Phone;
            result.Population = model.Population;
            result.MunicipalityType = model.MunicipalityType;
            result.MunicipalityId = result.MunicipalityId;
            _districtRepository.Update(result);
            _uow.SaveChanges();
        }
        public void DeleteMunicipalityDistrict(long Id)
        {
            var result = _districtRepository.TableNoTracking.Where(i => i.Id == Id).FirstOrDefault();
            _districtRepository.Delete(result);
            _uow.SaveChanges();
        }
        public IQueryable<Municipality> GetMunicipalityAll()
        {
            return  _municipalityRepository.TableNoTracking.OrderBy(i=>i.Name);
        }
        public IQueryable<District> GetMunicipalityDistrictAll(long municipalityId)
        {
            return _districtRepository.TableNoTracking.Where(i=>i.MunicipalityId== municipalityId).OrderBy(i => i.Name);
        }

        public District GetMunicipalityDistrict(long municipalityId)
        {
            return _districtRepository.TableNoTracking.Where(i => i.Id == municipalityId).FirstOrDefault();
        }

        public Municipality GetMunicipalityWithId(long municipalityId)
        {
            return _municipalityRepository.TableNoTracking.Where(i => i.Id == municipalityId).FirstOrDefault();
        }
    }
}

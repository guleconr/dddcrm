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
    public class LegislationAnnouncementDataLayer : ILegislationAnnouncementDataLayer
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<LegislationAnnouncement> _legislationAnnouncementRepository;
        private readonly IRepository<LegislationAnnouncementLang> _legislationAnnouncementlangRepository;
        private readonly IRepository<LegislationAnnouncementFile> _legislationAnnouncementFileRepository;

        private readonly IMapper _mapper;


        public LegislationAnnouncementDataLayer(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _legislationAnnouncementRepository = _uow.Repository<LegislationAnnouncement>();
            _legislationAnnouncementlangRepository = _uow.Repository<LegislationAnnouncementLang>();
            _legislationAnnouncementFileRepository = _uow.Repository<LegislationAnnouncementFile>();

            _mapper = mapper;

        }
        public IQueryable<LegislationAnnouncement> GetLegislationAnnouncement(long Id)
        {
            var result = _legislationAnnouncementRepository.TableNoTracking.Where(i => i.Id == Id);
            return result;
        }

        public void CreateLegislationAnnouncement(LegislationAnnouncement model)
        {
            _legislationAnnouncementRepository.Insert(model);
            _uow.SaveChanges();
        }
        public void UpdateLegislationAnnouncement(LegislationAnnouncement model)
        {
            var result = _legislationAnnouncementRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            result.ReleaseDate = model.ReleaseDate;
            result.IsRelease = model.IsRelease;
            result.EndReleaseDate = model.EndReleaseDate;
            _legislationAnnouncementRepository.Update(result);
            _uow.SaveChanges();
        }
        public void DeleteLegislationAnnouncement(long Id)
        {
            var result = _legislationAnnouncementRepository.TableNoTracking.Include(i=>i.LegislationAnnouncementLang).Where(i => i.Id == Id).FirstOrDefault();
            _legislationAnnouncementRepository.Delete(result);
            _uow.SaveChanges();
        }

        public void AppLegislationAnnouncement(long Id)
        {
            var result = _legislationAnnouncementRepository.TableNoTracking.Include(i => i.LegislationAnnouncementLang).Where(i => i.Id == Id).FirstOrDefault();
            result.ApprovalStatus = ApprovalStatus.Approval;
            _legislationAnnouncementRepository.Update(result);
            _uow.SaveChanges();
        }

        public void CreateLegislationAnnouncementLang(LegislationAnnouncementLang model)
        {
            _legislationAnnouncementlangRepository.Insert(model);
            _uow.SaveChanges();
        }
        public void UpdateLegislationAnnouncementLang(LegislationAnnouncementLang model)
        {
            var result = _legislationAnnouncementlangRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            result.Title = model.Title;
            result.Content = model.Content;
            result.Link = model.Link;
            result.LegislationAnnouncement = model.LegislationAnnouncement;
            if (model.LegislationAnnouncementFile.Count > 0)
            {
                var file = _legislationAnnouncementFileRepository.TableNoTracking.Where(i => i.LegislationAnnouncementLangId == model.Id);
                foreach (var item in file)
                {
                    _legislationAnnouncementFileRepository.Delete(item);
                }
                result.LegislationAnnouncementFile = model.LegislationAnnouncementFile;
            }
            result.LanguageId = model.LanguageId;
            _legislationAnnouncementlangRepository.Update(result);
            _uow.SaveChanges();
        }
        public void DeleteLegislationAnnouncementLang(long Id)
        {
            var result = _legislationAnnouncementlangRepository.TableNoTracking.Where(i => i.Id == Id).FirstOrDefault();
            _legislationAnnouncementlangRepository.Delete(result);
            _uow.SaveChanges();
        }
        public IQueryable<LegislationAnnouncement> GetLegislationAnnouncementAll()
        {
            return  _legislationAnnouncementRepository.TableNoTracking.Include(i=>i.LegislationAnnouncementLang).ThenInclude(i=>i.Language).OrderByDescending(i => i.ReleaseDate);
            
        }
        public IQueryable<LegislationAnnouncementLang> GetLegislationAnnouncementLangAll(long legislationAnnouncementId)
        {
            return _legislationAnnouncementlangRepository.TableNoTracking.Include(i => i.Language).Include(i => i.User).Where(i=>i.LegislationAnnouncementId== legislationAnnouncementId);
        }

        public LegislationAnnouncementLang GetLegislationAnnouncementLang(long legislationAnnouncementId)
        {
            return _legislationAnnouncementlangRepository.TableNoTracking.Include(i => i.Language).Include(i=>i.LegislationAnnouncementFile).Where(i => i.Id == legislationAnnouncementId).FirstOrDefault();
        }

        public LegislationAnnouncement GetLegislationAnnouncementWithId(long legislationAnnouncementId)
        {
            return _legislationAnnouncementRepository.TableNoTracking.Where(i => i.Id == legislationAnnouncementId).Include(i=>i.LegislationAnnouncementLang).ThenInclude(i=>i.Language).FirstOrDefault();
        }
    }
}

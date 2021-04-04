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
    public class AnnouncementDataLayer : IAnnouncementDataLayer
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Announcement> _announcementRepository;
        private readonly IRepository<AnnouncementLang> _announcementlangRepository;
        private readonly IMapper _mapper;


        public AnnouncementDataLayer(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _announcementRepository = _uow.Repository<Announcement>();
            _announcementlangRepository = _uow.Repository<AnnouncementLang>();
            _mapper = mapper;

        }
        public IQueryable<Announcement> GetAnnouncement(long Id)
        {
            var result = _announcementRepository.TableNoTracking.Where(i => i.Id == Id);
            return result;
        }

        public void CreateAnnouncement(Announcement model)
        {
            _announcementRepository.Insert(model);
            _uow.SaveChanges();
        }
        public void UpdateAnnouncement(Announcement model)
        {
            var result = _announcementRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            result.ReleaseDate = model.ReleaseDate;
            result.IsRelease = model.IsRelease;
            result.AnnouncementTypeId = model.AnnouncementTypeId;

            _announcementRepository.Update(result);
            _uow.SaveChanges();
        }
        public void DeleteAnnouncement(long Id)
        {
            var result = _announcementRepository.TableNoTracking.Include(i=>i.AnnouncementLang).Where(i => i.Id == Id).FirstOrDefault();
            _announcementRepository.Delete(result);
            _uow.SaveChanges();
        }

        public void CreateAnnouncementLang(AnnouncementLang model)
        {
            _announcementlangRepository.Insert(model);
            _uow.SaveChanges();
        }
        public void UpdateAnnouncementLang(AnnouncementLang model)
        {
            var result = _announcementlangRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            result.Title = model.Title;
            result.Content = model.Content;
            result.Image = model.Image;
            result.LanguageId = model.LanguageId;
            _announcementlangRepository.Update(result);
            _uow.SaveChanges();
        }
        public void DeleteAnnouncementLang(AnnouncementLang model)
        {
            var result = _announcementlangRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            _announcementlangRepository.Delete(result);
            _uow.SaveChanges();
        }
        public IQueryable<Announcement> GetAnnouncementAllAsync()
        {
            return  _announcementRepository.TableNoTracking.Include(i=>i.AnnouncementType).Include(i=>i.AnnouncementLang).ThenInclude(i=>i.Language);
        }
        public IQueryable<AnnouncementLang> GetAnnouncementLangAllAsync(long announcementId)
        {
            return _announcementlangRepository.TableNoTracking.Include(i => i.Language).Where(i=>i.AnnouncementId== announcementId);
        }

        public AnnouncementLang GetAnnouncementLang(long announcementId)
        {
            return _announcementlangRepository.TableNoTracking.Include(i => i.Language).Where(i => i.Id == announcementId).FirstOrDefault();
        }

        public Announcement GetAnnouncementWithId(long announcementId)
        {
            return _announcementRepository.TableNoTracking.Where(i => i.Id == announcementId).FirstOrDefault();
        }
    }
}

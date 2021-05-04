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
    public class AnnouncementDataLayer : IAnnouncementDataLayer
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Announcement> _announcementRepository;
        private readonly IRepository<AnnouncementLang> _announcementlangRepository;
        private readonly IRepository<AnnouncementFile> _announcementFileRepository;
        private readonly IMapper _mapper;


        public AnnouncementDataLayer(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _announcementRepository = _uow.Repository<Announcement>();
            _announcementlangRepository = _uow.Repository<AnnouncementLang>();
            _announcementFileRepository = _uow.Repository<AnnouncementFile>();

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
            result.EndReleaseDate = model.EndReleaseDate;
            result.IsRelease = model.IsRelease;
            result.AddSchedule = model.AddSchedule;

            _announcementRepository.Update(result);
            _uow.SaveChanges();
        }
        public void DeleteAnnouncement(long Id)
        {
            var result = _announcementRepository.TableNoTracking.Include(i => i.AnnouncementLang).Where(i => i.Id == Id).FirstOrDefault();
            _announcementRepository.Delete(result);
            _uow.SaveChanges();
        }

        public void AppAnnouncement(long Id)
        {
            var result = _announcementRepository.TableNoTracking.Include(i => i.AnnouncementLang).Where(i => i.Id == Id).FirstOrDefault();
            result.ApprovalStatus = ApprovalStatus.Approval;
            _announcementRepository.Update(result);
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
            if (model.ImageName != null)
            {
                result.Image = model.Image;
                result.ImageName = model.ImageName;
            }
            if (model.AnnouncementFile.Count > 0)
            {
                var file = _announcementFileRepository.TableNoTracking.Where(i => i.AnnouncementLangId == model.Id);
                foreach (var item in file)
                {
                    _announcementFileRepository.Delete(item);
                }
                result.AnnouncementFile = model.AnnouncementFile;
            }
            result.LanguageId = model.LanguageId;
            _announcementlangRepository.Update(result);
            _uow.SaveChanges();
        }
        public void DeleteAnnouncementLang(long Id)
        {
            var result = _announcementlangRepository.TableNoTracking.Where(i => i.Id == Id).FirstOrDefault();
            _announcementlangRepository.Delete(result);
            _uow.SaveChanges();
        }
        public IQueryable<Announcement> GetAnnouncementAll()
        {
            return _announcementRepository.TableNoTracking.Include(i => i.AnnouncementLang).ThenInclude(i => i.Language).Include(i => i.AnnouncementLang).ThenInclude(i => i.User).ThenInclude(i => i.UserRole).OrderByDescending(i=>i.ReleaseDate);
        }
        public IQueryable<AnnouncementLang> GetAnnouncementLangAll(long announcementId)
        {
            return _announcementlangRepository.TableNoTracking.Include(i => i.Language).Include(i => i.User).Where(i => i.AnnouncementId == announcementId);
        }

        public AnnouncementLang GetAnnouncementLang(long announcementId)
        {
            return _announcementlangRepository.TableNoTracking.Include(i => i.Language).Include(i => i.AnnouncementFile).Where(i => i.Id == announcementId).FirstOrDefault();
        }

        public Announcement GetAnnouncementWithId(long announcementId)
        {
            return _announcementRepository.TableNoTracking.Where(i => i.Id == announcementId).Include(i => i.AnnouncementLang).ThenInclude(i => i.Language).FirstOrDefault();
        }
    }
}

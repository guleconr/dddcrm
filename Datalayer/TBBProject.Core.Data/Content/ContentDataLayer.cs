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
    public class ContentDataLayer : IContentDataLayer
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Content> _contentRepository;
        private readonly IRepository<ContentLang> _contentlangRepository;
        private readonly IRepository<ContentGallery> _contentgalleryRepository;
        private readonly IRepository<ContentFile> _contentFileRepository;

        private readonly IMapper _mapper;


        public ContentDataLayer(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _contentRepository = _uow.Repository<Content>();
            _contentlangRepository = _uow.Repository<ContentLang>();
            _contentgalleryRepository = _uow.Repository<ContentGallery>();
            _contentFileRepository = _uow.Repository<ContentFile>();
            _mapper = mapper;

        }
        public IQueryable<Content> GetContent(long Id)
        {
            var result = _contentRepository.TableNoTracking.Where(i => i.Id == Id);
            return result;
        }

        public void CreateContent(Content model)
        {
            _contentRepository.Insert(model);
            _uow.SaveChanges();
        }
        public void UpdateContent(Content model)
        {
            var result = _contentRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            result.ReleaseDate = model.ReleaseDate;
            result.IsRelease = model.IsRelease;

            _contentRepository.Update(result);
            _uow.SaveChanges();
        }
        public void DeleteContent(long Id)
        {
            var result = _contentRepository.TableNoTracking.Include(i => i.ContentLang).Where(i => i.Id == Id).FirstOrDefault();
            _contentRepository.Delete(result);
            _uow.SaveChanges();
        }

        public void AppContent(long Id)
        {
            var result = _contentRepository.TableNoTracking.Include(i => i.ContentLang).Where(i => i.Id == Id).FirstOrDefault();
            result.ApprovalStatus = ApprovalStatus.Approval;
            _contentRepository.Update(result);
            _uow.SaveChanges();
        }

        public void CreateContentLang(ContentLang model)
        {
            _contentlangRepository.Insert(model);
            _uow.SaveChanges();
        }
        public void UpdateContentLang(ContentLang model)
        {
            var result = _contentlangRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            result.Title = model.Title;
            result.Content = model.Content;
            if (model.ImageName != null)
            {
                result.Image = model.Image;
                result.ImageName = model.ImageName;
            }
            if (model.ContentFile.Count > 0)
            {
                var file = _contentFileRepository.TableNoTracking.Where(i => i.ContentLangId == model.Id);
                foreach (var item in file)
                {
                    _contentFileRepository.Delete(item);
                }
                result.ContentFile = model.ContentFile;
            }
            if (model.ContentGallery.Count > 0)
            {
                var gallery = _contentgalleryRepository.TableNoTracking.Where(i => i.ContentLangId == model.Id);
                foreach (var item in gallery)
                {
                    _contentgalleryRepository.Delete(item);
                }
                result.ContentGallery = model.ContentGallery;
            }
            result.LanguageId = model.LanguageId;
            _contentlangRepository.Update(result);
            _uow.SaveChanges();
        }
        public void DeleteContentLang(long Id)
        {
            var result = _contentlangRepository.TableNoTracking.Where(i => i.Id == Id).FirstOrDefault();
            _contentlangRepository.Delete(result);
            _uow.SaveChanges();
        }
        public IQueryable<Content> GetContentAll()
        {
            return _contentRepository.TableNoTracking.Include(i => i.ContentLang).ThenInclude(i => i.Language).Include(i => i.ContentLang).ThenInclude(i => i.User).ThenInclude(i => i.UserRole).OrderByDescending(i => i.ReleaseDate);
        }
        public IQueryable<ContentLang> GetContentLangAll(long contentId)
        {
            return _contentlangRepository.TableNoTracking.Include(i => i.Language).Include(i => i.User).ThenInclude(i => i.UserRole).Where(i => i.ContentId == contentId);
        }

        public ContentLang GetContentLang(long contentId)
        {
            return _contentlangRepository.TableNoTracking.Include(i => i.Language).Include(i => i.ContentFile).Include(i => i.ContentGallery).Where(i => i.Id == contentId).FirstOrDefault();
        }

        public Content GetContentWithId(long contentId)
        {
            return _contentRepository.TableNoTracking.Where(i => i.Id == contentId).Include(i => i.ContentLang).ThenInclude(i => i.Language).FirstOrDefault();
        }
    }
}

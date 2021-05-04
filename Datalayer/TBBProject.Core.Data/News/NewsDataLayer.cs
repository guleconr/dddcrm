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
    public class NewsDataLayer : INewsDataLayer
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<News> _newsRepository;
        private readonly IRepository<NewsLang> _newslangRepository;
        private readonly IRepository<NewsGallery> _newsgalleryRepository;
        private readonly IMapper _mapper;


        public NewsDataLayer(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _newsRepository = _uow.Repository<News>();
            _newslangRepository = _uow.Repository<NewsLang>();
            _newsgalleryRepository = _uow.Repository<NewsGallery>();
            _mapper = mapper;

        }
        public IQueryable<News> GetNews(long Id)
        {
            var result = _newsRepository.TableNoTracking.Where(i => i.Id == Id);
            return result;
        }

        public void CreateNews(News model)
        {
            _newsRepository.Insert(model);
            _uow.SaveChanges();
        }
        public void UpdateNews(News model)
        {
            var result = _newsRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            result.ReleaseDate = model.ReleaseDate;
            result.IsRelease = model.IsRelease;

            _newsRepository.Update(result);
            _uow.SaveChanges();
        }
        public void DeleteNews(long Id)
        {
            var result = _newsRepository.TableNoTracking.Include(i => i.NewsLang).Where(i => i.Id == Id).FirstOrDefault();
            _newsRepository.Delete(result);
            _uow.SaveChanges();
        }

        public void AppNews(long Id)
        {
            var result = _newsRepository.TableNoTracking.Include(i => i.NewsLang).Where(i => i.Id == Id).FirstOrDefault();
            result.ApprovalStatus = ApprovalStatus.Approval;
            _newsRepository.Update(result);
            _uow.SaveChanges();
        }

        public void CreateNewsLang(NewsLang model)
        {
            _newslangRepository.Insert(model);
            _uow.SaveChanges();
        }
        public void UpdateNewsLang(NewsLang model)
        {
            var result = _newslangRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            result.Title = model.Title;
            result.Content = model.Content;
            result.HeadLine = model.HeadLine;
            if (model.SliderImageName != null)
            {
                result.SliderImage = model.SliderImage;
                result.SliderImageName = model.SliderImageName;
            }
            if (model.NewsImageName != null)
            {
                result.NewsImage = model.NewsImage;
                result.NewsImageName = model.NewsImageName;
            }
            if (model.NewsGallery.Count > 0)
            {
                var gallery = _newsgalleryRepository.TableNoTracking.Where(i => i.NewsLangId == model.Id);
                foreach (var item in gallery)
                {
                    _newsgalleryRepository.Delete(item);
                }
                result.NewsGallery = model.NewsGallery;
            }
            result.LanguageId = model.LanguageId;
            _newslangRepository.Update(result);
            _uow.SaveChanges();
        }
        public void DeleteNewsLang(long Id)
        {
            var result = _newslangRepository.TableNoTracking.Where(i => i.Id == Id).FirstOrDefault();
            _newslangRepository.Delete(result);
            _uow.SaveChanges();
        }
        public IQueryable<News> GetNewsAll()
        {
            return _newsRepository.TableNoTracking.Include(i => i.NewsLang).ThenInclude(i => i.Language).Include(i => i.NewsLang).ThenInclude(i => i.User).ThenInclude(i => i.UserRole).OrderByDescending(i => i.ReleaseDate);
            
        }
        public IQueryable<NewsLang> GetNewsLangAll(long newsId)
        {
            return _newslangRepository.TableNoTracking.Include(i => i.Language).Include(i => i.User).Where(i => i.NewsId == newsId);
        }

        public NewsLang GetNewsLang(long newsId)
        {
            return _newslangRepository.TableNoTracking.Include(i => i.Language).Include(i => i.NewsGallery).Where(i => i.Id == newsId).FirstOrDefault();
        }

        public News GetNewsWithId(long newsId)
        {
            return _newsRepository.TableNoTracking.Where(i => i.Id == newsId).Include(i => i.NewsLang).ThenInclude(i => i.Language).FirstOrDefault();
        }
    }
}

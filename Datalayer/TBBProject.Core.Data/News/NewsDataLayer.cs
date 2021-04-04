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
    public class NewsDataLayer : INewsDataLayer
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<News> _newsRepository;
        private readonly IRepository<NewsLang> _newslangRepository;
        private readonly IMapper _mapper;


        public NewsDataLayer(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _newsRepository = _uow.Repository<News>();
            _newslangRepository = _uow.Repository<NewsLang>();
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
            result.NewsImage = model.NewsImage;
            result.SliderImage = model.SliderImage;
            result.NewsGallery = model.NewsGallery;
            result.LanguageId = model.LanguageId;
            _newslangRepository.Update(result);
            _uow.SaveChanges();
        }
        public void DeleteNewsLang(NewsLang model)
        {
            var result = _newslangRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            _newslangRepository.Delete(result);
            _uow.SaveChanges();
        }
        public IQueryable<News> GetNewsAllAsync()
        {
            return _newsRepository.TableNoTracking.Include(i => i.NewsLang).ThenInclude(i => i.Language);
        }
        public IQueryable<NewsLang> GetNewsLangAllAsync(long newsId)
        {
            return _newslangRepository.TableNoTracking.Include(i => i.Language).Where(i => i.NewsId == newsId);
        }

        public NewsLang GetNewsLang(long newsId)
        {
            return _newslangRepository.TableNoTracking.Include(i => i.Language).Where(i => i.Id == newsId).FirstOrDefault();
        }

        public News GetNewsWithId(long newsId)
        {
            return _newsRepository.TableNoTracking.Where(i => i.Id == newsId).FirstOrDefault();
        }
    }
}

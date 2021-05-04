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
    public class VideoGalleryDataLayer : IVideoGalleryDataLayer
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<VideoGallery> _videoGalleryRepository;
        private readonly IRepository<VideoGalleryLang> _videoGallerylangRepository;
        private readonly IMapper _mapper;


        public VideoGalleryDataLayer(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _videoGalleryRepository = _uow.Repository<VideoGallery>();
            _videoGallerylangRepository = _uow.Repository<VideoGalleryLang>();
            _mapper = mapper;

        }
        public IQueryable<VideoGallery> GetVideoGallery(long Id)
        {
            var result = _videoGalleryRepository.TableNoTracking.Where(i => i.Id == Id);
            return result;
        }

        public void CreateVideoGallery(VideoGallery model)
        {
            _videoGalleryRepository.Insert(model);
            _uow.SaveChanges();
        }
        public void UpdateVideoGallery(VideoGallery model)
        {
            var result = _videoGalleryRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            result.ReleaseDate = model.ReleaseDate;

            _videoGalleryRepository.Update(result);
            _uow.SaveChanges();
        }
        public void DeleteVideoGallery(long Id)
        {
            var result = _videoGalleryRepository.TableNoTracking.Include(i => i.VideoGalleryLang).Where(i => i.Id == Id).FirstOrDefault();
            _videoGalleryRepository.Delete(result);
            _uow.SaveChanges();
        }

        public void CreateVideoGalleryLang(VideoGalleryLang model)
        {
            _videoGallerylangRepository.Insert(model);
            _uow.SaveChanges();
        }
        public void UpdateVideoGalleryLang(VideoGalleryLang model)
        {
            var result = _videoGallerylangRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            result.Title = model.Title;
            result.Url = model.Url;
            result.LanguageId = model.LanguageId;
            _videoGallerylangRepository.Update(result);
            _uow.SaveChanges();
        }
        public void DeleteVideoGalleryLang(long Id)
        {
            var result = _videoGallerylangRepository.TableNoTracking.Where(i => i.Id == Id).FirstOrDefault();
            _videoGallerylangRepository.Delete(result);
            _uow.SaveChanges();
        }
        public IQueryable<VideoGallery> GetVideoGalleryAll()
        {
            return _videoGalleryRepository.TableNoTracking.Include(i => i.VideoGalleryLang).ThenInclude(i => i.Language).OrderByDescending(i => i.ReleaseDate);
            
        }
        public IQueryable<VideoGalleryLang> GetVideoGalleryLangAll(long videoGalleryId)
        {
            return _videoGallerylangRepository.TableNoTracking.Include(i => i.Language).Where(i => i.VideoGalleryId == videoGalleryId);
        }

        public VideoGalleryLang GetVideoGalleryLang(long videoGalleryId)
        {
            return _videoGallerylangRepository.TableNoTracking.Include(i => i.Language).Where(i => i.Id == videoGalleryId).FirstOrDefault();
        }

        public VideoGallery GetVideoGalleryWithId(long videoGalleryId)
        {
            return _videoGalleryRepository.TableNoTracking.Where(i => i.Id == videoGalleryId).Include(i=>i.VideoGalleryLang).ThenInclude(i=>i.Language).FirstOrDefault();
        }
    }
}

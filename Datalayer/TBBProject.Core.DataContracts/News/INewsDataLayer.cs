using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.DataContracts
{
    public interface INewsDataLayer
    {
        IQueryable<News> GetNews(long Id);

        void CreateNews(News model);

        void UpdateNews(News model);

        void DeleteNews(long Id);
        void AppNews(long Id);

        IQueryable<News> GetNewsAll();
        IQueryable<NewsLang> GetNewsLangAll(long NewsId);

        NewsLang GetNewsLang(long NewsId);

        News GetNewsWithId(long NewsId);

        void CreateNewsLang(NewsLang model);

        void UpdateNewsLang(NewsLang model);

        void DeleteNewsLang(long Id);
    }
}

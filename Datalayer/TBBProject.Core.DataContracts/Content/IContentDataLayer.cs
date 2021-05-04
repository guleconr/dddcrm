using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.DataContracts
{
    public interface IContentDataLayer
    {
        IQueryable<Content> GetContent(long Id);

        void CreateContent(Content model);

        void UpdateContent(Content model);

        void DeleteContent(long Id);
        void AppContent(long Id);

        IQueryable<Content> GetContentAll();
        IQueryable<ContentLang> GetContentLangAll(long contentId);

        ContentLang GetContentLang(long contentId);

        Content GetContentWithId(long contentId);




        void CreateContentLang(ContentLang model);

        void UpdateContentLang(ContentLang model);

        void DeleteContentLang(long Id);
    }
}

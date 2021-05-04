using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace TBBProject.Core.Data.Domain
{
    public class AnnouncementFile: IEntity
    {
        [Key] 
        public long Id { get; set; }
        public string Name { get; set; }
        public byte[] File { get; set; }
        public long AnnouncementLangId { get; set; }
        public AnnouncementLang AnnouncementLang { get; set; }

    }
}

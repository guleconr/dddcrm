using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.Data.Domain
{
    public class Content : IEntity
    {
        [Key] 
        public long Id { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsRelease { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public List<ContentLang> ContentLang { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.Data.Domain
{
    public class Authority : IEntity
    {
        [Key] //Fix++ : need to know key fields
        public long Id { get; set; }
        public string Name { get; set; }
        public long? ParentMenu { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public AuthorityTypeEnum AuthorityType { get; set; } = AuthorityTypeEnum.Page;
        public Nullable<int> MenuOrder { get; set; }
        public YesNoEnum IsMenu { get; set; }
        public StatusEnum Status { get; set; }
        public string Title { get; set; }
        public string BreadCrumb { get; set; }


    }
}

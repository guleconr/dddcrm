using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace TBBProject.Core.Data.Domain
{
    public class RoleAuthority : IEntity
    {
        [Key] 
        [DataMember]
        public long Id { get; set; }
        public long RoleId { get; set; }
        public long AuthorityId { get; set; }
        [DataMember]
        virtual public Role Role { get; set; }
        [DataMember]
        virtual public Authority Authority { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace TBBProject.Core.Data.Domain
{
    public class UserAuthority : IEntity
    {
        [Key]
        [DataMember]
        public long Id { get; set; }
        public long UserId { get; set; }
        public long AuthorityId { get; set; }

        virtual public Users User { get; set; }
        virtual public Authority Authority { get; set; }
    }
}

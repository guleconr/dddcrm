using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace TBBProject.Core.Data.Domain
{
    public class UserRole : IEntity
    {
        [Key] 
        [DataMember]
        public long Id { get; set; }
        public long RoleId { get; set; }
        public long UserId { get; set; }
        [DataMember]
        virtual public Role Role { get; set; }
        [DataMember]
        virtual public Users User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TBBProject.Core.Data.Domain
{
    public class Role : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        [DataMember]
        virtual public ICollection<RoleAuthority> RoleAuthority { get; set; }
    }
}

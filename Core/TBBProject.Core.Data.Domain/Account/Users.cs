using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.Data.Domain
{
    public class Users : IEntity
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsFirstLogin { get; set; }
        public int IncorrectLoginCount { get; set; }
        public string Language { get; set; } = "tr-TR";
        public StatusEnum Status { get; set; }
        virtual public ICollection<UserRole> UserRole { get; set; }
        virtual public ICollection<UserAuthority> UserAuthority { get; set; }
    }
}

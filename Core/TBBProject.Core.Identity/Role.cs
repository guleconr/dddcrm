using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Web.Models
{
    public class Role : IdentityRole<long>, IEntity
    {
        public IList<UserRole> Users { get; set; } = new List<UserRole>();
    }
}

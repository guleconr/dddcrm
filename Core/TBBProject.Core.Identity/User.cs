using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Web.Models
{
    public class User : IdentityUser<long>, IEntity
    {
        public Guid UserGuid { get; set; }
        [Required(ErrorMessage = "Common.Required")]
        [StringLength(60)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Common.Required")]
        [StringLength(60)]
        public string Surname { get; set; }
        public string FullName => Name + " " + Surname;
        public DateTime Dateofbirth { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset LatestUpdatedOn { get; set; }
        [StringLength(450)]
        public string RefreshTokenHash { get; set; }
        public List<UserRole> Roles { get; set; } = new List<UserRole>();
        [StringLength(32)]
        public string Culture { get; set; }
        public virtual UserPreferences UserPreferences { get; set; } = new UserPreferences();
        public bool OnBehalfOfCompany { get; set; }
        [StringLength(100)]
        public string CompanyName { get; set; }
        public bool HasBackendAccess { get; set; }
        public DateTime LastLogin { get; set; }
        public UploadedFile Photo { get; set; }
        public string Gender { get; set; }
        public string Title { get; set; }
    }
}

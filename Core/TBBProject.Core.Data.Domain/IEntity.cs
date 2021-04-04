using System;
using System.ComponentModel.DataAnnotations;

namespace TBBProject.Core.Data.Domain
{
    public interface IEntity
    {
        [Key]
        long Id { get; set; }
    }
}

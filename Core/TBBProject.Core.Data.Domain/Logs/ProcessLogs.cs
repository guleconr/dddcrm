using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TBBProject.Core.Data.Domain
{
    public class ProcessLogs : IEntity
    {
        public ProcessLogs()
        {
        }

        public long Id { get; set; }
        public string Description { get; set; }
        public long UserId { get; set; }
        public int ProcessLevel { get; set; }
        public string PageUrl { get; set; }
        public string PostedData { get; set; }
        public DateTime ProcessDate { get; set; }
        public int ReferenceTableId { get; set; }
        public long ReferenceId { get; set; }
    }
}

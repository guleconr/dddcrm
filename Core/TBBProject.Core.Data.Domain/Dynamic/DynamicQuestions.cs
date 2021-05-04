using System;
using System.Collections.Generic;
using System.Text;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.Data.Domain
{
    public class DynamicQuestions : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime  EndDate{ get; set; }
        public long UserId { get; set; }
        public Users User { get; set; }
        public StatusEnum Status { get; set; }

        public List<DynamicQuestionsItem> DynamicMenuItems { get; set; }
        public List<DynamicQuestionsResult> DynamicMenuResults { get; set; }
    }
}

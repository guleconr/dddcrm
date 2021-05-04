using System;
using System.Collections.Generic;
using System.Text;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.Data.Domain
{
    public class DynamicQuestionsResult : IEntity
    {
        public long Id { get; set; }
        public string Result { get; set; }
        public DateTime EnterDate { get; set; }
        public long MyProperty { get; set; }
        public long DynamicQuestionsId { get; set; }
        public DynamicQuestions DynamicQuestions { get; set; }
    }
}

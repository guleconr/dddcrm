using System;
using System.Collections.Generic;
using System.Text;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.Data.Domain
{
    public class DynamicQuestionsItem : IEntity
    {
        public long Id { get; set; }
        public DynamicMenuTypeEnum MenuType { get; set; }
        public string Values { get; set; }
        public string PropertyName { get; set; }
        public long DynamicQuestionsId { get; set; }
        public DynamicQuestions DynamicQuestions { get; set; }
    }
}

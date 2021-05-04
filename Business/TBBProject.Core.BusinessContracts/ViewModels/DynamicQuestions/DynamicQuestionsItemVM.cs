using System;
using System.Collections.Generic;
using System.Text;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    public class DynamicQuestionsItemVM
    {
        public long Id { get; set; }
        public DynamicMenuTypeEnum QuestionType { get; set; }
        public string Values { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    public class FormResultModel
    {
        public bool IsSuccess { get; set; }
        public Object Data { get; set; }
        public string ResultText { get; set; }
    }
}

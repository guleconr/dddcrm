using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using TBBProject.Core.BusinessContracts.ViewModels;

namespace TBBProject.Core.BusinessContracts
{
    public interface IDynamicQuestionsBusiness
    {

        DataSourceResult ReadDynamicQuestions([DataSourceRequest] DataSourceRequest request);
    }
}

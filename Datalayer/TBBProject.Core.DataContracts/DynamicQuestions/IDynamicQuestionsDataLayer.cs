using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.DataContracts
{
    public interface IDynamicQuestionsDataLayer
    {
        IQueryable<DynamicQuestions> GetDynamicQuestions();
    }
}

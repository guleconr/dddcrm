using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using TBBProject.Core.BusinessContracts;
using TBBProject.Core.BusinessContracts.ViewModels;
using TBBProject.Core.DataContracts;

namespace TBBProject.Core.Business
{
    public class DynamicQuestionsBusiness : IDynamicQuestionsBusiness
    {
        private readonly IDynamicQuestionsDataLayer _dynamicQuestionsDataLayer;
        private readonly IMapper _mapper;
        public DynamicQuestionsBusiness(IDynamicQuestionsDataLayer dynamicQuestionsDataLayer, IMapper mapper)
        {
            _dynamicQuestionsDataLayer = dynamicQuestionsDataLayer;
            _mapper = mapper;
        }

        public DataSourceResult ReadDynamicQuestions([DataSourceRequest] DataSourceRequest request)
        {
            var dynamicQuestionsList = _dynamicQuestionsDataLayer.GetDynamicQuestions().ToDataSourceResult(request);
            dynamicQuestionsList.Data = _mapper.Map<List<DynamicQuestionsVM>>(dynamicQuestionsList.Data);
            return dynamicQuestionsList;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TBBProject.Core.Common.Enums;
using TBBProject.Core.Data;
using TBBProject.Core.Data.Domain;
using TBBProject.Core.DataContracts;

namespace TBBProject.Core.DataLayer
{
    public class DynamicQuestionsDataLayer : IDynamicQuestionsDataLayer
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<DynamicQuestions> _dynamicQuestionsRepository;
        private readonly IMapper _mapper;


        public DynamicQuestionsDataLayer(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _dynamicQuestionsRepository = _uow.Repository<DynamicQuestions>();
            _mapper = mapper;

        }
        public IQueryable<DynamicQuestions> GetDynamicQuestions()
        {
            return _dynamicQuestionsRepository.TableNoTracking;
        }

    }
}

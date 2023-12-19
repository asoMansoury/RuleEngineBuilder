using AutoMapper;
using RuleBuilderInfra.Application.PresentationModels;
using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence;
using RuleBuilderInfra.Persistence.Repositories.Contracts;
using System.Collections.Generic;

namespace RuleBuilderInfra.Application.Services.Implementations
{
    public class OperatorTypesService : BaseService, IOperatorTypesService
    {
        private readonly IOperatorTypesRepository _operatorTypesRepository;
        private readonly IFieldOperatorJoiningRepository _fieldOperatorJoiningRepository;
        public OperatorTypesService(IUnitOfWork unitOfWork, 
                                    IMapper mapper,
                                    IOperatorTypesRepository operatorTypesRepository, 
                                    IFieldOperatorJoiningRepository fieldOperatorJoiningRepository) : base(unitOfWork, mapper)
        {
            this._operatorTypesRepository = operatorTypesRepository;
            _fieldOperatorJoiningRepository = fieldOperatorJoiningRepository;
        }

        public async Task<List<OperatorTypesModel>> GetOperatorTypesAsync(string fieldTypeCode)
        {
            List<FieldOperatorJoiningEntity> allFieldOperatos =await _fieldOperatorJoiningRepository.GetFieldOperatorByCode(fieldTypeCode);
            return _mapper.Map<List<FieldOperatorJoiningEntity>,List <OperatorTypesModel>>(allFieldOperatos);
        }
    }
}

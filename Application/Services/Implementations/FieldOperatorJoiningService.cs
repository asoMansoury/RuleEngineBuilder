using AutoMapper;
using RuleBuilderInfra.Application.PresentationModels;
using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence;
using RuleBuilderInfra.Persistence.Repositories.Contracts;

namespace RuleBuilderInfra.Application.Services.Implementations
{
    public class FieldOperatorJoiningService : BaseService, IFieldOperatorJoiningService
    {
        private readonly IFieldOperatorJoiningRepository _fielOperatorJoiningRepository;
        public FieldOperatorJoiningService(IUnitOfWork unitOfWork, IMapper mapper, IFieldOperatorJoiningRepository fielOperatorJoiningRepository) : base(unitOfWork, mapper)
        {
            this._fielOperatorJoiningRepository = fielOperatorJoiningRepository;
        }

        public async Task<List<FieldOperatorJoiningModel>> GetFieldOperatorByCode(string code)
        {
            var entities = await _fielOperatorJoiningRepository.GetFieldOperatorByCode(code);
            return _mapper.Map<List<FieldOperatorJoiningEntity>, List<FieldOperatorJoiningModel>>(entities);
        }
    }
}

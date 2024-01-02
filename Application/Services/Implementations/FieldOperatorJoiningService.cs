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
        public FieldOperatorJoiningService(IUnitOfWork unitOfWork,  IFieldOperatorJoiningRepository fielOperatorJoiningRepository) : base(unitOfWork)
        {
            this._fielOperatorJoiningRepository = fielOperatorJoiningRepository;
        }

        public async Task<List<FieldOperatorJoiningEntity>> GetFieldOperatorByCode(string code)
        {
            return await _fielOperatorJoiningRepository.GetFieldOperatorByCode(code);
        }
    }
}

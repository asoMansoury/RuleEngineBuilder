using AutoMapper;
using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence;
using RuleBuilderInfra.Persistence.Repositories.Contracts;
using RuleBuilderInfra.Persistence.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Application.Services.Implementations
{
    public class RuleManagerService : BaseService, IRuleManagerService
    {
        private readonly IRuleEntityRepository _ruleEntityRepository;
        public RuleManagerService(IUnitOfWork unitOfWork, 
                                  IMapper mapper,
                                  IRuleEntityRepository ruleEntityRepository) : base(unitOfWork, mapper)
        {
            _ruleEntityRepository = ruleEntityRepository;   
        }

        public async Task AddNewRuleAsync(RuleEntity ruleEntity)
        {
            var entity = _ruleEntityRepository.AddAsync(ruleEntity);
            _unitOfWork.CommitToDatabase();
        }

        public async Task<List<RuleEntity>> GetAll()
        {
            var ruleEntities = await _ruleEntityRepository.GetAllAsync();
            return ruleEntities;
        }

        public async Task<IEnumerable<RuleEntity>> GetAllRulesAsync()
        {
            return await _ruleEntityRepository.GetAllAsync();
        }

        public async Task<RuleEntity> GetRuleEntityByIdAsync(int id)
        {
            return await _ruleEntityRepository.GetRuleEntityByIdAsync(id);
        }
    }
}


using RuleBuilderInfra.Application.PresentationModels;
using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence;
using RuleBuilderInfra.Persistence.Repositories.Contracts;

namespace RuleBuilderInfra.Application.Services.Implementations
{
    public class FieldTypesService : BaseService, IFieldTypesService
    {
        private readonly IFieldTypesRepository _fieldTypesRepository;
        public FieldTypesService(IUnitOfWork unitOfWork,IFieldTypesRepository fieldTypesRepository) : base(unitOfWork)
        {
            this._fieldTypesRepository = fieldTypesRepository;
        }

        public async Task<List<FieldTypesEntity>> GetFieldTypes()
        {
            return await this._fieldTypesRepository.GetFieldTypesAsync();
        }



        public async Task<FieldTypesEntity> GetFieldTypesByCode(string code)
        {
            return await _fieldTypesRepository.GetFieldTypesEntityByCodeAsync(code);
        }

        public async Task<List<FieldTypesEntity>> GetFieldTypesByFieldType(List<string> fieldTypes)
        {
            return await _fieldTypesRepository.GetFieldTypesByFieldType(fieldTypes);
        }

        public async Task<FieldTypesEntity> GetFieldTypesByFieldType(string fieldType)
        {
            return await _fieldTypesRepository.GetFieldTypesByFieldType(fieldType);
        }

    }
}

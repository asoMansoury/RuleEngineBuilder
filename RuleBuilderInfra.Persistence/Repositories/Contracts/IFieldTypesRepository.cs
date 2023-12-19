using RuleBuilderInfra.Domain.Entities;

namespace RuleBuilderInfra.Persistence.Repositories.Contracts
{
    public interface IFieldTypesRepository
    {
        Task<FieldTypesEntity> GetFieldTypesEntityByCodeAsync(string code);
        Task<List<FieldTypesEntity>> GetFieldTypesAsync();

        Task<List<FieldTypesEntity>> GetFieldTypesByFieldType(List<string> fieldTypes);
        Task<FieldTypesEntity> GetFieldTypesByFieldType(string fieldType);
    }
}

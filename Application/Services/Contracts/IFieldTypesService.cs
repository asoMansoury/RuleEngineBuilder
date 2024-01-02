using RuleBuilderInfra.Application.PresentationModels;
using RuleBuilderInfra.Domain.Entities;

namespace RuleBuilderInfra.Application.Services.Contracts
{
    public interface IFieldTypesService
    {
        Task<FieldTypesEntity> GetFieldTypesByCode(string code);
        Task<List<FieldTypesEntity>> GetFieldTypesByFieldType(List<string> fieldTypes);
        Task<FieldTypesEntity> GetFieldTypesByFieldType(string fieldType);

        Task<List<FieldTypesEntity>> GetFieldTypes();
    }
}

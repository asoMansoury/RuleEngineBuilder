using RuleBuilderInfra.Application.PresentationModels;

namespace RuleBuilderInfra.Application.Services.Contracts
{
    public interface IFieldTypesService
    {
        Task<FieldTypesModel> GetFieldTypesByCode(string code);
        Task<List<FieldTypesModel>> GetFieldTypesByFieldType(List<string> fieldTypes);
        Task<FieldTypesModel> GetFieldTypesByFieldType(string fieldType);

        Task<List<FieldTypesModel>> GetFieldTypes();
    }
}

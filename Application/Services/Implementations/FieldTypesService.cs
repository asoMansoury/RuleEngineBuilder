using AutoMapper;
using RuleBuilderInfra.Application.PresentationModels;
using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Persistence;
using RuleBuilderInfra.Persistence.Repositories.Contracts;

namespace RuleBuilderInfra.Application.Services.Implementations
{
    public class FieldTypesService : BaseService, IFieldTypesService
    {
        private readonly IFieldTypesRepository _fieldTypesRepository;
        public FieldTypesService(IUnitOfWork unitOfWork, IMapper mapper,IFieldTypesRepository fieldTypesRepository) : base(unitOfWork, mapper)
        {
            this._fieldTypesRepository = fieldTypesRepository;
        }

        public async Task<List<FieldTypesModel>> GetFieldTypes()
        {
            return _mapper.Map<List<FieldTypesModel>>(await this._fieldTypesRepository.GetFieldTypesAsync());
        }



        public async Task<FieldTypesModel> GetFieldTypesByCode(string code)
        {
            var foundObj =await _fieldTypesRepository.GetFieldTypesEntityByCodeAsync(code);
            return _mapper.Map<FieldTypesModel>(foundObj);
        }

        public async Task<List<FieldTypesModel>> GetFieldTypesByFieldType(List<string> fieldTypes)
        {
            var foundObjs = await _fieldTypesRepository.GetFieldTypesByFieldType(fieldTypes);
            return _mapper.Map<List<FieldTypesModel>>(foundObjs);
        }

        public async Task<FieldTypesModel> GetFieldTypesByFieldType(string fieldType)
        {
            var foundObj =await _fieldTypesRepository.GetFieldTypesByFieldType(fieldType);
            return _mapper.Map<FieldTypesModel>(foundObj);
        }

    }
}

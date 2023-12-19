using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Persistence.Repositories.Contracts;

namespace RuleBuilderInfra.Persistence.Repositories.Implementations
{
    public class FieldTypesRepository : BaseRepository, IFieldTypesRepository
    {
        public FieldTypesRepository(RuleEngineContext ruleEngineContext) : base(ruleEngineContext)
        {
        }

        public async Task<List<FieldTypesEntity>> GetFieldTypesAsync()
        {
            List<FieldTypesEntity>? entities = null;
            try
            {
                entities = base._dbContext.fieldTypesEntities.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return entities;
        }

        public async Task<List<FieldTypesEntity>> GetFieldTypesByFieldType(List<string> fieldTypes)
        {
            List<FieldTypesEntity>? entities = null;
            try
            {
                entities = base._dbContext.fieldTypesEntities.Where(z=>fieldTypes.Contains(z.FieldType)).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return entities;
        }

        public async Task<FieldTypesEntity> GetFieldTypesByFieldType(string fieldType)
        {
            FieldTypesEntity? entity = null;
            try
            {
                entity = base._dbContext.fieldTypesEntities.Single(z=> z.FieldType ==fieldType);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return entity;
        }

        public async Task<FieldTypesEntity> GetFieldTypesEntityByCodeAsync(string code)
        {
            FieldTypesEntity? entity = null;
            try
            {
                entity = base._dbContext.fieldTypesEntities.SingleOrDefault(z => z.FieldTypeCode == code);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return entity;
        }
    }
}

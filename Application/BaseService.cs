

using RuleBuilderInfra.Persistence;

namespace RuleBuilderInfra.Application
{
    public abstract class BaseService : IBaseService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public BaseService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public bool SaveChanges()
        {
            try
            {
                return _unitOfWork.CommitToDatabase();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Task SaveChangesAsync()
        {
            _unitOfWork.CommitToDatabaseAsync();
            return Task.CompletedTask;
        }
    }
}

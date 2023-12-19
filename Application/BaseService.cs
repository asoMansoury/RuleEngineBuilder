
using AutoMapper;
using RuleBuilderInfra.Persistence;

namespace RuleBuilderInfra.Application
{
    public abstract class BaseService : IBaseService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        public BaseService(IUnitOfWork unitOfWork,
                            IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
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

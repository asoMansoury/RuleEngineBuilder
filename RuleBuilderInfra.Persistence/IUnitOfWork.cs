using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Persistence
{
    public interface IUnitOfWork
    {
        bool CommitToDatabase();
        Task CommitToDatabaseAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly RuleEngineContext _dbContext;
        public UnitOfWork(RuleEngineContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public bool CommitToDatabase()
        {
            try
            {
                _dbContext.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task CommitToDatabaseAsync()
        {
            try
            {
                _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

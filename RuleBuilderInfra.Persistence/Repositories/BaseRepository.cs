﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Persistence.Repositories
{
    public abstract class BaseRepository : IBaseRepository
    {
        protected readonly RuleEngineContext _dbContext;
        public BaseRepository(RuleEngineContext ruleEngineContext)
        {
            this._dbContext = ruleEngineContext;
        }
    }

    public abstract class MainBaseRepository : IBaseRepository
    {
        protected readonly MainDatabase _dbContext;
        public MainBaseRepository(MainDatabase mainDbContext)
        {
            this._dbContext = mainDbContext;
        }
    }
}

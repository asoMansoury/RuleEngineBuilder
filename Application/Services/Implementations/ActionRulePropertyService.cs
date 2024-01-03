using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Application.Services.Implementations
{
    public class ActionRulePropertyService : BaseService, IActionRulePropertyService
    {
        public ActionRulePropertyService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

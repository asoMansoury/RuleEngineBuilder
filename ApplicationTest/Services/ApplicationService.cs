using RuleBuilderInfra.Application.PresentationModels.BuisinessEngineModels;
using RuleBuilderInfra.Application.Services.Contracts.RuleEngineer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTest.Services
{
    public class BusinessApplicationTest:BusinessEngineModel
    {
        public DateTime DateTime { get; set; }
    }
    internal class ApplicationService : BusinessEngine<BusinessApplicationTest>
    {
        protected override Task<object> PerformAsyncLogic(string outputSearchJson, BusinessApplicationTest inuptBusiness, params object[] objects)
        {
            throw new NotImplementedException();
        }
    }
}

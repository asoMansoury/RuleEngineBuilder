using Azure.Core;
using Newtonsoft.Json;
using RuleBuilderInfra.Application.PresentationModels;
using RuleBuilderInfra.Application.PresentationModels.BuisinessEngineModels;
using RuleBuilderInfra.Application.Services.Contracts;
using RuleBuilderInfra.Application.Services.Contracts.RuleEngineer;
using RuleBuilderInfra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RuleBuilderInfra.Application.Services.Implementations.RuleEngineer
{
    public class TaxCalculatorForFakeService : BusinessEngine<TaxCalculatorForFakeModel>
    {
        protected override Task<object> PerformAsyncLogic(string outputSearchJson, TaxCalculatorForFakeModel inuptBusiness, params object[] objects)
        {
            var outbputDesrilizedOnb = JsonConvert.DeserializeObject<List<FakeDataEntity>>(outputSearchJson);
            var uiParams = JsonConvert.DeserializeObject<TaxCalculatorForFakeModel>(objects[0].ToString());
            TaxlCalcModelResponse response = new TaxlCalcModelResponse();
            decimal taxPercent = (decimal)inuptBusiness.tax / 100;

            var distributerTax = uiParams.RefundAmound * taxPercent;
            var cineplexTax = uiParams.RefundAmound - distributerTax;
            var anynoymosResult = new { cineplexTax, distributerTax };
            //response.CineplexTax = request.Value - (request.Value * taxPercent);
            //response.DistributerTax = request.Value * taxPercent;
            return Task.FromResult<object>(anynoymosResult);
        }
    }

    public class TaxTest : BusinessEngine<Taxtest>
    {
        protected override Task<object> PerformAsyncLogic(string outputSearchJson, Taxtest inuptBusiness, params object[] objects)
        {
            var desrilizedObject = JsonConvert.DeserializeObject<List<FakeDataEntity>>(outputSearchJson);
            foreach (var item in desrilizedObject)
            {
                item.Distributer = inuptBusiness.name;
            }
            throw new NotImplementedException();
        }
    }
}

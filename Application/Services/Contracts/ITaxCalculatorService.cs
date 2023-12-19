using RuleBuilderInfra.Application.PresentationModels.BuisinessEngineModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Application.Services.Contracts
{
    public interface ITaxCalculatorService
    {
        Task<TaxlCalcModelResponse> TaxlCalcModelResponse(TaxlCalcModelRequest request);
    }
}

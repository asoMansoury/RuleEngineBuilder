using RuleBuilderInfra.Application.PresentationModels.BuisinessEngineModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Application.Services.Contracts.RuleEngineer
{
    public interface IBusinessEngine<T>
    {
        Task InvokeAsync(List<T> outputSearch, BusinessEngineModel inputValue);
    }
}

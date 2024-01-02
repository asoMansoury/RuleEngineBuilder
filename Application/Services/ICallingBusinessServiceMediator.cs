using RuleBuilderInfra.Application.PresentationModels.BuisinessEngineModels;
using RuleBuilderInfra.Application.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RuleBuilderInfra.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace RuleBuilderInfra.Application.Services
{
    public interface ICallingBusinessServiceMediator<TContext> where TContext: DbContext
    {
        void InvokeAsync(string categoryService, string serviceName, string outputSearchJson, string inputParamsJson, params object[] objects);
        Task<object> InvokeAsync(int ruleEntityId, params object[] objects);
    }
}


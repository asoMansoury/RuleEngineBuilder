using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Application.Services.Contracts.RuleEngineer
{
    public interface ICheckEntityIsScanned<T>
    {
        bool IsEntityTypeScannable();

        string GetTypeOfPropertyName(string propertyName);

        Dictionary<string, string> GetPropertyPairs();
    }
}

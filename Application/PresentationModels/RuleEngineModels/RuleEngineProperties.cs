using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Application.PresentationModels.RuleEngineModels
{
    public class RuleEngineProperties
    {
        public string PropertyName { get; set; } = string.Empty;
        public string FieldType { get; set; }   = string.Empty;

        public string FieldTypeCode { get; set; } = string.Empty;
    }
}

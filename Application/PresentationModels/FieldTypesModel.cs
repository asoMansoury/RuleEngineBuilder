using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Application.PresentationModels
{
    public class FieldTypesModel
    {
        public int Id { get; set; }
        public string FieldType { get; set; }
        public string FieldTypeCode { get; set; }
        public string AssemblyName { get; set; }
    }
}

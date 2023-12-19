
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Domain.Entities
{
    public class FieldOperatorJoiningEntity 
    {
        public string OperatorTypeCode { get; set; }
        public string FieldTypeCode { get; set; }

        public FieldTypesEntity FieldTypesEntity { get; set; }
        public OperatorTypesEntity OperatorTypesEntity { get; set; }

    }
}

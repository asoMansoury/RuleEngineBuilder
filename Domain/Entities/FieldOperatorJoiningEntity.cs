
using Newtonsoft.Json;
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


        [JsonIgnore]
        public FieldTypesEntity FieldTypesEntity { get; set; }
        [JsonIgnore]
        public OperatorTypesEntity OperatorTypesEntity { get; set; }

    }
}

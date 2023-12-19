using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Domain.Entities
{
    public class FieldTypesEntity
    {
        public string FieldType { get; set; }
        public string FieldTypeCode { get; set; }
        public string AssemblyName { get; set; }
        public ICollection<FieldOperatorJoiningEntity> FieldOperatorJoiningEntities { get; set; }
    }
}

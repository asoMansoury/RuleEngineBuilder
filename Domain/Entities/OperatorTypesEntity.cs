using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Domain.Entities
{
    public class OperatorTypesEntity
    {
        public string Name { get; set; }
        public string OperatorTypeCode { get; set; }
        public ICollection<FieldOperatorJoiningEntity> FieldOperatorJoiningEntities { get; set; }
    }
}

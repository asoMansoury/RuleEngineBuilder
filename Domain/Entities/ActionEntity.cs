using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Domain.Entities
{
    public class ActionEntity
    {
        public Int64 Id { get; set; }
        public string ServiceAssembly { get; set; }
        public string ServiceName { get; set; }
        public string CategoryService { get; set; }
        public bool IsActive { get; set; }

        public List<ActionPropertiesEntity> ActionPropertis { get; set; }

        [JsonIgnore]
        public List<ActionRuleEntity> actionRuleEntities { get; set; }




    }
}

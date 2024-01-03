using System.Numerics;
using System.Text.Json.Serialization;

namespace RuleBuilderInfra.Domain.Entities
{
    public class ActionPropertiesEntity
    {
        public Int64 Id { get; set; }
        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
        public bool IsActive { get; set; }
        public Int64 ActionEntityID { get; set; }
        public ActionEntity ActionEntity { get; set; }

        [JsonIgnore]
        public List<ActionRulePropertiesEntity>? actionRulePropertiesEntities { get; set; }

    }
}

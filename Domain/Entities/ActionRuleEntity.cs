using System.Text.Json.Serialization;

namespace RuleBuilderInfra.Domain.Entities
{
    public class ActionRuleEntity
    {
        public Guid Id { get; set; }
        public Int64 RuleEntityID { get; set; }
        [JsonIgnore]
        public RuleEntity RuleEntity { get; set; }


        public Int64 ActionEntityID { get; set; }
        [JsonIgnore]
        public ActionEntity ActionEntity { get; set; }
        
        [JsonIgnore]
        public List<ActionRulePropertiesEntity>? actionRulePropertiesEntities { get; set; }
    }
}

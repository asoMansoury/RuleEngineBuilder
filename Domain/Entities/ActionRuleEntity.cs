using System.Text.Json.Serialization;

namespace RuleBuilderInfra.Domain.Entities
{
    public class ActionRuleEntity
    {
        public Int64 RuleEntityID { get; set; }
        public Int64 ActionEntityID { get; set; }

        [JsonIgnore]
        public RuleEntity RuleEntity { get; set; }
        [JsonIgnore]
        public ActionEntity ActionEntity { get; set; }
    }
}

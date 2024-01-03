using System.Text.Json.Serialization;

namespace RuleBuilderInfra.Domain.Entities
{
    public class ConditionRuleEntity
    {
        [JsonIgnore]
        public int Id { get; set; }
        public String PropertyName { get; set; }
        public String Operator { get; set; }
        public String Value { get; set; }

        [JsonIgnore]
        public String? ConditionCode { get; set; }

        public int? ParentId { get; set; }
        public ConditionRuleEntity? Parent { get; set; }
        public List<ConditionRuleEntity>? Conditions { get; set; }

        public Int64? RuleEntityId { get;set; }
        public RuleEntity? RuleEntity { get; set; }
    }
}

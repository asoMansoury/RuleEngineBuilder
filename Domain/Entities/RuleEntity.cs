using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Domain.Entities
{

    public class RuleEntity
    {
        public Int64 Id { get; set; }
        public String EntityCode { get; set; }
        public String EntityCategoryCode { get; set; }


        public String RuleName { get; set; }

        public bool IsActive { get; set; }

        [NotMapped]
        public object? Value
        {
            get
            {
                if (string.IsNullOrEmpty(JsonValue))
                    return null;

                return JsonSerializer.Deserialize<object>(JsonValue);
            }
            set
            {
                if (value == null)
                {
                    JsonValue = null;
                }
                else
                {
                    JsonValue = JsonSerializer.Serialize(value);
                }
            }
        }

        [NotMapped]
        [JsonIgnore]
        [Column("JsonValue")]
        public String? JsonValue { get; set; }

        public String CategoryService { get; set; }
        public String ServiceName { get; set; }

        [NotMapped]
        public List<ConditionRuleEntity> Conditions { get; set; }

        [JsonIgnore]
        public List<ConditionRuleEntity>? ConditionRulesEntity { get; set; }

        [JsonIgnore]
        
        public String? QueryExpression { get; set; }

        [NotMapped]
        public String? RuleExpression { get; set; }

        [JsonIgnore]
        public List<ActionRuleEntity>? actionRuleEntities { get; set; }


    }
}

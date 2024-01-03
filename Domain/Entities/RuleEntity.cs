using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
        public List<ActionRuleEntity>? actionRuleEntities { get; set; }

        [JsonIgnore]
        public List<ActionRulePropertiesEntity>? actionRulePropertiesEntities { get;set; }

    }

    public class RunningSavedRule
    {
        public int Id { get; set; }

        public object? Value
        { get; set; }



    }
}

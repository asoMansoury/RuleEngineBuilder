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
        public int Id { get; set; }
        public String EntityCode { get; set; }
        public String EntityCategoryCode { get; set; }

        public String RuleName { get; set; }

        [NotMapped]
        public object Value
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
        public String JsonValue { get; set; }

        public String CategoryService { get; set; }
        public String ServiceName { get; set; }

        [NotMapped]
        public List<ConditionRuleEntity> Conditions
        {
            get
            {
                if (string.IsNullOrEmpty(ConditionJson))
                    return null;
                return JsonSerializer.Deserialize<List<ConditionRuleEntity>>(ConditionJson);
            }
            set
            {
                if (value == null)
                {
                    ConditionJson = null;
                }
                else
                {
                    ConditionJson = JsonSerializer.Serialize(value);
                }
            }
        }

        [JsonIgnore]
        public String ConditionJson { get; set; }
    }
}

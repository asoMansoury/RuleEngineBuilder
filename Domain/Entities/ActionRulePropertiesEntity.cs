﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Domain.Entities
{
    public class ActionRulePropertiesEntity
    {
        public Guid Id { get; set; }

        public string Value { get; set; }


        public Guid ActionRuleEntityId { get; set; }
        [JsonIgnore]
        public ActionRuleEntity ActionRuleEntity { get; set; }

        public Int64 ActionPropertyEntityId { get; set; }
        [JsonIgnore]
        public ActionPropertiesEntity ActionPropertyEntity { get; set; }

    }
}

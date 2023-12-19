﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Application.PresentationModels.RuleEngineModels
{
    public record ScannableEntities
    {
        public string EntityAssembly { get; init; }
        public string EntityCode { get; init; }
        public string Description { get; init; }
        public string Name { get; init; }
    }
}

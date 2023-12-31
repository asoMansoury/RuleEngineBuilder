﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Application.PresentationModels
{
    public class BusinessServiceModel
    {
        [JsonIgnore]
        public string ServiceAssembly { get; set; }
        public string ServiceName { get; set; }
        public string CategoryService { get; set; }
        public List<BuisinessServicePropertis> InputParams { get; set; }
    }
    public class BuisinessServicePropertis
    {
        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
    }
}

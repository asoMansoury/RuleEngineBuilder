﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Application.PresentationModels.BuisinessEngineModels
{
    public class TaxCalculatorForFakeModel : BusinessEngineModel
    {
        public int tax { get; set; }
    }

    public class TaxCalculatorForFakeModelResponse
    {
        public decimal CineplexTax { get; set; }
        public decimal DistributerTax { get; set; }
    }

    public class TaxCalculatorClient : BusinessEngineModel
    {
        public int EarnedAmount { get; set; }
    }

    public class Taxtest : BusinessEngineModel
    {
        public string name { get; set; }
        public string description { get; set; }

    }
}

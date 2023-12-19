using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Application.PresentationModels.BuisinessEngineModels
{
    public class TaxlCalcModelRequest
    {
        public string MovieCode { get; set; }
        public string ProvinceCode { get; set; }
        public int Value { get; set; }
        public decimal TaxValue { get; set; }

    }
    public class TaxlCalcModelResponse
    {
        public decimal DistributerTax { get; set; }
        public decimal CineplexTax { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace RuleBuilderInfra.Application.PresentationModels
{
    public class CategoryServiceModel
    {
        [JsonIgnore]
        public string AssemblyPath { get; set; }
        public string CategoryName { get; set; }
    }
}

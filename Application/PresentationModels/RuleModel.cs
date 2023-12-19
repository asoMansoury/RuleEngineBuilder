namespace RuleBuilderInfra.Application.PresentationModels
{
    public class RuleModel
    {
        public int Id { get; set; }
        public string RuleName { get; set; }
        public object Value { get; set; }
        public string FieldTypeCode { get; set; }
        public string Operator { get; set; }
    }
}

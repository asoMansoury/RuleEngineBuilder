namespace RuleBuilderInfra.Application.PresentationModels
{
    public class RunningSavedRule
    {
        public Int64 Id { get; set; }

        public object? Value
        { get; set; }
    }

    public class FakeDataModelSample
    {
        public FakeRule RuleEntity { get; set; }
        public int EarnedAmount {  get; set; }  
    }

    public class FakeRule
    {
        public string Movie { get; set; }

        public string Province { get; set; }

        public string Distributer { get; set; }
    }
}

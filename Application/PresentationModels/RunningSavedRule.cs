namespace RuleBuilderInfra.Application.PresentationModels
{
    public class RunningSavedRule
    {
        public int Id { get; set; }

        public object? Value
        { get; set; }
    }

    public class FakeDataModelSample
    {
        public int EarnedAmount { get; set; }
        public string Movie { get; set; }

        public string Province { get; set; }

        public string Distributer { get; set; }

    }
}

using RuleBuilderInfra.Application.BuisinessModel;
using RuleBuilderInfra.Domain.Entities;
using RuleBuilderInfra.Domain.ScanningEntities;

namespace RuleBuilderInfra.Application.PresentationModels
{

    [Scanning()]
    public class FakeDataModel:ScannedEntity
    {
        [ScanningAttribute(scannProperty: false)]
        public int Id { get; set; }

        public string Movie { get; set; }

        public string Province { get; set; }

        public string Distributer { get; set; }
    }
}

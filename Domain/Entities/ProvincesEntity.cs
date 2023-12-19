

using RuleBuilderInfra.Domain.ScanningEntities;

namespace RuleBuilderInfra.Domain.Entities
{
    [Scanning(name:nameof(ProvincesEntity))]
    public class ProvincesEntity : ScannedEntity
    {
        [Scanning(scannProperty: false)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}

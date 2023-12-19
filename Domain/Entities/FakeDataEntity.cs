using RuleBuilderInfra.Domain.ScanningEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Domain.Entities
{
    [Scanning(name: "Fake Entity", entityDescription: "This entity is used to reutrn list of movies in different distributers for testing data")]
    public class FakeDataEntity :ScannedEntity
    {
        private const int Constant = 1;

        [Scanning(scannProperty:false)]
        public int Id { get; set; }

        public string Movie { get; set; }

        public string Province { get; set; }

        public string Distributer { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Persistence.Repositories.Implementations.RuleEngine.Models
{
    internal class QueueEntity<T>
    {
        public Expression query { get; set; }
        public string condition { get; set; }
        public Int32 groupID { get; set; }
    }
}

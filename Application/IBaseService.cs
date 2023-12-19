using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Application
{
    public interface IBaseService
    {
        bool SaveChanges();
        Task SaveChangesAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Application
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class RuleBusinessAttribute : Attribute
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Domain.ScanningEntities
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class ScanningAttribute : Attribute
    {
        public bool ScannProperty { get; private set; }
        public string EntityDescription { get; private set; }
        public bool IsEntityDescriptionSet { get; private set; }
        public string Name { get; private set; }
        public bool IsNameSet { get; private set; }
        public ScanningAttribute(bool scannProperty = true, string entityDescription = "", string name = "")
        {
            this.ScannProperty = scannProperty;
            if (string.IsNullOrEmpty(entityDescription))
            { EntityDescription = "This field is settable."; IsEntityDescriptionSet = false; }
            else { EntityDescription = entityDescription; IsEntityDescriptionSet = true; }

            if (string.IsNullOrEmpty(name))
            { Name = "This field is settable."; IsNameSet = false; }
            else { Name = name; IsNameSet = true; }
        }
    }
}

using RuleBuilderInfra.Application.BuisinessModel;
using RuleBuilderInfra.Application.Services.Contracts.RuleEngineer;
using RuleBuilderInfra.Domain.ScanningEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Application.Services.Implementations.RuleEngineer
{
    public class CheckEntityIsScanned<T> : ICheckEntityIsScanned<T>
    {
        private Dictionary<string, string> _propertyKeyValuePairs;

        public bool IsEntityTypeScannable()
        {
            if (typeof(T).GetCustomAttributes(typeof(ScanningAttribute), true).Length > 0)
                return true;
            return false;
        }

        public Dictionary<string, string> GetPropertyPairs()
        {
            if (IsEntityTypeScannable() == false)
                throw new ArgumentException();
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            typeof(T).GetProperties().Select(item => item).ToList().ForEach(item =>
            {
                if (item.GetCustomAttributes(typeof(ScanningAttribute), true).Length == 0)
                {
                    var propertyName = item.Name;
                    var typeName = item.PropertyType.Name;
                    keyValuePairs.Add(propertyName, typeName);
                }
                else
                {
                    item.GetCustomAttributes(typeof(ScanningAttribute), true)
                         .Where(et => (et as ScanningAttribute).ScannProperty == true)
                         .ToList()
                         .ForEach(obj =>
                         {
                             keyValuePairs.Add(item.Name, item.PropertyType.Name);
                         });
                }

            });
            this._propertyKeyValuePairs = keyValuePairs;
            return keyValuePairs;
        }

        public string GetTypeOfPropertyName(string propertyName)
        {
            GetPropertyPairs();
            if (this._propertyKeyValuePairs.ContainsKey(propertyName))
                return this._propertyKeyValuePairs[propertyName];
            throw new ArgumentNullException(propertyName);
        }
    }
}

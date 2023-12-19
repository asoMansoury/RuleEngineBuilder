using Newtonsoft.Json;
using RuleBuilderInfra.Domain.ScanningEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilderInfra.Persistence
{
    public static class CommonUtility
    {
        public static TAttribute? GetAttribute<TAttribute>(this Enum value)
        {
            Type? enumType = value.GetType();
            var name = Enum.GetName(enumType, value);
            return enumType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }


        public static T? LoadJsonFile<T>(string fileName) where T : class
        {
            T? parsedObject = null;
            try
            {
                var datafilePath = Path.Combine(Directory.GetCurrentDirectory(), "FeedData", fileName);
                string jsonString = File.ReadAllText(datafilePath);
                parsedObject = JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch (Exception ex)
            {

                throw;
            }

            return parsedObject;
        }

        public static Type LoadAssemblyType(string assemblyName, string entityTypeCode)
        {
            Assembly assembly = Assembly.Load(assemblyName);
            Type entityType = assembly.GetType(entityTypeCode);
            return entityType;
        }

        public static Dictionary<string, string> GetKeyValuePairs(Type T)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            T.GetProperties().Select(item => item).ToList().ForEach(item =>
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
            return keyValuePairs;
        }


    }
}

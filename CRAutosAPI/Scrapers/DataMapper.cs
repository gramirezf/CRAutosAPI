using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CRAutosAPI.Scrapers
{
    public static class DataMapper
    {
        public static T ToObject<T>(this IDictionary<string, Object> source) where T : class, new()
        {
            var dataObject = new T();
            var dataObjectType = dataObject.GetType();

            foreach (var item in source)
            {
                dataObjectType.GetProperty(item.Key).SetValue(dataObject, item.Value, null);
            }

            return dataObject;
        }

        public static IDictionary<string, Object> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );

        }
    }
}

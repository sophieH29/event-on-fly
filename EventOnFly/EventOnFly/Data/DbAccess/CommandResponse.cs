using System;
using System.Collections.Generic;
using System.Linq;

namespace EventOnFly.Data.DbAccess
{
    public class CommandResponse
    {
        public CommandResponse()
        {
            ColumnNames = new List<string>();
            Values = new List<List<object>>();
        }

        public List<string> ColumnNames { get; }

        public IEnumerable<List<object>> Values { get; }

        public IEnumerable<T> MapToType<T>()
        {
            return Values.Select(MapValue<T>);
        }

        private T MapValue<T>(List<object> valueSet)
        {
            var res = Activator.CreateInstance<T>();
            var type = typeof(T);
            for(var i = 0; i < ColumnNames.Count; ++i)
            {
                var columnName = ColumnNames[i];
                var property = type.GetProperty(columnName);
                if (property == null) continue;
                var propType = property.GetType();
                var convertedValue = Convert.ChangeType(valueSet[i], propType);
                property.SetValue(res, convertedValue);
            }
            return res;
        }
    }
}

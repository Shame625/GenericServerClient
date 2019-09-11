using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Infrastructure.Helpers
{
    public static class Helper
    {
        public static string GetObjectProps(object obj)
        {
            var myType = obj.GetType();
            var props = new List<PropertyInfo>(myType.GetProperties());
            var fields = new List<FieldInfo>(myType.GetFields());

            var temp = "";

            foreach (var prop in props)
            {
                object propValue = prop.GetValue(obj, null);
                temp += string.Format("[{0} | {1}] ", prop.Name, propValue);
            }

            foreach(var field in fields)
            {
                object propValue = field.GetValue(obj);
                temp += string.Format("[{0} | {1}] ", field.Name, propValue);
            }

            return temp;
        }
    }
}

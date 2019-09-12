﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                temp += string.Format("[{0} | {1}] ", prop.Name, propValue.ToString());
            }

            foreach(var field in fields)
            {
                object fieldValue = field.GetValue(obj);
                if(fieldValue.GetType().IsArray)
                {
                    var values = (IEnumerable)field.GetValue(obj);

                    var tempList = new List<string>();

                    foreach(dynamic v in values)
                    {
                        tempList.Add(v.Value.ToString());
                    }
                    var stringValue = string.Join(", ", tempList);

                    temp += string.Format("[{0} | Collection: [{1}]] ", field.Name, fieldValue != null ? stringValue : "");
                }
                else
                {
                    fieldValue = field.GetValue(obj);
                    temp += string.Format("[{0} | {1}] ", field.Name, fieldValue != null ? fieldValue.ToString() : "");
                }
            }

            return temp;
        }
    }
}

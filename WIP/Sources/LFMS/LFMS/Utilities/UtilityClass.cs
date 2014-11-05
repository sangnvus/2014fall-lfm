using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LFMS.Utilities
{
    public static class UtilityClass
    {
        private static IDictionary<string, object> ConvertObjectToJson(object data, int currentLevel, List<string> exceptAttr,params List<string>[] include)
        {
            var result = new Dictionary<string, object>();

            var attrs = data.GetType().GetProperties();
            int maxLevel = include.Length;
            foreach (var attr in attrs)
            {
                var value = attr.GetValue(data, null);

                if (value != null && value.ToString().StartsWith("System.Data.Entity"))       // attribute la 1 object
                {
                    if (currentLevel < maxLevel && include[currentLevel].IndexOf(attr.Name) != -1)
                    {
                        result.Add(attr.Name, ConvertObjectToJson(value, currentLevel + 1, exceptAttr, include));
                    }
                }

                else if (value != null && value.ToString().StartsWith("System.Collections"))    // attribute la 1 list
                {
                    if (currentLevel < maxLevel && include[currentLevel].IndexOf(attr.Name) != -1)
                    {
                        var list = new List<object>();
                        foreach (var i in (IEnumerable<object>)value)
                        {
                            list.Add(ConvertObjectToJson(i, currentLevel + 1, exceptAttr, include));
                        }
                        result.Add(attr.Name, list);
                    }
                }
                else
                {
                    if (exceptAttr.IndexOf(attr.Name) != -1) break;
                    result.Add(attr.Name, attr.GetValue(data, null));
                }
            }

            return result;
        }
        public static IDictionary<string, object> ConvertDynamicObjectWithFullAttr(object data, params List<string>[] include)
        {
            return ConvertObjectToJson(data, 0,new  List<string>(),include);
        }

        public static IDictionary<string, object> ConvertDynamicObjectWithCustomAttr(object data, List<string> exceptAttr, params List<string>[] include)
        {
            return ConvertObjectToJson(data, 0, exceptAttr, include);
        }
    }

    public static class md5Encode
    {
        public static byte[] EncryptData(string data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashedBytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
            return hashedBytes;
        }
        public static string Md5Encode(string data)
        {
            return BitConverter.ToString(EncryptData(data)).Replace("-", "").ToLower();
        }
    }
}
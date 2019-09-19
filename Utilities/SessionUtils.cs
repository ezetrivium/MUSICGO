using System;
using System.Web;

namespace Utilities
{
    public static class SessionUtils
    {
        public static T Get<T>(string key)
        {
            T _tValue = default(T);
            object value = HttpContext.Current.Session[key];

            if (value != null)
            {
                try
                {
                    _tValue = ConversionUtils.Convert<T>(value);
                }
                catch
                {
                    throw new Exception("Type not valid");
                }
            }

            return _tValue;
        }

        public static string Get(string key)
        {
            return Get<string>(key);
        }

        public static void Set(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public static void Remove(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }

        public static bool HasKey(string key)
        {
            foreach (string item in HttpContext.Current.Session.Keys)
            {
                if (item == key) { return true; }
            }

            return false;
        }
    }
}
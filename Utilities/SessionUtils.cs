using System;
using System.Web;
using System.Web.SessionState;

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
                    throw new Exception(Messages.Generic_Error);
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
        public static void SetWithTimeout(string name, object value, TimeSpan expireAfter)
        {
            HttpContext.Current.Session[name] = value;
            HttpContext.Current.Session[name + "ExpDate"] = DateTime.Now.Add(expireAfter);
        }


        public static object GetWithTimeout(string name)
        {
            object value = HttpContext.Current.Session[name];
            if (value == null) return null;

            DateTime? expDate = HttpContext.Current.Session[name + "ExpDate"] as DateTime?;
            if (expDate == null) return null;

            if (expDate < DateTime.Now)
            {
                HttpContext.Current.Session.Remove(name); 
                HttpContext.Current.Session.Remove(name + "ExpDate");
                return null;
            }

            return value;
        }



    }
}
using System.Configuration;

namespace Utilities
{
    public static class ConfigurationUtils
    {
  

        public static string GetAppSettings(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static string GetConnectionString(string name)
        {
            return ConnectionString.GenerateConnectionString(name);
        }
    }
}
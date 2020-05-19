using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class GlobalValues
    {
        #region Consts

        private const string WEBCONNECTION_STRING_NAME = "ConnectionString.Name";
        private const string WEBMUSICGO_LOGGER_NAME = "MusicGO.Logger";

        private const string RECOVER_PASSWORD_URL = "RecoverPasswordURL";

        #endregion Consts

        #region Properties

        public static string WebConnectionString => ConfigurationUtils.GetConnectionString(ConnectionStringName);
        public static string WebLoggerName => ConfigurationUtils.GetAppSettings(WEBMUSICGO_LOGGER_NAME);

        public static string RecoverPasswordURL => ConfigurationUtils.GetAppSettings(RECOVER_PASSWORD_URL);

        #endregion Properties

        #region Private Properties

        private static string ConnectionStringName => ConfigurationUtils.GetAppSettings(WEBCONNECTION_STRING_NAME);

        #endregion
    }
}
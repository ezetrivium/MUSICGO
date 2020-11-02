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

        private const string FILE_MUSIC_DATA = "FileMusicData";

        private const string FILE_IMAGES_DATA = "FileImagesData";

        private const string IMAGE_STANDARD = "ImageStandard";

        private const string ACCESS_TOKEN = "AccessToken";

        private const string SUCCESS_PREFERENCE= "SuccessPreference";
        private const string PENDING_PREFERENCE = "PendingPreference";
        private const string FAILURE_PREFERENCE = "FailurePreference";

        private const string NOTIFICATION_URL = "NotificationURL";



        #endregion Consts

        #region Properties

        public static string WebConnectionString => ConfigurationUtils.GetConnectionString(ConnectionStringName);
        public static string WebLoggerName => ConfigurationUtils.GetAppSettings(WEBMUSICGO_LOGGER_NAME);

        public static string RecoverPasswordURL => ConfigurationUtils.GetAppSettings(RECOVER_PASSWORD_URL);

        public static string FileMusicData => ConfigurationUtils.GetAppSettings(FILE_MUSIC_DATA);

        public static string FileImagesData => ConfigurationUtils.GetAppSettings(FILE_IMAGES_DATA);

        public static string ImageStandard => ConfigurationUtils.GetAppSettings(IMAGE_STANDARD);


        public static string AccessToken => ConfigurationUtils.GetAppSettings(ACCESS_TOKEN);



        public static string SuccessPreference => ConfigurationUtils.GetAppSettings(SUCCESS_PREFERENCE);
        public static string PendingPreference => ConfigurationUtils.GetAppSettings(PENDING_PREFERENCE);
        public static string FailurePreference => ConfigurationUtils.GetAppSettings(FAILURE_PREFERENCE);


        public static string NotificationURL => ConfigurationUtils.GetAppSettings(NOTIFICATION_URL);


        #endregion Properties

        #region Private Properties

        private static string ConnectionStringName => ConfigurationUtils.GetAppSettings(WEBCONNECTION_STRING_NAME);

        #endregion
    }
}
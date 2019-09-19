using System.Configuration;
using System.Data.SqlClient;

namespace Utilities
{
    public static class ConnectionString
    {
        #region Consts

        /// <summary>
        /// Guarda el nombre de la key donde se encuentra el Password Encriptado "Password"
        /// </summary>
        private const string CONNECTION_STRING_PASSWORD = "ConnectionString.Password";

        #endregion Consts

        #region Properties

        private static string Password => ConfigurationUtils.GetAppSettings(CONNECTION_STRING_PASSWORD);

        #endregion Properties

        #region Public Methods

        public static string GenerateConnectionString(string name)
        {
            var builder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings[name].ConnectionString);
            //if (!builder.IntegratedSecurity)
            //{
            //    try
            //    {
            //        builder.Password = GenericEncrypter.Decrypter.DecryptTokenFromWS(Password).Split('|')[1];
            //    }
            //    catch
            //    {
            //        throw;
            //    }
            //}
            return builder.ConnectionString;
        }

        #endregion Public Methods
    }
}
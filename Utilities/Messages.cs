using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Messages
    {

        private const string USER_NOT_EXISTS = "user_not_exists";
        private const string PASSWORD_NOT_OK = "password_not_ok";
        private const string LOGIN_NOT_OK = "login_not_ok";
        private const string USER_BLOCKED = "user_blocked";
        private const string GENERIC_ERROR = "generic_error";
        private const string INVALID_LOGIN_REQUESTS = "invalid_login_request";
        private const string RESET_PASSWORD_NOT_OK = "reset_password_not_ok";
        private const string INVALID_RESET_PASSWORD_REQUEST = "invalid_reset_password_request";
        private const string SUCCESSFUL_RESET_PASSWORD_REQUEST = "successful_reset_password_request";
        private const string SUCCESSFUL_UPDATE_PASSWORD = "successful_update_password";
        private const string INVALID_UPDATE_PASSWORD_REQUEST = "invalid_update_password_request";


        public static string UserNotExists => USER_NOT_EXISTS;

        public static string PasswordNotOk => PASSWORD_NOT_OK;

        public static string LoginNotOk => LOGIN_NOT_OK;

        public static string User_Blocked => USER_BLOCKED;

        public static string Generic_Error =>GENERIC_ERROR;

        public static string InvalidLoginRequest => INVALID_LOGIN_REQUESTS;

        public static string ResetPasswordNotOk => RESET_PASSWORD_NOT_OK;

        public static string InvalidResetPasswordRequest => INVALID_RESET_PASSWORD_REQUEST;

        public static string SuccessfulResetPasswordRequest => SUCCESSFUL_RESET_PASSWORD_REQUEST;

        public static string SuccessfulUpdatePassword => SUCCESSFUL_UPDATE_PASSWORD;

        public static string InvalidUpdatePasswordRequest => INVALID_UPDATE_PASSWORD_REQUEST;
    }
}

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



        public static string UserNotExists => USER_NOT_EXISTS;

        public static string PasswordNotOk => PASSWORD_NOT_OK;

        public static string LoginNotOk => LOGIN_NOT_OK;

        public static string User_Blocked => USER_BLOCKED;

        public static string Generic_Error =>GENERIC_ERROR;
    }
}

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
        private const string USER_EXISTS = "user_exists";
        private const string PASSWORD_NOT_OK = "password_not_ok";
        private const string LOGIN_NOT_OK = "login_not_ok";
        private const string USER_BLOCKED = "user_blocked";
        private const string GENERIC_ERROR = "generic_error";
        private const string INVALID_LOGIN_REQUESTS = "invalid_login_request";
        private const string RESET_PASSWORD_NOT_OK = "reset_password_not_ok";
        private const string INVALID_RESET_PASSWORD_REQUEST = "invalid_reset_password_request";


        private const string SUCCESSFUL_RESET_PASSWORD_REQUEST = "successful_reset_password_request";
        private const string SUCCESSFUL_UPDATE_PASSWORD = "successful_update_password";
        private const string SUCCESSFUL_OPERATION_CRUD = "successful_operation_crud";

        private const string SUCCESSFUL_CONTRACT = "successful_contract";


        private const string INVALID_UPDATE_PASSWORD_REQUEST = "invalid_update_password_request";
        private const string INVALID_DATA = "invalid_data";

        private const string INVALID_DATA_USERID = "invalid_data_userid";



        private const string INVALID_IMAGE_FORMAT = "invalid_image_format";


        private const string ERROR_ADD_USER = "error_add_user";
        private const string ERROR_UPDATE_USER = "error_update_user";
        private const string ERROR_DELETE_USER = "error_delete_user";

        private const string ERROR_SUBSCRIPTION = "error_subscription";
        private const string ERROR_CONTRACT = "error_contract";

        private const string ERROR_CONTRACT_EXISTS = "error_contract_exists";

        private const string ERROR_CONTRACT_USER = "error_contract_user";

        private const string ERROR_SET_PERMISSION = "error_set_permission";


        public static string UserNotExists => USER_NOT_EXISTS;

        public static string UserExists => USER_EXISTS;

        public static string PasswordNotOk => PASSWORD_NOT_OK;

        public static string LoginNotOk => LOGIN_NOT_OK;

        public static string User_Blocked => USER_BLOCKED;

        public static string Generic_Error =>GENERIC_ERROR;

        public static string InvalidLoginRequest => INVALID_LOGIN_REQUESTS;

        public static string ResetPasswordNotOk => RESET_PASSWORD_NOT_OK;

        public static string InvalidResetPasswordRequest => INVALID_RESET_PASSWORD_REQUEST;

        public static string SuccessfulResetPasswordRequest => SUCCESSFUL_RESET_PASSWORD_REQUEST;

        public static string SuccessfulUpdatePassword => SUCCESSFUL_UPDATE_PASSWORD;

        public static string SuccessfulUperationCrud => SUCCESSFUL_OPERATION_CRUD;


        public static string SuccessfulContract => SUCCESSFUL_CONTRACT;

        public static string InvalidUpdatePasswordRequest => INVALID_UPDATE_PASSWORD_REQUEST;

        public static string InvalidData => INVALID_DATA;

        public static string InvalidDataUserID => INVALID_DATA_USERID;

        public static string InvalidImageFormat => INVALID_IMAGE_FORMAT;


        public static string ErrorAddUser => ERROR_ADD_USER;

        public static string ErrorUpdateUser => ERROR_UPDATE_USER;

        public static string ErrorDeleteUser => ERROR_DELETE_USER;

        public static string ErrorSubscription => ERROR_SUBSCRIPTION;

        public static string ErrorContract => ERROR_CONTRACT;

        public static string ErrorContractExists => ERROR_CONTRACT_EXISTS;

        public static string ErrorContractUser => ERROR_CONTRACT_USER;

        public static string ErrorSetPermission => ERROR_SET_PERMISSION;
    }
}

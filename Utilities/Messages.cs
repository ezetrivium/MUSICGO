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


        private const string SUCCESSFUL_BACKUP = "successful_backup";

        private const string SUCCESSFUL_RESTORE = "successful_restore";


        private const string INVALID_UPDATE_PASSWORD_REQUEST = "invalid_update_password_request";
        private const string INVALID_DATA = "invalid_data";

        private const string INVALID_DATA_USERID = "invalid_data_userid";

        private const string INVALID_DATA_SONGID = "invalid_data_songid";

        private const string INVALID_SONGFILE_REQUEST = "invalid_songfile_request";

        private const string INVALID_IMAGE_FORMAT = "invalid_image_format";

        private const string INVALID_FILE_FORMAT = "invalid_file_format";

        private const string INVALID_IMAGE_REQUEST = "invalid_image_request";

        private const string ERROR_UPDATE_PASSWORD = "error_update_password";

        private const string ERROR_ADD_USER = "error_add_user";
        private const string ERROR_UPDATE_USER = "error_update_user";
        private const string ERROR_DELETE_USER = "error_delete_user";

        private const string ERROR_SUBSCRIPTION = "error_subscription";
        private const string ERROR_CONTRACT = "error_contract";

        private const string ERROR_CONTRACT_EXISTS = "error_contract_exists";

        private const string ERROR_CONTRACT_USER = "error_contract_user";

        private const string ERROR_SET_PERMISSION = "error_set_permission";


        private const string ERROR_ADD_LANGUAGE = "error_add_language";
        private const string ERROR_UPDATE_LANGUAGE = "error_update_language";
        private const string ERROR_DELETE_LANGUAGE = "error_delete_language";


        private const string ERROR_ADD_PERMISSION = "error_add_permission";
        private const string ERROR_UPDATE_PERMISSION = "error_update_permission";
        private const string ERROR_ADD_PERMISSIONGROUP = "error_add_permissiongroup";


        private const string ERROR_ADD_CLAIM = "error_add_claim";
        private const string ERROR_UPDATE_CLAIM = "error_update_claim";


        private const string ERROR_ADD_DICTIONARY = "error_add_dictionary";
        private const string ERROR_UPDATE_DICTIONARY = "error_update_dictionary";


        private const string ERROR_ADD_SONG = "error_add_song";
        private const string ERROR_UPDATE_SONG = "error_update_song";
        private const string ERROR_DELETE_SONG = "error_delete_song";


        private const string ERROR_ADD_ALBUM = "error_add_album";
        private const string ERROR_UPDATE_ALBUM = "error_update_album";
        private const string ERROR_DELETE_ALBUM = "error_delete_album";


        private const string ERROR_CODE_LANGUAGE = "error_code_language";


        private const string ERROR_BACKUP_VERIFY = "error_backup_verify";

        private const string ERROR_BACKUP = "error_backup";


        private const string ERROR_RESTORE = "error_restore";


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


        public static string SuccessfulBackup => SUCCESSFUL_BACKUP;


        public static string SuccessfulRestore => SUCCESSFUL_RESTORE;

        public static string InvalidUpdatePasswordRequest => INVALID_UPDATE_PASSWORD_REQUEST;

        public static string InvalidData => INVALID_DATA;

        public static string InvalidDataUserID => INVALID_DATA_USERID;

        public static string InvalidDataSongID => INVALID_DATA_SONGID;

        public static string InvalidImageFormat => INVALID_IMAGE_FORMAT;

        public static string InvalidImageRequest => INVALID_IMAGE_REQUEST;

        public static string InvalidSongFileRequest => INVALID_SONGFILE_REQUEST;

        public static string InvalidFileFormat => INVALID_FILE_FORMAT;


        public static string ErrorUpdatePassword => ERROR_UPDATE_PASSWORD;
        public static string ErrorAddUser => ERROR_ADD_USER;

        public static string ErrorUpdateUser => ERROR_UPDATE_USER;

        public static string ErrorDeleteUser => ERROR_DELETE_USER;

        public static string ErrorAddSong => ERROR_ADD_SONG;

        public static string ErrorUpdateSong => ERROR_UPDATE_SONG;

        public static string ErrorDeleteSong => ERROR_DELETE_SONG;

        public static string ErrorAddAlbum => ERROR_ADD_ALBUM;

        public static string ErrorUpdateAlbum => ERROR_UPDATE_ALBUM;

        public static string ErrorDeleteAlbum => ERROR_DELETE_ALBUM;

        public static string ErrorSubscription => ERROR_SUBSCRIPTION;

        public static string ErrorContract => ERROR_CONTRACT;

        public static string ErrorContractExists => ERROR_CONTRACT_EXISTS;

        public static string ErrorContractUser => ERROR_CONTRACT_USER;

        public static string ErrorSetPermission => ERROR_SET_PERMISSION;


        public static string ErrorAddLanguage => ERROR_ADD_LANGUAGE;


        public static string ErrorUpdateLanguage => ERROR_UPDATE_LANGUAGE;


        public static string ErrorDeleteLanguage => ERROR_DELETE_LANGUAGE;


        public static string ErrorAddPermission => ERROR_ADD_PERMISSION;

        public static string ErrorAddPermissionGroup => ERROR_ADD_PERMISSIONGROUP;

        public static string ErrorUpdatePermission => ERROR_UPDATE_PERMISSION;


        public static string ErrorAddClaim => ERROR_ADD_CLAIM;


        public static string ErrorUpdateClaim => ERROR_UPDATE_CLAIM;


        public static string ErrorAddDictionary => ERROR_ADD_DICTIONARY;


        public static string ErrorUpdateDictionary => ERROR_UPDATE_DICTIONARY;


        public static string ErrorCodeLanguage => ERROR_CODE_LANGUAGE;


        public static string ErrorBackupVerify => ERROR_BACKUP_VERIFY;


        public static string ErrorBackup => ERROR_BACKUP;



        public static string ErrorRestore => ERROR_RESTORE;
    }
}

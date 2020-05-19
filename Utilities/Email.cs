using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Email
    {
        #region Consts

        private const string EMAIL_USER = "EmailAdmin";
        private const string EMAIL_PASSWORD = "PasswordEmailAdmin";
        private const string SUBJECT_RESET_PASSWORD = "SubjectResetPassword";
        private const string BODY_RESET_PASSWORD = "BodyResetPassword";
        private const string SMTP_SERVER= "smtp.gmail.com";

        private const int SMTP_PORT = 465;

        #endregion Consts

        #region Properties

        public static string EmailUser => ConfigurationUtils.GetAppSettings(EMAIL_USER);
        public static string EmailPassword => ConfigurationUtils.GetAppSettings(EMAIL_PASSWORD);

        public static string SubjectResetPassword => ConfigurationUtils.GetAppSettings(SUBJECT_RESET_PASSWORD);

        public static string BodyResetPassword => ConfigurationUtils.GetAppSettings(BODY_RESET_PASSWORD);

        #endregion Properties

        public static async Task SendAsyncEmail(string email, string subject, string body)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress
                                    ("MusicGO",
                                     EmailUser
                                     ));
            mimeMessage.To.Add(new MailboxAddress
                                     ("UserDestination",
                                     email
                                     ));
            mimeMessage.Subject = subject; //Subject  
            mimeMessage.Body = new TextPart("plain")
            {
                Text = body
            };

            //mimeMessage.Body = new TextPart("html")
            //{
            //    Text = "<!DOCTYPE html>\r\n\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head runat=\"server\">\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>\r\n    <title>Aviso De Turno</title>\r\n\r\n    <style>\r\n\r\n    body {\r\n        font: 14px/24px 'Frutiger', 'Roboto', Arial, Helvetica, sans-serif;\r\n    }\r\n\r\n\r\n\r\n    .modal {\r\n        position: fixed;\r\n        top: 0;\r\n        right: 0;\r\n        bottom: 0;\r\n        left: 0;\r\n        z-index: 1050;\r\n    }\r\n\r\n    .modal-body{\r\n        margin: 20px\r\n        \r\n    }\r\n\r\n    .modal-backdrop{\r\n        opacity: .5;\r\n        position: fixed;\r\n        top: 0;\r\n        right: 0;\r\n        bottom: 0;\r\n        left: 0;\r\n        z-index: 1040;\r\n        background-color: #000;\r\n    }\r\n\r\n    .btn-danger {\r\n        color: #fff!important;\r\n        background-color: #d9534f;\r\n    }\r\n    .btn {\r\n        display: inline-block;\r\n        margin-bottom: 0;\r\n        font-size: 14px;\r\n        font-weight: 400;\r\n        line-height: 1.42857143;\r\n        text-align: center;\r\n        white-space: nowrap;\r\n        vertical-align: middle;\r\n        cursor: pointer;       \r\n        border: 1px solid transparent;\r\n        border-radius: 10px;\r\n        text-decoration: none;\r\n        color:#fff!important;\r\n        }\r\n\r\n        \r\n\r\n\r\n        .modal-confirm .modal-header {\r\n            border-bottom: none;\r\n            position: relative;\r\n            background-color: #0E4482;\r\n            text-align: center;\r\n        }\r\n\r\n        .modal-confirm {\r\n            color: #636363;\r\n            width: 100%;\r\n            margin-top:8%;\r\n            max-width: 800px;\r\n            box-shadow: 0 0px 15px rgba(0,0,0,.5);\r\n        }\r\n\r\n        .modal-confirm .modal-content {\r\n            background-color: #fff;\r\n            padding: 20px;\r\n            border-radius: 5px 5px 0 0;\r\n            border: 1px #000;\r\n            border-style: solid;\r\n            \r\n        }\r\n\r\n\r\n        .modal-confirm .modal-buttons {\r\n            border: none;\r\n            text-align: center;\r\n            border-radius: 5px;\r\n            font-size: 13px;\r\n            padding: 10px 0px 10px;\r\n        }\r\n\r\n        .modal-confirm .btn {\r\n\r\n            border-radius: 4px;\r\n            line-height: normal;\r\n            min-width: 100px;\r\n            border: none;\r\n            border-radius: 3px;\r\n            margin: 5px 5px;\r\n            width:40%;\r\n            padding: 10px 0;\r\n        }\r\n\r\n\r\n            \r\n\r\n        .copyright, .copyright a {\r\n            color: #8d8d8d;\r\n        }\r\n        .copyright {\r\n            padding-top: 20px;\r\n            padding-bottom: 20px;\r\n            font-size: 12px;\r\n            text-transform: uppercase;\r\n            background-color: #262626;\r\n            padding-left: 20px;\r\n            border-radius: 0 0 5px 5px;\r\n            border: none;\r\n            \r\n        }\r\n\r\n        .copyright span, .copyright a:hover {\r\n            color: #4db1e2;\r\n        }\r\n \r\n\r\n       \r\n\r\n@media (max-width: 980px){\r\n\r\n    .modal-confirm {\r\n            color: #636363;\r\n            margin-top:20%;\r\n        }\r\n\r\n    .modal-confirm .btn {\r\n    \r\n        width:100%;\r\n    }\r\n\r\n\r\n    .copyright {\r\n            text-align: center;\r\n        \r\n            \r\n        }\r\n}\r\n\r\n\r\n.main-header {\r\n    background-color: #0E4482;\r\n}\r\n\r\n@media (min-width: 768px){\r\n    .modal-dialog {\r\n        margin: 30px auto;\r\n    }\r\n}\r\n    </style>\r\n</head>\r\n    \r\n<body>\r\n  \r\n        <div>\r\n          <div id=\"cancelModal\" class=\"modal fade in\" style=\"display: block; padding-left: 17px;\">\r\n\t           <div class=\"modal-dialog modal-confirm\">\r\n\t\t           <div class=\"modal-content\">\r\n\t\t\t             <div class=\"modal-header\">\t\t\t                \r\n                                <img src=\"https://www.hbritanicoweb.com.ar/images/logo.png\">\t\t\t               \t\t\t                         \r\n\t\t\t             </div>\r\n\t\t\t             <div class=\"modal-body\">                            \r\n                            test\r\n\t\t\t             </div>\r\n\t\t\t             <div class=\"modal-buttons\">\r\n                             <a role = \"button\" class=\"btn\" style =\"background-color: red;\" href =\"http://localhost:56049/Notification.aspx?accion=0&citacion=test&institucion=est\">Cancelar turno</a><a role = \"button\" class=\"btn\" style =\"background-color: white;\" href =\"http://localhost:56049/Notification.aspx?accion=1&citacion=test&institucion=est\">Confirmar turno</a><a role = \"button\" class=\"btn\" style =\"background-color: black;\" href =\"https://www.google.com.ar/\">este es un link</a>\r\n                             \r\n                                <!--<a role=\"button\"id=\"btnConfirm\" class=\"btn\" style=\"background-color: #4db1e2;\" href=\"{linkConfirm}\" >Confirmar</a>\r\n                                <a role=\"button\" id=\"btnCancel\" class=\"btn btn-danger\"href=\"{linkCancel}\" >Cancelar Turno</a>-->   \r\n                                                               \r\n                             \t\t                 \t\t\t                \r\n                         </div>\r\n                        \r\n                   </div>\r\n                  \r\n                    <div class=\"copyright\">                   \r\n                            <p >\r\n                                © Copyright 2016. Todos los derechos reservados por <span>Hospital Britanico</span>\r\n                            </p>\r\n                    </div>\r\n                    \r\n\t           </div>\r\n           </div>\r\n\r\n            <div class=\"modal-backdrop\"></div>\r\n           \r\n        </div>\r\n\r\n</body>\r\n\r\n</html>\r\n"
            //};

         

            using (var client = new SmtpClient())
            {
                client.Connect(SMTP_SERVER, SMTP_PORT, true);
                client.Authenticate(
                    EmailUser,
                    EmailPassword
                    );
                await client.SendAsync(mimeMessage);
                
                await client.DisconnectAsync(true);
            }
        }



    }
}

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ReportsTaskRunner.DAL
{
    public class Host
    {
        public string Address { get; set; }
        public int Port { get; set; }
    }

    public class MailCredentials : ICredentialsByHost
    {
        private readonly NetworkCredential wrappedNetworkCredential;
        public MailCredentials(string username, string password)
        {
            wrappedNetworkCredential = new NetworkCredential(username, password);
        }
        #region ICredentialsByHost Members
        public NetworkCredential GetCredential(string host, int port, string authenticationType)
        {
            if (authenticationType.ToLower() == "gssapi" || authenticationType.ToLower() == "ntlm")
            {
                return null;
            }
            return wrappedNetworkCredential.GetCredential(host, port, authenticationType);
        }
        #endregion
    }

    public class NotificationSettings
    {
        public Host NotificationSmtpServer { get; set; }
        public MailCredentials MailCredentials { get; set; }
        public string NotificationEmail { get; set; }
    }

    public class SettingsDAL
    {
        private static Settings settings = MainDAL.db.Settings.ToArray()[0];

        public static NotificationSettings GetNotificationSettings()
        {
            return new NotificationSettings
            {
                NotificationSmtpServer = new Host
                {
                    Address = settings.NotificationSmtp,
                    Port = settings.NotificationPort
                },
                MailCredentials = new MailCredentials(
                    settings.NotificationLogin,
                    settings.NotificationPassword
                ),
                NotificationEmail = settings.NotificationEmail
            };
        }
    }
}

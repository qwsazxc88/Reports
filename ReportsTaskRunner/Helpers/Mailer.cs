using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

using ReportsTaskRunner.DAL;

namespace ReportsTaskRunner.Helpers
{
    public class Mailer
    {
        private static NotificationSettings notificationSettings = SettingsDAL.GetNotificationSettings();

        public static void SendNotification(IList<string> toList, string subject, string body)
        {
            MailMessage mailMessage = null;
            try
            {
                mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(notificationSettings.NotificationEmail, notificationSettings.NotificationEmail);
                
                foreach (var to in toList)
                {
                    mailMessage.To.Add(new MailAddress(to, to));
                }

                mailMessage.Subject = subject;
                mailMessage.Body = string.Format("<html><body>{0}</body></html>", body);
                mailMessage.IsBodyHtml = true;
                
                SmtpClient smtpClient = new SmtpClient
                {
                    Host = notificationSettings.NotificationSmtpServer.Address,
                    Port = notificationSettings.NotificationSmtpServer.Port,
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = notificationSettings.MailCredentials
                };

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                //Log.Error("Exception:", ex);
                Console.WriteLine("Исключение: " + ex.GetBaseException().Message);
            }
            finally
            {
                if (mailMessage != null)
                {
                    mailMessage.Dispose();
                }
            }
        }
    }
    
}

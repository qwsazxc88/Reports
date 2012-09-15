using System;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using log4net;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Presenters.Services;
using Reports.Presenters.UI.ViewModel;


namespace Reports.Presenters.UI.Bl.Impl
{
    public class BaseBl:IBaseBl
    {
        protected static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        #region Fields
        protected IAuthenticationService authenticationService;
        protected IUserDao userDao;
        protected ISettingsDao settingsDao;
        #endregion
        public IAuthenticationService AuthenticationService
        {
            get { return Validate.Dependency(authenticationService); }
            set { authenticationService = value; }
        }
        public IUserDao UserDao
        {
            get { return Validate.Dependency(userDao); }
            set { userDao = value; }
        }
        public ISettingsDao SettingsDao
        {
            get { return Validate.Dependency(settingsDao); }
            set { settingsDao = value; }
        }
        public IUser CurrentUser
        {
            get { return AuthenticationService.CurrentUser; }
        }
        protected  User  SetUserModel(UserModel model,int userId)
        {
            User user = UserDao.Load(userId);
            SetUserModel(model, user);
            return user;
        }
        protected User SetUserModel(UserModel model, User user)
        {
            //User user = UserDao.Load(userId);
            model.UserId = user.Id;
            model.Code = user.Code;
            model.Comment = user.Comment;
            model.DateAccepted = user.DateAccept;
            model.DateRelease = user.DateRelease;
            model.FullName = user.FullName;
            return user;
        }
        protected void SendEmail(IEmailDtoSupport model,
            string to, string subject, string body)
        {
            try
            {
                SetEmailDto(null, model, to, subject, body);
                SendEmail(model.EmailDto);
            }
            catch (Exception ex)
            {
                Log.Error("Exception:", ex);
                model.EmailDto.Error = "Исключение: " + ex.GetBaseException().Message;
            }
        }

        protected void SendEmail(Settings settings, IEmailDtoSupport model,
                    string to, string subject, string body)
        {
            try
            {
                SetEmailDto(settings,model, to, subject, body);
                SendEmail(model.EmailDto);
            }
            catch (Exception ex)
            {
                Log.Error("Exception:", ex);
                model.EmailDto.Error = "Исключение: " + ex.GetBaseException().Message;
            }
        }
        protected void SetEmailDto(Settings settings,IEmailDtoSupport model,
            string to, string subject, string body)
        {
            EmailDto dto = new EmailDto();
            if(settings == null)
                settings = SettingsDao.LoadFirst();
            if (settings == null)
            {
                dto.Error = "Отсутствуют настройки в базе данных.";
                model.EmailDto = dto;
                return;
            }
            dto.SmtpServer = settings.NotificationSmtp;
            dto.SmtpPort = settings.NotificationPort;
            dto.UserName = settings.NotificationLogin;
            dto.Password = settings.NotificationPassword;
            dto.From = settings.NotificationEmail;
            dto.To = to ?? settings.NotificationEmail;
            dto.Subject = subject;
            dto.Body = body;
            model.EmailDto = dto;
        }

        protected static bool SendEmail(EmailDto dto)
        {
            if (!string.IsNullOrEmpty(dto.Error))
                return false;
            MailMessage mailMessage = null;
            try
            {
                mailMessage = new MailMessage
                                  {
                                      From = new MailAddress(dto.From, dto.From)
                                  };
                mailMessage.To.Add(new MailAddress(dto.To, dto.To));
                mailMessage.Subject = dto.Subject;
                mailMessage.Body = "<html>" + dto.Body + "</html>";
                mailMessage.IsBodyHtml = true;
                var smtpClient = new SmtpClient
                {
                    Host = dto.SmtpServer,
                    Port = dto.SmtpPort,
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new BlockGSSAPINTLMCredential(dto.UserName, dto.Password)//new NetworkCredential(dto.UserName, dto.Password),
                };
                smtpClient.Send(mailMessage);



                //WebMail.SmtpServer = dto.SmtpServer;
                //WebMail.SmtpPort = dto.SmtpPort;
                //WebMail.UserName = dto.UserName;
                //WebMail.Password = dto.Password;
                //WebMail.SmtpUseDefaultCredentials = false;
                //WebMail.Send(dto.To,
                //            dto.Subject,
                //            dto.Body,
                //            dto.From);
                Log.DebugFormat("Отправлено письмо на {0}, тема {1}, текст {2}"
                        , dto.To, dto.Subject, dto.Body);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Exception:", ex);
                dto.Error = "Исключение: " + ex.GetBaseException().Message;
                return false;
            }
            finally
            {
                if (mailMessage != null)
                    mailMessage.Dispose();
            }
        }


        protected void SendEmailToBilling(IEmailDtoSupport model,string from,
            string subject, string body)
        {
            try
            {
                SetEmailDtoForBilling(model, from, subject, body);
                SendEmail(model.EmailDto);
            }
            catch (Exception ex)
            {
                Log.Error("Exception:", ex);
                model.EmailDto.Error = "Исключение: " + ex.GetBaseException().Message;
            }
        }

        protected void SetEmailDtoForBilling(IEmailDtoSupport model, string from,
                string subject, string body)
        {
            EmailDto dto = new EmailDto();
            Settings settings = SettingsDao.LoadFirst();
            if (settings == null)
            {
                dto.Error = "Отсутствуют настройки в базе данных.";
                model.EmailDto = dto;
                return;
            }
            dto.SmtpServer = settings.BillingSmtp;
            dto.SmtpPort = settings.BillingPort;
            dto.UserName = settings.BillingLogin;
            dto.Password = settings.BillingPassword;
            dto.From = from ?? settings.NotificationEmail;
            dto.To = settings.BillingEmail;
            dto.Subject = subject;
            dto.Body = body;
            model.EmailDto = dto;
        }



        protected static string GetMonth(DateTime month)
        {
            return month.ToString("MMMM") + " " + month.Year;
        }

    }
    public class BlockGSSAPINTLMCredential : ICredentialsByHost
    {
        private readonly NetworkCredential wrappedNetworkCredential;
        public BlockGSSAPINTLMCredential(string username, string password)
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
}
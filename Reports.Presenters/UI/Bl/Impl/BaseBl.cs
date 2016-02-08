using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using log4net;
using Reports.Core;
using Reports.Core.Utils;
using Reports.Core.Dao;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Presenters.Services;
using Reports.Presenters.UI.ViewModel;
using System.Linq;
//using Reports.Presenters.UI.ViewModel;
namespace Reports.Presenters.UI.Bl.Impl
{
    public class BaseBl : IBaseBl
    {
        protected string SelectAll = "Все";

        public static string[] RublesWords =
                                        {
                                            "рубль",
                                            "рубля",
                                            "рублей",
                                        };
        public static string[] CopeckWords =
                                        {
                                            "копейка",
                                            "копейки",
                                            "копеек",
                                        };
       
        protected static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        #region Fields
        protected IAuthenticationService authenticationService;
        protected IUserDao userDao;
        protected IMailConfirmDao mailConfirmDao;
        protected IMailListDao mailListDao;
        protected ISettingsDao settingsDao;
        protected IDepartmentDao departmentDao;
        protected IRequestNextNumberDao requestNextNumberDao;
        #endregion
        public IMailConfirmDao MailConfirmDao
        {
            get { return Validate.Dependency(mailConfirmDao); }
            set { mailConfirmDao = value; }
        }
        public IMailListDao MailListDao
        {
            get { return Validate.Dependency(mailListDao); }
            set { mailListDao = value; }
        }
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
        public IDepartmentDao DepartmentDao
        {
            get { return Validate.Dependency(departmentDao); }
            set { departmentDao = value; }
        }
        /*protected IStaffEstablishedPostChargeLinksDao staffEstablishedPostChargeLinksDao;
        public IStaffEstablishedPostChargeLinksDao StaffEstablishedPostChargeLinksDao
        {
            get { return Validate.Dependency(staffEstablishedPostChargeLinksDao); }
            set { staffEstablishedPostChargeLinksDao = value; }
        }

        protected IStaffEstablishedPostDao staffEstablishedPostDao;
        public IStaffEstablishedPostDao StaffEstablishedPostDao
        {
            get { return Validate.Dependency(staffEstablishedPostDao); }
            set { staffEstablishedPostDao = value; }
        }
        protected IStaffEstablishedPostRequestDao staffEstablishedPostRequestDao;
        public IStaffEstablishedPostRequestDao StaffEstablishedPostRequestDao
        {
            get { return Validate.Dependency(staffEstablishedPostRequestDao); }
            set { staffEstablishedPostRequestDao = value; }
        }*/
        public IRequestNextNumberDao RequestNextNumberDao
        {
            get { return Validate.Dependency(requestNextNumberDao); }
            set { requestNextNumberDao = value; }
        }
        private IMessagesDao messagesDao;
        public IMessagesDao MessagesDao
        {
            get { return Validate.Dependency(messagesDao); }
            set { messagesDao = value; }
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
        public Result ConfirmMail(Guid key)
        {
            var confirm = MailConfirmDao.Load(key);
            if (confirm != null )
            {

                confirm.User.AlternativeMail = confirm.Mail;
                UserDao.SaveAndFlush(confirm.User);
                return new Result(true, "E-mail успешно подтвержден.");
            }
            return new Result(false, "При подтверждении адреса возникла ошибка.");
        }
        public string AddAlternativeMail(string Email)
        {
            return AddAlternativeMail(CurrentUser.Id, Email);
        }
        public string AddAlternativeMail(int UserId, string Email)
        {
            var user = UserDao.Load(UserId);
            MailConfirm confirm = new MailConfirm { Mail = Email, User = user };
            MailConfirmDao.SaveAndFlush(confirm);
            string confirmation = String.Format("Кто-то указал ваш адрес почты на Кадровом портале. Для подтверждения адреса почты пройдите по ссылке https://ruscount.com:8002/Account/Confirm?key={0}", confirm.Id.ToString());
            SendEmail(Email, "Подтверждение адреса почты", confirmation);
            return String.Format("https://ruscount.com:8002/Account/Confirm?key={0}",confirm.Id.ToString());
        }
        protected EmailDto SendEmailForManagerAcceptRequests(User user,DateTime acceptDate)
        {
            string to = null;
            switch (user.UserRole)
            {
                case UserRole.Manager:
                    foreach (User u in user.Personnels)
                    {
                        if (string.IsNullOrEmpty(u.Email))
                            Log.ErrorFormat("Cannot send request accept e-mail  from manager {0} to personnel manager {1} - empty email",  user.Id, u.FullName);
                        else
                        {
                            if (string.IsNullOrEmpty(to))
                                to = u.Email;
                            else
                                to += ";" + u.Email;
                        }
                    }
                    break;
                default:
                    throw new ArgumentException(string.Format("SendEmailForManagerAcceptRequests - ivalid user {0} role",user.Id));
            }
            string body;
            string subject = GetSubjectAndBodyForManagerAcceptRequest(user,acceptDate, out body);
            return SendEmail(to, subject, body);
        }
        protected string GetSubjectAndBodyForManagerAcceptRequest(User user, DateTime acceptDate, out string body)
        {
            body = string.Format("Пользователь {0} подтвердил ввод заявок за неделю {1} - {2}", 
                                 user.FullName,
                                 acceptDate.AddDays(-4).ToShortDateString(), 
                                 acceptDate.ToShortDateString());
            const string subject = "Подтверждение ввода заявок";
            return subject;
        }
        /// <summary>
        /// Получить всех руководителей сотрудника
        /// </summary>
        /// <param name="user">Сотрудник, для которого требуется найти руководителей</param>
        /// <returns>Список руководителей</returns>
        public IList<User> GetChiefsForManager(int userId)
        {
            IList<User> chiefs = new List<User>();

            User user = UserDao.Load(userId);
            User managerAccount = UserDao.GetManagerForEmployee(user.Login);

            IList<User> mainManagers;
            var manualchiefs = Ioc.Resolve<Reports.Core.Dao.IManualRoleRecordDao>().GetManualRoleHoldersForUser(user.Id, UserManualRole.ApprovesCommonRequests);
            foreach (var el in manualchiefs)
            {
                chiefs.Add(el);
            }
            
            // Для руководителей-замов ближайшие руководители находится на том же уровне
            if (managerAccount != null && !managerAccount.IsMainManager )//Отключено
            {
                mainManagers = DepartmentDao.GetDepartmentManagers(managerAccount.Department != null ? managerAccount.Department.Id : 0)
                    .Where<User>(manager => manager.IsMainManager)
                    .ToList<User>();

                foreach (var mainManager in mainManagers)
                {
                    chiefs.Add(mainManager);
                }
            }

            // Руководители вышележащих уровней по ветке для всех
            User currentUserOrManagerAccount = managerAccount ?? user;
            mainManagers = DepartmentDao.GetDepartmentManagers(currentUserOrManagerAccount.Department != null ? currentUserOrManagerAccount.Department.Id : 0, true)
                .Where<User>(manager => (currentUserOrManagerAccount.Department.ItemLevel ?? 0) > (manager.Department.ItemLevel ?? 0) && manager.Level > 3)
                .ToList<User>();

            foreach (var mainManager in mainManagers)
            {
                chiefs.Add(mainManager);
            }

            return chiefs;
        }
        protected EmailDto SendEmailForMissionOrderReject(IUser current, MissionOrder entity, bool isAdditional)
        {
            string to = entity.Creator.Email;
            if (string.IsNullOrEmpty(to))
            {
                Log.ErrorFormat("Cannot send e-mail about confirm of mission order {0} - email for user {1} empty", entity.Id, entity.Creator.Id);
                return null;
            }
            User curUser = UserDao.Load(CurrentUser.Id);
            string name = isAdditional ? "Изменение приказа на командировку" : "Приказ на командировку";
            string subject = string.Format(@"{0}{1}",name, isAdditional ? " отклонено" : " отклонен");
            string body = string.Format(@"{4} № {0} от {1} {5} руководителем {2}, {3}.<br/>
                                        <a href=""https://ruscount.com:8002"">Кадровый портал</a>"
                                    , isAdditional ? entity.Number + "-изм" : entity.Number.ToString()
                                    , entity.EditDate.ToShortDateString(),curUser.Name,curUser.Email,
                                    name, isAdditional ? " отклонено" : " отклонен"
                                    );
            return SendEmail(to, subject, body);
        }
        protected EmailDto SendEmailForMissionOrderConfirm(IUser current, MissionOrder entity, bool isAdditional)
        {
            string to = entity.User.Email;
            if(string.IsNullOrEmpty(to))
            {
                Log.ErrorFormat("Cannot send e-mail about confirm of mission order {0} - email for user {1} empty",entity.Id,entity.User.Id);
                return null;
            }
            User curUser = UserDao.Load(CurrentUser.Id);
            string name = isAdditional ? "Изменение приказа на командировку" : "Приказ на командировку";
            string subject = string.Format(@"{0}{1}", name, isAdditional ? " утверждено" : " утвержден"); //@"Приказ на командировку утвержден";
            string body = string.Format(@"{3} № {0} от {1} {4} руководителем {2}.<br/>
                                        <a href=""https://ruscount.com:8002"">Кадровый портал</a>"
                                        ,isAdditional ? entity.Number + "-изм" : entity.Number.ToString()
                                        ,entity.EditDate.ToShortDateString()
                                        , curUser.Name, name, isAdditional ? " утверждено" : " утвержден");
            return SendEmail(to, subject, body);
        }
        protected EmailDto SendEmailForMissionOrderNeedToApprove(string to, MissionOrder entity,bool isAdditional)
        {
            if (string.IsNullOrEmpty(to))
            {
                Log.ErrorFormat("Cannot send e-mail about need approve of mission order {0} - to is empty", entity.Id);
                return null;
            }
            to = to.Substring(0, to.Length - 1);
            string name = isAdditional ? "Новое изменение приказа на командировку" : "Новый приказ на командировку";
            //const string subject = @"Новый приказ на командировку";
            string body = string.Format(@"{4} № {0} от {1} ({2}, {3}) требует вашего согласования.<br/>
                                          <a href=""https://ruscount.com:8002"">Кадровый портал</a>",
                         (isAdditional ? entity.Number+"-изм" : entity.Number.ToString()), 
                         entity.EditDate.ToShortDateString(), entity.User.Name, entity.User.Department.Name, name);
            //return SendEmail(to, name, body);
            return new EmailDto();
        }

        protected IList<EmailDto> SendEmailForClearanceChecklistNeedToApprove(IList<User> addresseeList, Dismissal entity)
        {
            const string subject = @"Новый обходной лист";
            const string notificationEmail = "list-retired@sovcombank.ru";
            string body = string.Format(@"Новый Обходной лист № {0} от {1} ({2}, {3}) требует вашего согласования.<br/>
                                          <a href=""https://ruscount.com:8002"">Кадровый портал</a>",
                                          entity.Number, entity.CreateDate.ToShortDateString(), entity.User.Name, entity.User.Department.Name);
            IList<EmailDto> dtoList = new List<EmailDto>();
            /*foreach (var addressee in addresseeList)
            {
                dtoList.Add(SendEmail(addressee.Email, subject, body));
            }*/
            // dtoList.Add(SendEmail(addresseeList[0].Email, subject, body));
            dtoList.Add(SendEmail(notificationEmail, subject, body));

            return dtoList;
        }

        protected bool SendEmailForSicklistError(Sicklist entity)
        {
            string subject = @"Ошибки в заявке";
            string to = entity.User.Email;
            string body = string.Format(@"Ваша заявка №{0} от {1} (больничный лист) содержит ошибки. Описание ошибок приведено в комментариях к заявке.<br/>
                                          Это письмо сформировано автоматически, ответы на него обрабатываться не будут.<br/>
                                          <a href=""https://ruscount.com:8002"">Кадровый портал</a>",
                                          entity.Number, entity.CreateDate.ToShortDateString());
            EmailDto dto = GetEmailDto(null, to, subject, body);
            dto.From = "noreply@ruscount.ru";

            return SendEmail(dto);
        }
        public IList<MessagesDto> GetComments(int PlaceTypeId, int PlaceId)
        {
            return MessagesDao.GetMessages(PlaceTypeId, PlaceId);
        }
        public void AddComment(MessagesDto message)
        {
            Messages msg = new Messages();
            msg.CreateDate = DateTime.Now;
            msg.Creator = UserDao.Load(CurrentUser.Id);
            msg.Comment = message.Comment;
            msg.PlaceId = message.PlaceId;
            msg.CommentPlaceType = message.PlaceTypeId;
            MessagesDao.SaveAndFlush(msg);

        }
        protected bool SendEmailForVacationError(Vacation entity)
        {
            string subject = @"Ошибки в заявке";
            string to = entity.User.Email;
            string body = string.Format(@"Ваша заявка №{0} от {1} (отпуск) содержит ошибки. Описание ошибок приведено в комментариях к заявке.<br/>
                                          Это письмо сформировано автоматически, ответы на него обрабатываться не будут.<br/>
                                          <a href=""https://ruscount.com:8002"">Кадровый портал</a>",
                                          entity.Number, entity.CreateDate.ToShortDateString());
            EmailDto dto = GetEmailDto(null, to, subject, body);
            dto.From = "noreply@ruscount.ru";

            return SendEmail(dto);
        }

        protected EmailDto SendEmailForUserRequest(User user,IUser current,
            User creator,bool isDeleted,
            int requestId,int requestNumber,
            RequestTypeEnum requestType,bool isFromComment)
        {
            string to = null;
            switch(current.UserRole)
            {
                case UserRole.Employee:
                    if(user.Manager == null || string.IsNullOrEmpty(user.Manager.Email))
                    {
                        Log.ErrorFormat("Cannot send e-mail (request {0},requestType {1}) from user {2} to manager - no manager or empty email",requestId,requestType,user.Id);
                        return null;
                    }
                    to = user.Manager.Email;
                    break;
                case UserRole.DismissedEmployee:
                    if (user.Manager == null || string.IsNullOrEmpty(user.Manager.Email))
                    {
                        Log.ErrorFormat("Cannot send e-mail (request {0},requestType {1}) from user {2} to manager - no manager or empty email", requestId, requestType, user.Id);
                        return null;
                    }
                    to = user.Manager.Email;
                    break;
                case UserRole.Manager:
                    foreach (User u in user.Personnels)
                    {
                        if (string.IsNullOrEmpty(u.Email))
                            Log.ErrorFormat("Cannot send e-mail (request {0},requestType {1}) from manager {2} to personnel manager {3} - empty email", requestId, requestType, current.Id, u.FullName);
                        else
                        {
                            if (string.IsNullOrEmpty(to))
                                to = u.Email;
                            else
                                to += ";" + u.Email;
                        }
                    }
                    if ((creator.UserRole & UserRole.Manager) > 0)
                    {
                        if (string.IsNullOrEmpty(user.Email))
                            Log.ErrorFormat(
                                "Cannot send e-mail (request {0},requestType {1}) from manager {2} to user - empty email",
                                requestId, requestType, current.Id);
                        else
                        {
                            if (string.IsNullOrEmpty(to))
                                to = user.Email;
                            else
                                to += ";" + user.Email;
                        }
                    }
                    if (string.IsNullOrEmpty(to))
                        return null;
                    break;
                case UserRole.PersonnelManager:
                    if ((creator.UserRole & UserRole.PersonnelManager) > 0 || isDeleted)
                    {
                        if (user.Manager == null || string.IsNullOrEmpty(user.Manager.Email))
                            Log.ErrorFormat(
                                "Cannot send e-mail (request {0},requestType {1}) from personnel manager {2} to manager - no manager or empty email",
                                requestId, requestType, current.Id);
                        else
                            to = user.Manager.Email;

                        if (string.IsNullOrEmpty(user.Email))
                            Log.ErrorFormat(
                                "Cannot send e-mail (request {0},requestType {1}) from personnel manager {2} to user - empty email",
                                requestId, requestType, current.Id);
                        else
                        {
                            if (string.IsNullOrEmpty(to))
                                to = user.Email;
                            else
                                to += ";" + user.Email;
                        }
                    }
                    if (string.IsNullOrEmpty(to))
                        return null;
                    break;
            }
            string body;
            string subject = GetSubjectAndBody(current, requestId, requestNumber, 
                requestType,isDeleted,out body);
            return SendEmail(to, subject, body);
        }
        protected string GetSubjectAndBody(IUser current,int requestId,int requestNumber,
            RequestTypeEnum requestType, bool isDeleted, out string body)
        {
            string requestTypeStr;
            switch(requestType)
            {
                case RequestTypeEnum.Absence:
                    requestTypeStr = "Заявка на неявку";
                    break;
                case RequestTypeEnum.Dismissal:
                    requestTypeStr = "Заявка на увольнение";
                    break;
                case RequestTypeEnum.Employment:
                    requestTypeStr = "Заявка на прием на работу";
                    break;
                case RequestTypeEnum.HolidayWork:
                    requestTypeStr = "Заявка на оплату праздничных и выходных дней";
                    break;
                case RequestTypeEnum.Mission:
                    requestTypeStr = "Заявка на командировку";
                    break;
                case RequestTypeEnum.Sicklist:
                    requestTypeStr = "Заявка на больничный";
                    break;
                case RequestTypeEnum.TimesheetCorrection:
                    requestTypeStr = "Заявка на корректировку табеля";
                    break;
                case RequestTypeEnum.Vacation:
                    requestTypeStr = "Заявка на отпуск";
                    break;
                case RequestTypeEnum.ChildVacation:
                    requestTypeStr = "Заявка на отпуск по уходу за ребенком";
                    break;
                default:
                    throw new ArgumentException(string.Format("Unknown request type {0}",(int)requestType));
            }
            string acceptOrReject = isDeleted ? " отклонена" : " одобрена";
            body = requestTypeStr + " номер " + requestNumber + acceptOrReject + " пользователем " + current.Name;
            string subject = isDeleted ? "Отклонение заявки" : "Одобрение заявки";
            return subject;
        }

        protected EmailDto SendEmail(string to, string subject, string body)
        {
            EmailDto dto = GetEmailDto(null, to, subject, body);
            if (!string.IsNullOrEmpty(dto.Error))
                return dto;
            try
            {
                SendEmail(dto);
                return dto;
            }
            catch (Exception ex)
            {
                Log.Error("Exception:", ex);
                dto.Error = "Исключение: " + ex.GetBaseException().Message;
                return dto;
            }
        }
        
        protected EmailDto GetEmailDto(Settings settings,
                string to, string subject, string body)
        {
            EmailDto dto = new EmailDto();
            if (settings == null)
                settings = SettingsDao.LoadFirst();
            if (settings == null)
            {
                dto.Error = "Отсутствуют настройки в базе данных.";
                return dto;
            }
            dto.SmtpServer = settings.NotificationSmtp;
            dto.SmtpPort = settings.NotificationPort;
            dto.UserName = settings.NotificationLogin;
            dto.Password = settings.NotificationPassword;
            dto.From = settings.NotificationEmail;
            dto.To = to ?? settings.NotificationEmail;
            dto.Subject = subject;
            dto.Body = body;
            return dto;
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
                Log.Debug("Try to send mail to "+ dto.To+" from "+dto.From+" "+ dto.UserName);
                mailMessage = new MailMessage
                                  {
                                      From = new MailAddress(dto.From, dto.From)
                                  };
                string[] toAddresses = dto.To.Split(';');
                foreach (string address in toAddresses)
                    mailMessage.To.Add(new MailAddress(address, address));
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
        protected int GetDepartmentId(IdNameReadonlyDto department)
        {
            if (department.IsReadOnly)
                return department.Id;
            Department dep = null;
            if (department.Id != 0)
            {
                if (string.IsNullOrEmpty(department.Name))
                    return 0;
                dep = DepartmentDao.SearchByNameDistinct(department.Name);
            }
            return dep == null ? 0 : dep.Id;
        }
        public IdNameReadonlyDto GetDepartmentDto(User user)
        {
            return
                (AuthenticationService.CurrentUser.UserRole & UserRole.Employee) == UserRole.Employee 
                    ? new IdNameReadonlyDto
                          {
                              Id = user.Department == null ? 0 : user.Department.Id,
                              Name = user.Department == null ? string.Empty : user.Department.Name,
                              IsReadOnly = true,
                          }
                    : new IdNameReadonlyDto
                          {
                              Id = 0,
                              Name = string.Empty,
                              IsReadOnly = false,
                          };
        }
        public static void SetInitialDates(BeginEndCreateDate model)
        {
            DateTime today = DateTime.Today;
            model.BeginDate = new DateTime(today.Year, today.Month, 1);
            model.EndDate = today;
        }

        protected static string GetMonth(DateTime month)
        {
            return month.ToString("MMMM") + " " + month.Year;
        }
        protected static IList<IdNameDto> GetYearsList()
        {
            IList<IdNameDto> list = new List<IdNameDto>();
            for (int i = 2012; i <= DateTime.Today.Year + 1; i++)
                list.Add(new IdNameDto(i, i.ToString()));
            return list;
        }
        protected static IList<IdNameDto> GetMonthesList()
        {
            /*return new List<IdNameDto>
                       {
                           new IdNameDto(1,"Январь"),
                           new IdNameDto(2,"Февраль"),
                           new IdNameDto(3,"Март"),
                           new IdNameDto(4,"Апрель"),
                           new IdNameDto(5,"Май"),
                           new IdNameDto(6,"Июнь"),
                           new IdNameDto(7,"Июль"),
                           new IdNameDto(8,"Август"),
                           new IdNameDto(9,"Сентябрь"),
                           new IdNameDto(10,"Октябрь"),
                           new IdNameDto(11,"Ноябрь"),
                           new IdNameDto(12,"Декабрь"),
                       };*/
            IList<IdNameDto> list = new List<IdNameDto>();
            for (int i = 1; i < 13; i++)
                list.Add(new IdNameDto(i,GetMonthName(i)));
            return list;
        }
        public static string GetMonthName(int month, int type)
        {
            string[][] monthes=new string[3][];
            monthes[0] = new string[]{"Январь","Февраль","Март","Апрель","Май", "Июнь", "Июль", "Август","Сентябрь", "Октябрь", "Ноябрь", "Декабрь"};
            monthes[1] = new string[] { "Января", "Февраля", "Марта", "Апреля", "Мая", "Июня", "Июля", "Августа", "Сентября", "Октября", "Ноября", "Декабря" };
            monthes[2] = new string[] { "Январе", "Феврале", "Марте", "Апреле", "Мае", "Июне", "Июле", "Августе", "Сентябре", "Октябре", "Ноябре", "Декабре" };
            return monthes[type][month-1];
        }
        public static string GetMonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return "Январь";
                case 2:
                    return "Февраль";
                case 3:
                    return "Март";
                case 4:
                    return "Апрель";
                case 5:
                    return "Май";
                case 6:
                    return "Июнь";
                case 7:
                    return "Июль";
                case 8:
                    return "Август";
                case 9:
                    return "Сентябрь";
                case 10:
                    return "Октябрь";
                case 11:
                    return "Ноябрь";
                case 12:
                    return "Декабрь";
                default:
                    throw new ArgumentException(string.Format("Неизвестный месяц {0}", month));
            }
        }
        protected static string GetCopeckSumAsString(int sum)
        {
            return GetSumAsString(sum, CopeckWords);
        }
        protected static string GetRubleSumAsString(int sum)
        {
            return GetSumAsString(sum, RublesWords);
        }
        protected static string GetSumAsString(int sum, string[] words)
        {
            sum = sum % 100;
            if (sum > 19)
                sum = sum % 10;
            switch (sum)
            {
                case 1:
                    return words[0];
                case 2:
                case 3:
                case 4:
                    return words[1];
                default:
                    return words[2];
            }
        }
        public static string FormatSum(decimal sum)
        {
            return (int)sum == sum ? ((int)sum).ToString() : sum.ToString("0.00");
        }
        public static string FormatSum(decimal? sum)
        {
            return !sum.HasValue ? string.Empty : FormatSum(sum.Value);
        }
        public static string FormatDate(DateTime? date)
        {
            return date.HasValue ? date.Value.ToShortDateString() : string.Empty;
        }
        public static string FormatDate(DateTime date)
        {
            return date.ToShortDateString();
        }
        
        public void LoadUserData(StandartUserDto model)
        {
            if (model.Id <= 0) return;
            var user = UserDao.Load(model.Id);
            if (user == null) return;
            model.Name = user.Name;
            if (user.Organization != null)
            {
                model.Organization = user.Organization.Name;
            }
            if (user.Position != null)
            {
                model.PositionId = user.Position.Id;
                model.PositionName = user.Position.Name;
            }
            if(user.Personnels!=null && user.Personnels.Any())
            {
                var personnels=user.Personnels;
                model.Personnels = new List<string>();
                foreach(var el in personnels)
                {
                    model.Personnels.Add(el.Name);
                }
            }
            var chiefs=GetChiefsForManager(user.Id);
            if (user.Department != null)
            {
                var managers = DepartmentDao.GetDepartmentManagers(user.Department.Id, true);
                if (managers == null) managers = new List<User>();
                if (managers.Any())
                {
                    int maxlvlv = managers.Max(x => x.Level.HasValue ? x.Level.Value : 0);
                    managers = managers.Where(x => x.Level == maxlvlv).ToList();
                }

                model.Managers = new List<string>();
                foreach (var el in managers.OrderByDescending(x => x.Level))
                {
                    model.Managers.Add(String.Format("{0} ({1})", el.Name, el.Position != null ? el.Position.Name : ""));
                }

                if (chiefs != null && chiefs.Any())
                {
                    model.Chiefs = new List<string>();
                    foreach (var el in chiefs.OrderByDescending(x => x.Level))
                    {
                        if (managers.Any(x => x.Id == el.Id)) continue;
                        model.Chiefs.Add(String.Format("{0} ({1})", el.Name, el.Position != null ? el.Position.Name : ""));
                    }
                }
            }
            var Dep7=user.Department;
            if(Dep7!=null)
            {
                model.Dep7Id=Dep7.Id;
                model.Dep7Name=Dep7.Name;
                var Dep3= Dep7.Dep3;
                if(Dep3!=null && Dep3.Any())
                {
                    model.Dep3Id=Dep3.First().Id;
                    model.Dep3Name=Dep3.First().Name;
                }
            }
            /*var staffEstablishedPost = StaffEstablishedPostDao.Find(x => x.Department.Id == model.Dep7Id && x.Position.Id == model.PositionId);
            if (staffEstablishedPost != null && staffEstablishedPost.Any())
            {
                var post = staffEstablishedPost.First();
                model.Charges = StaffEstablishedPostChargeLinksDao.GetChargesForEstablishedPosts(post.Id);
                model.StaffEstablishedPostId = post.Id;
            }*/
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
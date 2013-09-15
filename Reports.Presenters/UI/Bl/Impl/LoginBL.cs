using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Domain;
using Reports.Presenters.Services;
using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl.Impl
{
    public class LoginBl : BaseBl, ILoginBl
    {
        #region Fields

        //protected IAuthenticationService authenticationService;
        protected IDBVersionDao dbVersionDao;
        protected IFormsAuthenticationService formsAuthenticationService;
        //protected IUserDao userDao;
        protected IUserLoginDao userLoginDao;
        protected IRoleDao roleDao;
        

        #endregion

        #region Properties

        //public IUserDao UserDao
        //{
        //    get { return Validate.Dependency(userDao); }
        //    set { userDao = value; }
        //}

        public IDBVersionDao DBVersionDao
        {
            get { return Validate.Dependency(dbVersionDao); }
            set { dbVersionDao = value; }
        }

        public IUserLoginDao UserLoginDao
        {
            get { return Validate.Dependency(userLoginDao); }
            set { userLoginDao = value; }
        }

        public IRoleDao RoleDao
        {
            get { return Validate.Dependency(roleDao); }
            set { roleDao = value; }
        }


        public IFormsAuthenticationService FormsAuthenticationService
        {
            get { return Validate.Dependency(formsAuthenticationService); }
            set { formsAuthenticationService = value; }
        }

        #endregion

        protected Version AppVersion
        {
            get
            {
                Assembly assembly = Assembly.Load("Reports.Core");
                return assembly.GetName().Version;
            }
        }

        #region ILoginBl Members

        public void InitForm(LogOnModel model)
        {
            CheckDbVesion(model);
        }

        public void OnLogin(LogOnModel model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.UserName) && !string.IsNullOrEmpty(model.Password))
                {
                    User user = UserDao.FindByLogin(model.UserName);
                    if (user != null)
                    {
                        if (user.PasswordIsValid(model.Password))
                        {
                            if (user.IsActive)
                            {

                                //AuthenticationService.CurrentUser = AuthenticationService.CreateUser(user);
                                if (user.IsFirstTimeLogin &&
                                    ((user.UserRole & UserRole.Employee) > 0 ||
                                     (user.UserRole & UserRole.Manager) > 0 ||
                                     (user.UserRole & UserRole.PersonnelManager) > 0))
                                {
                                    model.IsFirstTimeLogin = user.IsFirstTimeLogin;
                                    model.UserId = user.Id;
                                    IUser dto = AuthenticationService.CreateUser(user);
                                    AuthenticationService.SetChangePasswordCookie(dto);
                                }
                                else
                                {
                                    List<UserRole> roles = GetUserRoles(user);
                                    formsAuthenticationService.SignIn(model.UserName, false);
                                    if (roles.Count > 1)
                                    {
                                        model.NeedToSelectRole = true;
                                        user.RoleId = (int)UserRole.NoRole;
                                    }
                                    else
                                        AddRecordToUserLogin(user);
                                    IUser dto = AuthenticationService.CreateUser(user);
                                    AuthenticationService.setAuthTicket(dto);
                                }
                            }
                            else
                                model.ErrorMessage = Resources.Login_AccountInactive;
                        }
                        else
                            model.ErrorMessage = Resources.Login_PasswordIncorrect;
                    }
                    else
                        model.ErrorMessage = Resources.Login_LoginIncorrect;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception:", ex);
                model.ErrorMessage = "Exception:" + ex.GetBaseException().Message;
            }
        }
        protected List<UserRole> GetUserRoles(User user)
        {
            List<UserRole> roles = new List<UserRole>();
            if(!string.IsNullOrEmpty(user.Email))
            {
                IList<User> list = UserDao.FindByEmail(user.Email);
                foreach (User usr in list.Where(x => x.IsActive))
                    GetRolesForUser(roles, usr);
            }
            else
               GetRolesForUser(roles,user);
            return roles;
        }
        protected void GetRolesForUser(List<UserRole> roles,User user)
        {
            if((user.UserRole & UserRole.Accountant) > 0 && !roles.Contains(UserRole.Accountant))
                    roles.Add(UserRole.Accountant);
            if ((user.UserRole & UserRole.Admin) > 0 && !roles.Contains(UserRole.Admin))
                roles.Add(UserRole.Admin);
            if ((user.UserRole & UserRole.BudgetManager) > 0 && !roles.Contains(UserRole.BudgetManager))
                roles.Add(UserRole.BudgetManager);
            if ((user.UserRole & UserRole.Chief) > 0 && !roles.Contains(UserRole.Chief))
                roles.Add(UserRole.Chief);
            if ((user.UserRole & UserRole.Employee) > 0 && !roles.Contains(UserRole.Employee))
                roles.Add(UserRole.Employee);
            if ((user.UserRole & UserRole.Inspector) > 0 && !roles.Contains(UserRole.Inspector))
                roles.Add(UserRole.Inspector);
            if ((user.UserRole & UserRole.Manager) > 0 && !roles.Contains(UserRole.Manager))
                roles.Add(UserRole.Manager);
            if ((user.UserRole & UserRole.OutsourcingManager) > 0 && !roles.Contains(UserRole.OutsourcingManager))
                roles.Add(UserRole.OutsourcingManager);
            if ((user.UserRole & UserRole.PersonnelManager) > 0 && !roles.Contains(UserRole.PersonnelManager))
                roles.Add(UserRole.PersonnelManager);
        }
        public void CheckAndSetUserId(ChangePasswordModel model)
        {
            int? userId = AuthenticationService.GetUserIdFromChangePasswordCookue();
            if (!userId.HasValue)
                throw new FormatException("Доступ к странице запрещен.");
            model.UserId = userId.Value;
        }

        public void OnChangePassword(ChangePasswordModel model)
        {
            try
            {
                CheckAndSetUserId(model);
                User user = UserDao.Load(model.UserId);
                user.Password = model.NewPassword;
                user.IsFirstTimeLogin = false;
                user.Email = model.Email;
                UserDao.MergeAndFlush(user);
                formsAuthenticationService.SignIn(user.Login, false);
                IUser dto = AuthenticationService.CreateUser(user);
                AuthenticationService.setAuthTicket(dto);
                AuthenticationService.ClearChangePasswordCookue();
                SendEmail(model,
                    user.Email,
                    "Изменение пароля",
                    string.Format("Ваш пароль был изменен на {0}", model.NewPassword)
                    );
                if(!string.IsNullOrEmpty(model.EmailDto.Error))
                {
                    model.Error =
                        "Пароль и e-mail был изменены успешно, однако письсмо с новым паролем не было отправлено. Ошибка: " +
                        model.EmailDto.Error;
                }
                //AddRecordToUserLogin(user);
            }
            catch (Exception ex)
            {
                Log.Error("Exception:", ex);
                model.Error = "Исключение:" + ex.GetBaseException().Message;
            }
        }
        public void OnChangePwd(ChangePwdModel model)
        {
            try
            {
                User user = UserDao.Load(CurrentUser.Id);
                user.Password = model.NewPassword;
                UserDao.MergeAndFlush(user);
            }
            catch (Exception ex)
            {
                Log.Error("Exception:", ex);
                model.Error = "Исключение:" + ex.GetBaseException().Message;
            }
        }
        public void OnLoginRecovery(LoginRecoveryModel model)
        {
            try
            {
                IList<User> users = UserDao.FindByEmail(model.Email);
                if (users.Count == 0)
                {
                    model.Error =
                        "Не найден пользователь с таким адресом электронной почты.";
                    //model.IsSupportFormVisible = true;
                    model.Subject = string.Empty;
                    model.Text = string.Empty;
                    return;
                }
                //if (!user.IsActive)
                //{
                //    model.Error =
                //        "Пользователь с данным логином неактивен.Вы можете обратиться в тех. поддержку через форму ниже.";
                //    model.IsSupportFormVisible = true;
                //    model.Subject = string.Empty;
                //    model.Text = string.Empty;
                //    return;
                //}
                //if (string.IsNullOrEmpty(user.Email))
                //{
                //    model.Error =
                //        "У пользователя с данным логином не указан e-mail.Вы можете обратиться в тех. поддержку через форму ниже.";
                //    model.IsSupportFormVisible = true;
                //    model.Subject = string.Empty;
                //    model.Text = string.Empty;
                //    return;
                //}
                string message = string.Format("Информация для пользователя с адресом электронной почты {0}:<br/>",model.Email);
                message = users.Aggregate(message, (current, user) => current + string.Format("Логин {0} - пароль {1}<br/>", user.Login, user.Password));
                SendEmail(model,/*EmailType.UserPasswordRecovered,*/
                    model.Email,
                    "Восстановление пароля",
                    message);
                if (!string.IsNullOrEmpty(model.EmailDto.Error))
                {
                    model.Error = "Ошибка при отправке письма: " + model.EmailDto.Error; 
                                  //" Вы можете обратиться в тех. поддержку через форму ниже.";
                    //model.IsSupportFormVisible = true;
                    //model.Subject = string.Empty;
                    //model.Text = string.Empty;
                }
                else
                    model.IsRecoverySuccess = true;
                //model.Error = "Пароль отправлен на e-mail пользователя.";
            }
            catch (Exception ex)
            {
                Log.Error("Exception:", ex);
                model.Error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
            }
        }
        public bool SendToSupport(SendToSupportModel model)
        {
            try
            {
                SendEmail(model, null, model.Subject, model.Text);
                if(!string.IsNullOrEmpty(model.EmailDto.Error))
                {
                    model.Error = model.EmailDto.Error;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                
                Log.Error("Exception:",ex);
                model.Error = "Исключение: " + ex.GetBaseException().Message;
                return false;
            }
        }

        public void LogOff()
        {
            //AuthenticationService.CurrentUser = null;
            formsAuthenticationService.SignOut();
        }

        public ChangeRoleModel GetChangeRoleModel()
        {
            int userId = AuthenticationService.CurrentUser.Id;
            User user = UserDao.FindById(userId);
            if(user == null)
                throw new ValidationException(string.Format("Не могу загрузить пользователя с id {0}",userId));
            List<UserRole> roles = GetUserRoles(user);
            if(roles.Count == 0)
                throw new ArgumentException("Отсутствуют роли в системе");
            ChangeRoleModel model = new ChangeRoleModel { Roles = RoleDao.LoadRolesForList(roles) };
            return model;
        }
        public void SetUserRole(ChangeRoleModel model)
        {
            int userId = AuthenticationService.CurrentUser.Id;
            User user = UserDao.FindById(userId);
            if (user == null)
                throw new ValidationException(string.Format("Не могу загрузить пользователя с id {0}", userId));
            User first = GetFirstUserWithRole(user, model.RoleId);
            if(first == null)
                throw new ValidationException("Не найдено активных пользователей с указанной ролью.");
            IUser dto = AuthenticationService.CreateUser(first);
            AuthenticationService.setAuthTicket(dto);
            AddRecordToUserLogin(first);
        }
        protected User GetFirstUserWithRole(User user,int role)
        {
            if (!string.IsNullOrEmpty(user.Email))
            {
                IList<User> list = UserDao.FindByEmail(user.Email);
                return list.Where(x => x.IsActive).FirstOrDefault(usr => (usr.RoleId & role) > 0);
            }
            if (user.IsActive && (user.RoleId & role) > 0)
                return user;
            return null;
        }
        #endregion

        protected void AddRecordToUserLogin(User user)
        {
            var userLogin = new UserLogin(user);
            UserLoginDao.MergeAndFlush(userLogin);
        }


        protected void CheckDbVesion(LogOnModel model)
        {
            Version dbVersion;
            try
            {
                dbVersion = DBVersionDao.GetDatabaseVersion();
                if ((dbVersion.Major != AppVersion.Major) ||
                    (dbVersion.Build != AppVersion.Build))
                    model.ErrorMessage = string.Format("Версия приложения {0} не соответствует версии базы данных {1}",
                                                       AppVersion, dbVersion);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                model.ErrorMessage = string.Format("Exception on login:{0}", ex.GetBaseException().Message);
                return;
            }
        }
    }
}
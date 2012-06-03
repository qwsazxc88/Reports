using System;
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
                                    (user.UserRole == UserRole.Employee ||
                                     user.UserRole == UserRole.Manager ||
                                     user.UserRole == UserRole.PersonnelManager))
                                {
                                    model.IsFirstTimeLogin = user.IsFirstTimeLogin;
                                    model.UserId = user.Id;
                                    IUser dto = AuthenticationService.CreateUser(user);
                                    AuthenticationService.SetChangePasswordCookie(dto);
                                }
                                else
                                {
                                    formsAuthenticationService.SignIn(model.UserName, false);
                                    IUser dto = AuthenticationService.CreateUser(user);
                                    AuthenticationService.setAuthTicket(dto);
                                    AddRecordToUserLogin(user);
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
        public void OnLoginRecovery(LoginRecoveryModel model)
        {
            try
            {
                User user = UserDao.FindByLogin(model.Login);
                if (user == null)
                {
                    model.Error =
                        "Не найден пользователь с таким логином.Вы можете обратиться в тех. поддержку через форму ниже.";
                    model.IsSupportFormVisible = true;
                    model.Subject = string.Empty;
                    model.Text = string.Empty;
                    return;
                }
                if (!user.IsActive)
                {
                    model.Error =
                        "Пользователь с данным логином неактивен.Вы можете обратиться в тех. поддержку через форму ниже.";
                    model.IsSupportFormVisible = true;
                    model.Subject = string.Empty;
                    model.Text = string.Empty;
                    return;
                }
                if (string.IsNullOrEmpty(user.Email))
                {
                    model.Error =
                        "У пользователя с данным логином не указан e-mail.Вы можете обратиться в тех. поддержку через форму ниже.";
                    model.IsSupportFormVisible = true;
                    model.Subject = string.Empty;
                    model.Text = string.Empty;
                    return;
                }
                SendEmail(model,/*EmailType.UserPasswordRecovered,*/
                    user.Email,
                    "Восстановление пароля",
                    string.Format("Ваш пароль - {0} ",user.Password));
                if (!string.IsNullOrEmpty(model.EmailDto.Error))
                {
                    model.Error = "Ошибка при отправке письма c паролем: " + model.EmailDto.Error +
                                  " Вы можете обратиться в тех. поддержку через форму ниже.";
                    model.IsSupportFormVisible = true;
                    model.Subject = string.Empty;
                    model.Text = string.Empty;
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
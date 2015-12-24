using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Domain;
using Reports.Core.Dto;
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
                                     (user.UserRole & UserRole.PersonnelManager) > 0 /*||
                                     (user.UserRole & UserRole.ConsultantOutsorsingManager) > 0*/)) //DEPRECATED Убрал роль консультант ОК
                                {
                                    model.IsFirstTimeLogin = user.IsFirstTimeLogin;
                                    model.UserId = user.Id;
                                    IUser dto = AuthenticationService.CreateUser(user,user.UserRole);
                                    AuthenticationService.SetChangePasswordCookie(dto);
                                }
                                else
                                {
                                    formsAuthenticationService.SignIn(model.UserName, false);
                                    List<UserRolesDto> usersAndRoles = GetUserRoles(user);
                                    if (usersAndRoles.Count == 0)
                                        throw new ArgumentException("Отсутствуют роли в системе для данного пользователя");
                                    UserRole role = user.UserRole;
                                    if (IsUserHaveManyRoles(usersAndRoles))
                                    {
                                        model.NeedToSelectRole = true;
                                        role = UserRole.NoRole;
                                    }
                                    AddRecordToUserLogin(user,role);
                                    IUser dto = AuthenticationService.CreateUser(user,role);
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
        public static bool IsUserHaveManyRoles(List<UserRolesDto> dtoList)
        {
            if (dtoList == null && dtoList.Count == 0)
                return false;
            if (dtoList.Count > 1)
                return true;
            return dtoList[0].roles.Count > 1;
        }
        protected List<UserRolesDto> GetUserRoles(User user)
        {
            List<UserRolesDto> usersAndRoles = new List<UserRolesDto>();
            if (!string.IsNullOrEmpty(user.Email))
            {
                IList<User> list = UserDao.FindByEmail(user.Email);
                foreach (User usr in list.Where(x => x.IsActive))
                {
                    List<UserRole> roles = new List<UserRole>();
                    GetRolesForUser(roles, usr);
                    usersAndRoles.Add(new UserRolesDto {user = usr,roles = roles});
                }
            }
            else
            {
                List<UserRole> roles = new List<UserRole>();
                GetRolesForUser(roles, user);
                usersAndRoles.Add(new UserRolesDto { user = user, roles = roles });
            }
            return usersAndRoles;
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
            if ((user.UserRole & UserRole.Director) > 0 && !roles.Contains(UserRole.Director))
                roles.Add(UserRole.Director);
            if ((user.UserRole & UserRole.Secretary) > 0 && !roles.Contains(UserRole.Secretary))
                roles.Add(UserRole.Secretary);
            if ((user.UserRole & UserRole.Findep) > 0 && !roles.Contains(UserRole.Findep))
                roles.Add(UserRole.Findep);
            if ((user.UserRole & UserRole.StaffManager) > 0 && !roles.Contains(UserRole.StaffManager))
                roles.Add(UserRole.StaffManager);
            if ((user.UserRole & UserRole.Archivist) > 0 && !roles.Contains(UserRole.Archivist))
                roles.Add(UserRole.Archivist);
            if ((user.UserRole & UserRole.Candidate) > 0 && !roles.Contains(UserRole.Candidate))
                roles.Add(UserRole.Candidate);
            if ((user.UserRole & UserRole.Security) > 0 && !roles.Contains(UserRole.Security))
                roles.Add(UserRole.Security);
            if ((user.UserRole & UserRole.Trainer) > 0 && !roles.Contains(UserRole.Trainer))
                roles.Add(UserRole.Trainer);
            if ((user.UserRole & UserRole.ConsultantOutsourcing) > 0 && !roles.Contains(UserRole.ConsultantOutsourcing))
                roles.Add(UserRole.ConsultantOutsourcing);
            if ((user.UserRole & UserRole.ConsultantPersonnel) > 0 && !roles.Contains(UserRole.ConsultantPersonnel))
                roles.Add(UserRole.ConsultantPersonnel);
            if ((user.UserRole & UserRole.Accountant) > 0 && !roles.Contains(UserRole.Accountant))
                roles.Add(UserRole.Accountant);
            if ((user.UserRole & UserRole.StaffListOrder) > 0 && !roles.Contains(UserRole.StaffListOrder))
                roles.Add(UserRole.StaffListOrder); 
            if ((user.UserRole & UserRole.DismissedEmployee) > 0 && !roles.Contains(UserRole.DismissedEmployee))
                roles.Add(UserRole.DismissedEmployee);
            if ((user.UserRole & UserRole.Estimator) > 0 && !roles.Contains(UserRole.Estimator))
                roles.Add(UserRole.Estimator);
            if ((user.UserRole & UserRole.TaxCollector) > 0 && !roles.Contains(UserRole.TaxCollector))
                roles.Add(UserRole.TaxCollector);
            if ((user.UserRole & UserRole.SoftAdmin) > 0 && !roles.Contains(UserRole.SoftAdmin))
                roles.Add(UserRole.SoftAdmin); 
        }
        public string GetUserRole(IUser dto,out bool isLinkAvailable)
        {
            //int userId = AuthenticationService.CurrentUser.Id;
            isLinkAvailable = false;
            User user = UserDao.FindById(dto.Id);
            if (user == null)
                throw new ValidationException(string.Format("Не могу загрузить пользователя с id {0}", dto.Id));
            List<UserRolesDto> usersAndRoles = GetUserRoles(user);
            if(usersAndRoles.Count == 0)
                throw new ValidationException(string.Format("Отсутствуют роли для пользователя с id {0}", dto.Id));
            if(dto.UserRole == 0)
            {
                isLinkAvailable = true;
                return "Нет роли";
            }
            User curUser; 
            UserRole current; 
            if(usersAndRoles.Count == 1)
            {
                UserRole r = usersAndRoles[0].roles.Where(x => (x & dto.UserRole) > 0).FirstOrDefault();
                if(r == UserRole.NoRole)
                    throw new ValidationException(string.Format("Недопустимая роль {1} для пользователя с id {0}", dto.Id,dto.UserRole));
                current = r;
                curUser = usersAndRoles[0].user;
                 isLinkAvailable = usersAndRoles[0].roles.Count > 1;
            }
            else
            {

                UserRolesDto urDto = usersAndRoles.Where(x => x.user.Id == dto.Id).FirstOrDefault();
                if (urDto == null)
                    throw new ValidationException(string.Format("Не найден пользователь с идом {0} при определении роли ", dto.Id));
                UserRole r = urDto.roles.Where(x => (x & dto.UserRole) > 0).FirstOrDefault();
                if (r == UserRole.NoRole)
                    throw new ValidationException(string.Format("Недопустимая роль {1} для пользователя с id {0}", dto.Id, dto.UserRole));
                current = r;
                curUser = urDto.user;
                isLinkAvailable = true;
            }
            Role role = RoleDao.Load((int)(dto.UserRole&current));
            if (role == null)
                throw new ValidationException(string.Format("Не могу загрузить роль с id {0}", (int)dto.UserRole));

            return role.Name+ " "+ curUser.Login; 
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
                List<UserRolesDto> usersAndRoles = GetUserRoles(user);
                if(usersAndRoles.Count == 0)
                    throw new ArgumentException("Отсутствуют роли в системе для данного пользователя");
                UserRolesDto urDto = usersAndRoles.Where(x => x.user.Id == user.Id).FirstOrDefault();
                if (urDto == null)
                    throw new ValidationException(string.Format("Не найден пользователь с идом {0} при смене пароля роли ", user.Id));
                UserRole role = user.UserRole;
                if (urDto.roles.Count > 1)
                    role = urDto.roles[0];
                IUser dto = AuthenticationService.CreateUser(user,role);
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
                IList<User> alter = UserDao.QueryExpression(x => x.AlternativeMail == model.Email);
                IList<User> users = UserDao.FindByEmail(model.Email);
                if (users.Count == 0 && !alter.Any())
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
                if(users.Any())
                    message = users.Aggregate(message, (current, user) => current + string.Format("Логин {0} - пароль {1}<br/>", user.Login, user.Password));
                else
                    message = alter.Aggregate(message, (current, user) => current + string.Format("Логин {0} - пароль {1}<br/>", user.Login, user.Password));
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
            List<UserRolesDto> usersAndRoles = GetUserRoles(user);
            if(usersAndRoles.Count == 0)
                throw new ArgumentException("Отсутствуют роли в системе для данного пользователя");
            IList<Role> allRoles = RoleDao.LoadAll();
            List<IdLongNameDto> roles = new List<IdLongNameDto>();
            foreach (UserRolesDto dto in usersAndRoles)
            {
                foreach (UserRole role in dto.roles)
                {
                    Role r = allRoles.Where(x => x.Id == (int)role).FirstOrDefault();
                    if(r == null)
                        throw new ArgumentException(string.Format("Не могу загрузить роль с id {0}",(int)role));
                    roles.Add(new IdLongNameDto
                                  {
                                      Id = (ulong)r.Id * 100000 + (ulong)dto.user.Id,
                                      Name = r.Name + " " + dto.user.Login
                                  }); 
                }
            }
            ChangeRoleModel model = new ChangeRoleModel { Roles = roles};
            return model;
        }
        public void SetUserRole(ChangeRoleModel model)
        {
            int userId = AuthenticationService.CurrentUser.Id;
            User user = UserDao.FindById(userId);
            if (user == null)
                throw new ValidationException(string.Format("Не могу загрузить пользователя с id {0}", userId));
            UserRole role = (UserRole)(model.RoleId/100000);
            int modelUserId = (model.RoleId % 100000) <= int.MaxValue ? (int)(model.RoleId % 100000) : -1;
            User modelUser = UserDao.FindById(modelUserId);
            if (modelUser == null)
                throw new ValidationException(string.Format("Не могу загрузить пользователя с id {0}", modelUserId));
            if((modelUser.RoleId & (int)role) == 0)
                throw new ValidationException(string.Format("Пользователь с id {0} не имеет роли {1}", modelUserId,(int)role));
            //User first = GetFirstUserWithRole(user, model.RoleId);
            //if(first == null)
            //    throw new ValidationException("Не найдено активных пользователей с указанной ролью.");
            IUser dto = AuthenticationService.CreateUser(modelUser,role);
            AuthenticationService.setAuthTicket(dto);
            AddRecordToUserLogin(modelUser,role);
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

        protected void AddRecordToUserLogin(User user,UserRole roleId)
        {
            var userLogin = new UserLogin(user) {RoleId = (int) roleId};
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
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Presenters.UI.ViewModel;

namespace Reports.Presenters.UI.Bl.Impl
{
    public class AdminBl : BaseBl, IAdminBl
    {
        public const string Delimiter = ";";
        public const string TimesheetDayDelimiter = "-";
        public const char ImportDelimiter = ';';
        public const int UseOldMonthDayNummber = 4;
        public const int MaxInfoMessageLength = 4096;

        public const string Header =
            "Стотрудник;СотрудникКод;Месяц;НеСогласенКадровик;1;2;3;4;5;6;7;8;9;10;11;12;13;14;15;16;17;18;19;20;21;22;23;24;25;26;27;28;29;30;31";
        #region Daos

        protected IEmployeeDocumentSubTypeDao employeeDocumentSubTypeDao;
        protected IEmployeeDocumentTypeDao employeeDocumentTypeDao;
        protected IRoleDao roleDao;
        //protected ISettingsDao settingsDao;
        protected IExportImportActionDao exportImportActionDao;
        protected ITimesheetDao timesheetDao;
        protected IInformationDao informationDao;
        
        public IRoleDao RoleDao
        {
            get { return Validate.Dependency(roleDao); }
            set { roleDao = value; }
        }

        public IEmployeeDocumentTypeDao EmployeeDocumentTypeDao
        {
            get { return Validate.Dependency(employeeDocumentTypeDao); }
            set { employeeDocumentTypeDao = value; }
        }

        public IEmployeeDocumentSubTypeDao EmployeeDocumentSubTypeDao
        {
            get { return Validate.Dependency(employeeDocumentSubTypeDao); }
            set { employeeDocumentSubTypeDao = value; }
        }

        //public ISettingsDao SettingsDao
        //{
        //    get { return Validate.Dependency(settingsDao); }
        //    set { settingsDao = value; }
        //}

        public IExportImportActionDao ExportImportActionDao
        {
            get { return Validate.Dependency(exportImportActionDao); }
            set { exportImportActionDao = value; }
        }
        public ITimesheetDao TimesheetDao
        {
            get { return Validate.Dependency(timesheetDao); }
            set { timesheetDao = value; }
        }
        public IInformationDao InformationDao
        {
            get { return Validate.Dependency(informationDao); }
            set { informationDao = value; }
        }

        #endregion

        #region UserList

        public void GetUserListModel(UserListModel model)
        {
            //int id = CurrentUser.Id;
            UserRole role = CurrentUser.UserRole;
            if (role != UserRole.Admin && role != UserRole.PersonnelManager)
                throw new ArgumentException("Доступ запрещен.");
            model.Roles = GetRoleList(true,role);
            int numberOfPages;
            int currentPage = model.CurrentPage;
            if (role == UserRole.Admin)
            {
                model.Users = UserDao.GetUsersForAdmin(model.UserName, model.RoleId,
                                                       ref currentPage, out numberOfPages).ToList().
                    ConvertAll(x => new UserDtoModel
                                        {
                                            Id = x.Id,
                                            Name = x.FullName,
                                            IsActive = x.IsActive,
                                            Login = x.Login,
                                            Role = GetUserRoleName(x.UserRole),
                                        });
            }
            else if (role == UserRole.PersonnelManager)
            {
                model.Users = UserDao.GetUsersForPersonnel(model.UserName, CurrentUser.Id, ref currentPage, out numberOfPages).ToList().
                   ConvertAll(x => new UserDtoModel
                   {
                       Id = x.Id,
                       Name = x.FullName,
                       IsActive = x.IsActive,
                       Login = x.Login,
                       Role = GetUserRoleName(x.UserRole),
                   });
                
            }
            else
                throw new ArgumentException("Доступ запрещен.");
            model.NumberOfPages = numberOfPages;
            model.CurrentPage = currentPage;
        }

        #endregion

        #region User Edit

        public void GetUserEditModel(UserEditModel model)
        {
            UserRole role = CurrentUser.UserRole;
            if (role != UserRole.Admin && role != UserRole.PersonnelManager)
                throw new ArgumentException("Доступ запрещен.");
            model.Roles = GetRoleList(false,role);
            model.Managers = GetUsersWithRoleList(UserRole.Manager, true);
            model.Personnels = role == UserRole.Admin ? GetUsersWithRoleList(UserRole.PersonnelManager, true) 
                                                        : new List<IdNameDto> { new IdNameDto {Id = CurrentUser.Id,Name = CurrentUser.Name}};
            if (model.Id > 0)
            {
                User user = UserDao.Load(model.Id);
                model.Code = user.Code;
                model.Email = user.Email;
                model.IsActive = user.IsActive;
                model.IsNew = user.IsNew;
                model.Login = user.Login;
                model.Password = user.Password;
                model.RoleId = user.Role.Id;
                model.UserName = user.FullName;
                model.UserNameStatic = user.FullName;
                model.Version = user.Version;
                if (model.RoleId == (int)UserRole.Employee)
                {
                    if (user.Manager != null)
                        model.ManagerId = user.Manager.Id;
                    if (user.PersonnelManager != null)
                        model.PersonnelId = user.PersonnelManager.Id;
                }
                SetControlStates(model, user);
            }
            else
            {
                model.IsActive = true;
                model.IsNew = true;
                SetControlStates(model,null);
            }
            
        }

        public void SetStaticToModel(UserEditModel model,bool setStatic)
        {
            UserRole role = CurrentUser.UserRole;
            model.Roles = GetRoleList(false,role);
            model.Managers = GetUsersWithRoleList(UserRole.Manager, true);
            model.Personnels = role == UserRole.Admin ? GetUsersWithRoleList(UserRole.PersonnelManager, true)
                                                       : new List<IdNameDto> { new IdNameDto { Id = CurrentUser.Id, Name = CurrentUser.Name } };
            SetStaticUserPopertiesToModel(model);
        }

        public void SaveUser(UserEditModel model)
        {
            try
            {
                SetStaticToModel(model,false);
                //UserRole role = CurrentUser.UserRole;
                //model.Roles = GetRoleList(false,role);
                //model.Managers = GetUsersWithRoleList(UserRole.Manager, true);
                //model.Personnels = GetUsersWithRoleList(UserRole.PersonnelManager, true);
                var user = new User();
                if (model.Id > 0)
                    user = UserDao.Load(model.Id);
                if (!ValidateModel(model, user))
                {
//                    SetControlStates(model);
                    return;
                }
                user.IsActive = model.IsActive;
                string oldPassword = user.Password;
                if (model.Id == 0)
                {
                    user.Email = model.Email;
                    user.Login = model.Login;
                    user.Name = model.UserName;
                    user.Password = model.Password;
                    user.IsNew = true;
                    user.Role = RoleDao.Load(model.RoleId);
                    if (IsUserFrom1C((UserRole)(model.RoleId)))
                        user.IsFirstTimeLogin = true;
                    user.Code = string.Empty;
                    if (model.RoleId == (int) UserRole.Employee)
                    {
                        if (model.ManagerId != 0)
                            user.Manager = UserDao.Load(model.ManagerId);
                        if (model.PersonnelId != 0)
                            user.PersonnelManager = UserDao.Load(model.PersonnelId);
                    }
                }
                else
                {
                    if (!model.IsPasswordHide)
                    {
                        user.Email = model.Email;
                        user.Password = model.Password;
                        if (!IsUserFrom1C((UserRole) (model.RoleId)) || user.IsNew)
                        {
                            user.Login = model.Login;
                            user.Name = model.UserName;
                        }
                    }
                }
                user = UserDao.MergeAndFlush(user);
                model.Id = user.Id;
                model.IsNew = user.IsNew;
                model.Version = user.Version;
                model.UserNameStatic = model.UserName;
                if ((user.Role.Id != (int) UserRole.Employee) &&
                    ((model.ManagerId != 0) || (model.PersonnelId != 0)))
                    model.ClearManagers = true;

                SetControlStates(model,user);
                if (!string.IsNullOrEmpty(user.Email) && (oldPassword != user.Password))
                {
                    SendEmail(model,
                              user.Email,
                              "Изменение пароля",
                              string.Format("Ваш пароль был изменен.Новый пароль {0}", user.Password)
                        );
                    if (!string.IsNullOrEmpty(model.EmailDto.Error))
                    {
                        model.Error =
                            "Данные пользователя изменены успешно, однако письсмо с новым паролем не было отправлено. Ошибка: " +
                            model.EmailDto.Error;
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                model.Error = string.Format("Исключение {0} ", ex.GetBaseException().Message);
                model.NeedToReload = true;
            }
            
        }

        protected bool ValidateModel(UserEditModel model, User user)
        {
            if (UserDao.IsLoginWithOtherIdExists(model.Login, model.Id))
            {
                model.Error = "Логин должен быть уникальным";
                return false;
            }
            if (model.RoleId == (int)UserRole.Employee)
            {
                if (model.ManagerId == 0)
                {
                    model.Error = "Необходимо указать руководителя для сотрудника.";
                    return false;
                }
                if (model.PersonnelId == 0)
                {
                    model.Error = "Необходимо указать кадровика для сотрудника.";
                    return false;
                }
            }
            return true;
        }

        protected void SetControlStates(UserEditModel model, User user)
        {
            model.IsActiveEditable = true;
            model.IsRoleEditable = model.Id == 0;
            model.IsManagerEditable = model.Id == 0;
            model.IsPersonnelEditable = model.Id == 0;
            if (model.Id == 0)
            {
                model.IsLoginEditable = true;
                model.IsUserNameEditable = true;
            }
            else
            {
                if (CurrentUser.UserRole == UserRole.PersonnelManager && !user.IsFirstTimeLogin)
                {
                    model.IsPasswordHide = true;
                    model.Password = "1234567";
                }
                bool isEditable = !IsUserFrom1C((UserRole)(model.RoleId)) || model.IsNew && !model.IsPasswordHide;
                model.IsLoginEditable = isEditable ;
                model.IsUserNameEditable = isEditable;
                
            }
        }

        protected void SetStaticUserPopertiesToModel(UserEditModel model)
        {
            if (model.Id > 0)
            {
                User user = UserDao.Load(model.Id);
                model.UserNameStatic = user.FullName;
            }
        }

        #endregion

        #region Settings

        public void SetModel(SettingsModel model)
        {
            Settings settings = SettingsDao.LoadFirst();
            if (settings != null)
            {
                model.Id = settings.Id;
                model.Version = settings.Version;
                model.BillingEmail = settings.BillingEmail;
                model.BillingLogin = settings.BillingLogin;
                model.BillingPassword = settings.BillingPassword;
                model.BillingPort = settings.BillingPort;
                model.BillingSmtp = settings.BillingSmtp;
                model.DownloadDictionaryFilePath = settings.DownloadDictionaryFilePath;
                model.ExportImportEmail = settings.ExportImportEmail;
                model.NotificationEmail = settings.NotificationEmail;
                model.NotificationLogin = settings.NotificationLogin;
                model.NotificationPassword = settings.NotificationPassword;
                model.NotificationPort = settings.NotificationPort;
                model.NotificationSmtp = settings.NotificationSmtp;
                model.ReleaseEmployeeDeleteDate = settings.ReleaseEmployeeDeleteDate.ToString("dd.MM.yyyy");
                model.SendEmailToManagerAboutNew = settings.SendEmailToManagerAboutNew;
                model.UploadTimesheetFilePath = settings.UploadTimesheetFilePath;
            }
            else
            {
                model.NotificationPort = 25;
                model.BillingPort = 25;
                model.ReleaseEmployeeDeleteDate =
                    new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
                        .AddMonths(-3).ToString("dd.MM.yyyy");
            }
        }

        public void SaveSettings(SettingsModel model)
        {
            try
            {
                var settings = new Settings();
                if (model.Id > 0)
                    settings = SettingsDao.Load(model.Id);
                settings.BillingEmail = model.BillingEmail;
                settings.BillingLogin = model.BillingLogin;
                settings.BillingPassword = model.BillingPassword;
                settings.BillingPort = model.BillingPort;
                settings.BillingSmtp = model.BillingSmtp;
                settings.DownloadDictionaryFilePath = model.DownloadDictionaryFilePath;
                settings.ExportImportEmail = model.ExportImportEmail;
                settings.NotificationEmail = model.NotificationEmail;
                settings.NotificationLogin = model.NotificationLogin;
                settings.NotificationPassword = model.NotificationPassword;
                settings.NotificationPort = model.NotificationPort;
                settings.NotificationSmtp = model.NotificationSmtp;
                settings.ReleaseEmployeeDeleteDate = model.ValidReleaseEmployeeDeleteDate;
                settings.SendEmailToManagerAboutNew = model.SendEmailToManagerAboutNew;
                settings.UploadTimesheetFilePath = model.UploadTimesheetFilePath;
                settings = SettingsDao.MergeAndFlush(settings);
                model.Id = settings.Id;
                model.Version = settings.Version;
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                model.Error = string.Format("Исключение {0} ", ex.GetBaseException().Message);
            }
        }

        public bool DeleteEmployees(DeleteEmployeesModel model)
        {
            try
            {
                int deletedEmployees = UserDao.DeleteEmployees(model.Date);
                Log.DebugFormat("Удалено {0} сотрудников (дата {1})", deletedEmployees, model.Date.ToShortDateString());
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                model.Error = string.Format("Исключение {0} ", ex.GetBaseException().Message);
                return false;
            }
            return true;
        }

        #endregion
        #region Import/Export
        public void SetModel(ActionListModel model)
        {
           SetModelInternal(model,true);
        }
        protected void SetModelInternal(ActionListModel model,bool setSelection)
        {
            model.Actions = ExportImportActionDao.
                LoadForTypeSorted(model.Type).ToList().
                ConvertAll(x => new 
                    IdNameDto
                    (
                     x.Id, 
                     x.Date.ToString()+
                     (x.Month.HasValue?" "+GetMonth(x.Month.Value):string.Empty)
                    ));
            SetMonths(model, true);            
        }
        protected void SetMonths(ActionListModel model,bool setSelection)
        {
            if (model.Type == ExportImportType.Export)
            {
                model.Monthes = TimesheetDao.GetTimesheetDates().ToList().
                    ConvertAll(x => new DateDto
                    {
                        Date = x,
                        Name =
                            x.ToString("MMMM") + " " +
                            x.Year.ToString(),
                    });
                if(setSelection)
                    model.Month = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            }
            else
                model.Monthes = new List<DateDto> { new DateDto {Date = DateTime.Today,Name = string.Empty}};
        }
        #region Import

        public void AutoImport(AutoImportModel model)
        {
            model.Result = ImportFileInternal(model);
        }
        public void ImportFile(ActionListModel model)
        {
            ImportFileInternal(model);
        }
        protected bool ImportFileInternal(IExportImport model)
        {
            try
            {
                Settings settings = SettingsDao.LoadFirst();
                if (settings == null)
                    throw new ArgumentException("Отсутствуют настройки в базе данных.");
                string importFilePath = settings.DownloadDictionaryFilePath;
                Log.DebugFormat("Загрузка, файл {0}", importFilePath);
                bool hasWarnings;
                List<ImportDto> dtos = ReadFromFile(importFilePath, out hasWarnings);
                SetUsersToDatabase(dtos, ref hasWarnings);
                if (hasWarnings)
                {
                    model.Error = "В файле найдены ошибки, некоторые записи были пропущены.";
                    Log.Debug(model.Error);
                }
                ExportImportAction action = new ExportImportAction
                {
                    Type = (int)ExportImportType.Import,
                    Date = DateTime.Now,
                };
                ExportImportActionDao.MergeAndFlush(action);
                Log.DebugFormat("Загрузка, файл {0} успешна.", importFilePath);
                SendEmail(settings, model, settings.ExportImportEmail, "Загрузка файла",
                        string.Format("Файл был успешно загружен {0}", DateTime.Now));
                if (!string.IsNullOrEmpty(model.EmailDto.Error))
                {
                    string sendMessageError =
                    string.Format(@"Файл был успешно загружен {0}, 
                                   однако письмо об этом не было отправлено. 
                                   Ошибка: {1}", DateTime.Now, model.EmailDto.Error);
                    model.Error = string.IsNullOrEmpty(model.Error)
                                      ? sendMessageError
                                      : model.Error + " " + sendMessageError;
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                model.Error = "Исключение: " + ex.GetBaseException().Message;
                return false;
            }
        }
        protected void SetUsersToDatabase(List<ImportDto> dtos,ref bool hasWarnings)
        {
            List<string> codes = new List<string>();
            foreach (ImportDto dto in dtos)
            {
                if(dto.Role == UserRole.Employee)
                    codes.Add(dto.EmployeeCode);
                else if( dto.Role == UserRole.Manager)
                    codes.Add(dto.ManagerCode);
                else if( dto.Role == UserRole.PersonnelManager)
                    codes.Add(dto.PersonnelCode);
            }
            IList<User> users = UserDao.LoadUserByCodes(codes);
            IList<Role> roles = RoleDao.LoadAllSorted();
            SaveManagers(dtos,users,roles,ref hasWarnings);
            SavePersonnels(dtos, users, roles, ref hasWarnings);
            SaveEmployees(dtos, users, roles, ref hasWarnings);
        }
        protected void SaveManagers(List<ImportDto> dtos, IList<User> users, 
            IList<Role> roles,ref bool hasWarnings)
        {
            IEnumerable<ImportDto> managerDtos = dtos.Where(x => x.Role == UserRole.Manager);
            foreach (ImportDto dto in managerDtos)
            {
                User manager = users.Where(x => x.Code == dto.ManagerCode).FirstOrDefault();
                bool isNewManager = false;
                if (manager == null)
                {
                    manager = new User();
                    isNewManager = true;
                }
                manager.Name = dto.ManagerName;
                manager.Code = dto.ManagerCode;
                if(isNewManager)
                {
                    manager.Login = string.IsNullOrEmpty(dto.Login) ? dto.ManagerCode : dto.Login;
                    manager.Password = manager.Login;
                }
                manager.Comment = dto.Comment;
                if (!isNewManager && (manager.UserRole != UserRole.Manager))
                {
                    Log.WarnFormat("Роль в базе данных для пользователя {0} отличается от руководителя.", manager.Id);
                    hasWarnings = true;
                    continue;
                }
                if(isNewManager)
                {
                    Role role = roles.Where(x => x.Id == (int) dto.Role).FirstOrDefault();
                    manager.Role = role;
                    manager.IsActive = true;
                    manager.IsFirstTimeLogin = true;
                }
                manager = UserDao.Merge(manager);//MergeAndFlush(manager);)
                if (isNewManager)
                    users.Add(manager);
            }
            UserDao.Flush();
        }
        protected void SavePersonnels(List<ImportDto> dtos, IList<User> users,
            IList<Role> roles, ref bool hasWarnings)
        {
            IEnumerable<ImportDto> personnelDtos = dtos.Where(x => x.Role == UserRole.PersonnelManager);
            foreach (ImportDto dto in personnelDtos)
            {
                User personnel = users.Where(x => x.Code == dto.PersonnelCode).FirstOrDefault();
                bool isNewPersonnel = false;
                if (personnel == null)
                {
                    personnel = new User();
                    isNewPersonnel = true;
                }
                personnel.Name = dto.PersonnelName;
                personnel.Code = dto.PersonnelCode;
                if (isNewPersonnel)
                {
                    personnel.Login = string.IsNullOrEmpty(dto.Login) ? dto.PersonnelCode : dto.Login;
                    personnel.Password = personnel.Login;
                }
                personnel.Comment = dto.Comment;
                if (!isNewPersonnel && (personnel.UserRole != UserRole.PersonnelManager))
                {
                    Log.WarnFormat("Роль в базе данных для пользователя {0} отличается от кадровика.",personnel.Id);
                    hasWarnings = true;
                    continue;
                }
                if (isNewPersonnel)
                {
                    Role role = roles.Where(x => x.Id == (int)dto.Role).FirstOrDefault();
                    personnel.Role = role;
                    personnel.IsActive = true;
                    personnel.IsFirstTimeLogin = true;
                }
                personnel = UserDao.Merge(personnel);
                if (isNewPersonnel)
                    users.Add(personnel);
            }
            UserDao.Flush();
        }
        protected void SaveEmployees(List<ImportDto> dtos, IList<User> users,
            IList<Role> roles, ref bool hasWarnings)
        {
            IEnumerable<ImportDto> employeesDtos = dtos.Where(x => x.Role == UserRole.Employee);
            foreach (ImportDto dto in employeesDtos)
            {
                bool isNewEmployee = false;
                User employee = users.Where(x => x.Code == dto.EmployeeCode).FirstOrDefault() ?? new User();
                //if(employee.Id > 0)
                //    Log.WarnFormat("Пользователь с кодом {0} уже добавлен в базу данных.",employee.Code);
                if(employee.Id == 0)
                {
                    isNewEmployee = true;
                    User otherUser = users.Where(x => x.Login == dto.Login).FirstOrDefault();
                    if( otherUser != null)
                    {
                        Log.ErrorFormat("Логин для пользователя с кодом {0} уже указан для другого пользователя (код {1}).Пользователь пропущен.",dto.EmployeeCode,otherUser.Code);
                        continue;
                    }
                }
                employee.Name = dto.EmployeeName;
                employee.Code = dto.EmployeeCode;
                if (isNewEmployee)
                {
                    if (string.IsNullOrEmpty(dto.Login))
                    {
                        Log.WarnFormat("Не указан логин для сотрудника с кодом  {0}",dto.EmployeeCode);
                        employee.Login = dto.EmployeeCode;
                    }
                    else
                        employee.Login = dto.Login;
                    employee.Password = employee.Login;
                }
                employee.Comment = dto.Comment;
                employee.DateAccept = dto.DateAccept;
                employee.DateRelease = dto.DateRelease;
                if (!isNewEmployee && (employee.UserRole != UserRole.Employee))
                {
                    Log.WarnFormat("Роль в базе данных для пользователя {0} отличается от сотрудника.", employee.Id);
                    hasWarnings = true;
                    continue;
                }
                if (isNewEmployee)
                {
                    Role role = roles.Where(x => x.Id == (int)dto.Role).FirstOrDefault();
                    employee.Role = role;
                    employee.IsActive = !employee.DateRelease.HasValue;
                    employee.IsFirstTimeLogin = true;
                }
                User manager = users.Where(x => x.Code == dto.ManagerCode).FirstOrDefault(); 
                if(manager == null)
                {
                    Log.WarnFormat("Для сотрудника {0} ({1}) не найден руководитель с кодом {2}.", employee.Id,employee.Name,dto.ManagerCode);
                    hasWarnings = true;
                    continue;
                }
                employee.Manager = manager;
                User personnel = users.Where(x => x.Code == dto.PersonnelCode).FirstOrDefault();
                if (personnel == null)
                {
                    Log.WarnFormat("Для сотрудника {0} ({1}) не найден кадровик с кодом {2}.", employee.Id, employee.Name, dto.PersonnelCode);
                    hasWarnings = true;
                    continue;
                }
                employee.PersonnelManager = personnel;
                employee = UserDao.Merge(employee);
                if (isNewEmployee)
                    users.Add(employee);
            }
            UserDao.Flush();
        }
        protected List<ImportDto> ReadFromFile(string path, out bool hasWarnings)
        {
            List<ImportDto> dtos = new List<ImportDto>();
            Encoding encoding = Encoding.GetEncoding(1251);
            hasWarnings = false;
            string[] lines = System.IO.File.ReadAllLines(path,encoding);
            for (int i = 1; i < lines.Count(); i++)
            {
                ImportDto dto = ConvertToDto(lines[i]);
                if (dto != null)
                    dtos.Add(dto);
                else
                {
                    hasWarnings = true;
                    Log.WarnFormat("Запись (строка {0}) пропущена.",i+1);
                }
            }
            return dtos;
        }
        protected ImportDto ConvertToDto(string line)
        {
            ImportDto dto = new ImportDto(); 
            string[] fields = line.Split(new[] {ImportDelimiter});
            if(fields.Count() != 10)
                throw new ArgumentException(string.Format("Неверный формат файла загрузки."));
            dto.ManagerName = RemoveDelimiters(fields[0]);
            dto.ManagerCode = RemoveDelimiters(fields[1]);
            dto.PersonnelName = RemoveDelimiters(fields[2]);
            dto.PersonnelCode = RemoveDelimiters(fields[3]);
            dto.EmployeeName = RemoveDelimiters(fields[4]);
            dto.EmployeeCode = RemoveDelimiters(fields[5]);
            if (string.IsNullOrEmpty(dto.ManagerName) &&
               string.IsNullOrEmpty(dto.PersonnelName) &&
               string.IsNullOrEmpty(dto.EmployeeName))
            {
                Log.Warn("Для пользователя не указано имя,запись пропущена.");
                return null;
            }
            if (string.IsNullOrEmpty(dto.ManagerCode) &&
                string.IsNullOrEmpty(dto.PersonnelCode) &&
                string.IsNullOrEmpty(dto.EmployeeCode))
            {
                Log.Warn("Для пользователя не указан код,запись пропущена.");
                return null;
            }
            if (!string.IsNullOrEmpty(dto.EmployeeCode))
                dto.Role = UserRole.Employee;
            else if (!string.IsNullOrEmpty(dto.ManagerCode))
                dto.Role = UserRole.Manager;
            else if (!string.IsNullOrEmpty(dto.PersonnelCode))
                dto.Role = UserRole.PersonnelManager;

            if( string.IsNullOrEmpty(dto.EmployeeCode) &&
                !string.IsNullOrEmpty(dto.ManagerCode) &&
                !string.IsNullOrEmpty(dto.PersonnelCode))
            {
                Log.Warn("Для пользователя указаны и код руководителя, и код кадровика.Запись пропущена.");
                return null; 
            }
            if(dto.Role == UserRole.Employee
                && 
                (string.IsNullOrEmpty(dto.ManagerCode) 
               || string.IsNullOrEmpty(dto.PersonnelCode)))
            {
                Log.Warn("Для сотрудника не указан код руководителя или кадровика,запись пропущена.");
                return null; 
            }
            DateTime acceptDate;
            if (!DateTime.TryParse(RemoveDelimiters(fields[6]), out acceptDate))
            {
                if (dto.Role == UserRole.Employee)
                {
                    Log.Warn("Для сотрудника не указана дата приема,запись пропущена.");
                    return null; 
                }
                dto.DateAccept = new DateTime?();
            }
            else
                dto.DateAccept = acceptDate;
            DateTime releaseDate;
            dto.DateRelease = !DateTime.TryParse(RemoveDelimiters(fields[7]), out releaseDate) ? 
                new DateTime?() : releaseDate;
            dto.Login = RemoveDelimiters(fields[8]);
            dto.Comment = RemoveDelimiters(fields[9]);
            return dto;
        }
        public static string RemoveDelimiters(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            return text.Replace("\"", string.Empty);
        }
        #endregion
        #region Export
        public void AutoExport(AutoImportModel model)
        {
            model.Result = ExportFileInternal(model);
            model.ExportMonth = GetMonth(model.Month);
        }
        public void ExportFile(ActionListModel model)
        {
            try
            {
                ExportFileInternal(model);
            }
            finally
            {
                SetModelInternal(model,false);
            }
        }
        protected bool ExportFileInternal(IExportImport model)
        {
            try
            {
                Settings settings = SettingsDao.LoadFirst();
                if (settings == null)
                    throw new ArgumentException("Отсутствуют настройки в базе данных.");
                string exportFilePath = settings.UploadTimesheetFilePath;
                DateTime exportMonth = model.Month;
                Log.DebugFormat("Выгрузка за {0}, файл {1}",GetMonth(exportMonth),exportFilePath);
                IList<Timesheet> timesheets = TimesheetDao.LoadTimesheetsForMonth(exportMonth);
                List<string> result = SerializeTimesheets(timesheets);
                Encoding encoding = Encoding.GetEncoding(1251);
                System.IO.File.WriteAllLines(exportFilePath,result, encoding);
                ExportImportAction action = new ExportImportAction
                {
                    Type = (int)ExportImportType.Export,
                    Date = DateTime.Now,
                    Month = exportMonth,
                };
                ExportImportActionDao.MergeAndFlush(action);
                string exportMonthString = GetMonth(exportMonth);
                    //exportMonth.ToString("MMMM") + " " + exportMonth.Year;
                Log.DebugFormat("Выгрузка за {0} (файл {1}) успешна.", exportMonthString,exportFilePath);
                SendEmail(settings, model, settings.ExportImportEmail, "Выгрузка файла",
                        string.Format("Файл за {0} был успешно выгружен {1}",
                        exportMonthString , DateTime.Now));
                if (!string.IsNullOrEmpty(model.EmailDto.Error))
                {
                    string sendMessageError =
                    string.Format(@"Файл за {0} успешно выгружен {1}, 
                                   однако письмо об этом не было отправлено. 
                                   Ошибка: {2}", exportMonthString,
                                    DateTime.Now, model.EmailDto.Error);
                    model.Error = sendMessageError;
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                model.Error = "Исключение: " + ex.GetBaseException().Message;
                return false;
            }
        }
        protected List<string> SerializeTimesheets(IList<Timesheet> timesheets)
        {
            List<string> list = new List<string> {Header};
            list.AddRange(timesheets.Select(SerializeTimesheet));
            return list;
        }
        protected string SerializeTimesheet(Timesheet timesheet)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(timesheet.User.Name);
            builder.Append(Delimiter);
            builder.Append(timesheet.User.Code);
            builder.Append(Delimiter);
            builder.Append(timesheet.Month.ToString("dd.MM.yyyy"));
            builder.Append(Delimiter);
            builder.Append(timesheet.PersonnelNotAcceptDate.HasValue ? "1" : "0");
            builder.Append(Delimiter);
            int i = 1;
            foreach (TimesheetDay day in timesheet.Days)
            {
                builder.Append(day.Status.ShortName);
                builder.Append(TimesheetDayDelimiter);
                builder.Append(day.Hours.ToString(CultureInfo.InvariantCulture));
                builder.Append(Delimiter);
                i++;
            }
            for (int j = i; j < 32; j++)
            {
                builder.Append(Core.Dao.Impl.TimesheetDao.HolidayStatusCode);
                builder.Append(TimesheetDayDelimiter);
                builder.Append(Core.Dao.Impl.TimesheetDao.HolidayHours.ToString(CultureInfo.InvariantCulture));
                builder.Append(Delimiter);
            }
            string result = builder.ToString();
            return result.Substring(0, result.Length - 1);
        }
        #endregion
        #endregion
        #region Document Type

        public void SetModel(DocumentTypeListModel model)
        {
            model.Types = EmployeeDocumentTypeDao.LoadAllSorted().ToList().
                ConvertAll(x => new IdNameDto(x.Id, x.Name));
        }

        public void GetEditTypeModel(EditTypeModel model)
        {
            if (model.Id <= 0)
                return;
            EmployeeDocumentType type = EmployeeDocumentTypeDao.Load(model.Id);
            model.Name = type.Name;
        }

        public bool SaveType(EditTypeModel model)
        {
            try
            {
                if (!ValidateModel(model))
                    return false;
                var type = new EmployeeDocumentType();
                if (model.Id > 0)
                    type = EmployeeDocumentTypeDao.Load(model.Id);
                type.Name = model.Name.Trim();
                EmployeeDocumentTypeDao.MergeAndFlush(type);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                model.Error = "Исключение: " + ex.GetBaseException().Message;
                return false;
            }
        }

        protected bool ValidateModel(EditTypeModel model)
        {
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Name.Trim()))
            {
                model.Error = "Название - обязательное поле.";
                return false;
            }
            if (EmployeeDocumentTypeDao.IsTypeWithNameAndOtherIdExists(model.Name.Trim(), model.Id))
            {
                model.Error = "Название типа заявки должно быть уникальным.";
                return false;
            }
            return true;
        }

        #endregion

        #region Document Subtype

        public void SetModel(DocumentSubtypeListModel model)
        {
            model.DocumentTypes = EmployeeDocumentTypeDao.LoadAllSorted().ToList().
                ConvertAll(x => new IdNameDto(x.Id, x.Name));
            model.Subtypes = EmployeeDocumentTypeDao.LoadSubtypeForType(model.DocumentTypeId)
                .ToList().ConvertAll(x => new IdNameDto(x.Id, x.Name));
        }

        public void GetEditSubTypeModel(EditSubTypeModel model)
        {
            EmployeeDocumentType type = EmployeeDocumentTypeDao.Load(model.TypeId);
            model.TypeName = type.Name;
            if (model.Id <= 0)
                return;
            EmployeeDocumentSubType subtype = EmployeeDocumentSubTypeDao.Load(model.Id);
            model.Name = subtype.Name;
        }

        public bool SaveSubType(EditSubTypeModel model)
        {
            try
            {
                if (!ValidateModel(model))
                    return false;
                var subtype = new EmployeeDocumentSubType();
                if (model.Id > 0)
                    subtype = EmployeeDocumentSubTypeDao.Load(model.Id);
                else
                    subtype.Type = EmployeeDocumentTypeDao.Load(model.TypeId);
                subtype.Name = model.Name.Trim();
                EmployeeDocumentSubTypeDao.MergeAndFlush(subtype);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                model.Error = "Исключение: " + ex.GetBaseException().Message;
                return false;
            }
        }
        protected bool ValidateModel(EditSubTypeModel model)
        {
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Name.Trim()))
            {
                model.Error = "Название - обязательное поле.";
                return false;
            }
            if (EmployeeDocumentSubTypeDao.IsSubTypeWithNameAndOtherIdExists(model.Name.Trim(), model.Id, model.TypeId))
            {
                model.Error = "Название подтипа заявки должно быть уникальным.";
                return false;
            }
            return true;
        }


        #endregion

        #region Information
        public void GetInformationListModel(InformationListModel model)
        {
            model.Items = InformationDao.LoadAllSorted().ToList().
                ConvertAll(x => new InformationModelDto
                                    {
                                        Id = x.Id,
                                        Message = x.Message,
                                        Subject = x.Subject,
                                    });
        }
        public void GetEditInfoModel(EditInfoModel model)
        {
            if(model.Id > 0)
            {
                Information info = InformationDao.Load(model.Id);
                model.Subject = info.Subject;
                model.Message = info.Message;
            }
        }
        public bool SaveInfo(EditInfoModel model)
        {
            try
            {
                if (!ValidateModel(model))
                    return false;
                var info = new Information();
                if (model.Id > 0)
                    info = InformationDao.Load(model.Id);
                info.Subject = model.Subject.Trim();
                info.Message = model.Message.Trim();
                InformationDao.MergeAndFlush(info);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                model.Error = "Исключение: " + ex.GetBaseException().Message;
                return false;
            }
            
        }
        protected bool ValidateModel(EditInfoModel model)
        {
            if (model.Subject == null || string.IsNullOrEmpty(model.Subject.Trim()))
            {
                model.Error = "Тема - обязательное поле.";
                return false;
            }
            if (model.Message == null || string.IsNullOrEmpty(model.Message.Trim()))
            {
                model.Error = "Информация - обязательное поле.";
                return false;
            }
            if(model.Message.Trim().Length > MaxInfoMessageLength)
            {
                model.Error = string.Format("Длина поля 'Информация' не может превышать {0} символов.",MaxInfoMessageLength);
                return false;
            }
            return true;
        }
        #endregion   
        #region Helpers




        protected static string GetUserRoleName(UserRole role)
        {
            switch (role)
            {
                case UserRole.Admin:
                    return "Администратор";
                case UserRole.BudgetManager:
                    return "Бюджет";
                case UserRole.Employee:
                    return "Сотрудник";
                case UserRole.Manager:
                    return "Руководитель";
                case UserRole.OutsourcingManager:
                    return "Отусорсинг";
                case UserRole.PersonnelManager:
                    return "Кадровик";
                case UserRole.Inspector:
                    return "Контролер";
                default:
                    throw new ArgumentException(string.Format("Неизвестная роль {0}", (int) role));
            }
        }

        public IList<IdNameDto> GetRoleList(bool addAll, UserRole role)
        {
            
            List<IdNameDto> dtoList = RoleDao.LoadAllSorted().ToList().
                ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (role == UserRole.PersonnelManager)
                return dtoList.Where(x => x.Id == (int) UserRole.Employee).ToList();
            if (addAll)
                dtoList.Insert(0, new IdNameDto(0, "Все"));
            return dtoList;
        }

        public IList<IdNameDto> GetUsersWithRoleList(UserRole role, bool addAll)
        {
            List<IdNameDto> dtoList = UserDao.GetUsersWithRole(role).ToList().
                ConvertAll(x => new IdNameDto(x.Id, x.Name));
            if (addAll)
                dtoList.Insert(0, new IdNameDto(0, string.Empty));
            return dtoList;
        }


        public bool IsUserFrom1C(UserRole role)
        {
            return (role == UserRole.Employee) ||
                   (role == UserRole.Manager) ||
                   (role == UserRole.PersonnelManager);
        }

        #endregion
    }
}
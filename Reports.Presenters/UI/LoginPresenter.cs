using System;
using System.Collections.Generic;
using System.Reflection;
using Iesi.Collections.Generic;
using log4net;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.UI;
using Reports.Presenters.UI.Views;

namespace Reports.Presenters.UI
{
    public class LoginPresenter : AbstractPresenter<ILoginView>
    {
        #region Fields
        private IUserDao _userDao;
        private IDBVersionDao _dbVersionDao;
        private IUserLoginDao _userLoginDao;

        #endregion
        #region Enums
        public enum CheckResult
        {
            Ok = 0,
            LoginIncorrect = 1,
            PasswordIncorrect = 2,
            InactiveUser = 3,
            InvalidView = 4,
            OtherError = 100
        }
        #endregion
        #region Properties
        public IUserDao UserDao
        {
            get { return Validate.Dependency(_userDao); }
            set { _userDao = value; }
        }
        public IDBVersionDao DBVersionDao
        {
            get { return Validate.Dependency(_dbVersionDao); }
            set { _dbVersionDao = value; }
        }
        public IUserLoginDao UserLoginDao
        {
            get { return Validate.Dependency(_userLoginDao); }
            set { _userLoginDao = value; }
        }

        #endregion

        #region Methods
		//protected override void OnInitialize()
		//{
		//    base.OnInitialize();
		//    LoadData();
		//}
		//protected void LoadData()
		//{
		//    ISet<IdNameDto> filialsList = GetTestData();
		//    View.Filials = filialsList;
		//}
        public void InitView(bool isAuthenticated,Version appVersion)
        {
            Validate.NotNull(appVersion, "appVersion"); 
            bool exception;
            Version dbVersion;
            View.LabelAppVersion = string.Format(Resources.ApplicationVersion, appVersion);
            if (!CheckDbVesion(out dbVersion, out exception))
            {
                if (exception)
                    View.Message = Resources.Login_Exception;
                else
                    View.Message = String.Format(Resources.Login_IncorrectDatabaseVersion, appVersion.ToString(),
                        dbVersion == null ? Resources.UnknownDatabaseVersion : dbVersion.ToString());
                View.EnableControls = false;
            }
            else
                View.EnableControls = true;
            View.LabelDBVersion = string.Format(Resources.DatabaseVersion, dbVersion == null ? Resources.UnknownDatabaseVersion : dbVersion.ToString());
            if (isAuthenticated)
                View.RedirectToDefaultPage();
        }

        public bool OnLogin(out User outUser)
        {
            outUser = null;
            if(!View.IsValid)
                return false;
            if (!string.IsNullOrEmpty(View.Login) && !string.IsNullOrEmpty(View.Password))
            {
                User user = UserDao.FindByLogin(View.Login);
                if (user != null)
                {
                    if (user.PasswordIsValid(View.Password))
                    {
                        if (user.IsActive)
                        {
                                View.Id = user.Id;
                                View.Authenticate();
                                outUser = user;
                                AddRecordToUserLogin(user);
                                return true; 
                        }
                        else
                        {
                            View.Message = Resources.Login_AccountInactive;
                            return false; 
                        }
                    }
                    else
                    {
                        View.Message =Resources.Login_PasswordIncorrect;
                        return false; 
                    }
                }
                else
                {
                    View.Message =Resources.Login_LoginIncorrect;
                    return false; 
                }
            }
            else
                return false;
        }
        #endregion

        #region Constructors
        #endregion

        #region Events
        #endregion

        #region Helpers
        protected void AddRecordToUserLogin(User user)
        {
            UserLogin userLogin = new UserLogin(user);
            _userLoginDao.Save(userLogin);
        }
        private bool CheckDbVesion(out Version dbVersion, out bool Exception)
        {
            Exception = false;
            dbVersion = null; 
            try
            {
                dbVersion = DBVersionDao.GetDatabaseVersion();
                return (dbVersion.Major == View.AppVersion.Major) &&
                       (dbVersion.Build == View.AppVersion.Build);
            }
            catch(Exception ex)
            {
                log.Error(ex);
                Exception = true;
                return false;
            }
        }
        #endregion

    }
}

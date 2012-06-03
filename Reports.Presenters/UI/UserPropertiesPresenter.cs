using System;
using System.Collections.Generic;
using System.Text;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Dao.Impl;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.UI;
using Reports.Presenters.Services;
using Reports.Presenters.Services.Impl;
using Reports.Presenters.UI.Actions;
using Reports.Presenters.UI.Views;

namespace Reports.Presenters.UI
{
	public class UserPropertiesPresenter : AbstractPresenter<IUserPropertiesView>
	{
		#region Fields
		
		private IUserDao _userDao;
		private IAuthenticationService _authenticationService;
		#endregion
		#region Properties
		public IUserDao UserDao
		{
			get { return Validate.Dependency(_userDao); }
			set { _userDao = value; }
		}
		public IAuthenticationService AuthenticationService
		{
			get { return Validate.Dependency(_authenticationService); }
			set { _authenticationService = value; }
		}
		#endregion

		#region Methods
		protected override void OnInitialize()
		{
			base.OnInitialize();
			ViewAction action = new ViewAction();
			action.Parse(ContextService.QueryString);
			if (action.UserId.HasValue)
				View.UserId = action.UserId.Value;
			else
				View.UserId = AuthenticationService.CurrentUser.Id;
			View.ActivateButtonVisible = AuthenticationService.CurrentUser.IsAdministrator;
            View.CanAnswerEnabled = AuthenticationService.CurrentUser.IsAdministrator;
			LoadData();
		}
		public void Activate()
		{
            //User user = LoadAndCheckUser(_userDao, AuthenticationService.CurrentUser, View.UserId.Value);
            //if (AuthenticationService.CurrentUser.IsAdministrator)
            //{
            //    user.IsActive = !user.IsActive;
            //    UserDao.Save(user);
            //    LoadData();
            //}
		}
		public void ChangePassword()
		{
            //User user = LoadAndCheckUser(_userDao, AuthenticationService.CurrentUser, View.UserId.Value);
            //if (!string.IsNullOrEmpty(View.Password))
            //    user.Password = View.Password;
            //user.CanAnswer = View.CanAnswer;
            //UserDao.Save(user);
            //LoadData();
		}
		#endregion
		#region Helpers
		protected void LoadData()
		{
            //User user = LoadAndCheckUser(_userDao, AuthenticationService.CurrentUser, View.UserId.Value);
            //View.LastName = user.LastName;
            //View.FirstName = user.FirstName;
            //View.MiddleName = user.MiddleName;
            //View.DateOfEnrol = user.Date.ToString(CalcListDto.DateFormat);
            //View.Status = user.IsActive ? Resources.StatusActive : Resources.StatusInactive;
            //View.ActivateButtonName = user.IsActive ? Resources.ActivateButtonNameInactive : Resources.ActivateButtonNameActive;
            //View.Branch = user.Branch;
            //View.Department = user.Department;
            //View.Position = user.Position;
            //View.CanAnswer = user.CanAnswer;
		}
		public static User LoadAndCheckUser(IUserDao userDao,User currentUser,int userId)
		{
			User user = userDao.FindById(userId);
			if (user == null)
			{
				log.ErrorFormat("Cannot load user with id {0} from database.",userId);
				throw new ApplicationException(string.Format(Resources.Error_CannotLoadUserWithId, userId));
			}
			if ((user.Id != currentUser.Id) && !currentUser.IsAdministrator)
			{
				log.ErrorFormat("Access to page {0} is denied for user with id {1}.View.UserId {2}.", "UserProperties.aspx", currentUser.Id, userId);
				throw new ApplicationException(Resources.Error_AccessDeniedException);
			}
			return user;
		}
		#endregion 
	}
}

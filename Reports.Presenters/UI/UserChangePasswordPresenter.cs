using System;
using System.Collections.Generic;
using System.Text;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Domain;
using Reports.Core.UI;
using Reports.Presenters.Services;
using Reports.Presenters.UI.Views;

namespace Reports.Presenters.UI
{
	public class UserChangePasswordPresenter : AbstractPresenter<IUserChangePasswordView>
	{
		#region Fields

		private IAuthenticationService _authenticationService;
		private IUserDao _userDao;

		#endregion

		#region Constructors

		#endregion

		#region Properties


		public IAuthenticationService AuthenticationService
		{
			get { return Validate.Dependency(_authenticationService); }
			set { _authenticationService = value; }
		}

		public IUserDao UserDao
		{
			get { return Validate.Dependency(_userDao); }
			set { _userDao = value; }
		}

		#endregion

		#region Methods

		protected override void OnInitialize()
		{
		}

		public bool OnSubmitPassword()
		{
			if (!View.IsValid)
				return false;
			//if (View.NewPassword != View.ConfirmNewPassword)
			//{
			//    View.SaveResultOperation.Message = Resources.Message_UserChangePassword;
			//    View.SaveResultOperation.IsSuccessfully = false;
			//    return false;
			//}
			//User item = AuthenticationService.CurrentUser;
			//if (!item.PasswordIsValid(View.OldPassword))
			//{
			//    View.SaveResultOperation.Message = Resources.Login_OldPasswordIncorrect;
			//    View.SaveResultOperation.IsSuccessfully = false;
			//    return false;
			//}
			//if (View.NewPassword == View.OldPassword)
			//{
			//    View.SaveResultOperation.Message = Resources.Login_PasswordsIdentical;
			//    View.SaveResultOperation.IsSuccessfully = false;
			//    return false;
			//}
            //item.Password = View.NewPassword;
            //if(item.IsFirstTimeLogin)
            //    item.IsFirstTimeLogin = false;
            //UserDao.Save(item);
			//View.SaveResultOperation.Message = Resources.Login_PasswordSuccessfully;
			//View.SaveResultOperation.IsSuccessfully = true;
			//OnSaved();
			return true;
		}
		public static string GetPasswordRegExValidatorErrorMessage()
		{
			return Resources.PasswordRegExValidatorText;
		}
		public static string GetConfirmPasswordCompareValidatorErrorMessage()
		{
			return Resources.ConfirmPasswordCompareValidatorText;
		}
		#endregion

		#region Events
		#endregion
	}
}

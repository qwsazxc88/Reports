using System;
using System.Collections.Generic;
using System.Text;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Dto;
using Reports.Core.Filters;
using Reports.Core.UI;
using Reports.Presenters.Services;
using Reports.Presenters.UI.Views;

namespace Reports.Presenters.UI
{
    public class UserLoginListPresenter : AbstractPresenter<IUserLoginListView>
    {
        #region Fields
        private IUserLoginDao _userLoginDao;
        private IAuthenticationService _authenticationService;
        #endregion

        #region Properties

        public IUserLoginDao UserLoginDao
        {
            get { return Validate.Dependency(_userLoginDao); }
            set { _userLoginDao = value; }
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
            if (!AuthenticationService.CurrentUser.IsAdministrator)
            {
                log.ErrorFormat("Access to page {0} is denied for user with id {1}", "UserLoginList.aspx", AuthenticationService.CurrentUser.Id);
                throw new ApplicationException(Resources.Error_AccessDeniedException);
            }
        }
        public void ApplyFilter()
        {
            SelectData();
        }
        public void ClearFilter()
        {
            View.LastName = string.Empty;
            View.FirstName = string.Empty;
            View.MiddleName = string.Empty;
            View.DateFrom = DateTime.Today;
            View.DateTo = DateTime.Today;
            //View.Branch = string.Empty;
            //View.Department = string.Empty;
            //View.Position = string.Empty;
        }
        #endregion
        #region Helpers
        public void SelectData()
        {
            int count;
            IList<UserLoginItemDto> list = UserLoginDao.FindByFilter(
                new UserLoginFilter(
                View.FirstName,
                View.MiddleName, View.LastName,View.DateFrom,View.DateTo,
                View.SortExpression, View.SortAscending, View.Data.MaxRows, View.Data.StartRow), out count);
            View.Data.List = list;
            View.Data.TotalRowCount = count;
        }
        #endregion


    }
}

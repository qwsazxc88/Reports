using System;
using System.Collections.Generic;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Dto;
using Reports.Core.Filters;
using Reports.Core.UI;
using Reports.Presenters.Services;
using Reports.Presenters.UI.Views;

namespace Reports.Presenters.UI
{
    //public class UsersListPresenter : AbstractPresenter<IUsersListView>
    //{
    //    #region Fields
    //    private IUserDao _userDao;
    //    private IAuthenticationService _authenticationService;
    //    #endregion

    //    #region Properties

    //    public IUserDao UserDao
    //    {
    //        get { return Validate.Dependency(_userDao); }
    //        set { _userDao = value; }
    //    }
    //    public IAuthenticationService AuthenticationService
    //    {
    //        get { return Validate.Dependency(_authenticationService); }
    //        set { _authenticationService = value; }
    //    }
    //    #endregion
    //    #region Methods
    //    protected override void OnInitialize()
    //    {
    //        base.OnInitialize();
    //        if (!AuthenticationService.CurrentUser.IsAdministrator)
    //        {
    //            log.ErrorFormat("Access to page {0} is denied for user with id {1}", "UserList.aspx", AuthenticationService.CurrentUser.Id);
    //            throw new ApplicationException(Resources.Error_AccessDeniedException);
    //        }
    //        SetupCombos();
    //    }
    //    public void ApplyFilter()
    //    {
    //        SelectData();
    //    }
    //    public void ClearFilter()
    //    {
    //        View.LastName = string.Empty;
    //        View.FirstName = string.Empty;
    //        View.MiddleName = string.Empty;
    //        View.Branch = string.Empty;
    //        View.Department = string.Empty;
    //        View.Position = string.Empty;
    //    }
    //    #endregion
    //    #region Helpers
    //    public void SetupCombos()
    //    {
    //        View.Branches = UserDao.GetAllEntitiesForDictionary("Branch");
    //        View.Departments = UserDao.GetAllEntitiesForDictionary("Department");
    //        View.Positions = UserDao.GetAllEntitiesForDictionary("Position"); 
    //    }
    //    public void SelectData()
    //    {
    //        int count;
    //        IList<UsersListItemDto> list = UserDao.FindByFilter(
    //            new UserListFilter(
    //            View.FirstName, 
    //            View.MiddleName, View.LastName,
    //            View.Branch,View.Department,View.Position,
    //            View.SortExpression, View.SortAscending, View.Data.MaxRows, View.Data.StartRow), out count);
    //        View.Data.List = list;
    //        View.Data.TotalRowCount = count;
    //    }
    //    #endregion


    //}
}

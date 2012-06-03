using System;
using System.Collections.Generic;
using System.Text;
using Reports.Core;
using Reports.Core.Domain;
using Reports.Core.UI;
using Reports.Presenters.Services;
using Reports.Presenters.UI.Views;

namespace Reports.Presenters.UI
{
    public class UserNamePresenter : AbstractPresenter<IUserNameView>
    {
        #region Fields

        private IAuthenticationService _authenticationService;

        #endregion

        #region Properties

        public IAuthenticationService AuthenticationService
        {
            get { return Validate.Dependency(_authenticationService); }
            set { _authenticationService = value; }
        }

        #endregion

        #region Methods

        #endregion

        #region Constructors

        #endregion

        #region Events

        #endregion

        #region Helpers

        #endregion

        public void InitView()
        {
            //User user = AuthenticationService.CurrentUser;
            //View.FullName = user.FullName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web.UI.WebControls;
using Castle.Core;
using log4net;
using Reports.Core;
using Reports.Core.Domain;
using Reports.Presenters.Services;
using Reports.Presenters.Utils;

namespace Reports.Presenters.UI
{
    [Transient]
    public class MasterPagePresenter<TView>
    {
        #region  Fields
    	
		private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);        
        private IMasterPage view;
        private IAuthenticationService _authenticationService;
        #endregion
        #region Properties
        public IAuthenticationService AuthenticationService
        {
            get { return Validate.Dependency(_authenticationService); }
            set { _authenticationService = value; }
        }
        #endregion
        /// <summary>
        /// Attaches View instance
        /// </summary>
        /// <param name="viewMasterPage"></param>
        public void AttachView(IMasterPage viewMasterPage)
        {
            view = viewMasterPage;
        }
        public void SetupControls()
        {
            //User user = AuthenticationService.CurrentUser;
            //if(user != null)
            //    view.BirthdayControlVisible = user.Birthday.HasValue && (user.Birthday.Value.Date.CompareTo(DateTime.Today) == 0);
            //else
            //    view.BirthdayControlVisible = false;
        }
		public void UpdateTreeViewByRoles()
		{
            //User user = AuthenticationService.CurrentUser;
            //if (user.IsFirstTimeLogin)
            //{
            //    if (view.RequestUrl.ToString().Contains(WebUtils.ChangePasswordPageName))
            //        view.MapTreeView.Nodes.Remove(view.MapTreeView.Nodes[0]); // remove Admin
            //}
            //else if (!user.IsAdministrator)
            //{
            //        TreeNode tn = view.MapTreeView.Nodes[0];
            //        tn.ChildNodes.Remove(tn.ChildNodes[2]); // Remove user login list
            //        tn.ChildNodes.Remove(tn.ChildNodes[0]); // Remove user list
            //}
		}
    }
}

// Copyright (C) 2007-2008 CorePartners. All rights reserved.
//

using Reports.Core;
using Reports.Core.UI;
using Reports.Presenters.Services;
using Reports.Presenters.UI.Actions;
using Reports.Presenters.UI.Views;

namespace Reports.Presenters.UI
{
    public class ViewHeaderPresenter : AbstractPresenter<IViewHeaderView>
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



        #region Overrides

        protected override void OnInitialize()
        {
            ViewAction action = new ViewAction();
            action.Parse(ContextService.QueryString);
            if (action.UserId.HasValue)
            {
                View.PropertiesAction = new ViewAction(action.UserId);
				View.CalculationListsAction = new ViewCalculationListsAction(action.UserId);
				View.NDFLAction = new ViewNDFLAction(action.UserId);
				View.InformationAction = new ViewInformationAction(action.UserId);
				View.QuestionsAction = new ViewQuestionsAction(action.UserId);
				//View.CaseContentAction = new ViewCaseContentAction(action.CaseId);
				//View.CaseTemplatedItemsAction = new ViewCaseTemplatedItemsAction(action.CaseId);
                ///
                /// to be implemented visibility of content tab
                //  View.SetTabVisible(CaseViewTab.Content, true);
            }
			else
            {
            	int userId = AuthenticationService.CurrentUser.Id;
				View.PropertiesAction = new ViewAction(userId);
				View.CalculationListsAction = new ViewCalculationListsAction(userId);
				View.NDFLAction = new ViewNDFLAction(userId);
				View.InformationAction = new ViewInformationAction(userId);
                View.QuestionsAction = new ViewQuestionsAction(userId);
            }
        }

        #endregion

    }
}
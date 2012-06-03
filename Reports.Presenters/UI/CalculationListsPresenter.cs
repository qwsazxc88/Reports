using System;
using System.Collections.Generic;
using System.Text;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Filters;
using Reports.Core.Services;
using Reports.Core.UI;
using Reports.Presenters.Services;
using Reports.Presenters.UI.Actions;
using Reports.Presenters.UI.Views;

namespace Reports.Presenters.UI
{
	public class CalculationListsPresenter : AbstractPresenter<ICalculationListsView>
	{
		#region Fields
		private IDocumentDao _documentDao;
		private IAuthenticationService _authenticationService;
        private IConfigurationService _configurationService;
		#endregion

		#region Properties
		public IDocumentDao DocumentDao
		{
			get { return Validate.Dependency(_documentDao); }
			set { _documentDao = value; }
		}
		public IAuthenticationService AuthenticationService
		{
			get { return Validate.Dependency(_authenticationService); }
			set { _authenticationService = value; }
		}
        public IConfigurationService ConfigurationService
        {
            get { return Validate.Dependency(_configurationService); }
            set { _configurationService = value; }
        }
		#endregion
		#region Methods
		protected override void OnInitialize()
		{
			base.OnInitialize();
			ViewCalculationListsAction action = new ViewCalculationListsAction();
			action.Parse(ContextService.QueryString);
			if (action.UserId.HasValue)
				View.UserId = action.UserId.Value;
			else
				View.UserId = AuthenticationService.CurrentUser.Id;

			if (!AuthenticationService.CurrentUser.IsAdministrator && (View.UserId != AuthenticationService.CurrentUser.Id))
			{
				log.ErrorFormat("Access to page {0} is denied for user with id {1}.View.UserId {2}.", "CalculationLists.aspx",AuthenticationService.CurrentUser.Id,View.UserId);
				throw new ApplicationException(Resources.Error_AccessDeniedException);
			}
			SelectData();
		}
		#endregion
		#region Helpers
		public void SelectData()
		{
			int count;
			IList<CalcListDto> list = DocumentDao.FindByFilter(new CalcListFilter(ReportType.CalList,View.UserId.Value,
			    View.SortExpression, View.SortAscending, View.Data.MaxRows, View.Data.StartRow), 
				AuthenticationService.CurrentUser.IsAdministrator,_configurationService.UsersDocumentsDelayInDays,
				out count);
			View.Data.List = list;
			View.Data.TotalRowCount = count;
		}
		#endregion
	}
}

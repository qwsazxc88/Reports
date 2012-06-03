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
    public class ViewCalcListPresenter : AbstractPresenter<IViewCalcListView>
    {
        #region Fields
        private string BeforePageTextEnd = "<!DOCTYPE HTML PUBLIC";
        private IDocumentDao _documentDao;
        private IAuthenticationService _authenticationService;
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
        #endregion
        #region Methods
        public string GetHtmlForDocumentId(int id)
        {
            Document doc = DocumentDao.FindById(id);
            if (doc != null)
            {
                int index = doc.Html.IndexOf(BeforePageTextEnd);
                if(index  >= 0)
                {
                    string html = doc.Html.Substring(index);
                 
                    return doc.Html.Substring(index);
                }
                return string.Empty;
            }
            else
                return string.Empty;
        }
        #endregion
    }
}

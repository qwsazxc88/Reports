using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using log4net;
using Reports.Core.Services;
using Reports.Presenters.UI.Views;
using Reports.Core.Dao;
using Reports.Core.Domain;
using System.IO;
using Reports.Core;
using Reports.Core.UI;
using Reports.Presenters.Services;
using Reports.Presenters.Services.Impl;

namespace Reports.Presenters.UI
{
    public class GetDocHandler
    {
        #region Fields
		protected static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);        
    	private const int EmptyDocumentHtmlLength = 561;
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
        public void Process(IGetDocRequest request)
        {
            Validate.NotNull(request, "request");
            Document doc = DocumentDao.FindById(request.DocProperties.DocumentId);
			if (doc != null)
			{
				if (!AuthenticationService.CurrentUser.IsAdministrator && (doc.User.Id != AuthenticationService.CurrentUser.Id))
				{
					log.ErrorFormat("Access to page {0} is denied for user with id {1}. Doc.User.Id {2}", "GetDocument.ashx", AuthenticationService.CurrentUser.Id,doc.User.Id);
					throw new ApplicationException(Resources.Error_AccessDeniedException);
				}
                //if (!AuthenticationService.CurrentUser.IsAdministrator && (DateTime.Today > doc.DateCreated.AddDays(_configurationService.UsersDocumentsDelayInDays).Date))
                //{
                //    log.ErrorFormat("Try to access to old report, DateCreated {0} ,user.id {1}",doc.DateCreated,AuthenticationService.CurrentUser.Id);
                //    throw new ApplicationException(Resources.Error_AccessDeniedException);
                //}
				request.ContentType = "text/html";//"message/rfc822";
				request.ContentDisposition = "attachment; filename=\"" + Guid.NewGuid() + ".html\"";
				byte[] bytes = null ;
                //if (doc.Html.Length > EmptyDocumentHtmlLength)
                //{
                //    Encoding enc = Encoding.GetEncoding("utf-8");
                //    bytes = enc.GetBytes(doc.Html);
                //}
                //else
                //{
                //    log.InfoFormat("No data for report with id {0}",doc.Id);
                //    Encoding enc = Encoding.GetEncoding("windows-1251");
                //    bytes = enc.GetBytes("Отчет не содержит данных.");
                //}
				request.OutputStream.Write(bytes, 0, bytes.Length);
			}
			else
			{
				log.ErrorFormat("Cannot find document with id {0} in database.", request.DocProperties.DocumentId);
				throw new ApplicationException(string.Format("Отчет не найден в базе данных.(Id={0})", request.DocProperties.DocumentId));
			}
        }
    }
}

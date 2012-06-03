using System.Collections;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Web;

namespace Reports.Core.Services
{
    public interface IHttpContextService
    {
        IPrincipal User { get; }
        IDictionary Items { get; }
        object this[string name] { get; set;}
        bool HasError { get; }

        #region Methods
        /// <summary>
        /// Check URL access
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        //bool CheckUrlAccess(string url);
        //void RedirectToAction(IAction action);
        //string GetActionAbsoluteUrl(IAction action);
        //string AbsoluteUrlRootPath();
        #endregion

        NameValueCollection QueryString { get; }
        HttpResponse Response { get; }
    }
}

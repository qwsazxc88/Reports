using System;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Web;
using Reports.Core.Services;

namespace Reports.CommonWeb
{

	//[Transient]
	public class HttpContextService : IHttpContextService
	{

		#region Properties

		private static HttpContext WrappedContext
		{
			get
			{
				if (HttpContext.Current != null)
				{
					return HttpContext.Current;
				}
				throw new InvalidOperationException(Resources.HttpContextService_NoCurrentContext);
			}
		}

		#endregion

		#region IHttpContextService Members

		#region Properties

		public IPrincipal User
		{
			get { return WrappedContext.User; }
		}

		public IDictionary Items
		{
			get { return WrappedContext.Items; }
		}

		public bool HasError
		{
			get { return WrappedContext.Server.GetLastError() != null; }
		}

		/// <summary>
		/// Query string
		/// </summary>
		public NameValueCollection QueryString
		{
			get { return WrappedContext.Request.QueryString; }
		}

        public HttpResponse Response
        {
            get { return WrappedContext.Response; }
        }

		#endregion

		#region Indexer

		public object this[string name]
		{
			get { return WrappedContext.Session[name]; }
			set { WrappedContext.Session[name] = value; }
		}
        //public Session  Session { return HttpContext.}

		#endregion

		#region Methods

        //public bool CheckUrlAccess(string url)
        //{
        //    return UrlAuthorizationModule.CheckUrlAccessForPrincipal(url, User, "get");
        //}

        //public void RedirectToAction(IAction action)
        //{
        //    Validate.NotNull(action, "action");
        //    WrappedContext.Response.Redirect(ActionHelper.ResolveAction(action, false));
        //}

        //public string GetActionAbsoluteUrl(IAction action)
        //{
        //    return ActionHelper.ResolveAction(action).Replace("~", AbsoluteUrlRootPath());
        //}

        //public string AbsoluteUrlRootPath()
        //{
        //    if (WrappedContext.Request.Url.IsDefaultPort)
        //        return "http://" + WrappedContext.Request.Url.Host + WrappedContext.Request.ApplicationPath;
        //    else
        //        return "http://" + WrappedContext.Request.Url.Host + ":" + WrappedContext.Request.Url.Port +
        //               WrappedContext.Request.ApplicationPath;
        //}

		#endregion

		#endregion
	}
}
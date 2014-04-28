﻿using System;
using System.Reflection;
using System.Web.Mvc;
using log4net;
using Reports.Core;
using Reports.Presenters.Services;

namespace WebMvc.Controllers
{
    [HandleError(View = "Error")]
    public class BaseController : Controller
    {
        #region Fields

        protected static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected IAuthenticationService authenticationService;
        //protected IBaseBl baseBl;

        #endregion
        public const int MaxCommentLength = 256;

        public IAuthenticationService AuthenticationService
        {
            get
            {
                authenticationService = Ioc.Resolve<IAuthenticationService>();
                return Validate.Dependency(authenticationService);
            }
            set { authenticationService = value; }
        }

        //public IBaseBl BaseBl
        //{
        //    get
        //    {
        //        baseBl = Ioc.Resolve<IBaseBl>();
        //        return Validate.Dependency(baseBl);
        //    }
        //}
        public static string WriteErrorToLog(Exception ex,Uri url)
        {
            if(url != null)
                Log.ErrorFormat("Uri {0}",url.AbsoluteUri);
            if (ex != null)
                Log.Error("Error(MVC error page):", ex);
            else
                Log.Error("Error(MVC error page), exception = null");
            return string.Empty;
        }                       
 
    }
}
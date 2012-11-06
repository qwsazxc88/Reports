using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Reports.CommonWeb;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Presenters;
using Reports.Presenters.Services;
using Reports.Presenters.Services.Impl;


namespace WebMvc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public const string AuthorizedErrorPageUrl = "~/ErrorPage.aspx";
        public const string UnauthorizedErrorPageUrl = "~/UnautorizedErrorPage.aspx";

        /*public override void Init()
        {
            base.Init();
            Error += ErrorHandler;
        }
        private void ErrorHandler(object sender, EventArgs e)
        {
            var ex = Server.GetLastError().InnerException as HttpException ?? 
                     Server.GetLastError() as HttpException;
            if (ex != null && ex.WebEventCode == WebEventCodes.RuntimeErrorPostTooLarge)
            {
                // или другой свой код обработки
                Server.ClearError();
                //Request.Form.Clear();
                Response.Redirect("~/FileSizeTooLarge.htm",true);
            }
        }*/

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "AutoComplete", // Route name
                "AutoComplete/{action}", // URL with parameters
                new { controller = "AutoComplete", action = "Countries" } // Parameter defaults
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            AjaxHelper.GlobalizationScriptPath = "http://ajax.microsoft.com/ajax/4.0/1/globalization/";
            ModelMetadataProviders.Current = new CustomModelMetadataProvider();
            DefaultModelBinder.ResourceClassKey = "Resources";
            try
            {
                //// Register VirtualPathProvider for images.
                //ImagePathProvider imagePathProvider = new ImagePathProvider();
                //// HostingEnvironment.RegisterVirtualPathProvider(imagePathProvider);
                //// Previous line does not work properly for precompiled applications.
                //// Next lines replace that line invoking  RegisterVirtualPathProviderInternal
                //// See http://sunali.com/2008/01/09/virtualpathprovider-in-precompiled-web-sites/
                //HostingEnvironment hostingEnvironmentInstance =
                //    (HostingEnvironment)typeof(HostingEnvironment).InvokeMember("_theHostingEnvironment",
                //                                                                 BindingFlags.NonPublic | BindingFlags.Static |
                //                                                                 BindingFlags.GetField, null, null, null);
                //MethodInfo mi =
                //    typeof(HostingEnvironment).GetMethod("RegisterVirtualPathProviderInternal",
                //                                         BindingFlags.NonPublic | BindingFlags.Static);
                //mi.Invoke(hostingEnvironmentInstance, new object[] { imagePathProvider });

                Assembly assembly = Assembly.Load("Reports.Core");//Assembly.GetExecutingAssembly();
                Version version = assembly.GetName().Version;
                log4net.GlobalContext.Properties["Version"] =
                    string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);

                //string[] urlSegments = HttpContext.Current.Request.Url.ToString().Split('/');
                //if (urlSegments.Length > 2)
                //{
                //    log4net.GlobalContext.Properties["Server"] = urlSegments[2] + "/" + urlSegments[3];
                //}
                if (Ioc.Default == null)
                    Ioc.Container = new CmfContainer();
                //{
                //    Ioc.Container = new CmfContainer();
                //    IDBVersionDao dbVersionDao = Ioc.Resolve<IDBVersionDao>();
                //    log4net.LogManager.GetLogger(GetType()).Info("Database version: " + dbVersionDao.GetDatabaseVersion());
                //}
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(GetType()).Error("Exception on Application_Start: ", ex);
            }
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            try
            {
                SetCulture();
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(GetType()).Error("Exception on Application_BeginRequest: ", ex);
            }
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            Ioc.Resolve<IWebSessionManager>().HandleEndRequest();
        }
        protected void Application_End()
        {
            if (Ioc.Default != null)
            {
                try
                {
                    Ioc.Default.Dispose();
                }
                catch (Exception ex)
                {
                    log4net.LogManager.GetLogger(GetType()).Error("Exception on Application_End: ", ex);
                }
            }
        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            var cookieName = FormsAuthentication.FormsCookieName;
            var authCookie = Context.Request.Cookies[cookieName];
            if (null == authCookie)
            {
                return;
            }
            FormsAuthenticationTicket authTicket = null;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(GetType()).Error(ex);
                return;
            }

            if (null == authTicket)
            {
                return;
            }

            IUser info = null;
            try
            {
                info = UserDto.Deserialize(authTicket.UserData);
                if (info == null)
                    return;
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(GetType()).Error(ex);
                return;
            }

            var id = new FormsIdentity(authTicket);
            var principal = new ReportPrincipal(id, info);
            Context.User = principal;
        }


        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            log4net.LogManager.GetLogger(GetType()).Error("Error occured: ", ex);
            /*if (ex != null)
            {
                if (ex is HttpUnhandledException)
                    ex = ex.GetBaseException();
                //Guid guid = Guid.NewGuid();
                try
                {
                    HttpException httpEx = ex as HttpException;
                    //HttpContext.Current.Cache.Insert(guid.ToString(), ex);
                    if (httpEx != null && ((httpEx.GetHttpCode() == 500 || httpEx.GetHttpCode() == 400) 
                        && httpEx.WebEventCode == WebEventCodes.RuntimeErrorPostTooLarge))
                    {
                        Server.ClearError();
                        Server.Transfer("~/FileSizeTooLarge.htm",false);
                        //Response.Redirect("~/FileSizeTooLarge.htm", true);
                    }
                }
                catch (Exception ex1)
                {
                    log4net.LogManager.GetLogger(GetType()).Error("exception while set exception to session: ", ex1);
                }
            }*/
        }
        protected void SetCulture()
        {
            CultureInfo info = CultureInfo.CreateSpecificCulture("ru-RU");
            //BaseController.HackCulture(info);
            Thread.CurrentThread.CurrentCulture = info;
            Thread.CurrentThread.CurrentUICulture = info;
        }

    }
}
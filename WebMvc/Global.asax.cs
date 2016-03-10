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
using Reports.Presenters;
using Reports.Presenters.Services;
using Reports.Presenters.Services.Impl;
using System.IO;

namespace WebMvc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static readonly Version Version = Assembly.GetExecutingAssembly().GetName().Version;

        public const string AuthorizedErrorPageUrl = "~/ErrorPage.aspx";
        public const string UnauthorizedErrorPageUrl = "~/UnautorizedErrorPage.aspx";

        public override void Init()
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
                HttpRuntimeSection runTime = (HttpRuntimeSection)WebConfigurationManager.GetSection("system.web/httpRuntime");
                int maxRequestLength = (runTime.MaxRequestLength - 100) * 1024;
                // или другой свой код обработки
                HttpContext context = ((HttpApplication)sender).Context;
                if (context.Request.ContentLength > maxRequestLength)
                {
                    IServiceProvider provider = context;
                    HttpWorkerRequest workerRequest =
                        (HttpWorkerRequest) provider.GetService(typeof (HttpWorkerRequest));

                    // Check if body contains data
                    if (workerRequest.HasEntityBody())
                    {
                        // get the total body length
                        int requestLength = workerRequest.GetTotalEntityBodyLength();

                        // Get the initial bytes loaded
                        int initialBytes = 0;

                        if (workerRequest.GetPreloadedEntityBody() != null)
                            initialBytes = workerRequest.GetPreloadedEntityBody().Length;

                        if (!workerRequest.IsEntireEntityBodyIsPreloaded())
                        {
                            byte[] buffer = new byte[512000];

                            // Set the received bytes to initial bytes before start reading
                            int receivedBytes = initialBytes;

                            while (requestLength - receivedBytes >= initialBytes)
                            {
                                // Read another set of bytes
                                initialBytes = workerRequest.ReadEntityBody(buffer, buffer.Length);

                                // Update the received bytes
                                receivedBytes += initialBytes;
                            }
                            //initialBytes = 
                            workerRequest.ReadEntityBody(buffer, requestLength - receivedBytes);
                        }
                    }
                }
                context.Server.ClearError();
                context.Response.Redirect("~/FileSizeTooLarge.htm", true);
            }
        }

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
                "ShortCode", // Route name
                "заявка/{id}", // URL with parameters
                new { controller = "Shortcode", action = "Index", id = UrlParameter.Optional } // Parameter defaults

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
            string source =  Environment.NewLine + Request.ServerVariables["ALL_RAW"].ToString();
            
            var usr = UserDto.Deserialize(((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData); /*"System.Security.Principal.GenericIdentity" к типу "System.Web.Security.FormsIdentity".
																													http://stackoverflow.com/questions/20400855/unable-to-cast-object-of-type-system-security-principal-genericidentity-to-typ
																													*/
			string usrname = "";
            if (usr != null)
            {
                usrname = String.Format("{0} {1} {2} {3}", usr.Id, usr.Login, usr.Name, usr.UserRole);
            }
            log4net.LogManager.GetLogger(GetType()).Error(String.Format("Error occured: request.params: {0} {1} ", source, Environment.NewLine),ex);
            var requbl=Ioc.Resolve<Reports.Presenters.UI.Bl.IRequestBl>();
            /*if (requbl==null) return;
            requbl.sendEmail("baranov@ruscount.ru", "[WEBAPP] Ошибка :)", String.Format("Error occured: request.params: {0} {1} {2} {3}", source, Environment.NewLine, ex, usrname));*/
            /*HttpException lastErrorWrapper = ex as HttpException;

            Exception lastError = lastErrorWrapper;
            /*if (lastErrorWrapper.InnerException != null)
                lastError = lastErrorWrapper.InnerException;*/
            /*string txt = "";
            while (lastError != null)
            {
                string lastErrorTypeName = lastError.GetType().ToString();
                string lastErrorMessage = lastError.Message;
                string lastErrorStackTrace = lastError.StackTrace;
                var usr = UserDto.Deserialize(((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData);
                txt += string.Format("Произошла ошибка:<br/> {0} <br/> {1} <br/> {2} <br/> {3} {4} {5} <br> ", lastErrorTypeName, lastErrorMessage, lastErrorStackTrace, usr.Id, usr.Name, usr.UserRole);
                lastError = lastError.InnerException;
            }
            var requbl=Ioc.Resolve<Reports.Presenters.UI.Bl.IRequestBl>();
            if (requbl==null) return;
            requbl.sendEmail("baranov@ruscount.ru", "[WEBAPP] Ошибка :)", txt);*/
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reports.Core;
using Reports.Presenters.Services;
using Reports.Presenters.Services.Impl;
using Reports.Presenters.UI.ViewModel;

namespace WebMvc.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            //ViewBag.Message = "Welcome to ASP.NET MVC!";
            IAuthenticationService service = Ioc.Resolve<IAuthenticationService>();
            IUser user = service.CurrentUser;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}

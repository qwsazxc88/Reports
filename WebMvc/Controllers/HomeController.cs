using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reports.Core;
using Reports.Presenters.Services;
using Reports.Presenters.Services.Impl;
using Reports.Presenters.UI.ViewModel;
using WebMvc.Models;

namespace WebMvc.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {

        public ActionResult Index(int? menuId)
        {
            //ViewBag.Message = "Welcome to ASP.NET MVC!";
            IAuthenticationService service = Ioc.Resolve<IAuthenticationService>();
            IUser user = service.CurrentUser;
            return View(new HomeModel { menuId = menuId.HasValue ? menuId.Value: 0 });
        }

        public ActionResult About()
        {
            return View();
        }
    }
}

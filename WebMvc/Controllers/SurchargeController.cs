using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reports.Core;
using Reports.Presenters.UI.Bl;
using Reports.Core.Dto;
using Reports.Core.Services;
using Reports.Presenters.Services;
using Reports.Presenters.UI.ViewModel;
namespace WebMvc.Controllers
{
    public class SurchargeController : Controller
    {
        #region Properties
        protected ISurchargeBL surchargeBl;
        public ISurchargeBL SurchargeBl
        {
            get
            {
                surchargeBl = Ioc.Resolve<ISurchargeBL>();
                return Validate.Dependency(surchargeBl);
            }
        }
        protected IUser currentUser;
        public IUser CurrentUser
        {
            get
            {
                var serv = Ioc.Resolve<IAuthenticationService>();
                currentUser = serv.CurrentUser;
                return Validate.Dependency(currentUser);
            }
        }
        #endregion

        [HttpGet]
        public ActionResult Index()
        {
            var model = new SurchargeViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(SurchargeViewModel model)
        {
            return View(model);
        }
        public ActionResult AddSurcharge(int userId, int missionReportId, float sum)
        {
            SurchargeBl.AddSurcharge(userId, sum, CurrentUser.Id, DateTime.Now, missionReportId);
            return Json(new { status = "Ok" });
        }

    }
}

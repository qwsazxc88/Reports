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
using System.ComponentModel.DataAnnotations;
using WebMvc.Attributes;
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
        protected IRequestBl requestBl;
        public IRequestBl RequestBl
        {
            get
            {
                requestBl = Ioc.Resolve<IRequestBl>();
                return Validate.Dependency(requestBl);
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
            var docs = SurchargeBl.GetDocuments(CurrentUser.Id, CurrentUser.UserRole, model.DepartmentId, 0, model.BeginDate, model.EndDate, model.UserName, model.SortBy, model.SortDescending, model.Number);
            return PartialView("IndexPartial",docs);
        }
        [ReportAuthorize(UserRole.Accountant)]
        public ActionResult AddSurcharge(int userId, int missionReportId, float sum, int deductionNumber)
        {
            if (deductionNumber <= 0) return Json(new { status = "Error", message = "Не указан номер удержания." });
            //string error=RequestBl.SetDeductionDoc(deductionNumber, missionReportId);
            //if (error != "") return Json(new { status = "Error", message = error });
            int doc = SurchargeBl.AddSurcharge(userId, sum, CurrentUser.Id, DateTime.Now, missionReportId, deductionNumber);
            if (doc < 1)
            {
                if (doc == -1) return Json(new { status = "Error", message = "Ошибка добавления документа.  Доплата уже была создана." });
                
                return Json(new { status = "Error", message = "Ошибка добавления документа." });
            }
            return Json(new { status = "Ok", message = String.Format("Добавлен документ №{0}.",doc) });
        }

    }
}

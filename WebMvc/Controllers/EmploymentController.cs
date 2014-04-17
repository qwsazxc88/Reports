using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reports.Presenters.UI.ViewModel.Employment;
using Reports.Core.Dto.Employment;

namespace WebMvc.Controllers
{
    public class EmploymentController : Controller
    {
        //
        // GET: /Employment/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GeneralInfo()
        {
            var model = new GeneralInfoModel();
            model.NameChanges = new List<NameChangeDto>();
            model.NameChanges.Add(new NameChangeDto { PreviousName = "Vasya", Date = new DateTime(), Place = "NV", Reason = "Marriage" });
            return View(model);
        }

        [HttpPost]
        public ActionResult GeneralInfo(GeneralInfoModel model)
        {
            return View(model);
        }

        public ActionResult Passport()
        {
            return View();
        }

        public ActionResult Education()
        {
            return View();
        }

        public ActionResult Family()
        {
            return View();
        }
    }
}

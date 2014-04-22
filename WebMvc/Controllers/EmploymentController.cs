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
            return View(model);
        }

        [HttpPost]
        public ActionResult GeneralInfo(GeneralInfoModel model, string cmd = "")
        {
            if (cmd == "add-name-change")
            {
                //if (model.NameChanges == null)
                //{
                //    model.NameChanges = new List<NameChangeDto>();
                //}
                model.NameChanges.Add(new NameChangeDto { PreviousName = "John", Date = new DateTime(), Place = "NV", Reason = "Marriage" });
            }
            return View(model);
        }
        /*
        [HttpPost]
        public PartialViewResult AddNameChange(GeneralInfoModel model)
        {
            model.NameChanges.Add(new NameChangeDto { PreviousName = "John", Date = new DateTime(), Place = "NV", Reason = "Marriage" });
            return PartialView("Employment/NameChanges", model);
        }*/

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

        public ActionResult MilitaryService()
        {
            return View();
        }

        public ActionResult Experience()
        {
            return View();
        }

        public ActionResult Contacts()
        {
            return View();
        }

        public ActionResult BackgroundCheck()
        {
            return View();
        }

        public ActionResult Training()
        {
            return View();
        }

        public ActionResult ApplicationLetter()
        {
            return View();
        }

        public ActionResult Managers()
        {
            return View();
        }

        public ActionResult PersonnelManagers()
        {
            return View();
        }

        public ActionResult Roster()
        {
            return View();
        }

        public ActionResult CustomReport()
        {
            return View();
        }

        public ActionResult Signers()
        {
            return View();
        }
    }
}

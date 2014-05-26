using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reports.Presenters.UI.ViewModel.Employment2;
using Reports.Core.Dto.Employment2;
using Reports.Core;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;

namespace WebMvc.Controllers
{
    public class EmploymentController : Controller
    {
        protected IEmploymentBl employmentBl;
        public IEmploymentBl EmploymentBl
        {
            get
            {
                employmentBl = Ioc.Resolve<IEmploymentBl>();
                return Validate.Dependency(employmentBl);
            }
        }

        //
        // GET: /Employment/
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToActionPermanent("GeneralInfo");
        }

        // General Info
        [HttpGet]
        public ActionResult GeneralInfo()
        {
            var model = EmploymentBl.GetGeneralInfoModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult GeneralInfo(GeneralInfoModel model, string cmd = "")
        {
            switch (cmd)
            {
                case "add-name-change":
                    model.NameChanges.Add(new NameChangeDto());
                    break;
                default:
                    return RedirectToActionPermanent("Passport");
            }
            return View(model);
        }

        // Passport
        [HttpGet]
        public ActionResult Passport()
        {
            var model = EmploymentBl.GetPassportModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Passport(PassportModel model, string cmd = "")
        {
            switch (cmd)
            {
                default:
                    return RedirectToActionPermanent("Education");
            }
            // return View(model);
        }

        // Education
        [HttpGet]
        public ActionResult Education()
        {
            var model = new EducationModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Education(EducationModel model, string cmd = "")
        {
            switch (cmd)
            {
                case "add-higher-education-diploma":
                    model.HigherEducationDiplomas.Add(new HigherEducationDiplomaDto());
                    break;
                case "add-postgraduate-education-diploma":
                    model.PostGraduateEducationDiplomas.Add(new PostGraduateEducationDiplomaDto());
                    break;
                case "add-certification":
                    model.Certifications.Add(new CertificationDto());
                    break;
                case "add-training":
                    model.Training.Add(new TrainingDto());
                    break;
                default:
                    return RedirectToActionPermanent("Family");
            }

            return View(model);
        }

        // Family
        [HttpGet]
        public ActionResult Family()
        {
            var model = new FamilyModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Family(FamilyModel model, string cmd = "")
        {
            switch (cmd)
            {
                case "add-child":
                    model.Children.Add(new FamilyMemberDto());
                    break;
                default:
                    return RedirectToActionPermanent("MilitaryService");
            }
            return View(model);
        }

        // Military Service
        [HttpGet]
        public ActionResult MilitaryService()
        {
            var model = EmploymentBl.GetMilitaryServiceModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult MilitaryService(MilitaryServiceModel model, string cmd = "")
        {
            switch (cmd)
            {
                default:
                    return RedirectToActionPermanent("Experience");
            }
            // return View(model);
        }

        // Experience
        [HttpGet]
        public ActionResult Experience()
        {
            var model = new ExperienceModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Experience(ExperienceModel model, string cmd = "")
        {
            switch (cmd)
            {
                default:
                    return RedirectToActionPermanent("Contacts");
            }
            // return View(model);
        }

        // Contacts
        [HttpGet]
        public ActionResult Contacts()
        {
            var model = new ContactsModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Contacts(ContactsModel model, string cmd = "")
        {
            switch (cmd)
            {
                default:
                    return RedirectToActionPermanent("BackgroundCheck");
            }
            // return View(model);
        }

        // Background Check
        [HttpGet]
        public ActionResult BackgroundCheck()
        {
            var model = new BackgroundCheckModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult BackgroundCheck(BackgroundCheckModel model, string cmd = "")
        {
            switch (cmd)
            {
                default:
                    return RedirectToActionPermanent("OnsiteTraining");
            }
            // return View(model);
        }

        // Onsite Training
        [HttpGet]
        public ActionResult OnsiteTraining()
        {
            var model = new OnsiteTrainingModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult OnsiteTraining(OnsiteTrainingModel model, string cmd = "")
        {
            switch (cmd)
            {
                default:
                    return RedirectToActionPermanent("ApplicationLetter");
            }
            // return View(model);
        }

        // Application Letter
        [HttpGet]
        public ActionResult ApplicationLetter()
        {
            var model = new ApplicationLetterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult ApplicationLetter(ApplicationLetterModel model, string cmd = "")
        {
            return View(model);
        }

        // Filled out by managers
        [HttpGet]
        public ActionResult Managers()
        {
            var model = new ManagersModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Managers(ManagersModel model, string cmd = "")
        {
            return View(model);
        }

        // Filled out by personnel managers
        [HttpGet]
        public ActionResult PersonnelManagers()
        {
            var model = new PersonnelManagersModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult PersonnelManagers(PersonnelManagersModel model, string cmd = "")
        {
            return View(model);
        }

        // Employment roster
        [HttpGet]
        public ActionResult Roster()
        {
            var model = new RosterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Roster(RosterModel model, string cmd = "")
        {
            return View(model);
        }

        // Custom report
        [HttpGet]
        public ActionResult CustomReport()
        {
            var model = new CustomReportModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult CustomReport(CustomReportModel model, string cmd = "")
        {
            return View(model);
        }

        // Signers
        [HttpGet]
        public ActionResult Signers()
        {
            var model = new SignersModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Signers(SignersModel model, string cmd = "")
        {
            return View(model);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reports.Presenters.UI.ViewModel.Employment2;
using Reports.Core.Dto.Employment2;

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

        // General Info

        public ActionResult GeneralInfo()
        {
            var model = new GeneralInfoModel();            
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
                    break;
            }
            return View(model);
        }

        // Passport

        public ActionResult Passport()
        {
            var model = new PassportModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Passport(PassportModel model, string cmd = "")
        {
            return View(model);
        }

        // Education

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
                    break;
            }

            return View(model);
        }

        // Family

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
                    break;
            }
            return View(model);
        }

        // Military Service

        public ActionResult MilitaryService()
        {
            var model = new MilitaryServiceModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult MilitaryService(MilitaryServiceModel model, string cmd = "")
        {
            return View(model);
        }

        // Experience

        public ActionResult Experience()
        {
            var model = new ExperienceModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Experience(ExperienceModel model, string cmd = "")
        {
            return View(model);
        }

        // Contacts

        public ActionResult Contacts()
        {
            var model = new ContactsModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Contacts(ContactsModel model, string cmd = "")
        {
            return View(model);
        }

        // Background Check

        public ActionResult BackgroundCheck()
        {
            var model = new BackgroundCheckModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult BackgroundCheck(BackgroundCheckModel model, string cmd = "")
        {
            return View(model);
        }

        // Training

        public ActionResult Training()
        {
            var model = new TrainingModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Training(TrainingModel model, string cmd = "")
        {
            return View(model);
        }

        // Application Letter

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

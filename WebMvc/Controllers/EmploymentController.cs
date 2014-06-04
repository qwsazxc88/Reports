﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reports.Presenters.UI.ViewModel.Employment2;
using Reports.Core.Dto.Employment2;
using Reports.Core;
using Reports.Core.Domain;
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
        public ActionResult Index(int? userId)
        {
            return RedirectToActionPermanent("GeneralInfo");
        }

        // General Info
        [HttpGet]
        public ActionResult GeneralInfo(int? userId)
        {
            var model = EmploymentBl.GetGeneralInfoModel(userId);
            return View(model);
        }

        [HttpPost]
        public ActionResult GeneralInfo(GeneralInfoModel model, string cmd = "")
        {
            string error = String.Empty;

            switch (cmd)
            {
                case "add-name-change":
                    model.NameChanges.Add(new NameChangeDto());
                    break;
                default:
                    EmploymentBl.SaveModel<GeneralInfoModel, GeneralInfo>(model, out error);
                    return RedirectToActionPermanent("Passport");
            }
            return View(model);
        }

        // Passport
        [HttpGet]
        public ActionResult Passport(int? userId)
        {
            var model = EmploymentBl.GetPassportModel(userId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Passport(PassportModel model, string cmd = "")
        {
            string error = String.Empty;

            switch (cmd)
            {
                default:
                    EmploymentBl.SaveModel<PassportModel, Passport>(model, out error);
                    return RedirectToActionPermanent("Education");
            }
            // return View(model);
        }

        // Education
        [HttpGet]
        public ActionResult Education(int? userId)
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
        public ActionResult Family(int? userId)
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
        public ActionResult MilitaryService(int? userId)
        {
            var model = EmploymentBl.GetMilitaryServiceModel(userId);
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
        public ActionResult Experience(int? userId)
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
        public ActionResult Contacts(int? userId)
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
        public ActionResult BackgroundCheck(int? userId)
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
        public ActionResult OnsiteTraining(int? userId)
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
        public ActionResult ApplicationLetter(int? userId)
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
        public ActionResult Managers(int? userId)
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
        public ActionResult PersonnelManagers(int? userId)
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

        #region Model Validation

        protected bool ValidateModel(GeneralInfoModel model)
        {
            
            return ModelState.IsValid;
        }

        protected bool ValidateModel(PassportModel model)
        {

            return ModelState.IsValid;
        }

        protected bool ValidateModel(EducationModel model)
        {

            return ModelState.IsValid;
        }

        protected bool ValidateModel(FamilyModel model)
        {

            return ModelState.IsValid;
        }

        protected bool ValidateModel(MilitaryServiceModel model)
        {

            return ModelState.IsValid;
        }

        protected bool ValidateModel(ExperienceModel model)
        {

            return ModelState.IsValid;
        }

        protected bool ValidateModel(ContactsModel model)
        {

            return ModelState.IsValid;
        }

        protected bool ValidateModel(BackgroundCheckModel model)
        {

            return ModelState.IsValid;
        }

        protected bool ValidateModel(OnsiteTrainingModel model)
        {

            return ModelState.IsValid;
        }

        protected bool ValidateModel(ManagersModel model)
        {

            return ModelState.IsValid;
        }

        protected bool ValidateModel(PersonnelManagersModel model)
        {

            return ModelState.IsValid;
        }

        protected bool ValidateModel(RosterModel model)
        {

            return ModelState.IsValid;
        }

        protected bool ValidateModel(SignersModel model)
        {

            return ModelState.IsValid;
        }

        #endregion
    }
}

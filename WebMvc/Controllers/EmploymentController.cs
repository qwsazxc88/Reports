using System;
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
using WebMvc.Attributes;

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
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult GeneralInfo(int? userId)
        {
            var model = EmploymentBl.GetGeneralInfoModel(userId);
            return model.IsFinal? View("GeneralInfoReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult GeneralInfo(GeneralInfoModel model)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.SaveModel<GeneralInfoModel, GeneralInfo>(model, out error);
                ViewBag.Error = error;
            }
            model = EmploymentBl.GetGeneralInfoModel();
            return model.IsFinal ? View("GeneralInfoReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult GeneralInfoAddNameChange(NameChangeDto itemToAdd)
        {
            string error = String.Empty;

            GeneralInfoModel model = EmploymentBl.GetGeneralInfoModel();
            model.NameChanges.Add(itemToAdd);
            EmploymentBl.SaveModel<GeneralInfoModel, GeneralInfo>(model, out error);
            ViewBag.Error = error;

            model = EmploymentBl.GetGeneralInfoModel();
            return View("GeneralInfo", model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult GeneralInfoAddForeignLanguage(ForeignLanguageDto itemToAdd)
        {
            string error = String.Empty;

            GeneralInfoModel model = EmploymentBl.GetGeneralInfoModel();
            model.ForeignLanguages.Add(itemToAdd);
            EmploymentBl.SaveModel<GeneralInfoModel, GeneralInfo>(model, out error);
            ViewBag.Error = error;

            model = EmploymentBl.GetGeneralInfoModel();
            return View("GeneralInfo", model);
        }

        // Passport
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult Passport(int? userId)
        {
            var model = EmploymentBl.GetPassportModel(userId);
            return model.IsFinal ? View("PassportReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult Passport(PassportModel model)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.SaveModel<PassportModel, Passport>(model, out error);
                ViewBag.Error = error;
            }
            model = EmploymentBl.GetPassportModel();
            return model.IsFinal ? View("PassportReadOnly", model) : View(model);
        }

        // Education
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult Education(int? userId)
        {
            var model = EmploymentBl.GetEducationModel(userId);
            return model.IsFinal ? View("EducationReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult Education(EducationModel model)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.SaveModel<EducationModel, Education>(model, out error);
                ViewBag.Error = error;
            }
            model = EmploymentBl.GetEducationModel();
            return model.IsFinal ? View("EducationReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult EducationAddCertification(CertificationDto itemToAdd)
        {
            string error = String.Empty;

            EducationModel model = EmploymentBl.GetEducationModel();
            model.Certifications.Add(itemToAdd);
            EmploymentBl.SaveModel<EducationModel, Education>(model, out error);

            model = EmploymentBl.GetEducationModel();
            return View("Education", model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult EducationAddHigherEducationDiploma(HigherEducationDiplomaDto itemToAdd)
        {
            string error = String.Empty;

            EducationModel model = EmploymentBl.GetEducationModel();
            model.HigherEducationDiplomas.Add(itemToAdd);
            EmploymentBl.SaveModel<EducationModel, Education>(model, out error);

            model = EmploymentBl.GetEducationModel();
            return View("Education", model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult EducationAddPostGraduateEducationDiploma(PostGraduateEducationDiplomaDto itemToAdd)
        {
            string error = String.Empty;

            EducationModel model = EmploymentBl.GetEducationModel();
            model.PostGraduateEducationDiplomas.Add(itemToAdd);
            EmploymentBl.SaveModel<EducationModel, Education>(model, out error);

            model = EmploymentBl.GetEducationModel();
            return View("Education", model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult EducationAddTraining(TrainingDto itemToAdd)
        {
            string error = String.Empty;

            EducationModel model = EmploymentBl.GetEducationModel();
            model.Training.Add(itemToAdd);
            EmploymentBl.SaveModel<EducationModel, Education>(model, out error);

            model = EmploymentBl.GetEducationModel();
            return View("Education", model);
        }

        // Family
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult Family(int? userId)
        {
            var model = EmploymentBl.GetFamilyModel(userId);
            return model.IsFinal ? View("FamilyReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult Family(FamilyModel model)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.SaveModel<FamilyModel, Family>(model, out error);
                ViewBag.Error = error;
            }
            model = EmploymentBl.GetFamilyModel();
            return model.IsFinal ? View("FamilyReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult FamilyAddChild (FamilyMemberDto itemToAdd)
        {
            string error = String.Empty;

            FamilyModel model = EmploymentBl.GetFamilyModel();
            model.Children.Add(itemToAdd);
            EmploymentBl.SaveModel<FamilyModel, Family>(model, out error);

            model = EmploymentBl.GetFamilyModel();
            return View("Family", model);
        }

        // Military Service
        [HttpGet]
        public ActionResult MilitaryService(int? userId)
        {
            var model = EmploymentBl.GetMilitaryServiceModel(userId);
            return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult MilitaryService(MilitaryServiceModel model)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.SaveModel<MilitaryServiceModel, MilitaryService>(model, out error);
                return RedirectToActionPermanent("Experience");
            }
            else
            {
                model = EmploymentBl.GetMilitaryServiceModel();
                return View(model);
            }
        }

        // Experience
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult Experience(int? userId)
        {
            var model = EmploymentBl.GetExperienceModel(userId);
            return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult Experience(ExperienceModel model)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.SaveModel<ExperienceModel, Experience>(model, out error);
                return RedirectToActionPermanent("Contacts");
            }
            else
            {
                model = EmploymentBl.GetExperienceModel();
                return View(model);
            }
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult ExperienceAddExperienceItem(ExperienceItemDto itemToAdd)
        {
            string error = String.Empty;

            ExperienceModel model = EmploymentBl.GetExperienceModel();
            model.ExperienceItems.Add(itemToAdd);
            EmploymentBl.SaveModel<ExperienceModel, Experience>(model, out error);

            return RedirectToActionPermanent("Experience");
        }

        // Contacts
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult Contacts(int? userId)
        {
            var model = EmploymentBl.GetContactsModel(userId);
            return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult Contacts(ContactsModel model)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.SaveModel<ContactsModel, Contacts>(model, out error);
                return RedirectToActionPermanent("BackgroundCheck");
            }
            else
            {
                model = EmploymentBl.GetContactsModel();
                return View(model);
            }
        }

        // Background Check
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult BackgroundCheck(int? userId)
        {
            var model = EmploymentBl.GetBackgroundCheckModel(userId);
            return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult BackgroundCheck(BackgroundCheckModel model, string cmd = "")
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.SaveModel<BackgroundCheckModel, BackgroundCheck>(model, out error);
            }
            model = EmploymentBl.GetBackgroundCheckModel();
            return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult BackgroundCheckAddReference(ReferenceDto itemToAdd)
        {
            string error = String.Empty;

            BackgroundCheckModel model = EmploymentBl.GetBackgroundCheckModel();
            model.References.Add(itemToAdd);
            EmploymentBl.SaveModel<BackgroundCheckModel, BackgroundCheck>(model, out error);

            return RedirectToActionPermanent("BackgroundCheck");
        }

        // Onsite Training
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Trainer | UserRole.PersonnelManager | UserRole.OutsourcingManager)]
        public ActionResult OnsiteTraining(int? userId)
        {
            var model = EmploymentBl.GetOnsiteTrainingModel(userId);
            return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Trainer)]
        public ActionResult OnsiteTraining(OnsiteTrainingModel model)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.SaveModel<OnsiteTrainingModel, OnsiteTraining>(model, out error);
            }
            model = EmploymentBl.GetOnsiteTrainingModel();
            return View(model);
        }

        // Application Letter
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult ApplicationLetter(int? userId)
        {
            var model = new ApplicationLetterModel();
            return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult ApplicationLetter(ApplicationLetterModel model, string cmd = "")
        {
            // EmploymentBl.SaveModel<ApplicationLetterModel, ApplicationLetter>(model, out error);
            return View(model);
        }

        // Filled out by managers
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.PersonnelManager | UserRole.OutsourcingManager)]
        public ActionResult Managers(int? userId)
        {
            var model = EmploymentBl.GetManagersModel(userId);
            return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Manager)]
        public ActionResult Managers(ManagersModel model)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.SaveModel<ManagersModel, Managers>(model, out error);
            }
            model = EmploymentBl.GetManagersModel();
            return View(model);
        }

        // Filled out by personnel managers
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.PersonnelManager | UserRole.OutsourcingManager)]
        public ActionResult PersonnelManagers(int? userId)
        {
            var model = EmploymentBl.GetPersonnelManagersModel(userId);
            return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.PersonnelManager | UserRole.OutsourcingManager)]
        public ActionResult PersonnelManagers(PersonnelManagersModel model)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.SaveModel<PersonnelManagersModel, PersonnelManagers>(model, out error);
            }
            model = EmploymentBl.GetPersonnelManagersModel();
            return View(model);
        }

        // Employment roster
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.Trainer | UserRole.PersonnelManager | UserRole.OutsourcingManager)]
        public ActionResult Roster()
        {
            var model = EmploymentBl.GetRosterModel();
            return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.Trainer | UserRole.PersonnelManager | UserRole.OutsourcingManager)]
        public ActionResult Roster(RosterModel model)
        {
            model = EmploymentBl.GetRosterModel();
            return View(model);
        }

        // Custom report
        /*
        [HttpGet]
        public ActionResult CustomReport()
        {
            var model = new CustomReportModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult CustomReport(CustomReportModel model)
        {
            return View(model);
        }
        */

        // Signers
        [HttpGet]
        public ActionResult Signers()
        {
            var model = new SignersModel();
            return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult Signers(SignersModel model)
        {
            return View(model);
        }

        #region Model Validation

        [NonAction]
        protected bool ValidateModel(GeneralInfoModel model)
        {
            // Если не установлен флаг отсутствия отчества, то отчество должно быть заполнено
            if (!model.IsPatronymicAbsent && (model.Patronymic == null || model.Patronymic.Length == 0))
            {
                ModelState.AddModelError("Patronymic", "Обязательное поле, если не отмечен флаг \"Отчество отсутствует\"");
            }
            // Вид застрахованного лица только для граждан других государств
            // Срок действия справки не может быть меньше текущего дня
            if (model.DisabilityCertificateExpirationDate.HasValue && model.DisabilityCertificateExpirationDate < DateTime.Now)
            {
                ModelState.AddModelError("DisabilityCertificateExpirationDate", "Некорректный срок действия справки");
            }
            return ModelState.IsValid;
        }

        [NonAction]
        protected bool ValidateModel(PassportModel model)
        {

            return ModelState.IsValid;
        }

        [NonAction]
        protected bool ValidateModel(EducationModel model)
        {
            // Год поступления не больше года окончания
            return ModelState.IsValid;
        }

        [NonAction]
        protected bool ValidateModel(FamilyModel model)
        {
            // 
            return ModelState.IsValid;
        }

        [NonAction]
        protected bool ValidateModel(MilitaryServiceModel model)
        {

            return ModelState.IsValid;
        }

        [NonAction]
        protected bool ValidateModel(ExperienceModel model)
        {

            return ModelState.IsValid;
        }

        [NonAction]
        protected bool ValidateModel(ContactsModel model)
        {

            return ModelState.IsValid;
        }

        [NonAction]
        protected bool ValidateModel(BackgroundCheckModel model)
        {

            return ModelState.IsValid;
        }

        [NonAction]
        protected bool ValidateModel(OnsiteTrainingModel model)
        {

            return ModelState.IsValid;
        }

        [NonAction]
        protected bool ValidateModel(ManagersModel model)
        {

            return ModelState.IsValid;
        }

        [NonAction]
        protected bool ValidateModel(PersonnelManagersModel model)
        {

            return ModelState.IsValid;
        }

        [NonAction]
        protected bool ValidateModel(RosterModel model)
        {

            return ModelState.IsValid;
        }

        [NonAction]
        protected bool ValidateModel(SignersModel model)
        {

            return ModelState.IsValid;
        }

        #endregion
    }
}

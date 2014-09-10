using System;
using System.IO;
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
using Reports.Presenters.Services.Impl;
using Reports.Core.Dto;

namespace WebMvc.Controllers
{
    public class EmploymentController : BaseController
    {
        public const int MaxFileSize = 2 * 1024 * 1024;

        protected int RUSSIAN_FEDERATION = 643;        
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
        [ActionName("Index")]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.Trainer | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult Index()
        {
            return RedirectToAction(EmploymentBl.GetStartView());
        }
        
        // General Info
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult GeneralInfo(int? id)
        {
            var model = EmploymentBl.GetGeneralInfoModel(id);
            return (model.IsFinal || id.HasValue) ? View("GeneralInfoReadOnly", model) : View(model);
        }
        
        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult GeneralInfo(GeneralInfoModel model)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.ProcessSaving<GeneralInfoModel, GeneralInfo>(model, out error);
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
            EmploymentBl.ProcessSaving<GeneralInfoModel, GeneralInfo>(model, out error);
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
            EmploymentBl.ProcessSaving<GeneralInfoModel, GeneralInfo>(model, out error);
            ViewBag.Error = error;

            model = EmploymentBl.GetGeneralInfoModel();
            return View("GeneralInfo", model);
        }

        // Passport
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult Passport(int? id)
        {
            var model = EmploymentBl.GetPassportModel(id);
            return (model.IsFinal || id.HasValue) ? View("PassportReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult Passport(PassportModel model, IEnumerable<HttpPostedFileBase> files)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.ProcessSaving<PassportModel, Passport>(model, out error);
                ViewBag.Error = error;
            }
            model = EmploymentBl.GetPassportModel();
            return model.IsFinal ? View("PassportReadOnly", model) : View(model);
        }

        // Education
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult Education(int? id)
        {
            var model = EmploymentBl.GetEducationModel(id);
            return (model.IsFinal || id.HasValue) ? View("EducationReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult Education(EducationModel model)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.ProcessSaving<EducationModel, Education>(model, out error);
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
            EmploymentBl.ProcessSaving<EducationModel, Education>(model, out error);

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
            EmploymentBl.ProcessSaving<EducationModel, Education>(model, out error);

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
            EmploymentBl.ProcessSaving<EducationModel, Education>(model, out error);

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
            EmploymentBl.ProcessSaving<EducationModel, Education>(model, out error);

            model = EmploymentBl.GetEducationModel();
            return View("Education", model);
        }

        // Family
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult Family(int? id)
        {
            var model = EmploymentBl.GetFamilyModel(id);
            return (model.IsFinal || id.HasValue) ? View("FamilyReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult Family(FamilyModel model, IEnumerable<HttpPostedFileBase> files)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.ProcessSaving<FamilyModel, Family>(model, out error);
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
            EmploymentBl.ProcessSaving<FamilyModel, Family>(model, out error);

            model = EmploymentBl.GetFamilyModel();
            return View("Family", model);
        }

        // Military Service
        [HttpGet]
        public ActionResult MilitaryService(int? id)
        {
            var model = EmploymentBl.GetMilitaryServiceModel(id);
            return (model.IsFinal || id.HasValue) ? View("MilitaryServiceReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult MilitaryService(MilitaryServiceModel model, IEnumerable<HttpPostedFileBase> files)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.ProcessSaving<MilitaryServiceModel, MilitaryService>(model, out error);
                ViewBag.Error = error;
            }
            model = EmploymentBl.GetMilitaryServiceModel();
            return model.IsFinal ? View("MilitaryServiceReadOnly", model) : View(model);
        }

        // Experience
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult Experience(int? id)
        {
            var model = EmploymentBl.GetExperienceModel(id);
            return (model.IsFinal || id.HasValue) ? View("ExperienceReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult Experience(ExperienceModel model, IEnumerable<HttpPostedFileBase> files)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.ProcessSaving<ExperienceModel, Experience>(model, out error);
                ViewBag.Error = error;
            }
            model = EmploymentBl.GetExperienceModel();
            return model.IsFinal ? View("ExperienceReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult ExperienceAddExperienceItem(ExperienceItemDto itemToAdd)
        {
            string error = String.Empty;

            ExperienceModel model = EmploymentBl.GetExperienceModel();
            model.ExperienceItems.Add(itemToAdd);
            EmploymentBl.ProcessSaving<ExperienceModel, Experience>(model, out error);

            model = EmploymentBl.GetExperienceModel();
            return View("Experience", model);
        }

        // Contacts
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult Contacts(int? id)
        {
            var model = EmploymentBl.GetContactsModel(id);
            return (model.IsFinal || id.HasValue) ? View("ContactsReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult Contacts(ContactsModel model)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.ProcessSaving<ContactsModel, Contacts>(model, out error);
                ViewBag.Error = error;
            }
            model = EmploymentBl.GetContactsModel();
            return model.IsFinal ? View("ContactsReadOnly", model) : View(model);
        }

        // Background Check
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult BackgroundCheck(int? id)
        {
            var model = EmploymentBl.GetBackgroundCheckModel(id);
            return (model.IsFinal || id.HasValue) ? View("BackgroundCheckReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult BackgroundCheck(BackgroundCheckModel model, IEnumerable<HttpPostedFileBase> files)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.ProcessSaving<BackgroundCheckModel, BackgroundCheck>(model, out error);
                ViewBag.Error = error;
            }
            model = EmploymentBl.GetBackgroundCheckModel();
            return model.IsFinal ? View("BackgroundCheckReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult BackgroundCheckAddReference(ReferenceDto itemToAdd)
        {
            string error = String.Empty;

            BackgroundCheckModel model = EmploymentBl.GetBackgroundCheckModel();
            model.References.Add(itemToAdd);
            EmploymentBl.ProcessSaving<BackgroundCheckModel, BackgroundCheck>(model, out error);

            model = EmploymentBl.GetBackgroundCheckModel();
            return View("BackgroundCheck", model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Security)]
        public ActionResult BackgroundCheckApprove(int userId)
        {
            string error = String.Empty;

            EmploymentBl.ApproveBackgroundCheck(userId, out error);
            ViewBag.Error = error;

            BackgroundCheckModel model = EmploymentBl.GetBackgroundCheckModel();            
            return model.IsFinal ? View("BackgroundCheckReadOnly", model) : View("BackgroundCheck", model);
        }

        // Onsite Training
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Trainer | UserRole.PersonnelManager | UserRole.OutsourcingManager)]
        public ActionResult OnsiteTraining(int? id)
        {
            var model = EmploymentBl.GetOnsiteTrainingModel(id);
            return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Trainer)]
        public ActionResult OnsiteTraining(OnsiteTrainingModel model)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.SaveOnsiteTrainingReport(model, out error);
            }
            model = EmploymentBl.GetOnsiteTrainingModel();
            return View(model);
        }

        // Application Letter
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult ApplicationLetter(int? id)
        {
            var model = new ApplicationLetterModel();
            return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult ApplicationLetter(ApplicationLetterModel model, IEnumerable<HttpPostedFileBase> files)
        {
            // EmploymentBl.SaveModel<ApplicationLetterModel, ApplicationLetter>(model, out error);
            return View(model);
        }

        // Filled out by managers
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.PersonnelManager | UserRole.OutsourcingManager)]
        public ActionResult Managers(int? id)
        {
            var model = EmploymentBl.GetManagersModel(id);
            return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Manager)]
        public ActionResult Managers(ManagersModel model)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.ApproveCandidateByManager(model, out error);
            }
            model = EmploymentBl.GetManagersModel();
            return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Manager)]
        public ActionResult ManagersApproveByHigherManager(int userId)
        {
            string error = String.Empty;

            EmploymentBl.ApproveCandidateByHigherManager(userId, out error);
            ManagersModel model = EmploymentBl.GetManagersModel();
            return View(model);
        }

        // Filled out by personnel managers
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.PersonnelManager | UserRole.OutsourcingManager)]
        public ActionResult PersonnelManagers(int? id)
        {
            var model = EmploymentBl.GetPersonnelManagersModel(id);
            return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.PersonnelManager | UserRole.OutsourcingManager)]
        public ActionResult PersonnelManagers(PersonnelManagersModel model, IEnumerable<HttpPostedFileBase> files)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.SavePersonnelManagersReport(model, out error);
            }
            model = EmploymentBl.GetPersonnelManagersModel();
            return View(model);
        }

        // Employment roster
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.Trainer | UserRole.PersonnelManager | UserRole.OutsourcingManager)]
        public ActionResult Roster()
        {
            var model = EmploymentBl.GetRosterModel(null);
            return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.Trainer | UserRole.PersonnelManager | UserRole.OutsourcingManager)]
        public ActionResult Roster(RosterFiltersModel input, RosterModel roster, bool isApproveModified = false)
        {
            RosterModel model = EmploymentBl.GetRosterModel(input);
            /*
            string error = string.Empty;
            
            if (isApproveModified)
            {
                EmploymentBl.SaveApprovals(roster, out error);
            }
            */
            return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director)]
        public ActionResult RosterBulkApprove(IList<CandidateApprovalDto> roster)
        {
            string error = string.Empty;
            
            if (EmploymentBl.SaveApprovals(roster, out error))
            {
                return Json(new { ok = true });
            }
            else
            {
                return Json(new { ok = false, error = error });
            }
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
        protected void ValidateFileLength(HttpPostedFileBase postedFile, string inputName)
        {
            if (postedFile != null)
            {
                if (postedFile.ContentLength > MaxFileSize)
                {
                    ModelState.AddModelError(inputName, string.Format("Размер файла превышает допустимый ({0} Мб).", MaxFileSize / (1024 * 1024)));
                }
                if (postedFile.ContentLength == 0)
                {
                    ModelState.AddModelError(inputName, string.Format("Прикрепленный файл пуст."));
                }
            }

        }

        [NonAction]
        protected bool ValidateModel(GeneralInfoModel model)
        {
            // Если не установлен флаг отсутствия отчества, то отчество должно быть заполнено
            if (!model.IsPatronymicAbsent && string.IsNullOrEmpty(model.Patronymic))
            {
                ModelState.AddModelError("Patronymic", "Обязательное поле, если не отмечен флаг \"Отчество отсутствует\"");
            }
            // Вид застрахованного лица только для граждан других государств
            // Срок действия справки не может быть меньше текущего дня
            if (model.DisabilityCertificateExpirationDate.HasValue && model.DisabilityCertificateExpirationDate < DateTime.Now)
            {
                ModelState.AddModelError("DisabilityCertificateExpirationDate", "Некорректный срок действия справки");
            }

            if (model.InsuredPersonTypeId.HasValue && model.CitizenshipId == RUSSIAN_FEDERATION)
            {
                ModelState.AddModelError("InsuredPersonTypeId", "Заполняется только гражданами других государств");
            }
            if (!model.InsuredPersonTypeId.HasValue && model.CitizenshipId != RUSSIAN_FEDERATION)
            {
                ModelState.AddModelError("InsuredPersonTypeId", "*");
            }
            ValidateFileLength(model.PhotoFile, "PhotoFile");
            ValidateFileLength(model.INNScanFile, "INNScanFile");
            ValidateFileLength(model.SNILSScanFile, "SNILSScanFile");
            ValidateFileLength(model.DisabilityCertificateScanFile, "DisabilityCertificateScanFile");
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

        #region Attachments

        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public FileContentResult ViewAttachment(int id)
        {
            try
            {
                AttachmentModel model = EmploymentBl.GetFileContext(id);
                return File(model.Context, model.ContextType, model.FileName);
            }
            catch (Exception ex)
            {
                Log.Error("Error on ViewAttachment:", ex);
                throw;
            }
        }

        [HttpGet]
        public JsonResult DeleteAttachment(int id)
        {
            bool saveResult;
            string error;
            try
            {
                DeleteAttacmentModel model = new DeleteAttacmentModel { Id = id };
                saveResult = EmploymentBl.DeleteAttachment(model);
                error = model.Error;

            }
            catch (Exception ex)
            {
                Log.Error("Exception on DeleteAttachment:", ex);
                error = ex.GetBaseException().Message;
                saveResult = false;
            }
            return Json(new { Error = error, Result = saveResult });
        }

        #endregion
    }
}

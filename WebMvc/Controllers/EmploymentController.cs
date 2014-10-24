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
using System.Configuration;
using System.Text;
using System.Web.Security;
using System.Diagnostics;

namespace WebMvc.Controllers
{
    public class EmploymentController : BaseController
    {
        //public const int MaxFileSize = 2 * 1024 * 1024;

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

        // Create new candidate
        [HttpGet]
        public ActionResult CreateCandidate()
        {
            var model = EmploymentBl.GetCreateCandidateModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateCandidate(CreateCandidateModel model)
        {
            string error = string.Empty;

            if (ValidateModel(model))
            {
                model.UserId = EmploymentBl.CreateCandidate(model, out error);
                ViewBag.Error = error;
            }

            if (!string.IsNullOrEmpty(error))
            {
                ViewBag.Error = error;                
            }

            return View(model);
        }
        
        public ActionResult PrintCreatedCandidate(int userId)
        {
            string error = string.Empty;
            var model = EmploymentBl.GetPrintCreatedCandidateModel(userId, out error);
            if (!string.IsNullOrEmpty(error))
            {
                ViewBag.Error = error;
            }
            return View(model);
        }
        
        // General Info
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult GeneralInfo(int? id)
        {
            var model = EmploymentBl.GetGeneralInfoModel(id);
            return (model.IsFinal || id.HasValue) && !EmploymentBl.IsUnlimitedEditAvailable() ? View("GeneralInfoReadOnly", model) : View(model);
        }
        
        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
        public ActionResult GeneralInfo(GeneralInfoModel model)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.ProcessSaving<GeneralInfoModel, GeneralInfo>(model, out error);
                ViewBag.Error = error;
            }
            model = EmploymentBl.GetGeneralInfoModel();
            return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("GeneralInfoReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
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
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
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
            return (model.IsFinal || id.HasValue) && !EmploymentBl.IsUnlimitedEditAvailable() ? View("PassportReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
        public ActionResult Passport(PassportModel model, IEnumerable<HttpPostedFileBase> files)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.ProcessSaving<PassportModel, Passport>(model, out error);
                ViewBag.Error = error;
            }
            model = EmploymentBl.GetPassportModel();
            return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("PassportReadOnly", model) : View(model);
        }

        // Education
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult Education(int? id)
        {
            var model = EmploymentBl.GetEducationModel(id);
            return (model.IsFinal || id.HasValue) && !EmploymentBl.IsUnlimitedEditAvailable() ? View("EducationReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
        public ActionResult Education(EducationModel model)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.ProcessSaving<EducationModel, Education>(model, out error);
                ViewBag.Error = error;
            }
            model = EmploymentBl.GetEducationModel();
            return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("EducationReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
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
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
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
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
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
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
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
            return (model.IsFinal || id.HasValue) && !EmploymentBl.IsUnlimitedEditAvailable() ? View("FamilyReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
        public ActionResult Family(FamilyModel model, IEnumerable<HttpPostedFileBase> files)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.ProcessSaving<FamilyModel, Family>(model, out error);
                ViewBag.Error = error;
            }
            model = EmploymentBl.GetFamilyModel();
            return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("FamilyReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
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
            return (model.IsFinal || id.HasValue) && !EmploymentBl.IsUnlimitedEditAvailable() ? View("MilitaryServiceReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
        public ActionResult MilitaryService(MilitaryServiceModel model, IEnumerable<HttpPostedFileBase> files)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.ProcessSaving<MilitaryServiceModel, MilitaryService>(model, out error);
                ViewBag.Error = error;
            }
            model = EmploymentBl.GetMilitaryServiceModel();
            return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("MilitaryServiceReadOnly", model) : View(model);
        }

        // Experience
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult Experience(int? id)
        {
            var model = EmploymentBl.GetExperienceModel(id);
            return (model.IsFinal || id.HasValue) && !EmploymentBl.IsUnlimitedEditAvailable() ? View("ExperienceReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
        public ActionResult Experience(ExperienceModel model, IEnumerable<HttpPostedFileBase> files)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.ProcessSaving<ExperienceModel, Experience>(model, out error);
                ViewBag.Error = error;
            }
            model = EmploymentBl.GetExperienceModel();
            return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("ExperienceReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
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
            return (model.IsFinal || id.HasValue) && !EmploymentBl.IsUnlimitedEditAvailable() ? View("ContactsReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
        public ActionResult Contacts(ContactsModel model)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.ProcessSaving<ContactsModel, Contacts>(model, out error);
                ViewBag.Error = error;
            }
            model = EmploymentBl.GetContactsModel();
            return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("ContactsReadOnly", model) : View(model);
        }

        // Background Check
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult BackgroundCheck(int? id)
        {
            var model = EmploymentBl.GetBackgroundCheckModel(id);
            return (model.IsFinal || id.HasValue) && !EmploymentBl.IsUnlimitedEditAvailable() ? View("BackgroundCheckReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
        public ActionResult BackgroundCheck(BackgroundCheckModel model, IEnumerable<HttpPostedFileBase> files)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.ProcessSaving<BackgroundCheckModel, BackgroundCheck>(model, out error);
                ViewBag.Error = error;
            }
            model = EmploymentBl.GetBackgroundCheckModel();
            return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("BackgroundCheckReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
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
        public ActionResult BackgroundCheckApprove(int userId, bool? approvalStatus)
        {
            string error = String.Empty;

            EmploymentBl.ApproveBackgroundCheck(userId, approvalStatus, out error);

            if (!string.IsNullOrEmpty(error))
            {
                ViewBag.Error = error;
                BackgroundCheckModel model = EmploymentBl.GetBackgroundCheckModel();
                return View("BackgroundCheckReadOnly", model);
            }
            else
            {
                return RedirectToAction("Roster");
            }
        }

        // Onsite Training
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Trainer | UserRole.PersonnelManager | UserRole.OutsourcingManager)]
        public ActionResult OnsiteTraining(int? id)
        {
            var model = EmploymentBl.GetOnsiteTrainingModel(id);
            return model.ApprovalStatus.HasValue ? View("OnsiteTrainingReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Trainer)]
        public ActionResult OnsiteTraining(OnsiteTrainingModel model)
        {
            string error = String.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.SaveOnsiteTrainingReport(model, out error);

                if (!string.IsNullOrEmpty(error))
                {
                    ViewBag.Error = error;
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Roster");
                }
            }

            return View(model);
        }

        // Application Letter
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult ApplicationLetter(int? id)
        {
            var model = EmploymentBl.GetApplicationLetterModel(id);
            return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate)]
        public ActionResult ApplicationLetter(ApplicationLetterModel model)
        {
            string error = string.Empty;

            if (ValidateModel(model))
            {
                EmploymentBl.ProcessSaving(model, out error);
            }
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
                EmploymentBl.ProcessSaving<ManagersModel, Managers>(model, out error);
                EmploymentBl.ApproveCandidateByManager(model, out error);
            }
            if (!string.IsNullOrEmpty(error))
            {
                ViewBag.Error = error;
                return View(model);
            }
            else
            {
                return RedirectToAction("Roster");
            }
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Manager)]
        public ActionResult ManagersApproveByHigherManager(int userId, bool? higherManagerApprovalStatus)
        {
            string error = String.Empty;

            EmploymentBl.ApproveCandidateByHigherManager(userId, higherManagerApprovalStatus, out error);
            if (!string.IsNullOrEmpty(error))
            {
                ViewBag.Error = error;
                return View("Managers", EmploymentBl.GetManagersModel(userId));
            }
            else
            {
                return RedirectToAction("Roster");
            }
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
            if (!string.IsNullOrEmpty(error))
            {
                ViewBag.Error = error;
                return View(model);
            }
            else
            {
                return RedirectToAction("Roster");
            }
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

        protected bool ValidateModel(CreateCandidateModel model)
        {
            int numberOfFilledFields = 0;

            numberOfFilledFields += string.IsNullOrEmpty(model.PassportData) ? 0 : 1;
            numberOfFilledFields += string.IsNullOrEmpty(model.SNILS) ? 0 : 1;
            numberOfFilledFields += model.DateOfBirth.HasValue ? 1 : 0;

            if (numberOfFilledFields < 2)
            {
                ModelState.AddModelError(null, "Необходимо заполнить хотя бы 2 поля личных данных.");
            }

            return ModelState.IsValid;
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
        protected bool ValidateModel(ApplicationLetterModel model)
        {
            ValidateFileLength(model.ApplicationLetterScanFile, "ApplicationLetterScanFile");
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

        #region Print Forms

        // Обработка запросов на печать кадровых документов

        [HttpGet]
        public ActionResult GetPrintContractForm(int userId)
        {
            return GetPrintForm("PrintContractForm", userId);
        }

        [HttpGet]
        public ActionResult GetPrintEmploymentOrder(int userId)
        {
            return GetPrintForm("PrintEmploymentOrder", userId);
        }

        // Обработка запросов от конвертера PDF

        [HttpGet]
        public ActionResult PrintContractForm(int userId)
        {
            PrintContractFormModel model = EmploymentBl.GetPrintContractFormModel(userId);
            return View(model);
        }
        
        [HttpGet]
        public ActionResult PrintEmploymentOrder(int userId)
        {
            PrintEmploymentOrderModel model = EmploymentBl.GetPrintEmploymentOrderModel(userId);
            return View(model);
        }

        // Создание PDF

        [NonAction]
        public ActionResult GetPrintForm(string actionName, int userId, bool isLandscape = false)
        {
            string filePath = null;
            try
            {
                var folderPath = ConfigurationManager.AppSettings["PresentationFolderPath"];
                var fileName = string.Format("{0}.pdf", Guid.NewGuid());

                folderPath = HttpContext.Server.MapPath(folderPath);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                filePath = Path.Combine(folderPath, fileName);

                var arguments = new StringBuilder();

                var cookieName = FormsAuthentication.FormsCookieName;
                var authCookie = Request.Cookies[cookieName];
                if (authCookie == null || authCookie.Value == null)
                    throw new ArgumentException("Ошибка авторизации.");
                if (isLandscape)
                    arguments.AppendFormat(" --orientation Landscape {0}  --cookie {1} {2}",
                        GetConverterCommandParam(actionName, userId), cookieName, authCookie.Value);
                else
                    arguments.AppendFormat("{0} --cookie {1} {2}",
                        GetConverterCommandParam(actionName, userId), cookieName, authCookie.Value);
                arguments.AppendFormat(" \"{0}\"", filePath);
                var serverSideProcess = new Process
                {
                    StartInfo =
                    {
                        FileName = ConfigurationManager.AppSettings["PdfConverterCommandLineTemplate"],
                        Arguments = arguments.ToString(),
                        UseShellExecute = true,
                    },
                    EnableRaisingEvents = true,

                };
                serverSideProcess.Start();
                serverSideProcess.WaitForExit();
                return GetFile(Response, Request, Server, filePath, fileName, @"application/pdf", actionName + ".pdf");
            }
            catch (Exception ex)
            {
                Log.Error("Exception on GetPrintForm", ex);
                throw;
            }
            finally
            {
                if (!string.IsNullOrEmpty(filePath) && System.IO.File.Exists(filePath))
                {
                    try
                    {
                        System.IO.File.Delete(filePath);
                    }
                    catch (Exception ex)
                    {
                        Log.Warn(string.Format("Exception on delete file {0}", filePath), ex);
                    }
                }
            }
        }

        [NonAction]
        protected virtual string GetConverterCommandParam(string actionName, int userId)
        {
            var localhostUrl = ConfigurationManager.AppSettings["localhost"];
            string urlTemplate = string.Format("Employment/{0}?userId={1}", actionName, userId);
            return !string.IsNullOrEmpty(localhostUrl)
                ? string.Format(@"{0}/{1}", localhostUrl, urlTemplate)
                : Url.Content(string.Format(@"{0}", urlTemplate));
        }

        // Получение созданного PDF

        [NonAction]
        protected static ActionResult GetFile(HttpResponseBase Response, HttpRequestBase Request, HttpServerUtilityBase Server,
            string filePath, string fileName, string contentType, string userFileName)
        {
            byte[] value;
            using (FileStream stream = System.IO.File.Open(filePath, FileMode.Open))
            {
                value = new byte[stream.Length];
                stream.Read(value, 0, (int)stream.Length);
            }

            Response.Clear();
            if (Request.Browser.Browser == "IE")
            {
                string attachment = String.Format("attachment; filename=\"{0}\"", Server.UrlPathEncode(userFileName));
                Response.AddHeader("Content-Disposition", attachment);
            }
            else
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + userFileName + "\"");

            Response.ContentType = contentType;
            Response.Charset = "utf-8";
            Response.HeaderEncoding = Encoding.UTF8;
            Response.ContentEncoding = Encoding.UTF8;
            Response.BinaryWrite(value);
            Response.End();
            return null;
        }

        #endregion
    }
}

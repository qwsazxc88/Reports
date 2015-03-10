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
using System.Web.Script.Serialization;

namespace WebMvc.Controllers
{
    public class EmploymentController : BaseController
    {
        //public const int MaxFileSize = 2 * 1024 * 1024;

        #region Constants
        protected int RUSSIAN_FEDERATION = 643; 
        #endregion
        
        #region Dependencies
        protected IEmploymentBl employmentBl;

        public IEmploymentBl EmploymentBl
        {
            get
            {
                employmentBl = Ioc.Resolve<IEmploymentBl>();
                return Validate.Dependency(employmentBl);
            }
        } 
        #endregion

        #region Main Actions

        #region Index
        [HttpGet]
        [ActionName("Index")]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.Trainer | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult Index()
        {
            return RedirectToAction(EmploymentBl.GetStartView());
        } 
        #endregion
        
        #region Create new candidate
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
                //ViewBag.Error = error;
            }

            if (!string.IsNullOrEmpty(error))
            {
                //ViewBag.Error = error;
                ModelState.AddModelError("DepartmentId", error);
            }

            if (ModelState.Count != 0)
                model = EmploymentBl.GetCreateCandidateModel(model);

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
        #endregion

        #region General Info
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate | UserRole.Trainer)]
        public ActionResult GeneralInfo(int? id)
        {
            var model = EmploymentBl.GetGeneralInfoModel(id);
            return (model.IsFinal || id.HasValue) && !EmploymentBl.IsUnlimitedEditAvailable() ? View("GeneralInfoReadOnly", model) : View(model);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult GeneralInfoReadOnly(int? id)
        {
            GeneralInfoModel model = null;
            //для кадровиков на вкладках показываем анкету с полным функционалом, как у кандидата, в стадии черновика
            //такая же схема применяется для всех страниц анкеты
            if (Session["GeneralInfoM"] != null)
            {
                model = (GeneralInfoModel)Session["GeneralInfoM"];
                ModelState.Clear();
                for (int i = 0; i < ((ModelStateDictionary)Session["GeneralInfoMS"]).Count; i++)
                {
                    ModelState.Add(((ModelStateDictionary)Session["GeneralInfoMS"]).ElementAt(i));
                }

                Session.Remove("GeneralInfoM");
                Session.Remove("GeneralInfoMS");
            }
            else
                model = EmploymentBl.GetGeneralInfoModel(id);

            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
                return PartialView("GeneralInfo", model);
            else
                return PartialView(model);
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
                model = EmploymentBl.GetGeneralInfoModel(model.UserId);
            }
            else
            {   //так как при использования вкладок, страницу приходится перезагружать с потерей данных, то передаем модель с библиотекой ошибок через переменную сессии
                if (Session["GeneralInfoMS"] != null)
                    Session.Remove("GeneralInfoMS");
                if (Session["GeneralInfoMS"] == null)
                {
                    ModelStateDictionary mst = ModelState;
                    Session.Add("GeneralInfoMS", mst);
                }

                model = EmploymentBl.GetGeneralInfoModel(model);
                if (Session["GeneralInfoM"] != null)
                    Session.Remove("GeneralInfoM");
                if (Session["GeneralInfoM"] == null)
                    Session.Add("GeneralInfoM", model);
            }

            //для кадровиков при обновлении встаем на нужную вкладку
            //такая же схема применяется для всех страниц анкеты
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=0");
            else
                return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("GeneralInfoReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
        public ActionResult GeneralInfoAddNameChange(NameChangeDto itemToAdd, int? CandidateId)
        {
            string error = String.Empty;

            GeneralInfoModel model = EmploymentBl.GetGeneralInfoModel(CandidateId);
            model.NameChanges.Add(itemToAdd);
            EmploymentBl.ProcessSaving<GeneralInfoModel, GeneralInfo>(model, out error);
            ViewBag.Error = error;

            model = EmploymentBl.GetGeneralInfoModel(CandidateId);
            return Json(model.NameChanges);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
        public ActionResult GeneralInfoDeleteNameChange(int NameID, int? CandidateId)
        {
            string error = String.Empty;

            GeneralInfoModel model = EmploymentBl.GetGeneralInfoModel(CandidateId);
            EmploymentBl.DeleteNameChange(model, NameID);
            ViewBag.Error = error;

            model = EmploymentBl.GetGeneralInfoModel(CandidateId);
            return Json(model.NameChanges);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
        public ActionResult GeneralInfoAddForeignLanguage(ForeignLanguageDto itemToAdd, int? CandidateId)
        {
            string error = String.Empty;

            GeneralInfoModel model = EmploymentBl.GetGeneralInfoModel(CandidateId);
            model.ForeignLanguages.Add(itemToAdd);
            EmploymentBl.ProcessSaving<GeneralInfoModel, GeneralInfo>(model, out error);
            ViewBag.Error = error;

            model = EmploymentBl.GetGeneralInfoModel(CandidateId);
            return Json(model.ForeignLanguages);
        }
        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
        public ActionResult GeneralInfoDeleteForeignLanguage(int LanguageID, int? CandidateId)
        {
            string error = String.Empty;

            GeneralInfoModel model = EmploymentBl.GetGeneralInfoModel(CandidateId);
            EmploymentBl.DeleteLanguage(model, LanguageID);
            ViewBag.Error = error;

            model = EmploymentBl.GetGeneralInfoModel(CandidateId);
            return Json(model.ForeignLanguages);
        }
        #endregion

        #region Passport
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult Passport(int? id)
        {
            var model = EmploymentBl.GetPassportModel(id);
            return (model.IsFinal || id.HasValue) && !EmploymentBl.IsUnlimitedEditAvailable() ? View("PassportReadOnly", model) : View(model);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult PassportReadOnly(int? id)
        {
            PassportModel model = null;
            if (Session["PassportM"] != null)
            {
                model = (PassportModel)Session["PassportM"];
                ModelState.Clear();
                for (int i = 0; i < ((ModelStateDictionary)Session["PassportMS"]).Count; i++)
                {
                    ModelState.Add(((ModelStateDictionary)Session["PassportMS"]).ElementAt(i));
                }

                Session.Remove("PassportM");
                Session.Remove("PassportMS");
            }
            else
                model = EmploymentBl.GetPassportModel(id);

            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
                return PartialView("Passport", model);
            else
                return PartialView(model);
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
                model = EmploymentBl.GetPassportModel(model.UserId);
            }
            else
            {   //так как при использования вкладок, страницу приходится перезагружать с потерей данных, то передаем модель с библиотекой ошибок через переменную сессии
                if (Session["PassportMS"] != null)
                    Session.Remove("PassportMS");
                if (Session["PassportMS"] == null)
                {
                    ModelStateDictionary mst = ModelState;
                    Session.Add("PassportMS", mst);
                }

                model = EmploymentBl.GetPassportModel(model);
                if (Session["PassportM"] != null)
                    Session.Remove("PassportM");
                if (Session["PassportM"] == null)
                    Session.Add("PassportM", model);
            }
            
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=1");
            else
                return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("PassportReadOnly", model) : View(model);
        }
        #endregion

        #region Education
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult Education(int? id)
        {
            var model = EmploymentBl.GetEducationModel(id);
            return (model.IsFinal || id.HasValue) && !EmploymentBl.IsUnlimitedEditAvailable() ? View("EducationReadOnly", model) : View(model);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult EducationReadOnly(int? id)
        {
            var model = EmploymentBl.GetEducationModel(id);
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
                return PartialView("Education", model);
            else
                return PartialView(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
        public ActionResult Education(EducationModel model)
        {
            string error = String.Empty;
            if (model.Operation == 0)
            {
                if (ValidateModel(model))
                {
                    EmploymentBl.ProcessSaving<EducationModel, Education>(model, out error);
                    ViewBag.Error = error;
                }
            }
            else
            {
                EmploymentBl.DeleteEducationRow(model);
            }

            model = EmploymentBl.GetEducationModel(model.UserId);
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=2");
            else
                return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("EducationReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
        public ActionResult EducationAddCertification(CertificationDto itemToAdd)
        {
            string error = String.Empty;

            EducationModel model = EmploymentBl.GetEducationModel(itemToAdd.UserId);
            model.Certifications.Add(itemToAdd);
            EmploymentBl.ProcessSaving<EducationModel, Education>(model, out error);

            model = EmploymentBl.GetEducationModel(model.UserId);
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=2");
            else
                return View("Education", model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
        public ActionResult EducationAddHigherEducationDiploma(HigherEducationDiplomaDto itemToAdd)
        {
            string error = String.Empty;

            EducationModel model = EmploymentBl.GetEducationModel(itemToAdd.UserId);
            model.HigherEducationDiplomas.Add(itemToAdd);
            EmploymentBl.ProcessSaving<EducationModel, Education>(model, out error);

            model = EmploymentBl.GetEducationModel(model.UserId);
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=2");
            else
                return View("Education", model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
        public ActionResult EducationAddPostGraduateEducationDiploma(PostGraduateEducationDiplomaDto itemToAdd)
        {
            string error = String.Empty;

            EducationModel model = EmploymentBl.GetEducationModel(itemToAdd.UserId);
            model.PostGraduateEducationDiplomas.Add(itemToAdd);
            EmploymentBl.ProcessSaving<EducationModel, Education>(model, out error);

            model = EmploymentBl.GetEducationModel(model.UserId);
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=2");
            else
                return View("Education", model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
        public ActionResult EducationAddTraining(TrainingDto itemToAdd)
        {
            string error = String.Empty;

            EducationModel model = EmploymentBl.GetEducationModel(itemToAdd.UserId);
            model.Training.Add(itemToAdd);
            EmploymentBl.ProcessSaving<EducationModel, Education>(model, out error);

            model = EmploymentBl.GetEducationModel(model.UserId);
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=2");
            else
                return View("Education", model);
        }
        #endregion

        #region Family
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult Family(int? id)
        {
            var model = EmploymentBl.GetFamilyModel(id);
            return (model.IsFinal || id.HasValue) && !EmploymentBl.IsUnlimitedEditAvailable() ? View("FamilyReadOnly", model) : View(model);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult FamilyReadOnly(int? id)
        {
            var model = EmploymentBl.GetFamilyModel(id);
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
                return PartialView("Family", model);
            else
                return PartialView(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
        public ActionResult Family(FamilyModel model, IEnumerable<HttpPostedFileBase> files)
        {
            string error = String.Empty;
            if (model.RowID == 0)
            {
                if (ValidateModel(model))
                {
                    EmploymentBl.ProcessSaving<FamilyModel, Family>(model, out error);
                    ViewBag.Error = error;
                }
            }
            else
            {
                EmploymentBl.DeleteFamilyMember(model);
            }
            model = EmploymentBl.GetFamilyModel(model.UserId);
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=3");
            else
                return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("FamilyReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
        public ActionResult FamilyAddChild(FamilyMemberDto itemToAdd, int? CandidateId)
        {
            string error = String.Empty;

            FamilyModel model = EmploymentBl.GetFamilyModel(CandidateId);
            model.Children.Add(itemToAdd);
            EmploymentBl.ProcessSaving<FamilyModel, Family>(model, out error);

            model = EmploymentBl.GetFamilyModel(model.UserId);
            return Json(model.Children);
        }
        #endregion

        #region Military Service
        [HttpGet]
        public ActionResult MilitaryService(int? id)
        {
            var model = EmploymentBl.GetMilitaryServiceModel(id);
            return (model.IsFinal || id.HasValue) && !EmploymentBl.IsUnlimitedEditAvailable() ? View("MilitaryServiceReadOnly", model) : View(model);
        }

        [HttpGet]
        public ActionResult MilitaryServiceReadOnly(int? id)
        {
            var model = EmploymentBl.GetMilitaryServiceModel(id);
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
                return PartialView("MilitaryService", model);
            else
                return PartialView(model);
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
            model = EmploymentBl.GetMilitaryServiceModel(model.UserId);
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=4");
            else
                return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("MilitaryServiceReadOnly", model) : View(model);
        }
        #endregion

        #region Experience
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult Experience(int? id)
        {
            var model = EmploymentBl.GetExperienceModel(id);
            return (model.IsFinal || id.HasValue) && !EmploymentBl.IsUnlimitedEditAvailable() ? View("ExperienceReadOnly", model) : View(model);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult ExperienceReadOnly(int? id)
        {
            var model = EmploymentBl.GetExperienceModel(id);
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
                return PartialView("Experience", model);
            else
                return PartialView(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
        public ActionResult Experience(ExperienceModel model, IEnumerable<HttpPostedFileBase> files)
        {
            string error = String.Empty;

            if (model.RowID == 0)
            {
                if (ValidateModel(model))
                {
                    EmploymentBl.ProcessSaving<ExperienceModel, Experience>(model, out error);
                    ViewBag.Error = error;
                }
            }
            else
            {
                EmploymentBl.DeleteExperiensRow(model);
            }
            model = EmploymentBl.GetExperienceModel(model.UserId);
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=5");
            else
                return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("ExperienceReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
        public ActionResult ExperienceAddExperienceItem(ExperienceItemDto itemToAdd, int? CandidateId)
        {
            string error = String.Empty;

            ExperienceModel model = EmploymentBl.GetExperienceModel(CandidateId);
            model.ExperienceItems.Add(itemToAdd);
            EmploymentBl.ProcessSaving<ExperienceModel, Experience>(model, out error);

            model = EmploymentBl.GetExperienceModel(model.UserId);
            return Json(model.ExperienceItems);
        }
        #endregion

        #region Contacts
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult Contacts(int? id)
        {
            var model = EmploymentBl.GetContactsModel(id);
            return (model.IsFinal || id.HasValue) && !EmploymentBl.IsUnlimitedEditAvailable() ? View("ContactsReadOnly", model) : View(model);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult ContactsReadOnly(int? id)
        {
            var model = EmploymentBl.GetContactsModel(id);
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
                return PartialView("Contacts", model);
            else
                return PartialView(model);
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
            model = EmploymentBl.GetContactsModel(model.UserId);
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=6");
            else
                return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("ContactsReadOnly", model) : View(model);
        }
        #endregion

        #region Background Check
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult BackgroundCheck(int? id)
        {
            var model = EmploymentBl.GetBackgroundCheckModel(id);
            return (model.IsFinal || id.HasValue) && !EmploymentBl.IsUnlimitedEditAvailable() ? View("BackgroundCheckReadOnly", model) : View(model);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult BackgroundCheckReadOnly(int? id)
        {
            var model = EmploymentBl.GetBackgroundCheckModel(id);
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
                return PartialView("BackgroundCheck", model);
            else
                return PartialView(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
        public ActionResult BackgroundCheck(BackgroundCheckModel model, IEnumerable<HttpPostedFileBase> files)
        {
            string error = String.Empty;
            if (model.RowID == 0)
            {
                if (ValidateModel(model))
                {
                    EmploymentBl.ProcessSaving<BackgroundCheckModel, BackgroundCheck>(model, out error);
                    ViewBag.Error = error;
                }
            }
            else
            {
                EmploymentBl.DeleteBackgroundRow(model);
            }
            model = EmploymentBl.GetBackgroundCheckModel(model.UserId);
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=7");
            else
                return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("BackgroundCheckReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager)]
        public ActionResult BackgroundCheckAddReference(ReferenceDto itemToAdd, int? CandidateId)
        {
            string error = String.Empty;

            BackgroundCheckModel model = EmploymentBl.GetBackgroundCheckModel(CandidateId);
            model.References.Add(itemToAdd);
            EmploymentBl.ProcessSaving<BackgroundCheckModel, BackgroundCheck>(model, out error);

            model = EmploymentBl.GetBackgroundCheckModel(CandidateId);
            return Json(model.References);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Security)]
        public ActionResult BackgroundCheckReadOnly(int userId, bool isApprovalSkipped, bool? approvalStatus)
        {
            string error = String.Empty;

            EmploymentBl.ApproveBackgroundCheck(userId, isApprovalSkipped, approvalStatus, out error);

            if (!string.IsNullOrEmpty(error))
            {
                ViewBag.Error = error;
                BackgroundCheckModel model = EmploymentBl.GetBackgroundCheckModel();
                return PartialView("BackgroundCheckReadOnly", model);
            }
            else
            {
                BackgroundCheckModel model = EmploymentBl.GetBackgroundCheckModel(userId);
                return PartialView("BackgroundCheckReadOnly", model);
                //return RedirectToAction("Roster");
            }
        }
        #endregion

        #region Onsite Training
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Trainer | UserRole.PersonnelManager | UserRole.OutsourcingManager)]
        public ActionResult OnsiteTraining(int? id)
        {
            var model = EmploymentBl.GetOnsiteTrainingModel(id);
            return model.IsFinal ? View("OnsiteTrainingReadOnly", model) : View(model);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Trainer | UserRole.PersonnelManager | UserRole.OutsourcingManager)]
        public ActionResult OnsiteTrainingReadOnly(int? id)
        {
            var model = EmploymentBl.GetOnsiteTrainingModel(id);
            return model.IsFinal ? PartialView("OnsiteTrainingReadOnly", model) : PartialView("OnsiteTraining", model);
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
        #endregion

        #region Application Letter
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult ApplicationLetter(int? id)
        {
            var model = EmploymentBl.GetApplicationLetterModel(id);
            return View(model);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult ApplicationLetterReadOnly(int? id)
        {
            var model = EmploymentBl.GetApplicationLetterModel(id);
            return PartialView(model);
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
        #endregion

        #region Managers
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.PersonnelManager | UserRole.OutsourcingManager)]
        public ActionResult Managers(int? id)
        {
            var model = EmploymentBl.GetManagersModel(id);
            return View(model);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.PersonnelManager | UserRole.OutsourcingManager)]
        public ActionResult ManagersReadOnly(int? id)
        {
            var model = EmploymentBl.GetManagersModel(id);
            return PartialView(model);
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
                //model = EmploymentBl.GetManagersModel(model.UserId);
                //return PartialView(model);
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
                //return View("ManagersReadOnly", EmploymentBl.GetManagersModel(userId));
                //return View("Managers", EmploymentBl.GetManagersModel(userId));
                return RedirectToAction("Roster");
            }
        }
        #endregion

        #region PersonnelManagers
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.PersonnelManager | UserRole.OutsourcingManager)]
        public ActionResult PersonnelManagers(int? id)
        {
            var model = EmploymentBl.GetPersonnelManagersModel(id);
            return View(model);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.PersonnelManager | UserRole.OutsourcingManager)]
        public ActionResult PersonnelManagersReadOnly(int? id)
        {
            var model = EmploymentBl.GetPersonnelManagersModel(id);
            return PartialView(model);
            //return View(model);
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
            model = EmploymentBl.GetPersonnelManagersModel(model.UserId);
            if (!string.IsNullOrEmpty(error) || !ModelState.IsValid)
            {
                if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
                    return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=12");
                else
                {
                    ViewBag.Error = error;
                    return View(model);
                }
            }
            else
            {
                return RedirectToAction("Roster");
            }
        }
        #endregion

        #region Roster
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
        
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.Trainer | UserRole.PersonnelManager | UserRole.OutsourcingManager)]
        public ActionResult PersonnelInfo(int ID, bool IsCandidateInfoAvailable, bool IsBackgroundCheckAvailable, bool IsManagersAvailable, bool IsPersonalManagersAvailable, int TabIndex)
        {
            PersonnelInfoModel model = new PersonnelInfoModel();
            model.CandidateID = ID;
            model.IsCandidateInfoAvailable = IsCandidateInfoAvailable;
            model.IsBackgroundCheckAvailable = IsBackgroundCheckAvailable;
            model.IsManagersAvailable = IsManagersAvailable;
            model.IsPersonalManagersAvailable = IsPersonalManagersAvailable;
            model.TabIndex = TabIndex;
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

        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director)]
        public ActionResult RosterBulkChangeContractToIndefinite(IList<CandidateChangeContractToIndefiniteDto> roster)
        {
            string error = string.Empty;

            if (EmploymentBl.SaveContractChangesToIndefinite(roster, out error))
            {
                return Json(new { ok = true });
            }
            else
            {
                return Json(new { ok = false, error = error });
            }
        }
        
        #endregion

        #region CandidateDocuments
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate | UserRole.Trainer)]
        public ActionResult CandidateDocuments(int? id)
        {
            var model = EmploymentBl.GetCandidateDocumentsModel(id);
            if (AuthenticationService.CurrentUser.UserRole == UserRole.Candidate)
                return View(model);
            else
                return PartialView(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.PersonnelManager | UserRole.Candidate | UserRole.Manager)]
        public ActionResult CandidateDocuments(CandidateDocumentsModel model)
        {
            //, IEnumerable<HttpPostedFileBase> files
            string error = String.Empty;
            if (model.DeleteAttachmentId == 0)
                EmploymentBl.SaveCandidateDocumentsAttachments(model);
            else
            {
                DeleteAttacmentModel modelDel = new DeleteAttacmentModel { Id = model.DeleteAttachmentId };
                EmploymentBl.DeleteAttachment(modelDel);
            }
            model = EmploymentBl.GetCandidateDocumentsModel(model.UserId);

            if (AuthenticationService.CurrentUser.UserRole == UserRole.PersonnelManager || AuthenticationService.CurrentUser.UserRole == UserRole.Manager)
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=10");
            else
                return View(model);
        }
        #endregion

        #region // Custom report
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
        #endregion

        #region Signers
        [HttpGet]
        [ReportAuthorize(UserRole.PersonnelManager | UserRole.OutsourcingManager)]
        public ActionResult Signers()
        {
            // Get the current signers list
            var model = EmploymentBl.GetSignersModel();

            // Output the signers list for viewing/editing
            return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.PersonnelManager)]
        public ActionResult SignersAddOrEditSigner(SignerDto itemToSave)
        {
            string error = String.Empty;

            EmploymentBl.ProcessSaving(itemToSave, out error);

            var model = EmploymentBl.GetSignersModel();

            return View("Signers", model);
        }
        #endregion 

        #endregion

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
            const int minimumAge = 14;

            int numberOfFilledFields = 0;            

            numberOfFilledFields += string.IsNullOrEmpty(model.PassportData) ? 0 : 1;
            numberOfFilledFields += string.IsNullOrEmpty(model.SNILS) ? 0 : 1;
            numberOfFilledFields += model.DateOfBirth.HasValue ? 1 : 0;

            if (model.Surname == null)
                ModelState.AddModelError("Surname", "Заполните ФИО кандидата!");

            if (model.DepartmentId == 0)
                ModelState.AddModelError("DepartmentId", "Выберите структурное подразделение!");
            if (model.PersonnelId == 0)
                ModelState.AddModelError("PersonnelId", "Выберите сотрудника отдела кадров!");

            if (numberOfFilledFields < 2)
            {
                ModelState.AddModelError(string.Empty, "Необходимо заполнить хотя бы 2 поля личных данных, кроме ФИО.");
            }

            if (model.DateOfBirth > DateTime.Now.AddYears(-minimumAge))
            {
                ModelState.AddModelError("DateOfBirth", "Некорректная дата рождения.");
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

            if (!model.IsDraft)
            {
                if (!model.IsValidate)
                {
                    ModelState.AddModelError("AgreedToPersonalDataProcessing", "Подтвердите правильность предоставленных данных! Подтвердив правильность предоставленных данных, Вы не сможете больше вносить изменения в данную часть анкеты!");
                }
            }

            return ModelState.IsValid;
        }

        [NonAction]
        protected bool ValidateModel(PassportModel model)
        {
            if (!model.IsDraft)
            {
                if (!model.IsValidate)
                {
                    ModelState.AddModelError("IsValidate", "Подтвердите правильность предоставленных данных! Подтвердив правильность предоставленных данных, Вы не сможете больше вносить изменения в данную часть анкеты!");
                }
            }
            return ModelState.IsValid;
        }

        [NonAction]
        protected bool ValidateModel(EducationModel model)
        {
            if (!model.IsDraft)
            {
                if (!model.IsValidate)
                {
                    ModelState.AddModelError("IsValidate", "Подтвердите правильность предоставленных данных! Подтвердив правильность предоставленных данных, Вы не сможете больше вносить изменения в данную часть анкеты!");
                }
            }
            return ModelState.IsValid;
        }

        [NonAction]
        protected bool ValidateModel(FamilyModel model)
        {
            if (!model.IsDraft)
            {
                if (!model.IsValidate)
                {
                    ModelState.AddModelError("IsValidate", "Подтвердите правильность предоставленных данных! Подтвердив правильность предоставленных данных, Вы не сможете больше вносить изменения в данную часть анкеты!");
                }
            }
            return ModelState.IsValid;
        }

        [NonAction]
        protected bool ValidateModel(MilitaryServiceModel model)
        {
            if (!model.IsDraft)
            {
                if (!model.IsValidate)
                {
                    ModelState.AddModelError("IsValidate", "Подтвердите правильность предоставленных данных! Подтвердив правильность предоставленных данных, Вы не сможете больше вносить изменения в данную часть анкеты!");
                }
            }
            return ModelState.IsValid;
        }

        [NonAction]
        protected bool ValidateModel(ExperienceModel model)
        {
            if (!model.IsDraft)
            {
                if (!model.IsValidate)
                {
                    ModelState.AddModelError("IsValidate", "Подтвердите правильность предоставленных данных! Подтвердив правильность предоставленных данных, Вы не сможете больше вносить изменения в данную часть анкеты!");
                }
            }
            return ModelState.IsValid;
        }

        [NonAction]
        protected bool ValidateModel(ContactsModel model)
        {
            if (!model.IsDraft)
            {
                if (!model.IsValidate)
                {
                    ModelState.AddModelError("IsValidate", "Подтвердите правильность предоставленных данных! Подтвердив правильность предоставленных данных, Вы не сможете больше вносить изменения в данную часть анкеты!");
                }
            }
            return ModelState.IsValid;
        }

        [NonAction]
        protected bool ValidateModel(BackgroundCheckModel model)
        {
            if (!model.IsDraft)
            {
                if (!model.IsValidate)
                {
                    ModelState.AddModelError("IsValidate", "Подтвердите правильность предоставленных данных! Подтвердив правильность предоставленных данных, Вы не сможете больше вносить изменения в данную часть анкеты!");
                }
            }
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
            bool isFixedTermContract = EmploymentBl.IsFixedTermContract(model.UserId);
            if (model.ContractEndDate == null && isFixedTermContract)
            {
                ModelState.AddModelError("ContractEndDate", "*");
            }
            if (model.ContractEndDate != null && !isFixedTermContract)
            {
                ModelState.AddModelError("ContractEndDate", "Не заполняется при бессрочном ТД");
            }
            //if (!model.Level.HasValue || model.Level > 7 || model.Level < 2)
            //{
            //    ModelState.AddModelError("Level", "Требуется число от 2 до 7");
            //}
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

        #region Обработка запросов на печать кадровых документов

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

        [HttpGet]
        public ActionResult GetPrintLiabilityContract(int userId)
        {
            return GetPrintForm("PrintLiabilityContract", userId);
        }

        [HttpGet]
        public ActionResult GetPrintPersonalDataAgreement(int userId)
        {
            return GetPrintForm("PrintPersonalDataAgreement", userId);
        }

        [HttpGet]
        public ActionResult GetPrintPersonalDataObligation(int userId)
        {
            return GetPrintForm("PrintPersonalDataObligation", userId);
        }

        [HttpGet]
        public ActionResult GetPrintEmploymentFile(int userId)
        {
            return GetPrintForm("PrintEmploymentFile", userId);
        }

        [HttpGet]
        public ActionResult GetPrintRoster(RosterFiltersModel filters, int? sortBy, bool? sortDescending)
        {
            return GetListPrintForm("PrintRoster", filters, sortBy, sortDescending, true);
        } 

        #endregion

        #region Обработка запросов от конвертера PDF

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

        [HttpGet]
        public ActionResult PrintLiabilityContract(int userId)
        {
            PrintLiabilityContractModel model = EmploymentBl.GetPrintLiabilityContractModel(userId);
            return View(model);
        }

        [HttpGet]
        public ActionResult PrintPersonalDataAgreement(int userId)
        {
            PrintPersonalDataAgreementModel model = EmploymentBl.GetPrintPersonalDataAgreementModel(userId);
            return View(model);
        }

        [HttpGet]
        public ActionResult PrintPersonalDataObligation(int userId)
        {
            PrintPersonalDataObligationModel model = EmploymentBl.GetPrintPersonalDataObligationModel(userId);
            return View(model);
        }

        [HttpGet]
        public ActionResult PrintEmploymentFile(int userId)
        {
            PrintEmploymentFileModel model = EmploymentBl.GetPrintEmploymentFileModel(userId);
            return View(model);
        }

        [HttpGet]
        public ActionResult PrintRoster(RosterFiltersModel filters, int? sortBy, bool? sortDescending)
        {
            IList<CandidateDto> model = EmploymentBl.GetPrintRosterModel(filters, sortBy, sortDescending);
            return View(model);
        }

        #endregion

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
        public ActionResult GetListPrintForm(
            string actionName, RosterFiltersModel filters,
            int? sortBy, bool? sortDescending, bool isLandscape = false)
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
                        GetConverterCommandParam(actionName, filters, sortBy, sortDescending), cookieName, authCookie.Value);
                else
                    arguments.AppendFormat("{0} --cookie {1} {2}",
                        GetConverterCommandParam(actionName, filters, sortBy, sortDescending), cookieName, authCookie.Value);
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
        protected string GetConverterCommandParam(string actionName, int userId)
        {
            var localhostUrl = ConfigurationManager.AppSettings["localhost"];
            string urlTemplate = string.Format("Employment/{0}?userId={1}", actionName, userId);
            return !string.IsNullOrEmpty(localhostUrl)
                ? string.Format(@"{0}/{1}", localhostUrl, urlTemplate)
                : Url.Content(string.Format(@"{0}", urlTemplate));
        }

        [NonAction]
        protected string GetConverterCommandParam(
            string actionName, RosterFiltersModel filters,
            int? sortBy, bool? sortDescending)
        {
            var localhostUrl = ConfigurationManager.AppSettings["localhost"];

            string args = string.Format(@"{0}{1}{2}{3}{4}{5}{6}",
                filters.BeginDate.HasValue ? string.Format("beginDate={0}&", filters.BeginDate.Value.ToShortDateString()) : string.Empty,
                filters.EndDate.HasValue ? string.Format("endDate={0}&", filters.EndDate.Value.ToShortDateString()) : string.Empty,
                filters.DepartmentId,
                filters.StatusId.HasValue ? string.Format("statusId={0}&", filters.StatusId.Value) : string.Empty,
                !string.IsNullOrEmpty(filters.UserName) ? string.Format("userName={0}&", Server.UrlEncode(filters.UserName)) : string.Empty,
                sortBy.HasValue ? string.Format("sortBy={0}&", sortBy.Value) : string.Empty,
                sortDescending.HasValue ? string.Format("sortDescending={0}&", sortDescending.Value) : string.Empty
            );

            if (!string.IsNullOrEmpty(args))
            {
                args = args.Substring(0, args.Length - 1);
            }

            return !string.IsNullOrEmpty(localhostUrl)
                       ? string.Format(@"{0}/{1}/{2}?{2}", localhostUrl, "Employment", actionName, args)
                       : Url.Content(string.Format(@"{0}/{1}?{2}", "Employment", actionName, args));
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

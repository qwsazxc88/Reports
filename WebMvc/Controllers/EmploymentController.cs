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
using Reports.Core.Enum;

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

        #region Instruction
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.Trainer | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Candidate)]
        public ActionResult Instruction()
        {
            return View(new InstructionModel());
        } 
        #endregion

        #region Index
        [HttpGet]
        [ActionName("Index")]
        [ReportAuthorize(UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.Trainer | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Candidate | UserRole.StaffManager | UserRole.ConsultantOutsourcing)]
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

            if (model.IsSP)
            {
                model = EmploymentBl.GetCreateCandidateModel(model);
                if (model.PostUserLinks.Count == 0)
                    ModelState.AddModelError("UserLinkId", "Нет доступных вакансий в выбранном подразделении!");
                model.IsSP = false;
            }
            else
            {
                if (ValidateModel(model))
                {
                    model.UserId = EmploymentBl.CreateCandidate(model, out error);
                    //ViewBag.Error = error;
                }

                if (!string.IsNullOrEmpty(error))
                {
                    ViewBag.Error = error;
                    //ModelState.AddModelError("DepartmentId", error);
                }
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

        #region ScanOriginalDocuments
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Candidate | UserRole.ConsultantOutsourcing)]
        public ActionResult ScanOriginalDocuments(int? id)
        {
            ScanOriginalDocumentsModel model = null;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();
            //для кадровиков на вкладках показываем анкету с полным функционалом, как у кандидата, в стадии черновика
            //такая же схема применяется для всех страниц анкеты
            if (Session["ScanOriginalDocumentsM" + SPPath] != null)
            {
                model = (ScanOriginalDocumentsModel)Session["ScanOriginalDocumentsM" + SPPath];

                Session.Remove("ScanOriginalDocumentsM" + SPPath);
            }
            else
                model = EmploymentBl.GetScanOriginalDocumentsModel(id);

            if (Session["ScanOriginalDocumentsMS" + SPPath] != null)
            {
                ModelState.Clear();
                for (int i = 0; i < ((ModelStateDictionary)Session["ScanOriginalDocumentsMS" + SPPath]).Count; i++)
                {
                    ModelState.Add(((ModelStateDictionary)Session["ScanOriginalDocumentsMS" + SPPath]).ElementAt(i));
                }

                Session.Remove("ScanOriginalDocumentsMS" + SPPath);
            }


            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && EmploymentBl.IsUnlimitedEditAvailable())
                return PartialView("ScanOriginalDocuments", model);
            else
                return PartialView(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.ConsultantPersonnel | UserRole.PersonnelManager | UserRole.Manager)]
        public ActionResult ScanOriginalDocuments(ScanOriginalDocumentsModel model)
        {
            string error = String.Empty;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();

            if (model.DeleteAttachmentId != 0)
            {
                if (((AuthenticationService.CurrentUser.UserRole == UserRole.PersonnelManager) && !EmploymentBl.IsUnlimitedEditAvailable()) || AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing)
                {
                    ModelState.AddModelError("ErrorMessage", "У вас нет прав для редактирования данных!");
                    model = EmploymentBl.GetScanOriginalDocumentsModel(model.UserId);
                }
                else
                {
                    DeleteAttacmentModel modelDel = new DeleteAttacmentModel { Id = model.DeleteAttachmentId };
                    EmploymentBl.DeleteAttachment(modelDel);
                    model = EmploymentBl.GetScanOriginalDocumentsModel(model.UserId);
                    ModelState.AddModelError("ErrorMessage", "Файл удален!");
                }
            }
            else
            {
                ModelState.Clear();
                //кадровик не может менять список документов после выгрузки кандидата в 1С
                if (model.SendTo1C.HasValue)
                {
                    //model = EmploymentBl.GetScanOriginalDocumentsModel(model.UserId);
                    ModelState.AddModelError("ErrorMessage", "Кандидат выгружен в 1С! Любые изменения на данной страницы не возможны!");
                }
                else
                {
                    if (AuthenticationService.CurrentUser.UserRole == UserRole.PersonnelManager && !EmploymentBl.IsUnlimitedEditAvailable())
                    {
                        //model = EmploymentBl.GetScanOriginalDocumentsModel(model.UserId);
                        ModelState.AddModelError("ErrorMessage", "У вас нет прав для редактирования данных!");
                    }
                    else
                    {
                        if (ValidateModel(model))
                        {
                            string str = model.IsAgree ? "Данные сохранены!" : "Файл загружен!";
                            EmploymentBl.SaveScanOriginalDocumentsModelAttachments(model, out error);
                            //model = EmploymentBl.GetScanOriginalDocumentsModel(model.UserId);
                            ModelState.AddModelError("ErrorMessage", string.IsNullOrEmpty(error) ? str : error);
                        }
                        else
                        {   //так как при использования вкладок, страницу приходится перезагружать с потерей данных, то передаем модель с библиотекой ошибок через переменную сессии
                            //model = EmploymentBl.GetScanOriginalDocumentsModel(model.UserId);
                        }
                    }
                }
                model = EmploymentBl.GetScanOriginalDocumentsModel(model.UserId);
            }

            if (Session["ScanOriginalDocumentsM" + SPPath] != null)
                Session.Remove("ScanOriginalDocumentsM" + SPPath);
            if (Session["ScanOriginalDocumentsM" + SPPath] == null)
                Session.Add("ScanOriginalDocumentsM" + SPPath, model);

            if (Session["ScanOriginalDocumentsMS" + SPPath] != null)
                Session.Remove("ScanOriginalDocumentsMS" + SPPath);
            if (Session["ScanOriginalDocumentsMS" + SPPath] == null)
            {
                ModelStateDictionary mst = ModelState;
                Session.Add("ScanOriginalDocumentsMS" + SPPath, mst);
            }


            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && EmploymentBl.IsUnlimitedEditAvailable())
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=0");
            else
                return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("ScanOriginalDocumentsReadOnly", model) : View(model);
        }
        #endregion

        #region General Info
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Candidate | UserRole.Trainer)]
        public ActionResult GeneralInfo(int? id)
        {
            var model = EmploymentBl.GetGeneralInfoModel(id);
            return (model.IsFinal || id.HasValue) && !EmploymentBl.IsUnlimitedEditAvailable() ? View("GeneralInfoReadOnly", model) : View(model);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Candidate | UserRole.ConsultantOutsourcing)]
        public ActionResult GeneralInfoReadOnly(int? id)
        {
            GeneralInfoModel model = null;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();
            //для кадровиков на вкладках показываем анкету с полным функционалом, как у кандидата, в стадии черновика
            //такая же схема применяется для всех страниц анкеты
            if (Session["GeneralInfoM" + SPPath] != null)
            {
                model = (GeneralInfoModel)Session["GeneralInfoM" + SPPath];

                Session.Remove("GeneralInfoM" + SPPath);
            }
            else
                model = EmploymentBl.GetGeneralInfoModel(id);

            if (Session["GeneralInfoMS" + SPPath] != null)
            {
                ModelState.Clear();
                for (int i = 0; i < ((ModelStateDictionary)Session["GeneralInfoMS" + SPPath]).Count; i++)
                {
                    ModelState.Add(((ModelStateDictionary)Session["GeneralInfoMS" + SPPath]).ElementAt(i));
                }

                Session.Remove("GeneralInfoMS" + SPPath);
            }

            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && EmploymentBl.IsUnlimitedEditAvailable())
                return PartialView("GeneralInfo", model);
            else
                return PartialView(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.ConsultantPersonnel | UserRole.PersonnelManager)]
        public ActionResult GeneralInfo(GeneralInfoModel model)
        {
            string error = String.Empty;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();

            if (ValidateModel(model))
            {
                model.IsDraft = model.IsGIDraft;
                EmploymentBl.ProcessSaving<GeneralInfoModel, GeneralInfo>(model, out error);
                //ViewBag.Error = error;
                model = EmploymentBl.GetGeneralInfoModel(model.UserId);
                ModelState.AddModelError("AgreedToPersonalDataProcessing", string.IsNullOrEmpty(error) ? "Данные сохранены!" : error);
            }
            else
            {   //так как при использования вкладок, страницу приходится перезагружать с потерей данных, то передаем модель с библиотекой ошибок через переменную сессии
                model = EmploymentBl.GetGeneralInfoModel(model);
                if (Session["GeneralInfoM" + SPPath] != null)
                    Session.Remove("GeneralInfoM" + SPPath);
                if (Session["GeneralInfoM" + SPPath] == null)
                    Session.Add("GeneralInfoM" + SPPath, model);
            }

            if (Session["GeneralInfoMS" + SPPath] != null)
                Session.Remove("GeneralInfoMS" + SPPath);
            if (Session["GeneralInfoMS" + SPPath] == null)
            {
                ModelStateDictionary mst = ModelState;
                Session.Add("GeneralInfoMS" + SPPath, mst);
            }

            //для кадровиков при обновлении встаем на нужную вкладку
            //такая же схема применяется для всех страниц анкеты
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && EmploymentBl.IsUnlimitedEditAvailable())
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=1");
            else
                return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("GeneralInfoReadOnly", model) : View(model);
        }


        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.ConsultantPersonnel | UserRole.PersonnelManager)]
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
        [ReportAuthorize(UserRole.Candidate | UserRole.ConsultantPersonnel | UserRole.PersonnelManager)]
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
        [ReportAuthorize(UserRole.Candidate | UserRole.ConsultantPersonnel | UserRole.PersonnelManager)]
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
        [ReportAuthorize(UserRole.Candidate | UserRole.ConsultantPersonnel | UserRole.PersonnelManager)]
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
        [ReportAuthorize(UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Candidate)]
        public ActionResult Passport(int? id)
        {
            var model = EmploymentBl.GetPassportModel(id);
            return (model.IsFinal || id.HasValue) && !EmploymentBl.IsUnlimitedEditAvailable() ? View("PassportReadOnly", model) : View(model);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Candidate | UserRole.ConsultantOutsourcing)]
        public ActionResult PassportReadOnly(int? id)
        {
            PassportModel model = null;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();

            if (Session["PassportM" + SPPath] != null)
            {
                model = (PassportModel)Session["PassportM" + SPPath];

                Session.Remove("PassportM" + SPPath);
            }
            else
                model = EmploymentBl.GetPassportModel(id);

            if (Session["PassportMS" + SPPath] != null)
            {
                ModelState.Clear();
                for (int i = 0; i < ((ModelStateDictionary)Session["PassportMS" + SPPath]).Count; i++)
                {
                    ModelState.Add(((ModelStateDictionary)Session["PassportMS" + SPPath]).ElementAt(i));
                }
                Session.Remove("PassportMS" + SPPath);
            }

            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && EmploymentBl.IsUnlimitedEditAvailable())
                return PartialView("Passport", model);
            else
                return PartialView(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.ConsultantPersonnel | UserRole.PersonnelManager)]
        public ActionResult Passport(PassportModel model, IEnumerable<HttpPostedFileBase> files)
        {
            string error = String.Empty;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();

            if (ValidateModel(model))
            {
                model.IsDraft = model.IsPassportDraft;
                EmploymentBl.ProcessSaving<PassportModel, Passport>(model, out error);
                model = EmploymentBl.GetPassportModel(model.UserId);
                ViewBag.Error = error;
                ModelState.AddModelError("IsValidate", string.IsNullOrEmpty(error) ? "Данные сохранены!" : error);
            }
            else
            {   //так как при использования вкладок, страницу приходится перезагружать с потерей данных, то передаем модель с библиотекой ошибок через переменную сессии
                model = EmploymentBl.GetPassportModel(model);
                if (Session["PassportM" + SPPath] != null)
                    Session.Remove("PassportM" + SPPath);
                if (Session["PassportM" + SPPath] == null)
                    Session.Add("PassportM" + SPPath, model);
            }

            if (Session["PassportMS" + SPPath] != null)
                Session.Remove("PassportMS" + SPPath);
            if (Session["PassportMS" + SPPath] == null)
            {
                ModelStateDictionary mst = ModelState;
                Session.Add("PassportMS" + SPPath, mst);
            }

            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && EmploymentBl.IsUnlimitedEditAvailable())
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=2");
            else
                return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("PassportReadOnly", model) : View(model);
        }
        #endregion

        #region Education
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Candidate)]
        public ActionResult Education(int? id)
        {
            var model = EmploymentBl.GetEducationModel(id);
            return (model.IsFinal || id.HasValue) && !EmploymentBl.IsUnlimitedEditAvailable() ? View("EducationReadOnly", model) : View(model);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Candidate | UserRole.ConsultantOutsourcing)]
        public ActionResult EducationReadOnly(int? id)
        {
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();
            EducationModel model = null;

            if (Session["EducationM" + SPPath] != null)
            {
                model = (EducationModel)Session["EducationM" + SPPath];
                Session.Remove("EducationM" + SPPath);
            }
            else
                model = EmploymentBl.GetEducationModel(id);

            if (Session["EducationMS" + SPPath] != null)
            {
                ModelState.Clear();
                for (int i = 0; i < ((ModelStateDictionary)Session["EducationMS" + SPPath]).Count; i++)
                {
                    ModelState.Add(((ModelStateDictionary)Session["EducationMS" + SPPath]).ElementAt(i));
                }
                Session.Remove("EducationMS" + SPPath);
            }


            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && EmploymentBl.IsUnlimitedEditAvailable())
                return PartialView("Education", model);
            else
                return PartialView(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.ConsultantPersonnel | UserRole.PersonnelManager)]
        public ActionResult Education(EducationModel model)
        {
            string error = String.Empty;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();

            if (model.Operation == 0)
            {
                if (ValidateModel(model))
                {
                    model.IsDraft = model.IsEducationDraft;
                    EmploymentBl.ProcessSaving<EducationModel, Education>(model, out error);
                    model = EmploymentBl.GetEducationModel(model.UserId);
                    ModelState.AddModelError("IsValidate", string.IsNullOrEmpty(error) ? "Данные сохранены!" : error);
                    ViewBag.Error = error;
                }
                else
                {   //так как при использования вкладок, страницу приходится перезагружать с потерей данных, то передаем модель с библиотекой ошибок через переменную сессии
                    model = EmploymentBl.GetEducationModel(model.UserId);
                    if (Session["EducationM" + SPPath] != null)
                        Session.Remove("EducationM" + SPPath);
                    if (Session["EducationM" + SPPath] == null)
                        Session.Add("EducationM" + SPPath, model);
                }

                if (Session["EducationMS" + SPPath] != null)
                    Session.Remove("EducationMS" + SPPath);
                if (Session["EducationMS" + SPPath] == null)
                {
                    ModelStateDictionary mst = ModelState;
                    Session.Add("EducationMS" + SPPath, mst);
                }
            }
            else
            {
                EmploymentBl.DeleteEducationRow(model);
            }


            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && EmploymentBl.IsUnlimitedEditAvailable())
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=3");
            else
                return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("EducationReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.ConsultantPersonnel | UserRole.PersonnelManager)]
        public ActionResult EducationAddCertification(CertificationDto itemToAdd)
        {
            string error = String.Empty;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();

            EducationModel model = EmploymentBl.GetEducationModel(itemToAdd.UserId);

            if (!itemToAdd.CertificateDateOfIssue.HasValue || string.IsNullOrEmpty(itemToAdd.CertificateNumber) || !itemToAdd.CertificationDate.HasValue || string.IsNullOrEmpty(itemToAdd.InitiatingOrder) || string.IsNullOrEmpty(itemToAdd.LocationEI))
            {
                model.IsEducationCertificationsNotValid = true;
                //так как при использования вкладок, страницу приходится перезагружать с потерей данных, то передаем модель с библиотекой ошибок через переменную сессии
                //model = EmploymentBl.GetPassportModel(model);
                if (Session["EducationM" + SPPath] != null)
                    Session.Remove("EducationM" + SPPath);
                if (Session["EducationM" + SPPath] == null)
                    Session.Add("EducationM" + SPPath, model);

                if (Session["EducationMS" + SPPath] != null)
                    Session.Remove("EducationMS" + SPPath);
                if (Session["EducationMS" + SPPath] == null)
                {
                    ModelStateDictionary mst = ModelState;
                    Session.Add("EducationMS" + SPPath, mst);
                }
            }
            else
            {
                model.Certifications.Add(itemToAdd);
                EmploymentBl.ProcessSaving<EducationModel, Education>(model, out error);
                model = EmploymentBl.GetEducationModel(model.UserId);
                if (!string.IsNullOrEmpty(error))
                    model.IsEducationCertificationsNotValid = true;
            }

            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && EmploymentBl.IsUnlimitedEditAvailable())
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=3");
            else
                return View("Education", model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.ConsultantPersonnel | UserRole.PersonnelManager)]
        public ActionResult EducationAddHigherEducationDiploma(HigherEducationDiplomaDto itemToAdd)
        {
            string error = String.Empty;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();

            EducationModel model = EmploymentBl.GetEducationModel(itemToAdd.UserId);

            if (string.IsNullOrEmpty(itemToAdd.AdmissionYear) || string.IsNullOrEmpty(itemToAdd.Department) || itemToAdd.EducationTypeId == 0 || string.IsNullOrEmpty(itemToAdd.GraduationYear) ||
                string.IsNullOrEmpty(itemToAdd.IssuedBy) || string.IsNullOrEmpty(itemToAdd.LocationEI) || string.IsNullOrEmpty(itemToAdd.Number) || //string.IsNullOrEmpty(itemToAdd.Profession) ||
                string.IsNullOrEmpty(itemToAdd.Qualification) || string.IsNullOrEmpty(itemToAdd.Series) || string.IsNullOrEmpty(itemToAdd.Speciality))
            {
                try
                {
                    if (Convert.ToInt32(itemToAdd.GraduationYear) <= Convert.ToInt32(itemToAdd.AdmissionYear))
                        ModelState.AddModelError("GraduationYear", "Год окончания не может быть меньше года поступления!");
                }
                catch { }

                model.IsHigherEducationNotValid = true;
                //так как при использования вкладок, страницу приходится перезагружать с потерей данных, то передаем модель с библиотекой ошибок через переменную сессии
                //model = EmploymentBl.GetPassportModel(model);
                if (Session["EducationM" + SPPath] != null)
                    Session.Remove("EducationM" + SPPath);
                if (Session["EducationM" + SPPath] == null)
                    Session.Add("EducationM" + SPPath, model);

                if (Session["EducationMS" + SPPath] != null)
                    Session.Remove("EducationMS" + SPPath);
                if (Session["EducationMS" + SPPath] == null)
                {
                    ModelStateDictionary mst = ModelState;
                    Session.Add("EducationMS" + SPPath, mst);
                }
            }
            else
            {
                model.HigherEducationDiplomas.Add(itemToAdd);
                EmploymentBl.ProcessSaving<EducationModel, Education>(model, out error);
                model = EmploymentBl.GetEducationModel(model.UserId);
                if (!string.IsNullOrEmpty(error))
                    model.IsHigherEducationNotValid = true;
            }


            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && EmploymentBl.IsUnlimitedEditAvailable())
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=3");
            else
                return View("Education", model);
            
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.ConsultantPersonnel | UserRole.PersonnelManager)]
        public ActionResult EducationAddPostGraduateEducationDiploma(PostGraduateEducationDiplomaDto itemToAdd)
        {
            string error = String.Empty;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();

            EducationModel model = EmploymentBl.GetEducationModel(itemToAdd.UserId);
            if (string.IsNullOrEmpty(itemToAdd.GraduationYear) || string.IsNullOrEmpty(itemToAdd.AdmissionYear) || string.IsNullOrEmpty(itemToAdd.IssuedBy) || string.IsNullOrEmpty(itemToAdd.LocationEI) ||
                string.IsNullOrEmpty(itemToAdd.Number) || string.IsNullOrEmpty(itemToAdd.Series) || string.IsNullOrEmpty(itemToAdd.Speciality) ||
                itemToAdd.AdmissionYear.Length < 4 || itemToAdd.GraduationYear.Length < 4)
            {
                try
                {
                    if (Convert.ToInt32(itemToAdd.GraduationYear) < Convert.ToInt32(itemToAdd.AdmissionYear))
                        ModelState.AddModelError("GraduationYear", "Год окончания не может быть меньше года поступления!");
                }
                catch { }

                model.IsPostGraduateEducationNotValid = true;

                //так как при использования вкладок, страницу приходится перезагружать с потерей данных, то передаем модель с библиотекой ошибок через переменную сессии
                if (Session["EducationM" + SPPath] != null)
                    Session.Remove("EducationM" + SPPath);
                if (Session["EducationM" + SPPath] == null)
                    Session.Add("EducationM" + SPPath, model);

                if (Session["EducationMS" + SPPath] != null)
                    Session.Remove("EducationMS" + SPPath);
                if (Session["EducationMS" + SPPath] == null)
                {
                    ModelStateDictionary mst = ModelState;
                    Session.Add("EducationMS" + SPPath, mst);
                }
            }
            else
            {
                model.PostGraduateEducationDiplomas.Add(itemToAdd);
                EmploymentBl.ProcessSaving<EducationModel, Education>(model, out error);
                model = EmploymentBl.GetEducationModel(model.UserId);
                if (!string.IsNullOrEmpty(error))
                    model.IsPostGraduateEducationNotValid = true;
            }



            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && EmploymentBl.IsUnlimitedEditAvailable())
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=3");
            else
                return View("Education", model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.ConsultantPersonnel | UserRole.PersonnelManager)]
        public ActionResult EducationAddTraining(TrainingDto itemToAdd)
        {
            string error = String.Empty;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();

            EducationModel model = EmploymentBl.GetEducationModel(itemToAdd.UserId);
            if (!itemToAdd.BeginningDate.HasValue || string.IsNullOrEmpty(itemToAdd.CertificateIssuedBy) || !itemToAdd.EndDate.HasValue || string.IsNullOrEmpty(itemToAdd.LocationEI) || string.IsNullOrEmpty(itemToAdd.Number) || 
                string.IsNullOrEmpty(itemToAdd.Series) || string.IsNullOrEmpty(itemToAdd.Speciality))
            {
                if ((!itemToAdd.BeginningDate.HasValue || !itemToAdd.EndDate.HasValue) || (itemToAdd.BeginningDate.Value > itemToAdd.EndDate.Value))
                    ModelState.AddModelError("EndDate", "Дата окончания обучения не может быть меньше даты его начала!");

                model.IsEducationTrainingNotValid = true;

                //так как при использования вкладок, страницу приходится перезагружать с потерей данных, то передаем модель с библиотекой ошибок через переменную сессии
                //model = EmploymentBl.GetPassportModel(model);
                if (Session["EducationM" + SPPath] != null)
                    Session.Remove("EducationM" + SPPath);
                if (Session["EducationM" + SPPath] == null)
                    Session.Add("EducationM" + SPPath, model);

                if (Session["EducationMS" + SPPath] != null)
                    Session.Remove("EducationMS" + SPPath);
                if (Session["EducationMS" + SPPath] == null)
                {
                    ModelStateDictionary mst = ModelState;
                    Session.Add("EducationMS" + SPPath, mst);
                }
            }
            else
            {
                model.Training.Add(itemToAdd);
                EmploymentBl.ProcessSaving<EducationModel, Education>(model, out error);
                model = EmploymentBl.GetEducationModel(model.UserId);
                if (!string.IsNullOrEmpty(error))
                    model.IsEducationTrainingNotValid = true;
            }


            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && EmploymentBl.IsUnlimitedEditAvailable())
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=3");
            else
                return View("Education", model);
        }
        #endregion

        #region Family
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Candidate)]
        public ActionResult Family(int? id)
        {
            var model = EmploymentBl.GetFamilyModel(id);
            return (model.IsFinal || id.HasValue) && !EmploymentBl.IsUnlimitedEditAvailable() ? View("FamilyReadOnly", model) : View(model);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.ConsultantPersonnel | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Candidate | UserRole.ConsultantOutsourcing)]
        public ActionResult FamilyReadOnly(int? id)
        {
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();
            FamilyModel model = null;

            if (Session["FamilyM" + SPPath] != null)
            {
                model = (FamilyModel)Session["FamilyM" + SPPath];
                Session.Remove("FamilyM" + SPPath);
            }
            else
                model = EmploymentBl.GetFamilyModel(id);


            if (Session["FamilyMS" + SPPath] != null)
            {
                ModelState.Clear();
                for (int i = 0; i < ((ModelStateDictionary)Session["FamilyMS" + SPPath]).Count; i++)
                {
                    ModelState.Add(((ModelStateDictionary)Session["FamilyMS" + SPPath]).ElementAt(i));
                }
                Session.Remove("FamilyMS" + SPPath);
            }


            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && EmploymentBl.IsUnlimitedEditAvailable())
                return PartialView("Family", model);
            else
                return PartialView(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.ConsultantPersonnel | UserRole.PersonnelManager)]
        public ActionResult Family(FamilyModel model, IEnumerable<HttpPostedFileBase> files)
        {
            string error = String.Empty;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();

            if (model.RowID == 0)
            {
                if (ValidateModel(model))
                {
                    model.IsDraft = model.IsFDraft;
                    EmploymentBl.ProcessSaving<FamilyModel, Family>(model, out error);
                    model = EmploymentBl.GetFamilyModel(model.UserId);
                    ModelState.AddModelError("IsValidate", string.IsNullOrEmpty(error) ? "Данные сохранены!" : error);
                    ViewBag.Error = error;
                }
                else
                {   //так как при использования вкладок, страницу приходится перезагружать с потерей данных, то передаем модель с библиотекой ошибок через переменную сессии
                    model = EmploymentBl.GetFamilyModel(model);
                    if (Session["FamilyM" + SPPath] != null)
                        Session.Remove("FamilyM" + SPPath);
                    if (Session["FamilyM" + SPPath] == null)
                        Session.Add("FamilyM" + SPPath, model);
                }

                if (Session["FamilyMS" + SPPath] != null)
                    Session.Remove("FamilyMS" + SPPath);
                if (Session["FamilyMS" + SPPath] == null)
                {
                    ModelStateDictionary mst = ModelState;
                    Session.Add("FamilyMS" + SPPath, mst);
                }
            }
            else
            {
                EmploymentBl.DeleteFamilyMember(model);
                model = EmploymentBl.GetFamilyModel(model.UserId);
            }

            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && EmploymentBl.IsUnlimitedEditAvailable())
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=4");
            else
                return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("FamilyReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.ConsultantPersonnel | UserRole.PersonnelManager)]
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
        [ReportAuthorize(UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Candidate)]
        public ActionResult MilitaryService(int? id)
        {
            var model = EmploymentBl.GetMilitaryServiceModel(id);
            return (model.IsFinal || id.HasValue) && !EmploymentBl.IsUnlimitedEditAvailable() ? View("MilitaryServiceReadOnly", model) : View(model);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.ConsultantPersonnel | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Candidate | UserRole.ConsultantOutsourcing)]
        public ActionResult MilitaryServiceReadOnly(int? id)
        {
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();
            MilitaryServiceModel model = null;

            if (Session["MilitaryServiceM" + SPPath] != null)
            {
                model = (MilitaryServiceModel)Session["MilitaryServiceM" + SPPath];
                Session.Remove("MilitaryServiceM" + SPPath);
            }
            else
                model = EmploymentBl.GetMilitaryServiceModel(id);

            if (Session["MilitaryServiceMS" + SPPath] != null)
            {
                ModelState.Clear();
                for (int i = 0; i < ((ModelStateDictionary)Session["MilitaryServiceMS" + SPPath]).Count; i++)
                {
                    ModelState.Add(((ModelStateDictionary)Session["MilitaryServiceMS" + SPPath]).ElementAt(i));
                }
                Session.Remove("MilitaryServiceMS" + SPPath);
            }

            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && EmploymentBl.IsUnlimitedEditAvailable())
                return PartialView("MilitaryService", model);
            else
                return PartialView(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.ConsultantPersonnel | UserRole.PersonnelManager)]
        public ActionResult MilitaryService(MilitaryServiceModel model, IEnumerable<HttpPostedFileBase> files)
        {
            string error = String.Empty;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();

            if (ValidateModel(model))
            {
                model.IsDraft = model.IsMSDraft;
                EmploymentBl.ProcessSaving<MilitaryServiceModel, MilitaryService>(model, out error);
                model = EmploymentBl.GetMilitaryServiceModel(model.UserId);
                ViewBag.Error = error;
                ModelState.AddModelError("IsValidate", string.IsNullOrEmpty(error) ? "Данные сохранены!" : error);
            }
            else
            {   //так как при использования вкладок, страницу приходится перезагружать с потерей данных, то передаем модель с библиотекой ошибок через переменную сессии
                model = EmploymentBl.GetMilitaryServiceModel(model);
                if (Session["MilitaryServiceM" + SPPath] != null)
                    Session.Remove("MilitaryServiceM" + SPPath);
                if (Session["MilitaryServiceM" + SPPath] == null)
                    Session.Add("MilitaryServiceM" + SPPath, model);
            }

            if (Session["MilitaryServiceMS" + SPPath] != null)
                Session.Remove("MilitaryServiceMS" + SPPath);
            if (Session["MilitaryServiceMS" + SPPath] == null)
            {
                ModelStateDictionary mst = ModelState;
                Session.Add("MilitaryServiceMS" + SPPath, mst);
            }

            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && EmploymentBl.IsUnlimitedEditAvailable())
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=5");
            else
                return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("MilitaryServiceReadOnly", model) : View(model);
        }
        #endregion

        #region Experience
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Candidate)]
        public ActionResult Experience(int? id)
        {
            var model = EmploymentBl.GetExperienceModel(id);
            return (model.IsFinal || id.HasValue) && !EmploymentBl.IsUnlimitedEditAvailable() ? View("ExperienceReadOnly", model) : View(model);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Candidate | UserRole.ConsultantOutsourcing)]
        public ActionResult ExperienceReadOnly(int? id)
        {
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();
            ExperienceModel model = null;

            if (Session["ExperienceM" + SPPath] != null)
            {
                model = (ExperienceModel)Session["ExperienceM" + SPPath];
                Session.Remove("ExperienceM" + SPPath);
            }
            else
                model = EmploymentBl.GetExperienceModel(id);

            if (Session["ExperienceMS" + SPPath] != null)
            {
                ModelState.Clear();
                for (int i = 0; i < ((ModelStateDictionary)Session["ExperienceMS" + SPPath]).Count; i++)
                {
                    ModelState.Add(((ModelStateDictionary)Session["ExperienceMS" + SPPath]).ElementAt(i));
                }
                Session.Remove("ExperienceMS" + SPPath);
            }

            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && EmploymentBl.IsUnlimitedEditAvailable())
                return PartialView("Experience", model);
            else
                return PartialView(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.ConsultantPersonnel | UserRole.PersonnelManager)]
        public ActionResult Experience(ExperienceModel model, IEnumerable<HttpPostedFileBase> files)
        {
            string error = String.Empty;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();

            if (model.RowID == 0)
            {
                if (ValidateModel(model))
                {
                    model.IsDraft = model.IsExpDraft;
                    EmploymentBl.ProcessSaving<ExperienceModel, Experience>(model, out error);
                    model = EmploymentBl.GetExperienceModel(model.UserId);
                    ViewBag.Error = error;
                    ModelState.AddModelError("IsValidate", string.IsNullOrEmpty(error) ? "Данные сохранены!" : error);
                }
                else
                {   //так как при использования вкладок, страницу приходится перезагружать с потерей данных, то передаем модель с библиотекой ошибок через переменную сессии

                    model = EmploymentBl.GetExperienceModel(model);
                    if (Session["ExperienceM" + SPPath] != null)
                        Session.Remove("ExperienceM" + SPPath);
                    if (Session["ExperienceM" + SPPath] == null)
                        Session.Add("ExperienceM" + SPPath, model);
                }
            }
            else
            {
                EmploymentBl.DeleteExperiensRow(model);
                model = EmploymentBl.GetExperienceModel(model.UserId);
            }

            if (Session["ExperienceMS" + SPPath] != null)
                Session.Remove("ExperienceMS" + SPPath);
            if (Session["ExperienceMS" + SPPath] == null)
            {
                ModelStateDictionary mst = ModelState;
                Session.Add("ExperienceMS" + SPPath, mst);
            }

            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && EmploymentBl.IsUnlimitedEditAvailable())
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=6");
            else
                return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("ExperienceReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.ConsultantPersonnel | UserRole.PersonnelManager)]
        public ActionResult ExperienceAddExperienceItem(ExperienceItemDto itemToAdd)
        {
            string error = String.Empty;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();
            ExperienceModel model = EmploymentBl.GetExperienceModel(itemToAdd.UserId);


            if ((!itemToAdd.BeginningDate.HasValue || !itemToAdd.EndDate.HasValue) || (itemToAdd.BeginningDate.Value > itemToAdd.EndDate.Value))
            {
                ModelState.AddModelError("EndDate", "Дата окончания работы не может быть меньше даты ее начала!");
                model.IsExperienceItemNotValid = true;

                //так как при использования вкладок, страницу приходится перезагружать с потерей данных, то передаем модель с библиотекой ошибок через переменную сессии
                //model = EmploymentBl.GetPassportModel(model);
                if (Session["ExperienceM" + SPPath] != null)
                    Session.Remove("ExperienceM" + SPPath);
                if (Session["ExperienceM" + SPPath] == null)
                    Session.Add("ExperienceM" + SPPath, model);
            }
            else
            {
                model.ExperienceItems.Add(itemToAdd);
                EmploymentBl.ProcessSaving<ExperienceModel, Experience>(model, out error);
                ModelState.AddModelError("IsValidate", string.IsNullOrEmpty(error) ? "Данные сохранены!" : error);
                model = EmploymentBl.GetExperienceModel(model.UserId);
                if (!string.IsNullOrEmpty(error))
                    model.IsExperienceItemNotValid = true;
            }


            if (Session["ExperienceMS" + SPPath] != null)
                Session.Remove("ExperienceMS" + SPPath);
            if (Session["ExperienceMS" + SPPath] == null)
            {
                ModelStateDictionary mst = ModelState;
                Session.Add("ExperienceMS" + SPPath, mst);
            }


            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && EmploymentBl.IsUnlimitedEditAvailable())
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=6");
            else
                return View("Experience", model);
            //return Json(model.ExperienceItems);
        }
        #endregion

        #region Contacts
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.ConsultantPersonnel | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Candidate)]
        public ActionResult Contacts(int? id)
        {
            var model = EmploymentBl.GetContactsModel(id);
            return (model.IsFinal || id.HasValue) && !EmploymentBl.IsUnlimitedEditAvailable() ? View("ContactsReadOnly", model) : View(model);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.ConsultantPersonnel | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Candidate | UserRole.ConsultantOutsourcing)]
        public ActionResult ContactsReadOnly(int? id)
        {
            ContactsModel model = null;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();

            if (Session["ContactsM" + SPPath] != null)
            {
                model = (ContactsModel)Session["ContactsM" + SPPath];
                Session.Remove("ContactsM" + SPPath);
            }
            else
                model = EmploymentBl.GetContactsModel(id);

            if (Session["ContactsMS" + SPPath] != null)
            {
                ModelState.Clear();
                for (int i = 0; i < ((ModelStateDictionary)Session["ContactsMS" + SPPath]).Count; i++)
                {
                    ModelState.Add(((ModelStateDictionary)Session["ContactsMS" + SPPath]).ElementAt(i));
                }
                Session.Remove("ContactsMS" + SPPath);
            }

            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && EmploymentBl.IsUnlimitedEditAvailable())
                return PartialView("Contacts", model);
            else
                return PartialView(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.ConsultantPersonnel | UserRole.PersonnelManager)]
        public ActionResult Contacts(ContactsModel model)
        {
            string error = String.Empty;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();

            if (ValidateModel(model))
            {
                model.IsDraft = model.IsContactDraft;
                EmploymentBl.ProcessSaving<ContactsModel, Contacts>(model, out error);
                model = EmploymentBl.GetContactsModel(model.UserId);
                ModelState.AddModelError("IsValidate", string.IsNullOrEmpty(error) ? "Данные сохранены!" : error);
                ViewBag.Error = error;
            }
            else
            {   //так как при использования вкладок, страницу приходится перезагружать с потерей данных, то передаем модель с библиотекой ошибок через переменную сессии
                model = EmploymentBl.GetContactsModel(model);
                if (Session["ContactsM" + SPPath] != null)
                    Session.Remove("ContactsM" + SPPath);
                if (Session["ContactsM" + SPPath] == null)
                    Session.Add("ContactsM" + SPPath, model);
            }

            if (Session["ContactsMS" + SPPath] != null)
                Session.Remove("ContactsMS" + SPPath);
            if (Session["ContactsMS" + SPPath] == null)
            {
                ModelStateDictionary mst = ModelState;
                Session.Add("ContactsMS" + SPPath, mst);
            }

            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && EmploymentBl.IsUnlimitedEditAvailable())
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=7");
            else
                return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("ContactsReadOnly", model) : View(model);
        }
        #endregion

        #region Background Check
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.ConsultantPersonnel | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Candidate)]
        public ActionResult BackgroundCheck(int? id)
        {
            var model = EmploymentBl.GetBackgroundCheckModel(id);
            return (model.IsFinal || id.HasValue) && !EmploymentBl.IsUnlimitedEditAvailable() ? View("BackgroundCheckReadOnly", model) : View(model);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.ConsultantPersonnel | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Candidate | UserRole.ConsultantOutsourcing)]
        public ActionResult BackgroundCheckReadOnly(int? id)
        {
            BackgroundCheckModel model = null;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();

            if (Session["BackgroundCheckM" + SPPath] != null)
            {
                model = (BackgroundCheckModel)Session["BackgroundCheckM" + SPPath];
                Session.Remove("BackgroundCheckM" + SPPath);
            }
            else
                model = EmploymentBl.GetBackgroundCheckModel(id);

            if (Session["BackgroundCheckMS" + SPPath] != null)
            {
                ModelState.Clear();
                for (int i = 0; i < ((ModelStateDictionary)Session["BackgroundCheckMS" + SPPath]).Count; i++)
                {
                    ModelState.Add(((ModelStateDictionary)Session["BackgroundCheckMS" + SPPath]).ElementAt(i));
                }
                Session.Remove("BackgroundCheckMS" + SPPath);
            }


            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 && EmploymentBl.IsUnlimitedEditAvailable())
                return PartialView("BackgroundCheck", model);
            else
                return PartialView(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.PersonnelManager | UserRole.ConsultantPersonnel | UserRole.Security)]
        public ActionResult BackgroundCheck(BackgroundCheckModel model, IEnumerable<HttpPostedFileBase> files)
        {
            string error = String.Empty;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();

            if (model.DeleteAttachmentId == 0)
            {
                if (model.RowID == 0)
                {
                    if (ValidateModel(model))
                    {
                        model.IsDraft = model.IsBGDraft;
                        EmploymentBl.ProcessSaving<BackgroundCheckModel, BackgroundCheck>(model, out error);
                        model = EmploymentBl.GetBackgroundCheckModel(model.UserId);
                        ModelState.AddModelError("IsValidate", string.IsNullOrEmpty(error) ? "Данные сохранены!" : error);
                        //ViewBag.Error = error;
                    }
                    else
                    {   //так как при использования вкладок, страницу приходится перезагружать с потерей данных, то передаем модель с библиотекой ошибок через переменную сессии
                        model = EmploymentBl.GetBackgroundCheckModel(model);
                        if (Session["BackgroundCheckM" + SPPath] != null)
                            Session.Remove("BackgroundCheckM" + SPPath);
                        if (Session["BackgroundCheckM" + SPPath] == null)
                            Session.Add("BackgroundCheckM" + SPPath, model);
                    }

                    if (Session["BackgroundCheckMS" + SPPath] != null)
                        Session.Remove("BackgroundCheckMS" + SPPath);
                    if (Session["BackgroundCheckMS" + SPPath] == null)
                    {
                        ModelStateDictionary mst = ModelState;
                        Session.Add("BackgroundCheckMS" + SPPath, mst);
                    }
                }
                else
                {
                    EmploymentBl.DeleteBackgroundRow(model);
                    model = EmploymentBl.GetBackgroundCheckModel(model.UserId);
                }
            }
            else
            {
                if (AuthenticationService.CurrentUser.UserRole == UserRole.PersonnelManager && !EmploymentBl.IsUnlimitedEditAvailable())
                {
                    ModelState.AddModelError("IsValidate", "У вас нет прав для редактирования данных!");
                    model = EmploymentBl.GetBackgroundCheckModel(model.UserId);
                }
                else
                {
                    DeleteAttacmentModel modelDel = new DeleteAttacmentModel { Id = model.DeleteAttachmentId };
                    EmploymentBl.DeleteAttachment(modelDel);
                    model = EmploymentBl.GetBackgroundCheckModel(model.UserId);
                    ModelState.AddModelError("IsValidate", "Файл удален!");
                }

                if (Session["BackgroundCheckMS" + SPPath] != null)
                    Session.Remove("BackgroundCheckMS" + SPPath);
                if (Session["BackgroundCheckMS" + SPPath] == null)
                {
                    ModelStateDictionary mst = ModelState;
                    Session.Add("BackgroundCheckMS" + SPPath, mst);
                }
            }

            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 || (AuthenticationService.CurrentUser.UserRole & UserRole.Security) > 0)
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=8");
            else
                return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("BackgroundCheckReadOnly", model) : View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.ConsultantPersonnel | UserRole.PersonnelManager)]
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
        [ReportAuthorize(UserRole.Security | UserRole.ConsultantOutsourcing)]
        //public ActionResult BackgroundCheckReadOnly(int userId, bool isApprovalSkipped, bool? approvalStatus, string PyrusRef, bool IsCancelApproveAvailale, IEnumerable<HttpPostedFileBase> files)
        public ActionResult BackgroundCheckReadOnly(BackgroundCheckModel model)
        {
            string error = String.Empty;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();
            //BackgroundCheckModel model = null;
            //if (model.IsCancelApproveAvailale && AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing)//для консультантов
            //    EmploymentBl.ApproveBackgroundCheck(model, out error);
            //else
            //    EmploymentBl.ApproveBackgroundCheck(model, out error);

            EmploymentBl.ApproveBackgroundCheck(model, out error);

            model = EmploymentBl.GetBackgroundCheckModel(model.UserId);
            

            ModelState.AddModelError("IsValidate", string.IsNullOrEmpty(error) ? "Кандидат утвержден!" : error);

            if (Session["BackgroundCheckM" + SPPath] != null)
                Session.Remove("BackgroundCheckM" + SPPath);
            if (Session["BackgroundCheckM" + SPPath] == null)
                Session.Add("BackgroundCheckM" + SPPath, model);

            if (Session["BackgroundCheckMS" + SPPath] != null)
                Session.Remove("BackgroundCheckMS" + SPPath);
            if (Session["BackgroundCheckMS" + SPPath] == null)
            {
                ModelStateDictionary mst = ModelState;
                Session.Add("BackgroundCheckMS" + SPPath, mst);
            }

            //return PartialView("BackgroundCheckReadOnly", model);
            return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=8");

        }
        /// <summary>
        /// Предварительное согласование.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="PrevApprovalStatus"></param>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Security | UserRole.ConsultantOutsourcing)]
        public ActionResult BackgroundPrevCheck(int userId, bool? PrevApprovalStatus, bool IsCancelApproveAvailale)
        {
            string error = String.Empty;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();
            ScanOriginalDocumentsModel model = null;

            if (IsCancelApproveAvailale)
                EmploymentBl.PrevApproveBackgroundCheck(userId, PrevApprovalStatus, IsCancelApproveAvailale, out error);
            else
            {
                if (PrevApprovalStatus.HasValue)
                    EmploymentBl.PrevApproveBackgroundCheck(userId, PrevApprovalStatus, IsCancelApproveAvailale, out error);
                else
                    error = "Выберите вид операции!";
            }


            model = EmploymentBl.GetScanOriginalDocumentsModel(userId);
            ModelState.AddModelError("ErrorMessage", string.IsNullOrEmpty(error) ? "Операция выполнена!" : error);

            return PartialView("ScanOriginalDocuments", model);

        }

        [HttpPost]
        [ReportAuthorize(UserRole.Candidate | UserRole.ConsultantPersonnel | UserRole.PersonnelManager | UserRole.Security)]
        public ActionResult BackGroundCheckAddComments(BackgroundCheckModel model)
        {
            string error = String.Empty;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();
            if (!string.IsNullOrEmpty(model.Comment))
            {
                if (!EmploymentBl.SaveComments(model.UserId, (int)EmploymentCommentTypeEnum.BackgroundCheck, model.Comment, out error))
                {
                    model = EmploymentBl.GetBackgroundCheckModel(model);
                    if (Session["BackgroundCheckM" + SPPath] != null)
                        Session.Remove("BackgroundCheckM" + SPPath);
                    if (Session["BackgroundCheckM" + SPPath] == null)
                        Session.Add("BackgroundCheckM" + SPPath, model);

                    ModelState.AddModelError("IsValidate", error);

                    if (Session["BackgroundCheckMS" + SPPath] != null)
                        Session.Remove("BackgroundCheckMS" + SPPath);
                    if (Session["BackgroundCheckMS" + SPPath] == null)
                    {
                        ModelStateDictionary mst = ModelState;
                        Session.Add("BackgroundCheckMS" + SPPath, mst);
                    }
                }
            }
            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0 || (AuthenticationService.CurrentUser.UserRole & UserRole.Security) > 0)
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=8");
            else
                return model.IsFinal && !EmploymentBl.IsUnlimitedEditAvailable() ? View("BackgroundCheckReadOnly", model) : View(model);
        }
        #endregion

        #region Onsite Training
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.ConsultantPersonnel | UserRole.Director | UserRole.Trainer | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator)]
        public ActionResult OnsiteTraining(int? id)
        {
            var model = EmploymentBl.GetOnsiteTrainingModel(id);
            return model.IsFinal ? View("OnsiteTrainingReadOnly", model) : View(model);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.ConsultantPersonnel | UserRole.Director | UserRole.Trainer | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.ConsultantOutsourcing)]
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
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.ConsultantPersonnel | UserRole.Director | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Candidate)]
        public ActionResult ApplicationLetter(int? id)
        {
            var model = EmploymentBl.GetApplicationLetterModel(id);
            return View(model);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.ConsultantPersonnel | UserRole.Director | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Candidate)]
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
        [ReportAuthorize(UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.Chief | UserRole.Director | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator)]
        public ActionResult Managers(int? id)
        {
            var model = EmploymentBl.GetManagersModel(id);
            return View(model);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.ConsultantPersonnel | UserRole.Director | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.ConsultantOutsourcing)]
        public ActionResult ManagersReadOnly(int? id)
        {
            ManagersModel model = null;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();

            if (Session["ManagersM" + SPPath] != null)
            {
                model = (ManagersModel)Session["ManagersM" + SPPath];
                Session.Remove("ManagersM" + SPPath);
            }
            else
                model = EmploymentBl.GetManagersModel(id);

            if (Session["ManagersMS" + SPPath] != null)
            {
                ModelState.Clear();
                for (int i = 0; i < ((ModelStateDictionary)Session["ManagersMS" + SPPath]).Count; i++)
                {
                    ModelState.Add(((ModelStateDictionary)Session["ManagersMS" + SPPath]).ElementAt(i));
                }
                Session.Remove("ManagersMS" + SPPath);
            }

            return PartialView(model);
        }

        /// <summary>
        /// Автозаполнение должности.
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public ActionResult AutocompletePositionSearch(string term)
        {
            IList<IdNameDto> Positions = EmploymentBl.GetPositionAutocomplete(term);
            var PositionList = Positions.ToList().Select(a => new { label = a.Name, PositionId = a.Id }).Distinct();

            return Json(PositionList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.PersonnelManager | UserRole.ConsultantOutsourcing)]
        public ActionResult Managers(ManagersModel model)
        {
            string error = String.Empty;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();

            if (ValidateModel(model))
            {
                if (model.IsDraftM)
                {
                    EmploymentBl.ProcessSaving<ManagersModel, Managers>(model, out error);
                    ModelState.AddModelError("MessageStr", string.IsNullOrEmpty(error) ? "Данные сохранены!" : error);
                }
                else
                {
                    EmploymentBl.ApproveCandidateByManager(model, out error);
                    ModelState.AddModelError("MessageStr", string.IsNullOrEmpty(error) ? "Кандидат утвержден!" : error);
                }
                model = EmploymentBl.GetManagersModel(model.UserId);
            }
            else
            {   //так как при использования вкладок, страницу приходится перезагружать с потерей данных, то передаем модель с библиотекой ошибок через переменную сессии
                //EmploymentBl.LoadDictionaries(model);
                model = EmploymentBl.GetManagersModel(model);
                if (Session["ManagersM" + SPPath] != null)
                    Session.Remove("ManagersM" + SPPath);
                if (Session["ManagersM" + SPPath] == null)
                    Session.Add("ManagersM" + SPPath, model);
            }

            if (Session["ManagersMS" + SPPath] != null)
                Session.Remove("ManagersMS" + SPPath);
            if (Session["ManagersMS" + SPPath] == null)
            {
                ModelStateDictionary mst = ModelState;
                Session.Add("ManagersMS" + SPPath, mst);
            }

            if (!string.IsNullOrEmpty(error) || !ModelState.IsValid)
            {
                ViewBag.Error = error;
                //return View(model);
                return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=11");
            }
            else
            {
                //model = EmploymentBl.GetManagersModel(model.UserId);
                //return PartialView(model);
                return RedirectToAction("Roster");
            }
        }
        /// <summary>
        /// Достаем информацию по штатной единице.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.PersonnelManager | UserRole.ConsultantOutsourcing | UserRole.Employee | UserRole.OutsourcingManager)]
        public ActionResult GetStaffEstablishmentPostDetails(bool IsSP, int DepartmentId, int UserLinkId, int UserId)
        {
            string error = String.Empty;
            bool result = false;
            ManagersModel model = new ManagersModel();
            model.IsSP = IsSP;
            model.DepartmentId = DepartmentId;
            model.UserLinkId = UserLinkId;
            model.UserId = UserId;

            EmploymentBl.GetStaffEstablishmentPostDetails(model);
            result = true;

            if (model.IsSP)
                return Json(new { ok = result, msg = error, UserLinkId = model.UserLinkId, model.PostUserLinks });
            else
                return Json(new { ok = result, msg = error, model.SalaryBasis, model.AreaMultiplier });
        }
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.ConsultantOutsourcing)]
        public ActionResult ManagersApproveByHigherManager(int userId, bool? higherManagerApprovalStatus, bool IsCancelApproveHigherAvailale)
        {
            string error = String.Empty;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();

            EmploymentBl.ApproveCandidateByHigherManager(userId, higherManagerApprovalStatus, IsCancelApproveHigherAvailale, out error);
            if (!string.IsNullOrEmpty(error) && !IsCancelApproveHigherAvailale)
            {
                ViewBag.Error = error;
                ModelState.AddModelError("MessageStr", error);
                //return View("Managers", EmploymentBl.GetManagersModel(userId));
            }
            else
            {
                ModelState.AddModelError("MessageStr", string.IsNullOrEmpty(error) ? "Кандидат утвержден!" : error);
            }

            
            if (Session["ManagersMS" + SPPath] != null)
                Session.Remove("ManagersMS" + SPPath);
            if (Session["ManagersMS" + SPPath] == null)
            {
                ModelStateDictionary mst = ModelState;
                Session.Add("ManagersMS" + SPPath, mst);
            }

            return Redirect("PersonnelInfo?id=" + userId.ToString() + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=11");
        }
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.PersonnelManager)]
        public ActionResult ManagersAddComments(ManagersModel model)
        {
            string error = String.Empty;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();
            if (!string.IsNullOrEmpty(model.Comment))
            {
                if (!EmploymentBl.SaveComments(model.UserId, (int)EmploymentCommentTypeEnum.Managers, model.Comment, out error))
                {
                    model = EmploymentBl.GetManagersModel(model);
                    if (Session["ManagersM" + SPPath] != null)
                        Session.Remove("ManagersM" + SPPath);
                    if (Session["ManagersM" + SPPath] == null)
                        Session.Add("ManagersM" + SPPath, model);

                    ModelState.AddModelError("IsValidate", error);

                    if (Session["ManagersMS" + SPPath] != null)
                        Session.Remove("ManagersMS" + SPPath);
                    if (Session["ManagersMS" + SPPath] == null)
                    {
                        ModelStateDictionary mst = ModelState;
                        Session.Add("ManagersMS" + SPPath, mst);
                    }
                }
            }
            return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=11");
        }

        #endregion

        #region PersonnelManagers
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.ConsultantPersonnel | UserRole.Director | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator)]
        public ActionResult PersonnelManagers(int? id)
        {
            var model = EmploymentBl.GetPersonnelManagersModel(id);
            return View(model);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.ConsultantPersonnel | UserRole.Director | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.ConsultantOutsourcing)]
        public ActionResult PersonnelManagersReadOnly(int? id)
        {
            PersonnelManagersModel model = null;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();
            

            if (Session["PersonnelManagersM" + SPPath] != null)
            {
                model = (PersonnelManagersModel)Session["PersonnelManagersM" + SPPath];
                Session.Remove("PersonnelManagersM" + SPPath);
            }
            else
                model = EmploymentBl.GetPersonnelManagersModel(id);

            if (Session["PersonnelManagersMS" + SPPath] != null)
            {
                ModelState.Clear();
                for (int i = 0; i < ((ModelStateDictionary)Session["PersonnelManagersMS" + SPPath]).Count; i++)
                {
                    ModelState.Add(((ModelStateDictionary)Session["PersonnelManagersMS" + SPPath]).ElementAt(i));
                }
                Session.Remove("PersonnelManagersMS" + SPPath);
                //затираются значения, потому что в контроллер приходят пустые поля и присваиваются в коде
                //ModelState.SetModelValue("EmploymentOrderNumber", new ValueProviderResult(model.EmploymentOrderNumber, model.EmploymentOrderNumber, System.Globalization.CultureInfo.CurrentCulture));
                //ModelState.SetModelValue("ContractNumber", new ValueProviderResult(model.ContractNumber, model.ContractNumber, System.Globalization.CultureInfo.CurrentCulture));
            }
            
            //model = EmploymentBl.GetPersonnelManagersModel(id);

            return PartialView(model);
            //return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.PersonnelManager | UserRole.ConsultantPersonnel | UserRole.OutsourcingManager | UserRole.Estimator)]
        public ActionResult PersonnelManagers(PersonnelManagersModel model, IEnumerable<HttpPostedFileBase> files)
        {
            string error = String.Empty;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();
            
            if (model.IsReject)
            {
                EmploymentBl.SavePersonnelManagersRejecting(model, out error);
                ModelState.Clear();
                ModelState.AddModelError("MessageStr", string.IsNullOrEmpty(error) ? "Кандидат отклонен!" : error);
            }
            else
            {
                if (ValidateModel(model))
                {
                    if (model.IsDraftPM)
                    {
                        EmploymentBl.ProcessSaving<PersonnelManagersModel, PersonnelManagers>(model, out error);
                        ModelState.AddModelError("MessageStr", string.IsNullOrEmpty(error) ? "Данные сохранены!" : error);
                    }
                    else
                    {
                        EmploymentBl.SavePersonnelManagersReport(model, out error);
                        ModelState.AddModelError("MessageStr", string.IsNullOrEmpty(error) ? "Кандидат утвержден! Данные готовы к выгрузке в 1С!" : error);
                    }
                    model = EmploymentBl.GetPersonnelManagersModel(model.UserId);

                }
                else
                {   //так как при использования вкладок, страницу приходится перезагружать с потерей данных, то передаем модель с библиотекой ошибок через переменную сессии
                    model = EmploymentBl.GetPersonnelManagersModel(model);
                    if (Session["PersonnelManagersM" + SPPath] != null)
                        Session.Remove("PersonnelManagersM" + SPPath);
                    if (Session["PersonnelManagersM" + SPPath] == null)
                        Session.Add("PersonnelManagersM" + SPPath, model);
                }
            }


            if (Session["PersonnelManagersMS" + SPPath] != null)
                Session.Remove("PersonnelManagersMS" + SPPath);
            if (Session["PersonnelManagersMS" + SPPath] == null)
            {
                ModelStateDictionary mst = ModelState;
                Session.Add("PersonnelManagersMS" + SPPath, mst);
            }

            
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

        [HttpPost]
        [ReportAuthorize(UserRole.PersonnelManager | UserRole.ConsultantPersonnel | UserRole.OutsourcingManager | UserRole.Estimator)]
        public ActionResult PersonnelManagersAddComments(PersonnelManagersModel model)
        {
            string error = String.Empty;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();
            if (!string.IsNullOrEmpty(model.Comment))
            {
                if (!EmploymentBl.SaveComments(model.UserId, (int)EmploymentCommentTypeEnum.PersonnelManagers, model.Comment, out error))
                {
                    model = EmploymentBl.GetPersonnelManagersModel(model);
                    if (Session["PersonnelManagersM" + SPPath] != null)
                        Session.Remove("PersonnelManagersM" + SPPath);
                    if (Session["PersonnelManagersM" + SPPath] == null)
                        Session.Add("PersonnelManagersM" + SPPath, model);

                    ModelState.AddModelError("IsValidate", error);

                    if (Session["PersonnelManagersMS" + SPPath] != null)
                        Session.Remove("PersonnelManagersMS" + SPPath);
                    if (Session["PersonnelManagersMS" + SPPath] == null)
                    {
                        ModelStateDictionary mst = ModelState;
                        Session.Add("PersonnelManagersMS" + SPPath, mst);
                    }
                }
            }
            return Redirect("PersonnelInfo?id=" + model.UserId + "&IsCandidateInfoAvailable=true&IsBackgroundCheckAvailable=true&IsManagersAvailable=true&IsPersonalManagersAvailable=true&TabIndex=12");
        }
        #endregion

        #region Roster
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.Trainer | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.StaffManager | UserRole.ConsultantOutsourcing)]
        public ActionResult Roster()
        {
            var model = EmploymentBl.GetRosterModel(null);
            return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.Trainer | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.StaffManager | UserRole.ConsultantOutsourcing)]
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
        [ReportAuthorize(UserRole.PersonnelManager | UserRole.ConsultantPersonnel)]
        public ActionResult CandidateSaveTechDissmiss(IList<CandidateTechDissmissDto> roster)
        {
            bool result = EmploymentBl.SaveCandidateTechDissmiss(roster);
            return Json(new { ok = result });
        }
        /// <summary>
        /// Сохраняем отметки о плучении кадровиком оригинала трудовой книжки (ТК) кандидата.
        /// </summary>
        /// <param name="roster">Обрабатываемый список.</param>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.PersonnelManager | UserRole.ConsultantPersonnel)]
        public ActionResult CandidateSaveTKRecieved(IList<CandidateDocRecievedDto> roster)
        {
            bool result = EmploymentBl.SaveCandidateDocRecieved(roster, true);
            return Json(new { ok = result, roster });
        }
        /// <summary>
        /// Сохраняем отметки о плучении кадровиком оригинала трудовой договора (ТД) кандидата.
        /// </summary>
        /// <param name="roster">Обрабатываемый список.</param>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.PersonnelManager | UserRole.ConsultantPersonnel)]
        public ActionResult CandidateSaveTDRecieved(IList<CandidateDocRecievedDto> roster)
        {
            bool result = EmploymentBl.SaveCandidateDocRecieved(roster, false);
            return Json(new { ok = result, roster });
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.Chief | UserRole.Director)]
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
        [ReportAuthorize(UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Candidate | UserRole.Trainer | UserRole.ConsultantOutsourcing)]
        public ActionResult CandidateDocuments(int? id)
        {
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();
            CandidateDocumentsModel model = new CandidateDocumentsModel();

            if (Session["CandidateDocumentsM" + SPPath] != null)
            {
                model = (CandidateDocumentsModel)Session["CandidateDocumentsM" + SPPath];
                Session.Remove("CandidateDocumentsM" + SPPath);
            }
            else
                model = EmploymentBl.GetCandidateDocumentsModel(id);



            if (Session["CandidateDocumentsMS" + SPPath] != null)
            {
                ModelState.Clear();
                for (int i = 0; i < ((ModelStateDictionary)Session["CandidateDocumentsMS" + SPPath]).Count; i++)
                {
                    ModelState.Add(((ModelStateDictionary)Session["CandidateDocumentsMS" + SPPath]).ElementAt(i));
                }
                Session.Remove("CandidateDocumentsMS" + SPPath);
            }


            if (AuthenticationService.CurrentUser.UserRole == UserRole.Candidate)
                return View(model);
            else
                return PartialView(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.PersonnelManager | UserRole.ConsultantPersonnel | UserRole.Candidate | UserRole.Manager)]
        public ActionResult CandidateDocuments(CandidateDocumentsModel model)
        {
            //, IEnumerable<HttpPostedFileBase> files
            string error = String.Empty;
            string SPPath = AuthenticationService.CurrentUser.Id.ToString();

            

            if (model.DeleteAttachmentId == 0)
            {
                ModelState.Clear();
                //кадровик не может менять список документов после выгрузки кандидата в 1С
                if (model.IsSave && model.SendTo1C.HasValue)
                {
                    ModelState.AddModelError("SendTo1C", "Кандидат выгружен в 1С! Изменение перечня документов для подписи не возможно!");
                }
                else
                {
                    if (AuthenticationService.CurrentUser.UserRole == UserRole.PersonnelManager && !EmploymentBl.IsUnlimitedEditAvailable())
                    {
                        model = EmploymentBl.GetCandidateDocumentsModel(model.UserId);
                        ModelState.AddModelError("SendTo1C", "У вас нет прав для редактирования данных!");
                    }
                    else
                    {
                        string str = model.IsSave ? "Список документов для подписи сформирован! Если список документов сформирован впервые или был изменен, то будет отправлено сообщение руководителю!" : "Файл загружен!";
                        EmploymentBl.SaveCandidateDocumentsAttachments(model, out error);
                        model = EmploymentBl.GetCandidateDocumentsModel(model.UserId);
                        ModelState.AddModelError("SendTo1C", string.IsNullOrEmpty(error) ? str : error);
                    }
                }

                if (Session["CandidateDocumentsM" + SPPath] != null)
                    Session.Remove("CandidateDocumentsM" + SPPath);
                if (Session["CandidateDocumentsM" + SPPath] == null)
                    Session.Add("CandidateDocumentsM" + SPPath, model);

                if (Session["CandidateDocumentsMS" + SPPath] != null)
                    Session.Remove("CandidateDocumentsMS" + SPPath);
                if (Session["CandidateDocumentsMS" + SPPath] == null)
                {
                    ModelStateDictionary mst = ModelState;
                    Session.Add("CandidateDocumentsMS" + SPPath, mst);
                }
            }
            else
            {
                if (AuthenticationService.CurrentUser.UserRole == UserRole.PersonnelManager && !EmploymentBl.IsUnlimitedEditAvailable())
                {
                    ModelState.AddModelError("SendTo1C", "У вас нет прав для редактирования данных!");
                    model = EmploymentBl.GetCandidateDocumentsModel(model.UserId);
                }
                else
                {
                    //DeleteAttacmentModel modelDel = new DeleteAttacmentModel { Id = model.DeleteAttachmentId };
                    //EmploymentBl.DeleteAttachment(modelDel);
                    EmploymentBl.DeleteCandidateDocument(model, out error);
                    model = EmploymentBl.GetCandidateDocumentsModel(model.UserId);
                    ModelState.AddModelError("SendTo1C", error);
                }

                if (Session["CandidateDocumentsMS" + SPPath] != null)
                    Session.Remove("CandidateDocumentsMS" + SPPath);
                if (Session["CandidateDocumentsMS" + SPPath] == null)
                {
                    ModelStateDictionary mst = ModelState;
                    Session.Add("CandidateDocumentsMS" + SPPath, mst);
                }
            }

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
        [ReportAuthorize(UserRole.PersonnelManager | UserRole.ConsultantPersonnel | UserRole.OutsourcingManager | UserRole.Estimator)]
        public ActionResult Signers()
        {
            // Get the current signers list
            var model = EmploymentBl.GetSignersModel();

            // Output the signers list for viewing/editing
            return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.PersonnelManager | UserRole.ConsultantPersonnel)]
        public ActionResult SignersAddOrEditSigner(SignerDto itemToSave)
        {
            string error = String.Empty;

            EmploymentBl.ProcessSaving(itemToSave, out error);

            var model = EmploymentBl.GetSignersModel();

            return View("Signers", model);
        }
        #endregion 

        #region PersonnelInfo
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.Trainer | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.ConsultantOutsourcing)]
        public ActionResult PersonnelInfo(int ID, bool IsCandidateInfoAvailable, bool IsBackgroundCheckAvailable, bool IsManagersAvailable, bool IsPersonalManagersAvailable, int TabIndex)
        {
            PersonnelInfoModel model = new PersonnelInfoModel();
            model.CandidateID = ID;
            model.IsCandidateInfoAvailable = IsCandidateInfoAvailable;
            model.IsBackgroundCheckAvailable = IsBackgroundCheckAvailable;
            model.IsManagersAvailable = IsManagersAvailable;
            model.IsPersonalManagersAvailable = IsPersonalManagersAvailable;
            model.TabIndex = TabIndex;
            model = EmploymentBl.GetPersonnelInfoModel(model);
            return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Security | UserRole.PersonnelManager | UserRole.ConsultantOutsourcing)]
        public ActionResult PersonnelInfoSendEmail(int CandidateId, int ToUserId, string Subject, string EmailMessage)
        {
            PersonnelInfoModel model = new PersonnelInfoModel();
            model.CandidateID = CandidateId;
            model.ToUserId = ToUserId;
            model.Subject = Subject;
            model.EmailMessage = EmailMessage;
            
            string error = String.Empty;
            bool result = EmploymentBl.EmploymentProccedRegistrationSendEmail(model, out error);

            if (result)
                model = EmploymentBl.GetPersonnelInfoModel(model);
            
            return Json(new { ok = result, msg = error, EmailMessageStr = model.EmailMessage });
        }
        #endregion
        #endregion

        #region Model Validation

        [NonAction]
        protected void ValidateFileLength(HttpPostedFileBase postedFile, string inputName, double MaxFileSize)
        {
            //пришлось для каждого скана сделать индивидуальную проверку на допустимый размер
            MaxFileSize = MaxFileSize * (1024 * 1024);
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

            numberOfFilledFields += /*string.IsNullOrEmpty(model.PassportData) ? 0 :*/ 1;//Номер паспорта больше не нужен, он ушёл в прошлое и про него забыли, пусть вместо него будет 1.
            numberOfFilledFields += string.IsNullOrEmpty(model.SNILS) ? 0 : 1;
            numberOfFilledFields += model.DateOfBirth.HasValue ? 1 : 0;

            if (string.IsNullOrEmpty(model.Surname) || string.IsNullOrWhiteSpace(model.Surname))
                ModelState.AddModelError("Surname", "Заполните ФИО кандидата!");
            else
            {
                if (model.Surname.Trim().Length == 0)
                    ModelState.AddModelError("Surname", "Заполните ФИО кандидата!");
            }


            if (!model.UserLinkId.HasValue || model.UserLinkId.Value == 0)
                ModelState.AddModelError("UserLinkId", "Выберте штатную единицу!");

            if (model.DepartmentId == 0)
                ModelState.AddModelError("DepartmentId", "Выберите структурное подразделение!");
            if (model.PersonnelId == 0)
                ModelState.AddModelError("PersonnelId", "Выберите сотрудника отдела кадров!");

            if ((AuthenticationService.CurrentUser.UserRole & UserRole.PersonnelManager) > 0)
            {
                if (!model.OnBehalfOfManagerId.HasValue || model.OnBehalfOfManagerId == 0)
                    ModelState.AddModelError("OnBehalfOfManagerId", "Выберите руководителя!");
            }

            if (numberOfFilledFields < 2)
            {
                ModelState.AddModelError(string.Empty, "Необходимо заполнить хотя бы 2 поля личных данных, кроме ФИО.");
            }

            if (model.DateOfBirth > DateTime.Now.AddYears(-minimumAge))
            {
                ModelState.AddModelError("DateOfBirth", "Некорректная дата рождения.");
            }

            if (!model.PlanRegistrationDate.HasValue)
                ModelState.AddModelError("PlanRegistrationDate", "Укажите планируемую дату приема!");

            //if (string.IsNullOrEmpty(model.PyrusNumber) || string.IsNullOrWhiteSpace(model.PyrusNumber))
            //    ModelState.AddModelError("PyrusNumber", "Укажите номер задачи в системе Pyrus!");
            //else
            //{
            //    try
            //    {
            //        Convert.ToInt32(model.PyrusNumber);
            //    }
            //    catch
            //    {
            //        ModelState.AddModelError("PyrusNumber", "Номер задачи в системе Пайрус должен содержать только цифры!");
            //    }
            //}

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

            
            ValidateFileLength(model.PhotoFile, "PhotoFile", 2);
            ValidateFileLength(model.INNScanFile, "INNScanFile", 2);
            ValidateFileLength(model.SNILSScanFile, "SNILSScanFile", 2);
            ValidateFileLength(model.DisabilityCertificateScanFile, "DisabilityCertificateScanFile", 2);

            if (!model.IsGIDraft)
            {
                //GeneralInfoModel mt = EmploymentBl.GetGeneralInfoModel(model.UserId);
                //закомментарены проверки на наличие сканов из-зи новой страницы
                ////должен быть скан инн и снилс
                //if (!string.IsNullOrEmpty(model.INN))
                //{
                //    if (model.INNScanFile == null && string.IsNullOrEmpty(mt.INNScanAttachmentFilename))
                //    {
                //        ModelState.AddModelError("INNScanFile", "Не выбран файл скана ИНН для загрузки!");
                //    }
                //}
                //if (model.SNILSScanFile == null && string.IsNullOrEmpty(mt.SNILSScanAttachmentFilename))
                //{
                //    ModelState.AddModelError("SNILSScanFile", "Не выбран файл скана СНИЛС для загрузки!");
                //}

                ////скан справки по инвалидости, если какие-то из полей заполнены
                //if (!string.IsNullOrEmpty(model.DisabilityCertificateSeries) || !string.IsNullOrEmpty(model.DisabilityCertificateNumber) || model.DisabilityCertificateDateOfIssue.HasValue ||
                //    model.DisabilityCertificateExpirationDate.HasValue || (model.DisabilityDegreeId.HasValue && model.DisabilityDegreeId != 0) || model.IsDisabilityTermLess)
                //{
                //    if (model.DisabilityCertificateScanFile == null && string.IsNullOrEmpty(mt.DisabilityCertificateScanAttachmentFilename))
                //    {
                //        ModelState.AddModelError("DisabilityCertificateScanFile", "Не выбран файл скана справки об инвалидности для загрузки!");
                //    }
                //}

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
            
            ValidateFileLength(model.InternalPassportScanFile, "InternalPassportScanFile", 20);

            //проверка для номера паспорта
            if (string.IsNullOrEmpty(model.InternalPassportNumber) || string.IsNullOrWhiteSpace(model.InternalPassportNumber))
            {
                ModelState.AddModelError("InternalPassportNumber", "Обязательное поле");
            }
            else
            {
                try
                {
                    Convert.ToInt32(model.InternalPassportNumber);
                }
                catch
                {
                    ModelState.AddModelError("InternalPassportNumber", "Числовое поле");
                }

                //Российский паспорт
                if (model.DocumentTypeId == 1 && model.InternalPassportNumber.Length != 6)
                {
                    ModelState.AddModelError("InternalPassportNumber", "Требуется 6 цифр");
                }
            }

            if (!model.IsPassportDraft)
            {
                //PassportModel mt = EmploymentBl.GetPassportModel(model.UserId);    
                //if (model.InternalPassportScanFile == null && string.IsNullOrEmpty(mt.InternalPassportScanAttachmentFilename))
                //{
                //    ModelState.AddModelError("InternalPassportScanFile", "Не выбран файл скана документа для загрузки!");
                //}


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
            ModelState.Clear();
            ValidateFileLength(model.HigherEducationDiplomaScanFile, "HigherEducationDiplomaScanFile", 5);
            ValidateFileLength(model.PostGraduateEducationDiplomaScanFile, "PostGraduateEducationDiplomaScanFile", 2);
            ValidateFileLength(model.CertificationScanFile, "CertificationScanFile", 2);
            ValidateFileLength(model.TrainingScanFile, "TrainingScanFile", 2);
            if (!model.IsEducationDraft)
            {
                //EducationModel mt = EmploymentBl.GetEducationModel(model.UserId);

                //if (EmploymentBl.CheckExistsEducationRecord(model.UserId, 1) != 0 && model.HigherEducationDiplomaScanFile == null && string.IsNullOrEmpty(mt.HigherEducationDiplomaScanFileName))
                //{
                //    ModelState.AddModelError("HigherEducationDiplomaScanFile", "Не выбран файл скана документа об образовании для загрузки!");
                //}

                //if (EmploymentBl.CheckExistsEducationRecord(model.UserId, 2) != 0 && model.PostGraduateEducationDiplomaScanFile == null && string.IsNullOrEmpty(mt.PostGraduateEducationDiplomaScanFileName))
                //{
                //    ModelState.AddModelError("PostGraduateEducationDiplomaScanFile", "Не выбран файл скана документа об образовании для загрузки!");
                //}

                //if (EmploymentBl.CheckExistsEducationRecord(model.UserId, 3) != 0 && model.CertificationScanFile == null && string.IsNullOrEmpty(mt.CertificationScanFileName))
                //{
                //    ModelState.AddModelError("CertificationScanFile", "Не выбран файл скана документа об образовании для загрузки!");
                //}

                //if (EmploymentBl.CheckExistsEducationRecord(model.UserId, 4) != 0 && model.TrainingScanFile == null && string.IsNullOrEmpty(mt.TrainingScanFileName))
                //{
                //    ModelState.AddModelError("TrainingScanFile", "Не выбран файл скана документа об образовании для загрузки!");
                //}
                
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
            ValidateFileLength(model.MarriageCertificateScanFile, "MarriageCertificateScanFile", 2);
            ValidateFileLength(model.ChildBirthCertificateScanFile, "ChildBirthCertificateScanFile", 2);

            if (!model.IsFDraft)
            {
                //FamilyModel mt = EmploymentBl.GetFamilyModel(model.UserId);
                //if (model.IsMarried)
                //{
                //    if (model.MarriageCertificateScanFile == null && string.IsNullOrEmpty(mt.MarriageCertificateScanAttachmentFilename))
                //    {
                //        ModelState.AddModelError("MarriageCertificateScanFile", "Не выбран файл скана свидетельства о браке для загрузки!");
                //    }
                //}

                //if (mt.Children.Count != 0)
                //{
                //    if (model.ChildBirthCertificateScanFile == null && string.IsNullOrEmpty(mt.ChildBirthCertificateScanAttachmentFilename))
                //    {
                //        ModelState.AddModelError("ChildBirthCertificateScanFile", "Не выбран файл скана свидетельств о рождении детей для загрузки!");
                //    }
                //}

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
            ValidateFileLength(model.MilitaryCardScanFile, "MilitaryCardScanFile", 20);
            ValidateFileLength(model.MobilizationTicketScanFile, "MobilizationTicketScanFile", 2);

            if (!model.IsMSDraft)
            {
                if (model.IsLiableForMilitaryService)
                {
                    if (!model.ReserveCategoryId.HasValue)
                    {
                        ModelState.AddModelError("ReserveCategoryId", "Укажите категорию запаса!");
                    }
                }
                //MilitaryServiceModel mt = EmploymentBl.GetMilitaryServiceModel(model.UserId);
                //if (model.IsLiableForMilitaryService)
                //{
                //    if (model.MilitaryCardScanFile == null && string.IsNullOrEmpty(mt.MilitaryCardScanAttachmentFilename))
                //    {
                //        ModelState.AddModelError("MilitaryCardScanFile", "Не выбран файл скана военного билета для загрузки!");
                //    }
                //}
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
            //чистим ошибки для полей из модальной формы
            ModelState.Remove("BeginningDate");
            ModelState.Remove("EndDate");
            ModelState.Remove("Company");
            ModelState.Remove("Position");
            ModelState.Remove("CompanyContacts");

            ValidateFileLength(model.WorkBookScanFile, "WorkBookScanFile", 20);
            ValidateFileLength(model.WorkBookSupplementScanFile, "WorkBookSupplementScanFile", 20);
            
            if (!model.IsExpDraft)
            {
                ModelState.Clear();

                //ExperienceModel mt = EmploymentBl.GetExperienceModel(model.UserId);
                //if (model.WorkBookScanFile == null && string.IsNullOrEmpty(mt.WorkBookScanAttachmentFilename))
                //{
                //    ModelState.AddModelError("WorkBookScanFile", "Не выбран файл скана трудовой книжки/заявления для загрузки!");
                //}

                //if (!string.IsNullOrEmpty(model.WorkBookSupplementSeries) || !string.IsNullOrEmpty(model.WorkBookSupplementNumber) || model.WorkBookSupplementDateOfIssue.HasValue)
                //{
                //    if (model.WorkBookSupplementScanFile == null && string.IsNullOrEmpty(mt.WorkBookSupplementScanAttachmentFilename))
                //    {
                //        ModelState.AddModelError("WorkBookSupplementScanFile", "Не выбран файл скана трудовой книжки/заявления для загрузки!");
                //    }
                //}

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
            if (!model.IsContactDraft)
            {
                //ModelState.Clear();
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
            ValidateFileLength(model.PersonalDataProcessingScanFile, "PersonalDataProcessingScanFile", 0.5);
            ValidateFileLength(model.InfoValidityScanFile, "InfoValidityScanFile", 0.5);
            ValidateFileLength(model.EmploymentFile, "EmploymentFile", 4);
            ValidateFileLength(model.EmploymentFile, "IsValidate", 2);

            if (!model.IsBGDraft)
            {
                if (!model.IsVolga)
                {
                    BackgroundCheckModel mt = EmploymentBl.GetBackgroundCheckModel(model.UserId);
                    if (model.EmploymentFile == null && string.IsNullOrEmpty(mt.EmploymentFileName))
                    {
                        ModelState.AddModelError("IsValidate", "Не выбран файл скана анкеты для загрузки!");
                        ModelState.AddModelError("EmploymentFile", "Не выбран файл скана анкеты для загрузки!");
                    }
                }
                //if (model.PersonalDataProcessingScanFile == null && string.IsNullOrEmpty(mt.PersonalDataProcessingScanAttachmentFilename))
                //{
                //    ModelState.AddModelError("PersonalDataProcessingScanFile", "Не выбран файл скана для загрузки!");
                //}

                //if (model.InfoValidityScanFile == null && string.IsNullOrEmpty(mt.InfoValidityScanAttachmentFilename))
                //{
                //    ModelState.AddModelError("InfoValidityScanFile", "Не выбран файл скана для загрузки!");
                //}

                //if (model.PersonalDataProcessingScanFile != null)
                //{
                //    //model.PersonalDataProcessingScanFile.ContentLength
                //}

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
            ValidateFileLength(model.ApplicationLetterScanFile, "ApplicationLetterScanFile", 2);
            return ModelState.IsValid;
        }

        [NonAction]
        protected bool ValidateModel(ManagersModel model)
        {
            if (AuthenticationService.CurrentUser.UserRole == UserRole.PersonnelManager && !EmploymentBl.IsUnlimitedEditAvailable())
            {
                ModelState.AddModelError("MessageStr", "У вас нет прав для редактирования данных!");
            }

            if (model.UserLinkId == 0)
            {
                ModelState.AddModelError("UserLinkId", "Выберите штатную единицу!");
            }

            //if (model.PositionId == 0)
            //    ModelState.AddModelError("PositionId", "Укажите должность кандидата!");

            //if (!model.SalaryBasis.HasValue)
            //    ModelState.AddModelError("SalaryBasis", "Укажите должностной оклад!");
            //else
            //{
            //    if (model.SalaryBasis.Value <= 0)
            //    {
            //        ModelState.AddModelError("SalaryBasis", "Должностной оклад должен иметь значение больше нуля!");
            //    }
            //}

            if (!model.SalaryMultiplier.HasValue)
                ModelState.AddModelError("SalaryMultiplier", "Заполните поле 'Ставка'!");
            else
            {
                if (model.SalaryMultiplier.Value <= 0)
                    ModelState.AddModelError("SalaryMultiplier", "Ставка должна иметь значение больше нуля!");
                if (model.SalaryMultiplier.Value > 1)
                    ModelState.AddModelError("SalaryMultiplier", "Ставка не может быть больше единицы!");
            }

            //проверка на задачу в пайрусе
            if ((model.PersonalAddition.HasValue && model.PersonalAddition.Value != 0)
                //|| (model.PositionAddition.HasValue && model.PositionAddition.Value != 0)
                || (model.AreaAddition.HasValue && model.AreaAddition.Value != 0)
                || (model.TravelRelatedAddition.HasValue && model.TravelRelatedAddition.Value != 0)
                || (model.CompetenceAddition.HasValue && model.CompetenceAddition.Value != 0)
                || (model.FrontOfficeExperienceAddition.HasValue && model.FrontOfficeExperienceAddition.Value != 0))
            {
                if(string.IsNullOrEmpty(model.PyrusNumber) || string.IsNullOrWhiteSpace(model.PyrusNumber))
                    ModelState.AddModelError("PyrusNumber", "Введите номер задачи в системе Pyrus!");
                
            }

            if (!string.IsNullOrEmpty(model.PyrusNumber) || !string.IsNullOrWhiteSpace(model.PyrusNumber))
            {
                try
                {
                    Convert.ToInt32(model.PyrusNumber);
                }
                catch
                {
                    ModelState.AddModelError("PyrusNumber", "Номер задачи в системе Пайрус должен содержать только цифры!");
                }
            }
            //model.PyrusNumber;
            
            if (!model.SendTo1C.HasValue)
            {
                if (!model.RegistrationDate.HasValue)
                    ModelState.AddModelError("RegistrationDate", "Укажите дату оформления!");
                else
                {
                    if (DateTime.Today >= new DateTime(2015, 8, 1).Date)
                    {
                        if (model.RegistrationDate.Value.Year != DateTime.Today.Year || model.RegistrationDate.Value.Month != DateTime.Today.Month)
                        {
                            //если дата приема стоит прошлым месяцем относительно текущей даты, то можно принять только до 5 числа текущего месяца (Экспресс-Волга до 8 числа)
                            if (model.RegistrationDate.Value.AddMonths(1).Year == DateTime.Today.Year && model.RegistrationDate.Value.AddMonths(1).Month == DateTime.Today.Month && DateTime.Today.Day > 5/*(model.IsVolga ? 8 : 5)*/)
                            {
                                ModelState.AddModelError("RegistrationDate", "Прием сотрудника в прошлом периоде запрещен!");
                            }
                            else //if (model.RegistrationDate.Value.Year == DateTime.Today.Year && model.RegistrationDate.Value.Month == DateTime.Today.Month)
                            {
                                if (model.RegistrationDate.Value < DateTime.Today && (model.RegistrationDate.Value.AddMonths(1).Year != DateTime.Today.Year || model.RegistrationDate.Value.AddMonths(1).Month != DateTime.Today.Month))
                                    ModelState.AddModelError("RegistrationDate", "Дата оформления не должна быть меньше текущей даты!");
                            }
                        }
                    }
                    else
                    {
                        if (model.RegistrationDate.Value < Convert.ToDateTime("01/04/2015") /*< DateTime.Today*/)//на время теста
                            ModelState.AddModelError("RegistrationDate", "Дата оформления не должна быть меньше текущей даты!");
                    }
                }
            }

            if (!string.IsNullOrEmpty(model.ProbationaryPeriod))
            {
                try { Convert.ToInt32(model.ProbationaryPeriod); }
                catch 
                {
                    ModelState.AddModelError("ProbationaryPeriod", "Испытательный срок должен содержать только цифры!");
                }
            }


            if (!ModelState.IsValid)
            {
                model.ManagerApprovalStatus = null;
            }

            return ModelState.IsValid;
        }

        [NonAction]
        protected bool ValidateModel(PersonnelManagersModel model)
        {
            bool isFixedTermContract = EmploymentBl.IsFixedTermContract(model.UserId);
            model.IsFixedTermContract = isFixedTermContract;
            bool flgError = false;

            if (AuthenticationService.CurrentUser.UserRole == UserRole.PersonnelManager && !EmploymentBl.IsUnlimitedEditAvailable())
            {
                ModelState.AddModelError("MessageStr", "У вас нет прав для редактирования данных!");
            }

            if (model.ContractEndDate == null && isFixedTermContract && model.ContractPoint_1_Id.Value == 1)
            {
                ModelState.AddModelError("ContractEndDate", "*");
                ModelState.AddModelError("MessageStr", "Укажите дату окончания ТД");
                flgError = true;
            }
            if (model.ContractEndDate != null && !isFixedTermContract)
            {
                ModelState.AddModelError("ContractEndDate", "Не заполняется при бессрочном ТД");
                ModelState.AddModelError("MessageStr", "Не заполняется при бессрочном ТД");
                flgError = true;
            }

            if (model.ContractPoint_1_Id == 2)
            {
                if (model.ContractPointsFio == null || model.ContractPointsFio.Trim().Length == 0)
                {
                    ModelState.AddModelError("ContractPointsFio", "Заполните поле!");
                    ModelState.AddModelError("MessageStr", "Заполните поле!");
                    flgError = true;
                }
            }

            if (model.ContractPoint_2_Id == 4)
            {
                if (model.ContractPointsAddress == null || model.ContractPointsAddress.Trim().Length == 0)
                {
                    ModelState.AddModelError("ContractPointsAddress", "Заполните поле!");
                    ModelState.AddModelError("MessageStr", "Заполните поле!");
                    flgError = true;
                }
            }


            if (!model.IsHourlySalaryBasis && model.ScheduleId != 48)
            {
                ModelState.AddModelError("ScheduleId", "Для оклада по дням доступен только основной график работы!");
                ModelState.AddModelError("MessageStr", "Для оклада по дням доступен только основной график работы!");
                flgError = true;
            }

            if (model.IsHourlySalaryBasis && model.ScheduleId == 48)
            {
                ModelState.AddModelError("ScheduleId", "Для оклада по часам основной график работы не доступен!");
                ModelState.AddModelError("MessageStr", "Для оклада по часам основной график работы не доступен!");
                flgError = true;
            }

            if (!model.SendTo1C.HasValue)
            {
                //на время теста разрешили не раньше 1 апреля
                //if (model.EmploymentDate.HasValue && model.EmploymentDate.Value < DateTime.Today)
                if (model.EmploymentDate.HasValue && model.EmploymentDate.Value < Convert.ToDateTime("01/04/2015"))
                {
                    ModelState.AddModelError("EmploymentDate", "Дата принятия на работу не должна быть меньше текущей даты!");
                    ModelState.AddModelError("MessageStr", "Дата принятия на работу не должна быть меньше текущей даты!");
                    flgError = true;
                }

                //if (model.ContractDate.HasValue && model.ContractDate.Value < DateTime.Today)
                if (model.ContractDate.HasValue && model.ContractDate.Value < Convert.ToDateTime("01/04/2015"))//на время теста
                {
                    ModelState.AddModelError("EmploymentDate", "Дата приказа принятия на работу и дата ТД не должна быть меньше текущей даты!");
                    ModelState.AddModelError("MessageStr", "Дата приказа принятия на работу и дата ТД не должна быть меньше текущей даты!");
                    flgError = true;
                }

                if (model.ContractDate.HasValue && model.EmploymentDate.HasValue)
                {
                    if (model.ContractDate.Value > model.EmploymentDate.Value)
                    {
                        ModelState.AddModelError("EmploymentDate", "Дата принятия на работу не должна быть меньше даты приказа принятия на работу и даты ТД!");
                        ModelState.AddModelError("MessageStr", "Дата принятия на работу не должна быть меньше даты приказа принятия на работу и даты ТД!");
                        flgError = true;
                    }
                }

                if (model.ContractEndDate.HasValue)
                {
                    if (model.ContractEndDate.Value < model.ContractDate.Value)
                    {
                        ModelState.AddModelError("ContractEndDate", "Дата окончания ТД не должна быть меньше, чем дата ТД!");
                        ModelState.AddModelError("MessageStr", "Дата окончания ТД не должна быть меньше, чем дата ТД!");
                    }
                }
            }


            if (model.NorthExperienceType == 3)
            {
                if (model.NorthExperienceYears == 0 && model.NorthExperienceMonths == 0 && model.NorthExperienceDays == 0)
                {
                    ModelState.AddModelError("NorthExperienceYears", "*");
                    ModelState.AddModelError("NorthExperienceMonths", "*");
                    ModelState.AddModelError("NorthExperienceDays", "*");
                    ModelState.AddModelError("MessageStr", "Введите северный стаж!");
                }

                if (!model.NorthernAreaAddition.HasValue)
                {
                    ModelState.AddModelError("NorthernAreaAddition", "*");
                    ModelState.AddModelError("MessageStr", "Укажите северную надбавку!");
                }
            }
            

            if (!ModelState.IsValid && flgError)
                ModelState.AddModelError("MessageStr", "Проверьте правильность заполнени полей!");
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

        [NonAction]
        protected bool ValidateModel(ScanOriginalDocumentsModel model)
        {
            ValidateFileLength(model.INNScanFile, "INNScanFile", 2);
            ValidateFileLength(model.SNILSScanFile, "SNILSScanFile", 2);
            ValidateFileLength(model.DisabilityCertificateScanFile, "DisabilityCertificateScanFile", 2);
            ValidateFileLength(model.InternalPassportScanFile, "InternalPassportScanFile", 20);
            ValidateFileLength(model.HigherEducationDiplomaScanFile, "HigherEducationDiplomaScanFile", 5);
            ValidateFileLength(model.PostGraduateEducationDiplomaScanFile, "PostGraduateEducationDiplomaScanFile", 2);
            ValidateFileLength(model.CertificationScanFile, "CertificationScanFile", 2);
            ValidateFileLength(model.TrainingScanFile, "TrainingScanFile", 2);
            ValidateFileLength(model.MarriageCertificateScanFile, "MarriageCertificateScanFile", 2);
            ValidateFileLength(model.ChildBirthCertificateScanFile, "ChildBirthCertificateScanFile", 2);
            ValidateFileLength(model.MilitaryCardScanFile, "MilitaryCardScanFile", 20);
            ValidateFileLength(model.MobilizationTicketScanFile, "MobilizationTicketScanFile", 2);
            ValidateFileLength(model.WorkBookScanFile, "WorkBookScanFile", 20);
            ValidateFileLength(model.WorkBookSupplementScanFile, "WorkBookSupplementScanFile", 20);
            ValidateFileLength(model.PersonalDataProcessingScanFile, "PersonalDataProcessingScanFile", 1);
            ValidateFileLength(model.InfoValidityScanFile, "InfoValidityScanFile", 1);

            if (!model.AgreedToPersonalDataProcessing)
                ModelState.AddModelError("AgreedToPersonalDataProcessing", "Подтвердите правильность предоставленных данных! Подтвердив правильность предоставленных данных, Вы не сможете больше вносить изменения в данную часть анкеты!");

            if (!model.IsScanFinal && model.IsAgree)
            {
                ModelState.AddModelError("IsScanFinal", "Подтвердите достоверность всех приложенных сканов документов! Подтвердив данный пункт, Вы не сможете больше вносить изменения в данную часть анкеты!");
            }

            if (model.IsScanFinal && model.IsAgree)
            {
                ScanOriginalDocumentsModel mt = EmploymentBl.GetScanOriginalDocumentsModel(model.UserId);

                if (model.SNILSScanFile == null && string.IsNullOrEmpty(mt.SNILSScanAttachmentFilename))
                {
                    ModelState.AddModelError("SNILSScanFile", "Не выбран файл скана СНИЛС для загрузки!");
                }

                if (model.InternalPassportScanFile == null && string.IsNullOrEmpty(mt.InternalPassportScanAttachmentFilename))
                {
                    ModelState.AddModelError("InternalPassportScanFile", "Не выбран файл скана документа для загрузки!");
                }

                if (!model.IsVolga)
                {
                    if (model.HigherEducationDiplomaScanFile == null && string.IsNullOrEmpty(mt.HigherEducationDiplomaScanFileName))
                    {
                        ModelState.AddModelError("HigherEducationDiplomaScanFile", "Не выбран файл скана документа об образовании для загрузки!");
                    }
                }

                if (!model.IsVolga)
                {
                    if (model.WorkBookScanFile == null && string.IsNullOrEmpty(mt.WorkBookScanAttachmentFilename))
                    {
                        ModelState.AddModelError("WorkBookScanFile", "Не выбран файл скана трудовой книжки/заявления для загрузки!");
                    }
                }

                if (model.PersonalDataProcessingScanFile == null && string.IsNullOrEmpty(mt.PersonalDataProcessingScanAttachmentFilename))
                {
                    ModelState.AddModelError("PersonalDataProcessingScanFile", "Не выбран файл скана для загрузки!");
                }

                if (model.InfoValidityScanFile == null && string.IsNullOrEmpty(mt.InfoValidityScanAttachmentFilename))
                {
                    ModelState.AddModelError("InfoValidityScanFile", "Не выбран файл скана для загрузки!");
                }
            }


            return ModelState.IsValid;
        }
        #endregion

        #region Attachments

        [ReportAuthorize(UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.PersonnelManager | UserRole.Estimator | UserRole.OutsourcingManager | UserRole.Candidate)]
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
        public ActionResult GetPrintT2(int userId)
        {
            return GetPrintForm("PrintT2", userId);
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
        public ActionResult GetPrintRegisterPersonalRecord(int userId)
        {
            return GetPrintForm("PrintRegisterPersonalRecord", userId);
        }

        [HttpGet]
        public ActionResult GetPrintInstructionOfSecret(int userId)
        {
            return GetPrintForm("PrintInstructionOfSecret", userId);
        }

        [HttpGet]
        public ActionResult GetPrintInstructionEnsuringSafety(int userId)
        {
            return GetPrintForm("PrintInstructionEnsuringSafety", userId);
        }

        [HttpGet]
        public ActionResult GetPrintAgreePersonForChecking(int userId)
        {
            return GetPrintForm("PrintAgreePersonForChecking", userId);
        }

        [HttpGet]
        public ActionResult GetPrintCashWorkAddition1(int userId)
        {
            return GetPrintForm("PrintCashWorkAddition1", userId);
        }

        [HttpGet]
        public ActionResult GetPrintCashWorkAddition2(int userId)
        {
            return GetPrintForm("PrintCashWorkAddition2", userId);
        }

        [HttpGet]
        public ActionResult GetPrintObligationTradeSecret(int userId)
        {
            return GetPrintForm("PrintObligationTradeSecret", userId);
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
        public ActionResult PrintT2(int userId)
        {
            PrintT2Model model = EmploymentBl.GetPrintT2Model(userId);
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
        public ActionResult PrintRegisterPersonalRecord(int userId)
        {
            PrintRegisterPersonalRecordModel model = EmploymentBl.GetPrintRegisterPersonalRecordModel(userId);
            return View(model);
        }

        [HttpGet]
        public ActionResult PrintInstructionOfSecret(int userId)
        {
            PrintInstructionOfSecretModel model = EmploymentBl.GetPrintInstructionOfSecretModel(userId);
            return View(model);
        }

        [HttpGet]
        public ActionResult PrintInstructionEnsuringSafety(int userId)
        {
            PrintInstructionEnsuringSafetyModel model = EmploymentBl.GetPrintInstructionEnsuringSafetyModel(userId);
            return View(model);
        }

        [HttpGet]
        public ActionResult PrintAgreePersonForChecking(int userId)
        {
            PrintAgreePersonForCheckingModel model = EmploymentBl.GetPrintAgreePersonForCheckingModel(userId);
            return View(model);
        }

        [HttpGet]
        public ActionResult PrintCashWorkAddition1(int userId)
        {
            PrintCashWorkAddition1Model model = EmploymentBl.GetPrintCashWorkAddition1Model(userId);
            return View(model);
        }

        [HttpGet]
        public ActionResult PrintCashWorkAddition2(int userId)
        {
            PrintCashWorkAddition2Model model = EmploymentBl.GetPrintCashWorkAddition2Model(userId);
            return View(model);
        }

        [HttpGet]
        public ActionResult PrintObligationTradeSecret(int userId)
        {
            PrintObligationTradeSecretModel model = EmploymentBl.GetPrintObligationTradeSecretModel(userId);
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

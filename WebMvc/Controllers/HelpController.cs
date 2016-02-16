using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;
using WebMvc.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace WebMvc.Controllers
{
    [Authorize]
    [PreventSpam]
    public class HelpController : BaseController
    {
        protected IHelpBl helpBl;
        public IHelpBl HelpBl
        {
            get
            {
                helpBl = Ioc.Resolve<IHelpBl>();
                return Validate.Dependency(helpBl);
            }
        }
        protected IRequestBl requestBl;
        public IRequestBl RequestBl
        {
            get
            {
                requestBl = Ioc.Resolve<IRequestBl>();
                return Validate.Dependency(requestBl);
            }
        }

        //public const string StrCommentsLoadError = "Ошибка при загрузке данных:";
        public const string StrCommentIsRequired = "Комментарий - обязательное поле";
        public const string StrReleaseDateIsRequired = "Дата версии - обязательное поле";
        public const string StrReleaseDateIsInvalid = "Дата версии - неправильная дата";
        public const string StrCommentLengthError = "Длина поля 'Комментарий' не может превышать {0} символов.";
        public const int MaxVersionLength = 8192;

        public const string StrQuestionIsRequired = "Вопрос - обязательное поле";
        public const string StrQuestionLengthError = "Длина поля 'Вопрос' не может превышать {0} символов.";
        public const int MaxQuestionLength = 8192;
        public const string StrAnswerIsRequired = "Ответ - обязательное поле";
        public const string StrAnswerLengthError = "Длина поля 'Ответ' не может превышать {0} символов.";
        public const int MaxAnswerLength = 8192;
        public const string StrCannotDeleteTemplate = "Вам запрещено удаление информации";
        public const string StrCannotSendRequestWithoutTemplate = "Заявка не может быть согласована без прикрепленого скана образца.";

        public const string StrNameIsRequired = "Название - обязательное поле";
        public const string StrNameIsTooLong = "Длина поля 'Название' не может превышать {0} символов.";
        public const string StrInvalidFileSize = "Размер прикрепленного файла > {0} байт.";
        public const string StrErrorOnDataLoad = "Ошибка при загрузке данных: ";
        public const string StrDateRangeIsInvalid = "Дата в поле <Период с> не может быть больше даты в поле <по>.";

        public const int MaxTemplateNameLength = 256;

        #region Service Requests
        [HttpGet]
        [ReportAuthorize(UserRole.Employee | UserRole.Manager | UserRole.OutsourcingManager | UserRole.ConsultantPersonnel
            | UserRole.Admin | UserRole.ConsultantOutsourcing | UserRole.PersonnelManager | UserRole.Estimator | UserRole.DismissedEmployee)]
        public ActionResult Index()
        {
            //UserRole.PersonnelManager
            //var dto = UserDto;
            var model = HelpBl.GetServiceRequestsList();
            //SetMissionOrderListModelFromSession(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(HelpServiceRequestsListModel model)
        { 
            //ModelState.Clear();
            bool hasError = !ValidateModel(model);
            //if (!hasError)
            //    SetMissionOrderFilterToSession(model);
            if (model.IsOriginalsReceivedChanged)
            {
                HelpBl.SaveDocumentsToModel(model);
                model.IsOriginalsReceivedChanged = false;
            }
            HelpBl.SetServiceRequestsListModel(model, hasError);
            //if (model.HasErrors)
            //    ModelState.AddModelError(string.Empty, "При согласовании приказов произошла(и) ошибка(и).Не все приказы были согласованы.");
            return View(model);
        }
        [HttpPost]
        public string SaveServiceDocuments()
        {
            return("test");
        }
        protected bool ValidateModel(BeginEndCreateDate model)
        {
            if (model.BeginDate.HasValue && model.EndDate.HasValue &&
                model.BeginDate.Value > model.EndDate.Value)
                ModelState.AddModelError("BeginDate",StrDateRangeIsInvalid);
            return ModelState.IsValid;
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.PersonnelManager | UserRole.ConsultantPersonnel)]
        public ActionResult CreateServiceRequest(int? isForQuestion)
        {
            CreateHelpServiceRequestModel model = HelpBl.GetCreateHelpServiceRequestModel();
            if (isForQuestion.HasValue)
                model.IsForQuestion = true;
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateServiceRequest(CreateHelpServiceRequestModel model)
        {
            return RedirectToAction(model.IsForQuestion ? "HelpQuestionEdit" : "ServiceRequestEdit", 
                new RouteValueDictionary {
                                            {"id", 0}, 
                                            {"userId", model.UserId}
                                          });
        }
        /// <summary>
        /// Автозаполнение фио в создании набора реквизитов.
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public ActionResult AutocompletePersonSearch(string term)
        {       
            List<int> roleIds=null;
            try
            {
                if (Request.UrlReferrer.AbsolutePath.ToLower().Contains("staffmovements"))
                {
                    roleIds = new List<int>() { 4 };
                }
            }
            catch (Exception e) { Log.Error("Произошла ошибка в контроллере HELP AutocompletePersonSearch ", e); }
            IList<IdNameDto> Persons = HelpBl.GetPersonAutocomplete(term,roleIds);

            var PersonList = Persons.Select(a => new { label = a.Name, Id = a.Id }).Distinct();

            return Json(PersonList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateServiceRequestForFired()
        {
            HelpServiceRequestEditModel model = HelpBl.GetServiceRequestEditModel(0, AuthenticationService.CurrentUser.Id);
            model.IsForFiredUser = true;
            return View("ServiceRequestEdit", model);
        }
        [HttpGet]
        [ReportAuthorize(UserRole.Employee | UserRole.Manager | UserRole.OutsourcingManager | UserRole.ConsultantPersonnel
         | UserRole.Admin | UserRole.ConsultantOutsourcing | UserRole.PersonnelManager | UserRole.Estimator| UserRole.DismissedEmployee)]
        public ActionResult ServiceRequestEdit(int id, int? userId)
        {
            HelpServiceRequestEditModel model = HelpBl.GetServiceRequestEditModel(id, userId);
            return View(model);
        }
        [HttpGet]
        public ContentResult GetDictionariesStates(int typeId)
        {
            //bool saveResult;
            //string error = string.Empty;
            HelpServiceDictionariesStatesModel model = new HelpServiceDictionariesStatesModel {Error = string.Empty};
            try
            {
                    HelpBl.GetDictionariesStates(typeId,model);
            }
            catch (Exception ex)
            {
                Log.Error("Exception on GetDictionariesStates:", ex);
                model.Error = ex.GetBaseException().Message;
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }
        [HttpPost]
        public ActionResult ServiceRequestEdit(HelpServiceRequestEditModel model)
        {
            //CorrectCheckboxes(model);
            CorrectDropdowns(model);
            UploadFileDto fileDto = GetFileContext(); 
            //bool needToReload;
            //string error;
            if (!ValidateServiceRequestEditModel(model, fileDto))
            {
                model.Operation = 0;
                HelpBl.ReloadDictionariesToModel(model);
                return View(model);
            }
            string error;
            if (!HelpBl.SaveServiceRequestEditModel(model, fileDto, out error))
            {
                //HttpContext.AddError(new Exception(error));
                if (model.ReloadPage)
                {
                    ModelState.Clear();
                    if (!string.IsNullOrEmpty(error))
                        ModelState.AddModelError("", error);
                    return View(HelpBl.GetServiceRequestEditModel(model.Id, model.UserId));
                }
                if (!string.IsNullOrEmpty(error))
                    ModelState.AddModelError("", error);
            }
            return View(model);
        }
        protected bool ValidateServiceRequestEditModel(HelpServiceRequestEditModel model, UploadFileDto fileDto)
        {
            /*if (model.Id > 0 && fileDto == null && model.AttachmentId == 0)
            {
                if (model.Operation == 1)
                    ModelState.AddModelError(string.Empty,StrCannotSendRequestWithoutTemplate);
            }*/

            //if (model.TypeId == 15)
            //{
            //    ModelState.AddModelError("TypeId", "Данный вид услуги не доступен для создания заявки!");
            //}

            if(model.IsForFiredUser)
            {
                if (String.IsNullOrWhiteSpace(model.UserBirthDate)) ModelState.AddModelError("UserBirthDate", "Не заполнена дата рождения");
                if (String.IsNullOrWhiteSpace(model.FiredUserName)) ModelState.AddModelError("FiredUserName", "Не заполнено имя");
                if (String.IsNullOrWhiteSpace(model.FiredUserSurname)) ModelState.AddModelError("FiredUserSurname", "Не заполнено фамилия");
                if (String.IsNullOrWhiteSpace(model.FiredUserPatronymic)) ModelState.AddModelError("FiredUserPatronymic", "Не заполнено отчество");
            }
            return ModelState.IsValid;
        }
        protected void CorrectDropdowns(HelpServiceRequestEditModel model)
        {
            if (!model.IsEditable)
            {
                model.TypeId = model.TypeIdHidden;
                model.ProductionTimeTypeId = model.ProductionTimeTypeIdHidden;
                model.PeriodId = model.PeriodIdHidden;
                model.TransferMethodTypeId = model.TransferMethodTypeIdHidden;
            }
            model.NoteList = HelpBl.GetAllNodeTypesDto();
        }
        #endregion
        #region Service Questions
        [HttpGet]
        public ActionResult ServiceQuestionsList()
        {
            var model = HelpBl.GetServiceQuestionsListModel();
            //SetMissionOrderListModelFromSession(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult ServiceQuestionsList(HelpServiceQuestionsListModel model)
        {
            bool hasError = !ValidateModel(model);
            HelpBl.SetServiceQuestionsListModel(model, hasError);
            return View(model);
        }
        [HttpGet]
        public ActionResult HelpQuestionEdit(int id, int? userId)
        {
            HelpQuestionEditModel model = HelpBl.GetHelpQuestionEditModel(id, userId);
            return View(model);
        }
        [HttpPost]
        public ActionResult HelpQuestionEdit(HelpQuestionEditModel model)
        {
            CorrectModel(model);
            CorrectDropdowns(model);
            //UploadFileDto fileDto = GetFileContext();
            //bool needToReload;
            //string error;
            if (!ValidateHelpQuestionEditModel(model))
            {
                model.Operation = 0;
                HelpBl.ReloadDictionariesToModel(model);
                return View(model);
            }
            string error;
            if (!HelpBl.SaveHelpQuestionEditModel(model, out error))
            {
                //HttpContext.AddError(new Exception(error));
                if (model.ReloadPage)
                {
                    ModelState.Clear();
                    if (!string.IsNullOrEmpty(error))
                        ModelState.AddModelError("", error);
                    return View(HelpBl.GetHelpQuestionEditModel(model.Id, model.UserId));
                }
                if (!string.IsNullOrEmpty(error))
                    ModelState.AddModelError("", error);
            }
            return View(model);
        }
        protected bool ValidateHelpQuestionEditModel(HelpQuestionEditModel model)
        {
            return ModelState.IsValid;
        }
        protected void CorrectModel(HelpQuestionEditModel model)
        {
            if (model.Operation == 6 && !string.IsNullOrEmpty(model.Answer))
            {
                if (ModelState.ContainsKey("Answer"))
                    ModelState.Remove("Answer");
            }
            if (model.Operation == 5)
            {
                if (ModelState.ContainsKey("Question"))
                    ModelState.Remove("Question");
                if (ModelState.ContainsKey("Answer"))
                    ModelState.Remove("Answer");
            }
        }
        protected void CorrectDropdowns(HelpQuestionEditModel model)
        {
            if (!model.IsTypeEditable)
            {
                model.TypeId = model.TypeIdHidden;
                model.SubtypeId = model.SubtypeIdHidden;
            }
        }
        [HttpGet]
        public ContentResult GetSubtypesForType(int typeId)
        {
            //bool saveResult;
            //string error = string.Empty;
            HelpQuestionSubtypesModel model = new HelpQuestionSubtypesModel { Error = string.Empty };
            try
            {
                HelpBl.GetSubtypesForType(typeId, model);
            }
            catch (Exception ex)
            {
                Log.Error("Exception on GetSubtypesForType:", ex);
                model.Error = ex.GetBaseException().Message;
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }
         [HttpGet]
        public ActionResult QuestionRedirectDialog(int id)
        {
            try
            {
                HelpQuestionRedirectModel model = HelpBl.GetQuestionRedirectModel(id);
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                string error = StrErrorOnDataLoad + ex.GetBaseException().Message;
                return PartialView("QuestionRedirectDialogError", new DialogErrorModel { Error = error });
            }
        }
        
        #endregion
        #region Versions
        [HttpGet]
        public ActionResult Version()
        {
            HelpVersionsListModel model = HelpBl.GetVersionsModel();
            return View(model);
        }
        [HttpGet]
        public ActionResult RenderVersions()
        {
            //IContractRequest bo = Ioc.Resolve<IContractRequest>();
            HelpVersionsListModel model = HelpBl.GetVersionsModel();
            return PartialView("VersionPartial", model);
        }

        [HttpGet]
        public ActionResult EditVersionDialog(int id)
        {
            try
            {
                HelpEditVersionModel model = HelpBl.GetEditVersionModel(id);
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                string error = StrErrorOnDataLoad + ex.GetBaseException().Message;
                return PartialView("VersionDialogError", new DialogErrorModel { Error = error });
            }
        }

        [HttpPost]
        public ContentResult SaveVersion(HelpSaveVersionModelFromView vModel)
        {
            bool saveResult = false;
            string error = string.Empty;
            DateTime dtReleaseDate = DateTime.Today;
            try
            {
                if (string.IsNullOrEmpty(vModel.Comment.Trim()))
                    error = StrCommentIsRequired;
                else if (vModel.Comment.Trim().Length > MaxVersionLength)
                    error = string.Format(StrCommentLengthError, MaxCommentLength);

                if (string.IsNullOrEmpty(vModel.ReleaseDate.Trim()))
                    error = StrReleaseDateIsRequired;
                else if (!DateTime.TryParse(vModel.ReleaseDate, out dtReleaseDate))
                    error = StrReleaseDateIsInvalid;
                if(string.IsNullOrEmpty(error))
                {
                    HelpSaveVersionModel model = new HelpSaveVersionModel
                                                     {
                                                         Comment = vModel.Comment.Trim(),
                                                         Id = vModel.Id,
                                                         ReleaseDate = dtReleaseDate,
                                                     };
                    saveResult = HelpBl.SaveVersion(model);
                    if (!saveResult)
                        error = model.Error;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception on SaveVersion:", ex);
                error = ex.GetBaseException().Message;
                saveResult = false;
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(new SaveTypeResult { Error = error, Result = saveResult });
            return Content(jsonString);
        }

        [HttpGet]
        public ContentResult DeleteVersion(int id)
        {
            bool saveResult = false;
            string error;
            try
            {
                if (AuthenticationService.CurrentUser.UserRole == UserRole.Admin)
                {
                    DeleteAttacmentModel model = new DeleteAttacmentModel {Id = id};
                    saveResult = HelpBl.DeleteVersion(model);
                    error = model.Error;
                }
                else
                    error = StrCannotDeleteTemplate;

            }
            catch (Exception ex)
            {
                Log.Error("Exception on DeleteVersion:", ex);
                error = ex.GetBaseException().Message;
                saveResult = false;
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(new SaveTypeResult { Error = error, Result = saveResult });
            return Content(jsonString);
        }
       
        #endregion
        #region Faq
        [HttpGet]
        public ActionResult Faq()
        {
            HelpFaqListModel model = HelpBl.GetFaqModel();
            return View(model);
        }
        [HttpGet]
        public ActionResult RenderQuestions()
        {
            HelpFaqListModel model = HelpBl.GetFaqModel();
            return PartialView("FaqPartial", model);
        }
        [HttpGet]
        public ActionResult EditQuestionDialog(int id)
        {
            try
            {
                HelpEditFaqModel model = HelpBl.GetEditQuestionModel(id);
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                string error = StrErrorOnDataLoad + ex.GetBaseException().Message;
                return PartialView("FaqDialogError", new DialogErrorModel { Error = error });
            }
        }

        [HttpPost]
        public ContentResult SaveQuestion(/*int id, string question, string answer*/HelpSaveFaqModel model)
        {
            bool saveResult = false;
            string error = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(model.Question.Trim()))
                    error = StrQuestionIsRequired;
                else if (model.Question.Trim().Length > MaxQuestionLength)
                    error = string.Format(StrQuestionLengthError, MaxQuestionLength);

                if (string.IsNullOrEmpty(model.Answer.Trim()))
                    error = StrAnswerIsRequired;
                else if (model.Answer.Trim().Length > MaxAnswerLength)
                    error = string.Format(StrAnswerLengthError, MaxAnswerLength);

                
                if (string.IsNullOrEmpty(error))
                {
                    saveResult = HelpBl.SaveFaq(model);
                    if (!saveResult)
                        error = model.Error;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception on SaveQuestion:", ex);
                error = ex.GetBaseException().Message;
                saveResult = false;
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(new SaveTypeResult { Error = error, Result = saveResult });
            return Content(jsonString);
        }
        [HttpGet]
        public ContentResult DeleteQuestion(int id)
        {
            bool saveResult;
            string error;
            try
            {
                DeleteAttacmentModel model = new DeleteAttacmentModel { Id = id };
                saveResult = HelpBl.DeleteFaq(model);
                error = model.Error;

            }
            catch (Exception ex)
            {
                Log.Error("Exception on DeleteQuestion:", ex);
                error = ex.GetBaseException().Message;
                saveResult = false;
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(new SaveTypeResult { Error = error, Result = saveResult });
            return Content(jsonString);
        }
        #endregion
        #region Templates
        [HttpGet]
        public ActionResult Template()
        {
            HelpTemplateListModel model = HelpBl.GetTemplateModel();
            return View(model);
        }
        [HttpGet]
        public ActionResult RenderTemplates()
        {
            HelpTemplateListModel model = HelpBl.GetTemplateModel();
            return PartialView("TemplatePartial", model);
        }
        [HttpGet]
        public ActionResult EditTemplateDialog(int id)
        {
            try
            {
                //HelpTemplateEditModel model = new HelpTemplateEditModel
                //{
                //    DocumentId = id,
                //    Description = name,
                //    IsDescriptionDisabled = !string.IsNullOrEmpty(name)
                //};
                HelpTemplateEditModel model = HelpBl.GetTemplateEditModel(id);
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                string error = StrErrorOnDataLoad + ex.GetBaseException().Message;
                return PartialView("EditTemplateDialogError", new DialogErrorModel { Error = error });
            }
        }

        [HttpPost]
        public ContentResult SaveTemplate(int id, string name, object qqFile)
        {
            bool saveResult;
            string error;
            try
            {
                var length = Request.ContentLength;
                var bytes = new byte[length];
                Request.InputStream.Read(bytes, 0, length);

                saveResult = true;
                if (length > MaxFileSize)
                {
                    error = string.Format(StrInvalidFileSize, MaxFileSize);
                }
                else if (name == null || string.IsNullOrEmpty(name.Trim()))
                {
                    error = StrNameIsRequired;
                }
                else if (name.Trim().Length > MaxTemplateNameLength)
                {
                    error = string.Format(StrNameIsTooLong, MaxCommentLength);
                }
                else
                {
                    byte[] context;
                    string fileName = MissionOrderController.GetFileName(qqFile, out context);
                    if (context != null)
                        bytes = context;
                    var model = new SaveAttacmentModel
                    {
                        EntityId = 0,
                        Id = id,
                        EntityTypeId = RequestAttachmentTypeEnum.HelpTemplate,
                        Description = name.Trim(),
                        FileDto = new UploadFileDto
                        {
                            Context = bytes,
                            FileName = fileName,
                            //ContextType = Request.Content,
                        }
                    };
                    saveResult = HelpBl.SaveTemplate(model);
                    error = model.Error;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception on SaveAttachment:", ex);
                error = ex.GetBaseException().Message;
                saveResult = false;
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(new SaveTypeResult { Error = error, Result = saveResult });
            return Content(jsonString);
        }
        [HttpPost]
        public ContentResult SaveTemplateName(SaveAttacmentModelFromView vModel)
        {
            bool saveResult;
            string error;
            try
            {
                saveResult = true;
                if (vModel.Name == null || string.IsNullOrEmpty(vModel.Name.Trim()))
                    error = StrNameIsRequired;
                else if (vModel.Name.Trim().Length > MaxTemplateNameLength)
                    error = string.Format(StrNameIsTooLong, MaxCommentLength);
                else
                {
                    var model = new SaveAttacmentModel
                    {
                        EntityId = 0,
                        Id = vModel.Id,
                        EntityTypeId = RequestAttachmentTypeEnum.HelpTemplate,
                        Description = vModel.Name.Trim(),
                    };
                    saveResult = HelpBl.SaveTemplateName(model);
                    error = model.Error;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception on SaveAttachment:", ex);
                error = ex.GetBaseException().Message;
                saveResult = false;
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(new SaveTypeResult { Error = error, Result = saveResult });
            return Content(jsonString);
        }
        [HttpGet]
        public FileContentResult ViewTemplate(int id)
        {
            try
            {
                AttachmentModel model = RequestBl.GetFileContext(id);
                return File(model.Context, model.ContextType, model.FileName);
            }
            catch (Exception ex)
            {
                Log.Error("Error on ViewTemplate:", ex);
                throw;
            }
        }
        [HttpGet]
        public ContentResult DeleteTemplate(int id)
        {
            bool saveResult;
            string error;
            try
            {
                DeleteAttacmentModel model = new DeleteAttacmentModel { Id = id };
                saveResult = RequestBl.DeleteAttachment(model);
                error = model.Error;

            }
            catch (Exception ex)
            {
                Log.Error("Exception on DeleteTemplate:", ex);
                error = ex.GetBaseException().Message;
                saveResult = false;
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(new SaveTypeResult { Error = error, Result = saveResult });
            return Content(jsonString);
        }

        #endregion
        #region Personnel Billing
        [HttpGet]
        [ReportAuthorize(UserRole.OutsourcingManager  | UserRole.Estimator | UserRole.PersonnelManager | UserRole.ConsultantOutsourcing | UserRole.ConsultantPersonnel | UserRole.Accountant | UserRole.TaxCollector)]
        public ActionResult PersonnelBillingList()
        {
            var model = HelpBl.GetPersonnelBillingList();
            return View(model);
        }
        [HttpPost]
        [ReportAuthorize(UserRole.OutsourcingManager  | UserRole.Estimator | UserRole.PersonnelManager | UserRole.ConsultantOutsourcing | UserRole.ConsultantPersonnel | UserRole.Accountant | UserRole.TaxCollector)]
        public ActionResult PersonnelBillingList(HelpPersonnelBillingListModel model)
        {
            bool hasError = !ValidateModel(model);
            HelpBl.SetPersonnelBillingListModel(model, hasError);
            return View(model);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.OutsourcingManager  | UserRole.Estimator | UserRole.PersonnelManager | UserRole.ConsultantOutsourcing | UserRole.ConsultantPersonnel | UserRole.Accountant | UserRole.TaxCollector)]
        public ActionResult EditPersonnelBillingRequest(int id)
        {
            EditPersonnelBillingRequestViewModel model = HelpBl.GetPersonnelBillingRequestEditModel(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditPersonnelBillingRequest(EditPersonnelBillingRequestViewModel model)
        {
            CorrectCheckboxes(model);
            CorrectDropdowns(model);
            if (!ValidateModel(model))
            {
                HelpBl.ReloadDictionariesToModel(model);
                return View(model);
            }
            string error;
            if (!HelpBl.SavePersonnelBillingRequestModel(model, out error))
            {
                if (model.ReloadPage)
                {
                    ModelState.Clear();
                    if (!string.IsNullOrEmpty(error))
                        ModelState.AddModelError("", error);
                    return View(HelpBl.GetPersonnelBillingRequestEditModel(model.Id));
                }
                if (!string.IsNullOrEmpty(error))
                    ModelState.AddModelError("", error);
            }
            return View(model);
        }
        /// <summary>
        /// Сохраняем список исполнителей при создании новой задачи в биллинге.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult HelpBillingExecutorTasks(IList<HelpPersonnelBillingRecipientDto> RecipientList, IList<HelpPersonnelBillingRecipientGroupsDto> RecipientGroups)
        {
            var jsonSerializer = new JavaScriptSerializer();
            HelpPersonnelBillingExecutorsDto ExecutorList = HelpBl.GetRecipients(RecipientList, RecipientGroups);


            string jsonString = jsonSerializer.Serialize(ExecutorList);
            return Content(jsonString);
        }

        protected bool ValidateModel(EditPersonnelBillingRequestViewModel model)
        {
            if (model.Operation == 1 && string.IsNullOrEmpty(model.DateSended) && ((model.RecipientList == null || model.RecipientList.Count == 0) || model.RecipientList.Where(x => x.IsRecipient == true).Count() == 0))
                ModelState.AddModelError("RecipientList", "Выберите исполнителя!");
            return ModelState.IsValid;
        }
        protected void CorrectDropdowns(EditPersonnelBillingRequestViewModel model)
        {
            if (!model.IsEditable)
            {
                model.TitleId = model.TitleIdHidden;
                //model.RecipientId = model.RecipientIdHidden;
                model.UrgencyId = model.UrgencyIdHidden;
            }
        }
        protected void CorrectCheckboxes(EditPersonnelBillingRequestViewModel model)
        {
            if (!model.IsWorkBeginAvailable)
            {
                if (ModelState.ContainsKey("IsWorkBegin"))
                    ModelState.Remove("IsWorkBegin");
                model.IsWorkBegin = model.IsWorkBeginHidden;
            }
        }
        #endregion
        #region Attachments
        [HttpGet]
        public FileContentResult ViewAttachment(int id)
        {
            try
            {
                AttachmentModel model = RequestBl.GetFileContext(id);
                return File(model.Context, model.ContextType, model.FileName);
            }
            catch (Exception ex)
            {
                Log.Error("Error on ViewAttachment:", ex);
                throw;
            }
        }
        [HttpGet]
        public ActionResult RenderAttachments(int id, int typeId)
        {
            //IContractRequest bo = Ioc.Resolve<IContractRequest>();
            RequestAttachmentsModel model = HelpBl.GetBillingAttachmentsModel(id, (RequestAttachmentTypeEnum)typeId);
            return PartialView("RequestAttachmentsPartial", model);
        }
        [HttpGet]
        public ActionResult AddAttachmentDialog(int id, int typeId, string name)
        {
            try
            {
                AddAttachmentModel model = new AddAttachmentModel
                {
                    DocumentId = id,
                    Description = string.Empty,
                    IsDescriptionDisabled = false
                };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                string error = "Ошибка при загрузке данных: " + ex.GetBaseException().Message;
                return PartialView("AttachmentDialogError", new DialogErrorModel { Error = error });
            }
        }
        [HttpPost]
        public ContentResult SaveAttachment(int id,int type, string description, object qqFile)
        {
            bool saveResult;
            string error;
            try
            {
                var length = Request.ContentLength;
                var bytes = new byte[length];
                Request.InputStream.Read(bytes, 0, length);

                saveResult = true;
                if (length > MaxFileSize)
                {
                    error = string.Format("Размер прикрепленного файла > {0} байт.", MaxFileSize);
                }
                else if (description == null || string.IsNullOrEmpty(description.Trim()))
                {
                    error = "Описание - обязательное поле";
                }
                else if (description.Trim().Length > MaxCommentLength)
                {
                    error = string.Format("Длина поля 'Описание' не может превышать {0} символов.", MaxCommentLength);
                }
                else
                {
                    byte[] context;
                    string fileName = MissionOrderController.GetFileName(qqFile, out context);
                    if (context != null)
                        bytes = context;
                    var model = new SaveAttacmentModel
                    {
                        EntityId = id,
                        EntityTypeId = (RequestAttachmentTypeEnum)type,
                        Description = description.Trim(),
                        FileDto = new UploadFileDto
                        {
                            Context = bytes,
                            FileName = fileName,
                        }
                    };
                    saveResult = RequestBl.SaveAttachment(model);
                    error = model.Error;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception on SaveAttachment:", ex);
                error = ex.GetBaseException().Message;
                saveResult = false;
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(new SaveTypeResult { Error = error, Result = saveResult });
            return Content(jsonString);
        }
        [HttpGet]
        public ContentResult DeleteAttachment(int id)
        {
            bool saveResult;
            string error;
            try
            {
                DeleteAttacmentModel model = new DeleteAttacmentModel { Id = id };
                saveResult = RequestBl.DeleteAttachment(model);
                error = model.Error;

            }
            catch (Exception ex)
            {
                Log.Error("Exception on DeleteAttachment:", ex);
                error = ex.GetBaseException().Message;
                saveResult = false;
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(new SaveTypeResult { Error = error, Result = saveResult });
            return Content(jsonString);
        }
        [HttpGet]
        public FileContentResult GetPrintForm(int id, int typeId)
        {
            try
            {
                AttachmentModel model = RequestBl.GetPrintFormFileContext(id, (RequestPrintFormTypeEnum)typeId);
                return File(model.Context, model.ContextType, model.FileName);
            }
            catch (Exception ex)
            {
                Log.Error("Error on GetPrintForm:", ex);
                throw;
            }
        }
        #endregion
    }
}
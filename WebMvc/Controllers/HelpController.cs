﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Reports.Core;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;

namespace WebMvc.Controllers
{
    [Authorize]
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

        public const string StrCommentsLoadError = "Ошибка при загрузке данных:";
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

        public const int MaxTemplateNameLength = 256;

        [HttpGet]
        public ActionResult Index()
        {
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
            HelpBl.SetServiceRequestsListModel(model, hasError);
            //if (model.HasErrors)
            //    ModelState.AddModelError(string.Empty, "При согласовании приказов произошла(и) ошибка(и).Не все приказы были согласованы.");
            return View(model);
        }
        protected bool ValidateModel(BeginEndCreateDate model)
        {
            if (model.BeginDate.HasValue && model.EndDate.HasValue &&
                model.BeginDate.Value > model.EndDate.Value)
                ModelState.AddModelError("BeginDate", "Дата в поле <Период с> не может быть больше даты в поле <по>.");
            return ModelState.IsValid;
        }

        public ActionResult ServiceRequestEdit(int id, int? userId)
        {
            HelpServiceRequestEditModel model = HelpBl.GetServiceRequestEditModel(id, userId);
            return View(model);
        }
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
                string error = StrCommentsLoadError + ex.GetBaseException().Message;
                return PartialView("VersionDialogError", new DialogErrorModel { Error = error });
            }
        }

        [HttpPost]
        public ContentResult SaveVersion(int id, string comment, string releaseDate)
        {
            bool saveResult = false;
            string error = string.Empty;
            DateTime dtReleaseDate = DateTime.Today;
            try
            {
                if (string.IsNullOrEmpty(comment.Trim()))
                    error = StrCommentIsRequired;
                else if (comment.Trim().Length > MaxVersionLength)
                    error = string.Format(StrCommentLengthError, MaxCommentLength);
                
                if (string.IsNullOrEmpty(releaseDate.Trim()))
                    error = StrReleaseDateIsRequired;
                else if (!DateTime.TryParse(releaseDate,out dtReleaseDate))
                    error = StrReleaseDateIsInvalid;
                if(string.IsNullOrEmpty(error))
                {
                    HelpSaveVersionModel model = new HelpSaveVersionModel
                                                     {
                                                         Comment = comment.Trim(),
                                                         Id = id,
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
                string error = StrCommentsLoadError + ex.GetBaseException().Message;
                return PartialView("FaqDialogError", new DialogErrorModel { Error = error });
            }
        }

        [HttpPost]
        public ContentResult SaveQuestion(int id, string question, string answer)
        {
            bool saveResult = false;
            string error = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(question.Trim()))
                    error = StrQuestionIsRequired;
                else if (question.Trim().Length > MaxQuestionLength)
                    error = string.Format(StrQuestionLengthError, MaxQuestionLength);

                if (string.IsNullOrEmpty(answer.Trim()))
                    error = StrAnswerIsRequired;
                else if (answer.Trim().Length > MaxAnswerLength)
                    error = string.Format(StrAnswerLengthError, MaxAnswerLength);

                
                if (string.IsNullOrEmpty(error))
                {
                    HelpSaveFaqModel model = new HelpSaveFaqModel
                    {
                        Question = question.Trim(),
                        Answer = answer.Trim(),
                        Id = id,
                    };
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
                string error = "Ошибка при загрузке данных: " + ex.GetBaseException().Message;
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
                    error = string.Format("Размер прикрепленного файла > {0} байт.", MaxFileSize);
                }
                else if (name == null || string.IsNullOrEmpty(name.Trim()))
                {
                    error = "Название - обязательное поле";
                }
                else if (name.Trim().Length > MaxTemplateNameLength)
                {
                    error = string.Format("Длина поля 'Название' не может превышать {0} символов.", MaxCommentLength);
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
        public ContentResult SaveTemplateName(int id, string name)
        {
            bool saveResult;
            string error;
            try
            {
                saveResult = true;
                if (name == null || string.IsNullOrEmpty(name.Trim()))
                    error = "Название - обязательное поле";
                else if (name.Trim().Length > MaxTemplateNameLength)
                    error = string.Format("Длина поля 'Название' не может превышать {0} символов.", MaxCommentLength);
                else
                {
                    var model = new SaveAttacmentModel
                    {
                        EntityId = 0,
                        Id = id,
                        EntityTypeId = RequestAttachmentTypeEnum.HelpTemplate,
                        Description = name.Trim(),
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

    }
}
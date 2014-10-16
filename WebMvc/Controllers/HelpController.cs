﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Reports.Core;
using Reports.Core.Dto;
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

        [HttpGet]
        public ActionResult Index()
        {
            return View();
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
            bool saveResult;
            string error;
            try
            {
                DeleteAttacmentModel model = new DeleteAttacmentModel { Id = id };
                saveResult = HelpBl.DeleteVersion(model);
                error = model.Error;

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
        #endregion

    }
}
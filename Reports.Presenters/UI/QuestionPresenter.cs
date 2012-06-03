using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Reports.CommonWeb;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Domain;
using Reports.Core.UI;
using Reports.Presenters.Services;
using Reports.Presenters.UI.Actions;
using Reports.Presenters.UI.Views;

namespace Reports.Presenters.UI
{
    public class QuestionPresenter : AbstractPresenter<IQuestionView>
    {
        #region Fields
        private IQuestionDao _questionDao;
        private IAuthenticationService _authenticationService;
        //private IConfigurationService _configurationService;
        #endregion
        #region Properties
        public IQuestionDao QuestionDao
        {
            get { return Validate.Dependency(_questionDao); }
            set { _questionDao = value; }
        }
        public IAuthenticationService AuthenticationService
        {
            get { return Validate.Dependency(_authenticationService); }
            set { _authenticationService = value; }
        }
        //public IConfigurationService ConfigurationService
        //{
        //    get { return Validate.Dependency(_configurationService); }
        //    set { _configurationService = value; }
        //}
        #endregion
        #region Methods
        protected override void OnInitialize()
        {
            base.OnInitialize();
            EditQuestionAction action = new EditQuestionAction();
            action.Parse(ContextService.QueryString);
            if (action.QuestionId.HasValue)
                View.QuestionId = action.QuestionId.Value;
            else
                View.QuestionId = new int?();
            //LoadData();
            //if (!AuthenticationService.CurrentUser.IsAdministrator &&
            //    (View.UserId != AuthenticationService.CurrentUser.Id))
            //{
            //    log.ErrorFormat("Access to page {0} is denied for user with id {1}. View.UserId {2}.", "NDFL.aspx", AuthenticationService.CurrentUser.Id, View.UserId);
            //    throw new ApplicationException(Resources.Error_AccessDeniedException);
            //}
            //SelectData();
        }
        public void LoadData()
        {
            LoadData(View.QuestionId);
        }
        protected override void OnSubmit()
        {
            //Question item;
            //if (View.QuestionId.HasValue)
            //{
            //    item = QuestionDao.FindById(View.QuestionId.Value);
            //    if (item == null)
            //        throw new ApplicationException(string.Format(CultureInfo.CurrentCulture, Resources.Error_QuestionLoad, View.QuestionId.Value));

            //    if (AuthenticationService.CurrentUser.CanAnswer)
            //    {
            //        if (!item.AnswerDate.HasValue)
            //        {
            //            item.AnswerText = View.AnswerText;
            //            item.AnswerUser = AuthenticationService.CurrentUser;
            //            item.AnswerDate = DateTime.Now;
            //        }
            //    }
            //    //if (item.Version.CompareTo(((IViewWithVersion)View).ItemVersion) != 0)
            //    //{
            //    //    ((IViewWithVersion)View).ReloadPageAction = new EditInstitutionAction(View.Id);
            //    //    return;
            //    //}
            //}
            //else
            //    item = new Question(AuthenticationService.CurrentUser,View.QuestionText);
            ////try
            ////{
            //QuestionDao.Save(item);
            //Ioc.Resolve<IWebSessionManager>().CurrentSession.Flush();
            ////}
            ////catch (StaleObjectStateException ex)
            ////{
            ////    log.Warn("StaleObjectStateException while edit Institution: ", ex);
            ////    ((IViewWithVersion)View).ReloadPageAction = new EditInstitutionAction(View.Id);
            ////    Ioc.Resolve<IWebSessionManager>().CurrentSession.Clear();
            ////    return;
            ////}
            ////if (!isError)
            ////{
            ////    View.ResultOperationSave.Message = Resources.Message_SavedSuccessfully;
            ////    View.ResultOperationSave.IsSuccessfully = true;
            ////}
            //LoadData(item.Id);
        }


        #endregion
        #region Helpers
        public void LoadData(int? id)
        {
            //if (id.HasValue)
            //{
            //    int Id = id.Value;
            //    Question item = QuestionDao.FindById(Id);
            //    if (item == null)
            //        throw new ApplicationException(string.Format(CultureInfo.CurrentCulture, Resources.Error_QuestionLoad, Id));
            //    View.QuestionId = item.Id;
            //    View.QuestionText = item.Text;
            //    View.QuestionReadOnly = true;
            //    View.AnswerText = string.IsNullOrEmpty(item.AnswerText) ? string.Empty : item.AnswerText;
            //    if (AuthenticationService.CurrentUser.CanAnswer)
            //    {
            //        View.AnswerVisible = true;
            //        View.AnswerReadOnly = item.AnswerDate.HasValue;
            //        View.SaveEnabled = !item.AnswerDate.HasValue;
            //    }
            //    else
            //    {
            //        View.AnswerVisible = item.AnswerDate.HasValue;
            //        View.AnswerReadOnly = true;
            //        View.SaveEnabled = false;
            //    }
            //}
            //else
            //{
            //    View.QuestionText = string.Empty;
            //    View.QuestionReadOnly = false;
            //    View.AnswerVisible = false;
            //    View.SaveEnabled = true;
            //}
        }
        #endregion

    }
}

using System;
using System.Web.Mvc;
using Reports.Core;
using Reports.Core.Enum;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.Bl.Impl;
using Reports.Presenters.UI.ViewModel;
using WebMvc.Attributes;

namespace WebMvc.Controllers
{
    public class DeductionController : BaseController
    {
        protected IRequestBl requestBl;
        public IRequestBl RequestBl
        {
            get
            {
                requestBl = Ioc.Resolve<IRequestBl>();
                return Validate.Dependency(requestBl);
            }
        }

        [ReportAuthorize(UserRole.Accountant | UserRole.OutsourcingManager)]
        public ActionResult Index()
        {
            DeductionListModel model = RequestBl.GetDeductionListModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(DeductionListModel model)
        {
            RequestBl.SetDeductionListModel(model, !ValidateModel(model));
            return View(model);
        }
        protected bool ValidateModel(DeductionListModel model)
        {
            if (model.BeginDate.HasValue && model.EndDate.HasValue &&
                model.BeginDate.Value > model.EndDate.Value)
                ModelState.AddModelError("BeginDate", "Дата в поле <Период с> не может быть больше даты в поле <по>.");
            return ModelState.IsValid;
        }

        [HttpGet]
        public ActionResult DeductionEdit(int id)
        {
            DeductionEditModel model = RequestBl.GetDeductionEditModel(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult DeductionEdit(DeductionEditModel model)
        {
            CorrectCheckboxes(model);
            CorrectDropdowns(model);
            //UploadFileDto fileDto = GetFileContext();
            if (!ValidateDeductionEditModel(model))
            {
                //model.IsApproved = false;
                //model.IsApprovedForAll = false;
                RequestBl.ReloadDictionariesToModel(model);
                return View(model);
            }

            string error;
            if (!RequestBl.SaveDeductionEditModel(model,out error))
            {

                if (model.ReloadPage)
                {
                    ModelState.Clear();
                    if (!string.IsNullOrEmpty(error))
                        ModelState.AddModelError("", error);
                    return View(RequestBl.GetDeductionEditModel(model.Id));
                }
                if (!string.IsNullOrEmpty(error))
                    ModelState.AddModelError("", error);
            }
            return View(model);
        }
        protected void CorrectCheckboxes(DeductionEditModel model)
        {
            if (!model.IsEditable && model.IsFastDismissalHidden)
            {
                if (ModelState.ContainsKey("IsFastDismissal"))
                    ModelState.Remove("IsFastDismissal");
                model.IsFastDismissal = model.IsFastDismissalHidden;
            }
        }
        protected void CorrectDropdowns(DeductionEditModel model)
        {
            // todo ???
            //if (!model.IsEditable)
            //{
            //    model.TypeId = model.TypeIdHidden;
            //    model.KindId = model.KindIdHidden;
            //    model.MonthId = model.MonthIdHidden;
            //    model.UserId = model.UserIdHidden;
            //}
        }
        protected bool ValidateDeductionEditModel(DeductionEditModel model)
        {
            if(string.IsNullOrEmpty(model.Sum))
                ModelState.AddModelError("Sum", "'Сумма' - обязательное поле.");
            else
            {
                decimal sum;
                if(!Decimal.TryParse(model.Sum,out sum)) 
                    ModelState.AddModelError("Sum", "Поле 'Сумма' должно быть числом.");
                else if (sum <= 0)
                    ModelState.AddModelError("Sum", "Поле 'Сумма' должно быть положительным числом.");
            }
            if (model.TypeId != (int)DeductionTypeEnum.Deduction)
            {
                if(!model.DismissalDate.HasValue)
                    ModelState.AddModelError("DismissalDate", "'Дата увольнения' - обязательное поле.");
            }
            return ModelState.IsValid;
        }

        public ActionResult UserChange(int userId)
        {
            DeductionUserInfoModel model = new DeductionUserInfoModel();
            try
            {
                RequestBl.SetDeductionUserInfoModel(model, userId);
            }
            catch (Exception ex)
            {
                Log.Error("Exception on UserChange",ex);
                model.UserInfoError = string.Format("Ошибка при загрузке данных сотрудника:{0}",ex.GetBaseException().Message);
            }
            
            return PartialView("DeductionUserInfo",model);
        }
    }
}
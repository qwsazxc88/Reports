using System;
using System.Web.Mvc;
using Reports.Core;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;
using WebMvc.Attributes;

namespace WebMvc.Controllers
{
    [PreventSpam]
    [ReportAuthorize(UserRole.Director | UserRole.OutsourcingManager | UserRole.Manager | UserRole.PersonnelManager | UserRole.StaffManager)]
    public class AppointmentController : BaseController
    {
        public const string StrInvalidReasonFromDate = "Неверная дата для основания появления вакансии";
        public const string StrInvalidDesirableBeginDate = "Неверная желательная дата выхода";
        public const string StrDesirableBeginDateIsSmall = "Желательная дата выхода должна быть не ранее 2 недель с момента создания заявки";
        public const string StrInvalidDepartment = "Указано неверное структурное подразделение.У вас нет права создания заявки для него.";
        public const string StrInvalidDepartmentLevel = "Выбор структурного подразделения уровня {0} обязателен";
        public const string StrInvalidListDates = "Дата в поле <Период с> не может быть больше даты в поле <по>.";

        protected IAppointmentBl appointmentBl;
        public IAppointmentBl AppointmentBl
        {
            get
            {
                appointmentBl = Ioc.Resolve<IAppointmentBl>();
                return Validate.Dependency(appointmentBl);
            }
        }
        [HttpGet]
        public ActionResult Index()
        {
            var model = AppointmentBl.GetAppointmentListModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(AppointmentListModel model)
        {
            bool hasError = !ValidateModel(model);
            AppointmentBl.SetAppointmentListModel(model, hasError);
            return View(model);
        }
        protected bool ValidateModel(AppointmentListModel model)
        {
            if (model.BeginDate.HasValue && model.EndDate.HasValue &&
                model.BeginDate.Value > model.EndDate.Value)
                ModelState.AddModelError("BeginDate", StrInvalidListDates);
            return ModelState.IsValid;
        }
        [HttpGet]
        public ActionResult AppointmentEdit(int id)
        {
            AppointmentEditModel model = AppointmentBl.GetAppointmentEditModel(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult AppointmentEdit(AppointmentEditModel model)
        {

            CorrectCheckboxes(model);
            CorrectDropdowns(model);
            if (!ValidateAppointmentEditModel(model))
            {
                model.IsDelete = false;
                AppointmentBl.ReloadDictionaries(model);
                return View(model);
            }
            string error;
            if (!AppointmentBl.SaveAppointmentEditModel(model, out error))
            {
                model.IsDelete = false;
                if (model.ReloadPage)
                {
                    ModelState.Clear();
                    if (!string.IsNullOrEmpty(error))
                        ModelState.AddModelError("", error);
                    return View(AppointmentBl.GetAppointmentEditModel(model.Id));
                }
                if (!string.IsNullOrEmpty(error))
                    ModelState.AddModelError("", error);
            }
            model.IsDelete = false;
            return View(model);
            if (!string.IsNullOrEmpty(error))
                return View(model);
            return RedirectToAction("Index");
        }
        protected bool ValidateAppointmentEditModel(AppointmentEditModel model)
        {
            //return false;
            //if (RequestBl.CheckOtherOrdersExists(model))
            //    ModelState.AddModelError("BeginMissionDate", StrOtherOrdersExists);
            if (model.IsDelete)
                return true;
            if(model.ReasonId != 3 && !string.IsNullOrEmpty(model.ReasonBeginDate))
            {
                DateTime beginDate;
                if (!DateTime.TryParse(model.ReasonBeginDate, out beginDate))
                {
                    ModelState.AddModelError("ReasonBeginDate", StrInvalidReasonFromDate);
                    /*switch (model.ReasonId)
                    {
                        case 1:
                        case 2:
                            ModelState.AddModelError("ReasonBeginDate", StrInvalidReasonFromDate);
                            break;
                        case 4:
                            ModelState.AddModelError("ReasonBeginDate", StrInvalidReasonFromDate);
                            break;
                        case 5:
                            ModelState.AddModelError("ReasonBeginDate", StrInvalidReasonFromDate);
                            break;
                    }*/
                }
            }
            if (!string.IsNullOrEmpty(model.ReasonBeginDate))
            {
                DateTime beginDate;
                if (!DateTime.TryParse(model.DesirableBeginDate, out beginDate))
                    ModelState.AddModelError("DesirableBeginDate", StrInvalidDesirableBeginDate);
                else
                {
                    DateTime createDate = DateTime.Today;
                    if (!string.IsNullOrEmpty(model.DateCreatedHidden))
                        DateTime.TryParse(model.DateCreatedHidden, out createDate);
                    if(beginDate.Subtract(createDate).Days < 14)
                        ModelState.AddModelError("DesirableBeginDate", StrDesirableBeginDateIsSmall);
                }
            }
            //todo need to check department
            int depLevel;
            if (!AppointmentBl.CheckDepartment(model.DepartmentId, out depLevel))
            {
                if(depLevel != AppointmentBl.GetRequeredDepartmentLevel())
                    ModelState.AddModelError("SelectDepartmentBtn", string.Format(StrInvalidDepartmentLevel, 
                        AppointmentBl.GetRequeredDepartmentLevel()));
                else
                    ModelState.AddModelError("SelectDepartmentBtn", StrInvalidDepartment);
            }
            return ModelState.IsValid;
        }
        protected void CorrectDropdowns(AppointmentEditModel model)
        {
            if (!model.IsEditable)
            {
                model.PositionId = model.PositionIdHidden;
                model.ReasonId = model.ReasonIdHidden;
                model.TypeId = model.TypeIdHidden;
                model.IsVacationExists = model.IsVacationExistsHidden;
            }
        }
        protected void CorrectCheckboxes(AppointmentEditModel model)
        {
            if (!model.IsManagerApproveAvailable)
            {
                if (ModelState.ContainsKey("IsManagerApproved"))
                    ModelState.Remove("IsManagerApproved");
                model.IsManagerApproved = model.IsManagerApprovedHidden;
            }
            if (!model.IsChiefApproveAvailable)
            {
                if (ModelState.ContainsKey("IsChiefApproved"))
                    ModelState.Remove("IsChiefApproved");
                model.IsChiefApproved = model.IsChiefApprovedHidden;
            }
            if (!model.IsPersonnelApproveAvailable)
            {
                if (ModelState.ContainsKey("IsPersonnelApproved"))
                    ModelState.Remove("IsPersonnelApproved");
                model.IsPersonnelApproved = model.IsPersonnelApprovedHidden;
            }
            if (!model.IsStaffApproveAvailable)
            {
                if (ModelState.ContainsKey("IsStaffApproved"))
                    ModelState.Remove("IsStaffApproved");
                model.IsStaffApproved = model.IsStaffApprovedHidden;
            }
           
            /*if (model.IsManagerApproveAvailable && model.IsManagerApproved.HasValue
                && !model.IsManagerApproved.Value)
            {
                if (ModelState.ContainsKey("IsManagerApproved"))
                    ModelState.Remove("IsManagerApproved");
            }
            if (model.IsChiefApproveAvailable && model.IsChiefApproved.HasValue
                && !model.IsChiefApproved.Value)
            {
                if (ModelState.ContainsKey("IsChiefApproved"))
                    ModelState.Remove("IsChiefApproved");
                if (ModelState.ContainsKey("IsManagerApproved"))
                    ModelState.Remove("IsManagerApproved");
            }*/
        }
    }
}
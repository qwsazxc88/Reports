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
            if (!ValidateMissionOrderEditModel(model))
            {
                AppointmentBl.ReloadDictionaries(model);
                return View(model);
            }
            string error;
            if (!AppointmentBl.SaveAppointmentEditModel(model, out error))
            {
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
            if (!string.IsNullOrEmpty(error))
                return View(model);
            return RedirectToAction("Index");
        }
        protected bool ValidateMissionOrderEditModel(AppointmentEditModel model)
        {
            //return false;
            //if (RequestBl.CheckOtherOrdersExists(model))
            //    ModelState.AddModelError("BeginMissionDate", StrOtherOrdersExists);
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
            if (!model.IsManagerApproveAvailable && !model.IsManagerApproved.HasValue)
            {
                if (ModelState.ContainsKey("IsManagerApproved"))
                    ModelState.Remove("IsManagerApproved");
                model.IsManagerApproved = model.IsManagerApprovedHidden;
            }
            if (!model.IsChiefApproveAvailable && !model.IsChiefApproved.HasValue)
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
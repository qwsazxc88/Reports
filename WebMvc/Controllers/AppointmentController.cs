using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Reports.Core;
using Reports.Core.Dto;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;
using WebMvc.Attributes;

namespace WebMvc.Controllers
{
    [PreventSpam]
    [ReportAuthorize(UserRole.OutsourcingManager | UserRole.Manager | UserRole.PersonnelManager | UserRole.StaffManager)]
    public class AppointmentController : BaseController
    {
        public const int MaxFileSize = 2 * 1024 * 1024;

        public const string StrInvalidReasonFromDate = "Неверная дата для основания появления вакансии";
        public const string StrInvalidDesirableBeginDate = "Неверная желательная дата выхода";
        public const string StrDesirableBeginDateIsSmall = "Желательная дата выхода должна быть не ранее 2 недель с момента создания заявки";
        public const string StrInvalidDepartment = "Указано неверное структурное подразделение.У вас нет права создания заявки для него.";
        public const string StrInvalidDepartmentLevel = "Выбор структурного подразделения уровня {0} обязателен";
        public const string StrInvalidListDates = "Дата в поле <Период с> не может быть больше даты в поле <по>.";
        public const string StrFileSizeError = "Размер прикрепленного файла не может превышать {0} Мб.";

        public const string StrInvalidColloquyDate = "Неверная дата собеседования";
        public const string StrInvalidDateAccept = "Неверная дата приема на работу";

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
            else
            {
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
            if(model.IsDelete)
            {
                if (ModelState.ContainsKey("IsStaffReceiveRejectMail"))
                    ModelState.Remove("IsStaffReceiveRejectMail");
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

        [HttpGet]
        [ReportAuthorize(UserRole.OutsourcingManager | UserRole.Manager | UserRole.StaffManager)]
        public ActionResult AppointmentReportEdit(int id)
        {
            AppointmentReportEditModel model = AppointmentBl.GetAppointmentReportEditModel(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult AppointmentReportEdit(AppointmentReportEditModel model)
        {
            CorrectCheckboxes(model);
            CorrectDropdowns(model);
            UploadFileDto fileDto = GetFileContext(Request,ModelState);
            if (!ValidateAppointmentReportEditModel(model, fileDto))
            {
                model.IsDelete = false;
                AppointmentBl.ReloadDictionariesToModel(model);
                return View(model);
            }

            string error;
            if (!AppointmentBl.SaveAppointmentReportEditModel(model, fileDto, out error))
            {
                if (model.ReloadPage)
                {
                    ModelState.Clear();
                    if (!string.IsNullOrEmpty(error))
                        ModelState.AddModelError("", error);
                    return View(AppointmentBl.GetAppointmentReportEditModel(model.Id));
                }
                if (!string.IsNullOrEmpty(error))
                    ModelState.AddModelError("", error);
            }
            return View(model);
        }
        protected bool ValidateAppointmentReportEditModel(AppointmentReportEditModel model,UploadFileDto fileDto)
        {
            if (model.IsDelete)
                return true;
            if (!string.IsNullOrEmpty(model.ColloquyDate))
            {
                DateTime colloquyDate;
                if (!DateTime.TryParse(model.ColloquyDate, out colloquyDate))
                    ModelState.AddModelError("ColloquyDate", StrInvalidColloquyDate);
            }
            if (!string.IsNullOrEmpty(model.DateAccept))
            {
                DateTime dateAccept;
                if (!DateTime.TryParse(model.DateAccept, out dateAccept))
                    ModelState.AddModelError("DateAccept", StrInvalidDateAccept);
            }
            return ModelState.IsValid;
        }
        protected void CorrectCheckboxes(AppointmentReportEditModel model)
        {
            if (!model.IsStaffApproveAvailable)
            {
                if (ModelState.ContainsKey("IsStaffApproved"))
                    ModelState.Remove("IsStaffApproved");
                model.IsStaffApproved = model.IsStaffApprovedHidden;
            }
            if (!model.IsManagerApproveAvailable)
            {
                if (ModelState.ContainsKey("IsManagerApproved"))
                    ModelState.Remove("IsManagerApproved");
                model.IsManagerApproved = model.IsManagerApprovedHidden;
            }
        }
        protected void CorrectDropdowns(AppointmentReportEditModel model)
        {
            if (!model.IsEditable)
            {
                model.TypeId = model.TypeIdHidden;
                model.IsEducationExists = model.IsEducationExistsHidden;
            }
        }

        public static UploadFileDto GetFileContext(HttpRequestBase request, ModelStateDictionary modelState)
        {
            if (request.Files.Count == 0)
                return null;
            string file = request.Files.GetKey(0);
            return GetFileContext(request,modelState,file);
        }
        protected static UploadFileDto GetFileContext(HttpRequestBase request, ModelStateDictionary modelState, string file)
        {
            HttpPostedFileBase hpf = request.Files[file];
            if ((hpf == null) || (hpf.ContentLength == 0))
                return null;
            if (hpf.ContentLength > MaxFileSize)
            {
                modelState.AddModelError("", string.Format(StrFileSizeError, MaxFileSize / (1024 * 1024)));
                return null;
            }
            byte[] context = GetFileData(hpf);
            return new UploadFileDto
            {
                Context = context,
                ContextType = hpf.ContentType,
                FileName = Path.GetFileName(hpf.FileName),
            };
        }
        protected static byte[] GetFileData(HttpPostedFileBase file)
        {
            var length = file.ContentLength;
            var fileContent = new byte[length];
            file.InputStream.Read(fileContent, 0, length);
            return fileContent;
        }

        public FileContentResult ViewAttachment(int id)
        {
            try
            {
                AttachmentModel model = AppointmentBl.GetFileContext(id);
                return File(model.Context, model.ContextType, model.FileName);
            }
            catch (Exception ex)
            {
                Log.Error("Error on ViewAttachment:", ex);
                throw;
            }
        }
    }
}
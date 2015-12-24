using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using Reports.Core;
using Reports.Core.Dto;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;
using WebMvc.Attributes;
using System.Linq;
using WebMvc.Helpers;
namespace WebMvc.Controllers
{
    [PreventSpam]
    [ReportAuthorize(UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.StaffManager | UserRole.PersonnelManager | UserRole.Security | UserRole.Trainer)]
    public class AppointmentController : BaseController
    {
        //public const int MaxFileSize = 2 * 1024 * 1024;

        public const string StrInvalidReasonFromDate = "Неверная дата для основания появления вакансии";
        public const string StrInvalidDesirableBeginDate = "Неверная желательная дата выхода";
        public const string StrDesirableBeginDateIsSmall = "Желательная дата выхода должна быть не ранее 2 недель с момента создания заявки";
        public const string StrInvalidDepartment = "Указано неверное структурное подразделение.У вас нет права создания заявки для него.";
        public const string StrInvalidDepartmentLevel = "Выбор структурного подразделения уровня {0} обязателен";
        public const string StrInvalidListDates = "Дата в поле <Период с> не может быть больше даты в поле <по>.";
        //public const string StrFileSizeError = "Размер прикрепленного файла не может превышать {0} Мб.";

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
        #region Appointment
        public ActionResult MoveToEmployment(int id)
        {
            var model = AppointmentBl.FillCreateCandidateModelByReportId(id);
            return View("~/Views/Employment/CreateCandidate.cshtml", model);
        }
        [HttpGet]
        [ReportAuthorize(UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Manager | UserRole.StaffManager | UserRole.ConsultantPersonnel | UserRole.PersonnelManager | UserRole.Security | UserRole.Trainer)]
        public ActionResult Index()
        {
            var model = AppointmentBl.GetAppointmentListModel();
            return View(model);
        }
        [HttpGet]
        [ReportAuthorize(UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Manager | UserRole.StaffManager | UserRole.ConsultantPersonnel | UserRole.PersonnelManager | UserRole.Security | UserRole.Trainer)]
        public ActionResult AppointmentReportList()
        {
            var model = AppointmentBl.GetAppointmentReportListModel();
            return View(model);
        }

        [HttpPost]
        [ReportAuthorize(UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Manager | UserRole.StaffManager | UserRole.ConsultantPersonnel | UserRole.PersonnelManager | UserRole.Security)]
        public ActionResult Index(AppointmentListModel model)
        {
            if(model.ForPrint>0)
                return RedirectToAction("GetPrintForm", "Graphics", new { param = model.ToParamsString(), controll = "Appointment", actionName = "Print", isLandscape = false });
            bool hasError = !ValidateModel(model);
            AppointmentBl.SetAppointmentListModel(model, hasError);
            return View("Index",model);
        }
        [HttpGet]
        public ActionResult Print(AppointmentListModel model)
        {
            if (model.ForPrint == 1)
            { model.ForPrint = 0; return Index(model); }
            else
            { model.ForPrint = 0; return AppointmentReportList(model); }
        }
        [HttpPost]
        [ReportAuthorize(UserRole.ConsultantPersonnel)]
        public ActionResult SendEmailFor(int AppointmentId)
        {
            int result=AppointmentBl.SendEmailForAppointmentManager(AppointmentId);
            switch(result)
            {
                case 0: return Json(new {status="Сообщение отправлено."});
                case 1: return Json(new {status="Не найден email руководителя."});
                default: return Json(new {status="При отправке сообщения произошла ошибка."}); 
            }
        }
        [HttpGet]
        [ReportAuthorize(UserRole.OutsourcingManager | UserRole.Estimator)]
        public ActionResult CreateReportForOldAppointment(int id)
        {
            int rid= AppointmentBl.CreateReportForOldAppointment(id);
            return RedirectToAction("AppointmentReportEdit", new { id = rid });
        }
        [HttpPost]
        [ReportAuthorize(UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Manager | UserRole.StaffManager | UserRole.ConsultantPersonnel | UserRole.PersonnelManager | UserRole.Security | UserRole.Trainer)]
        public ActionResult AppointmentReportList(AppointmentListModel model)
        {
            if (model.ForPrint > 0)
                return RedirectToAction("GetPrintForm", "Graphics", new { param = model.ToParamsString(), controll = "Appointment", actionName = "Print", isLandscape = false });
            
            bool hasError = !ValidateModel(model);
            AppointmentBl.SetAppointmentReportsListModel(model, hasError);
            return View("AppointmentReportList",model);
        }
        protected bool ValidateModel(AppointmentListModel model)
        {
            if (model.BeginDate.HasValue && model.EndDate.HasValue &&
                model.BeginDate.Value > model.EndDate.Value)
                ModelState.AddModelError("BeginDate", StrInvalidListDates);
            return ModelState.IsValid;
        }
        [HttpGet]
        [ReportAuthorize(UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Manager | UserRole.StaffManager | UserRole.ConsultantPersonnel | UserRole.PersonnelManager)]
        public ActionResult AppointmentWithoutStaffEdit(int id, int? managerId)
        {
            AppointmentEditModel model = AppointmentBl.GetAppointmentEditModel(id, managerId);
            model.Recruter = 2;
            model.ShowStaff = false;
            return View("AppointmentEdit",model);
        }
        [HttpGet]
        [ReportAuthorize(UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Manager | UserRole.StaffManager | UserRole.ConsultantPersonnel | UserRole.PersonnelManager | UserRole.Security)]
        public ActionResult AppointmentEdit(int id,int? managerId)
        {
            AppointmentEditModel model = AppointmentBl.GetAppointmentEditModel(id, managerId);
            if(id==0) model.Recruter = 1;
            //if(model.ShowStaff)model.Reasons = model.Reasons.Where(x => x.Id != 6).ToList();
            return View(model);
        }
        public JsonResult CopyAppointmentReport(int AppointmentNumber,int AppointmentReportId)
        {
            var res = AppointmentBl.CopyAppointmentReport(AppointmentNumber, AppointmentReportId);
            return Json(res);
        }
        public JsonResult CheckUserDismissal(int userId)
        {
            var res=AppointmentBl.CheckUserDismissal(userId);
            return Json(res ? (object)new { result = true } : (object)new { result = false });
        }
        [HttpPost]
        [ReportAuthorize(UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Manager | UserRole.ConsultantPersonnel | UserRole.StaffManager)]
        public ActionResult AppointmentEdit(AppointmentEditModel model)
        {

            CorrectCheckboxes(model);
            CorrectDropdowns(model);
            if (!ValidateAppointmentEditModel(model))
            {
                model.IsDelete = false;
                model.ApproveForAll = false;
                AppointmentBl.ReloadDictionaries(model);
                return View(model);
            }
            string error;
            if (!AppointmentBl.SaveAppointmentEditModel(model, out error))
            {
                model.IsDelete = false;
                model.ApproveForAll = false;
                if (model.ReloadPage)
                {
                    ModelState.Clear();
                    if (!string.IsNullOrEmpty(error))
                        ModelState.AddModelError("", error);
                    var mdl = AppointmentBl.GetAppointmentEditModel(model.Id,
                                model.StaffCreatorId == 0 ? new int?() : model.UserId);
                    return View(mdl);
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
            model.ApproveForAll = false;
            if (string.IsNullOrEmpty(error)) ViewBag.Message = "Данные успешно сохранены";
            return View(model);
            /*if (!string.IsNullOrEmpty(error))
                return View(model);
            return RedirectToAction("Index");*/
        }
        
        public ActionResult Note(int id)
        {
            var model = AppointmentBl.GetNoteModel(id);
            return View(model);
        }
        public FileResult NoteDocX(int id)
        {
             var model = AppointmentBl.GetNoteModel(id);
            
            var data=NoteDocumentCreator.NoteCreator.CreateNote(Server.MapPath("~/Files"),model.To,model.From,model.Theme,model.DateFrom,model.Date,model.Reason,model.Departments.Aggregate("",(sum,next)=>sum+=next+"; "),model.Position,model.PositionsCount,model.Salary,model.Premium);
            return File(data,"application/vnd.openxmlformats-officedocument.wordprocessingml.document",id+".docx");
        }
        protected bool ValidateAppointmentEditModel(AppointmentEditModel model)
        {
            //return false;
            //if (RequestBl.CheckOtherOrdersExists(model))
            //    ModelState.AddModelError("BeginMissionDate", StrOtherOrdersExists);
            if (model.IsDelete || model.NonActual)
                return true;
            if (model.Recruter == 2 && String.IsNullOrWhiteSpace(model.FIO))
            {
                ModelState.AddModelError("FIO", "ФИО Кандидата должно быть заполнено!");
            }
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
            if (!string.IsNullOrEmpty(model.ReasonBeginDate) && model.ShowStaff)
            {
                DateTime beginDate;
                if (!DateTime.TryParse(model.DesirableBeginDate, out beginDate))
                    ModelState.AddModelError("DesirableBeginDate", StrInvalidDesirableBeginDate);
                else
                {
                    DateTime createDate = DateTime.Today;
                    if (!string.IsNullOrEmpty(model.DateCreated))
                        createDate = DateTime.Parse(model.DateCreated);
                    if (beginDate.Subtract(createDate).Days < 14)
                        ModelState.AddModelError("DesirableBeginDate", StrDesirableBeginDateIsSmall);
                }
            }
            //todo need to check department
            int depLevel;
            if (!AppointmentBl.CheckDepartment(model, out depLevel))
            {
                if (depLevel != AppointmentBl.GetRequeredDepartmentLevel())
                    ModelState.AddModelError("SelectDepartmentBtn", string.Format(StrInvalidDepartmentLevel,
                        AppointmentBl.GetRequeredDepartmentLevel()));
                else
                {
                    ModelState.AddModelError("SelectDepartmentBtn", StrInvalidDepartment);
                }
            }
            return ModelState.IsValid;
        }
        protected void CorrectDropdowns(AppointmentEditModel model)
        {
            
            if (!model.IsEditable)
            {
                //model.PositionId = model.PositionIdHidden;
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
            /*if (!model.IsPersonnelApproveAvailable)
            {
                if (ModelState.ContainsKey("IsPersonnelApproved"))
                    ModelState.Remove("IsPersonnelApproved");
                model.IsPersonnelApproved = model.IsPersonnelApprovedHidden;
            }*/
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
            if (model.ApproveForAll)
            {
                if (ModelState.ContainsKey("IsManagerApproved"))
                    ModelState.Remove("IsManagerApproved");
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
        //[ReportAuthorize(UserRole.Manager)]
        public ActionResult SelectManagerDialog()
        {
            try
            {
                //MissionReportEditCostModel model = new MissionReportEditCostModel { CostId = id };
                //if (!string.IsNullOrEmpty(json))
                //{
                //    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                //    CostDto dto = jsonSerializer.Deserialize<CostDto>(json);
                //    model.AccountantSum = dto.AccountantSum;
                //    model.CostTypeId = dto.CostTypeId;
                //    //model.Count = dto.Count;
                //    model.GradeSum = dto.GradeSum;
                //    model.PurchaseBookSum = dto.PurchaseBookSum;
                //    model.UserSum = dto.UserSum;


                //}
                AppointmentSelectManagerModel model = AppointmentBl.GetSelectManagerModel();
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                string error = "Ошибка при загрузке данных: " + ex.GetBaseException().Message;
                return PartialView("SelectManagerDialogError", new DialogErrorModel { Error = error });
            }
        }
        #endregion
        [HttpGet]
        public ActionResult Instruction()
        { return View(); }
         
        #region AppointmentReport
        [HttpGet]
        [ReportAuthorize(UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Manager | UserRole.StaffManager | UserRole.ConsultantPersonnel | UserRole.PersonnelManager | UserRole.Security | UserRole.Trainer)]
        public ActionResult AppointmentReportEdit(int id)
        {
            AppointmentReportEditModel model = AppointmentBl.GetAppointmentReportEditModel(id);
            return View(model);
        }
        [HttpGet]
        [ReportAuthorize(UserRole.StaffManager)]
        public ActionResult CreateReport(int id)
        {
            int newReportId = AppointmentBl.CreateNewReport(id);
            return RedirectToAction("AppointmentReportEdit", new RouteValueDictionary { { "id", newReportId } });
        }
        [HttpPost]
        [ReportAuthorize(UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Manager | UserRole.StaffManager | UserRole.Trainer)]
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
            else
            {
                model = AppointmentBl.GetAppointmentReportEditModel(model.Id);                
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
            }
            if (!model.IsManagerEditable)
            {
                if(!model.IsStaffSetDateAcceptAvailable && !model.IsTrainerCanSave)
                    model.IsEducationExists = model.IsEducationExistsHidden;
                model.IsColloquyPassed = model.IsColloquyPassedHidden;
            }
            
        }

        #region FileContext
        /*public static UploadFileDto GetFileContext(HttpRequestBase request, ModelStateDictionary modelState)
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
        }*/
        #endregion
        #region Attachment
        [ReportAuthorize(UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Manager | UserRole.StaffManager)]
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
        [HttpGet]
        [ReportAuthorize(UserRole.StaffManager)]
        public ContentResult DeleteAttachment(int id)
        {
            bool saveResult;
            string error;
            try
            {
                DeleteAttacmentModel model = new DeleteAttacmentModel { Id = id };
                saveResult = AppointmentBl.DeleteAttachment(model);
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
        #endregion
        #region PrintLogin
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.StaffManager)]
        public ActionResult PrintLoginForm(int id)
        {
            PrintLoginFormModel model = AppointmentBl.GetPrintLoginFormModel(id);
            return View(model);
        }
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.StaffManager)]
        public ActionResult GetLoginPrintForm(int id)
        {
            string args = string.Format(@"id={0}",id);
            return GetPrintForm(args, "PrintLoginForm");
        }
        [HttpGet]
        public ActionResult GetPrintForm(string arguments, string actionName)
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

                var argumrnts = new StringBuilder();

                var cookieName = FormsAuthentication.FormsCookieName;
                var authCookie = Request.Cookies[cookieName];
                if (authCookie == null || authCookie.Value == null)
                    throw new ArgumentException("Ошибка авторизации.");
                argumrnts.AppendFormat("{0} --cookie {1} {2}",
                    GetConverterCommandParam(arguments, actionName)
                    , cookieName, authCookie.Value);
                argumrnts.AppendFormat(" \"{0}\"", filePath);
                var serverSideProcess = new Process
                {
                    StartInfo =
                    {
                        FileName = ConfigurationManager.AppSettings["PdfConverterCommandLineTemplate"],
                        Arguments = argumrnts.ToString(),
                        UseShellExecute = true,
                        //UseShellExecute = false,
                        //RedirectStandardOutput = true,
                        //RedirectStandardError = true,    
                    },
                    EnableRaisingEvents = true,
                };
                serverSideProcess.Start();
                //string stdoutx = serverSideProcess.StandardOutput.ReadToEnd();
                //string stderrx = serverSideProcess.StandardError.ReadToEnd();       
                serverSideProcess.WaitForExit();
                return MissionOrderController.GetFile(Response, Request, Server, filePath, fileName, @"application/pdf", "TempLoginPassword.pdf");
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
        protected virtual string GetConverterCommandParam(string args, string actionName)
        {
            var localhostUrl = ConfigurationManager.AppSettings["localhost"];
            string urlTemplate = string.Format("Appointment/{0}", actionName);
            //string args = @"/" + id;
            return !string.IsNullOrEmpty(localhostUrl)
                       ? string.Format(@"{0}/{1}?{2}", localhostUrl, urlTemplate, args)
                       : Url.Content(string.Format(@"{0}?{1}", urlTemplate, args));
        }
        #endregion
        #endregion
    }
}
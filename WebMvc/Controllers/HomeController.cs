using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;
using System.Web.Script.Serialization;
using Reports.Core;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Presenters.Services;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;
using WebMvc.Attributes;
using WebMvc.Models;
using System.Web;
namespace WebMvc.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {


        public const string StrInvalidCommentType = "Неверный тип комментария {0}";
        public const string StrCommentIsRequired = "Комментарий - обязательное поле";
        public const string StrCommentLengthError = "Длина поля 'Комментарий' не может превышать {0} символов.";
        public const string StrCommentsLoadError = "Ошибка при загрузке данных:";

        protected IRequestBl requestBl;

        public IRequestBl RequestBl
        {
            get
            {
                requestBl = Ioc.Resolve<IRequestBl>();
                return Validate.Dependency(requestBl);
            }
        }

        protected IAppointmentBl appointmentBl;
        public IAppointmentBl AppointmentBl
        {
            get
            {
                appointmentBl = Ioc.Resolve<IAppointmentBl>();
                return Validate.Dependency(appointmentBl);
            }
        }
        protected IHelpBl helpBl;
        public IHelpBl HelpBl
        {
            get
            {
                helpBl = Ioc.Resolve<IHelpBl>();
                return Validate.Dependency(helpBl);
            }
        }

        public ActionResult Index(int? menuId)
        {
            //ViewBag.Message = "Welcome to ASP.NET MVC!";
            var service = Ioc.Resolve<IAuthenticationService>();
            IUser user = service.CurrentUser;
            return View(new HomeModel {menuId = menuId.HasValue ? menuId.Value : 0});
        }
        [HttpGet]
        public ActionResult BugReport()
        {
            ModelState.Clear();
            BugReportModel model = new BugReportModel();
            model.BrowserVersion = Request.Browser.Version;
            model.Browser = Request.Browser.Type;
            return View(model);
        }
        [ReportAuthorize(UserRole.Admin)]
        public ActionResult BugReportEdit(int Id)
        {
            var model = RequestBl.GetBugEditModel(Id,Path.Combine(Server.MapPath("~/Files"),".."));
            return View(model);
        }
        [ReportAuthorize(UserRole.Admin)]
        public ActionResult BugReportList()
        {
            BugReportListModel model = RequestBl.GetBugListModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult BugReport(BugReportModel model)
        {
            ModelState.Clear();
            if (model.Summary == null || string.IsNullOrEmpty(model.Summary.Trim()))
            {
                ModelState.AddModelError("Summary", "Необходимо заполнить краткое описание.");
            }
            if (model.Description == null || string.IsNullOrEmpty(model.Description.Trim()))
            {
                ModelState.AddModelError("Description", "Необходимо заполнить подробное описание.");
            }
            if (ModelState.IsValid)
            {
                HttpFileCollectionBase files = Request.Files;
                
                var guid = Guid.NewGuid();
                if (files != null && files.Count > 0)
                    for (int i = 0; i < files.Count;i++ )
                    {                        
                        var file = files[i];
                        if (file.ContentLength > 0)
                        {
                            string filename = Path.GetFileName(file.FileName);
                            string path = Path.Combine(Server.MapPath("~/Files"), "..\\Content\\BugReport", guid.ToString());
                            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                            path = Path.Combine(path, filename);
                            file.SaveAs(path);
                        }
                    }

                RequestBl.SendBugReport(model, guid.ToString());
                model = new BugReportModel();
                model.BrowserVersion = Request.Browser.Version;
                model.Browser = Request.Browser.Type;
                ViewBag.Message = "Сообщение отправлено!";
            }
            return View(model);
        }
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DepartmentDialog(int id /*, int typeId*/)
        {
            try
            {
                //DepartmentTreeModel model = new DepartmentTreeModel { DepartmentID = id };
                DepartmentTreeModel model = RequestBl.GetDepartmentTreeModel(id);
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                string error = "Ошибка при загрузке данных: " + ex.GetBaseException().Message;
                return PartialView("DialogError", new DialogErrorModel {Error = error});
            }
        }
       
        [HttpGet]
        public ContentResult GetChildren(int parentId, int level)
        {
            DepartmentChildrenDto model;
            try
            {
                model = RequestBl.GetChildren(parentId, level);
            }
            catch (Exception ex)
            {
                Log.Error("Exception on GetChildren:", ex);
                string error = ex.GetBaseException().Message;
                model = new DepartmentChildrenDto
                            {
                                Error = string.Format("Ошибка: {0}", error),
                                Children = new List<IdNameDto>()
                            };
            }
            var jsonSerializer = new JavaScriptSerializer();
            string jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.OutsourcingManager | UserRole.Estimator)]
        public ActionResult SetShortNameDialog()
        {
            try
            {
                //DepartmentTreeModel model = new DepartmentTreeModel { DepartmentID = id };
                TerraGraphicsSetShortNameModel model = RequestBl.SetShortNameModel();
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                string error = "Ошибка при загрузке данных: " + ex.GetBaseException().Message;
                return PartialView("TpDialogError", new DialogErrorModel { Error = error });
            }
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.OutsourcingManager | UserRole.Estimator)]
        public ContentResult GetTerraPointChildren(int parentId, int level)
        {
            TerraPointChildrenDto model;
            try
            {
                model = RequestBl.GetTerraPointChildren(parentId, level);
            }
            catch (Exception ex)
            {
                Log.Error("Exception on GetTerraPointChildren:", ex);
                string error = ex.GetBaseException().Message;
                model = new TerraPointChildrenDto
                {
                    Error = string.Format("Ошибка: {0}", error),
                    Children = new List<IdNameDto>()
                };
            }
            var jsonSerializer = new JavaScriptSerializer();
            string jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.OutsourcingManager | UserRole.Estimator)]
        public ContentResult GetTerraPointShortName(int pointId)
        {
            TerraPointShortNameDto model;
            try
            {
                model = RequestBl.GetTerraPointShortName(pointId);
            }
            catch (Exception ex)
            {
                Log.Error("Exception on GetTerraPointShortName:", ex);
                string error = ex.GetBaseException().Message;
                model = new TerraPointShortNameDto
                {
                    Error = string.Format("Ошибка: {0}", error),
                    ShortName = string.Empty,
                };
            }
            var jsonSerializer = new JavaScriptSerializer();
            string jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }
        [HttpGet]
        [ReportAuthorize(UserRole.Manager)]
        public ContentResult SaveTerraPointShortName(int pointId, string shortName)
        {
            TerraPointShortNameDto model;
            try
            {
                model = RequestBl.SaveTerraPointShortName(pointId,shortName);
            }
            catch (Exception ex)
            {
                Log.Error("Exception on SaveTerraPointShortName:", ex);
                string error = ex.GetBaseException().Message;
                model = new TerraPointShortNameDto
                {
                    Error = string.Format("Ошибка: {0}", error),
                    ShortName = string.Empty,
                };
            }
            var jsonSerializer = new JavaScriptSerializer();
            string jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }

        #region Comments
        [HttpGet]
        public ActionResult RenderComments(int id, int typeId)
        {
            //IContractRequest bo = Ioc.Resolve<IContractRequest>();
            CommentsModel model; 
            switch (typeId)
            {
                case (int)RequestTypeEnum.Appointment:
                case (int)RequestTypeEnum.AppointmentReport:
                    model = AppointmentBl.GetCommentsModel(id, (RequestTypeEnum)typeId);
                    break;
                case (int)RequestTypeEnum.HelpServiceRequest:
                //case (int)RequestTypeEnum.AppointmentReport:
                    model = HelpBl.GetCommentsModel(id, (RequestTypeEnum)typeId);
                    break;
                default:
                    throw new ArgumentException(string.Format(StrInvalidCommentType,typeId)); 
            }
            //CommentsModel model = RequestBl.GetCommentsModel(id, typeId);
            return PartialView("CommentPartial", model);
        }
        [HttpGet]
        public ActionResult AddCommentDialog(int id, int typeId)
        {
            try
            {
                AddCommentModel model = new AddCommentModel { DocumentId = id };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                string error = StrCommentsLoadError + ex.GetBaseException().Message;
                return PartialView("DialogError", new DialogErrorModel { Error = error });
            }
        }
        [HttpPost]
        public ContentResult SaveComment(int id, int typeId, string comment)
        {
            bool saveResult = false;
            string error;
            try
            {
                if (comment == null || string.IsNullOrEmpty(comment.Trim()))
                {
                    error = StrCommentIsRequired;
                }
                else if (comment.Trim().Length > MaxCommentLength)
                {
                    error = string.Format(StrCommentLengthError, MaxCommentLength);
                }
                else
                {
                    var model = new SaveCommentModel
                    {
                        DocumentId = id,
                        TypeId = typeId,
                        Comment = comment.Trim(),
                    };
                    //saveResult = RequestBl.SaveComment(model);
                    switch (typeId)
                    {
                        case (int)RequestTypeEnum.Appointment:
                            saveResult = AppointmentBl.SaveComment(model, RequestTypeEnum.Appointment);
                            break;
                        case (int)RequestTypeEnum.AppointmentReport:
                            saveResult = AppointmentBl.SaveComment(model, RequestTypeEnum.AppointmentReport);
                            break;
                        case (int)RequestTypeEnum.HelpServiceRequest:
                            saveResult = HelpBl.SaveComment(model, RequestTypeEnum.HelpServiceRequest);
                            break;
                        default:
                            throw new ArgumentException(string.Format(StrInvalidCommentType, typeId));
                    }
                    error = model.Error;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception on SaveComment:", ex);
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
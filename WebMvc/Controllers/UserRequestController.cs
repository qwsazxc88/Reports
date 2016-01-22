using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using Reports.Core;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;
using WebMvc.Attributes;
using System.Configuration;
using System.Text;
using System.Web.Security;
using System.Diagnostics;

namespace WebMvc.Controllers
{
    [PreventSpamAttribute]
    [ReportAuthorize(UserRole.Employee | UserRole.Manager | UserRole.PersonnelManager | UserRole.ConsultantPersonnel |
        UserRole.Inspector | UserRole.Chief | UserRole.OutsourcingManager | UserRole.Estimator |
        UserRole.Director | UserRole.Accountant | UserRole.Secretary | UserRole.Findep | UserRole.Archivist | UserRole.DismissedEmployee | UserRole.ConsultantOutsourcing)]
    public class UserRequestController : BaseController
    {
        
        //public const int MaxFileSize = 2 * 1024 * 1024;

        protected IRequestBl requestBl;
        public IRequestBl RequestBl
        {
            get
            {
                requestBl = Ioc.Resolve<IRequestBl>();
                return Validate.Dependency(requestBl);
            }
        }
        //
        // GET: /UserRequestController/
         #region CreateRequest
        [HttpGet]
        [ReportAuthorize(UserRole.Employee | UserRole.Manager | UserRole.PersonnelManager | 
         UserRole.Inspector | UserRole.Chief | UserRole.OutsourcingManager | UserRole.Estimator |
         UserRole.Director | UserRole.Accountant | UserRole.Secretary | UserRole.Findep | UserRole.Archivist)]
         public ActionResult CreateRequest()
         {
             int? userId = new int?();
             CreateRequestModel model = RequestBl.GetCreateRequestModel(userId);
             return View(model);
         }
        [HttpGet]
        public ContentResult GetUsersForDepartment(int departmentId)
        {
            DepartmentChildrenDto model;
            try
            {
                model = RequestBl.GetUsersForDepartment(departmentId);
            }
            catch (Exception ex)
            {
                Log.Error("Exception on GetUsersForDepartment:", ex);
                string error = ex.GetBaseException().Message;
                model = new DepartmentChildrenDto
                {
                    Error = string.Format("Ошибка: {0}", error),
                    Children = new List<IdNameDto>()
                };
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }

         [HttpPost]
         public ActionResult CreateRequest(CreateRequestModel model)
         {
             RequestTypeEnum type = (RequestTypeEnum)model.RequestTypeId;
             switch (type)
             {
                 case RequestTypeEnum.Vacation:
                     return RedirectToAction("VacationEdit",
                                             new RouteValueDictionary {
                                                                        {"id", 0}, 
                                                                        {"userId", model.UserId}
                                                                       });
                 case RequestTypeEnum.ChildVacation:
                     return RedirectToAction("ChildVacationEdit",
                                             new RouteValueDictionary {
                                                                        {"id", 0}, 
                                                                        {"userId", model.UserId}
                                                                       });
                 case RequestTypeEnum.Absence:
                     return RedirectToAction("AbsenceEdit",
                                             new RouteValueDictionary {
                                                                        {"id", 0}, 
                                                                        {"userId", model.UserId}
                                                                       });
                 case RequestTypeEnum.Sicklist:
                     return RedirectToAction("SicklistEdit",
                                             new RouteValueDictionary {
                                                                        {"id", 0}, 
                                                                        {"userId", model.UserId}
                                                                       });
                 case RequestTypeEnum.HolidayWork:
                     return RedirectToAction("HolidayWorkEdit",
                                             new RouteValueDictionary {
                                                                        {"id", 0}, 
                                                                        {"userId", model.UserId}
                                                                       });
                 case RequestTypeEnum.Mission:
                     return RedirectToAction("MissionEdit",
                                             new RouteValueDictionary {
                                                                        {"id", 0}, 
                                                                        {"userId", model.UserId}
                                                                       });
                 case RequestTypeEnum.Dismissal:
                     return RedirectToAction("DismissalEdit",
                                             new RouteValueDictionary {
                                                                        {"id", 0}, 
                                                                        {"userId", model.UserId}
                                                                       });
                 case RequestTypeEnum.TimesheetCorrection:
                     return RedirectToAction("TimesheetCorrectionEdit",
                                             new RouteValueDictionary {
                                                                        {"id", 0}, 
                                                                        {"userId", model.UserId}
                                                                       });
                 case RequestTypeEnum.Employment:
                     return RedirectToAction("EmploymentEdit",
                                             new RouteValueDictionary {
                                                                        {"id", 0}, 
                                                                        {"userId", model.UserId}
                                                                       });
                 default:
                     throw new ArgumentException("Неизвестный тип заявки");
             }
         }
         [HttpGet]
         [ReportAuthorize(UserRole.Employee | UserRole.Manager | UserRole.PersonnelManager | UserRole.ConsultantPersonnel |
          UserRole.Inspector | UserRole.Chief | UserRole.OutsourcingManager | UserRole.Estimator |
          UserRole.Director | UserRole.Accountant | UserRole.Secretary | UserRole.Findep | UserRole.Archivist | UserRole.ConsultantOutsourcing)]
         public ActionResult AllRequestList()
         {
             AllRequestListModel model = RequestBl.GetAllRequestListModel();
             return View(model);
         }
         [HttpPost]
         public ActionResult AllRequestList(AllRequestListModel model)
         {
             RequestBl.SetAllRequestListModel(model, !ValidateModel(model));
             return View(model);
         }
         #endregion

         #region Employment
         [HttpGet]
         [ReportAuthorize(UserRole.Employee | UserRole.Manager | UserRole.PersonnelManager | UserRole.ConsultantPersonnel |
          UserRole.Inspector | UserRole.Chief | UserRole.OutsourcingManager | UserRole.Estimator |
          UserRole.Director | UserRole.Accountant | UserRole.Secretary | UserRole.Findep | UserRole.Archivist)]
         public ActionResult EmploymentList()
         {
             EmploymentListModel model = RequestBl.GetEmploymentListModel();
             return View(model);
         }
         [HttpPost]
         public ActionResult EmploymentList(EmploymentListModel model)
         {
             RequestBl.SetEmploymentListModel(model,!ValidateModel(model));
             return View(model);
         }
         [HttpGet]
         [ReportAuthorize(UserRole.Employee | UserRole.Manager | UserRole.PersonnelManager | UserRole.ConsultantPersonnel |
          UserRole.Inspector | UserRole.Chief | UserRole.OutsourcingManager | UserRole.Estimator |
          UserRole.Director | UserRole.Accountant | UserRole.Secretary | UserRole.Findep | UserRole.Archivist)]
         public ActionResult EmploymentEdit(int id, int userId)
         {
             EmploymentEditModel model = RequestBl.GetEmploymentEditModel(id, userId);
             return View(model);
         }
         [HttpPost]
         public ActionResult EmploymentEdit(EmploymentEditModel model)
         {
             CorrectCheckboxes(model);
             CorrectDropdowns(model);
             //UploadFilesDto filesDto = GetFilesContexts();
             if (!ValidateEmploymentEditModel(model/*,filesDto*/))
             {
                 RequestBl.ReloadDictionariesToModel(model);
                 return View(model);
             }
             string error;
             if (!RequestBl.SaveEmploymentEditModel(model, /*filesDto,*/out error))
             {
                 if (model.ReloadPage)
                 {
                     ModelState.Clear();
                     if (!string.IsNullOrEmpty(error))
                         ModelState.AddModelError("", error);
                     return View(RequestBl.GetEmploymentEditModel(model.Id, model.UserId));
                 }
                 if (!string.IsNullOrEmpty(error))
                     ModelState.AddModelError("", error);
             }
             return View(model);
         }
         protected void CorrectDropdowns(EmploymentEditModel model)
         {
             if (!model.IsTypeEditable)
                 model.TypeId = model.TypeIdHidden;
             if (!model.IsTimesheetStatusEditable)
                 model.TimesheetStatusId = model.TimesheetStatusIdHidden;
             if (!model.IsTypeEditable)
                 model.GraphicTypeId = model.GraphicTypeIdHidden;
             if (!model.IsTypeEditable)
                 model.AdditionId = model.AdditionIdHidden;
             if (!model.IsTypeEditable)
                 model.PositionId = model.PositionIdHidden;
         }
         protected bool ValidateEmploymentEditModel(EmploymentEditModel model/*, UploadFilesDto filesDto*/)
         {
             if (model.Id > 0)
             {
                 int attachmentCount = RequestBl.GetAttachmentsCount(model.Id,RequestAttachmentTypeEnum.Employment);
                 if(attachmentCount < 4)
                 {
                     UserRole role = AuthenticationService.CurrentUser.UserRole;
                     if ((role == UserRole.Employee && model.IsApprovedByUser) ||
                         (role == UserRole.Manager && model.IsApprovedByManager) ||
                         (role == UserRole.PersonnelManager && model.IsApprovedByPersonnelManager))
                     {

                         ModelState.AddModelError(string.Empty,
                                                  "Заявка не может быть одобрена без прикрепления 4 обязательных файлов (копии паспорта, пенсионного, ИНН, 2НДФЛ).");
                         if (role == UserRole.Employee && model.IsApprovedByUser)
                         {
                             ModelState.Remove("IsApprovedByUser");
                             model.IsApprovedByUser = false;
                         }
                         if (role == UserRole.Manager && model.IsApprovedByManager)
                         {
                             ModelState.Remove("IsApprovedByManager");
                             model.IsApprovedByManager = false;
                         }
                         if (role == UserRole.PersonnelManager && model.IsApprovedByPersonnelManager)
                         {
                             ModelState.Remove("IsApprovedByPersonnelManager");
                             model.IsApprovedByPersonnelManager = false;
                         }
                     }
                 }
             }
             if (!string.IsNullOrEmpty(model.Probaion))
             {
                 int probation;
                 if (!Int32.TryParse(model.Probaion, out probation) ||
                     probation <= 0 || probation > 24)
                     ModelState.AddModelError("Probaion", "Поле 'Испытательный срок' должно быть положительным целым числом не больше 3.");
             }
             CheckDecimalNotNegativeValue("Salary", "'Оклад (тарифная ставка)'",model.Salary);
             CheckDecimalNotNegativeValue("RegionFactor", "'Районный коэффициент'", model.RegionFactor);
             CheckDecimalNotNegativeValue("NorthFactor", "'Северный коэффициент'", model.NorthFactor);
             CheckIntValue("RegionAddition", "'Надбавка территориальная'", model.RegionAddition, 1, 100000);
             CheckIntValue("PersonalAddition", "'Надбавка персональная'", model.PersonalAddition, 1, 100000);
             CheckIntValue("TravelWorkAddition", "'Надбавка за разъездной характер работы'", model.TravelWorkAddition, 1, 100000);
             CheckIntValue("SkillAddition", "'Надбавка за квалификацию'", model.SkillAddition, 1, 100000);
             CheckIntValue("LongWorkAddition", "'Надбавка за выслугу лет рабочим и служащим'", model.LongWorkAddition, 1, 100000);
             /*if (!string.IsNullOrEmpty(model.Salary))
             {
                 decimal salary;
                 if (!Decimal.TryParse(model.Salary, out salary) ||
                     salary <= 0)
                     ModelState.AddModelError("Salary", "Поле 'Оклад (тарифная ставка)' должно быть неотрицательным числом.");
             }*/
             //if(model.BeginDate.HasValue && model.EndDate.HasValue && model.BeginDate.Value > model.EndDate.Value)
             //    ModelState.AddModelError("BeginDate", "Поле 'Дата начало Т Д' должно быть не больше поля 'Дата окончания Т Д.'.");
             return ModelState.IsValid;
         }
         protected void CheckDecimalValue(string fieldModelName, string fieldName, 
             string modelValue, decimal? minValue, decimal? maxValue)
         {
             decimal value;
             if (!Decimal.TryParse(modelValue, out value))
                 ModelState.AddModelError(fieldModelName,string.Format("Поле {0} должно быть десятичным числом.",fieldName));
             else
             {
                 if(minValue.HasValue && minValue.Value > value)
                     ModelState.AddModelError(fieldModelName, string.Format("Поле {0} должно быть не меньше {1}.", fieldName, minValue.Value));
                 if (maxValue.HasValue && maxValue.Value < value)
                     ModelState.AddModelError(fieldModelName, string.Format("Поле {0} должно быть не больше {1}.", fieldName, maxValue.Value));
             }
         }
         protected void CheckIntValue(string fieldModelName, string fieldName,
              string modelValue, int? minValue, int? maxValue)
         {
             if (!string.IsNullOrEmpty(modelValue))
             {
                 int value;
                 if (!int.TryParse(modelValue, out value))
                     ModelState.AddModelError(fieldModelName,
                                              string.Format("Поле {0} должно быть целым числом.", fieldName));
                 else
                 {
                     if (minValue.HasValue && minValue.Value > value)
                         ModelState.AddModelError(fieldModelName,
                                                  string.Format("Поле {0} должно быть не меньше {1}.", fieldName,
                                                                minValue.Value));
                     if (maxValue.HasValue && maxValue.Value < value)
                         ModelState.AddModelError(fieldModelName,
                                                  string.Format("Поле {0} должно быть не больше {1}.", fieldName,
                                                                maxValue.Value));
                 }
             }
         }
         protected void CheckDecimalNotNegativeValue(string fieldModelName,string fieldName, string modelValue)
         {
             if (!string.IsNullOrEmpty(modelValue))
             {
                 decimal value;
                 if (!Decimal.TryParse(modelValue, out value))
                     ModelState.AddModelError(fieldModelName,
                                              string.Format("Поле {0} должно быть десятичным числом.", fieldName));
                 else
                 {
                     if (value <= 0)
                         ModelState.AddModelError(fieldModelName,
                                                  string.Format("Поле {0} должно быть положительным десятичным числом.",
                                                                fieldName));
                     if (value > 100000000)
                         ModelState.AddModelError(fieldModelName,
                                                  string.Format("Поле {0} должно быть положительным десятичным числом.",
                                                                fieldName));

                 }
             }
         }

         #endregion

         #region Timesheet Correction
         [HttpGet]
         [ReportAuthorize(UserRole.Employee | UserRole.Manager | UserRole.PersonnelManager | UserRole.ConsultantPersonnel |
          UserRole.Inspector | UserRole.Chief | UserRole.OutsourcingManager | UserRole.Estimator |
          UserRole.Director | UserRole.Accountant | UserRole.Secretary | UserRole.Findep | UserRole.Archivist)]
         public ActionResult TimesheetCorrectionList()
         {
             TimesheetCorrectionListModel model = RequestBl.GetTimesheetCorrectionListModel();
             return View(model);
         }
         [HttpPost]
         public ActionResult TimesheetCorrectionList(TimesheetCorrectionListModel model)
         {
             RequestBl.SetTimesheetCorrectionListModel(model,!ValidateModel(model));
             return View(model);
         }
         [HttpGet]
         [ReportAuthorize(UserRole.Employee | UserRole.Manager | UserRole.PersonnelManager | UserRole.ConsultantPersonnel |
          UserRole.Inspector | UserRole.Chief | UserRole.OutsourcingManager | UserRole.Estimator |
          UserRole.Director | UserRole.Accountant | UserRole.Secretary | UserRole.Findep | UserRole.Archivist)]
         public ActionResult TimesheetCorrectionEdit(int id, int userId)
         {
             TimesheetCorrectionEditModel model = RequestBl.GetTimesheetCorrectionEditModel(id, userId);
             return View(model);
         }
         [HttpPost]
         public ActionResult TimesheetCorrectionEdit(TimesheetCorrectionEditModel model)
         {
             CorrectCheckboxes(model);
             CorrectDropdowns(model);
             if (!ValidateTimesheetCorrectionEditModel(model))
             {
                 model.IsApproved = false;
                 RequestBl.ReloadDictionariesToModel(model);
                 return View(model);
             }
             string error;
             if (!RequestBl.SaveTimesheetCorrectionEditModel(model, out error))
             {
                 if (model.ReloadPage)
                 {
                     ModelState.Clear();
                     if (!string.IsNullOrEmpty(error))
                         ModelState.AddModelError("", error);
                     return View(RequestBl.GetTimesheetCorrectionEditModel(model.Id, model.UserId));
                 }
                 if (!string.IsNullOrEmpty(error))
                     ModelState.AddModelError("", error);
             }
             return View(model);
         }
         protected void CorrectDropdowns(TimesheetCorrectionEditModel model)
         {
             if (!model.IsTypeEditable)
                 model.TypeId = model.TypeIdHidden;
             if (!model.IsStatusEditable)
                 model.StatusId = model.StatusIdHidden;
         }
         protected bool ValidateTimesheetCorrectionEditModel(TimesheetCorrectionEditModel model)
         {
             if (!string.IsNullOrEmpty(model.Hours))
             {
                 int hours;
                 if (!Int32.TryParse(model.Hours, out hours) ||
                     hours <= 0 || hours > 24)
                     ModelState.AddModelError("Hours", "Поле 'Часы' должно быть положительным целым числом не больше 24.");
             }
             if(model.EventDate.HasValue && AuthenticationService.CurrentUser.UserRole == UserRole.Employee)
             {
                 DateTime eventDate = DateTime.Today;
                 DateTime lastSunday = eventDate.AddDays((int)eventDate.DayOfWeek == 0 ? -7 : - (int)eventDate.DayOfWeek);
                 if(lastSunday >= model.EventDate.Value)
                     ModelState.AddModelError("EventDate", "Дата не может быть ранее понедельника текущей недели.");
             }
             return ModelState.IsValid;
         }

         #endregion

         #region Dismissal

         [HttpGet]
         public ActionResult DismissalList()
         {
             DismissalListModel model = RequestBl.GetDismissalListModel();
             return View(model);
         }

         [HttpPost]
         public ActionResult DismissalList(DismissalListModel model)
         {
             RequestBl.SetDismissalListModel(model, !ValidateModel(model));
             ModelState.Clear();
             return View(model);
         }

         [HttpGet]
         public ActionResult DismissalEdit(int id, int userId)
         {
             DismissalEditModel model = RequestBl.GetDismissalEditModel(id, userId);
             return View(model);
         }

         [HttpPost]
         public ActionResult DismissalEdit(DismissalEditModel model)
         {
             CorrectCheckboxes(model);
             CorrectDropdowns(model);
             UploadFileDto fileDto = GetFileContext();
             UploadFileDto orderScanFileDto = GetFileContext("orderScanFile");
             UploadFileDto unsignedOrderScanFileDto = GetFileContext("unsignedOrderScanFile");
             UploadFileDto t2ScanFileDto = GetFileContext("t2ScanFile");
             UploadFileDto dismissalAgreementScanFileDto = GetFileContext("dismissalAgreementScanFile");
             UploadFileDto f182NScanFileDto = GetFileContext("f182NScanFile");
             UploadFileDto f2NDFLScanFileDto = GetFileContext("f2NDFLScanFile");
             UploadFileDto unsignedT2ScanFileDto = GetFileContext("unsignedT2ScanFile");
             UploadFileDto unsignedDismissalAgreementScanFileDto = GetFileContext("unsignedDismissalAgreementScanFile");
             UploadFileDto workbookRequestScanFileDto = GetFileContext("workbookRequestScanFile");

             if (!ValidateDismissalEditModel(model,fileDto))
             {
                 model.IsApproved = false;
                 model.IsApprovedForAll = false;
                 RequestBl.ReloadDictionariesToModel(model);
                 return View(model);
             }
             string error;
             if (!RequestBl.SaveDismissalEditModel(model, new Dictionary<RequestAttachmentTypeEnum, UploadFileDto> {
                    { RequestAttachmentTypeEnum.Dismissal, fileDto},
                    { RequestAttachmentTypeEnum.UnsignedDismissalOrderScan, unsignedOrderScanFileDto},
                    { RequestAttachmentTypeEnum.DismissalOrderScan, orderScanFileDto},
                    { RequestAttachmentTypeEnum.T2Scan, t2ScanFileDto },
                    { RequestAttachmentTypeEnum.DismissalAgreementScan, dismissalAgreementScanFileDto },
                    { RequestAttachmentTypeEnum.F182NScan, f182NScanFileDto },
                    { RequestAttachmentTypeEnum.F2NDFLScan, f2NDFLScanFileDto },
                    { RequestAttachmentTypeEnum.UnsignedT2Scan, unsignedT2ScanFileDto },
                    { RequestAttachmentTypeEnum.UnsignedDismissalAgreementScan, unsignedDismissalAgreementScanFileDto },
                    { RequestAttachmentTypeEnum.WorkbookRequestScan, workbookRequestScanFileDto}
                }, out error))
             {
                 //HttpContext.AddError(new Exception(error));
                 if (model.ReloadPage)
                 {
                     ModelState.Clear();
                     if (!string.IsNullOrEmpty(error))
                         ModelState.AddModelError("", error);
                     return View(RequestBl.GetDismissalEditModel(model.Id, model.UserId));
                 }
                 if (!string.IsNullOrEmpty(error))
                     ModelState.AddModelError("", error);
             }
             return View(model);
         }

         protected void CorrectDropdowns(DismissalEditModel model)
         {
             if (!model.IsPersonnelFieldsEditable)
                 model.TypeId = model.TypeIdHidden;
             /*if (!model.IsPersonnelFieldsEditable)
                 model.StatusId = model.StatusIdHidden;*/
             //model.DaysCount = model.DaysCountHidden;
         }

         protected bool ValidateDismissalEditModel(DismissalEditModel model, UploadFileDto fileDto)
         {
             //if (model.BeginDate.HasValue && model.EndDate.HasValue &&
             //    model.BeginDate > model.EndDate)
             //    ModelState.AddModelError("BeginDate", "Дата начала не может превышать дату окончания.");
             UserRole role = AuthenticationService.CurrentUser.UserRole;
             if (model.Id > 0 && fileDto == null)
             {
                 int attachmentCount = RequestBl.GetAttachmentsCount(model.Id, RequestAttachmentTypeEnum.Dismissal);
                 if (attachmentCount <= 0)
                 {
                     if ((role == UserRole.Employee && model.IsApprovedByUser) ||
                         (role == UserRole.Manager && model.IsApprovedByManager) ||
                         (role == UserRole.PersonnelManager && model.IsApprovedByPersonnelManager))
                     {

                         ModelState.AddModelError(string.Empty,
                                                  "Заявка не может быть согласована без прикрепленого заявления.");
                         if (role == UserRole.Employee && model.IsApprovedByUser)
                         {
                             ModelState.Remove("IsApprovedByUser");
                             model.IsApprovedByUser = false;
                         }
                         if (role == UserRole.Manager && model.IsApprovedByManager)
                         {
                             ModelState.Remove("IsApprovedByManager");
                             model.IsApprovedByManager = false;
                         }
                         if (role == UserRole.PersonnelManager && model.IsApprovedByPersonnelManager)
                         {
                             ModelState.Remove("IsApprovedByPersonnelManager");
                             model.IsApprovedByPersonnelManager = false;
                         }
                         //error = "Заявка не может быть согласована без прикрепленого скана больничного.";
                         //needToReload = true;
                         //return false;

                     }
                 }
             }
             if (role == UserRole.PersonnelManager && string.IsNullOrEmpty(model.Reason))
                 ModelState.AddModelError("Reason", "Основание документ - обязательное поле.");
             if(!string.IsNullOrEmpty(model.Compensation))
             {
                 decimal compensation;
                 if(!Decimal.TryParse(model.Compensation,out compensation) ||
                     compensation < 0)
                     ModelState.AddModelError("Compensation", "Кол-во дней компенсации должно быть неотрицательным десятичным числом.");
             }
             if (!string.IsNullOrEmpty(model.Reduction))
             {
                 decimal reduction;
                 if (!Decimal.TryParse(model.Reduction, out reduction) ||
                     reduction < 0)
                     ModelState.AddModelError("Reduction", "Кол-во дней удержания должно быть неотрицательным десятичным числом.");
             }
             if (role == UserRole.PersonnelManager && string.IsNullOrEmpty(model.Compensation) && string.IsNullOrEmpty(model.Reduction))
                 ModelState.AddModelError("Compensation", "Укажите \"Кол-во дней компенсации\" и/или \"Кол-во дней удержания\"");

             CheckEndDate(model);
             return ModelState.IsValid;
         }

         #endregion

         #region ClearanceChecklist

         [HttpGet]
         public ActionResult ClearanceChecklistList()
         {
             var model = RequestBl.GetClearanceChecklistListModel();
             return View(model);
         }

         [HttpPost]
         public ActionResult ClearanceChecklistList(ClearanceChecklistListModel model)
         {
             RequestBl.SetClearanceChecklistListModel(model, !ValidateModel(model));
             return View(model);
         }

         [HttpGet]
         public ActionResult ClearanceChecklistEdit(int id, int? parentId, int userId)
         {
             if (parentId == null)
             {
                 var model = RequestBl.GetClearanceChecklistEditModel(id, userId);
                 return View(model);
             }
             else
             {
                 var model = RequestBl.GetClearanceChecklistEditModelByParentId((int)parentId, userId);
                 return PartialView("ClearanceChecklistEditPartial", model);
             }
         }

         [HttpPost]
         public ActionResult ClearanceChecklistEdit(ClearanceChecklistEditModel model)
         {
             model = RequestBl.GetClearanceChecklistEditModel(model.Id, model.UserId);
             return View(model);
         }
        
         [HttpPost]
         public JsonResult ClearanceChecklistApprove(int id)
         {
             string error = "";
             ClearanceChecklistApprovalDto modifiedApproval;
             if (RequestBl.SetClearanceChecklistApproval(id, AuthenticationService.CurrentUser.Id, out modifiedApproval, out error))
             {
                 return Json(new { ok = true, approvalId = id,  approvedBy = modifiedApproval.ApprovedBy, approvalDate = modifiedApproval.ApprovalDate });
             }
             else
             {
                 return Json(new { ok = false, approvalId = id, error = error });
             }
         }

         [HttpPost]
         public JsonResult ClearanceChecklistSaveComment(int id, string comment)
         {
             string error = "";
             if (RequestBl.SetClearanceChecklistComment(id, comment, out error))
             {
                 return Json(new { ok = true });
             }
             else
             {
                 return Json(new { ok = false, error = error });
             }            
         }

         [HttpPost]
         public JsonResult ClearanceChecklistSaveBottomFields(int id, int? registryNumber, decimal? personalIncomeTax, string oKTMO)
         {
             string error = "";
             if (RequestBl.SetClearanceChecklistBottomFields(id, registryNumber, personalIncomeTax, oKTMO, out error))
             {
                 return Json(new { ok = true });
             }
             else
             {
                 return Json(new { ok = false, error = error });
             }
         }

         #endregion

         #region Mission
         [HttpGet]
         public ActionResult MissionList()
         {
             MissionListModel model = RequestBl.GetMissionListModel();
             return View(model);
         }
         [HttpPost]
         public ActionResult MissionList(MissionListModel model)
         {
             RequestBl.SetMissionListModel(model,!ValidateModel(model));
             if (model.HasErrors)
                 ModelState.AddModelError(string.Empty, "При согласовании заявок произошла(и) ошибка(и).Не всем заявкам был установлен флаг пересчета.");
             return View(model);
         }
         protected bool ValidateModel(MissionListModel model)
         {
             if (model.BeginDate.HasValue && model.EndDate.HasValue &&
                 model.BeginDate.Value > model.EndDate.Value)
                 ModelState.AddModelError("BeginDate", "Дата в поле <Период с> не может быть больше даты в поле <по>.");
             return ModelState.IsValid;
         }
         [HttpGet]
         public ActionResult MissionEdit(int id, int userId)
         {
             MissionEditModel model = RequestBl.GetMissionEditModel(id, userId);
             return View(model);
         }
         [HttpPost]
         public ActionResult MissionEdit(MissionEditModel model)
         {
             CorrectCheckboxes(model);
             CorrectDropdowns(model);
             UploadFileDto orderScanFileDto = GetFileContext("orderScanFile");
             if (!ValidateMissionEditModel(model))
             {
                 model.IsApproved = false;
                 model.IsApprovedForAll = false;
                 RequestBl.ReloadDictionariesToModel(model);
                 return View(model);
             }
             string error;
             if (!RequestBl.SaveMissionEditModel(model, orderScanFileDto, out error))
             {
                 //HttpContext.AddError(new Exception(error));
                 if (model.ReloadPage)
                 {
                     ModelState.Clear();
                     if (!string.IsNullOrEmpty(error))
                         ModelState.AddModelError("", error);
                     return View(RequestBl.GetMissionEditModel(model.Id, model.UserId));
                 }
                 if (!string.IsNullOrEmpty(error))
                     ModelState.AddModelError("", error);
             }
             return View(model);
         }
         protected void CorrectDropdowns(MissionEditModel model)
         {
             if (!model.IsTypeEditable)
                 model.TypeId = model.TypeIdHidden;
             if (!model.IsTimesheetStatusEditable)
                 model.TimesheetStatusId = model.TimesheetStatusIdHidden;
             model.DaysCount = model.DaysCountHidden;
         }
         protected bool ValidateMissionEditModel(MissionEditModel model)
         {
             if (model.BeginDate.HasValue && model.EndDate.HasValue &&
                 model.BeginDate > model.EndDate)
                 ModelState.AddModelError("BeginDate", "Дата начала не может превышать дату окончания.");
             UserRole role = AuthenticationService.CurrentUser.UserRole;
             if(role == UserRole.PersonnelManager && string.IsNullOrEmpty(model.Reason))
                 ModelState.AddModelError("Reason", "Основание командировки - обязательное поле.");
             CheckBeginDate(model);
             return ModelState.IsValid;
         }
         #endregion

         #region HolidayWork
         [HttpGet]
         public ActionResult HolidayWorkList()
         {
             HolidayWorkListModel model = RequestBl.GetHolidayWorkListModel();
             return View(model);
         }
         [HttpPost]
         public ActionResult HolidayWorkList(HolidayWorkListModel model)
         {
             RequestBl.SetHolidayWorkListModel(model,!ValidateModel(model));
             return View(model);
         }
         [HttpGet]
         public ActionResult HolidayWorkEdit(int id, int userId)
         {
             HolidayWorkEditModel model = RequestBl.GetHolidayWorkEditModel(id, userId);
             return View(model);
         }
         [HttpPost]
         public ActionResult HolidayWorkEdit(HolidayWorkEditModel model)
         {
             CorrectCheckboxes(model);
             CorrectDropdowns(model);
             if (!ValidateHolidayWorkEditModel(model))
             {
                 RequestBl.ReloadDictionariesToModel(model);
                 return View(model);
             }
             string error;
             if (!RequestBl.SaveHolidayWorkEditModel(model, out error))
             {
                 if (model.ReloadPage)
                 {
                     ModelState.Clear();
                     if (!string.IsNullOrEmpty(error))
                         ModelState.AddModelError("", error);
                     return View(RequestBl.GetHolidayWorkEditModel(model.Id, model.UserId));
                 }
                 if (!string.IsNullOrEmpty(error))
                     ModelState.AddModelError("", error);
             }
             return View(model);
         }
         protected bool ValidateHolidayWorkEditModel(HolidayWorkEditModel model)
         {
             if (model.IsTypeEditable)
             {
                 //if (!string.IsNullOrEmpty(model.Rate))
                 //{
                 //    int rate;
                 //    if (!Int32.TryParse(model.Rate, out rate))
                 //        ModelState.AddModelError("Rate", "Неправильное поле 'Часовая тарифная ставка'.");
                 //    else if (rate <= 0)
                 //        ModelState.AddModelError("Rate",
                 //                                 "Поле 'Часовая тарифная ставка' должно быть положительным числом.");
                 //}
                 if (!string.IsNullOrEmpty(model.Hours))
                 {
                     int hours;
                     if (!Int32.TryParse(model.Hours, out hours))
                         ModelState.AddModelError("Hours", "Неправильное поле 'Кол-во отработанных часов'.");
                     else if (hours <= 0 || hours > 24)
                         ModelState.AddModelError("Hours",
                                                  "Поле 'Кол-во отработанных часов'  должно быть положительным числом меньшим 25.");
                 }
             }
             return ModelState.IsValid;
         }
         protected void CorrectDropdowns(HolidayWorkEditModel model)
         {
             if (!model.IsTypeEditable)
                 model.TypeId = model.TypeIdHidden;
             if (!model.IsTimesheetStatusEditable)
                 model.TimesheetStatusId = model.TimesheetStatusIdHidden;
         }
         #endregion

         #region UserPersonnelData
         [HttpGet]
         public ActionResult UserPersonnelDataList()
         {
             var model= RequestBl.GetUsersPersonnelDataListModel();
             return View(model);
         }
        [HttpPost]
         public ActionResult UserPersonnelDataList(UsersPersonnelDataViewModel model)
         {
             model = RequestBl.SetDocuments(model);
             return View(model);
         }
        
         public ContentResult SetUserINN(int personId, string inn)
         {
             return Content(RequestBl.SetUserInn(personId, inn));
         }
         #endregion
         #region Sicklist
         [HttpGet]
         public ActionResult SicklistList()
         {                    
             SicklistListModel model = RequestBl.GetSicklistListModel();
             return View(model);
         }
         [HttpPost]
         public ActionResult SicklistList(SicklistListModel model)
         {             
             RequestBl.SetSicklistListModel(model, !ValidateModel(model));
             ModelState.Clear();
             return View(model);
         }

         [HttpGet]
         public ActionResult SicklistEdit(int id, int userId)
         {
             SicklistEditModel model = RequestBl.GetSicklistEditModel(id, userId);
             return View(model);
         }
         [HttpPost]
         public ActionResult SicklistEdit(SicklistEditModel model)
         {
             CorrectCheckboxes(model);
             CorrectDropdowns(model);
             UploadFileDto fileDto = GetFileContext();
             //bool needToReload;
             //string error;
             if (!ValidateSicklistEditModel(model, fileDto/*,out needToReload,out error*/))
             {
                 model.IsApproved = false;
                 model.IsApprovedForAll = false;
                 //if(needToReload)
                 //{
                 //    ModelState.Clear();
                 //    if (!string.IsNullOrEmpty(error))
                 //        ModelState.AddModelError("", error);
                 //    return View(RequestBl.GetSicklistEditModel(model.Id, model.UserId)); 
                 //}
                 RequestBl.ReloadDictionariesToModel(model);
                 return View(model);
             }
             string error;
             if (!RequestBl.SaveSicklistEditModel(model, fileDto, out error))
             {
                 //HttpContext.AddError(new Exception(error));
                 if (model.ReloadPage)
                 {
                     ModelState.Clear();
                     if (!string.IsNullOrEmpty(error))
                         ModelState.AddModelError("", error);
                     return View(RequestBl.GetSicklistEditModel(model.Id, model.UserId));
                 }
                 if (!string.IsNullOrEmpty(error))
                     ModelState.AddModelError("", error);
             }
             return View(model);
         }
         
         [HttpPost]
         public JsonResult SicklistSendErrorNotification(int id)
         {
             string error = "";
             if (RequestBl.ResetSicklistApprovals(id, out error))
             {
                 return Json(new { ok = true });
             }
             else
             {
                 return Json(new { ok = false, error = error });
             }
         }
         
         protected bool ValidateSicklistEditModel(SicklistEditModel model, UploadFileDto fileDto
             /*out bool needToReload,out string error*/)
         {
             //needToReload = false;
             //error = string.Empty;
             if (model.Id > 0 && fileDto == null)
             {
                 int attachmentCount = RequestBl.GetAttachmentsCount(model.Id,RequestAttachmentTypeEnum.Sicklist);
                 if (attachmentCount <= 0)
                 {
                     UserRole role = AuthenticationService.CurrentUser.UserRole;
                     if ((role == UserRole.Employee && model.IsApprovedByUser) ||
                         (role == UserRole.Manager && model.IsApprovedByManager) ||
                         ((role == UserRole.PersonnelManager || role == UserRole.OutsourcingManager || role == UserRole.Estimator) && model.IsApprovedByPersonnelManager))
                     {

                         ModelState.AddModelError(string.Empty,
                                                  "Заявка не может быть согласована без прикрепленого скана больничного.");
                         if (role == UserRole.Employee && model.IsApprovedByUser)
                         {
                             ModelState.Remove("IsApprovedByUser");
                             model.IsApprovedByUser = false;
                         }
                         if (role == UserRole.Manager && model.IsApprovedByManager)
                         {
                             ModelState.Remove("IsApprovedByManager");
                             model.IsApprovedByManager = false;
                         }
                         if (role == UserRole.PersonnelManager && model.IsApprovedByPersonnelManager)
                         {
                             ModelState.Remove("IsApprovedByPersonnelManager");
                             model.IsApprovedByPersonnelManager = false;
                         }
                         //error = "Заявка не может быть согласована без прикрепленого скана больничного.";
                         //needToReload = true;
                         //return false;

                     }
                 }
             }
             if (model.BeginDate.HasValue && model.EndDate.HasValue)
             {
                 UserRole role = AuthenticationService.CurrentUser.UserRole;

                 // Проверка на дубликаты
                 int requestCount = RequestBl.GetOtherRequestCountsForUserAndDates(model.BeginDate.Value, model.EndDate.Value, model.UserId, model.Id, RequestTypeEnum.Sicklist);
                 if (requestCount > 0)
                     ModelState.AddModelError("BeginDate", "Для данного пользователя существуют другие заявки в указанном интервале дат.");

                 if(model.BeginDate > model.EndDate)
                    ModelState.AddModelError("BeginDate", "Дата начала отпуска не может превышать дату окончания отпуска.");
                 else if (!model.IsDelete && model.IsApproved)
                 {
                     DateTime beginDate = model.BeginDate.Value;
                     DateTime current = DateTime.Today;
                     DateTime monthBegin = new DateTime(current.Year, current.Month, 1);
                     bool isValid = true;
                     if ((current.Day != 1) && monthBegin > beginDate)
                     {
                         isValid = RequestBl.HaveAbsencesForPeriod(model.BeginDate.Value,
                             model.EndDate.Value, model.UserId, AuthenticationService.CurrentUser.Id,role);
                         //ModelState.AddModelError(string.Empty, "Создание/изменение заявки в прошлом запрещено .");
                         //return;
                     }
                     if ((current.Day == 1) && monthBegin.AddMonths(-1) > beginDate)
                     {
                         isValid = RequestBl.HaveAbsencesForPeriod(model.BeginDate.Value,
                             model.EndDate.Value, model.UserId, AuthenticationService.CurrentUser.Id,role);
                         //ModelState.AddModelError(string.Empty, "Создание/изменение заявки в прошлом запрещено .");
                         //return;
                     }
                     if(!isValid)
                     {
                         Log.InfoFormat("Absence not found for sicklist {0}",model.Id);
                         ModelState.AddModelError(string.Empty,
          "Период, указанный в заявке,не соответствует данным по неявкам в табеле.Вы не можете согласовать эту заявку.");
                     }
                 }
             }

             if (!string.IsNullOrEmpty(model.SicklistNumber))
             {
                 Regex r = new Regex(@"^\d{12}$");
                 if(!r.IsMatch(model.SicklistNumber))
                    ModelState.AddModelError("SicklistNumber", "Номер больничного листа должен содержать 12 цифр");
             }

             if (model.IsPersonnelFieldsEditable && AuthenticationService.CurrentUser.UserRole != UserRole.OutsourcingManager && AuthenticationService.CurrentUser.UserRole != UserRole.Estimator)
             {
                 if (string.IsNullOrEmpty(model.ExperienceYears) && string.IsNullOrEmpty(model.ExperienceYears) && !(model.ExperienceIn1C == true))
                    ModelState.AddModelError("ExperienceYears", "Необходимо заполнить хотя бы одно из полей стажа.");
                 
                 if (!string.IsNullOrEmpty(model.ExperienceYears))
                 {
                     int experienceYears;
                     if (!Int32.TryParse(model.ExperienceYears, out experienceYears))
                         ModelState.AddModelError("ExperienceYears", "Неправильно указано число лет стажа.");
                     else if (experienceYears < 0)
                         ModelState.AddModelError("ExperienceYears",
                                                  "Число лет стажа должно быть неотрицательным числом.");
                 }
                 if (!string.IsNullOrEmpty(model.ExperienceMonthes))
                 {
                     int experienceMonth;
                     if (!Int32.TryParse(model.ExperienceMonthes, out experienceMonth))
                         ModelState.AddModelError("ExperienceMonthes", "Неправильно указано число месяцев стажа.");
                     else if (experienceMonth < 0 || experienceMonth > 11)
                         ModelState.AddModelError("ExperienceMonthes",
                                                  "Число месяцев стажа должно быть неотрицательным числом меньшим 12.");
                 }
             }

             if (model.IsPersonnelFieldsEditable)
             {
                 if (!model.PaymentBeginDate.HasValue)
                     ModelState.AddModelError("PaymentBeginDate","'Назначить с даты' - обязательное поле.");

                 if(model.PaymentBeginDate.HasValue && model.BeginDate.HasValue && model.BeginDate.Value > model.PaymentBeginDate.Value)
                     ModelState.AddModelError("PaymentBeginDate",
                                                  "Поле 'Назначить с даты' не должно быть меньше поля 'Дата начала'.");
                 if (model.PaymentBeginDate.HasValue && model.EndDate.HasValue && model.EndDate.Value < model.PaymentBeginDate.Value)
                     ModelState.AddModelError("PaymentBeginDate",
                                                  "Поле 'Назначить с даты' не должно быть больше поля 'Дата окончания'.");
                 if (model.PaymentDecreaseDate.HasValue && model.BeginDate.HasValue && model.BeginDate.Value > model.PaymentDecreaseDate.Value)
                     ModelState.AddModelError("PaymentDecreaseDate",
                                                  "Поле 'Снизить пособие за нарушение режима с' не должно быть меньше поля 'Дата начала'.");
                 if (model.PaymentDecreaseDate.HasValue && model.EndDate.HasValue && model.EndDate.Value < model.PaymentDecreaseDate.Value)
                     ModelState.AddModelError("PaymentBeginDate",
                                                  "Поле 'Снизить пособие за нарушение режима с' не должно быть больше поля 'Дата окончания'.");
             }
             return ModelState.IsValid;
         }
         protected void CorrectDropdowns(SicklistEditModel model)
         {
             if (!model.IsTypeEditable)
             {
                 model.TypeId = model.TypeIdHidden;
                 model.BabyMindingTypeId = model.BabyMindingTypeIdHidden;
             }
             if(!model.IsDatesEditable)
                 model.IsContinued = model.IsContinuedHidden;
             /*if (!model.IsBabyMindingTypeEditable)
                 model.BabyMindingTypeId = model.BabyMindingTypeIdHidden;*/
             if (!model.IsTimesheetStatusEditable)
                 model.TimesheetStatusId = model.TimesheetStatusIdHidden;
             model.DaysCount = model.DaysCountHidden;
             if(!model.IsPersonnelFieldsEditable)
             {
                 model.PaymentRestrictTypeId = model.PaymentRestrictTypeIdHidden;
                 model.PaymentPercentTypeId = model.PaymentPercentTypeIdHidden;
             }
         }
         #endregion

         #region Absence
         [HttpGet]
         [ReportAuthorize(UserRole.Employee | UserRole.Manager | UserRole.PersonnelManager | UserRole.ConsultantPersonnel |
         UserRole.Inspector | UserRole.Chief | UserRole.OutsourcingManager | UserRole.Estimator |
         UserRole.Director | UserRole.Accountant | UserRole.Secretary | UserRole.Findep | UserRole.Archivist | UserRole.ConsultantOutsourcing)]
         public ActionResult AbsenceList()
         {
             AbsenceListModel model = RequestBl.GetAbsenceListModel();
             return View(model);
         }
         [HttpPost]
         public ActionResult AbsenceList(AbsenceListModel model)
         {
             if (model.DepartmentName == null)
                 model.DepartmentName = string.Empty;
             RequestBl.SetAbsenceListModel(model, !ValidateModel(model));
             return View(model);
         }
         protected bool ValidateModel(BeginEndCreateDate model)
         {
             if (model.BeginDate.HasValue && model.EndDate.HasValue &&
                 model.BeginDate.Value > model.EndDate.Value)
                 ModelState.AddModelError("BeginDate", "Дата в поле <Период с> не может быть больше даты в поле <по>.");
             return ModelState.IsValid;
         }
         [HttpGet]
         [ReportAuthorize(UserRole.Employee | UserRole.Manager | UserRole.PersonnelManager | UserRole.ConsultantPersonnel |
          UserRole.Inspector | UserRole.Chief | UserRole.OutsourcingManager | UserRole.Estimator |
          UserRole.Director | UserRole.Accountant | UserRole.Secretary | UserRole.Findep | UserRole.Archivist | UserRole.ConsultantOutsourcing)]
         public ActionResult AbsenceEdit(int id, int userId)
         {
             //int? userId = new int?();
             AbsenceEditModel model = RequestBl.GetAbsenceEditModel(id, userId);
             return View(model);
         }
         [HttpPost]
         public ActionResult AbsenceEdit(AbsenceEditModel model)
         {
             CorrectCheckboxes(model);
             CorrectDropdowns(model);
             if (!ValidateAbsenceEditModel(model))
             {
                 model.IsApproved = false;
                 model.IsApprovedForAll = false;
                 RequestBl.ReloadDictionariesToModel(model);
                 return View(model);
             }
             string error;
             if (!RequestBl.SaveAbsenceEditModel(model, out error))
             {
                 //HttpContext.AddError(new Exception(error));
                 if (model.ReloadPage)
                 {
                     ModelState.Clear();
                     if (!string.IsNullOrEmpty(error))
                         ModelState.AddModelError("", error);
                     return View(RequestBl.GetAbsenceEditModel(model.Id, model.UserId));
                 }
                 if (!string.IsNullOrEmpty(error))
                     ModelState.AddModelError("", error);
             }
             return View(model);
         }
         protected void CorrectDropdowns(AbsenceEditModel model)
         {
             if (!model.IsAbsenceTypeEditable)
                model.AbsenceTypeId = model.AbsenceTypeIdHidden;
             if (!model.IsTimesheetStatusEditable)
                 model.TimesheetStatusId = model.TimesheetStatusIdHidden;
             model.DaysCount = model.DaysCountHidden;
         }
         protected bool ValidateAbsenceEditModel(AbsenceEditModel model)
         {
             if (model.BeginDate.HasValue && model.EndDate.HasValue &&
                 model.BeginDate > model.EndDate)
                 ModelState.AddModelError("BeginDate", "Дата начала отпуска не может превышать дату окончания отпуска.");
             //int dayCounts;
             //if(!Int32.TryParse(model.DaysCount, out dayCounts))
             //   ModelState.AddModelError("DaysCount", "Количество дней (часов) должно быть числом.");
             //else
             //{
             //    if (dayCounts <= 0)
             //        ModelState.AddModelError("DaysCount", "Количество дней (часов) должно быть положительным числом.");
             //}
             CheckBeginDate(model);
             return ModelState.IsValid;
         }
         #endregion

         #region Vacation
         [HttpGet]
         [ReportAuthorize(UserRole.Employee | UserRole.Manager | UserRole.PersonnelManager | UserRole.ConsultantPersonnel |
          UserRole.Inspector | UserRole.Chief | UserRole.OutsourcingManager | UserRole.Estimator|
          UserRole.Director | UserRole.Accountant | UserRole.Secretary | UserRole.Findep | UserRole.Archivist | UserRole.ConsultantOutsourcing)]
         public ActionResult VacationList()
         {
             //int? userId = new int?();
             VacationListModel model = RequestBl.GetVacationListModel();
             return View(model);
         }
         [HttpPost]
         public ActionResult VacationList(VacationListModel model)
         {
             RequestBl.SetVacationListModel(model, !ValidateModel(model));
             ModelState.Clear();
             return View(model);
         }

        protected bool ValidateModel(VacationListModel model)
         {
             if(model.BeginDate.HasValue && model.EndDate.HasValue && 
                 model.BeginDate.Value > model.EndDate.Value)
                 ModelState.AddModelError("BeginDate", "Дата в поле <Период с> не может быть больше даты в поле <по>.");
             return ModelState.IsValid;
         }

        [HttpGet]
        [ReportAuthorize(UserRole.Employee | UserRole.Manager | UserRole.PersonnelManager | UserRole.ConsultantPersonnel |
         UserRole.Inspector | UserRole.Chief | UserRole.OutsourcingManager | UserRole.Estimator |
         UserRole.Director | UserRole.Accountant | UserRole.Secretary | UserRole.Findep | UserRole.Archivist | UserRole.ConsultantOutsourcing)]
         public ActionResult VacationEdit(int id,int userId)
         {
             //int? userId = new int?();
             VacationEditModel model = RequestBl.GetVacationEditModel(id,userId);
             ModelState.Clear();
             return View(model);
         }
         [HttpPost]
         public ActionResult VacationEdit(VacationEditModel model)
         {
             string source = Newtonsoft.Json.JsonConvert.SerializeObject(model);
             CorrectCheckboxes(model);
             CorrectDropdowns(model);
             UploadFileDto fileDto = GetFileContext();
             UploadFileDto orderScanFileDto = GetFileContext("orderScanFile");
             UploadFileDto unsignedOrderScanFileDto = GetFileContext("unsignedOrderScanFile");
             #region Для сохранения файла после выгрузки в 1с #Заплатка
             bool IsSaveFileAfterSendTo1C = false;
             if(orderScanFileDto!=null && model.Id>0)
             RequestBl.SaveVacationFileAfter1C(model, orderScanFileDto)
                 .OnSuccess(x =>
                     {
                         RequestBl.ReloadDictionariesToModel(model);
                         IsSaveFileAfterSendTo1C = true;
                     }
             );
             if (IsSaveFileAfterSendTo1C) return View(model);
             #endregion
             if (!ValidateVacationEditModel(model,fileDto))
             {
                 model.IsApproved = false;
                 model.IsApprovedForAll = false;
                 
                     RequestBl.ReloadDictionariesToModel(model);
                
                 return View(model);
             }

             string error;
             if (!RequestBl.SaveVacationEditModel(model, fileDto, unsignedOrderScanFileDto, orderScanFileDto, out error))
             {
                 
                 if (model.ReloadPage)
                 {
                     ModelState.Clear();
                     if (!string.IsNullOrEmpty(error))
                         ModelState.AddModelError("", error);
                     return View(RequestBl.GetVacationEditModel(model.Id, model.UserId));
                 }
                 if (!string.IsNullOrEmpty(error))
                     ModelState.AddModelError("", error);
             }
             return View(model);
         }

         [HttpPost]
         public JsonResult VacationSendErrorNotification(int id)
         {
             string error = "";
             if (RequestBl.ResetVacationApprovals(id, out error))
             {
                 return Json(new { ok = true });
             }
             else
             {
                 return Json(new { ok = false, error = error });
             }
         }

         protected bool ValidateVacationEditModel(VacationEditModel model, UploadFileDto fileDto)
         {
             ModelState.Clear();
             UserRole role = AuthenticationService.CurrentUser.UserRole;
             if (model.Id > 0 && fileDto == null)
             {
                 int attachmentCount = RequestBl.GetAttachmentsCount(model.Id, RequestAttachmentTypeEnum.Vacation);
                 if (attachmentCount <= 0)
                 {
                     if ((role == UserRole.Employee && model.IsApprovedByUser) ||
                         (role == UserRole.Manager && model.IsApprovedByManager) ||
                         (role == UserRole.PersonnelManager && model.IsApprovedByPersonnelManager))
                     {

                         ModelState.AddModelError(string.Empty,
                                                  "Заявка не может быть согласована без прикрепленого документа.");
                         if (role == UserRole.Employee && model.IsApprovedByUser)
                         {
                             ModelState.Remove("IsApprovedByUser");
                             model.IsApprovedByUser = false;
                         }
                         if (role == UserRole.Manager && model.IsApprovedByManager)
                         {
                             ModelState.Remove("IsApprovedByManager");
                             model.IsApprovedByManager = false;
                         }
                         if (role == UserRole.PersonnelManager && model.IsApprovedByPersonnelManager)
                         {
                             ModelState.Remove("IsApprovedByPersonnelManager");
                             model.IsApprovedByPersonnelManager = false;
                         }
                     }
                 }
             }

             if(model.BeginDate.HasValue && model.EndDate.HasValue)
             {
                 if(model.BeginDate > model.EndDate)
                    ModelState.AddModelError("BeginDate", "Дата начала отпуска не может превышать дату окончания отпуска.");
                 else if(!model.IsDelete)
                 {
                     int requestCount = RequestBl.GetOtherRequestCountsForUserAndDates
                         (model.BeginDate.Value, model.EndDate.Value, 
                         model.UserId, model.Id, false);
                     if(requestCount > 0)
                         ModelState.AddModelError("BeginDate", 
                      "Для данного пользователя существуют другие заявки в указанном интервале дат.");
                 }
             }
             CheckBeginDate(model);
             return ModelState.IsValid;
         }
         protected void CorrectDropdowns(VacationEditModel model)
         {
             if (!model.IsVacationTypeEditable)
                 model.VacationTypeId = model.VacationTypeIdHidden;
             if (!model.IsTimesheetStatusEditable)
                 model.TimesheetStatusId = model.TimesheetStatusIdHidden;
             model.DaysCount = model.DaysCountHidden;
         }
         protected void CorrectCheckboxes(ICheckBoxes model)
         {
             if (!model.IsApprovedByManagerEnable && model.IsApprovedByManagerHidden)
             {
                 //if (ModelState.ContainsKey("IsApprovedByManager"))
                 //    ModelState.Remove("IsApprovedByManager");
                 model.IsApprovedByManager = model.IsApprovedByManagerHidden;
             }
             if (!model.IsApprovedByPersonnelManagerEnable && model.IsApprovedByPersonnelManagerHidden)
             {
                 //if (ModelState.ContainsKey("IsApprovedByPersonnelManager"))
                 //    ModelState.Remove("IsApprovedByPersonnelManager");
                 model.IsApprovedByPersonnelManager = model.IsApprovedByPersonnelManagerHidden;
             }
             if (!model.IsApprovedByUserEnable && model.IsApprovedByUserHidden)
             {
                 //if (ModelState.ContainsKey("IsApprovedByUser"))
                 //    ModelState.Remove("IsApprovedByUser");
                 model.IsApprovedByUser = model.IsApprovedByUserHidden;
             }
             //if (model.IsApprovedByUserEnable && model.IsApprovedByUserChecked)
             //{
             //    if (ModelState.ContainsKey("IsApprovedByUser"))
             //        ModelState.Remove("IsApprovedByUser");
             //    model.IsApprovedByUser = true;
             //}
             if (!model.IsPostedTo1CEnable && model.IsPostedTo1CHidden)
             {
                 //if (ModelState.ContainsKey("IsPostedTo1C"))
                 //    ModelState.Remove("IsPostedTo1C");
                 model.IsPostedTo1C = model.IsPostedTo1CHidden;
             }
         }
         #endregion

         #region Child Vacation
         [HttpGet]
         [ReportAuthorize(UserRole.Employee | UserRole.Manager | UserRole.PersonnelManager | UserRole.ConsultantPersonnel |
          UserRole.Inspector | UserRole.Chief | UserRole.OutsourcingManager | UserRole.Estimator |
          UserRole.Director | UserRole.Accountant | UserRole.Secretary | UserRole.Findep | UserRole.Archivist | UserRole.ConsultantOutsourcing)]
         public ActionResult ChildVacationList()
         {
             //int? userId = new int?();
             ChildVacationListModel model = RequestBl.GetChildVacationListModel();
             return View(model);
         }
         [HttpPost]
         public ActionResult ChildVacationList(ChildVacationListModel model)
         {
             RequestBl.SetChildVacationListModel(model, !ValidateModel(model));
             //ModelState.Clear();
             return View(model);
         }
         protected bool ValidateModel(ChildVacationListModel model)
         {
             if (model.BeginDate.HasValue && model.EndDate.HasValue &&
                 model.BeginDate.Value > model.EndDate.Value)
                 ModelState.AddModelError("BeginDate", "Дата в поле <Период с> не может быть больше даты в поле <по>.");
             if (model.Number != null)
             {
                 try { Convert.ToInt32(model.Number); }                 
                 catch{ ModelState.AddModelError("Number", "Номер заявки числовое поле");}
             }
             return ModelState.IsValid;
         }


         [HttpGet]
         [ReportAuthorize(UserRole.Employee | UserRole.Manager | UserRole.PersonnelManager | UserRole.ConsultantPersonnel |
          UserRole.Inspector | UserRole.Chief | UserRole.OutsourcingManager | UserRole.Estimator |
          UserRole.Director | UserRole.Accountant | UserRole.Secretary | UserRole.Findep | UserRole.Archivist | UserRole.ConsultantOutsourcing)]
         public ActionResult ChildVacationEdit(int id, int userId)
         {
             //int? userId = new int?();
             ChildVacationEditModel model = RequestBl.GetChildVacationEditModel(id, userId);
             return View(model);
         }
         [HttpPost]
         public ActionResult ChildVacationEdit(ChildVacationEditModel model)
         {
             CorrectCheckboxes(model);
             CorrectDropdowns(model);
             UploadFileDto fileDto = GetFileContext();
             UploadFileDto orderScanFileDto = GetFileContext("orderScanFile");
             if (!ValidateChildVacationEditModel(model, fileDto))
             {
                 model.IsApproved = false;
                 model.IsApprovedForAll = false;
                 RequestBl.ReloadDictionariesToModel(model);
                 return View(model);
             }

             string error;
             if (!RequestBl.SaveChildVacationEditModel(model, fileDto, orderScanFileDto, out error))
             {

                 if (model.ReloadPage)
                 {
                     ModelState.Clear();
                     if (!string.IsNullOrEmpty(error))
                         ModelState.AddModelError("", error);
                     return View(RequestBl.GetChildVacationEditModel(model.Id, model.UserId));
                 }
                 if (!string.IsNullOrEmpty(error))
                     ModelState.AddModelError("", error);
             }
             return View(model);
         }
         protected bool ValidateChildVacationEditModel(ChildVacationEditModel model, UploadFileDto fileDto)
         {
             UserRole role = AuthenticationService.CurrentUser.UserRole;
             if (model.Id > 0 && fileDto == null)
             {
                 int attachmentCount = RequestBl.GetAttachmentsCount(model.Id, RequestAttachmentTypeEnum.ChildVacation);
                 if (attachmentCount <= 0)
                 {
                     if ((role == UserRole.Employee && model.IsApprovedByUser) ||
                         (role == UserRole.Manager && model.IsApprovedByManager) ||
                         (role == UserRole.PersonnelManager && model.IsApprovedByPersonnelManager))
                     {

                         ModelState.AddModelError(string.Empty,
                                                  "Заявка не может быть согласована без прикрепленого документа.");
                         if (role == UserRole.Employee && model.IsApprovedByUser)
                         {
                             ModelState.Remove("IsApprovedByUser");
                             model.IsApprovedByUser = false;
                         }
                         if (role == UserRole.Manager && model.IsApprovedByManager)
                         {
                             ModelState.Remove("IsApprovedByManager");
                             model.IsApprovedByManager = false;
                         }
                         if (role == UserRole.PersonnelManager && model.IsApprovedByPersonnelManager)
                         {
                             ModelState.Remove("IsApprovedByPersonnelManager");
                             model.IsApprovedByPersonnelManager = false;
                         }
                     }
                 }
             }
             if (model.BeginDate.HasValue && model.EndDate.HasValue)
             {
                 if (model.BeginDate > model.EndDate)
                     ModelState.AddModelError("BeginDate", "Дата начала отпуска не может превышать дату окончания отпуска.");
                 else if (!model.IsDelete)
                 {
                     /*int requestCount = RequestBl.GetOtherRequestCountsForUserAndDates
                         (model.BeginDate.Value, model.EndDate.Value,
                         model.UserId, model.Id,true);
                     if (requestCount > 0)
                         ModelState.AddModelError("BeginDate","Для данного пользователя существуют другие заявки в указанном интервале дат.");*/
                     if (model.IsApproved)
                     {
                         DateTime beginDate = model.BeginDate.Value;
                         DateTime current = DateTime.Today;
                         DateTime monthBegin = new DateTime(current.Year, current.Month, 1);
                         bool isValid = true;
                         if ((current.Day != 1) && monthBegin > beginDate)
                         {
                             isValid = RequestBl.HaveAbsencesForPeriod(model.BeginDate.Value,
                                                                       model.EndDate.Value, model.UserId,
                                                                       AuthenticationService.CurrentUser.Id, role);
                         }
                         if ((current.Day == 1) && monthBegin.AddMonths(-1) > beginDate)
                         {
                             isValid = RequestBl.HaveAbsencesForPeriod(model.BeginDate.Value,
                                                                       model.EndDate.Value, model.UserId,
                                                                       AuthenticationService.CurrentUser.Id, role);
                         }
                         if (!isValid)
                         {
                             Log.InfoFormat("Absence not found for child vacation {0}", model.Id);
                             ModelState.AddModelError(string.Empty,
                         "Период, указанный в заявке,не соответствует данным по неявкам в табеле.Вы не можете согласовать эту заявку.");
                         }
                     }
                 }
             }
             if (model.PaidToDate.HasValue && model.EndDate.HasValue && model.EndDate.Value < model.PaidToDate.Value)
                 ModelState.AddModelError("PaidToDate","Поле 'Выплачивать по' не должно быть больше поля 'Дата окончания'.");
             if (model.PaidToDate1.HasValue && model.EndDate.HasValue && model.EndDate.Value < model.PaidToDate1.Value)
                 ModelState.AddModelError("PaidToDate1", "Поле 'Выплачивать по' не должно быть больше поля 'Дата окончания'.");

             if (model.IsPersonnelFieldsEditable)
             {
                 if (!string.IsNullOrEmpty(model.ChildrenCount))
                 {
                     int childrenCount;
                     if (!Int32.TryParse(model.ChildrenCount, out childrenCount))
                         ModelState.AddModelError("ChildrenCount", "Неправильно указано количество детей.");
                     else if (childrenCount <= 0)
                         ModelState.AddModelError("ChildrenCount", "Количество детей должно быть целым положительным числом.");
                 }
             }
             //CheckBeginDate(model);
             return ModelState.IsValid;
         }
         protected void CorrectDropdowns(ChildVacationEditModel model)
         {
             if (!model.IsPersonnelFieldsEditable)
             {
                 if (!model.IsFirstChild && model.IsFirstChildHidden)
                 {
                     if (ModelState.ContainsKey("IsFirstChild"))
                         ModelState.Remove("IsFirstChild");
                     model.IsFirstChild = model.IsFirstChildHidden;
                 }
                 if (!model.IsFreeRate && model.IsFreeRateHidden)
                 {
                     if (ModelState.ContainsKey("IsFreeRate"))
                         ModelState.Remove("IsFreeRate");
                     model.IsFreeRate = model.IsFreeRateHidden;
                 }
                 if (!model.IsPreviousPaymentCounted && model.IsPreviousPaymentCountedHidden)
                 {
                     if (ModelState.ContainsKey("IsPreviousPaymentCounted"))
                         ModelState.Remove("IsPreviousPaymentCounted");
                     model.IsPreviousPaymentCounted = model.IsPreviousPaymentCountedHidden;
                 }
             }
         }
         protected void CorrectCheckBox()
         {
             
         }

  
         #endregion

         #region AccessGroupsList

         [HttpGet]
         [ReportAuthorize(UserRole.OutsourcingManager | UserRole.Estimator | UserRole.ConsultantPersonnel | UserRole.PersonnelManager | UserRole.Manager)]
         public ActionResult AccessGroupsList()
         {
             AccessGroupsListModel model = RequestBl.GetAccessGroupsListModel();
             return View(model);
         }
         [HttpPost]
         [ReportAuthorize(UserRole.OutsourcingManager | UserRole.Estimator | UserRole.ConsultantPersonnel | UserRole.PersonnelManager | UserRole.Manager)]
         public ActionResult AccessGroupsList(AccessGroupsListModel model)
         {
             model = RequestBl.SetAccessGroupsListModel(model);
             return View(model);
         }
         #endregion


         #region Comments
         [HttpGet]
         public ActionResult RenderComments(int id, int typeId, string addCommentText = null, bool hasParent = false)
         {
             //IContractRequest bo = Ioc.Resolve<IContractRequest>();
             RequestCommentsModel model = RequestBl.GetCommentsModel(id, typeId, null, hasParent);
             return PartialView("RequestCommentPartial", model);
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
                 string error = "Ошибка при загрузке данных: " + ex.GetBaseException().Message;
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
                     error = "Комментарий - обязательное поле";
                 }
                 else if (comment.Trim().Length > MaxCommentLength)
                 {
                     error = string.Format("Длина поля 'Комментарий' не может превышать {0} символов.", MaxCommentLength);
                 }
                 else
                 {
                     var model = new SaveCommentModel
                     {
                         DocumentId = id,
                         TypeId = typeId,
                         Comment = comment.Trim(),
                     };
                     saveResult = RequestBl.SaveComment(model);
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

         #region Attachment
         public FileContentResult ViewAttachment(int id/*,int type*/)
         {
             try
             {
                 AttachmentModel model = RequestBl.GetFileContext(id/*,type*/);
                 return File(model.Context, model.ContextType, model.FileName);
             }
             catch (Exception ex)
             {
                 Log.Error("Error on ViewAttachment:", ex);
                 throw;
             }
         }
         //protected UploadFilesDto GetFilesContexts()
         //{
         //    UploadFilesDto dto = new UploadFilesDto(); 
         //    if (Request.Files.Count == 0)
         //        return dto;
             
         //    dto.attachment = GetFileContext("file");
         //    dto.penAttachment = GetFileContext("filePen");
         //    dto.innAttachment = GetFileContext("fileInn");
         //    dto.ndflAttachment = GetFileContext("fileNdfl");
         //    return dto;
         //}
         [HttpGet]
         public ActionResult RenderAttachments(int id, int typeId)
         {
             //IContractRequest bo = Ioc.Resolve<IContractRequest>();
             RequestAttachmentsModel model = RequestBl.GetAttachmentsModel(id, (RequestAttachmentTypeEnum)typeId);
             return PartialView("RequestAttachmentsPartial", model);
         }
         [HttpGet]
         public ActionResult AddAttachmentDialog(int id, int typeId)
         {
             try
             {
                 AddAttachmentModel model = new AddAttachmentModel { DocumentId = id };
                 return PartialView(model);
             }
             catch (Exception ex)
             {
                 Log.Error("Exception", ex);
                 string error = "Ошибка при загрузке данных: " + ex.GetBaseException().Message;
                 return PartialView("DialogError", new DialogErrorModel { Error = error });
             }
         }
         /*protected UploadFileDto GetFileContext()
         {
             if (Request.Files.Count == 0)
                 return null;
             string file = Request.Files.GetKey(0);
             return GetFileContext(file);
         }*/
         [HttpPost]
         public ContentResult SaveAttachment(int id, string description, string qqFile)
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
                     var model = new SaveAttacmentModel
                     {
                         EntityId = id,
                         EntityTypeId = RequestAttachmentTypeEnum.Employment,
                         Description = description.Trim(),
                         FileDto = new UploadFileDto
                                       {
                                           Context = bytes,
                                           FileName = qqFile,
                                           //ContextType = Request.Content,
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
                     DeleteAttacmentModel model = new DeleteAttacmentModel{Id = id};
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
 
         /*protected UploadFileDto GetFileContext(string file)
         {
             //if (Request.Files.Count == 0)
             //    return null;
             //string file = Request.Files.GetKey(0);
             HttpPostedFileBase hpf = Request.Files[file];
             if ((hpf == null) || (hpf.ContentLength == 0))
                 return null;
             if (hpf.ContentLength > MaxFileSize)
             {
                 ModelState.AddModelError("", string.Format("Размер прикрепленного файла не может превышать {0} Мб.", MaxFileSize/(1024*1024)));
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
         protected byte[] GetFileData(HttpPostedFileBase file)
         {
             var length = file.ContentLength;
             var fileContent = new byte[length];
             file.InputStream.Read(fileContent, 0, length);
             return fileContent;
         }*/
           #endregion

         #region Print
         [HttpGet]
         public FileContentResult GetPrintForm(int id, int typeId)
         {
            try
            {
                AttachmentModel model = RequestBl.GetPrintFormFileContext(id,(RequestPrintFormTypeEnum)typeId);
                return File(model.Context, model.ContextType, model.FileName);
            }
            catch (Exception ex)
            {
                Log.Error("Error on GetPrintForm:", ex);
                throw;
            }
         }

         //  

         [HttpGet]
         public ActionResult RenderToPdf(string urlTemplate, DateTime? beginDate, DateTime? endDate, int? departmentId,
                     int? requestStatusId, int? typeId, string userName, int? sortBy, bool? sortDescending)
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

                 var arguments = new StringBuilder();

                 var cookieName = FormsAuthentication.FormsCookieName;
                 var authCookie = Request.Cookies[cookieName];
                 if (authCookie == null || authCookie.Value == null)
                     throw new ArgumentException("Ошибка авторизации.");
                 arguments.AppendFormat("{0} --cookie {1} {2}",
                     GetConverterCommandParam(urlTemplate, beginDate, endDate, departmentId,
                     requestStatusId, typeId, userName, sortBy, sortDescending)
                     , cookieName, authCookie.Value);
                 
                 arguments.AppendFormat(" \"{0}\"", filePath);
                 var serverSideProcess = new Process
                 {
                     StartInfo =
                     {
                         FileName = ConfigurationManager.AppSettings["PdfConverterCommandLineTemplate"],
                         Arguments = arguments.ToString(),
                         UseShellExecute = true
                     },
                     EnableRaisingEvents = true
                 };
                 serverSideProcess.Start();
                 serverSideProcess.WaitForExit();
                 return GetFile(filePath, fileName, @"application/pdf");
             }
             catch (Exception ex)
             {
                 Log.Error("Exception on RenderToPdf", ex);
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

         protected string GetConverterCommandParam(string urlTemplate, DateTime? beginDate, DateTime? endDate, int? departmentId,
                    int? requestStatusId, int? typeId, string userName, int? sortBy, bool? sortDescending)
         {
             var localhostUrl = ConfigurationManager.AppSettings["localhost"];
             string args = @"";
             if (beginDate.HasValue)
                 args += string.Format("beginDate={0}&", beginDate.Value.ToShortDateString());
             if (endDate.HasValue)
                 args += string.Format("endDate={0}&", endDate.Value.ToShortDateString());
             if (departmentId.HasValue)
                 args += string.Format("departmentId={0}&", departmentId.Value);
             if (requestStatusId.HasValue)
                 args += string.Format("requestStatusId={0}&", requestStatusId.Value);
             if (typeId.HasValue)
                 args += string.Format("typeId={0}&", typeId.Value);
             if (!string.IsNullOrEmpty(userName))
                 args += string.Format("userName={0}&", Server.UrlEncode(userName));
             if (sortBy.HasValue)
                 args += string.Format("sortBy={0}&", sortBy.Value);
             if (sortDescending.HasValue)
                 args += string.Format("sortDescending={0}&", sortDescending.Value);
             if (!string.IsNullOrEmpty(args))
                 args = args.Substring(0, args.Length - 1);
             args = "?" + args;

             return !string.IsNullOrEmpty(localhostUrl)
                        ? string.Format(@"{0}/{1}{2}", localhostUrl, urlTemplate, args)
                        : Url.Content(string.Format(@"{0}{1}", urlTemplate, args));
         }

         protected ActionResult GetFile(string filePath, string fileName, string contentType)
         {
             byte[] value;
             using (FileStream stream = System.IO.File.Open(filePath, FileMode.Open))
             {
                 value = new byte[stream.Length];
                 stream.Read(value, 0, (int)stream.Length);
             }
             const string userFileName = "PrintOut.pdf";
             //const string contentType = "application/pdf";
             Response.Clear();
             if (Request.Browser.Browser == "IE")
             {
                 string attachment = String.Format("attachment; filename=\"{0}\"", Server.UrlPathEncode(userFileName));
                 Response.AddHeader("Content-Disposition", attachment);
             }
             else
                 Response.AddHeader("Content-Disposition", "attachment; filename=\"" + userFileName + "\"");

             Response.ContentType = contentType;
             Response.Charset = "utf-8";
             Response.HeaderEncoding = Encoding.UTF8;
             Response.ContentEncoding = Encoding.UTF8;
             Response.BinaryWrite(value);
             Response.End();
             return null;
         }

         [HttpGet]
         public ActionResult PrintDismissalList(string beginDate, string endDate, int? departmentId,
                     int? requestStatusId, int? typeId, string userName, int? sortBy, bool? sortDescending)
         {
             DismissalListModel model = new DismissalListModel
             {
                 BeginDate = parseDateTime(beginDate),
                 EndDate = parseDateTime(endDate),
                 DepartmentId = departmentId.HasValue ? departmentId.Value : 0,
                 StatusId = requestStatusId.HasValue ? requestStatusId.Value : 0,
                 TypeId = typeId.HasValue ? typeId.Value : 0,
                 UserName = string.IsNullOrEmpty(userName) ? string.Empty : Server.UrlDecode(userName),
                 SortBy = sortBy.HasValue ? sortBy.Value : 0,
                 SortDescending = sortDescending.HasValue ? sortDescending.Value : new bool?(),
             };
             RequestBl.SetDismissalListModel(model, !ValidateModel(model));
             return View(model);
         }

         protected DateTime? parseDateTime(string value)
         {
             if (string.IsNullOrEmpty(value))
                 return new DateTime?();
             DateTime result;
             if (!DateTime.TryParse(value, out result))
                 return new DateTime?();
             return result;
         }

         /*[HttpGet]
         public ActionResult VacationPrint(int id)
         {
             VacationPrintModel model = RequestBl.GetVacationPrintModel(id);
             return View(model);
         }
         [HttpGet]
         public ActionResult PrintForm(int id,int typeId)
         {
             //int? userId = new int?();
             switch((RequestTypeEnum)typeId)
             {
                 case RequestTypeEnum.Vacation:
                     return RedirectToAction("VacationPrint", new  RouteValueDictionary
                                                                    {
                                                                        {"id", id},
                                                                       // {"userId",6}
                                                                     });
                 default:
                     throw new ArgumentException(string.Format("Неизвестный тип формы {0}",typeId));
             }
         }
         
         [HttpGet]
         public ActionResult RenderToPdf(int id,int typeId)
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
                 argumrnts.AppendFormat("{0} --cookie {1} {2}", GetConverterCommandParam(id, typeId), cookieName, authCookie.Value);
                 //argumrnts.AppendFormat("\"{0}\"", GetConverterCommandParam(id));
                 argumrnts.AppendFormat(" \"{0}\"", filePath);
                 var serverSideProcess = new Process
                 {
                     StartInfo =
                     {
                         FileName = ConfigurationManager.AppSettings["PdfConverterCommandLineTemplate"],
                         Arguments = argumrnts.ToString(),
                         UseShellExecute = true
                     },
                     EnableRaisingEvents = true
                 };
                 serverSideProcess.Start();
                 serverSideProcess.WaitForExit();
                 return GetFile(filePath, fileName, @"application/pdf");
             }
             catch (Exception ex)
             {
                 Log.Error("Exception on RenderToPdf", ex);
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
        
         protected string GetConverterCommandParam(int formId, int typeId)
         {
             //if (!formId.HasValue && !typeId.HasValue)
             //    throw new ArgumentException("Неправильный вызов функции  GetConverterCommandParam.");
             var localhostUrl = ConfigurationManager.AppSettings["localhost"];
             const string urlTemplate = "UserRequest/PrintForm";
             return !string.IsNullOrEmpty(localhostUrl)
                        ? string.Format("{0}/{1}/{2}?typeId={3}", localhostUrl, urlTemplate, formId, typeId)
                        : Url.Content(string.Format("{0}/{1}?typeId={2}", urlTemplate, formId, typeId));
             //!string.IsNullOrEmpty(localhostUrl) ?
             //formId.HasValue ?
             //string.Format("{0}/{1}/{2}", localhostUrl, urlTemplate, formId.Value) :
             //string.Format("{0}/{1}?typeId={2}", localhostUrl, urlTemplate, typeId.Value)
             //:
             //formId.HasValue ?
             //Url.Content(string.Format("{0}/{1}", urlTemplate, formId.Value)) :
             //Url.Content(string.Format("{0}?typeId={1}", urlTemplate, typeId.Value));
         }
         protected ActionResult GetFile(string filePath, string fileName, string contentType)
         {
             byte[] value;
             using (FileStream stream = System.IO.File.Open(filePath, FileMode.Open))
             {
                 value = new byte[stream.Length];
                 stream.Read(value, 0, (int)stream.Length);
             }

             //const string contentType = "application/pdf";
             Response.Clear();
             if (Request.Browser.Browser == "IE")
             {
                 string attachment = String.Format("attachment; filename=\"{0}\"", Server.UrlPathEncode(fileName));
                 Response.AddHeader("Content-Disposition", attachment);
             }
             else
                 Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");

             Response.ContentType = contentType;
             Response.Charset = "utf-8";
             Response.HeaderEncoding = Encoding.UTF8;
             Response.ContentEncoding = Encoding.UTF8;
             Response.BinaryWrite(value);
             Response.End();
             return null;
         }
         [HttpGet]
         public ActionResult PrintVacationOrder(int id)
         {
             string filePath = null;
             string tempTemplatePath = null;
             try
             {
                 string folderPath = ConfigurationManager.AppSettings["PresentationFolderPath"];
                 const string templateName = @"vacationtemplate.docx";
                 string  fileName = string.Format("{0}.docx", Guid.NewGuid());
                 string  tempTemplateName = string.Format("{0}.docx", Guid.NewGuid());

                 folderPath = HttpContext.Server.MapPath(folderPath);
                 if (!Directory.Exists(folderPath))
                     Directory.CreateDirectory(folderPath);
                 string templatePath = Path.Combine(folderPath, templateName);
                 filePath = Path.Combine(folderPath, fileName);
                 tempTemplatePath = Path.Combine(folderPath, tempTemplateName);
                 System.IO.File.Copy(templatePath,tempTemplatePath,true);
                 RequestBl.CreateVacationOrder(id, tempTemplatePath, filePath);

                  
             ////заменить на реальный запрос к базе
             //    //DataTable dt = new DataTable();

             //    //dt.Columns.Add("firstname");
             //    //dt.Columns.Add("lastname");
             //    //dt.Columns.Add("gender");
             //    //dt.Columns.Add("debt", Type.GetType("System.Decimal"));
             //    //dt.Rows.Add(new object[] { "Ivan", "Ivanov", "M", 123500 });
             //    //dt.Rows.Add(new object[4] { "Carmen", "Electra", "F", 100 });
             //    //dt.Rows.Add(new object[4] { "Jhon", "Smith", "M", 5005 });
             //    //dt.Rows.Add(new object[4] { "Monica", "Bellucci", "F", 123 });

             //    //Объекты для работы с вордаом
             //    //заглушка для опциональных аргументов
             //    object oMissing = Missing.Value;
             //    //разделитель страниц http://msdn.microsoft.com/en-us/library/bb213704%28office.12%29.aspx
             //    object pageBreak = WdBreakType.wdPageBreak;
             //    //не сохранять изменения
             //    object noSave = WdSaveOptions.wdDoNotSaveChanges;


             //    //путь к шаблону
             //    //object template = @"template.doc";

             //    //куда сохранять полученный документ
             //    //object destination = @"destination.doc";

             //    //запускаем Word
             //    _Application word = new Application {Visible = true};

             //    //word.Options.DefaultFilePath[WdDefaultFilePath.wdUserTemplatesPath] = folderPath;
             //    //Можем сделать его видимым и смотреть как скачут слова, абзацы и страницы

             //    //Создаем временный документ, в котором будем заменять ключевые слова на наши
             //    _Document sdoc = word.Documents.Add(ref templatePath, ref oMissing, ref oMissing, ref oMissing);

             //    //загружаем ключевые слова
             //    string[] keyWords = { "FNAME", "LNAME", "DEBT", "MR" };

             //    //Ищем позиции ключевых слов в документе и добавляем в список
             //    List<keyWordEntry> keyWordEntries = new List<keyWordEntry>();
             //    for (int i = 0; i < sdoc.Words.Count; i++)
             //    {
             //        foreach (string keyWord in keyWords)
             //        {
             //            if (sdoc.Words[i + 1].Text.Trim() == keyWord) //не забываем, что ворд считает с единицы, а не нуля
             //            {
             //                keyWordEntries.Add(new keyWordEntry(keyWord, i + 1, sdoc.Words[i + 1].Text.Remove(0, keyWord.Length)));
             //            }
             //        }
             //    }


             //    //Создаем документ назначения, на основе шаблона, чтобы сохранилась разметка страницы, стили, колонтитулы и т.п.
             //    _Document ddoc = word.Documents.Add(ref templatePath, ref oMissing, ref oMissing, ref oMissing);
             //    //Удаляем из него все тексты картинки и т.п.
             //    ddoc.Range(ref oMissing, ref oMissing).Delete(ref oMissing, ref oMissing);

             //    int rowCount = dt.Rows.Count;

             //    //Размечаем документ по количеству записей
             //    for (int i = 0; i < rowCount; i++)
             //    {
             //        ddoc.Range(ref oMissing, ref oMissing).InsertParagraphAfter();
             //    }

             //    //заполняем документ с конца
             //    for (int i = rowCount; i > 0; i--)
             //    {
             //        if (i < rowCount)
             //        {
             //            ddoc.Paragraphs[i].Range.InsertParagraphAfter();
             //            ddoc.Paragraphs[i + 1].Range.InsertBreak(ref pageBreak);
             //        }
             //        //подставляем слова во временный документ
             //        foreach (keyWordEntry ke in keyWordEntries)
             //        {
             //            string replaceWith;
             //            switch (ke.keyword)
             //            {
             //                case "FNAME":
             //                    replaceWith = dt.Rows[i - 1]["firstname"] + ke.spacesAfter;
             //                    break;
             //                case "LNAME":
             //                    replaceWith = dt.Rows[i - 1]["lastname"] + ke.spacesAfter;
             //                    break;
             //                case "DEBT":
             //                    replaceWith = dt.Rows[i - 1]["debt"] + ke.spacesAfter;
             //                    break;
             //                case "MR":
             //                    if ((string) dt.Rows[i - 1]["gender"] == "M")
             //                    {
             //                        replaceWith = "Mr" + ke.spacesAfter;
             //                    }
             //                    else
             //                    {
             //                        replaceWith = "Mrs" + ke.spacesAfter;
             //                    }
             //                    break;
             //                default:
             //                    replaceWith = ke.keyword + ke.spacesAfter;
             //                    break;
             //            }
             //            sdoc.Words[ke.position].Text = replaceWith;
             //        }
             //        sdoc.Range(ref oMissing, ref oMissing).Copy();
             //        ddoc.Paragraphs[i].Range.Paste();
             //    }

             //    //закрываем временный документ без сохранения
             //    sdoc.Close(ref noSave, ref oMissing, ref oMissing);
             //    //сахраняем полученный документ
             //    ddoc.SaveAs(ref filePath, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
             //    //закрываем полученный документ
             //    ddoc.Close(ref oMissing, ref oMissing, ref oMissing);
             //    //завершаем наш процесс ворда
             //    word.Quit(ref oMissing, ref oMissing, ref oMissing);
                 return GetFile(filePath, fileName, @"application/word");

             }
             catch (Exception ex)
             {
                 Log.Error("Exception on TestDocInsert",ex);
                 throw;
             }
             finally
             {
                 DeleteFile(filePath);
                 DeleteFile(tempTemplatePath);    
             }
         }
         public static void DeleteFile(string strFilePath)
         {
             if (!string.IsNullOrEmpty(strFilePath) && System.IO.File.Exists(strFilePath))
             {
                 try
                 {
                     System.IO.File.Delete(strFilePath);
                 }
                 catch (Exception ex)
                 {
                     Log.Warn(string.Format("Exception on delete file {0}", strFilePath), ex);
                 }
             }
         }*/
         #endregion

         #region Misc
         protected void CheckBeginDate(ICheckForEntityBeginDate model)
         {
            CheckRequestDate(model.IsDelete,model.BeginDate);
         }
         protected void CheckEndDate(ICheckForEntityEndDate model)
         {
            CheckRequestDate(model.IsDelete, model.EndDate);
         }
        protected void CheckRequestDate(bool isDelete,DateTime? date)
        {
            if (isDelete || !date.HasValue)
                return;
            DateTime beginDate = date.Value;
            DateTime current = DateTime.Today;
            int limitDate = AuthenticationService.CurrentUser.UserRole == UserRole.PersonnelManager ? 5 : (AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing ? 30 : 1);
            DateTime monthBegin = new DateTime(current.Year, current.Month, 1);
            if ((current.Day > limitDate) && monthBegin > beginDate)
            {
                ModelState.AddModelError(string.Empty, "Создание/изменение заявки в прошлом запрещено .");
                return;
            }
            if ((current.Day <= limitDate) && monthBegin.AddMonths(-1) > beginDate)
            {
                ModelState.AddModelError(string.Empty, "Создание/изменение заявки в прошлом запрещено .");
                return;
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
                     Error = string.Format("Ошибка: {0}",error),
                     Children = new List<IdNameDto>()
                 };
             }
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             var jsonString = jsonSerializer.Serialize(model);
             return Content(jsonString);
         }


         [HttpGet]
         public ActionResult AcceptRequests(int? month, int? year)
         {
             if (!month.HasValue)
                 month = DateTime.Today.Month;
             if (!year.HasValue)
                 year = DateTime.Today.Year;
             AcceptRequestsModel model = new AcceptRequestsModel
             {
                 Month = month.Value,
                 Year = year.Value,
             };
             RequestBl.GetAcceptRequestsModel(model);
             return View(model);
         }
         [HttpPost]
         public ActionResult AcceptRequests(AcceptRequestsModel model)
         {
             RequestBl.SetAcceptDate(model);
             if(!string.IsNullOrEmpty(model.Error))
                 ModelState.AddModelError(string.Empty,model.Error);
             return View(model);
         }

         [HttpGet]
         public ActionResult ConstantList(int? year)
         {
             if (!year.HasValue)
                 year = DateTime.Today.Year;
             ConstantListModel model = new ConstantListModel
             {
                 Year = year.Value,
             };
             RequestBl.GetConstantListModel(model);
             return View(model);
         }
         [HttpGet]
         public ActionResult ConstantEdit(int? entityId,int? month,int? year)
         {
             ConstantEditModel model = new ConstantEditModel
                                           {
                                               Id = entityId.HasValue ? entityId.Value : 0,
                                               Month = month.HasValue?month.Value:0,
                                               Year = year.HasValue?year.Value:0,
                                           };
             RequestBl.GetConstantEditModel(model);
             return View(model);
         }
         [HttpPost]
         public ActionResult ConstantEdit(ConstantEditModel model)
         {
             if (!ValidateConstantEditModel(model))
             {
                 RequestBl.ReloadDictionariesToModel(model);
                 return View(model);
             }
             string error;
             if (!RequestBl.SaveConstantEditModel(model, out error))
             {

                 if (model.ReloadPage)
                 {
                     ModelState.Clear();
                     if (!string.IsNullOrEmpty(error))
                         ModelState.AddModelError("", error);
                     RequestBl.GetConstantEditModel(model);
                     return View(model);
                 }
                 if (!string.IsNullOrEmpty(error))
                     ModelState.AddModelError("", error);
             }
             return View(model);

         }
         protected bool ValidateConstantEditModel(ConstantEditModel model)
         {
            if(!string.IsNullOrEmpty(model.Days))
            {
                int days;
                if (!Int32.TryParse(model.Days, out days))
                    ModelState.AddModelError("Days", "Поле <Баланс мес.(дней)> должно быть целым числом.");
                else if (days < 1 || days > 31)
                    ModelState.AddModelError("Days", "Поле <Баланс мес.(дней)> должно быть целым числом от 1 до 31.");
                
            }
            if (!string.IsNullOrEmpty(model.Hours))
            {
                int hours;
                if (!Int32.TryParse(model.Hours, out hours))
                    ModelState.AddModelError("Hours", "Поле <Баланс мес.(часов)> должно быть целым числом.");
                else if (hours < 1 || hours > 248)
                    ModelState.AddModelError("Hours", "Поле <Баланс мес.(часов)> должно быть целым числом от 1 до 248.");
            }
            return ModelState.IsValid;
         }
        /*[HttpGet]
        public ActionResult TemplatesList()
        {
            return View(new TemplatesListModel());
        }*/
         #endregion

         public ViewResult TerrapointDepartment()
         {
             return View();
         }
         public ViewResult DepartmentTerrapoint()
         {
             return View();
         }
         public ContentResult GetTP_D_list()
         {
             var content = RequestBl.GetTP_D_list();
             return  Content( Newtonsoft.Json.JsonConvert.SerializeObject(content));
         }
         public ContentResult GetD_TP_list()
         {
             var content = RequestBl.GetD_TP_list();
             return Content(Newtonsoft.Json.JsonConvert.SerializeObject(content));
         }
        #region VacationReturn
        [HttpGet]
        public ActionResult CreateVacationReturn()
        {
            var model = RequestBl.GetCreateModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateVacationReturn(int UserId)
        {
            var model = RequestBl.GetNewVacationReturnViewModel(UserId);
            return View("VacationReturnEdit",model);
        }
        [HttpGet]
        public ActionResult VacationReturnEdit(int id)
        {
            var model = RequestBl.GetVacationReturnEditModel(id);
            
            return View(model.Value);
        }
        [HttpPost]
        public ActionResult VacationReturnEdit(VacationReturnViewModel model)
        {
            ModelState.Clear();
            model.FileDto = GetFileContext(Request, ModelState, "File");
            var result = RequestBl.SaveVacationReturnEditModel(model);
            result
                .OnSuccess(x => model = x.Value)
                .OnError(x => { RequestBl.GetVacationReturnEditModel(model.Id).OnSuccess(y => model = y.Value); ModelState.AddModelError("SaveError", x.Message); });
            return View(model);
        }
        [HttpGet]
        public ActionResult VacationReturnList()
        {
            return View(RequestBl.GetVacationReturnListModel());                 
        }
        [HttpPost]
        public ContentResult VacationReturnList(VacationReturnListModel model)
        {
            Console.Write(Request.ToString());
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(RequestBl.GetDocuments(model)));
        }
        #endregion
    }        
}
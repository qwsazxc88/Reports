using System;
using System.IO;
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

namespace WebMvc.Controllers
{
    [ReportAuthorize(UserRole.Employee | UserRole.Manager | UserRole.PersonnelManager)]
    public class UserRequestController : BaseController
    {
        public const int MaxCommentLength = 256;
        public const int MaxFileSize = 2 * 1024 * 1024;

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
         [HttpGet]
         public ActionResult CreateRequest()
         {
             int? userId = new int?();
             CreateRequestModel model = RequestBl.GetCreateRequestModel(userId);
             return View(model);
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
                 default:
                     throw new ArgumentException("Неизвестный тип заявки");
             }
         }
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
             RequestBl.SetHolidayWorkListModel(model);
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
                 if (!string.IsNullOrEmpty(model.Rate))
                 {
                     int rate;
                     if (!Int32.TryParse(model.Rate, out rate))
                         ModelState.AddModelError("Rate", "Неправильное поле 'Часовая тарифная ставка'.");
                     else if (rate <= 0)
                         ModelState.AddModelError("Rate",
                                                  "Поле 'Часовая тарифная ставка' должно быть положительным числом.");
                 }
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
             RequestBl.SetSicklistListModel(model);
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
             if (!ValidateSicklistEditModel(model))
             {
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
         protected bool ValidateSicklistEditModel(SicklistEditModel model)
         {
             if (model.BeginDate.HasValue && model.EndDate.HasValue &&
                 model.BeginDate > model.EndDate)
                 ModelState.AddModelError("BeginDate", "Дата начала отпуска не может превышать дату окончания отпуска.");
             if (model.IsPersonnelFieldsEditable)
             {
                 if (string.IsNullOrEmpty(model.ExperienceYears) && string.IsNullOrEmpty(model.ExperienceYears))
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
                 model.TypeId = model.TypeIdHidden;
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
         public ActionResult AbsenceList()
         {
             AbsenceListModel model = RequestBl.GetAbsenceListModel();
             return View(model);
         }
         [HttpPost]
         public ActionResult AbsenceList(AbsenceListModel model)
         {
             RequestBl.SetAbsenceListModel(model);
             return View(model);
         }
         [HttpGet]
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
             return ModelState.IsValid;
         }
         #endregion
         #region Vacation
         [HttpGet]
         public ActionResult VacationList()
         {
             //int? userId = new int?();
             VacationListModel model = RequestBl.GetVacationListModel();
             return View(model);
         }
         [HttpPost]
         public ActionResult VacationList(VacationListModel model)
         {
             RequestBl.SetVacationListModel(model);
             return View(model);
         }
         [HttpGet]
         public ActionResult VacationEdit(int id,int userId)
         {
             //int? userId = new int?();
             VacationEditModel model = RequestBl.GetVacationEditModel(id,userId);
             return View(model);
         }
         [HttpPost]
         public ActionResult VacationEdit(VacationEditModel model)
         {
             CorrectCheckboxes(model);
             CorrectDropdowns(model);
             if (!ValidateVacationEditModel(model))
             {
                 RequestBl.ReloadDictionariesToModel(model);
                 return View(model);
             }

             string error;
             if(!RequestBl.SaveVacationEditModel(model,out error))
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
         protected bool ValidateVacationEditModel(VacationEditModel model)
         {
             if(model.BeginDate.HasValue && model.EndDate.HasValue &&
                 model.BeginDate > model.EndDate)
                 ModelState.AddModelError("BeginDate", "Дата начала отпуска не может превышать дату окончания отпуска.");
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
                 if (ModelState.ContainsKey("IsApprovedByManager"))
                     ModelState.Remove("IsApprovedByManager");
                 model.IsApprovedByManager = model.IsApprovedByManagerHidden;
             }
             if (!model.IsApprovedByPersonnelManagerEnable && model.IsApprovedByPersonnelManagerHidden)
             {
                 if (ModelState.ContainsKey("IsApprovedByPersonnelManager"))
                     ModelState.Remove("IsApprovedByPersonnelManager");
                 model.IsApprovedByPersonnelManager = model.IsApprovedByPersonnelManagerHidden;
             }
             if (!model.IsApprovedByUserEnable && model.IsApprovedByUserHidden)
             {
                 if (ModelState.ContainsKey("IsApprovedByUser"))
                     ModelState.Remove("IsApprovedByUser");
                 model.IsApprovedByUser = model.IsApprovedByUserHidden;
             }
             if (!model.IsPostedTo1CEnable && model.IsPostedTo1CHidden)
             {
                 if (ModelState.ContainsKey("IsPostedTo1C"))
                     ModelState.Remove("IsPostedTo1C");
                 model.IsPostedTo1C = model.IsPostedTo1CHidden;
             }
         }
         #endregion
         [HttpGet]
         public ActionResult RenderComments(int id,int typeId)
         {
             //IContractRequest bo = Ioc.Resolve<IContractRequest>();
             RequestCommentsModel model = RequestBl.GetCommentsModel(id,typeId);
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
         protected UploadFileDto GetFileContext()
         {
             if (Request.Files.Count == 0)
                 return null;
             string file = Request.Files.GetKey(0);
             HttpPostedFileBase hpf = Request.Files[file];
             if ((hpf == null) || (hpf.ContentLength == 0))
                 return null;
             if (hpf.ContentLength > MaxFileSize)
             {
                 ModelState.AddModelError("", string.Format("Размер прикрепленного файла > {0} байт.", MaxFileSize));
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
         }
     }
}
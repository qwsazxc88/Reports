using System;
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
                 default:
                     throw new ArgumentException("Неизвестный тип заявки");
             }
         }
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
         protected bool ValidateAbsenceEditModel(AbsenceEditModel model)
         {
             if (model.BeginDate.HasValue && model.EndDate.HasValue &&
                 model.BeginDate > model.EndDate)
                 ModelState.AddModelError("BeginDate", "Дата начала отпуска не может превышать дату окончания отпуска.");
             int dayCounts;
             if(!Int32.TryParse(model.DaysCount, out dayCounts))
                ModelState.AddModelError("DaysCount", "Количество дней (часов) должно быть числом.");
             else
             {
                 if (dayCounts <= 0)
                     ModelState.AddModelError("DaysCount", "Количество дней (часов) должно быть положительным числом.");
             }
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

         
     }
}
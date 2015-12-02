using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Reports.Core;
using Reports.Core.Dto;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;

namespace WebMvc.Controllers
{
    [Authorize]
    public class EmployeeController : BaseController
    {
        //public const int MaxFileSize = 2*1024*1024;
        protected IEmployeeBl employeeBl;
        public IEmployeeBl EmployeeBl
        {
            get
            {
                employeeBl = Ioc.Resolve<IEmployeeBl>();
                return Validate.Dependency(employeeBl);
            }
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DocumentList(int? ownerId,bool? viewHeader,
            bool? showNew,int? subtypeId)
        {
            EmployeeDocumentListModel model = EmployeeBl.GetModel(ownerId,viewHeader,showNew,subtypeId);
            return View(model);
        }
        [HttpGet]
        public ActionResult DocumentEdit(int id,int? ownerId)
        {
            if (!ownerId.HasValue)
                ownerId = AuthenticationService.CurrentUser.Id;
            EmployeeDocumentEditModel model = EmployeeBl.GetDocumentEditModel(id,ownerId.Value);
            return View(model);
        }
        [HttpPost]
        public ActionResult DocumentEdit(EmployeeDocumentEditModel model)
        {
            try
            {
                CorrectCheckboxes(model);
                UploadFileDto fileDto = GetFileContext();
                if (ModelState.IsValid)
                {
                    string error;
                    if (!EmployeeBl.SaveEditDocument(model, fileDto,out error))
                    {
                        if(!string.IsNullOrEmpty(error))
                            ModelState.AddModelError("",error);
                        if (model.ReloadPage)
                            return View(EmployeeBl.GetDocumentEditModel(model.DocumentId, model.UserId.Value));
                    }
                }
                else
                    EmployeeBl.SetStaticDataToModel(model);
                return View(model);

            }
            catch (Exception ex)
            {
                Log.Error("Error on DocumentEdit:", ex);                
                throw;
            }
        }
        [HttpPost]
        public ContentResult SendToBilling(int documentId)
        {
            bool saveResult;
            string error;
            try
            {
                var model = new SendToBillingModel
                    {
                        DocumentId = documentId,
                    };
                    saveResult = EmployeeBl.SendToBilling(model);
                    error = model.Error;
            }
            catch (Exception ex)
            {
                Log.Error("Exception on SendToBilling:", ex);
                error = "Исключение: " + ex.GetBaseException().Message;
                saveResult = false;
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(new SaveTypeResult { Error = error, Result = saveResult });
            return Content(jsonString);
        }


        protected void CorrectCheckboxes(EmployeeDocumentEditModel model)
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
            if (!model.IsApprovedByBudgetManagerEnable && model.IsApprovedByBudgetManagerHidden)
            {
                if (ModelState.ContainsKey("IsApprovedByBudgetManager"))
                    ModelState.Remove("IsApprovedByBudgetManager");
                model.IsApprovedByBudgetManager = model.IsApprovedByBudgetManagerHidden;
            }
            if (!model.IsApprovedByOutsorsingManagerEnable && model.IsApprovedByOutsorsingManagerHidden)
            {
                if (ModelState.ContainsKey("IsApprovedByOutsorsingManager"))
                    ModelState.Remove("IsApprovedByOutsorsingManager");
                model.IsApprovedByOutsorsingManager = model.IsApprovedByOutsorsingManagerHidden;
            }
        }

        /*protected UploadFileDto GetFileContext()
        {
            string file = Request.Files.GetKey(0);
            HttpPostedFileBase hpf = Request.Files[file];
            if ((hpf == null) || (hpf.ContentLength == 0))
                return null;
            if(hpf.ContentLength > MaxFileSize)
            {
                ModelState.AddModelError("", string.Format("Размер прикрепленного файла > {0} байт.",MaxFileSize));
                return null;
            }
            byte[] context = GetFileData(hpf);
            return new UploadFileDto
                       {
                           Context = context,
                           ContextType = hpf.ContentType,
                           FileName = hpf.FileName,
                       };
        }
        protected byte[] GetFileData(HttpPostedFileBase file)
        {
            var length = file.ContentLength;
            var fileContent = new byte[length];
            file.InputStream.Read(fileContent, 0, length);
            return fileContent;
        }*/
        public FileContentResult ViewAttachment(int id)
        {
            try
            {
                AttachmentModel model = EmployeeBl.GetFileContext(id);
                return File(model.Context, model.ContextType, model.FileName);
            }
            catch (Exception ex)
            {
                Log.Error("Error on ViewAttachment:", ex);
                throw;
            }
        }
        public ContentResult GetSubTypes(int typeId)
        {
            var result = EmployeeBl.GetSubTypes(typeId);
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(result);
            return Content(jsonString);
        }

        [HttpGet]
        public ActionResult RenderComments(int documentId)
        {
            //IContractRequest bo = Ioc.Resolve<IContractRequest>();
            DocumentCommentsModel model = EmployeeBl.GetCommentsModel(documentId);
            return PartialView("DocumentCommentPartial", model);
        }

        [HttpGet]
        public ActionResult AddCommentDialog(int documentId)
        {
            try
            {
                AddCommentModel model = new AddCommentModel { DocumentId = documentId };
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
        public ContentResult SaveComment(int documentId, string comment)
        {
            bool saveResult = false;
            string error;
            try
            {
                if (comment == null || string.IsNullOrEmpty(comment.Trim()))
                {
                    error = "Комментарий - обязательное поле";
                }
                else if(comment.Trim().Length > MaxCommentLength)
                {
                    error = string.Format("Длина поля 'Комментарий' не может превышать {0} символов.",MaxCommentLength);
                }
                else
                {
                    var model = new SaveCommentModel
                                    {
                                        DocumentId = documentId, 
                                        Comment = comment.Trim(),
                                    };
                    saveResult = EmployeeBl.SaveComment(model);
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


        [HttpGet]
        public ActionResult EmployeeList(int? currentPage)
        {
            EmployeeListModel model = new EmployeeListModel
                                          {
                                              CurrentPage = currentPage.HasValue?
                                                            currentPage.Value:1,
                                          };
            EmployeeBl.GetEmployeeListModel(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult EmployeeList(EmployeeListModel model)
        {
            EmployeeBl.GetEmployeeListModel(model);
            return View(model);
        }

        [HttpGet]
        public ActionResult TimesheetList(int managerId, int? month, int? year)
        {
            if (!month.HasValue)
                month = DateTime.Today.Month;
            if(!year.HasValue)
                year = DateTime.Today.Year;
            TimesheetListModel model = new TimesheetListModel
                {
                    ManagerId = managerId,
                    Month = month.Value,
                    Year = year.Value,
                    //DepartmentId = departmentId.HasValue? departmentId.Value:0,
                    //DepartmentName = departmentName,
                    //CurrentPage = currentPage.HasValue?currentPage.Value:0,
                    //UserName = userName,
                    //IsEditable = false,
                };
            EmployeeBl.SetupDepartment(model);
            EmployeeBl.GetTimesheetListModel(model);
            return View(model);
        }
        
        [HttpPost]
        public ActionResult TimesheetList(TimesheetListModel model)
        {
            if(model.IsSaveNeed)
            {
                if(!ValidateModel(model))
                {
                    EmployeeBl.SetListboxes(model);
                    return View(model);
                }

                model.IsSaveNeed = false;
                EmployeeBl.SaveGraphicsRecord(model);
            }
            EmployeeBl.GetTimesheetListModel(model);
            return View(model);
            //if(!ValidateModel(model))
            //{
            //    EmployeeBl.GetTimesheetListModel(model);
            //    return View(model);
            //}
            //CorrectHours(model);
            ////EmployeeBl.SetTimesheetsHours(model);
            //return View(model);
        }
        [HttpPost]
        public FileResult TimesheetListExcel(TimesheetListModel model)
        {            
            EmployeeBl.GetTimesheetListModel(model);
            string result = "";
            bool RowFirst = true;
            foreach (var dto in model.TimesheetDtos)
            {
                string row1 = "<tr><td>ФИО сотрудника</td><td>Календарные дни месяца</td>";
                string row2 = "<tr><td>" + dto.UserNameAndCode + "</td><td>Рабочие дни</td>";
                string row3 = "<tr><td>" + dto.UserNameAndCode + "</td><td>Рабочие часы</td>";
                string row4 = "<tr><td>" + dto.UserNameAndCode + "</td><td>Фактические часы</td>";
                foreach (var day in dto.Days)
                {
                    row1 += "<td>" + (day.isStatRecord ? day.StatCode : day.Number.ToString()) + "</td>";
                    row2 += "<td>" + day.Status + "</td>";
                    row3 += "<td>" + day.Hours + "</td>";
                    row4 += "<td>" + day.Graphic + "</td>";

                }
                row1 += "</tr>";
                row2 += "</tr>";
                row3 += "</tr>";
                row4 += "</tr>";
                result += RowFirst? row1:"" + row2 + row3 + row4;
                RowFirst = false;
            }
            result += "";
            return base.Excel(result);
            //if(!ValidateModel(model))
            //{
            //    EmployeeBl.GetTimesheetListModel(model);
            //    return View(model);
            //}
            //CorrectHours(model);
            ////EmployeeBl.SetTimesheetsHours(model);
            //return View(model);
        }

        /*protected void CorrectHours(TimesheetListModel model)
        {
            float hours = model.Hours;
            hours = (float)Math.Round(hours * 100) / 100;
            if (hours != model.Hours)
            {
                model.Hours = hours;
                if (ModelState.ContainsKey("Hours"))
                    ModelState.Remove("Hours");
            }
        }*/
        protected bool ValidateModel(TimesheetListModel model)
        {
            if(!ValidateTimeSheets(model.TimesheetDtos))
                ModelState.AddModelError(string.Empty, "Указаны недопустимые значения рабочего времени.Значения должны быть от 0 до 24 или пустыми.");
            return ModelState.IsValid;
        }
        protected bool ValidateTimeSheets(IList<TimesheetDto> dtos)
        {
            foreach (TimesheetDto dto in dtos)
            {
                foreach (TimesheetDayDto dayDto in dto.Days)
                {
                    if (!ValidateDay(dayDto))
                        return false;
                }
            }
            return true;
        }
        protected bool ValidateDay(TimesheetDayDto dto)
        {
            if (dto.isStatRecord)
                return true;
            string workHours = dto.Graphic;
            if (string.IsNullOrEmpty(workHours))
                return true;
            double value;
            if (Double.TryParse(workHours, out value) || Double.TryParse(workHours.Replace(".",","), out value))
            {
                dto.Graphic = (int)value == value 
                    ? ((int)value).ToString() 
                    : value.ToString("0.0");
                return true;
            }
            return false;
        }


        [HttpGet]
        public ActionResult TimesheetYearList(int managerId)
        {
            TimesheetYearListModel model = new TimesheetYearListModel
            {
                ManagerId = managerId,
            };
            EmployeeBl.SetupDepartment(model);
            SetInitialDates(model); 
            EmployeeBl.GetTimesheetListModel(model);
            return View(model);
        }
        public static void SetInitialDates(BeginEndCreateDate model)
        {
            DateTime today = DateTime.Today;
            model.BeginDate = new DateTime(today.Year, today.Month, 1);
            model.EndDate = new DateTime(model.BeginDate.Value.Year, model.BeginDate.Value.Month, 1).AddMonths(1).AddDays(-1); 
        }
        [HttpPost]
        public ActionResult TimesheetYearList(TimesheetYearListModel model)
        {
            if(!ValidateModel(model))
            {
                model.DatesPeriod = string.Empty;
                model.CurrentPage = 0;
                model.NumberOfPages = 0;
                model.TimesheetDtos = new List<TimesheetDto>();
                return View(model);
            }
            EmployeeBl.GetTimesheetListModel(model);
            return View(model);
        }
        protected bool ValidateModel(TimesheetYearListModel model)
        {
            DateTime today = DateTime.Today;
            if (!model.BeginDate.HasValue)
            {
                ModelState.Remove("BeginDate");
                model.BeginDate = model.EndDate.HasValue ? 
                    new DateTime(model.EndDate.Value.Year, model.EndDate.Value.Month, 1) : 
                    new DateTime(today.Year, today.Month, 1);
            }
            if (!model.EndDate.HasValue)
            {
                ModelState.Remove("EndDate");
                model.EndDate = new DateTime(model.BeginDate.Value.Year, model.BeginDate.Value.Month, 1).AddMonths(1).AddDays(-1);
            }
            if (model.BeginDate.Value > model.EndDate.Value)
                ModelState.AddModelError("BeginDate", "Дата в поле <Период с> не может быть больше даты в поле <по>.");
            return ModelState.IsValid;
        }

        protected void CorrectTimesheetEditCheckboxes(TimesheetEditModel model)
        {
            if (!model.IsNotApprovedByUserEnable && model.IsNotApprovedByUserHidden)
            {
                if (ModelState.ContainsKey("IsNotApprovedByUser"))
                    ModelState.Remove("IsNotApprovedByUser");
                model.IsNotApprovedByUser = model.IsNotApprovedByUserHidden;
            }
            if (!model.IsNotApprovedByPersonnelEnable && model.IsNotApprovedByPersonnelHidden)
            {
                if (ModelState.ContainsKey("IsNotApprovedByPersonnel"))
                    ModelState.Remove("IsNotApprovedByPersonnel");
                model.IsNotApprovedByPersonnel = model.IsNotApprovedByPersonnelHidden;
            }
        }




        [HttpGet]
        public ActionResult EditDayDialog(int Id,int statusId,string hours)
        {
            try
            {
                double modelHours;
                Double.TryParse(hours, out modelHours);
                EditDayModel model = new EditDayModel{Id = Id,StatusId = statusId,Hours = (float)modelHours};
                EmployeeBl.GetEditDayModel(model);
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.Error("Exception", ex);
                string error = "Ошибка при загрузке данных: " + ex.GetBaseException().Message;
                return PartialView("DialogError", new DialogErrorModel { Error = error });
            }
        }


        [HttpGet]
        public ActionResult EmployeeTimesheetList()
        {
            EmployeeTimesheetListModel model = EmployeeBl.GetEmployeeTimesheetListModel();
            return View(model);
        }

        //[HttpGet]
        //public ActionResult TimesheetTest()
        //{
        //    TimesheetModel model = new TimesheetModel
        //                               {
        //                                   TimesheetDtos = new List<TimesheetDto>
        //                                                       {
        //                                                           GetTestModel(1, 1, false),
        //                                                           GetTestModel(2, 2, false),
        //                                                       },
        //                                   TimesheetDto = GetTestModel(3, 3, true)
        //                               };

        //    return View(model);
        //}
        //[HttpPost]
        //public ActionResult TimesheetTest(TimesheetModel model)
        //{
        //    model.TimesheetDtos = new List<TimesheetDto>
        //                              {
        //                                  GetTestModel(1,1,false),
        //                                  GetTestModel(2,2,false),
        //                              };
        //    model.TimesheetDto = GetTestModel(3, 3, true);
        //    return View(model);
        //}
        //protected TimesheetDto GetTestModel(int id, int ownerId, bool isEditable)
        //{
        //    IList<IdNameDto> statuses = EmployeeBl.GetTimesheetStatusesList();
        //    TimesheetDto dto = new TimesheetDto
        //                           {
        //                               Id = id,
        //                               IsEditable = isEditable,
        //                               MonthAndYear = "Январь 2011",
        //                               OwnerId = ownerId,
        //                               //Statuses = EmployeeBl.GetTimesheetStatusesList(),
        //                               UserNameAndCode = "Иванов Иван Иванович, АБС001",
        //                           };
        //    IdNameDto holidayStatus = statuses.Where(x => x.Name == "Я").First();
        //    IList<TimesheetDayDto> days = new List<TimesheetDayDto>();
        //    for(var i=0;i<31;i++)
        //        days.Add(new TimesheetDayDto
        //                     {
        //                         Hours = 7.5f, 
        //                         Number = i + 1, 
        //                         Status = holidayStatus.Name, 
        //                         StatusId = holidayStatus.Id
        //                     });
        //    dto.Days = days;
        //    return dto;
        //}
    }
}

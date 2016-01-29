using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using Reports.Core;
using Reports.Core.Enum;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;
using WebMvc.Attributes;
using Reports.Core.Dto;
using Reports.Core.Domain;
using Reports.Core.Dao;
using System.Collections.Generic;
using System.Linq;



namespace WebMvc.Controllers
{
    [PreventSpamAttribute]
    [ReportAuthorize(UserRole.Accountant | UserRole.OutsourcingManager | UserRole.Estimator | UserRole.Admin)]
    public class DeductionController : BaseController
    {
        protected IRequestBl requestBl;
        protected const string emptyValue = ""; 
        public IRequestBl RequestBl
        {
            get
            {
                requestBl = Ioc.Resolve<IRequestBl>();
                return Validate.Dependency(requestBl);
            }
        }
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
        [HttpPost]
        [ReportAuthorize(UserRole.Accountant | UserRole.OutsourcingManager | UserRole.Estimator)]
        public ActionResult ChangeNotUseInAnalyticalStatement(int[] ids, bool[] notuse)
        {
            if (RequestBl.ChangeNotUseInAnalyticalStatement(ids, notuse)) return Json(new { Status = "Ok" });
            else return Json(new { Status = "Error", Message="При обновлении данных произошла ошибка" });
        }
        [HttpGet]
        public ActionResult DeductionEdit(int id)
        {
            DeductionEditModel model = RequestBl.GetDeductionEditModel(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult DeductionEditAdmin(int id)
        {
            DeductionEditModel model = RequestBl.GetDeductionEditModel(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult DeductionEditAdmin(DeductionEditModel model)
        {
            //ModelState.Clear();
            CorrectCheckboxes(model);
            //CorrectDropdowns(model);
            model.TypeId = model.TypeIdHidden;
            model.KindId = model.KindIdHidden;
            model.MonthId = model.MonthIdHidden;
            model.UserId = model.UserIdHidden;
            //
            RequestBl.SaveDeductionEditAdminModel(model);
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
            //чтобы письма рассылались только когда идет работа с рабочей базой
            bool EnableSendEmail = Request.Url.Port == 8002 || Request.Url.Port == 500 ? true : false;
            if (!RequestBl.SaveDeductionEditModel(model, EnableSendEmail, out error))
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
        [HttpGet]
        [ReportAuthorize(UserRole.Accountant )]
        public ActionResult DeductionImport()
        {
            var model=RequestBl.GetDeductionImportModel();
            return View(model);
        }
        [HttpPost]
        [ReportAuthorize(UserRole.Accountant )]
        public ActionResult DeductionImport(DeductionImportModel model)
        {
            model = RequestBl.GetDeductionImportModel(model);
            if (model.File != null && model.File.ContentLength > 0)
            {
                var fileName = Guid.NewGuid()+".input.csv";
                var path = Path.Combine(Server.MapPath("~/Files"), fileName);
                model.File.SaveAs(path);
                var Errors = new List<string>();
                bool isFileExist=false;
                model.Imported = RequestBl.ImportDeductionFromFile(ref path, ref Errors,ref isFileExist);
                if (isFileExist)
                {
                    ModelState.AddModelError("File", "Файл уже был загружен. Отображены результаты предидущей загрузки.");
                    new FileInfo(Path.Combine(Server.MapPath("~/Files"), fileName)).Delete();
                }
                model.Errors = Errors;
                ViewBag.ReportFile = "/Files/" + Path.GetFileName(path).Replace(".input.csv", ".report.txt");
            }
            else { ModelState.AddModelError("File", new Exception("Файл не выбран или пустой."));  }
            return View(model);
        }
        /// <summary>
        /// Автозаполнение фио в создании набора реквизитов.
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public ActionResult AutocompletePersonSearch(string term)
        {

            IList<IdNameDto> Persons = RequestBl.GetUserListForDeduction(term, 0);
            var PersonList = Persons.ToList().Select(a => new { label = a.Name, UserId = a.Id }).Distinct();

            return Json(PersonList, JsonRequestBehavior.AllowGet);
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
            if (!model.IsEditable)
            {
                //model.TypeId = model.TypeIdHidden;
                //model.KindId = model.KindIdHidden;
                //model.MonthId = model.MonthIdHidden;
                model.UserId = model.UserIdHidden;
            }
        }
        protected bool ValidateDeductionEditModel(DeductionEditModel model)
        {
            if (model.Id == 0)
            {
                if (model.UserId != 0)
                {
                    User DeductionUser = RequestBl.GetUser(model.UserId);
                    if ((DeductionUser.UserRole & UserRole.DismissedEmployee) > 0 && DeductionUser.DateRelease <= DateTime.Today.AddMonths(-3))
                    {
                        ModelState.AddModelError("Surname", "Нельзя создать заявку на удержание, так как выбранный сотрудник уволен более 3 месяцев назад!");
                    }
                }
                else
                {
                    ModelState.AddModelError("Surname", "Укажите сотрудника!");
                }
            }
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
            if (Convert.ToDateTime(model.DateEdited).Month == DateTime.Today.Month && Convert.ToDateTime(model.DateEdited).Year == DateTime.Today.Year)
            {
                if (model.TypeId != (int)DeductionTypeEnum.Deduction)
                {
                    if (!model.DismissalDate.HasValue)
                        ModelState.AddModelError("DismissalDate", "'Дата увольнения' - обязательное поле.");
                }
            }
            else
            {
                ModelState.AddModelError("DismissalDate", "Отклонение заявки в прошлом периоде запрещено!");
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

        [HttpGet]
        public ActionResult PrintDeductionList(string beginDate,string endDate, int? departmentId,
                    int? requestStatusId, int? typeId, string userName, int? sortBy, bool? sortDescending)
        {
            

            DeductionListModel model = new DeductionListModel
                                           {
                                               BeginDate = parseDateTime(beginDate),
                                               EndDate = parseDateTime(endDate),
                                               DepartmentId = departmentId.HasValue?departmentId.Value:0,
                                               RequestStatusId = requestStatusId.HasValue?requestStatusId.Value:0,
                                               TypeId = typeId.HasValue?typeId.Value:0,
                                               UserName = string.IsNullOrEmpty(userName)?string.Empty:Server.UrlDecode(userName),
                                               SortBy = sortBy.HasValue?sortBy.Value:0,
                                               SortDescending = sortDescending.HasValue?sortDescending.Value:new bool?(),
                                           };
            RequestBl.SetDeductionListModel(model, !ValidateModel(model));
            return View(model);
        }
        protected DateTime? parseDateTime(string value)
        {
            if(string.IsNullOrEmpty(value))
                return new DateTime?();
            DateTime result;
            if(!DateTime.TryParse(value,out result))
                return new DateTime?();
            return result;
        }
        [HttpGet]
        public ActionResult RenderToPdf(DateTime? beginDate, DateTime? endDate, int? departmentId,
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

                var argumrnts = new StringBuilder();

                var cookieName = FormsAuthentication.FormsCookieName;
                var authCookie = Request.Cookies[cookieName];
                if (authCookie == null || authCookie.Value == null)
                    throw new ArgumentException("Ошибка авторизации.");
                argumrnts.AppendFormat("{0} --cookie {1} {2}",
                    GetConverterCommandParam(beginDate, endDate, departmentId, 
                    requestStatusId, typeId,userName,sortBy, sortDescending)
                    , cookieName, authCookie.Value);
                //argumrnts.AppendFormat("\"{0}\"", GetConverterCommandPtaram(id));
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
        protected string GetConverterCommandParam(DateTime? beginDate, DateTime? endDate, int? departmentId, 
                    int? requestStatusId, int? typeId,string userName,int? sortBy,bool? sortDescending)
        {
            var localhostUrl = ConfigurationManager.AppSettings["localhost"];
            const string urlTemplate = "Deduction/PrintDeductionList";
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

//            string args =
//                string.Format(
//                    @"?beginDate={0}&endDate={1}&departmentId={2}&requestStatusId={3}&typeId={4}
//                        &userName={5}&sortBy={6}&sortDescending={7}",
//                    beginDate.HasValue?beginDate.Value.ToShortDateString():emptyValue,
//                    endDate.HasValue ? endDate.Value.ToShortDateString() : emptyValue,
//                    departmentId.HasValue ? departmentId.Value.ToString() : emptyValue,
//                    requestStatusId.HasValue?requestStatusId.Value:0, 
//                    typeId.HasValue?typeId.Value:0,
//                    string.IsNullOrEmpty(userName) ? emptyValue : Server.UrlEncode(userName),
//                    sortBy.HasValue ? sortBy.Value.ToString() : emptyValue,
//                    sortDescending.HasValue ? sortDescending.Value.ToString() : emptyValue);
            //args = string.Empty;))
            return !string.IsNullOrEmpty(localhostUrl)
                       ? string.Format(@"{0}/{1}{2}",localhostUrl, urlTemplate,args)
                       : Url.Content(string.Format(@"{0}{1}",urlTemplate, args));
        }
        protected ActionResult GetFile(string filePath, string fileName, string contentType)
        {
            byte[] value;
            using (FileStream stream = System.IO.File.Open(filePath, FileMode.Open))
            {
                value = new byte[stream.Length];
                stream.Read(value, 0, (int)stream.Length);
            }
            const string userFileName = "Deduction.pdf";
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
    }
}
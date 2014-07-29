using System;
using System.IO;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Reports.Core;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;
using WebMvc.Attributes;

namespace WebMvc.Controllers
{
    [Authorize]
    public class AdminController : BaseController
    {
        protected IAdminBl adminBl;
        public IAdminBl AdminBl
        {
            get
            {
                adminBl = Ioc.Resolve<IAdminBl>();
                return Validate.Dependency(adminBl);
            }
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UserList(int? currentPage)
        {
            UserListModel model = new UserListModel
            {
                CurrentPage = currentPage.HasValue ?
                              currentPage.Value : 1,
            };
            AdminBl.GetUserListModel(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult UserList(UserListModel model)
        {
            CheckUserRole(true);
            AdminBl.GetUserListModel(model);
            return View(model);
        }
        [HttpGet]
        public ActionResult UserEdit(int id)
        {
            CheckUserRole(true);
            UserEditModel model = new UserEditModel{Id = id};
            AdminBl.GetUserEditModel(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult UserEdit(UserEditModel model)
        {
            if (!ValidateModel(model))
            {
                AdminBl.SetStaticToModel(model,true);
                return View(model);
            }
            AdminBl.SaveUser(model);
            if(model.NeedToReload)
            {
                ModelState.Clear();
                ModelState.AddModelError("Error", model.Error);
                UserEditModel newModel = new UserEditModel { Id = model.Id};
                AdminBl.GetUserEditModel(newModel);
                return View(newModel);
            }
            if(!string.IsNullOrEmpty(model.Error))
                ModelState.AddModelError("Error", model.Error);

            if(model.ClearManagers)
            {
                if (ModelState.ContainsKey("ManagerId"))
                    ModelState.Remove("ManagerId");
                model.ManagerId = 0;
                /*if (ModelState.ContainsKey("PersonnelId"))
                    ModelState.Remove("PersonnelId");
                model.PersonnelId = 0;*/
            }
            return View(model);
        }
        #region Settings
        [HttpGet]
        public ActionResult Settings()
        {
            CheckUserRole(false);
            SettingsModel model = new SettingsModel();
            AdminBl.SetModel(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Settings(SettingsModel model)
        {
            CheckUserRole(false);
            if(!ValidateModel(model))
                return View(model);
            AdminBl.SaveSettings(model);
            if(!string.IsNullOrEmpty(model.Error))
                ModelState.AddModelError("Error", model.Error);
            return View(model);
        }
        protected bool ValidateModel(SettingsModel model)
        {
            if(!string.IsNullOrEmpty(model.ReleaseEmployeeDeleteDate))
            {
                DateTime releaseDate;
                if (!DateTime.TryParse(model.ReleaseEmployeeDeleteDate, out releaseDate))
                    ModelState.AddModelError("ReleaseEmployeeDeleteDate", "Поле <Удалить сотрудников(и их документы),уволеных до> должно быть датой.");
                else if (releaseDate >= DateTime.Today)
                    ModelState.AddModelError("ReleaseEmployeeDeleteDate", "Поле <Удалить сотрудников(и их документы),уволеных до> должно быть датой в прошлом.");
                else
                    model.ValidReleaseEmployeeDeleteDate = releaseDate;
            }
            if(!CheckFileExists(model.DownloadDictionaryFilePath))
                ModelState.AddModelError("DownloadDictionaryFilePath", "<Путь к файлу для загрузки справочника ...> - файл не существует.");
            CheckDirectoryAndFileNameExists(model.UploadTimesheetFilePath);
            return ModelState.IsValid;
        }

        protected bool CheckFileExists(string path)
        {
            return System.IO.File.Exists(path);
        }
        protected bool CheckDirectoryAndFileNameExists(string path)
        {
            if (string.IsNullOrEmpty(Path.GetFileName(path)))
            {
                ModelState.AddModelError("UploadTimesheetFilePath", "<Путь к файлу выгрузки табеля> - отсутствует имя файла.");
                return false;
            }
            string directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                ModelState.AddModelError("UploadTimesheetFilePath", "<Путь к файлу выгрузки табеля> - папка не существует.");
                return false;
            }
            return true;
        }
        #endregion
        #region Import/Export
        [HttpGet]
        public ActionResult ActionsList(int type)
        {
            CheckUserRole(false);
            ActionListModel model = new ActionListModel {Type = (ExportImportType)type};
            AdminBl.SetModel(model);
            ViewBag.Title = "Список " + (model.Type == ExportImportType.Import ? "загрузок" : "выгрузок"); 
            return View(model);
        }
        [HttpPost]
        public ActionResult ActionsList(ActionListModel model)
        {
            CheckUserRole(false);
            if (model.Type == ExportImportType.Import)
            {
                AdminBl.ImportFile(model);
                AdminBl.SetModel(model);
            }
            else
                AdminBl.ExportFile(model);
            if(!string.IsNullOrEmpty(model.Error))
                ModelState.AddModelError(string.Empty,model.Error);
            //AdminBl.SetModel(model);
            ViewBag.Title = "Список " + (model.Type == ExportImportType.Import ? "загрузок" : "выгрузок");
            return View(model);
        }
        [HttpGet]
        public ActionResult AutoImport()
        {
            CheckUserRole(false);
            AutoImportModel model = new AutoImportModel();
            AdminBl.AutoImport(model);
            return View(model);
        }
        [HttpGet]
        public ActionResult AutoExport(string month)
        {
            CheckUserRole(false);
            AutoImportModel model = new AutoImportModel();
            if (!ParseMonth(model, month))
                return View(model); 
            AdminBl.AutoExport(model);
            return View(model);
        }
        public bool ParseMonth(AutoImportModel model,string month)
        {
            if(string.IsNullOrEmpty(month))
            {
                model.Error = "Отсутствует месяц выгрузки";
                return false;
            }
            if(month.Length != 6)
            {
                model.Error = "Неправильный месяц выгрузки";
                return false;
            }
            string strMonth = month.Substring(0, 2);
            int monthNum;
            if(!int.TryParse(strMonth,out monthNum))
            {
                model.Error = "Неправильный месяц выгрузки";
                return false;
            }
            string strYear = month.Substring(2);
            int year;
            if (!int.TryParse(strYear, out year))
            {
                model.Error = "Неправильный месяц выгрузки";
                return false;
            }
            try
            {
                DateTime monthDate = new DateTime(year,monthNum,1);
                model.Month = monthDate;
            }
            catch (Exception)
            {
                model.Error = "Неправильный месяц выгрузки";
                return false;
            }
            return true;
        }
        #endregion
        [HttpGet]
        public ActionResult DocumentTypeList()
        {
            CheckUserRole(false);
            DocumentTypeListModel model = new DocumentTypeListModel();
            AdminBl.SetModel(model);
            return View(model);
        }
        [HttpGet]
        public ActionResult DocumentSubTypeList(int typeId)
        {
            CheckUserRole(false);
            DocumentSubtypeListModel model = new DocumentSubtypeListModel {DocumentTypeId = typeId};
            AdminBl.SetModel(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult DocumentSubTypeList(DocumentSubtypeListModel model)
        {
            CheckUserRole(false);
            AdminBl.SetModel(model);
            return View(model);
        }
        [HttpGet]
        public ActionResult EditSubTypeDialog(int Id,int typeId)
        {
            try
            {
                EditSubTypeModel model = new EditSubTypeModel { Id = Id,TypeId = typeId};
                AdminBl.GetEditSubTypeModel(model);
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
        public ContentResult SaveSubType(int id, int typeId, string name)
        {
            bool saveResult;
            string error;
            try
            {
                var model = new EditSubTypeModel { Id = id, Name = name,TypeId = typeId};
                saveResult = AdminBl.SaveSubType(model);
                error = model.Error;
            }
            catch (Exception ex)
            {
                Log.Error("Exception on SaveSubType:", ex);
                error = ex.GetBaseException().Message;
                saveResult = false;
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(new SaveTypeResult { Error = error, Result = saveResult });
            return Content(jsonString);
        }
        [HttpGet]
        public ActionResult EditTypeDialog(int Id)
        {
            try
            {
                EditTypeModel model = new EditTypeModel { Id = Id };
                AdminBl.GetEditTypeModel(model);
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.Error("Exception",ex);
                string error = "Ошибка при загрузке данных: " + ex.GetBaseException().Message;
                return PartialView("DialogError", new DialogErrorModel {Error = error});
            }
        }
        [HttpPost]
        public ContentResult SaveType(int id, string name)
        {
            bool saveResult;
            string error;
            try
            {
                var model = new EditTypeModel { Id = id, Name = name};
                saveResult = AdminBl.SaveType(model);
                error = model.Error;
            }
            catch (Exception ex)
            {
                Log.Error("Exception on SaveType:", ex);
                error = ex.GetBaseException().Message;
                saveResult = false;
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(new SaveTypeResult { Error = error, Result = saveResult });
            return Content(jsonString);
        }

        [HttpGet]
        public ActionResult InformationsList()
        {
            CheckUserRole(false);
            InformationListModel model = new InformationListModel();
            AdminBl.GetInformationListModel(model);
            return View(model);
        }
        [HttpGet]
        public ActionResult EditInfoDialog(int id)
        {
            try
            {
                EditInfoModel model = new EditInfoModel { Id = id };
                AdminBl.GetEditInfoModel(model);
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
        public ContentResult SaveInfo(int id, string subject, string message)
        {
            bool saveResult;
            string error;
            try
            {
                var model = new EditInfoModel { Id = id, Message = message,Subject = subject};
                saveResult = AdminBl.SaveInfo(model);
                error = model.Error;
            }
            catch (Exception ex)
            {
                Log.Error("Exception on SaveInfo:", ex);
                error = ex.GetBaseException().Message;
                saveResult = false;
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(new SaveTypeResult { Error = error, Result = saveResult });
            return Content(jsonString);
        }




        protected bool ValidateModel(UserEditModel model)
        {
            CheckUserRole(true);
            return ModelState.IsValid;
        }
        protected void CheckUserRole(bool checkPersonnel)
        {
            if(checkPersonnel)
            {
                if ((AuthenticationService.CurrentUser.UserRole != UserRole.Admin) &&
                    (AuthenticationService.CurrentUser.UserRole != UserRole.PersonnelManager))
                    throw new ArgumentException("Доступ к документу запрещен.");            
            }
            else if (AuthenticationService.CurrentUser.UserRole != UserRole.Admin)
                throw new ArgumentException("Доступ к документу запрещен.");            
        }

        [HttpPost]
        public ContentResult DeleteEmployees(string date)
        {
            
            bool saveResult = false;
            string error;
            try
            {
                CheckUserRole(false);
                DateTime deleteDate;
                if (!DateTime.TryParse(date, out deleteDate))
                    error = "Неправильная дата";
                else if (deleteDate >= DateTime.Today)
                    error = "Дата должна быть в прошлом.";
                else
                {
                    var model = new DeleteEmployeesModel { Date = deleteDate};
                    saveResult = AdminBl.DeleteEmployees(model);
                    error = model.Error;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception on DeleteEmployees:", ex);
                error = ex.GetBaseException().Message;
                saveResult = false;
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(new SaveTypeResult { Error = error, Result = saveResult });
            return Content(jsonString);
        }

        [HttpGet]
        [ReportAuthorize(UserRole.Admin)]
        public ContentResult ConvertAttachments()
        {
            bool saveResult = true;
            string error;
            try
            {
                DeleteAttacmentModel model = new DeleteAttacmentModel {Error = string.Empty};
                saveResult = AdminBl.ConvertAttachments(model);
                error = model.Error;

            }
            catch (Exception ex)
            {
                Log.Error("Exception on ConvertAttachments:", ex);
                error = ex.GetBaseException().Message;
                saveResult = false;
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(new SaveTypeResult { Error = error, Result = saveResult });
            return Content(jsonString);
        }
    }
}
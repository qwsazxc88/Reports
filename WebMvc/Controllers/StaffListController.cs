using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMvc.Attributes;
using Reports.Presenters.UI.Bl;
using Reports.Core;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Presenters.UI.ViewModel.StaffList;
using System.Web.Script.Serialization;

namespace WebMvc.Controllers
{
    public class StaffListController : Controller
    {
        #region Dependencies
        protected IStaffListBl stafflistBl;

        public IStaffListBl StaffListBl
        {
            get
            {
                stafflistBl = Ioc.Resolve<IStaffListBl>();
                return Validate.Dependency(stafflistBl);
            }
        }
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        #region Штатные расписание.
        /// <summary>
        /// Штатное расписание, первичная загрузка страницы.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.Findep | UserRole.PersonnelManager | UserRole.Accountant | UserRole.OutsourcingManager)]
        public ActionResult StaffList()
        {
            StaffListModel model = new StaffListModel();
            model.Departments = StaffListBl.GetDepartmentListByParent(null);
            return View(model);
        }
        /// <summary>
        /// Штатное расписание, подгружаем уровень подразделений с должностями и сотрудниками.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.Findep | UserRole.PersonnelManager | UserRole.Accountant | UserRole.OutsourcingManager)]
        public ActionResult StaffList(string DepId)
        {
            var jsonSerializer = new JavaScriptSerializer();
            StaffListModel model = StaffListBl.GetDepartmentStructureWithStaffPost(DepId);
            string jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }
        #endregion

        #region Заявки для подразделений
        /// <summary>
        /// Реестр заявок для подразделений.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.Findep | UserRole.PersonnelManager | UserRole.Accountant | UserRole.OutsourcingManager)]
        public ActionResult StaffDepartmentRequestList()
        {
            StaffDepartmentRequestListModel model = new StaffDepartmentRequestListModel();
            model = StaffListBl.GetStaffDepartmentRequestList();
            
            return View(model);
        }
        /// <summary>
        /// Реестр заявок для подразделений.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.Findep | UserRole.PersonnelManager | UserRole.Accountant | UserRole.OutsourcingManager)]
        public ActionResult StaffDepartmentRequestList(StaffDepartmentRequestListModel model)
        {
            if (ValidateModel(model))
                model = StaffListBl.SetStaffDepartmentRequestList(model);
            else
                model.Statuses = StaffListBl.GetDepRequestStatuses();
            return View(model);
        }
        /// <summary>
        /// Загрузка заявки для подразделения на создание/изменение/удаление.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.Findep | UserRole.PersonnelManager | UserRole.Accountant | UserRole.OutsourcingManager)]
        public ActionResult StaffDepartmentRequest(int RequestType, int? DepartmentId, int? Id)
        {
            StaffDepartmentRequestModel model = new StaffDepartmentRequestModel();
            ViewBag.Title = RequestType == 1 ? "Заявка на создание нового подразделения" : (RequestType == 2 ? "Заявка на изменение подразделения" : "Заявка на удаление продразделения");
            model.RequestTypeId = RequestType;
            if (model.RequestTypeId == 1)
                model.ParentId = DepartmentId.Value;
            else
                model.DepartmentId = DepartmentId.Value;

            model.Id = Id.HasValue ? Id.Value : 0;
            model = StaffListBl.GetDepartmentRequest(model);

            //if (RequestType == 1)
            //{
            //    model.ParentId = DepartmentId.Value;
            //    model.Id = Id.HasValue ? Id.Value : 0;
            //    model = StaffListBl.GetDepartmentRequest(model);
            //}
            //else
            //{
            //    model.DepartmentId = DepartmentId.Value;
            //}
            return View(model);
        }
        /// <summary>
        /// Жмем на кнопки в заявке для подразделений.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.Findep | UserRole.PersonnelManager | UserRole.Accountant | UserRole.OutsourcingManager)]
        public ActionResult StaffDepartmentRequest(StaffDepartmentRequestModel model)
        {
            ModelState.Clear();
            string error = string.Empty;
            bool IsComplete = false;
            if (model.IsDraft)  //сохранение черновика
            {
                IsComplete = model.Id == 0 ? StaffListBl.SaveNewDepartmentRequest(model, out error) : StaffListBl.SaveEditDepartmentRequest(model, out error);
                if (!IsComplete)
                {
                    StaffListBl.LoadDictionaries(model);
                    ModelState.AddModelError("Message", error);
                }
                else
                {
                    model = StaffListBl.GetDepartmentRequest(model);
                    ModelState.AddModelError("Message", "Данные сохранены!");
                }
            }
            else
            {
                if (ValidateModel(model))//проверки
                {
                    //отправка на согласование НЕ СДЕЛАНО
                    StaffListBl.LoadDictionaries(model);
                    ModelState.AddModelError("Message", "В разработке!");
                    //if (!StaffListBl.SaveEditDepartmentRequest(model, out error))
                    //{
                    //    StaffListBl.LoadDictionaries(model);
                    //    ModelState.AddModelError("Message", error);
                    //    //return View(model);
                    //}
                }
            }
            
            //заглушка для выкладки на тест для показа прототипов
            //StaffListBl.LoadDictionaries(model);
            //ModelState.AddModelError("Message", "В разработке!");

            return View(model);
        }
        /// <summary>
        /// Начальная загрузка формы для построения адресов.
        /// </summary>
        /// <param name="Id">Id адреса</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Address(int? Id, string PostIndex, string RegionCode, string AreaCode, string CityCode, string SettlementCode, string StreetCode, int HouseType, string HouseNumber, int BuildType, string BuildNumber, int FlatType, string FlatNumber)
        {
            AddressModel model = new AddressModel();
            model.Id = Id;
            model.PostIndex = PostIndex == "" ? string.Empty : PostIndex;
            model.RegionCode = RegionCode == "" ? string.Empty : RegionCode;
            model.AreaCode = AreaCode == null ? string.Empty : AreaCode;
            model.CityCode = CityCode == "" ? string.Empty : CityCode;
            model.SettlementCode = SettlementCode == "" ? string.Empty : SettlementCode;
            model.StreetCode = StreetCode == "" ? string.Empty : StreetCode;
            model.HouseType = HouseType;
            model.HouseNumber = HouseNumber == "" ? string.Empty : HouseNumber;
            model.BuildType = BuildType;
            model.BuildNumber = BuildNumber == "" ? string.Empty : BuildNumber;
            model.FlatType = FlatType;
            model.FlatNumber = FlatNumber == "" ? string.Empty : FlatNumber;

            model = StaffListBl.GetAddress(model);

            return PartialView(model);
        }
        /// <summary>
        /// Частичная подгрузка справочника КЛАДР
        /// </summary>
        /// <param name="Code">Код для объекта в класификаторе.</param>
        /// <param name="AddressType">Тип объекта</param>
        /// <returns></returns>
        [HttpGet]
        public ContentResult GetKladr(string Code, int AddressType)
        {
            var jsonSerializer = new JavaScriptSerializer();
            string jsonString = jsonSerializer.Serialize(StaffListBl.GetKladr(Code, AddressType, null, null, null, null));
            return Content(jsonString);
        }
        #endregion

        #region Заявки для штатных единиц.
        /// <summary>
        /// Реестр заявок для ШЕ.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.Findep | UserRole.PersonnelManager | UserRole.Accountant | UserRole.OutsourcingManager)]
        public ActionResult StaffEstablishedPostRequestList()
        {
            StaffEstablishedPostRequestListModel model = new StaffEstablishedPostRequestListModel();
            model = StaffListBl.GetStaffEstablishedPostRequestList();

            return View(model);
        }
        /// <summary>
        /// Реестр заявок для ШЕ.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.Findep | UserRole.PersonnelManager | UserRole.Accountant | UserRole.OutsourcingManager)]
        public ActionResult StaffEstablishedPostRequestList(StaffEstablishedPostRequestListModel model)
        {
            
            //if (ValidateModel(model))
            //    model = StaffListBl.SetStaffEstablishedPostRequestList(model);
            //else
            //    model.Statuses = StaffListBl.GetDepRequestStatuses();

            ModelState.AddModelError("MessageStr", "В разработке");
            model.Statuses = StaffListBl.GetDepRequestStatuses();
            return View(model);
        }
        /// <summary>
        /// Загрузка заявки для ШЕ на создание/изменение/удаление.
        /// </summary>
        /// <param name="RequestType">Тип заявки.</param>
        /// <param name="DepartmentId">Id подраздление</param>
        /// <param name="SEPId">Id штатной единицы.</param>
        /// <param name="Id">Id заявки.</param>
        /// <returns></returns>
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.Findep | UserRole.PersonnelManager | UserRole.Accountant | UserRole.OutsourcingManager)]
        public ActionResult StaffEstablishedPostRequest(int RequestType, int? DepartmentId, int? SEPId, int? Id)
        {
            StaffEstablishedPostRequestModel model = new StaffEstablishedPostRequestModel();
            ViewBag.Title = RequestType == 1 ? "Заявка на создание ШЕ" : (RequestType == 2 ? "Заявка на изменение ШЕ" : "Заявка на сокращение ШЕ");
            model.RequestTypeId = RequestType;
            model.DepartmentId = DepartmentId.Value;
            model.SEPId = SEPId.HasValue ? SEPId.Value : 0;
            model.Id = Id.HasValue ? Id.Value : 0;
            model = StaffListBl.GetEstablishedPostRequest(model);

            return View(model);
        }
        /// <summary>
        /// Жмем на кнопки в заявке для ШЕ.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.Findep | UserRole.PersonnelManager | UserRole.Accountant | UserRole.OutsourcingManager)]
        public ActionResult StaffEstablishedPostRequest(StaffEstablishedPostRequestModel model)
        {
            ModelState.Clear();
            //string error = string.Empty;
            //bool IsComplete = false;
            //if (model.IsDraft)  //сохранение черновика
            //{
            //    IsComplete = model.Id == 0 ? StaffListBl.SaveNewDepartmentRequest(model, out error) : StaffListBl.SaveEditDepartmentRequest(model, out error);
            //    if (!IsComplete)
            //    {
            //        StaffListBl.LoadDictionaries(model);
            //        ModelState.AddModelError("Message", error);
            //    }
            //    else
            //    {
            //        model = StaffListBl.GetDepartmentRequest(model);
            //        ModelState.AddModelError("Message", "Данные сохранены!");
            //    }
            //}
            //else
            //{
            //    if (ValidateModel(model))//проверки
            //    {
            //        //отправка на согласование НЕ СДЕЛАНО
            //        StaffListBl.LoadDictionaries(model);
            //        ModelState.AddModelError("Message", "В разработке!");
            //        //if (!StaffListBl.SaveEditDepartmentRequest(model, out error))
            //        //{
            //        //    StaffListBl.LoadDictionaries(model);
            //        //    ModelState.AddModelError("Message", error);
            //        //    //return View(model);
            //        //}
            //    }
            //}

            //заглушка для выкладки на тест для показа прототипов
            StaffListBl.LoadDictionaries(model);
            ModelState.AddModelError("MessageStr", "В разработке!");

            return View(model);
        }
        /// <summary>
        /// Автозаполнение должности.
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public ActionResult AutocompletePositionSearch(string term)
        {
            IList<IdNameDto> Positions = StaffListBl.GetPositionAutocomplete(term);
            var PositionList = Positions.ToList().Select(a => new { label = a.Name, PositionId = a.Id }).Distinct();

            return Json(PositionList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Валидация
        protected bool ValidateModel(StaffDepartmentRequestListModel model)
        {
            return ModelState.IsValid;
        }
        protected bool ValidateModel(StaffDepartmentRequestModel model)
        {
            return ModelState.IsValid;
        }
        protected bool ValidateModel(StaffEstablishedPostRequestListModel model)
        {
            return ModelState.IsValid;
        }
        #endregion

        #region Заявки для штатных единиц
        #endregion

        #region Для тестов
        /// <summary>
        /// Рекурсивное построение дерева.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[ReportAuthorize(UserRole.Manager | UserRole.Chief | UserRole.Director | UserRole.Security | UserRole.Trainer | UserRole.PersonnelManager | UserRole.OutsourcingManager | UserRole.Candidate)]
        public ActionResult TreeView()
        {
            TreeViewModel model = StaffListBl.GetDepartmentList();
            return View(model);
        }
        /// <summary>
        /// Загрузка второго уровня подразделений из структуры банка для построения дерева.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TreeViewAjax()
        {
            TreeViewAjaxModel model = new TreeViewAjaxModel();//StaffListBl.GetDepartmentList();
            model.Departments = StaffListBl.GetDepartmentListByParent("9900424");
            return View(model);
        }
        /// <summary>
        /// Загрузка списка подразделений/должностей (для 7 уровня подразделений) по заданному коду родительского подразделения.
        /// </summary>
        /// <param name="DepId">Код подразделения</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TreeViewAjax(string DepId)
        {
            TreeViewAjaxModel model = new TreeViewAjaxModel();
            model.Departments = StaffListBl.GetDepartmentListByParent(DepId);
            return Json(model.Departments);
        }
        /// <summary>
        /// Загрузка второго уровня подразделений из структуры банка для построения дерева в таблице.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TreeGridAjax()
        {
            TreeGridAjaxModel model = StaffListBl.GetDepartmentStructure("9900424");
            return View(model);
        }
        /// <summary>
        /// Загрузка списка подразделений/должностей (для 7 уровня подразделений) по заданному коду родительского подразделения.
        /// </summary>
        /// <param name="DepId">Код подразделения</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TreeGridAjax(string DepId)
        {
            var jsonSerializer = new JavaScriptSerializer();
            TreeGridAjaxModel model = StaffListBl.GetDepartmentStructure(DepId);

            string jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }
        /// <summary>
        /// Загрузка страницы структуры подразделений с привязкой к точкам Фиграда.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DepStructureFingradPoints()
        {
            DepStructureFingradPointsModel model = StaffListBl.GetDepartmentStructureWithFingradPoins("9900424");
            return View(model);
        }
        /// <summary>
        /// Загрузка структуры подразделений с привязкой к точкам Фиграда по коду родительского подразделения.
        /// </summary>
        /// <param name="DepId">Код подразделения</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DepStructureFingradPoints(string DepId)
        {
            var jsonSerializer = new JavaScriptSerializer();
            DepStructureFingradPointsModel model = StaffListBl.GetDepartmentStructureWithFingradPoins(DepId);

            string jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }
        #endregion

    }
}

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
using Reports.Core.Dao;
using Reports.Presenters.UI.ViewModel.StaffList;
using System.Web.Script.Serialization;

namespace WebMvc.Controllers
{
    public class StaffListController : BaseController
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
            //для комментариев
            ViewBag.PlaceId = model.Id;
            ViewBag.PlaceTypeId = 2; 

           
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
                    ModelState.AddModelError("MessageStr", error);
                }
                else
                {
                    model = StaffListBl.GetDepartmentRequest(model);
                    ModelState.AddModelError("MessageStr", "Данные сохранены!");
                }
            }
            else
            {
                if (ValidateModel(model))//проверки
                {
                    StaffListBl.LoadDictionaries(model);
                    ModelState.AddModelError("MessageStr", "В разработке!");
                    //if (!StaffListBl.SaveEditDepartmentRequest(model, out error))
                    //{
                    //    StaffListBl.LoadDictionaries(model);
                    //    ModelState.AddModelError("MessageStr", "В разработке!");
                    //}
                    //else
                    //{
                    //    StaffListBl.LoadDictionaries(model);
                    //    ModelState.AddModelError("MessageStr", "Данные сохранены! Заявка утверждена!");
                    //}
                }
            }
            
            //заглушка для выкладки на тест для показа прототипов
            //StaffListBl.LoadDictionaries(model);
            //ModelState.AddModelError("Message", "В разработке!");
            //для комментариев
            ViewBag.PlaceId = model.Id;
            ViewBag.PlaceTypeId = 2; 

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
            
            if (ValidateModel(model))
                model = StaffListBl.SetStaffEstablishedPostRequestList(model);
            else
                model.Statuses = StaffListBl.GetDepRequestStatuses();

            //ModelState.AddModelError("MessageStr", "В разработке");
            //model.Statuses = StaffListBl.GetDepRequestStatuses();
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
            //для комментариев
            ViewBag.PlaceId = model.Id;
            ViewBag.PlaceTypeId = 3; 

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
            string error = string.Empty;
            bool IsComplete = false;

            if (model.IsDraft)  //сохранение черновика
            {
                IsComplete = model.Id == 0 ? StaffListBl.SaveNewEstablishedPostRequest(model, out error) : StaffListBl.SaveEditEstablishedPostRequest(model, out error);
                if (!IsComplete)
                {
                    StaffListBl.LoadDictionaries(model);
                    ModelState.AddModelError("MessageStr", error);
                }
                else
                {
                    model = StaffListBl.GetEstablishedPostRequest(model);
                    ModelState.AddModelError("MessageStr", "Данные сохранены!");
                }
            }
            else
            {
                if (ValidateModel(model))//проверки
                {
                    //отправка на согласование НЕ СДЕЛАНО, пока сразу сохраняем изменения в справочнике
                    if (!StaffListBl.SaveEditEstablishedPostRequest(model, out error))
                    {
                        StaffListBl.LoadDictionaries(model);
                        ModelState.AddModelError("MessageStr", error);
                    }
                    else
                    {
                        model = StaffListBl.GetEstablishedPostRequest(model);
                        ModelState.AddModelError("MessageStr", "Данные сохранены! Штатная единица утверждена!");
                    }
                }
                else
                {
                    StaffListBl.LoadDictionaries(model);
                }
            }

            //для комментариев
            ViewBag.PlaceId = model.Id;
            ViewBag.PlaceTypeId = 3; 

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

        #region Штатная расстановка.
        /// <summary>
        /// Штатное расстановка, первичная загрузка страницы.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.Findep | UserRole.PersonnelManager | UserRole.Accountant | UserRole.OutsourcingManager)]
        public ActionResult StaffListArrangement()
        {
            StaffListArrangementModel model = new StaffListArrangementModel();
            model.Departments = StaffListBl.GetDepartmentListByParent(null);
            return View(model);
        }
        /// <summary>
        /// Штатное расписание, подгружаем уровень подразделений с должностями и сотрудниками.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.Findep | UserRole.PersonnelManager | UserRole.Accountant | UserRole.OutsourcingManager)]
        public ActionResult StaffListArrangement(string DepId)
        {
            var jsonSerializer = new JavaScriptSerializer();
            StaffListArrangementModel model = StaffListBl.GetDepartmentStructureWithStaffArrangement(DepId);
            string jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }
        #endregion

        #region Справочники
        #region Справочник ПО
        /// <summary>
        /// Загрузка справочника ПО.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult StaffDepartmentSoftReference(bool? IsModal)
        {
            /*
             входящий параметр и все что с ним связано - это была попытка вытаскивать справочник в заявке модально 
             * не доработал
             * возможно надо переделать форму справочника или решить проблему с пропаданием вкладок справочника в модальном окне после submit
             * 
             */
            StaffDepartmentSoftReferenceModel model = StaffListBl.GetSoftReference(new StaffDepartmentSoftReferenceModel());
            model.TabIndex = 0;
            model.IsModal = IsModal.HasValue ? IsModal.Value : false;
            if (model.IsModal)
                return PartialView(model);
            else
                return View(model);
        }
        /// <summary>
        /// Сохранение данных в справочнике ПО.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult StaffDepartmentSoftReference(StaffDepartmentSoftReferenceModel model)
        {
            string error = string.Empty;

            ModelState.Clear();
            if (model.SwitchOperation == 0)
            {
                model.IsError = false;
                model = StaffListBl.GetSoftReference(model);
            }
            else
            {
                if (ValidateModel(model))
                {
                    if (!StaffListBl.SaveSoftReference(model, out error))
                    {
                        ModelState.AddModelError("MessageStr", error);
                    }
                    else
                    {
                        model.IsError = false;
                        model = StaffListBl.GetSoftReference(model);
                    }
                }
            }

            if (model.IsModal)
                return PartialView(model);
            else
                return View(model);
        }
        #endregion

        #region Cправочник кодировок
        /// <summary>
        /// Загрузка справочника ПО.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult StaffDepartmentEncoding(int? TabIndex)
        {
            StaffDepartmentEncodingModel model = new StaffDepartmentEncodingModel();
            model.TabIndex = TabIndex.HasValue && TabIndex.Value > 0 ? TabIndex.Value : 0;
            return View(model);
        }

        #region Справочник филиалов
        /// <summary>
        /// Загрузка справочника филиалов.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult StaffDepartmentBranch()
        {
            StaffDepartmentBranchModel model = StaffListBl.GetStaffDepartmentBranch(new StaffDepartmentBranchModel());
            return PartialView(model);
        }

        /// <summary>
        /// Сохраняем данные.
        /// </summary>
        /// <param name="itemToAdd"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddEditStaffDepartmentBranch(StaffDepartmentBranchDto itemToAddEdit)
        {
            string error = String.Empty;
            bool result = false;

            if (ValidateModel(itemToAddEdit, out error))
            {
                if (StaffListBl.SaveStaffDepartmentBranch(itemToAddEdit, out error))
                    result = true;
            }


            StaffDepartmentBranchModel model = StaffListBl.GetStaffDepartmentBranch(new StaffDepartmentBranchModel());
            ViewBag.Error = error;
          
            return Json(new { ok = result, msg = error, model.Branches });
        }

        /// <summary>
        /// Удаляем данные.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteStaffDepartmentBranch(int Id)
        {
            string error = String.Empty;
            bool result = false;

            if (StaffListBl.DeleteStaffDepartmentBranch(Id, out error))
                result = true;

            StaffDepartmentBranchModel model = StaffListBl.GetStaffDepartmentBranch(new StaffDepartmentBranchModel());
            ViewBag.Error = error;

            return Json(new { ok = result, msg = error, model.Branches });
        }
        #endregion

        #region Справочник дирекций
        /// <summary>
        /// Загрузка справочника дирекций.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult StaffDepartmentManagement()
        {
            StaffDepartmentManagementModel model = StaffListBl.GetStaffDepartmentManagement(new StaffDepartmentManagementModel());
            return PartialView(model);
        }

        /// <summary>
        /// Сохраняем данные.
        /// </summary>
        /// <param name="itemToAdd"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddEditStaffDepartmentMenegment(StaffDepartmentManagementDto itemToAddEdit)
        {
            string error = String.Empty;
            bool result = false;

            if (ValidateModel(itemToAddEdit, out error))
            {
                if (StaffListBl.SaveStaffDepartmentManagement(itemToAddEdit, out error))
                    result = true;
            }


            StaffDepartmentManagementModel model = StaffListBl.GetStaffDepartmentManagement(new StaffDepartmentManagementModel());
            ViewBag.Error = error;

            return Json(new { ok = result, msg = error, model.Managements });
        }

        /// <summary>
        /// Удаляем данные.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteStaffDepartmentMenegment(int Id)
        {
            string error = String.Empty;
            bool result = false;

            if (StaffListBl.DeleteStaffDepartmentManagement(Id, out error))
                result = true;

            StaffDepartmentManagementModel model = StaffListBl.GetStaffDepartmentManagement(new StaffDepartmentManagementModel());
            ViewBag.Error = error;

            return Json(new { ok = result, msg = error, model.Managements });
        }
        #endregion

        #region Справочник управлений
        /// <summary>
        /// Загрузка справочника управлений.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult StaffDepartmentAdministration()
        {
            StaffDepartmentAdministrationModel model = StaffListBl.GetStaffDepartmentAdministration(new StaffDepartmentAdministrationModel());
            return PartialView(model);
        }

        /// <summary>
        /// Сохраняем данные.
        /// </summary>
        /// <param name="itemToAdd"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddEditStaffDepartmentAdministration(StaffDepartmentAdministrationDto itemToAddEdit)
        {
            string error = String.Empty;
            bool result = false;

            if (ValidateModel(itemToAddEdit, out error))
            {
                if (StaffListBl.SaveStaffDepartmentAdministration(itemToAddEdit, out error))
                    result = true;
            }

            StaffDepartmentAdministrationModel model = StaffListBl.GetStaffDepartmentAdministration(new StaffDepartmentAdministrationModel());
            ViewBag.Error = error;

            return Json(new { ok = result, msg = error, model.Administrations });
        }

        /// <summary>
        /// Удаляем данные.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteStaffDepartmentAdministration(int Id)
        {
            string error = String.Empty;
            bool result = false;

            if (StaffListBl.DeleteStaffDepartmentAdministration(Id, out error))
                result = true;

            StaffDepartmentAdministrationModel model = StaffListBl.GetStaffDepartmentAdministration(new StaffDepartmentAdministrationModel());
            ViewBag.Error = error;

            return Json(new { ok = result, msg = error, model.Administrations });
        }
        #endregion

        #region Справочник бизнес-групп
        /// <summary>
        /// Загрузка справочника бизнес-групп.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult StaffDepartmentBusinessGroup()
        {
            StaffDepartmentBusinessGroupModel model = StaffListBl.GetStaffDepartmentBusinessGroup(new StaffDepartmentBusinessGroupModel(), 0, 0, 0);
            return PartialView(model);
        }

        /// <summary>
        /// Загрузка справочника бизнес-групп.
        /// </summary>
        /// <param name="AdminFilterId">Id управления.</param>
        /// <param name="ManagementFilterId">Id дирекции</param>
        /// <param name="BranchFilterId">Id филиала</param>
        /// <returns></returns>
        /// <returns></returns>
        [HttpPost]
        public ActionResult StaffDepartmentBusinessGroup(int AdminFilterId, int ManagementFilterId, int BranchFilterId)
        {
            StaffDepartmentBusinessGroupModel model = StaffListBl.GetStaffDepartmentBusinessGroup(new StaffDepartmentBusinessGroupModel(), AdminFilterId, ManagementFilterId, BranchFilterId);
            return Json(new { ok = true, msg = "", model.BusinessGroups });
            //return PartialView(model);
        }

        /// <summary>
        /// Сохраняем данные.
        /// </summary>
        /// <param name="itemToAdd"></param>
        /// <param name="AdminFilterId">Id управления.</param>
        /// <param name="ManagementFilterId">Id дирекции</param>
        /// <param name="BranchFilterId">Id филиала</param>
        /// <returns></returns>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddEditStaffDepartmentBusinessGroup(StaffDepartmentBusinessGroupDto itemToAddEdit, int AdminFilterId, int ManagementFilterId, int BranchFilterId)
        {
            string error = String.Empty;
            bool result = false;

            if (ValidateModel(itemToAddEdit, out error))
            {
                if (StaffListBl.SaveStaffDepartmentBusinessGroup(itemToAddEdit, out error))
                    result = true;
            }

            StaffDepartmentBusinessGroupModel model = StaffListBl.GetStaffDepartmentBusinessGroup(new StaffDepartmentBusinessGroupModel(), AdminFilterId, ManagementFilterId, BranchFilterId);
            ViewBag.Error = error;

            return Json(new { ok = result, msg = error, model.BusinessGroups });
        }

        /// <summary>
        /// Удаляем данные.
        /// </summary>
        /// <param name="Id">Id удаляемой записи</param>
        /// <param name="AdminFilterId">Id управления.</param>
        /// <param name="ManagementFilterId">Id дирекции</param>
        /// <param name="BranchFilterId">Id филиала</param>
        /// <returns></returns>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteStaffDepartmentBusinessGroup(int Id, int AdminFilterId, int ManagementFilterId, int BranchFilterId)
        {
            string error = String.Empty;
            bool result = false;

            if (StaffListBl.DeleteStaffDepartmentBusinessGroup(Id, out error))
                result = true;

            StaffDepartmentBusinessGroupModel model = StaffListBl.GetStaffDepartmentBusinessGroup(new StaffDepartmentBusinessGroupModel(), AdminFilterId, ManagementFilterId, BranchFilterId);
            ViewBag.Error = error;

            return Json(new { ok = result, msg = error, model.BusinessGroups });
        }
        #endregion

        #region Справочник РП-привязок
        /// <summary>
        /// Загрузка справочника РП-привязок.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult StaffDepartmentRPLink()
        {
            StaffDepartmentRPLinkModel model = StaffListBl.GetStaffDepartmentRPLink(new StaffDepartmentRPLinkModel());
            return PartialView(model);
        }

        /// <summary>
        /// Сохраняем данные.
        /// </summary>
        /// <param name="itemToAdd"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddEditStaffDepartmentRPLink(StaffDepartmentRPLinkDto itemToAddEdit)
        {
            string error = String.Empty;
            bool result = false;

            if (ValidateModel(itemToAddEdit, out error))
            {
                if (StaffListBl.SaveStaffDepartmentRPLink(itemToAddEdit, out error))
                    result = true;
            }

            StaffDepartmentRPLinkModel model = StaffListBl.GetStaffDepartmentRPLink(new StaffDepartmentRPLinkModel());
            ViewBag.Error = error;

            return Json(new { ok = result, msg = error, model.RPLinks });
        }

        /// <summary>
        /// Удаляем данные.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteStaffDepartmentRPLink(int Id)
        {
            string error = String.Empty;
            bool result = false;

            if (StaffListBl.DeleteStaffDepartmentRPLink(Id, out error))
                result = true;

            StaffDepartmentRPLinkModel model = StaffListBl.GetStaffDepartmentRPLink(new StaffDepartmentRPLinkModel());
            ViewBag.Error = error;

            return Json(new { ok = result, msg = error, model.RPLinks });
        }
        #endregion
        #endregion
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
        protected bool ValidateModel(StaffEstablishedPostRequestModel model)
        {
            if (model.Quantity <= 0)
                ModelState.AddModelError("Quantity", "Укажите количество!");
            if (model.Salary <= 0)
                ModelState.AddModelError("Salary", "Укажите оклад!");
            if (model.PostChargeLinks.Where(x => x.Amount != 0 && x.AmountProc != 0).Count() != 0)
            {
                ModelState.AddModelError("MessageStr", "Надбавка должна указываться только в одной единице измерения!");

                for (int i = 0; i < model.PostChargeLinks.Count; i++)
                {
                    if (model.PostChargeLinks[i].Amount != 0 && model.PostChargeLinks[i].AmountProc != 0)
                    {
                        ModelState.AddModelError("PostChargeLinks[" + i.ToString() + "].Amount", "*");
                        ModelState.AddModelError("PostChargeLinks[" + i.ToString() + "].AmountProc", "*");
                    }
                }
            }

            return ModelState.IsValid;
        }
        protected bool ValidateModel(StaffDepartmentSoftReferenceModel model)
        {
            switch (model.TabIndex)
            {
                case 0:
                    if (model.SwitchOperation == 1)//добавление
                    {
                        if (string.IsNullOrEmpty(model.SoftGroupName) || string.IsNullOrWhiteSpace(model.SoftGroupName))
                            ModelState.AddModelError("SoftGroupName", "Введите название группы ПО!");
                    }
                    if (model.SwitchOperation == 2)//редактирование
                    {
                        for (int i = 0; i < model.GroupList.Count; i++)
                        {
                            if (string.IsNullOrEmpty(model.GroupList[i].Name) || string.IsNullOrWhiteSpace(model.GroupList[i].Name))
                            {
                                ModelState.AddModelError("GroupList[" + i.ToString() + "].Name", "*");
                                ModelState.AddModelError("MessageStr", "Введите название группы ПО");
                            }
                        }
                    }
                    break;
                case 1:
                    if (model.SwitchOperation == 3)
                    {
                        if (model.SoftGroupLink.Where(x => x.IsUsed).Count() == 0)
                        {
                            ModelState.AddModelError("MessageStr", "Группа ПО должна содержать в себе установленное ПО!");
                        }
                    }
                    break;
                case 2:
                    if (model.SwitchOperation == 4)//добавление
                    {
                        if (string.IsNullOrEmpty(model.SoftName) || string.IsNullOrWhiteSpace(model.SoftName))
                            ModelState.AddModelError("SoftName", "Введите название ПО!");
                    }
                    if (model.SwitchOperation == 5)//редактирование
                    {
                        for (int i = 0; i < model.SoftList.Count; i++)
                        {
                            if (string.IsNullOrEmpty(model.SoftList[i].Name) || string.IsNullOrWhiteSpace(model.SoftList[i].Name))
                            {
                                ModelState.AddModelError("SoftList[" + i.ToString() + "].Name", "*");
                                ModelState.AddModelError("MessageStr", "Введите название ПО");
                            }
                        }
                    }
                    break;
            }

            model.IsError = !ModelState.IsValid;

            return ModelState.IsValid;
        }
        protected bool ValidateModel(StaffDepartmentBranchDto EditRow, out string error)
        {
            error = string.Empty;
            return StaffListBl.ValidateDepartmentBranchRow(EditRow, out error);
        }
        protected bool ValidateModel(StaffDepartmentManagementDto EditRow, out string error)
        {
            error = string.Empty;
            return StaffListBl.ValidateDepartmentManagementRow(EditRow, out error);
        }
        protected bool ValidateModel(StaffDepartmentAdministrationDto EditRow, out string error)
        {
            error = string.Empty;
            return StaffListBl.ValidateDepartmentAdministrationRow(EditRow, out error);
        }
        protected bool ValidateModel(StaffDepartmentBusinessGroupDto EditRow, out string error)
        {
            error = string.Empty;
            return StaffListBl.ValidateDepartmentBusinessGroupRow(EditRow, out error);
        }
        protected bool ValidateModel(StaffDepartmentRPLinkDto EditRow, out string error)
        {
            error = string.Empty;
            return StaffListBl.ValidateDepartmentRPLinkRow(EditRow, out error);
        }
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

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
using Reports.Presenters.UI.ViewModel;
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
        /// <param name="DepId">Id родительского подразделения</param>
        /// <param name="IsParentDepOnly">Признак достать только родительское подазделение.</param>
        /// <param name="IsBegin">Флажок показывающий, что это первоначальная загрузка.</param>
        /// <returns></returns>
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing | UserRole.TaxCollector | UserRole.SoftAdmin | UserRole.StaffListOrder)]
        public ActionResult StaffList(string DepId, bool? IsParentDepOnly, bool? IsBegin)
        {
            StaffListModel model = new StaffListModel();
            model.Departments = StaffListBl.GetDepartmentListByParent(DepId, IsParentDepOnly.HasValue ? IsParentDepOnly.Value : false, !IsBegin.HasValue ? true : IsBegin.Value);
            return View(model);
        }
        
        /// <summary>
        /// Штатное расписание, подгружаем уровень подразделений с должностями и сотрудниками.
        /// </summary>
        /// <param name="DepId">Id родительского подразделения</param>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing | UserRole.TaxCollector)]
        public ActionResult StaffListGetNodes(string DepId)
        {
            var jsonSerializer = new JavaScriptSerializer();
            StaffListModel model = StaffListBl.GetDepartmentStructureWithStaffPost(DepId, false);
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
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing | UserRole.TaxCollector | UserRole.SoftAdmin | UserRole.StaffListOrder)]
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
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing | UserRole.TaxCollector | UserRole.SoftAdmin | UserRole.StaffListOrder)]
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
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing | UserRole.TaxCollector | UserRole.SoftAdmin | UserRole.StaffListOrder)]
        public ActionResult StaffDepartmentRequest(int RequestType, int? DepartmentId, int? Id)
        {
            ModelState.Clear();
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
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing | UserRole.TaxCollector | UserRole.SoftAdmin | UserRole.StaffListOrder)]
        public ActionResult StaffDepartmentRequest(StaffDepartmentRequestModel model)
        {
            ModelState.Clear();
            string error = string.Empty;
            bool IsComplete = false;
            ViewBag.Title = model.RequestTypeId == 1 ? "Заявка на создание нового подразделения" : (model.RequestTypeId == 2 ? "Заявка на изменение подразделения" : "Заявка на удаление продразделения");

            if (ValidateModel(model))//проверки
            {
                if (model.IsDraft)  //сохранение черновика
                {
                    if (!model.IsDelete)
                        IsComplete = model.Id == 0 ? StaffListBl.SaveNewDepartmentRequest(model, out error) : StaffListBl.SaveEditDepartmentRequest(model, out error);
                    else
                        IsComplete = model.Id == 0 ? false : StaffListBl.DeleteDepartmentRequest(model, out error);
                    

                    if (!IsComplete)
                    {
                        StaffListBl.LoadDictionaries(model);
                        ModelState.AddModelError("MessageStr", error);
                    }
                    else
                    {
                        model = StaffListBl.GetDepartmentRequest(model);
                        ModelState.AddModelError("MessageStr", error);
                    }
                }
                else
                {
                    IsComplete = model.IsSoftAdminApprove && model.IsSoftAdmin ? StaffListBl.SaveProgramCodes(model, out error) : StaffListBl.SaveEditDepartmentRequest(model, out error);

                    if (!IsComplete)
                    {
                        StaffListBl.LoadDictionaries(model);
                        ModelState.AddModelError("MessageStr", error);
                    }
                    else
                    {
                        model = StaffListBl.GetDepartmentRequest(model);
                        ModelState.AddModelError("MessageStr", error);
                    }
                }
            }
            else
                StaffListBl.LoadDictionaries(model);
            
            
            
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
        /// <summary>
        /// Обновляем справочники на странице после закрытия модальных окон.
        /// </summary>
        /// <param name="SwitchReference">Переключатель: 1 - справочник ПО, 2 - справочник операций</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RefreshDepartmentRequestReference(int SwitchReference)
        {
            StaffDepartmentRequestModel model = new StaffDepartmentRequestModel();
            model.RequestTypeId = 1;
            model.Id = 0;
            StaffListBl.LoadDictionaries(model);

            if (SwitchReference == 1)
                return Json(new { ok = true, msg = "", model.SoftGroups });
            else 
                return Json(new { ok = true, msg = "", model.OperationGroups });
        }
        #endregion

        #region Заявки для штатных единиц.
        /// <summary>
        /// Реестр заявок для ШЕ.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing | UserRole.Inspector | UserRole.PersonnelManager)]
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
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing | UserRole.Inspector | UserRole.PersonnelManager)]
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
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing | UserRole.Inspector | UserRole.PersonnelManager)]
        public ActionResult StaffEstablishedPostRequest(int RequestType, int? DepartmentId, int? SEPId, int? Id)
        {
            ModelState.Clear();
            StaffEstablishedPostRequestModel model = new StaffEstablishedPostRequestModel();
            ViewBag.Title = RequestType == 1 ? "Заявка на создание ШЕ" : (RequestType == 2 ? "Заявка на изменение ШЕ" : "Заявка на сокращение ШЕ");
            model.RequestTypeId = RequestType;
            model.DepartmentId = DepartmentId.Value;
            model.SEPId = SEPId.HasValue ? SEPId.Value : 0;
            model.Id = Id.HasValue ? Id.Value : 0;
            model = StaffListBl.GetEstablishedPostRequest(model);
            //для комментариев
            ViewBag.PlaceId = model.Id;
            ViewBag.PlaceTypeId = 4; 

            return View(model);
        }
        /// <summary>
        /// Жмем на кнопки в заявке для ШЕ.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.ConsultantOutsourcing | UserRole.Inspector)]
        public ActionResult StaffEstablishedPostRequest(StaffEstablishedPostRequestModel model)
        {
            ViewBag.Title = model.RequestTypeId == 1 ? "Заявка на создание ШЕ" : (model.RequestTypeId == 2 ? "Заявка на изменение ШЕ" : "Заявка на сокращение ШЕ");
            ModelState.Clear();
            string error = string.Empty;
            bool IsComplete = false;

            if (ValidateModel(model))
            {
                if (model.IsDraft)  //сохранение черновика
                {
                    if (!model.IsDelete)
                        IsComplete = model.Id == 0 ? StaffListBl.SaveNewEstablishedPostRequest(model, out error) : StaffListBl.SaveEditEstablishedPostRequest(model, out error);
                    else
                        IsComplete = model.Id == 0 ? false : StaffListBl.DeleteEstablishedPostRequest(model, out error);

                    if (!IsComplete)
                    {
                        StaffListBl.LoadDictionaries(model);
                        ModelState.AddModelError("MessageStr", error);
                    }
                    else
                    {
                        model = StaffListBl.GetEstablishedPostRequest(model);
                        ModelState.AddModelError("MessageStr", error);
                    }
                }
                else
                {
                    if (!StaffListBl.SaveEditEstablishedPostRequest(model, out error))
                    {
                        StaffListBl.LoadDictionaries(model);
                        ModelState.AddModelError("MessageStr", error);
                    }
                    else
                    {
                        model = StaffListBl.GetEstablishedPostRequest(model);
                        ModelState.AddModelError("MessageStr", error);
                    }
                }
            }
            else
                StaffListBl.LoadDictionaries(model);

            //для комментариев
            ViewBag.PlaceId = model.Id;
            ViewBag.PlaceTypeId = 4; 

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
        /// <param name="DepId">Id родительского подразделения</param>
        /// <param name="IsParentDepOnly">Признак достать только родительское подазделение.</param>
        /// <param name="IsBegin">Флажок показывающий, что это первоначальная загрузка.</param>
        /// <returns></returns>
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing | UserRole.PersonnelManager)]
        public ActionResult StaffListArrangement(string DepId, bool? IsParentDepOnly, bool? IsBegin)
        {
            StaffListArrangementModel model = new StaffListArrangementModel();
            model = StaffListBl.GetStaffListArrangementModel(model);
            model.Departments = StaffListBl.GetDepartmentListByParent(DepId, IsParentDepOnly.HasValue ? IsParentDepOnly.Value : false, !IsBegin.HasValue ? true : IsBegin.Value);
            return View(model);
        }
        /// <summary>
        /// Штатное расписание, подгружаем уровень подразделений с должностями и сотрудниками.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing | UserRole.PersonnelManager)]
        public ActionResult StaffListArrangement(string DepId)
        {
            var jsonSerializer = new JavaScriptSerializer();
            StaffListArrangementModel model = StaffListBl.GetDepartmentStructureWithStaffArrangement(DepId, false);
            string jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }
        /// <summary>
        /// Штатное расписание, подгружаем уровень подразделений с должностями и сотрудниками.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing | UserRole.PersonnelManager)]
        public ActionResult CreateTemporaryReleaseVacancyRequest(string DepId)
        {
            var jsonSerializer = new JavaScriptSerializer();
            StaffListArrangementModel model = StaffListBl.GetDepartmentStructureWithStaffArrangement(DepId, false);
            string jsonString = jsonSerializer.Serialize(model);
            return Content(jsonString);
        }

        /// <summary>
        /// Всплывающее окно для выбора типа заявки в кадровом перемещении.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SelectMovementType(int UserId)
        {
            SelectMovementTypeModel model = StaffListBl.GetSelectMovementTypeModel(UserId);
            return PartialView(model);
        }
        #endregion

        #region Справочники
        #region Справочник ПО
        /// <summary>
        /// Загрузка справочника ПО.
        /// </summary>
        /// <param name="TabIndex">Позиционируемся на заданой вкладке</param>
        /// <param name="IsModal">Признак модального режима</param>
        /// <returns></returns>
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
        public ActionResult StaffDepartmentSoftReference(int? TabIndex, bool? IsModal)
        {
            StaffDepartmentSoftReferenceModel model = new StaffDepartmentSoftReferenceModel();
            model.TabIndex = TabIndex.HasValue && TabIndex.Value > 0 ? TabIndex.Value : 0;
            model.IsModal = IsModal.HasValue ? IsModal.Value : false;
            if (!model.IsModal)
                return View(model);
            else
                return PartialView(model);
        }
        
        #region Справочник групп ПО
        /// <summary>
        /// Загрузка справочника групп ПО.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
        public ActionResult StaffDepartmentSoftGroup()
        {
            StaffDepartmentSoftGroupModel model = StaffListBl.GetStaffDepartmentSoftGroup(new StaffDepartmentSoftGroupModel());
            return PartialView(model);
        }

        /// <summary>
        /// Сохраняем данные.
        /// </summary>
        /// <param name="itemToAdd"></param>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
        public ActionResult AddEditStaffDepartmentSoftGroup(StaffDepartmentSoftGroupDto itemToAddEdit)
        {
            string error = String.Empty;
            bool result = false;

            if (ValidateModel(itemToAddEdit, out error))
            {
                if (StaffListBl.SaveStaffDepartmentSoftGroup(itemToAddEdit, out error))
                    result = true;
            }


            StaffDepartmentSoftGroupModel model = StaffListBl.GetStaffDepartmentSoftGroup(new StaffDepartmentSoftGroupModel());
            ViewBag.Error = error;

            return Json(new { ok = result, msg = error, model.GroupList });
        }

        #endregion

        #region Справочник банковского ПО
        /// <summary>
        /// Загрузка справочника банковского ПО.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
        public ActionResult StaffDepartmentInstallSoft()
        {
            StaffDepartmentInstallSoftModel model = StaffListBl.GetStaffDepartmentInstallSoft(new StaffDepartmentInstallSoftModel());
            return PartialView(model);
        }

        /// <summary>
        /// Сохраняем данные.
        /// </summary>
        /// <param name="itemToAdd"></param>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
        public ActionResult AddEditStaffDepartmentInstallSoft(StaffDepartmentInstallSoftDto itemToAddEdit)
        {
            string error = String.Empty;
            bool result = false;

            if (ValidateModel(itemToAddEdit, out error))
            {
                if (StaffListBl.SaveStaffDepartmentInstallSoft(itemToAddEdit, out error))
                    result = true;
            }


            StaffDepartmentInstallSoftModel model = StaffListBl.GetStaffDepartmentInstallSoft(new StaffDepartmentInstallSoftModel());
            ViewBag.Error = error;

            return Json(new { ok = result, msg = error, model.SoftList });
        }

        #endregion

        #region Связи ПО с группами
        /// <summary>
        /// Загрузка связей.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
        public ActionResult StaffDepartmentSoftGroupLinks()
        {
            StaffDepartmentSoftGroupLinksModel model = StaffListBl.GetStaffDepartmentSoftGroupLinks(new StaffDepartmentSoftGroupLinksModel(), 0);
            return PartialView(model);
        }

        /// <summary>
        /// Загрузка связей при выборе в выпадающем списке.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
        public ActionResult StaffDepartmentSoftGroupLinks(int SoftGroupId)
        {
            StaffDepartmentSoftGroupLinksModel model = StaffListBl.GetStaffDepartmentSoftGroupLinks(new StaffDepartmentSoftGroupLinksModel(), SoftGroupId);

            return Json(new { ok = true, msg = "", model.SoftGroupLink });
        }

        /// <summary>
        /// Сохраняем данные.
        /// </summary>
        /// <param name="SoftGroupLink">Данные</param>
        /// <param name="OperGroupId">Id группы операций</param>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
        public ActionResult AddEditStaffDepartmentSoftGroupLinks(IList<StaffDepartmentSoftGroupLinksDto> SoftGroupLink, int SoftGroupId)
        {
            string error = String.Empty;
            bool result = false;

            if (StaffListBl.SaveStaffDepartmentSoftGroupLinks(SoftGroupLink, SoftGroupId, out error))
                result = true;


            StaffDepartmentSoftGroupLinksModel model = StaffListBl.GetStaffDepartmentSoftGroupLinks(new StaffDepartmentSoftGroupLinksModel(), SoftGroupId);
            ViewBag.Error = error;

            return Json(new { ok = result, msg = error, model.SoftGroupLink });
        }
        #endregion
        #endregion

        #region Cправочник кодировок
        /// <summary>
        /// Загрузка справочника ПО.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
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
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
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
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
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
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
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
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
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
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
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
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
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
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
        public ActionResult StaffDepartmentAdministration()
        {
            StaffDepartmentAdministrationModel model = StaffListBl.GetStaffDepartmentAdministration(new StaffDepartmentAdministrationModel(), 0, 0);
            return PartialView(model);
        }

        /// <summary>
        /// Загрузка справочника управлений.
        /// </summary>
        /// <param name="ManagementFilterId">Id дирекции</param>
        /// <param name="BranchFilterId">Id филиала</param>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
        public ActionResult StaffDepartmentAdministration(int ManagementFilterId, int BranchFilterId)
        {
            StaffDepartmentAdministrationModel model = StaffListBl.GetStaffDepartmentAdministration(new StaffDepartmentAdministrationModel(), ManagementFilterId, BranchFilterId);
            return Json(new { ok = true, msg = "", model.Administrations });
        }

        /// <summary>
        /// Сохраняем данные.
        /// </summary>
        /// <param name="itemToAdd"></param>
        /// <param name="ManagementFilterId">Id дирекции</param>
        /// <param name="BranchFilterId">Id филиала</param>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
        public ActionResult AddEditStaffDepartmentAdministration(StaffDepartmentAdministrationDto itemToAddEdit, int ManagementFilterId, int BranchFilterId)
        {
            string error = String.Empty;
            bool result = false;

            if (ValidateModel(itemToAddEdit, out error))
            {
                if (StaffListBl.SaveStaffDepartmentAdministration(itemToAddEdit, out error))
                    result = true;
            }

            StaffDepartmentAdministrationModel model = StaffListBl.GetStaffDepartmentAdministration(new StaffDepartmentAdministrationModel(), ManagementFilterId, BranchFilterId);
            ViewBag.Error = error;

            return Json(new { ok = result, msg = error, model.Administrations });
        }

        /// <summary>
        /// Удаляем данные.
        /// </summary>
        /// <param name="Id">Id строки</param>
        /// <param name="ManagementFilterId">Id дирекции</param>
        /// <param name="BranchFilterId">Id филиала</param>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
        public ActionResult DeleteStaffDepartmentAdministration(int Id, int ManagementFilterId, int BranchFilterId)
        {
            string error = String.Empty;
            bool result = false;

            if (StaffListBl.DeleteStaffDepartmentAdministration(Id, out error))
                result = true;

            StaffDepartmentAdministrationModel model = StaffListBl.GetStaffDepartmentAdministration(new StaffDepartmentAdministrationModel(), ManagementFilterId, BranchFilterId);
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
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
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
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
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
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
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
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
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
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
        public ActionResult StaffDepartmentRPLink()
        {
            StaffDepartmentRPLinkModel model = StaffListBl.GetStaffDepartmentRPLink(new StaffDepartmentRPLinkModel(), 0, 0, 0, 0);
            return PartialView(model);
        }

        /// <summary>
        /// Загрузка справочника РП-привязок.
        /// </summary>
        /// <param name="BGFilterId">Id бизнес-группы</param>
        /// <param name="AdminFilterId">Id управления.</param>
        /// <param name="ManagementFilterId">Id дирекции</param>
        /// <param name="BranchFilterId">Id филиала</param>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
        public ActionResult StaffDepartmentRPLink(int BGFilterId, int AdminFilterId, int ManagementFilterId, int BranchFilterId)
        {
            StaffDepartmentRPLinkModel model = StaffListBl.GetStaffDepartmentRPLink(new StaffDepartmentRPLinkModel(), BGFilterId, AdminFilterId, ManagementFilterId, BranchFilterId);
            return Json(new { ok = true, msg = "", model.RPLinks });
        }

        /// <summary>
        /// Сохраняем данные.
        /// </summary>
        /// <param name="itemToAdd"></param>
        /// <param name="BGFilterId">Id бизнес-группы</param>
        /// <param name="AdminFilterId">Id управления.</param>
        /// <param name="ManagementFilterId">Id дирекции</param>
        /// <param name="BranchFilterId">Id филиала</param>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
        public ActionResult AddEditStaffDepartmentRPLink(StaffDepartmentRPLinkDto itemToAddEdit, int BGFilterId, int AdminFilterId, int ManagementFilterId, int BranchFilterId)
        {
            string error = String.Empty;
            bool result = false;

            if (ValidateModel(itemToAddEdit, out error))
            {
                if (StaffListBl.SaveStaffDepartmentRPLink(itemToAddEdit, out error))
                    result = true;
            }

            StaffDepartmentRPLinkModel model = StaffListBl.GetStaffDepartmentRPLink(new StaffDepartmentRPLinkModel(), BGFilterId, AdminFilterId, ManagementFilterId, BranchFilterId);
            ViewBag.Error = error;

            return Json(new { ok = result, msg = error, model.RPLinks });
        }

        /// <summary>
        /// Удаляем данные.
        /// </summary>
        /// <param name="Id">Id записи</param>
        /// <param name="BGFilterId">Id бизнес-группы</param>
        /// <param name="AdminFilterId">Id управления.</param>
        /// <param name="ManagementFilterId">Id дирекции</param>
        /// <param name="BranchFilterId">Id филиала</param>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
        public ActionResult DeleteStaffDepartmentRPLink(int Id, int BGFilterId, int AdminFilterId, int ManagementFilterId, int BranchFilterId)
        {
            string error = String.Empty;
            bool result = false;

            if (StaffListBl.DeleteStaffDepartmentRPLink(Id, out error))
                result = true;

            StaffDepartmentRPLinkModel model = StaffListBl.GetStaffDepartmentRPLink(new StaffDepartmentRPLinkModel(), BGFilterId, AdminFilterId, ManagementFilterId, BranchFilterId);
            ViewBag.Error = error;

            return Json(new { ok = result, msg = error, model.RPLinks });
        }
        #endregion
        #endregion

        #region Справочник операций
        /// <summary>
        /// Загрузка справочника операций подразделений.
        /// </summary>
        /// <param name="TabIndex">Позиционируемся на заданой вкладке</param>
        /// <param name="IsModal">Признак модального режима</param>
        /// <returns></returns>
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
        public ActionResult StaffDepartmentOperationReference(int? TabIndex, bool? IsModal)
        {
            StaffDepartmentOperationReferenceModel model = new StaffDepartmentOperationReferenceModel();
            model.TabIndex = TabIndex.HasValue && TabIndex.Value > 0 ? TabIndex.Value : 0;
            model.IsModal = IsModal.HasValue ? IsModal.Value : false;
            if (!model.IsModal)
                return View(model);
            else
                return PartialView(model);
        }

        #region Справочник групп операций
        /// <summary>
        /// Загрузка справочника групп операций.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
        public ActionResult StaffDepartmentOperationGroups()
        {
            StaffDepartmentOperationGroupsModel model = StaffListBl.GetStaffDepartmentOperationGroups(new StaffDepartmentOperationGroupsModel());
            return PartialView(model);
        }

        /// <summary>
        /// Сохраняем данные.
        /// </summary>
        /// <param name="itemToAdd"></param>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
        public ActionResult AddEditStaffDepartmentOperationGroups(StaffDepartmentOperationGroupsDto itemToAddEdit)
        {
            string error = String.Empty;
            bool result = false;

            if (ValidateModel(itemToAddEdit, out error))
            {
                if (StaffListBl.SaveStaffDepartmentOperationGroups(itemToAddEdit, out error))
                    result = true;
            }


            StaffDepartmentOperationGroupsModel model = StaffListBl.GetStaffDepartmentOperationGroups(new StaffDepartmentOperationGroupsModel());
            ViewBag.Error = error;

            return Json(new { ok = result, msg = error, model.OperationGroups });
        }
        #endregion

        #region Справочник операций
        /// <summary>
        /// Загрузка справочника операций.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
        public ActionResult StaffDepartmentOperations()
        {
            StaffDepartmentOperationsModel model = StaffListBl.GetStaffDepartmentOperations(new StaffDepartmentOperationsModel());
            return PartialView(model);
        }

        /// <summary>
        /// Сохраняем данные.
        /// </summary>
        /// <param name="itemToAdd"></param>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
        public ActionResult AddEditStaffDepartmentOperations(StaffDepartmentOperationsDto itemToAddEdit)
        {
            string error = String.Empty;
            bool result = false;

            if (ValidateModel(itemToAddEdit, out error))
            {
                if (StaffListBl.SaveStaffDepartmentOperations(itemToAddEdit, out error))
                    result = true;
            }


            StaffDepartmentOperationsModel model = StaffListBl.GetStaffDepartmentOperations(new StaffDepartmentOperationsModel());
            ViewBag.Error = error;

            return Json(new { ok = result, msg = error, model.Operations });
        }
        #endregion

        #region Связи операций с группами
        /// <summary>
        /// Загрузка связей.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
        public ActionResult StaffDepartmentOperationLinks()
        {
            StaffDepartmentOperationLinksModel model = StaffListBl.GetStaffDepartmentOperationLinks(new StaffDepartmentOperationLinksModel(), 0);
            return PartialView(model);
        }

        /// <summary>
        /// Загрузка связей при выборе в выпадающем списке.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
        public ActionResult StaffDepartmentOperationLinks(int OperationGroupId)
        {
            StaffDepartmentOperationLinksModel model = StaffListBl.GetStaffDepartmentOperationLinks(new StaffDepartmentOperationLinksModel(), OperationGroupId);

            return Json(new { ok = true, msg = "", model.OperationList });
        }   

        /// <summary>
        /// Сохраняем данные.
        /// </summary>
        /// <param name="OperationList">Данные</param>
        /// <param name="OperGroupId">Id группы операций</param>
        /// <returns></returns>
        [HttpPost]
        [ReportAuthorize(UserRole.Manager | UserRole.Director | UserRole.ConsultantPersonnel | UserRole.Inspector | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)]
        public ActionResult AddEditStaffDepartmentOperationLinks(IList<StaffDepartmentOperationLinksDto> OperationList, int OperationGroupId)
        {
            string error = String.Empty;
            bool result = false;

            if (StaffListBl.SaveStaffDepartmentOperationLinks(OperationList, OperationGroupId, out error))
                result = true;
            

            StaffDepartmentOperationLinksModel model = StaffListBl.GetStaffDepartmentOperationLinks(new StaffDepartmentOperationLinksModel(), OperationGroupId);
            ViewBag.Error = error;

            return Json(new { ok = result, msg = error, model.OperationList });
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
            //добавление
            //если по данному подразделению это не первая заявка, то нужно достать предыдущий код для финграда

            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrWhiteSpace(model.Name))
            {
                ModelState.AddModelError("Name", "Введите название подразделения!");
                ModelState.AddModelError("MessageStr", "Введите название подразделения!");
            }

            if (model.BFGId == 0)
            {
                ModelState.AddModelError("BFGId", "Укажите принадлежность подразделения!");
                ModelState.AddModelError("MessageStr", "Укажите принадлежность подразделения!");
            }

            //для налоговиков при согласовании
            if (!model.IsDraft)
            {
                if (model.DepNextId == 0 && (model.ItemLevel <= 2 || model.ItemLevel == 7) && AuthenticationService.CurrentUser.UserRole == UserRole.TaxCollector && model.BFGId == 2)
                {
                    ModelState.AddModelError("DepNextId", "Укажите подразделение с налоговыми рекизитами!");
                    ModelState.AddModelError("MessageStr", "Укажите подразделение с налоговыми рекизитами!");
                }
            }

            //проверки при редактировании действующей заявки
            if (model.IsUsed)
            {
                //если меняется тип подразделения на определенные виды (КК/ОО/ДО в МО и обратно)
                if ((model.DepTypeId == 9 && (model.DepTypeIdOld == 8 || model.DepTypeIdOld == 14 || model.DepTypeIdOld == 7)) ||
                    (model.DepTypeIdOld == 9 && (model.DepTypeId == 8 || model.DepTypeId == 14 || model.DepTypeId == 7)))
                {
                    ModelState.AddModelError("DepTypeId", "В действующей заявке нельзя изменить текущий тип подразделения!");
                    ModelState.AddModelError("MessageStr", "В действующей заявке нельзя изменить текущий тип подразделения!");
                }

                //если меняется причина внесения в справочник (9, 10, 11, 12 - виды переездов)
                if (((model.ReasonId == 9 || model.ReasonId == 10 || model.ReasonId == 11 || model.ReasonId == 12) && model.ReasonId != model.ReasonIdOld) ||
                    ((model.ReasonIdOld == 9 || model.ReasonIdOld == 10 || model.ReasonIdOld == 11 || model.ReasonIdOld == 12) && model.ReasonId != model.ReasonIdOld))
                {
                    ModelState.AddModelError("ReasonId", "В действующей заявке нельзя изменить текущую причину внесения в справочник!");
                    ModelState.AddModelError("MessageStr", "В действующей заявке нельзя изменить текущую причину внесения в справочник!");
                }
            }
            

            return ModelState.IsValid;
        }
        protected bool ValidateModel(StaffEstablishedPostRequestListModel model)
        {
            return ModelState.IsValid;
        }
        protected bool ValidateModel(StaffEstablishedPostRequestModel model)
        {
            

            if (model.IsDraft)
            {
                if ((model.RequestTypeId == 1 || model.RequestTypeId == 2) && model.Quantity == 0)
                {
                    ModelState.AddModelError("Quantity", "Укажите количество штатных единиц не равное 0!");
                    model.Quantity = model.QuantityPrev;
                    model.Salary = model.SalaryPrev;
                }                

                if (model.RequestTypeId == 2 && model.QuantityPrev > model.Quantity)
                    ModelState.AddModelError("Quantity", "Изменение количества штатной единицы в меньшую сторону является сокращением! Создайте заявку на сокращение штатной единицы!");
            }
            else
            {
                if (model.BeginAccountDate < new DateTime(2015, 12, 1))
                    ModelState.AddModelError("BeginAccountDate", "Дата начала учета в системе не может быть меньше 1 декабря 2015 года!");

                if (model.RequestTypeId == 1 || model.RequestTypeId == 2)
                {
                    if (model.Quantity <= 0)
                        ModelState.AddModelError("Quantity", "Укажите количество!");
                    if (model.Salary <= 0)
                        ModelState.AddModelError("Salary", "Укажите оклад!");
                }

                if (model.PostChargeLinks.Where(x => x.ActionId == 0 && x.Amount != 0).Count() != 0)
                {
                    int i = 1;
                    foreach (var item in model.PostChargeLinks)
                    {
                        if (item.ActionId == 0)
                        {
                            ModelState.AddModelError("PostChargeLinks[" + (i - 1) + "].ActionId", "Укажите действие!");
                        }
                        i += 1;
                    }
                }

                if (model.Personnels != null)
                {
                    if ((model.RequestTypeId == 3 || model.RequestTypeId == 4) && model.Personnels.Where(x => x.UserId != 0 && x.IsDismissal).Count() != 0)
                    {
                        int i = 1;
                        foreach (var item in model.Personnels)
                        {
                            if (item.UserId != 0 && item.IsDismissal)
                            {
                                ModelState.AddModelError("Personnels[" + (i - 1) + "].IsDismissal", "На данный момент можно сократить свободные вакансии! Сокращение вакансия занятых сотрудниками в разработке и будет доступным в ближайшее время!");
                            }
                            i += 1;
                        }
                    }
                }
            }

            return ModelState.IsValid;
        }
        protected bool ValidateModel(StaffDepartmentSoftGroupDto EditRow, out string error)
        {
            error = string.Empty;
            return StaffListBl.ValidateDepartmentSoftGroupRow(EditRow, out error);
        }
        protected bool ValidateModel(StaffDepartmentInstallSoftDto EditRow, out string error)
        {
            error = string.Empty;
            return StaffListBl.ValidateDepartmentInstallSoftRow(EditRow, out error);
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
        protected bool ValidateModel(StaffDepartmentOperationGroupsDto EditRow, out string error)
        {
            error = string.Empty;
            return StaffListBl.ValidateDepartmentOperationGroupsRow(EditRow, out error);
        }
        protected bool ValidateModel(StaffDepartmentOperationsDto EditRow, out string error)
        {
            error = string.Empty;
            return StaffListBl.ValidateDepartmentOperationRow(EditRow, out error);
        }
        #endregion

    }
}

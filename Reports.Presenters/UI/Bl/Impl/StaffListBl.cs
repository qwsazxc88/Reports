using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Presenters.UI.ViewModel.StaffList;
using Reports.Core;
using Reports.Core.Domain;
using Reports.Core.Dao;
using Reports.Core.Dto;
using Reports.Core.Enum;
using System.Web.Mvc;

namespace Reports.Presenters.UI.Bl.Impl
{
    public class StaffListBl : RequestBl, IStaffListBl
    {
        #region Dependencies
        protected IAppointmentReasonDao appointmentreasonDao;
        public IAppointmentReasonDao AppointmentReasonDao
        {
            get { return Validate.Dependency(appointmentreasonDao); }
            set { appointmentreasonDao = value; }
        }

        protected IKladrDao kladrDao;
        public IKladrDao KladrDao
        {
            get { return Validate.Dependency(kladrDao); }
            set { kladrDao = value; }
        }

        protected IStaffDepartmentRequestDao staffdepartmentrequestDao;
        public IStaffDepartmentRequestDao StaffDepartmentRequestDao
        {
            get { return Validate.Dependency(staffdepartmentrequestDao); }
            set { staffdepartmentrequestDao = value; }
        }

        protected IStaffDepartmentCBDetailsDao staffdepartmentCBdetailsDao;
        public IStaffDepartmentCBDetailsDao StaffDepartmentCBDetailsDao
        {
            get { return Validate.Dependency(staffdepartmentCBdetailsDao); }
            set { staffdepartmentCBdetailsDao = value; }
        }
        
        protected IStaffDepartmentTypesDao staffdepartmenttypesDao;
        public IStaffDepartmentTypesDao StaffDepartmentTypesDao
        {
            get { return Validate.Dependency(staffdepartmenttypesDao); }
            set { staffdepartmenttypesDao = value; }
        }

        protected IStaffProgramCodesDao staffprogramcodesDao;
        public IStaffProgramCodesDao StaffProgramCodesDao
        {
            get { return Validate.Dependency(staffprogramcodesDao); }
            set { staffprogramcodesDao = value; }
        }

        protected IStaffDepartmentLandmarksDao staffdepartmentlandmarksDao;
        public IStaffDepartmentLandmarksDao StaffDepartmentLandmarksDao
        {
            get { return Validate.Dependency(staffdepartmentlandmarksDao); }
            set { staffdepartmentlandmarksDao = value; }
        }

        protected IStaffDepartmentOperationLinksDao staffdepartmentoperationlinksDao;
        public IStaffDepartmentOperationLinksDao StaffDepartmentOperationLinksDao
        {
            get { return Validate.Dependency(staffdepartmentoperationlinksDao); }
            set { staffdepartmentoperationlinksDao = value; }
        }

        protected IRefAddressesDao refaddressesDao;
        public IRefAddressesDao RefAddressesDao
        {
            get { return Validate.Dependency(refaddressesDao); }
            set { refaddressesDao = value; }
        }

        protected IStaffDepartmentOperationsDao staffdepartmentoperationsDao;
        public IStaffDepartmentOperationsDao StaffDepartmentOperationsDao
        {
            get { return Validate.Dependency(staffdepartmentoperationsDao); }
            set { staffdepartmentoperationsDao = value; }
        }

        protected IStaffProgramReferenceDao staffprogramreferenceDao;
        public IStaffProgramReferenceDao StaffProgramReferenceDao
        {
            get { return Validate.Dependency(staffprogramreferenceDao); }
            set { staffprogramreferenceDao = value; }
        }

        protected IStaffLandmarkTypesDao stafflandmarktypesDao;
        public IStaffLandmarkTypesDao StaffLandmarkTypesDao
        {
            get { return Validate.Dependency(stafflandmarktypesDao); }
            set { stafflandmarktypesDao = value; }
        }

        protected IStaffDepartmentRequestTypesDao staffdepartmentrequestTypesDao;
        public IStaffDepartmentRequestTypesDao StaffDepartmentRequestTypesDao
        {
            get { return Validate.Dependency(staffdepartmentrequestTypesDao); }
            set { staffdepartmentrequestTypesDao = value; }
        }

        protected IStaffDepartmentTaxDetailsDao staffdepartmenttaxDetailsDao;
        public IStaffDepartmentTaxDetailsDao StaffDepartmentTaxDetailsDao
        {
            get { return Validate.Dependency(staffdepartmenttaxDetailsDao); }
            set { staffdepartmenttaxDetailsDao = value; }
        }

        protected IStaffDepartmentOperationModesDao staffdepartmentOperationModesDao;
        public IStaffDepartmentOperationModesDao StaffDepartmentOperationModesDao
        {
            get { return Validate.Dependency(staffdepartmentOperationModesDao); }
            set { staffdepartmentOperationModesDao = value; }
        }

        protected IStaffEstablishedPostDao staffestablishedPostDao;
        public IStaffEstablishedPostDao StaffEstablishedPostDao
        {
            get { return Validate.Dependency(staffestablishedPostDao); }
            set { staffestablishedPostDao = value; }
        }

        protected IStaffEstablishedPostRequestTypesDao staffestablishedPostRequestTypesDao;
        public IStaffEstablishedPostRequestTypesDao StaffEstablishedPostRequestTypesDao
        {
            get { return Validate.Dependency(staffestablishedPostRequestTypesDao); }
            set { staffestablishedPostRequestTypesDao = value; }
        }

        protected IStaffEstablishedPostRequestDao staffestablishedPostRequestDao;
        public IStaffEstablishedPostRequestDao StaffEstablishedPostRequestDao
        {
            get { return Validate.Dependency(staffestablishedPostRequestDao); }
            set { staffestablishedPostRequestDao = value; }
        }

        protected IStaffEstablishedPostChargeLinksDao staffestablishedPostChargeLinksDao;
        public IStaffEstablishedPostChargeLinksDao StaffEstablishedPostChargeLinksDao
        {
            get { return Validate.Dependency(staffestablishedPostChargeLinksDao); }
            set { staffestablishedPostChargeLinksDao = value; }
        }

        protected IStaffExtraChargesDao staffextraChargesDao;
        public IStaffExtraChargesDao StaffExtraChargesDao
        {
            get { return Validate.Dependency(staffextraChargesDao); }
            set { staffextraChargesDao = value; }
        }

        protected IStaffDepartmentReasonsDao staffsepartmentReasonsDao;
        public IStaffDepartmentReasonsDao StaffDepartmentReasonsDao
        {
            get { return Validate.Dependency(staffsepartmentReasonsDao); }
            set { staffsepartmentReasonsDao = value; }
        }

        protected IStaffNetShopIdentificationDao staffnetshopIdentificationDao;
        public IStaffNetShopIdentificationDao StaffNetShopIdentificationDao
        {
            get { return Validate.Dependency(staffnetshopIdentificationDao); }
            set { staffnetshopIdentificationDao = value; }
        }

        protected IStaffDepartmentCashDeskAvailableDao staffdepartmentCashDeskAvailableDao;
        public IStaffDepartmentCashDeskAvailableDao StaffDepartmentCashDeskAvailableDao
        {
            get { return Validate.Dependency(staffdepartmentCashDeskAvailableDao); }
            set { staffdepartmentCashDeskAvailableDao = value; }
        }

        protected IStaffDepartmentRentPlaceDao staffdepartmentRentPlaceDao;
        public IStaffDepartmentRentPlaceDao StaffDepartmentRentPlaceDao
        {
            get { return Validate.Dependency(staffdepartmentRentPlaceDao); }
            set { staffdepartmentRentPlaceDao = value; }
        }

        protected IStaffDepartmentSKB_GEDao staffdepartmentSKB_GEDao;
        public IStaffDepartmentSKB_GEDao StaffDepartmentSKB_GEDao
        {
            get { return Validate.Dependency(staffdepartmentSKB_GEDao); }
            set { staffdepartmentSKB_GEDao = value; }
        }

        protected IStaffDepartmentSoftGroupDao staffdepartmentSoftGroupDao;
        public IStaffDepartmentSoftGroupDao StaffDepartmentSoftGroupDao
        {
            get { return Validate.Dependency(staffdepartmentSoftGroupDao); }
            set { staffdepartmentSoftGroupDao = value; }
        }

        protected IStaffDepartmentInstallSoftDao staffdepartmentInstallSoftDao;
        public IStaffDepartmentInstallSoftDao StaffDepartmentInstallSoftDao
        {
            get { return Validate.Dependency(staffdepartmentInstallSoftDao); }
            set { staffdepartmentInstallSoftDao = value; }
        }

        protected IStaffDepartmentSoftGroupLinksDao staffdepartmentSoftGroupLinksDao;
        public IStaffDepartmentSoftGroupLinksDao StaffDepartmentSoftGroupLinksDao
        {
            get { return Validate.Dependency(staffdepartmentSoftGroupLinksDao); }
            set { staffdepartmentSoftGroupLinksDao = value; }
        }

        protected IStaffDepartmentBranchDao staffdepartmentBranchDao;
        public IStaffDepartmentBranchDao StaffDepartmentBranchDao
        {
            get { return Validate.Dependency(staffdepartmentBranchDao); }
            set { staffdepartmentBranchDao = value; }
        }

        protected IStaffDepartmentManagementDao staffsepartmentManagementDao;
        public IStaffDepartmentManagementDao StaffDepartmentManagementDao
        {
            get { return Validate.Dependency(staffsepartmentManagementDao); }
            set { staffsepartmentManagementDao = value; }
        }

        protected IStaffDepartmentAdministrationDao staffdepartmentAdministrationDao;
        public IStaffDepartmentAdministrationDao StaffDepartmentAdministrationDao
        {
            get { return Validate.Dependency(staffdepartmentAdministrationDao); }
            set { staffdepartmentAdministrationDao = value; }
        }

        protected IStaffDepartmentBusinessGroupDao staffdepartmentBusinessGroupDao;
        public IStaffDepartmentBusinessGroupDao StaffDepartmentBusinessGroupDao
        {
            get { return Validate.Dependency(staffdepartmentBusinessGroupDao); }
            set { staffdepartmentBusinessGroupDao = value; }
        }

        protected IStaffDepartmentRPLinkDao staffdepartmentRPLinkDao;
        public IStaffDepartmentRPLinkDao StaffDepartmentRPLinkDao
        {
            get { return Validate.Dependency(staffdepartmentRPLinkDao); }
            set { staffdepartmentRPLinkDao = value; }
        }

        protected IStaffDepartmentOperationGroupsDao staffdepartmentOperationGroupsDao;
        public IStaffDepartmentOperationGroupsDao StaffDepartmentOperationGroupsDao
        {
            get { return Validate.Dependency(staffdepartmentOperationGroupsDao); }
            set { staffdepartmentOperationGroupsDao = value; }
        }

        protected IStaffDepartmentAccessoryDao staffdepartmentAccessoryDao;
        public IStaffDepartmentAccessoryDao StaffDepartmentAccessoryDao
        {
            get { return Validate.Dependency(staffdepartmentAccessoryDao); }
            set { staffdepartmentAccessoryDao = value; }
        }

        protected IDepartmentArchiveDao departmentarchiveDao;
        public IDepartmentArchiveDao DepartmentArchiveDao
        {
            get { return Validate.Dependency(departmentarchiveDao); }
            set { departmentarchiveDao = value; }
        }

        protected IStaffWorkingConditionsDao staffworkingConditionsDao;
        public IStaffWorkingConditionsDao StaffWorkingConditionsDao
        {
            get { return Validate.Dependency(staffworkingConditionsDao); }
            set { staffworkingConditionsDao = value; }
        }

        protected IScheduleDao scheduleDao;
        public IScheduleDao ScheduleDao
        {
            get { return Validate.Dependency(scheduleDao); }
            set { scheduleDao = value; }
        }

        protected IDocumentApprovalDao documentapprovalDao;
        public IDocumentApprovalDao DocumentApprovalDao
        {
            get { return Validate.Dependency(documentapprovalDao); }
            set { documentapprovalDao = value; }
        }

        protected IStaffRequestPyrusTasksDao staffrequestPyrusTasksDao;
        public IStaffRequestPyrusTasksDao StaffRequestPyrusTasksDao
        {
            get { return Validate.Dependency(staffrequestPyrusTasksDao); }
            set { staffrequestPyrusTasksDao = value; }
        }

        protected IStaffEstablishedPostUserLinksDao staffestablishedPostUserLinksDao;
        public IStaffEstablishedPostUserLinksDao StaffEstablishedPostUserLinksDao
        {
            get { return Validate.Dependency(staffestablishedPostUserLinksDao); }
            set { staffestablishedPostUserLinksDao = value; }
        }

        protected IStaffExtraChargeActionsDao staffextraChargeActionsDao;
        public IStaffExtraChargeActionsDao StaffExtraChargeActionsDao
        {
            get { return Validate.Dependency(staffextraChargeActionsDao); }
            set { staffextraChargeActionsDao = value; }
        }

        protected IrefStaffMovementsTypesDao refstaffmovementsTypesDao;
        public IrefStaffMovementsTypesDao refStaffMovementsTypesDao
        {
            get { return Validate.Dependency(refstaffmovementsTypesDao); }
            set { refstaffmovementsTypesDao = value; }
        }
        
        #endregion

        #region Штатное расписание.
        /// <summary>
        /// Загружаем структуру по заданному коду подразделения и штатные единицы
        /// </summary>
        /// <param name="DepId">Код родительского подразделения</param>
        /// <returns></returns>
        public StaffListModel GetDepartmentStructureWithStaffPost(string DepId)
        {
            StaffListModel model = new StaffListModel();

            //если не определены права ничего не грузим
            if (string.IsNullOrEmpty(DepId)) return model;

            Department dep = DepartmentDao.GetByCode(DepId);
            int DepartmentId = dep.Id;
            int itemLevel = dep.ItemLevel.Value;
            bool IsSalaryEnable = AuthenticationService.CurrentUser.UserRole == UserRole.TaxCollector ? false : true;
            
            //достаем уровень подразделений и штатных единиц к ним
            //если на входе код подразделения 7 уровня, то надо достать должности и сотрудников
            if (itemLevel != 7)
            {
                model.EstablishedPosts = StaffEstablishedPostDao.GetStaffEstablishedPosts(DepartmentId, IsSalaryEnable);
                //уровень подразделений
                model.Departments = GetDepartmentListByParent(DepId, false)
                    .OrderBy(x => x.Priority)
                    .ToList();
                    
            }
            else
            {
                model.EstablishedPosts = StaffEstablishedPostDao.GetStaffEstablishedPosts(DepartmentId, IsSalaryEnable);
            }

            return model;
        }

        #region Построение дерева
        /// <summary>
        /// Подгружаем только подчиненые ветки на один уровень ниже. Процедура работает для построения дерева штатного расписания и штатной расстановки.
        /// </summary>
        /// <param name="DepId">Id родительского подразделения</param>
        /// <param name="IsParentDepOnly">Признак достать только родительское подазделение.</param>
        /// <returns></returns>
        public IList<StaffListDepartmentDto> GetDepartmentListByParent(string DepId, bool IsParentDepOnly)
        {
            //определяем подразделение по правам текущего пользователя для начальной загрузки страницы
            if (string.IsNullOrEmpty(DepId))
            {
                if (AuthenticationService.CurrentUser.UserRole == UserRole.OutsourcingManager || AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing
                    || AuthenticationService.CurrentUser.UserRole == UserRole.Inspector || AuthenticationService.CurrentUser.UserRole == UserRole.PersonnelManager
                    || AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantPersonnel || AuthenticationService.CurrentUser.UserRole == UserRole.Director
                    || AuthenticationService.CurrentUser.UserRole == UserRole.TaxCollector
                    || AuthenticationService.CurrentUser.Id == 6638 //|| AuthenticationService.CurrentUser.Id == 22821
                    || AuthenticationService.CurrentUser.Id == 664
                    || AuthenticationService.CurrentUser.Id == 24926 || AuthenticationService.CurrentUser.Id == 513)//временно открыт доступ 4 сотрудникам к всей структуре
                {
                    //DepId = "9900424";
                    //return DepartmentDao.LoadAll().Where(x => x.Code1C.ToString() == DepId).ToList();
                }
                else
                {
                    User cur = UserDao.Load(AuthenticationService.CurrentUser.Id);
                    if (string.IsNullOrEmpty(DepId))
                        IsParentDepOnly = true;

                    DepId = (cur == null || cur.Department == null ? null : cur.Department.Code1C.ToString());
                    
                }

                return GetDepListWithSEPCount(DepId, IsParentDepOnly);
            }

            return GetDepListWithSEPCount(DepId, IsParentDepOnly);
        }
        /// <summary>
        /// Достаем уровень подчиненных подразделений и дополнительно к подразделениям делаем подсчет количества штатных единиц.
        /// </summary>
        /// <param name="DepId">Id родительского подразделения.</param>
        /// <param name="IsParentDepOnly">Признак достать только родительское подазделение.</param>
        /// <returns></returns>
        protected IList<StaffListDepartmentDto> GetDepListWithSEPCount(string DepId, bool IsParentDepOnly)
        {
            IList<StaffListDepartmentDto> Sdeps = DepartmentDao.DepFingradName(DepId, IsParentDepOnly, AuthenticationService.CurrentUser.UserRole);
            return Sdeps;
        }
        #endregion

        #region Заявки для подразделений
        /// <summary>
        /// Загрузка запросной формы реестра заявок подразделений.
        /// </summary>
        /// <returns></returns>
        public StaffDepartmentRequestListModel GetStaffDepartmentRequestList()
        {
            StaffDepartmentRequestListModel model = new StaffDepartmentRequestListModel();
            DateTime today = DateTime.Today;
            model.DateBegin = new DateTime(today.Year, today.Month, 1);
            model.DateEnd = today;
            model.Statuses = GetDepRequestStatuses();
            model.RequestTypes = StaffDepartmentRequestTypesDao.LoadAll();
            model.RequestTypes.Insert(0, new StaffDepartmentRequestTypes() { Id = 0, Name = "Все" });

            return model;
        }
        /// <summary>
        /// Загрузка реестра заявок подразделений.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public StaffDepartmentRequestListModel SetStaffDepartmentRequestList(StaffDepartmentRequestListModel model)
        {
            model.DepRequestList = StaffDepartmentRequestDao.GetDepartmentRequestList(userDao.Load(AuthenticationService.CurrentUser.Id), 
                AuthenticationService.CurrentUser.UserRole,
                model.DepartmentId, 
                model.Id.HasValue ? model.Id.Value : 0, 
                model.Creator, 
                model.DateBegin, 
                model.DateEnd, 
                model.StatusId,
                model.SortBy, 
                model.SortDescending,
                model.RequestTypeId);

            model.Statuses = GetDepRequestStatuses();
            model.RequestTypes = StaffDepartmentRequestTypesDao.LoadAll();
            model.RequestTypes.Insert(0, new StaffDepartmentRequestTypes() { Id = 0, Name = "Все" });

            return model;
        }
        /// <summary>
        /// Заполняем модель заявки для подразделения.
        /// </summary>
        /// <param name="model">Модель заявки.</param>
        /// <returns></returns>
        public StaffDepartmentRequestModel GetDepartmentRequest(StaffDepartmentRequestModel model)
        {
            //готовая заявка грузиться из реестра
            //создание заявки на создание подразделения, идет по ветке где Id = 0
            //создание заявки на изменение/удаление, нужно заполнить текущими даными, но с обнуленным Id, чтобы создалась новая заявка
            //для заявок на редактирование надо сделать поиск текущей заявки по Id подразделения, для того чтобы ее заполнить только данными, а Id самой заявки обнулить, чтобы запись добавилась, а не редактировалась
            bool IsRequestExists = model.Id == 0 ? false : true;
            if (model.Id == 0)
                model.Id = StaffDepartmentRequestDao.GetCurrentRequestId(model.DepartmentId);

            //заполняем заявку на все случаи жизни
            if (model.Id == 0)
            {
                //Общие реквизиты
                model.DateState = null;
                model.DepartmentId = model.RequestTypeId == 1 ? 0 : model.DepartmentId;
                model.ParentId = model.RequestTypeId != 1 ? DepartmentDao.GetByCode(DepartmentDao.Load(model.DepartmentId).ParentId.ToString()).Id : model.ParentId;
                model.DepParentName = model.RequestTypeId != 1 ? DepartmentDao.GetByCode(DepartmentDao.Load(model.DepartmentId).ParentId.ToString()).Name : DepartmentDao.Get(model.ParentId).Name;
                model.ItemLevel = model.RequestTypeId == 1 ? DepartmentDao.Load(model.ParentId).ItemLevel + 1 : DepartmentDao.Load(model.DepartmentId).ItemLevel;
                model.Name = model.RequestTypeId == 1 ? string.Empty : DepartmentDao.Load(model.DepartmentId).Name;//string.Empty;
                model.BFGId = 0;
                model.OrderNumber = string.Empty;
                model.OrderDate = null;
                model.LegalAddressId = 0;
                model.LegalAddress = string.Empty;
                model.IsTaxAdminAccount = false;
                model.IsEmployeAvailable = false;
                model.DepNextId = 0;
                model.IsPlan = false;

                StaffDepartmentFingradStructureDto FinStructure = StaffDepartmentRPLinkDao.GetFingradStructureForDeparment(model.ParentId);
                if (FinStructure != null)
                {
                    model.ManagementCode = FinStructure.ManagementCode;
                    model.ManagementName = FinStructure.ManagementName;
                    model.AdminCode = FinStructure.AdminCode;
                    model.AdminName = FinStructure.AdminName;
                    model.BGCode = FinStructure.BGCode;
                    model.BGName = FinStructure.BGName;
                    model.RPLInkCode = FinStructure.RPLinkCode;
                    model.RPLInkName = FinStructure.RPLinkName;
                }


                //налоговые реквизиты
                model.KPP = string.Empty;
                model.OKTMO = string.Empty;
                model.OKATO = string.Empty;
                model.RegionCode = string.Empty;
                model.TaxAdminCode = string.Empty;
                model.TaxAdminName = string.Empty;
                model.PostAddress = string.Empty;

                //ЦБ реквизиты
                model.ATMCountTotal = 0;
                model.ATMCashInStarted = 0;
                model.ATMCashInCount = 0;
                model.ATMCount = 0;
                model.DepCachinId = 0;
                model.DepATMId = 0;
                model.CashInStartedDate = null;
                model.ATMStartedDate = null;

                //Управленческие реквизиты
                model.NameShort = string.Empty;
                model.DepCode = string.Empty;
                model.PrevDepCode = string.Empty;
                model.ReasonId = 0;
                model.FactAddressId = 0;
                model.DepStatus = string.Empty;
                model.DepTypeId = 0;
                model.SKB_GE_Id = 0;
                model.OpenDate = null;
                model.CloseDate = null;
                model.OperationMode = string.Empty;
                model.OperationModeCash = string.Empty;
                model.OperationModeATM = string.Empty;
                model.OperationModeCashIn = string.Empty;
                model.BeginIdleDate = null;
                model.EndIdleDate = null;
                model.RentPlaceId = 0;
                model.AgreementDetails = string.Empty;
                model.DivisionArea = 0;
                model.AmountPayment = 0;
                model.Phone = string.Empty;
                model.IsBlocked = false;
                model.NetShopId = 0;
                model.IsLegalEntity = false;
                model.PlanEPCount = 0;
                model.PlanSalaryFund = 0; model.Note = string.Empty;
                model.CDAvailableId = 0;

                LoadDictionaries(model);

                //кнопки
                //сохранение/черновик
                if (UserRole.Manager == AuthenticationService.CurrentUser.UserRole || UserRole.Inspector == AuthenticationService.CurrentUser.UserRole
                    || UserRole.ConsultantOutsourcing == AuthenticationService.CurrentUser.UserRole || UserRole.ConsultantPersonnel == AuthenticationService.CurrentUser.UserRole)
                    model.IsDraftButtonAvailable = true;

                model.IsAgreeButtonAvailable = false;
            }
            else
            {

                StaffDepartmentRequest entity = StaffDepartmentRequestDao.Get(model.Id);
                if (entity == null) //если нет заявки с таким идентификатором, грузим новую заявку на создание подразделения
                {
                    model.Id = 0;
                    return GetDepartmentRequest(model);
                }

                if (!IsRequestExists)
                {
                    model.Id = 0;
                }

                //Общие реквизиты
                model.DateRequest = entity.DateRequest;
                model.UserId = entity.Creator != null ? entity.Creator.Id : 0;
                model.DateState = entity.DateState;
                model.DepartmentId = entity.Department != null ? entity.Department.Id : 0;
                model.ParentId = entity.ParentDepartment != null ? entity.ParentDepartment.Id : 0;
                model.DepParentName = entity.ParentDepartment != null ? entity.ParentDepartment.Name : string.Empty;
                model.ItemLevel = entity.ItemLevel;
                model.Name = entity.Name;
                model.BFGId = entity.DepartmentAccessory != null ? entity.DepartmentAccessory.Id : 0;
                model.OrderNumber = entity.OrderNumber;
                model.OrderDate = entity.OrderDate;
                model.BeginAccountDate = entity.BeginAccountDate;
                if (entity.LegalAddress != null)
                {
                    model.LegalAddressId = entity.LegalAddress.Id;
                    model.LegalAddress = entity.LegalAddress.Address;
                    model.LegalPostIndex = entity.LegalAddress.PostIndex;
                    model.LegalRegionCode = entity.LegalAddress.RegionCode;
                    model.LegalAreaCode = entity.LegalAddress.AreaCode;
                    model.LegalCityCode = entity.LegalAddress.CityCode;
                    model.LegalSettlementCode = entity.LegalAddress.SettlementCode;
                    model.LegalStreetCode = entity.LegalAddress.StreetCode;
                    model.LegalHouseType = entity.LegalAddress.HouseType;
                    model.LegalHouseNumber = entity.LegalAddress.HouseNumber;
                    model.LegalBuildType = entity.LegalAddress.BuildType;
                    model.LegalBuildNumber = entity.LegalAddress.BuildNumber;
                    model.LegalFlatType = entity.LegalAddress.FlatType;
                    model.LegalFlatNumber = entity.LegalAddress.FlatNumber;
                }
                model.IsTaxAdminAccount = entity.IsTaxAdminAccount;
                model.IsEmployeAvailable = entity.IsEmployeAvailable;
                model.DepNextId = entity.DepNext != null ? entity.DepNext.Id : 0;
                model.DepNextName = entity.DepNext != null ? entity.DepNext.Name : string.Empty;
                model.DepDepositId = entity.DepDeposit != null ? entity.DepDeposit.Id : 0;
                model.DepDepositName = entity.DepDeposit != null ? entity.DepDeposit.Name : string.Empty;
                model.IsPlan = entity.IsPlan;
                model.IsUsed = entity.IsUsed;
                model.IsTaxRequest = entity.IsTaxRequest;

                StaffDepartmentFingradStructureDto FinStructure = StaffDepartmentRPLinkDao.GetFingradStructureForDeparment(model.ParentId);
                if (FinStructure != null)
                {
                    model.ManagementCode = FinStructure.ManagementCode;
                    model.ManagementName = FinStructure.ManagementName;
                    model.AdminCode = FinStructure.AdminCode;
                    model.AdminName = FinStructure.AdminName;
                    model.BGCode = FinStructure.BGCode;
                    model.BGName = FinStructure.BGName;
                    model.RPLInkCode = FinStructure.RPLinkCode;
                    model.RPLInkName = FinStructure.RPLinkName;
                }

                //налоговые реквизиты
                if (entity.Department != null)
                {
                    //entity.Department
                    StaffDepartmentTaxDetails dt = StaffDepartmentTaxDetailsDao.GetDetailsByDepartmentId(entity.Department);
                    model.KPP = dt != null ? dt.KPP : string.Empty;
                    model.OKTMO = dt != null ? dt.OKTMO : string.Empty;
                    model.OKATO = dt != null ? dt.OKATO : string.Empty;
                    model.OKPO = dt != null ? dt.OKPO : string.Empty;
                    model.RegionCode = dt != null ? dt.RegionCode : string.Empty;
                    model.TaxAdminCode = dt != null ? dt.TaxAdminCode : string.Empty;
                    model.TaxAdminName = dt != null ? dt.TaxAdminName : string.Empty;
                    model.PostAddress = dt != null ? dt.PostAddress : string.Empty;
                }

                //ЦБ реквизиты
                if (entity.DepartmentCBDetails.Count != 0)
                {
                    StaffDepartmentCBDetails cbd = entity.DepartmentCBDetails[0];
                    model.ATMCountTotal = cbd.ATMCountTotal.HasValue ? cbd.ATMCountTotal.Value : 0;
                    model.ATMCashInStarted = cbd.ATMCashInStarted.HasValue ? cbd.ATMCashInStarted.Value : 0;
                    model.ATMCashInCount = cbd.ATMCashInCount.HasValue ? cbd.ATMCashInCount.Value : 0;
                    model.ATMCount = cbd.ATMCount.HasValue ? cbd.ATMCount.Value : 0;
                    model.DepCachinId = cbd.DepCashin != null ? cbd.DepCashin.Id : 0;
                    model.DepATMId = cbd.DepATM != null ? cbd.DepATM.Id : 0;
                    model.DepCachinName = cbd.DepCashin != null ? cbd.DepCashin.Name : string.Empty;
                    model.DepATMName = cbd.DepATM != null ? cbd.DepATM.Name : string.Empty;
                    model.CashInStartedDate = cbd.CashInStartedDate;
                    model.ATMStartedDate = cbd.ATMStartedDate;
                }

                //Управленческие реквизиты
                if (entity.DepartmentManagerDetails.Count != 0)
                {
                    StaffDepartmentManagerDetails dmd = entity.DepartmentManagerDetails[0];
                    model.DMDetailId = dmd.Id;
                    model.NameShort = dmd.NameShort;
                    model.DepCode = dmd.DepCode;
                    model.PrevDepCode = dmd.PrevDepCode;
                    model.ReasonId = dmd.DepartmentReasons != null ? dmd.DepartmentReasons.Id : 0;
                    model.ReasonIdOld = dmd.DepartmentReasons != null ? dmd.DepartmentReasons.Id : 0;
                    if (dmd.FactAddress != null)
                    {
                        model.FactAddressId = dmd.FactAddress.Id;
                        model.FactAddress = dmd.FactAddress.Address;
                        model.FactPostIndex = dmd.FactAddress.PostIndex;
                        model.FactRegionCode = dmd.FactAddress.RegionCode;
                        model.FactAreaCode = dmd.FactAddress.AreaCode;
                        model.FactCityCode = dmd.FactAddress.CityCode;
                        model.FactSettlementCode = dmd.FactAddress.SettlementCode;
                        model.FactStreetCode = dmd.FactAddress.StreetCode;
                        model.FactHouseType = dmd.FactAddress.HouseType;
                        model.FactHouseNumber = dmd.FactAddress.HouseNumber;
                        model.FactBuildType = dmd.FactAddress.BuildType;
                        model.FactBuildNumber = dmd.FactAddress.BuildNumber;
                        model.FactFlatType = dmd.FactAddress.FlatType;
                        model.FactFlatNumber = dmd.FactAddress.FlatNumber;
                    }

                    model.DepStatus = dmd.DepStatus;
                    model.DepTypeId = dmd.DepartmentType != null ? dmd.DepartmentType.Id : 0;
                    model.DepTypeIdOld = dmd.DepartmentType != null ? dmd.DepartmentType.Id : 0;
                    model.SKB_GE_Id = dmd.SKB_GE != null ? dmd.SKB_GE.Id : 0;
                    model.OpenDate = dmd.OpenDate;
                    model.CloseDate = dmd.CloseDate;
                    model.OperationMode = dmd.OperationMode;
                    model.OperationModeCash = dmd.OperationModeCash;
                    model.OperationModeATM = dmd.OperationModeATM;
                    model.OperationModeCashIn = dmd.OperationModeCashIn;
                    model.BeginIdleDate = dmd.BeginIdleDate;
                    model.EndIdleDate = dmd.EndIdleDate;
                    model.RentPlaceId = dmd.RentPlace != null ? dmd.RentPlace.Id : 0;
                    model.AgreementDetails = dmd.AgreementDetails;
                    model.DivisionArea = dmd.DivisionArea;
                    model.AmountPayment = dmd.AmountPayment;
                    model.Phone = dmd.Phone;
                    model.IsBlocked = dmd.IsBlocked;
                    model.NetShopId = dmd.NetShopIdentification != null ? dmd.NetShopIdentification.Id : 0;
                    model.IsLegalEntity = dmd.IsLegalEntity;
                    model.PlanEPCount = dmd.PlanEPCount;
                    model.PlanSalaryFund = dmd.PlanSalaryFund;
                    model.Note = dmd.Note;
                    model.CDAvailableId = dmd.CashDeskAvailable != null ? dmd.CashDeskAvailable.Id : 0;
                    model.OperGroupId = dmd.DepartmentOperationGroup != null ? dmd.DepartmentOperationGroup.Id : 0;
                }

                model.PyrusNumber = "";
                //заполнение справочников
                LoadDictionaries(model);

                //кнопки
                //сохранение/черновик
                if (UserRole.Manager == AuthenticationService.CurrentUser.UserRole || UserRole.Inspector == AuthenticationService.CurrentUser.UserRole
                    || UserRole.ConsultantOutsourcing == AuthenticationService.CurrentUser.UserRole || UserRole.ConsultantPersonnel == AuthenticationService.CurrentUser.UserRole)
                    model.IsDraftButtonAvailable = true;
                else
                    model.IsDraftButtonAvailable = false;
                
            }

            //model.IsTaxCollector = AuthenticationService.CurrentUser.UserRole == UserRole.TaxCollector;
           
            return model;
        }
        /// <summary>
        /// Процедура сохранения новой заявки для подразделения.
        /// </summary>
        /// <param name="model">Модель заявки.</param>
        /// <param name="error">Сообщенио об ошибке.</param>
        /// <returns></returns>
        public bool SaveNewDepartmentRequest(StaffDepartmentRequestModel model, out string error)
        {
            error = string.Empty;
            StaffDepartmentRequest entity;// = StaffDepartmentRequestDao.Load(model.Id.HasValue ? model.Id.Value : 0);
            User curUser = UserDao.Load(AuthenticationService.CurrentUser.Id);

            if (model.Id == 0)
            {
                //поля общих реквизитов
                entity = new StaffDepartmentRequest
                {
                    RequestType = StaffDepartmentRequestTypesDao.Load(model.RequestTypeId),
                    DateRequest = DateTime.Now,
                    Department = model.DepartmentId == 0 ? null : DepartmentDao.Get(model.DepartmentId),
                    ParentDepartment = model.ParentId == 0 ? null : DepartmentDao.Load(model.ParentId),
                    DepNext = model.DepNextId == 0 ? null : DepartmentDao.Load(model.DepNextId),
                    DepDeposit = model.DepDepositId == 0 ? null : DepartmentDao.Load(model.DepDepositId),
                    ItemLevel = model.ItemLevel.Value,
                    Name = model.Name,
                    DepartmentAccessory = model.BFGId == 0 ? null : StaffDepartmentAccessoryDao.Load(model.BFGId),
                    OrderNumber = model.OrderNumber,
                    OrderDate = model.OrderDate,
                    BeginAccountDate = model.BeginAccountDate,
                    IsTaxAdminAccount = model.IsTaxAdminAccount,
                    IsEmployeAvailable = model.IsEmployeAvailable,
                    IsPlan = model.IsPlan,
                    IsUsed = false,
                    IsDraft = true,
                    IsTaxRequest = false,
                    Creator = curUser,
                    CreateDate = DateTime.Now
                };

                //юридический адрес
                if (!string.IsNullOrEmpty(model.LegalAddress))
                {
                    RefAddresses la = new RefAddresses();
                    la.Address = model.LegalAddress;
                    la.PostIndex = model.LegalPostIndex;
                    la.RegionCode = model.LegalRegionCode;
                    if (!string.IsNullOrEmpty(model.LegalRegionCode))
                    {
                        KladrDto kd = KladrDao.GetKladrByCode(model.LegalRegionCode).Single();
                        la.RegionName = kd.Name + " " + kd.ShortName;
                    }
                    la.AreaCode = model.LegalAreaCode;
                    if (!string.IsNullOrEmpty(model.LegalAreaCode))
                    {
                        KladrDto kd = KladrDao.GetKladrByCode(model.LegalAreaCode).Single();
                        la.AreaName = kd.Name + " " + kd.ShortName;
                    }
                    la.CityCode = model.LegalCityCode;
                    if (!string.IsNullOrEmpty(model.LegalCityCode))
                    {
                        KladrDto kd = KladrDao.GetKladrByCode(model.LegalCityCode).Single();
                        la.CityName = kd.Name + " " + kd.ShortName;
                    }
                    la.SettlementCode = model.LegalSettlementCode;
                    if (!string.IsNullOrEmpty(model.LegalSettlementCode))
                    {
                        KladrDto kd = KladrDao.GetKladrByCode(model.LegalSettlementCode).Single();
                        la.SettlementName = kd.Name + " " + kd.ShortName;
                    }
                    la.StreetCode = model.LegalStreetCode;
                    if (!string.IsNullOrEmpty(model.LegalStreetCode))
                    {
                        KladrDto kd = KladrDao.GetKladrByCode(model.LegalStreetCode).Single();
                        la.StreetName = kd.Name + " " + kd.ShortName;
                    }
                    la.HouseType = model.LegalHouseType;
                    la.HouseNumber = model.LegalHouseNumber;
                    la.BuildType = model.LegalBuildType;
                    la.BuildNumber = model.LegalBuildNumber;
                    la.FlatType = model.LegalFlatType;
                    la.FlatNumber = model.LegalFlatNumber;
                    la.IsUsed = true;
                    la.Creator = curUser;
                    la.CreateDate = DateTime.Now;

                    RefAddressesDao.SaveAndFlush(la);

                    entity.LegalAddress = la;
                }


                //entity.ParentDepartment = model.ParentId == 0 ? null : DepartmentDao.Load(model.ParentId);
                //entity.DepNext = model.DepNextId == 0 ? null : DepartmentDao.Load(model.DepNextId);                
                

                //поля ЦБ реквизитов
                entity.DepartmentCBDetails = new List<StaffDepartmentCBDetails>();
                entity.DepartmentCBDetails.Add(new StaffDepartmentCBDetails
                {
                    DepRequest = entity,
                    ATMCountTotal = model.ATMCountTotal,
                    ATMCashInStarted = model.ATMCashInStarted,
                    ATMCashInCount = model.ATMCashInCount,
                    ATMCount = model.ATMCount,
                    DepCashin = model.DepCachinId == 0 ? null : DepartmentDao.Load(model.DepCachinId),
                    DepATM = model.DepATMId == 0 ? null : DepartmentDao.Load(model.DepATMId),
                    CashInStartedDate = model.CashInStartedDate,
                    ATMStartedDate = model.ATMStartedDate,
                    Creator = curUser,
                    CreateDate = DateTime.Now
                });
                

                //поля управленческих реквизитов
                entity.DepartmentManagerDetails = new List<StaffDepartmentManagerDetails>();
                StaffDepartmentManagerDetails dmd = new StaffDepartmentManagerDetails();
                dmd.DepRequest = entity;
                dmd.NameShort = model.NameShort;
                dmd.DepartmentReasons = !model.ReasonId.HasValue || model.ReasonId.Value == 0 ? null : StaffDepartmentReasonsDao.Load(model.ReasonId.Value);

                //фактический адрес
                if (!string.IsNullOrEmpty(model.FactAddress))
                {
                    RefAddresses fa = new RefAddresses();
                    fa.Address = model.FactAddress;
                    fa.PostIndex = model.FactPostIndex;
                    fa.RegionCode = model.FactRegionCode;
                    if (!string.IsNullOrEmpty(model.FactRegionCode))
                    {
                        KladrDto kd = KladrDao.GetKladrByCode(model.FactRegionCode).Single();
                        fa.RegionName = kd.Name + " " + kd.ShortName;
                    }
                    fa.AreaCode = model.FactAreaCode;
                    if (!string.IsNullOrEmpty(model.FactAreaCode))
                    {
                        KladrDto kd = KladrDao.GetKladrByCode(model.FactAreaCode).Single();
                        fa.AreaName = kd.Name + " " + kd.ShortName;
                    }
                    fa.CityCode = model.FactCityCode;
                    if (!string.IsNullOrEmpty(model.FactCityCode))
                    {
                        KladrDto kd = KladrDao.GetKladrByCode(model.FactCityCode).Single();
                        fa.CityName = kd.Name + " " + kd.ShortName;
                    }
                    fa.SettlementCode = model.FactSettlementCode;
                    if (!string.IsNullOrEmpty(model.FactSettlementCode))
                    {
                        KladrDto kd = KladrDao.GetKladrByCode(model.FactSettlementCode).Single();
                        fa.SettlementName = kd.Name + " " + kd.ShortName;
                    }
                    fa.StreetCode = model.FactStreetCode;
                    if (!string.IsNullOrEmpty(model.FactStreetCode))
                    {
                        KladrDto kd = KladrDao.GetKladrByCode(model.FactStreetCode).Single();
                        fa.StreetName = kd.Name + " " + kd.ShortName;
                    }
                    fa.HouseType = model.FactHouseType;
                    fa.HouseNumber = model.FactHouseNumber;
                    fa.BuildType = model.FactBuildType;
                    fa.BuildNumber = model.FactBuildNumber;
                    fa.FlatType = model.FactFlatType;
                    fa.FlatNumber = model.FactFlatNumber;
                    fa.IsUsed = true;
                    fa.Creator = curUser;
                    fa.CreateDate = DateTime.Now;

                    RefAddressesDao.SaveAndFlush(fa);

                    dmd.FactAddress = fa;
                }

                dmd.DepStatus = model.DepStatus;
                dmd.DepartmentType = !model.DepTypeId.HasValue || model.DepTypeId.Value == 0 ? null : StaffDepartmentTypesDao.Load(model.DepTypeId.Value);
                dmd.SKB_GE = !model.SKB_GE_Id.HasValue || model.SKB_GE_Id.Value == 0 ? null : StaffDepartmentSKB_GEDao.Load(model.SKB_GE_Id.Value);
                dmd.OpenDate = model.OpenDate;
                dmd.CloseDate = model.CloseDate;
                dmd.OperationMode = model.OperationMode;
                dmd.OperationModeCash = model.OperationModeCash;
                dmd.OperationModeATM = model.OperationModeATM;
                dmd.OperationModeCashIn = model.OperationModeCashIn;
                dmd.BeginIdleDate = model.BeginIdleDate;
                dmd.EndIdleDate = model.EndIdleDate;
                dmd.RentPlace = !model.RentPlaceId.HasValue || model.RentPlaceId.Value == 0 ? null : StaffDepartmentRentPlaceDao.Load(model.RentPlaceId.Value);
                dmd.AgreementDetails = model.AgreementDetails;
                dmd.DivisionArea = model.DivisionArea;
                dmd.AmountPayment = model.AmountPayment;
                dmd.Phone = model.Phone;
                dmd.IsBlocked = model.IsBlocked;
                dmd.NetShopIdentification = !model.NetShopId.HasValue || model.NetShopId.Value == 0 ? null : StaffNetShopIdentificationDao.Load(model.NetShopId.Value);
                dmd.CashDeskAvailable = !model.CDAvailableId.HasValue || model.CDAvailableId.Value == 0 ? null : StaffDepartmentCashDeskAvailableDao.Load(model.CDAvailableId.Value);
                dmd.IsLegalEntity = model.IsLegalEntity;
                dmd.PlanEPCount = model.PlanEPCount;
                dmd.PlanSalaryFund = model.PlanSalaryFund;
                dmd.Note = model.Note;
                dmd.Creator = curUser;
                dmd.CreateDate = DateTime.Now;

                //режим работы
                dmd.DepOperationModes = new List<StaffDepartmentOperationModes>();
                if (model.OperationModes != null)
                {
                    foreach (var item in model.OperationModes)
                    {
                        dmd.DepOperationModes.Add(new StaffDepartmentOperationModes
                        {
                            DepartmentManagerDetail = dmd,
                            ModeType = item.ModeType,
                            WeekDay = item.WeekDay,
                            WorkBegin = item.WorkBegin,
                            WorkEnd = item.WorkEnd,
                            BreakBegin = item.BreakBegin,
                            BreakEnd = item.BreakEnd,
                            IsWorkDay = item.IsWorkDay,
                            Creator = curUser,
                            CreateDate = DateTime.Now
                        });
                    }
                }

                //операции
                dmd.DepartmentOperationGroup = model.OperGroupId == 0 ? null : StaffDepartmentOperationGroupsDao.Get(model.OperGroupId);
                //foreach (var item in model.Operations.Where(x => x.IsUsed == true))
                //{
                //    dmd.DepOperations.Add(new StaffDepartmentOperationLinks { 
                //        DepartmentManagerDetail = dmd, 
                //        DepartmentOperation = StaffDepartmentOperationsDao.Load(item.OperationId), 
                //        Creator = curUser, 
                //        CreateDate = DateTime.Now 
                //    });
                //}

                //коды программ
                //могут редактировать только администраторы ПО банка (переменная определяется в процедуре определения состояния согласования)
                if (model.IsSoftAdminApproveAvailable)
                {
                    dmd.ProgramCodes = new List<StaffProgramCodes>();
                    if (model.ProgramCodes != null)
                    {
                        foreach (var item in model.ProgramCodes.Where(x => x.Code != null))
                        {
                            dmd.ProgramCodes.Add(new StaffProgramCodes
                            {
                                DepartmentManagerDetail = dmd,
                                Program = StaffProgramReferenceDao.Load(item.ProgramId),
                                Code = item.Code,
                                Creator = curUser,
                                CreateDate = DateTime.Now
                            });
                        }
                    }
                }

                //ориентиры
                dmd.DepartmentLandmarks = new List<StaffDepartmentLandmarks>();
                if (model.DepLandmarks != null)
                {
                    foreach (var item in model.DepLandmarks.Where(x => x.Description != null))
                    {
                        dmd.DepartmentLandmarks.Add(new StaffDepartmentLandmarks
                        {
                            DepartmentManagerDetail = dmd,
                            LandmarkTypes = StaffLandmarkTypesDao.Load(item.LandmarkId),
                            Description = item.Description,
                            Creator = curUser,
                            CreateDate = DateTime.Now
                        });
                    }
                }

                //entity.DepartmentManagerDetails.Add(dmd);
                entity.DepartmentManagerDetails.Add(dmd);

                try
                {
                    StaffDepartmentRequestDao.SaveAndFlush(entity);
                    model.Id = entity.Id;
                    return true;
                }
                catch (Exception ex)
                {
                    StaffDepartmentRequestDao.RollbackTran();
                    RefAddressesDao.RollbackTran();
                    error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                    return false;
                }
            }
            //если не по той ветке пошли
            error = "Произошла ошибка при сохранении данных! Обратитесь к разработчикам!";
            return false;
        }
        /// <summary>
        /// Процедура сохранения существующей заявки для подразделения.
        /// </summary>
        /// <param name="model">Модель заявки.</param>
        /// <param name="error">Сообщенио об ошибке.</param>
        /// <returns></returns>
        public bool SaveEditDepartmentRequest(StaffDepartmentRequestModel model, out string error)
        {
            error = string.Empty;
            StaffDepartmentRequest entity = StaffDepartmentRequestDao.Get(model.Id);
            if (entity == null)
            {
                error = "Заявка не найдена! Обратитесь к разработчикам!";
                return false;
            }
            User curUser = UserDao.Load(AuthenticationService.CurrentUser.Id);

            //поля общих реквизитов
            entity.Name = model.Name;
            entity.DepartmentAccessory = model.BFGId == 0 ? null : StaffDepartmentAccessoryDao.Load(model.BFGId);
            entity.OrderNumber = model.OrderNumber;
            entity.OrderDate = model.OrderDate;
            entity.BeginAccountDate = model.BeginAccountDate;
            entity.IsTaxAdminAccount = model.IsTaxAdminAccount;
            entity.IsEmployeAvailable = model.IsEmployeAvailable;
            entity.IsPlan = model.IsPlan;
            entity.IsDraft = entity.IsUsed ? false : model.IsDraft;
            entity.Editor = curUser;
            entity.EditDate = DateTime.Now;
            entity.ParentDepartment = model.ParentId == 0 ? null : DepartmentDao.Load(model.ParentId);
            entity.DepNext = model.DepNextId == 0 ? null : DepartmentDao.Load(model.DepNextId);
            entity.DepDeposit = model.DepDepositId == 0 ? null : DepartmentDao.Load(model.DepDepositId);
            entity.IsTaxRequest = model.IsTaxRequest;

            //юридический адрес
            RefAddresses la = null;
            //создаем новую запись или редактируем старую
            if (!string.IsNullOrEmpty(model.LegalAddress) && entity.LegalAddress == null)
                la = new RefAddresses();
            else
            {
                if (entity.LegalAddress != null)
                    la = RefAddressesDao.Get(entity.LegalAddress.Id);
            }

            if (la != null)
            {
                la.Address = model.LegalAddress;
                la.PostIndex = model.LegalPostIndex;
                la.RegionCode = model.LegalRegionCode;
                if (!string.IsNullOrEmpty(model.LegalRegionCode))
                {
                    KladrDto kd = KladrDao.GetKladrByCode(model.LegalRegionCode).Single();
                    la.RegionName = kd.Name + " " + kd.ShortName;
                }
                la.AreaCode = model.LegalAreaCode;
                if (!string.IsNullOrEmpty(model.LegalAreaCode))
                {
                    KladrDto kd = KladrDao.GetKladrByCode(model.LegalAreaCode).Single();
                    la.AreaName = kd.Name + " " + kd.ShortName;
                }
                la.CityCode = model.LegalCityCode;
                if (!string.IsNullOrEmpty(model.LegalCityCode))
                {
                    KladrDto kd = KladrDao.GetKladrByCode(model.LegalCityCode).Single();
                    la.CityName = kd.Name + " " + kd.ShortName;
                }
                la.SettlementCode = model.LegalSettlementCode;
                if (!string.IsNullOrEmpty(model.LegalSettlementCode))
                {
                    KladrDto kd = KladrDao.GetKladrByCode(model.LegalSettlementCode).Single();
                    la.SettlementName = kd.Name + " " + kd.ShortName;
                }
                la.StreetCode = model.LegalStreetCode;
                if (!string.IsNullOrEmpty(model.LegalStreetCode))
                {
                    KladrDto kd = KladrDao.GetKladrByCode(model.LegalStreetCode).Single();
                    la.StreetName = kd.Name + " " + kd.ShortName;
                }
                la.HouseType = model.LegalHouseType;
                la.HouseNumber = model.LegalHouseNumber;
                la.BuildType = model.LegalBuildType;
                la.BuildNumber = model.LegalBuildNumber;
                la.FlatType = model.LegalFlatType;
                la.FlatNumber = model.LegalFlatNumber;
                la.IsUsed = true;
                if (la.Id == 0)
                {
                    la.Creator = curUser;
                    la.CreateDate = DateTime.Now;
                }
                else
                {
                    la.Editor = curUser;
                    la.EditDate = DateTime.Now;
                }

                RefAddressesDao.SaveAndFlush(la);

                entity.LegalAddress = la;
            }
            


            //поля ЦБ реквизитов
            if (entity.DepartmentCBDetails == null || entity.DepartmentCBDetails.Count == 0)//если в виду какого нить сбоя при первичном сохранении не добавлиась запись, то создаем ее
            {
                if (entity.DepartmentCBDetails == null)
                    entity.DepartmentCBDetails = new List<StaffDepartmentCBDetails>();

                entity.DepartmentCBDetails.Add(new StaffDepartmentCBDetails
                {
                    DepRequest = entity,
                    ATMCountTotal = model.ATMCountTotal,
                    ATMCashInStarted = model.ATMCashInStarted,
                    ATMCashInCount = model.ATMCashInCount,
                    ATMCount = model.ATMCount,
                    DepCashin = model.DepCachinId == 0 ? null : DepartmentDao.Load(model.DepCachinId),
                    DepATM = model.DepATMId == 0 ? null : DepartmentDao.Load(model.DepATMId),
                    CashInStartedDate = model.CashInStartedDate,
                    ATMStartedDate = model.ATMStartedDate,
                    Creator = curUser,
                    CreateDate = DateTime.Now
                });
            }
            else
            {
                entity.DepartmentCBDetails[0].ATMCountTotal = model.ATMCountTotal;
                entity.DepartmentCBDetails[0].ATMCashInStarted = model.ATMCashInStarted;
                entity.DepartmentCBDetails[0].ATMCashInCount = model.ATMCashInCount;
                entity.DepartmentCBDetails[0].ATMCount = model.ATMCount;
                entity.DepartmentCBDetails[0].DepCashin = model.DepCachinId == 0 ? null : DepartmentDao.Load(model.DepCachinId);
                entity.DepartmentCBDetails[0].DepATM = model.DepATMId == 0 ? null : DepartmentDao.Load(model.DepATMId);
                entity.DepartmentCBDetails[0].CashInStartedDate = model.CashInStartedDate;
                entity.DepartmentCBDetails[0].ATMStartedDate = model.ATMStartedDate;
                entity.DepartmentCBDetails[0].Editor = curUser;
                entity.DepartmentCBDetails[0].EditDate = DateTime.Now;
            }



            //поля управленческих реквизитов
            if (entity.DepartmentManagerDetails == null || entity.DepartmentManagerDetails.Count == 0) //если в виду какого нить сбоя при первичном сохранении не добавлиась управленческие реквизиты, то создаем их
            {
                if (entity.DepartmentManagerDetails == null)
                    entity.DepartmentManagerDetails = new List<StaffDepartmentManagerDetails>();

                StaffDepartmentManagerDetails dmd = new StaffDepartmentManagerDetails();
                dmd.DepRequest = entity;
                dmd.NameShort = model.NameShort;
                dmd.DepartmentReasons = model.ReasonId == 0 ? null : StaffDepartmentReasonsDao.Load(model.ReasonId.Value);

                //фактический адрес
                if (!string.IsNullOrEmpty(model.FactAddress))
                {
                    RefAddresses fa = new RefAddresses();
                    fa.Address = model.FactAddress;
                    fa.PostIndex = model.FactPostIndex;
                    fa.RegionCode = model.FactRegionCode;
                    if (!string.IsNullOrEmpty(model.FactRegionCode))
                    {
                        KladrDto kd = KladrDao.GetKladrByCode(model.FactRegionCode).Single();
                        fa.RegionName = kd.Name + " " + kd.ShortName;
                    }
                    fa.AreaCode = model.FactAreaCode;
                    if (!string.IsNullOrEmpty(model.FactAreaCode))
                    {
                        KladrDto kd = KladrDao.GetKladrByCode(model.FactAreaCode).Single();
                        fa.AreaName = kd.Name + " " + kd.ShortName;
                    }
                    fa.CityCode = model.FactCityCode;
                    if (!string.IsNullOrEmpty(model.FactCityCode))
                    {
                        KladrDto kd = KladrDao.GetKladrByCode(model.FactCityCode).Single();
                        fa.CityName = kd.Name + " " + kd.ShortName;
                    }
                    fa.SettlementCode = model.FactSettlementCode;
                    if (!string.IsNullOrEmpty(model.FactSettlementCode))
                    {
                        KladrDto kd = KladrDao.GetKladrByCode(model.FactSettlementCode).Single();
                        fa.SettlementName = kd.Name + " " + kd.ShortName;
                    }
                    fa.StreetCode = model.FactStreetCode;
                    if (!string.IsNullOrEmpty(model.FactStreetCode))
                    {
                        KladrDto kd = KladrDao.GetKladrByCode(model.FactStreetCode).Single();
                        fa.StreetName = kd.Name + " " + kd.ShortName;
                    }
                    fa.HouseType = model.FactHouseType;
                    fa.HouseNumber = model.FactHouseNumber;
                    fa.BuildType = model.FactBuildType;
                    fa.BuildNumber = model.FactBuildNumber;
                    fa.FlatType = model.FactFlatType;
                    fa.FlatNumber = model.FactFlatNumber;
                    fa.IsUsed = true;
                    fa.Creator = curUser;
                    fa.CreateDate = DateTime.Now;

                    RefAddressesDao.SaveAndFlush(fa);

                    dmd.FactAddress = fa;
                }
                

                dmd.DepStatus = model.DepStatus;
                dmd.DepartmentType = model.DepTypeId.Value == 0 ? null : StaffDepartmentTypesDao.Load(model.DepTypeId.Value);
                dmd.SKB_GE = model.SKB_GE_Id.Value == 0 ? null : StaffDepartmentSKB_GEDao.Load(model.SKB_GE_Id.Value);
                dmd.OpenDate = model.OpenDate;
                dmd.CloseDate = model.CloseDate;
                dmd.OperationMode = model.OperationMode;
                dmd.OperationModeCash = model.OperationModeCash;
                dmd.OperationModeATM = model.OperationModeATM;
                dmd.OperationModeCashIn = model.OperationModeCashIn;
                dmd.BeginIdleDate = model.BeginIdleDate;
                dmd.EndIdleDate = model.EndIdleDate;
                dmd.RentPlace = model.RentPlaceId.Value == 0 ? null : StaffDepartmentRentPlaceDao.Load(model.RentPlaceId.Value);
                dmd.AgreementDetails = model.AgreementDetails;
                dmd.DivisionArea = model.DivisionArea;
                dmd.AmountPayment = model.AmountPayment;
                dmd.Phone = model.Phone;
                dmd.IsBlocked = model.IsBlocked;
                dmd.NetShopIdentification = model.NetShopId.Value == 0 ? null : StaffNetShopIdentificationDao.Load(model.NetShopId.Value);
                dmd.CashDeskAvailable = model.CDAvailableId.Value == 0 ? null : StaffDepartmentCashDeskAvailableDao.Load(model.CDAvailableId.Value);
                dmd.IsLegalEntity = model.IsLegalEntity;
                dmd.PlanEPCount = model.PlanEPCount;
                dmd.PlanSalaryFund = model.PlanSalaryFund;
                dmd.Note = model.Note;
                dmd.Creator = curUser;
                dmd.CreateDate = DateTime.Now;

                //режим работы
                dmd.DepOperationModes = new List<StaffDepartmentOperationModes>();
                foreach (var item in model.OperationModes)
                {
                    dmd.DepOperationModes.Add(new StaffDepartmentOperationModes
                    {
                        DepartmentManagerDetail = dmd,
                        ModeType = item.ModeType,
                        WorkBegin = item.WorkBegin,
                        WorkEnd = item.WorkEnd,
                        BreakBegin = item.BreakBegin,
                        BreakEnd = item.BreakEnd,
                        IsWorkDay = item.IsWorkDay,
                        Creator = curUser,
                        CreateDate = DateTime.Now
                    });
                }

                //операции
                dmd.DepartmentOperationGroup = model.OperGroupId == 0 ? null : StaffDepartmentOperationGroupsDao.Get(model.OperGroupId);

                //коды программ
                dmd.ProgramCodes = new List<StaffProgramCodes>();
                foreach (var item in model.ProgramCodes.Where(x => x.Code != null))
                {
                    dmd.ProgramCodes.Add(new StaffProgramCodes
                    {
                        DepartmentManagerDetail = dmd,
                        Program = StaffProgramReferenceDao.Load(item.ProgramId),
                        Code = item.Code,
                        Creator = curUser,
                        CreateDate = DateTime.Now
                    });
                }

                //ориентиры
                dmd.DepartmentLandmarks = new List<StaffDepartmentLandmarks>();
                foreach (var item in model.DepLandmarks.Where(x => x.Description != null))
                {
                    dmd.DepartmentLandmarks.Add(new StaffDepartmentLandmarks
                    {
                        DepartmentManagerDetail = dmd,
                        LandmarkTypes = StaffLandmarkTypesDao.Load(item.LandmarkId),
                        Description = item.Description,
                        Creator = curUser,
                        CreateDate = DateTime.Now
                    });
                }

                //entity.DepartmentManagerDetails.Add(dmd);
                entity.DepartmentManagerDetails.Add(dmd);
            }
            else
            {
                entity.DepartmentManagerDetails[0].NameShort = model.NameShort;
                entity.DepartmentManagerDetails[0].DepartmentReasons = !model.ReasonId.HasValue || model.ReasonId == 0 ? null : StaffDepartmentReasonsDao.Load(model.ReasonId.Value);

                //фактический адрес
                RefAddresses fa = null;
                //создаем новую запись или рекдактируем старую
                if (!string.IsNullOrEmpty(model.FactAddress) && entity.DepartmentManagerDetails[0].FactAddress == null)
                    fa = new RefAddresses();
                else
                {
                    if (entity.DepartmentManagerDetails[0].FactAddress != null)
                        fa = RefAddressesDao.Get(entity.DepartmentManagerDetails[0].FactAddress.Id);
                }

                if (fa != null)
                {
                    fa.Address = model.FactAddress;
                    fa.PostIndex = model.FactPostIndex;
                    fa.RegionCode = model.FactRegionCode;
                    if (!string.IsNullOrEmpty(model.FactRegionCode))
                    {
                        KladrDto kd = KladrDao.GetKladrByCode(model.FactRegionCode).Single();
                        fa.RegionName = kd.Name + " " + kd.ShortName;
                    }
                    fa.AreaCode = model.FactAreaCode;
                    if (!string.IsNullOrEmpty(model.FactAreaCode))
                    {
                        KladrDto kd = KladrDao.GetKladrByCode(model.FactAreaCode).Single();
                        fa.AreaName = kd.Name + " " + kd.ShortName;
                    }
                    fa.CityCode = model.FactCityCode;
                    if (!string.IsNullOrEmpty(model.FactCityCode))
                    {
                        KladrDto kd = KladrDao.GetKladrByCode(model.FactCityCode).Single();
                        fa.CityName = kd.Name + " " + kd.ShortName;
                    }
                    fa.SettlementCode = model.FactSettlementCode;
                    if (!string.IsNullOrEmpty(model.FactSettlementCode))
                    {
                        KladrDto kd = KladrDao.GetKladrByCode(model.FactSettlementCode).Single();
                        fa.SettlementName = kd.Name + " " + kd.ShortName;
                    }
                    fa.StreetCode = model.FactStreetCode;
                    if (!string.IsNullOrEmpty(model.FactStreetCode))
                    {
                        KladrDto kd = KladrDao.GetKladrByCode(model.FactStreetCode).Single();
                        fa.StreetName = kd.Name + " " + kd.ShortName;
                    }
                    fa.HouseType = model.FactHouseType;
                    fa.HouseNumber = model.FactHouseNumber;
                    fa.BuildType = model.FactBuildType;
                    fa.BuildNumber = model.FactBuildNumber;
                    fa.FlatType = model.FactFlatType;
                    fa.FlatNumber = model.FactFlatNumber;
                    fa.IsUsed = true;
                    if (fa.Id == 0)
                    {
                        fa.Creator = curUser;
                        fa.CreateDate = DateTime.Now;
                    }
                    else
                    {
                        fa.Editor = curUser;
                        fa.EditDate = DateTime.Now;
                    }

                    RefAddressesDao.SaveAndFlush(fa);

                    entity.DepartmentManagerDetails[0].FactAddress = fa;
                }

                entity.DepartmentManagerDetails[0].DepStatus = model.DepStatus;
                entity.DepartmentManagerDetails[0].DepartmentType = !model.DepTypeId.HasValue || model.DepTypeId.Value == 0 ? null : StaffDepartmentTypesDao.Load(model.DepTypeId.Value);
                entity.DepartmentManagerDetails[0].SKB_GE = !model.SKB_GE_Id.HasValue || model.SKB_GE_Id.Value == 0 ? null : StaffDepartmentSKB_GEDao.Load(model.SKB_GE_Id.Value);
                entity.DepartmentManagerDetails[0].OpenDate = model.OpenDate;
                entity.DepartmentManagerDetails[0].CloseDate = model.CloseDate;
                entity.DepartmentManagerDetails[0].OperationMode = model.OperationMode;
                entity.DepartmentManagerDetails[0].OperationModeCash = model.OperationModeCash;
                entity.DepartmentManagerDetails[0].OperationModeATM = model.OperationModeATM;
                entity.DepartmentManagerDetails[0].OperationModeCashIn = model.OperationModeCashIn;
                entity.DepartmentManagerDetails[0].BeginIdleDate = model.BeginIdleDate;
                entity.DepartmentManagerDetails[0].EndIdleDate = model.EndIdleDate;
                entity.DepartmentManagerDetails[0].RentPlace = !model.RentPlaceId.HasValue || model.RentPlaceId.Value == 0 ? null : StaffDepartmentRentPlaceDao.Load(model.RentPlaceId.Value);
                entity.DepartmentManagerDetails[0].AgreementDetails = model.AgreementDetails;
                entity.DepartmentManagerDetails[0].DivisionArea = model.DivisionArea;
                entity.DepartmentManagerDetails[0].AmountPayment = model.AmountPayment;
                entity.DepartmentManagerDetails[0].Phone = model.Phone;
                entity.DepartmentManagerDetails[0].IsBlocked = model.IsBlocked;
                entity.DepartmentManagerDetails[0].NetShopIdentification = !model.NetShopId.HasValue || model.NetShopId.Value == 0 ? null : StaffNetShopIdentificationDao.Load(model.NetShopId.Value);
                entity.DepartmentManagerDetails[0].CashDeskAvailable = !model.CDAvailableId.HasValue || model.CDAvailableId.Value == 0 ? null : StaffDepartmentCashDeskAvailableDao.Load(model.CDAvailableId.Value);
                entity.DepartmentManagerDetails[0].IsLegalEntity = model.IsLegalEntity;
                entity.DepartmentManagerDetails[0].PlanEPCount = model.PlanEPCount;
                entity.DepartmentManagerDetails[0].PlanSalaryFund = model.PlanSalaryFund;
                entity.DepartmentManagerDetails[0].Note = model.Note;
                entity.DepartmentManagerDetails[0].Editor = curUser;
                entity.DepartmentManagerDetails[0].EditDate = DateTime.Now;


                //режим работы
                if (entity.DepartmentManagerDetails[0].DepOperationModes == null)
                    entity.DepartmentManagerDetails[0].DepOperationModes = new List<StaffDepartmentOperationModes>();

                if (model.OperationModes != null)
                {
                    foreach (var item in model.OperationModes)
                    {
                        StaffDepartmentOperationModes dom = new StaffDepartmentOperationModes();
                        //если не было, добавляем
                        if (item.Id == 0)
                        {
                            dom.DepartmentManagerDetail = entity.DepartmentManagerDetails[0];
                            dom.ModeType = item.ModeType;
                            dom.WeekDay = item.WeekDay;
                            dom.WorkBegin = item.WorkBegin;
                            dom.WorkEnd = item.WorkEnd;
                            dom.BreakBegin = item.BreakBegin;
                            dom.BreakEnd = item.BreakEnd;
                            dom.IsWorkDay = item.IsWorkDay;
                            dom.Creator = curUser;
                            dom.CreateDate = DateTime.Now;

                            entity.DepartmentManagerDetails[0].DepOperationModes.Add(dom);
                        }
                        else//редактируем
                        {
                            entity.DepartmentManagerDetails[0].DepOperationModes.Where(x => x.Id == item.Id).Single().WorkBegin = item.WorkBegin;
                            entity.DepartmentManagerDetails[0].DepOperationModes.Where(x => x.Id == item.Id).Single().WorkEnd = item.WorkEnd;
                            entity.DepartmentManagerDetails[0].DepOperationModes.Where(x => x.Id == item.Id).Single().BreakBegin = item.BreakBegin;
                            entity.DepartmentManagerDetails[0].DepOperationModes.Where(x => x.Id == item.Id).Single().BreakEnd = item.BreakEnd;
                            entity.DepartmentManagerDetails[0].DepOperationModes.Where(x => x.Id == item.Id).Single().IsWorkDay = item.IsWorkDay;
                            entity.DepartmentManagerDetails[0].DepOperationModes.Where(x => x.Id == item.Id).Single().Editor = curUser;
                            entity.DepartmentManagerDetails[0].DepOperationModes.Where(x => x.Id == item.Id).Single().EditDate = DateTime.Now;
                        }
                    }
                }

                //операции
                entity.DepartmentManagerDetails[0].DepartmentOperationGroup = model.OperGroupId == 0 ? null : StaffDepartmentOperationGroupsDao.Get(model.OperGroupId);

                //коды программ
                //могут редактировать только администраторы ПО банка (переменная определяется в процедуре определения состояния согласования)
                if (model.IsSoftAdminApproveAvailable)
                {
                    if (entity.DepartmentManagerDetails[0].ProgramCodes == null)
                        entity.DepartmentManagerDetails[0].ProgramCodes = new List<StaffProgramCodes>();

                    if (model.ProgramCodes != null)
                    {
                        foreach (var item in model.ProgramCodes)
                        {
                            StaffProgramCodes pc = new StaffProgramCodes();

                            //если была запись и убрали значение кода, то удаляем
                            if (item.Id != 0 && item.Code == null)
                            {
                                pc = entity.DepartmentManagerDetails[0].ProgramCodes.Where(x => x.Id == item.Id && x.Code != null).Single();
                                entity.DepartmentManagerDetails[0].ProgramCodes.Remove(pc);
                            }

                            //если не было записи и ввели код, то добавляем
                            if (item.Id == 0 && item.Code != null)
                            {
                                pc.DepartmentManagerDetail = entity.DepartmentManagerDetails[0];
                                pc.Program = StaffProgramReferenceDao.Load(item.ProgramId);
                                pc.Code = item.Code;
                                pc.Creator = curUser;
                                pc.CreateDate = DateTime.Now;

                                entity.DepartmentManagerDetails[0].ProgramCodes.Add(pc);
                            }

                            //запись была и есть код, то предпологаем, что это редактирование
                            if (item.Id != 0 && item.Code != null)
                            {
                                entity.DepartmentManagerDetails[0].ProgramCodes.Where(x => x.Id == item.Id).Single().Code = item.Code;
                                entity.DepartmentManagerDetails[0].ProgramCodes.Where(x => x.Id == item.Id).Single().Editor = curUser;
                                entity.DepartmentManagerDetails[0].ProgramCodes.Where(x => x.Id == item.Id).Single().EditDate = DateTime.Now;
                            }
                        }
                    }
                }
                


                //ориентиры
                if (entity.DepartmentManagerDetails[0].DepartmentLandmarks == null)
                    entity.DepartmentManagerDetails[0].DepartmentLandmarks = new List<StaffDepartmentLandmarks>();

                if (model.DepLandmarks != null)
                {
                    foreach (var item in model.DepLandmarks)
                    {
                        StaffDepartmentLandmarks lm = new StaffDepartmentLandmarks();

                        //если была запись и убрали значение кода, то удаляем
                        if (item.Id != 0 && item.Description == null)
                        {
                            lm = entity.DepartmentManagerDetails[0].DepartmentLandmarks.Where(x => x.Id == item.Id && x.Description != null).Single();
                            entity.DepartmentManagerDetails[0].DepartmentLandmarks.Remove(lm);
                        }

                        //если не было записи и ввели код, то добавляем
                        if (item.Id == 0 && item.Description != null)
                        {
                            lm.DepartmentManagerDetail = entity.DepartmentManagerDetails[0];
                            lm.LandmarkTypes = StaffLandmarkTypesDao.Load(item.LandmarkId);
                            lm.Description = item.Description;
                            lm.Creator = curUser;
                            lm.CreateDate = DateTime.Now;

                            entity.DepartmentManagerDetails[0].DepartmentLandmarks.Add(lm);
                        }

                        //запись была и есть код, то предпологаем, что это редактирование
                        if (item.Id != 0 && item.Description != null)
                        {
                            entity.DepartmentManagerDetails[0].DepartmentLandmarks.Where(x => x.Id == item.Id).Single().Description = item.Description;
                            entity.DepartmentManagerDetails[0].DepartmentLandmarks.Where(x => x.Id == item.Id).Single().Editor = curUser;
                            entity.DepartmentManagerDetails[0].DepartmentLandmarks.Where(x => x.Id == item.Id).Single().EditDate = DateTime.Now;
                        }
                    }
                }
            }

            //находим действующую заявку и убираем у нее признак использования
            //int OldRequestId = StaffDepartmentRequestDao.GetCurrentRequestId(entity.Department != null ? entity.Department.Id : 0);
            StaffDepartmentRequest OldEntity = null;

            //Утверждение
            if (!model.IsDraft)
            {

                //проверки для текущей заявки 
                if (!ValidateDepartmentRequest(entity, out error))
                {
                    return false;
                }

                //только после утверждения заявки можно редактировать справочник подразделений.
                //ветка срабатывает только после последней фазы согласования (на данный момент это утверждение заявки)
                //пока данные корректируются после начальной загрузки, то для таких заявок согласования не нужно
                int Result = entity.RequestType.Id == 4 ? 0 : SaveDepartmentApprovals(model, entity, curUser, out error);
                if (Result == -1) return false;
                if (Result == 0)
                {
                    //для первоначальных данных
                    if (entity.RequestType.Id == 4)
                    {
                        entity.DateState = DateTime.Now;
                        entity.DateSendToApprove = DateTime.Now;
                    }

                    //занесение данных по подразделению в справочник подазделений и создание кодов для подразделений
                    if (!SaveDepartmentReference(entity, curUser, out error))
                    {
                        return false;
                    }

                    //находим действующую заявку и убираем у нее признак использования
                    int OldRequestId = StaffDepartmentRequestDao.GetCurrentRequestId(entity.Department != null ? entity.Department.Id : 0);
                    OldEntity = StaffDepartmentRequestDao.Get(OldRequestId);

                    //если заявка на изменение/удаление подразделения
                    if (entity.RequestType.Id != 1 && entity.RequestType.Id != 4)
                    {

                        if (OldEntity != null)
                        {
                            //если переезд, то у старой заявки ставим дату закрытия автоматически
                            OldEntity.DepartmentManagerDetails[0].CloseDate = DateTime.Now;
                            OldEntity.IsUsed = false;
                            OldEntity.Editor = curUser;
                            OldEntity.EditDate = DateTime.Now;
                            //записываем предыдущий код
                            entity.DepartmentManagerDetails[0].PrevDepCode = OldEntity.DepartmentManagerDetails[0].DepCode;
                        }
                    }

                    //у текущей заявки ставим признак использования
                    entity.IsUsed = true;
                    error = "Заявка утверждена!";
                }

                
            }


            if (model.Id != 0)
            {
                try
                {
                    if (OldEntity != null)
                        StaffDepartmentRequestDao.SaveAndFlush(OldEntity);

                    StaffDepartmentRequestDao.SaveAndFlush(entity);
                    model.Id = entity.Id;
                }
                catch (Exception ex)
                {
                    StaffDepartmentRequestDao.RollbackTran();
                    RefAddressesDao.RollbackTran();
                    error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                    return false;
                }

                return true;

            }

            //если не по той ветке пошли
            error = "Произошла ошибка при сохранении данных! Обратитесь к разработчикам!";
            return false;
        }
        /// <summary>
        /// Сохраняем изменения в справочнике подразделений.
        /// </summary>
        /// <param name="entity">Текущая заявка.</param>
        /// <param name="curUser">текущий пользователь.</param>
        /// <param name="error">Сообщение об ошибке.</param>
        /// <returns></returns>
        protected bool SaveDepartmentReference(StaffDepartmentRequest entity, User curUser, out string error)
        {
            error = string.Empty;

            if (entity.DepNext != null && entity.DepartmentAccessory.Id == 2)
            {
                if (string.IsNullOrEmpty(entity.DepNext.DepartmentTaxDetails[0].TaxAdminCode) || string.IsNullOrWhiteSpace(entity.DepNext.DepartmentTaxDetails[0].TaxAdminCode))
                {
                    error = "Выбранное подразделение с налоговыми реквизитами не имеет таковых!";
                    return false;
                }
            }


            Department dep = entity.Department != null ? DepartmentDao.Get(entity.Department.Id) : new Department();
            //родительское подразделение
            Department ParentDep = entity.ParentDepartment != null ? DepartmentDao.Get(entity.ParentDepartment.Id) : new Department();
            //если заявка на создание, создаем новую запись и делаем в заявке на нее ссылку
            if (entity.RequestType.Id == 1 || entity.RequestType.Id == 4)
            {
                dep.Code = null;
                dep.Name = entity.Name;
                dep.Code1C = null;
                dep.ParentId = ParentDep.Code1C;
                dep.Path = ParentDep.Path + "__new";
                dep.ItemLevel = (ParentDep.ItemLevel + 1) != entity.ItemLevel ? ParentDep.ItemLevel + 1 : entity.ItemLevel;
                dep.CodeSKD = null;
                dep.Priority = 99;//искусственное значение
                dep.IsUsed = true;
                dep.DepartmentAccessory = entity.DepartmentAccessory;
                //dep.FingradCode = "";//формируем код ниже
                dep.Creator = curUser;
                dep.CreateDate = DateTime.Now;
            }

            //если заявка на редактирование/удаление, редактируем текущую запись в справочнике
            if (entity.RequestType.Id != 1 && entity.RequestType.Id != 4)
            {
                if (entity.RequestType.Id == 2)
                {
                    dep.Name = entity.Name;
                    dep.ParentId = ParentDep.Code1C;
                    dep.Path = ParentDep.ItemLevel != entity.ItemLevel ? ParentDep.Path + "__new" : dep.Path;
                    dep.ItemLevel = (ParentDep.ItemLevel + 1) != entity.ItemLevel ? ParentDep.ItemLevel + 1 : entity.ItemLevel;
                    dep.IsUsed = true;
                }
                else if (entity.RequestType.Id == 3)
                {
                    dep.IsUsed = false; //делаем неактивной текущую запись в справочнике
                }
                dep.Editor = curUser;
                dep.EditDate = DateTime.Now;
            }


            try
            {
                DepartmentDao.SaveAndFlush(dep);

                if (entity.Department == null)
                    entity.Department = new Department();

                //если нет кода или изменился родитель, то надо подкорректировать путь
                dep.Path = ParentDep.Path + (dep.Code1C.HasValue ? dep.Code1C.Value.ToString() : dep.Id.ToString()) + ".";

                //только при добавлении надо заполнить эти поля, так как в структуре на поле Code1C ссылается поле ParentId, то есть значения в поле Code1C должны быть уникальными
                if (entity.RequestType.Id == 1 || entity.RequestType.Id == 4)
                {
                    dep.Code = dep.Id.ToString();
                    dep.Code1C = dep.Id;
                }

                //у текущей заявки делаем ссылку на новое подразделение и ставим признак использования
                entity.Department = dep;

                //создаем код для подразделения
                
                if (entity.RequestType.Id != 3)
                {
                    if (!CreateCodeForDepartment(entity, dep, curUser, out error))
                    {
                        error = "Произошла ошибка при формировании кода подразделения!";
                        DepartmentDao.RollbackTran();
                        DepartmentArchiveDao.RollbackTran();
                    }
                }

                //архивируем изменения
                DepartmentArchive da = new DepartmentArchive()
                {
                    Department = dep,
                    Code = dep.Code,
                    Name = dep.Name,
                    Code1C = dep.Code1C,
                    ParentId = dep.ParentId,
                    Path = dep.Path,
                    ItemLevel = dep.ItemLevel,
                    Priority = dep.Priority,
                    IsUsed = dep.IsUsed,
                    Creator = curUser,
                    CreateDate = DateTime.Now
                };

                DepartmentArchiveDao.SaveAndFlush(da);
            }
            catch (Exception ex)
            {
                DepartmentDao.RollbackTran();
                DepartmentArchiveDao.RollbackTran();
                error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }

            return true;
        }
        /// <summary>
        /// Сохраняем согласования.
        /// </summary>
        /// <param name="model">Модель</param>
        /// <param name="entity">Текущая заявка.</param>
        /// <param name="curUser">текущий пользователь.</param>
        /// <param name="error">Сообщение об ошибке.</param>
        /// <returns></returns>
        protected int SaveDepartmentApprovals(StaffDepartmentRequestModel model, StaffDepartmentRequest entity, User curUser, out string error)
        {
            //возвращает значения от 1 до 4 - этап согласования прошел успешно, 0 - согласование завершено, заявка утверждена, -1 - ошибка при сохранении данных

            //только после утверждения заявки можно редактировать справочник подразделений.
            error = string.Empty;


            //в каждой ветке определяемся с участниками групповухи под кодовым названием согласование/утверждение заявки для подразделения
            //текущим пользователем может быть куратор ии кадровик, которые могут действовать за согласовантов
            User Assistant = AuthenticationService.CurrentUser.UserRole == UserRole.Inspector
                || AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantPersonnel
                || AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing
                || AuthenticationService.CurrentUser.UserRole == UserRole.TaxCollector ? curUser : null;//куратор/кадровик банка/консультант РК

            //список руководителей по по ветке
            IList<User> Initiators = DepartmentDao.GetDepartmentManagers(entity.ParentDepartment.Id, true)
                .OrderByDescending<User, int?>(manager => manager.Level)
                .ToList<User>();

            bool IsInitiator = Initiators.Where(x => x.Id == AuthenticationService.CurrentUser.Id).Count() != 0 ? true : false;
            bool IsTopManager = Initiators.Where(x => x.Id == AuthenticationService.CurrentUser.Id && x.Level == 3).Count() != 0 ? true : false;
            bool IsBoardMember = AuthenticationService.CurrentUser.UserRole == UserRole.Director;
            bool IsCurator = (AuthenticationService.CurrentUser.UserRole == UserRole.Inspector);
            bool IsPersonnelBank = (AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantPersonnel);
            bool IsConsultant = (AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing);
            bool IsTaxCollector = (AuthenticationService.CurrentUser.UserRole == UserRole.TaxCollector);
            bool IsStaffListOrder = (AuthenticationService.CurrentUser.UserRole == UserRole.StaffListOrder);

            

            //вышестоящее руководство
            model.TopManagers = Initiators.Where(x => x.Level == 3).ToList().ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name + " - " + x.Position.Name });
            
            //выбираем из согласования не архивные записи.
            IList<DocumentApproval> DocApproval = DocumentApprovalDao.GetDocumentApproval(entity.Id, (int)ApprovalTypeEnum.StaffDepartmentRequest);
            DocumentApproval da = new DocumentApproval();

            da.ApprovalType = (int)ApprovalTypeEnum.StaffDepartmentRequest;
            da.DocId = entity.Id;
            da.IsArchive = false;
            da.CreateDate = DateTime.Now;

            if (model.IsImportance)//обязательное согласование
            {
                if (DocApproval.Where(x => x.Number == 1).Count() == 0 && (IsInitiator || IsCurator || IsPersonnelBank || IsConsultant))//инициатор, куратор, кадровик, консультант
                {
                    //если иницатор не выбран, это значит, что инициатор действует сам
                    User Initiator = model.InitiatorId != 0 ? UserDao.Get(model.InitiatorId) : curUser;//инициатор

                    da.ApproveUser = Initiator;
                    da.AssistantUser = Assistant;
                    da.Number = 1;
                    da.IsImportance = true;
                    error = "Заявка создана!";
                }

                if (DocApproval.Where(x => x.Number == 1).Count() == 1 && DocApproval.Where(x => x.Number == 2).Count() == 0 && (entity.DepartmentAccessory.Id == 2 || entity.DepartmentAccessory.Id == 6) && IsCurator)//только фронты и БэкФронты
                {
                    da.ApproveUser = curUser;
                    da.AssistantUser = null;
                    da.Number = 2;
                    da.IsImportance = true;
                    error = "Заявка проверена куратором!";
                }

                //if (DocApproval.Where(x => x.Number == 2).Count() == 1 && DocApproval.Where(x => x.Number == 3).Count() == 0)
                if (DocApproval.Where(x => x.Number == 1).Count() == 1 && DocApproval.Where(x => x.Number == 3).Count() == 0 && entity.DepartmentAccessory.Id == 1 && IsPersonnelBank)//только бэки
                {
                    da.ApproveUser = curUser;
                    da.AssistantUser = null;
                    da.Number = 3;
                    da.IsImportance = true;
                    error = "Заявка проверена кадровиком банка!";
                }

                if (DocApproval.Where(x => x.Number == 3 || x.Number == 2).Count() == 1 && DocApproval.Where(x => x.Number == 5).Count() == 0 && (IsTopManager || IsCurator || IsPersonnelBank || IsConsultant))//высший руководитель, куратор, кадровик, консультант
                {
                    //если согласовант не выбран, это значит, что он действует сам
                    User TopManager = model.TopManagerId != 0 ? UserDao.Get(model.TopManagerId) : curUser;//высший руководитель

                    da.ApproveUser = TopManager;
                    da.AssistantUser = Assistant;
                    da.Number = 5;
                    da.IsImportance = true;
                    error = "Заявка согласована!";
                }

                if (DocApproval.Where(x => x.Number == 5).Count() == 1 && DocApproval.Where(x => x.Number == 6).Count() == 0 && (IsBoardMember || IsCurator || IsPersonnelBank || IsConsultant))//член правления, куратор, кадровик, консультант
                {
                    //если утверждающий не выбран, это значит, что он действует сам
                    User BoardMember = model.BoardMemberId != 0 ? UserDao.Get(model.BoardMemberId) : curUser;//член правления

                    da.ApproveUser = BoardMember;
                    da.AssistantUser = Assistant;
                    da.IsImportance = true;
                    da.Number = 6;
                    error = "Заявка утверждена!";
                }

            }
            else   //согласование специалистами
            {
                if (DocApproval.Where(x => x.Number == 7).Count() == 0 && (IsStaffListOrder || IsConsultant))
                {
                    da.ApproveUser = curUser;
                    da.AssistantUser = null;
                    da.Number = 7;
                    error = "Приказ составлен!";
                }

                if (DocApproval.Where(x => x.Number == 4).Count() == 0 && (IsTaxCollector || IsConsultant))
                {
                    da.ApproveUser = curUser;
                    da.AssistantUser = null;
                    da.Number = 4;
                    error = "Заявка согласована налоговиком!";
                }

            }

            try
            {
                DocumentApprovalDao.SaveAndFlush(da);
                
                //сохраняем здачу пайруса
                if (!string.IsNullOrEmpty(model.PyrusNumber))
                {
                    entity.StaffRequestPyrusTasks.Add(new StaffRequestPyrusTasks()
                    {
                        DepRequest = entity,
                        DocumentApproval = da,
                        NumberTask = model.PyrusNumber,
                        Creator = curUser,
                        CreateDate = DateTime.Now
                    });
                }

                if (da.Number == 1)
                {
                    entity.DateSendToApprove = DateTime.Now;
                    return da.Number;
                }

                if (da.Number > 1 && da.Number < 6)
                    return da.Number;

                if (da.Number == 6)
                {
                    entity.DateState = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                DocumentApprovalDao.RollbackTran();
                error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                return -1;
            }

            return 0;
        }
        /// <summary>
        /// Сохраняем админами по банка коды.
        /// </summary>
        /// <param name="model">Модель</param>
        /// <param name="error">Сообщение</param>
        /// <returns></returns>
        public bool SaveProgramCodes(StaffDepartmentRequestModel model, out string error)
        {
            //этот кусок есть в общем сохранении, но когда решили дать админам отдельную кнопку, то вынес его сюда.
            error = string.Empty;
            User curUser = UserDao.Get(AuthenticationService.CurrentUser.Id);
            StaffDepartmentRequest entity = StaffDepartmentRequestDao.Get(model.Id);

            if (entity.DepartmentManagerDetails[0].ProgramCodes == null)
                entity.DepartmentManagerDetails[0].ProgramCodes = new List<StaffProgramCodes>();

            if (model.ProgramCodes != null)
            {
                foreach (var item in model.ProgramCodes)
                {
                    StaffProgramCodes pc = new StaffProgramCodes();

                    //если была запись и убрали значение кода, то удаляем
                    if (item.Id != 0 && item.Code == null)
                    {
                        pc = entity.DepartmentManagerDetails[0].ProgramCodes.Where(x => x.Id == item.Id && x.Code != null).Single();
                        entity.DepartmentManagerDetails[0].ProgramCodes.Remove(pc);
                    }

                    //если не было записи и ввели код, то добавляем
                    if (item.Id == 0 && item.Code != null)
                    {
                        pc.DepartmentManagerDetail = entity.DepartmentManagerDetails[0];
                        pc.Program = StaffProgramReferenceDao.Load(item.ProgramId);
                        pc.Code = item.Code;
                        pc.Creator = curUser;
                        pc.CreateDate = DateTime.Now;

                        entity.DepartmentManagerDetails[0].ProgramCodes.Add(pc);
                    }

                    //запись была и есть код, то предпологаем, что это редактирование
                    if (item.Id != 0 && item.Code != null)
                    {
                        entity.DepartmentManagerDetails[0].ProgramCodes.Where(x => x.Id == item.Id).Single().Code = item.Code;
                        entity.DepartmentManagerDetails[0].ProgramCodes.Where(x => x.Id == item.Id).Single().Editor = curUser;
                        entity.DepartmentManagerDetails[0].ProgramCodes.Where(x => x.Id == item.Id).Single().EditDate = DateTime.Now;
                    }
                }

                try
                {
                    StaffDepartmentRequestDao.SaveAndFlush(entity);
                    model.Id = entity.Id;
                    error = "Данные сохранены!";
                }
                catch (Exception ex)
                {
                    StaffDepartmentRequestDao.RollbackTran();
                    RefAddressesDao.RollbackTran();
                    error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                    return false;
                }

            }
            return true;
        }
        /// <summary>
        /// Загрузка реквизитов инициатора к заявкам для подразделений
        /// </summary>
        /// <param name="Id">Id заявки.</param>
        /// <param name="DateRequest">Дата составления заявки.</param>
        /// <param name="UserId">Id инициатора.</param>
        /// <returns></returns>
        protected DepRequestInfoModel GetDepRequestInfo(int Id, DateTime? DateRequest, int UserId)
        {
            DepRequestInfoModel model = new DepRequestInfoModel();
            User curUser = UserDao.Load(UserId != 0 ? UserId : AuthenticationService.CurrentUser.Id);
            model.DateRequest = DateRequest;
            model.Id = Id;
            model.DepartmentName = curUser.Department != null ? curUser.Department.Name : string.Empty;
            model.RequestInitiator = curUser.Name + " - " + (curUser.Position != null ? curUser.Position.Name : string.Empty);
            model.DepManager5 = GetManagers(curUser, 5);
            model.DepManager4 = GetManagers(curUser, 4);
            model.DepManager3 = GetManagers(curUser, 3);
            model.DepManager2 = GetManagers(curUser, 2);
            
            return model;
        }
        /// <summary>
        /// Проверка на возможность создать код Финград для подразделения в текущей заявке.
        /// </summary>
        /// <param name="entity">Проверяемая заявка.</param>
        /// <param name="error">Переменная для сообщений.</param>
        /// <returns></returns>
        protected bool ValidateDepartmentRequest(StaffDepartmentRequest entity, out string error)
        {
            error = string.Empty;

            if (entity.RequestType.Id != 3)//создание/редактирование
            {
                if (entity.DepartmentAccessory == null)
                {
                    error = "Укажите принадлежность подразделения в разделе с общими реквизитами!";
                    return false;
                }
                //проверка на возможность сформировать код для подразделения (только для фронтов)
                if (entity.DepartmentAccessory.Id == 2)
                {
                    if (!StaffDepartmentRequestDao.IsEnableCreateCode(entity.ParentDepartment.Id))
                    {
                        error = "Невозможно создать код для данного подразделения! Проверьте наличие кодов и связей для всей ветки подразделений в справочнике кодировок.";
                        return false;
                    }
                }
            }
            else//закрытие
            {
                //проверяем наличие сотрудников в подразделении по ветке вниз
                if (!StaffDepartmentRequestDao.IsEnableCloseDepartment(entity.Department.Id))
                {
                    error = "Данное подразделение нельзя закрыть, так как в нем работают сотрудники!";
                    return false;
                }

                if (entity.ItemLevel == 7 && !entity.DepartmentManagerDetails[0].CloseDate.HasValue)
                {
                    error = "Укажите дату закрытия подразделения!";
                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// Процедура создания кода для подразделения.
        /// </summary>
        /// <param name="entity">Заявка для подразделения</param>
        /// <param name="dep">Подразделение, для которго надо создать код.</param>
        /// <param name="curUser">Текущий пользователь.</param>
        /// <param name="error">Переменная для сообщений.</param>
        /// <returns></returns>
        protected bool CreateCodeForDepartment(StaffDepartmentRequest entity, Department dep, User curUser, out string error)
        {
            error = string.Empty;
            string Code = string.Empty;

            if (entity.ItemLevel == 1 || entity.ItemLevel > 7)
            {
                error = "Создание подразделения такого уровня не предусмотрено программой!";
                return true;
            }

            //в заявках на изменение код создается только для точек 7 иуровня
            if (entity.RequestType.Id == 2 && entity.ItemLevel <= 6)
            {
                return true;
            }

            StaffDepartmentBranch br = new StaffDepartmentBranch();
            StaffDepartmentManagement mn = new StaffDepartmentManagement();

            switch (entity.ItemLevel)
            {
                case 2://филиал
                    StaffDepartmentBranch Depbr = new StaffDepartmentBranch()
                    {
                        Code = StaffDepartmentBranchDao.GetNewBranchCode(),
                        Name = dep.Name,
                        Department = dep,
                        Creator = curUser,
                        CreateDate = DateTime.Now
                    };

                    try
                    {
                        StaffDepartmentBranchDao.SaveAndFlush(Depbr);
                    }
                    catch (Exception ex)
                    {
                        StaffDepartmentBranchDao.RollbackTran();
                        error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                        return false;
                    }
                    break;
                case 3://дирекция
                    br = StaffDepartmentBranchDao.GetDepartmentBranchByDeparment(DepartmentDao.GetParentDepartmentWithLevel(dep, dep.ItemLevel.Value - 1));
                    if (br == null)
                    {
                        error = "Для данной точки не определен филиал! Проверьте данные в справочнике кодировки.";
                        return false;
                    }

                    StaffDepartmentManagement Depm = new StaffDepartmentManagement()
                    {
                        Code = StaffDepartmentManagementDao.GetNewManagementCode(br),//сделать
                        Name = dep.Name,
                        DepartmentBranch = br,
                        Department = dep,
                        Creator = curUser,
                        CreateDate = DateTime.Now
                    };

                    try
                    {
                        StaffDepartmentManagementDao.SaveAndFlush(Depm);
                    }
                    catch (Exception ex)
                    {
                        StaffDepartmentManagementDao.RollbackTran();
                        error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                        return false;
                    }
                    break;
                case 4://управление
                    mn = StaffDepartmentManagementDao.GetDepartmentManagementByDeparment(DepartmentDao.GetParentDepartmentWithLevel(dep, dep.ItemLevel.Value - 1));

                    if (mn == null)
                    {
                        error = "Для данной точки не определена дирекция! Проверьте данные в справочнике кодировки.";
                        return false;
                    }

                    StaffDepartmentAdministration Depa = new StaffDepartmentAdministration()
                    {
                        Code = StaffDepartmentAdministrationDao.GetNewAdministrationCode(mn),
                        Name = dep.Name,
                        DepartmentManagement = mn,
                        Department = dep,
                        Creator = curUser,
                        CreateDate = DateTime.Now
                    };

                    try
                    {
                        StaffDepartmentAdministrationDao.SaveAndFlush(Depa);
                    }
                    catch (Exception ex)
                    {
                        StaffDepartmentAdministrationDao.RollbackTran();
                        error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                        return false;
                    }
                    break;
                case 5://бизнес-группа
                    StaffDepartmentAdministration adm = StaffDepartmentAdministrationDao.GetDepartmentAdministrationByDeparment(DepartmentDao.GetParentDepartmentWithLevel(dep, dep.ItemLevel.Value - 1));

                    if (adm == null)
                    {
                        error = "Для данной точки не определено управление! Проверьте данные в справочнике кодировки.";
                        return false;
                    }

                    StaffDepartmentBusinessGroup Depbg = new StaffDepartmentBusinessGroup()
                    {
                        Code = StaffDepartmentBusinessGroupDao.GetNewBusinessGroupCode(adm),
                        Name = dep.Name,
                        DepartmentAdministration = adm,
                        Department = dep,
                        Creator = curUser,
                        CreateDate = DateTime.Now
                    };

                    try
                    {
                        StaffDepartmentBusinessGroupDao.SaveAndFlush(Depbg);
                    }
                    catch (Exception ex)
                    {
                        StaffDepartmentBusinessGroupDao.RollbackTran();
                        error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                        return false;
                    }
                    break;
                case 6://рп-привязка
                    StaffDepartmentBusinessGroup bg = StaffDepartmentBusinessGroupDao.GetDepartmentBusinessGroupByDeparment(DepartmentDao.GetParentDepartmentWithLevel(dep, dep.ItemLevel.Value - 1));

                    if (bg == null)
                    {
                        error = "Для данной точки не определена бизнес-группа! Проверьте данные в справочнике кодировки.";
                        return false;
                    }

                    StaffDepartmentRPLink Deprp = new StaffDepartmentRPLink()
                    {
                        Code = StaffDepartmentRPLinkDao.GetNewRPLinkCode(bg),
                        Name = dep.Name,
                        DepartmentBG = bg,
                        Department = dep,
                        Creator = curUser,
                        CreateDate = DateTime.Now
                    };

                    try
                    {
                        StaffDepartmentRPLinkDao.SaveAndFlush(Deprp);
                    }
                    catch (Exception ex)
                    {
                        StaffDepartmentRPLinkDao.RollbackTran();
                        error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                        return false;
                    }
                    break;
                case 7://подразделение (точка)
                    //создаем код только для фронтов и бэкфронтов
                    if (entity.DepartmentAccessory.Id == 2 || entity.DepartmentAccessory.Id == 6)
                    {
                        br = StaffDepartmentBranchDao.GetDepartmentBranchByDeparment(DepartmentDao.GetParentDepartmentWithLevel(dep, dep.ItemLevel.Value - 1));
                        if (br == null)
                        {
                            error = "Для данной точки не определен филиал! Проверьте данные в справочнике кодировки.";
                            return false;
                        }

                        mn = StaffDepartmentManagementDao.GetDepartmentManagementByDeparment(DepartmentDao.GetParentDepartmentWithLevel(dep, dep.ItemLevel.Value - 1));
                        if (mn == null)
                        {
                            error = "Для данной точки не определена дирекция! Проверьте данные в справочнике кодировки.";
                            return false;
                        }

                        StaffDepartmentRPLink rp = StaffDepartmentRPLinkDao.GetDepartmentRPLinkByDeparment(DepartmentDao.GetParentDepartmentWithLevel(dep, dep.ItemLevel.Value - 1));
                        if (rp == null)
                        {
                            error = "Для данной точки не определена РП-привязка! Проверьте данные в справочнике кодировки.";
                            return false;
                        }

                        entity.DepartmentManagerDetails[0].DepCode = StaffDepartmentRequestDao.GetNewFinDepCode(br, mn, rp);
                        dep.FingradCode = entity.DepartmentManagerDetails[0].DepCode;

                        try
                        {
                            DepartmentDao.SaveAndFlush(dep);
                        }
                        catch (Exception ex)
                        {
                            DepartmentDao.RollbackTran();
                            error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                            return false;
                        }
                    }
                    break;
            }

            
            return true;
        }
        /// <summary>
        /// Определяем состояние согласования заявки.
        /// </summary>
        /// <param name="model">Модель</param>
        /// <param name="entity">Данные заявки</param>
        protected void SetApprovalFlags(StaffDepartmentRequestModel model, StaffDepartmentRequest entity)
        {
            User curUser = UserDao.Get(AuthenticationService.CurrentUser.Id);//текущий пользователь

            //заполняем списки согласовантов
            GetApprovalLists(model, entity);

            model.IsImportance = true;
            //за согласовантов могут отработать кураторы и кадровики банка.
            model.IsCurator = (AuthenticationService.CurrentUser.UserRole == UserRole.Inspector);
            model.IsPersonnelBank = (AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantPersonnel);
            model.IsConsultant = (AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing);
            model.IsTaxCollector = (AuthenticationService.CurrentUser.UserRole == UserRole.TaxCollector);
            model.IsOrder = (AuthenticationService.CurrentUser.UserRole == UserRole.StaffListOrder);
            model.IsSoftAdmin = (AuthenticationService.CurrentUser.UserRole == UserRole.SoftAdmin);

            //разбираемся с состоянием птиц
            //выбираем из согласования не архивные записи.
            IList<DocumentApproval> DocApproval = DocumentApprovalDao.GetDocumentApproval(entity.Id, (int)ApprovalTypeEnum.StaffDepartmentRequest);

            //для новых/автоматически сформированных заявок
            if (DocApproval == null || DocApproval.Count == 0)
            {
                model.IsInitiatorApproveAvailable = entity.IsUsed ? false : (model.Initiators.Count != 0 && (model.IsCurator || model.IsPersonnelBank || model.IsConsultant || model.Initiators.Where(x => x.Id == curUser.Id).Count() != 0) ? true : false);
                model.IsTopManagerApproveAvailable = false;
                model.IsBoardMemberApproveAvailable = false;
                model.IsAgreeButtonAvailable = model.IsInitiatorApproveAvailable;
            }
            else
            {
                //потом проводим слесарную обработку по состоянию согласования 
                //обязательные согласованты
                foreach (DocumentApproval item in DocApproval.OrderBy(x => x.Number))
                {
                    string PyrusNumber = entity.StaffRequestPyrusTasks.Where(x => x.DocumentApproval.Number == item.Number).Count() != 0 ? entity.StaffRequestPyrusTasks.Where(x => x.DocumentApproval.Number == item.Number).Single().NumberTask : string.Empty;
                    if (item.IsImportance)
                    {
                        switch (item.Number)
                        {
                            case 1://инициатор
                                model.IsInitiatorApprove = true;
                                model.IsInitiatorApproveAvailable = false;
                                model.InitiatorApproveName = "Заявка создана " + item.CreateDate.Value.ToShortDateString() + " " + (item.AssistantUser == null ? "Инициатор: " + item.ApproveUser.Name + " - " + item.ApproveUser.Position.Name
                                    : "Автор заявки: " + item.AssistantUser.Name + "; Инициатор: " + item.ApproveUser.Name + " - " + item.ApproveUser.Position.Name);
                                model.InitiatorId = item.AssistantUser == null ? item.ApproveUser.Id : item.AssistantUser.Id;
                                //entity.StaffRequestPyrusTasks.Where(x => x.DocumentApproval.Number == 1)
                                if (!string.IsNullOrEmpty(PyrusNumber))
                                {
                                    model.InitiatorPyrusName = "Задача в Пайрус № " + PyrusNumber;
                                    model.InitiatorPyrusRef = @"https://pyrus.com/t#id" + PyrusNumber;
                                }

                                //открываем согласование для следующего участника процесса
                                if (entity.DepartmentAccessory != null)
                                {
                                    if (entity.DepartmentAccessory.Id == 2 || entity.DepartmentAccessory.Id == 6)//кураторы согласуют фронты и бэкфронты
                                    {
                                        model.IsCuratorApproveAvailable = model.IsCurator || model.IsConsultant ? true : false;
                                        model.IsAgreeButtonAvailable = model.IsCuratorApproveAvailable;
                                    }
                                    else
                                    {
                                        model.IsPersonnelBankApproveAvailable = model.IsPersonnelBank || model.IsConsultant ? true : false;
                                        model.IsAgreeButtonAvailable = model.IsPersonnelBankApproveAvailable;
                                    }
                                }
                                break;
                            case 2://куратор
                                model.IsCuratorApprove = true;
                                model.IsCuratorApproveAvailable = false;
                                model.CuratorApproveName = "Заявка проверена " + item.CreateDate.Value.ToShortDateString() + " " + "Куратор: " + item.ApproveUser.Name;
                                if (!string.IsNullOrEmpty(PyrusNumber))
                                {
                                    model.CuratorPyrusName = "Задача в Пайрус № " + PyrusNumber;
                                    model.CuratorPyrusRef = @"https://pyrus.com/t#id" + PyrusNumber;
                                }

                                //открываем согласование для следующего участника процесса
                                if (entity.DepartmentAccessory != null)
                                {
                                    if (entity.DepartmentAccessory.Id == 1)//кадровики согласуют только бэк
                                    {
                                        model.IsPersonnelBankApproveAvailable = model.IsPersonnelBank || model.IsConsultant ? true : false;
                                        model.IsAgreeButtonAvailable = model.IsPersonnelBankApproveAvailable;
                                    }
                                    else
                                    {
                                        model.IsTopManagerApproveAvailable = model.TopManagers.Count != 0 && (model.IsCurator || model.IsPersonnelBank || model.IsConsultant || model.TopManagers.Where(x => x.Id == curUser.Id).Count() != 0) ? true : false;
                                        model.IsAgreeButtonAvailable = model.IsTopManagerApproveAvailable;
                                    }
                                }
                                break;
                            case 3://кадровик
                                model.IsPersonnelBankApprove = true;
                                model.IsPersonnelBankApproveAvailable = false;
                                model.PersonnelBankApproveName = "Заявка проверена " + item.CreateDate.Value.ToShortDateString() + " " + "Кадровик банка: " + item.ApproveUser.Name;
                                if (!string.IsNullOrEmpty(PyrusNumber))
                                {
                                    model.PersonnelBankPyrusName = "Задача в Пайрус № " + PyrusNumber;
                                    model.PersonnelBankPyrusRef = @"https://pyrus.com/t#id" + PyrusNumber;
                                }

                                //открываем согласование для следующего участника процесса
                                model.IsTopManagerApproveAvailable = model.TopManagers.Count != 0 && (model.IsCurator || model.IsPersonnelBank || model.IsConsultant || model.TopManagers.Where(x => x.Id == curUser.Id).Count() != 0) ? true : false;
                                model.IsAgreeButtonAvailable = model.IsTopManagerApproveAvailable;
                                break;
                            case 5://вышестоящий руководитель
                                model.IsTopManagerApprove = true;
                                model.IsTopManagerApproveAvailable = false;
                                model.TopManagerApproveName = "Заявка согласована " + item.CreateDate.Value.ToShortDateString() + " " + (item.AssistantUser == null ? "Согласовант: " + item.ApproveUser.Name + " - " + item.ApproveUser.Position.Name
                                    : "Согласовал: " + item.AssistantUser.Name + "; Согласовант: " + item.ApproveUser.Name + " - " + item.ApproveUser.Position.Name);
                                model.TopManagerId = item.AssistantUser == null ? item.ApproveUser.Id : item.AssistantUser.Id;
                                if (!string.IsNullOrEmpty(PyrusNumber))
                                {
                                    model.TopManagerPyrusName = "Задача в Пайрус № " + PyrusNumber;
                                    model.TopManagerPyrusRef = @"https://pyrus.com/t#id" + PyrusNumber;
                                }

                                //открываем согласование для следующего участника процесса
                                model.IsBoardMemberApproveAvailable = model.BoardMembers.Count != 0 && (model.IsCurator || model.IsPersonnelBank || model.IsConsultant || model.BoardMembers.Where(x => x.Id == curUser.Id).Count() != 0) ? true : false;
                                model.IsAgreeButtonAvailable = model.IsBoardMemberApproveAvailable;
                                break;
                            case 6://член правления
                                model.IsBoardMemberApprove = true;
                                model.IsBoardMemberApproveAvailable = false;
                                model.BoardMemberApproveName = "Заявка утверждена " + item.CreateDate.Value.ToShortDateString() + " " + (item.AssistantUser == null ? "Утвердил: " + item.ApproveUser.Name + " - Член правления банка"// + item.ApproveUser.Position.Name
                                    : "Утвердил: " + item.AssistantUser.Name + "; Утверждающий: " + item.ApproveUser.Name + " - Член правления банка");
                                model.BoardMemberId = item.AssistantUser == null ? item.ApproveUser.Id : item.AssistantUser.Id;
                                model.IsAgreeButtonAvailable = false;

                                if (!string.IsNullOrEmpty(PyrusNumber))
                                {
                                    model.BoardMemberPyrusName = "Задача в Пайрус № " + PyrusNumber;
                                    model.BoardMemberPyrusRef = @"https://pyrus.com/t#id" + PyrusNumber;
                                }
                                break;
                        }
                    }
                    else
                    {
                        model.IsTaxCollectorApproveAvailable = model.IsTaxCollector || model.IsConsultant ? true : false;
                        model.IsOrderApproveAvailable = model.IsOrder || model.IsConsultant ? true : false;

                        switch (item.Number)
                        {
                            case 4://налоговик
                                model.IsTaxCollectorApprove = true;
                                model.IsTaxCollectorApproveAvailable = false;
                                model.TaxCollectorApproveName = "Заявка согласована " + item.CreateDate.Value.ToShortDateString() + " " + "Налоговик: " + item.ApproveUser.Name;
                                //если попали сюда, значит согласование данной цепочки уже прошло, задраиваем люки
                                model.IsAgreeButtonAvailable = model.IsTaxCollectorApproveAvailable;
                                break;
                            case 7://налоговик
                                model.IsOrderApprove = true;
                                model.IsOrderApproveAvailable = false;
                                model.OrderApproveName = "Приказы составлены " + item.CreateDate.Value.ToShortDateString() + " " + "Ответственный за составление приказов: " + item.ApproveUser.Name;
                                //если попали сюда, значит согласование данной цепочки уже прошло, задраиваем люки
                                model.IsAgreeButtonAvailable = model.IsOrderApproveAvailable;
                                break;
                        }
                    }
                }

            }


            //налоговик
            if (model.IsTaxCollector && DocApproval.Where(x => x.Number == 4).Count() == 0)
            {
                //model.IsTaxCollectorApprove = true;
                model.IsTaxCollectorApproveAvailable = true;
                model.IsAgreeButtonAvailable = model.IsTaxCollectorApproveAvailable;
                model.IsImportance = false;
            }

            //приказы
            if (model.IsOrder && DocApproval.Where(x => x.Number == 7).Count() == 0)
            {
                //model.IsOrderApprove = true;
                model.IsOrderApproveAvailable = true;
                model.IsAgreeButtonAvailable = model.IsOrderApproveAvailable;
                model.IsImportance = false;
            }

            //для администратора ПО банка
            if (model.IsSoftAdmin || model.IsCurator || model.IsConsultant)
            {
                model.IsSoftAdminApprove = true;
                model.IsSoftAdminApproveAvailable = true;
                model.IsDraftButtonAvailable = false;
                model.IsAgreeButtonAvailable = false;
                model.IsImportance = false;
            }


            if (UserRole.Manager != AuthenticationService.CurrentUser.UserRole && UserRole.Inspector != AuthenticationService.CurrentUser.UserRole
                    && UserRole.ConsultantOutsourcing != AuthenticationService.CurrentUser.UserRole && UserRole.ConsultantPersonnel != AuthenticationService.CurrentUser.UserRole
                    && UserRole.Director != AuthenticationService.CurrentUser.UserRole && UserRole.TaxCollector != AuthenticationService.CurrentUser.UserRole)
            {
                model.IsAgreeButtonAvailable = false;
            }
        }
        /// <summary>
        /// Процедура заполняет списки согласовантов, на случай, если за них согласовывают кураторы или кадровики банка.
        /// </summary>
        /// <param name="model">Модель</param>
        /// <param name="entity">Данные заявки.</param>
        protected void GetApprovalLists(StaffDepartmentRequestModel model, StaffDepartmentRequest entity)
        {
            //если заявка на создание, то подразделения еще нет, есть только родительское
            Department Parentdep = null;
            if (entity.RequestType.Id != 1 && entity.Department != null)
                Parentdep = DepartmentDao.GetByCode(entity.Department.ParentId.ToString());//родительское подразделение
            else
                Parentdep = DepartmentDao.Get(entity.ParentDepartment.Id);
            

            //относительно родительского ищем руководителей
            //помним, что начальника может не быть в родительском подразделении, по этому ищем вверх по ветке всех руководителей
            IList<User> Initiators = DepartmentDao.GetDepartmentManagers(Parentdep.Id, true)
                .OrderByDescending<User, int?>(manager => manager.Level)
                .ToList<User>();


            ////если инициатором является куратор или кадровик банка, то по ветке подразделения находим руководителей на уровень выше создаваемого подразделения
            if (entity.Creator.Id == AuthenticationService.CurrentUser.Id && AuthenticationService.CurrentUser.UserRole == UserRole.Manager)
            {
                model.Initiators = Initiators.Where(x => x.Id == AuthenticationService.CurrentUser.Id).ToList().ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name + " - " + x.Position.Name });
            }
            else
            {
                model.Initiators = Initiators.Where(x => x.Level >= 3).ToList().ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name + " - " + x.Position.Name });
                //если создатель заявки руководитель, то позиционируемся на нем
                if (entity.Creator.UserRole == UserRole.Manager)
                    model.InitiatorId = entity.Creator.Id;
            }

            
            //вышестоящее руководство
            model.TopManagers = Initiators.Where(x => x.Level == 3).ToList().ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name + " - " + x.Position.Name });


            //члены правления
            model.BoardMembers = UserDao.GetUsersWithRole(UserRole.Director)
                .Where(x => x.IsActive)
                .ToList()
                .ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name + " - Член правления банка" });

        }
        #endregion

        #region Заявки для штатных единиц
        /// <summary>
        /// Загрузка запросной формы реестра заявок ШЕ.
        /// </summary>
        /// <returns></returns>
        public StaffEstablishedPostRequestListModel GetStaffEstablishedPostRequestList()
        {
            StaffEstablishedPostRequestListModel model = new StaffEstablishedPostRequestListModel();
            DateTime today = DateTime.Today;
            model.DateBegin = new DateTime(today.Year, today.Month, 1);
            model.DateEnd = today;
            model.Statuses = GetDepRequestStatuses();
            model.RequestTypes = StaffEstablishedPostRequestTypesDao.LoadAll();
            model.RequestTypes.Insert(0, new StaffEstablishedPostRequestTypes() { Id = 0, Name = "Все" });

            return model;
        }
        /// <summary>
        /// Загрузка реестра заявок ШЕ.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public StaffEstablishedPostRequestListModel SetStaffEstablishedPostRequestList(StaffEstablishedPostRequestListModel model)
        {
            model.EPRequestList = StaffEstablishedPostRequestDao.GetEstablishedPostRequestList(userDao.Load(AuthenticationService.CurrentUser.Id),
                AuthenticationService.CurrentUser.UserRole,
                model.DepartmentId,
                model.Id.HasValue ? model.Id.Value : 0,
                model.Creator,
                model.DateBegin,
                model.DateEnd,
                model.StatusId,
                model.SortBy,
                model.SortDescending,
                model.RequestTypeId);

            model.Statuses = GetDepRequestStatuses();
            model.RequestTypes = StaffEstablishedPostRequestTypesDao.LoadAll();
            model.RequestTypes.Insert(0, new StaffEstablishedPostRequestTypes() { Id = 0, Name = "Все" });
            return model;
        }
        /// <summary>
        /// Заполняем модель заявки для штатной единицы.
        /// </summary>
        /// <param name="model">Модель заявки.</param>
        /// <returns></returns>
        public StaffEstablishedPostRequestModel GetEstablishedPostRequest(StaffEstablishedPostRequestModel model)
        {
            //готовая заявка грузиться по Id
            //создание заявки на создание ШЕ, идет по ветке где Id = 0
            //создание заявки на изменение/удаление, нужно заполнить текущими даными, но с обнуленным Id, чтобы создалась новая заявка
            //для заявок на редактирование надо сделать поиск текущей заявки по Id штатной единицы, для того чтобы ее заполнить только данными, а Id самой заявки обнулить, чтобы запись добавилась, а не редактировалась
            bool IsRequestExists = model.Id == 0 ? false : true;
            if (model.Id == 0)
                model.Id = StaffEstablishedPostRequestDao.GetCurrentRequestId(model.SEPId);

                        
            //заполняем заявку на все случаи жизни
            if (model.Id == 0)
            {
                model.DateRequest = null;
                model.UserId = AuthenticationService.CurrentUser.Id;
                model.PositionId = 0;
                model.PositionName = string.Empty;
                model.Quantity = 0;
                model.Salary = 0;
                model.ReasonId = 0;
                model.ScheduleId = 0;
                model.WCId = 0;
                model.BeginAccountDate = DateTime.Now;
                model.EPInfo = string.Empty;

                //кнопки
                model.IsDraftButtonAvailable = true;
                model.IsAgreeButtonAvailable = false;
            }
            else
            {
                StaffEstablishedPostRequest PrevEntity = new StaffEstablishedPostRequest(); //для заявок на изменение нужно показать предыдущий оклад

                StaffEstablishedPostRequest entity = StaffEstablishedPostRequestDao.Get(model.Id);
                if (entity == null) //если нет заявки с таким идентификатором, грузим новую заявку на создание штатной единицы
                {
                    model.Id = 0;
                    return GetEstablishedPostRequest(model);
                }

                if (model.SEPId != 0)
                    PrevEntity = StaffEstablishedPostRequestDao.GetPrevEstablishedPostRequest(model.SEPId, entity.Id);

                if (!IsRequestExists)
                {
                    model.Id = 0;
                }

                model.IsUsed = entity.IsUsed;
                model.RequestTypeId = model.Id == 0 ? model.RequestTypeId : entity.RequestType.Id;
                model.UserId = entity.Creator != null ? entity.Creator.Id : 0;
                model.DateRequest = entity.DateRequest;
                model.DepartmentId = entity.Department != null ? entity.Department.Id : 0;
                model.BFGId = entity.Department != null && entity.Department.DepartmentAccessory != null ? entity.Department.DepartmentAccessory.Id : 0;
                model.PositionId = entity.Position != null ? entity.Position.Id : 0;
                model.PositionName = entity.Position != null ? entity.Position.Name : string.Empty;
                model.Quantity = entity.Quantity;
                model.QuantityPrev = entity.Quantity;
                model.Salary = entity.Salary;
                model.SalaryPrev = PrevEntity != null ? PrevEntity.Salary : 0;
                model.ReasonId = entity.Reason == null ? 0 : entity.Reason.Id;
                model.ScheduleId = entity.Schedule == null ? 0 : entity.Schedule.Id;
                model.WCId = entity.WorkingCondition == null ? 0 : entity.WorkingCondition.Id;
                model.BeginAccountDate = model.Id == 0 ? model.BeginAccountDate : entity.BeginAccountDate;

                int UsersCount = StaffEstablishedPostDao.GetEstablishedPostUsed(entity.StaffEstablishedPost != null ? entity.StaffEstablishedPost.Id : 0);
                model.EPInfo = "Занято - " + (UsersCount).ToString() + "; Вакантно - " + (entity.Quantity - UsersCount).ToString();

                //кнопки
                model.IsDraftButtonAvailable = true;
                model.IsAgreeButtonAvailable = !entity.DateAccept.HasValue;


            }

            LoadDictionaries(model);
            //для новых заявок надо подгружать надбавки от текущего состояния штатной единицы, берем действующую заявку, иначе по заполняем по текущей заявке
            model.PostChargeLinks = StaffEstablishedPostChargeLinksDao.GetChargesForRequests(model.RequestTypeId != 1 && model.Id == 0 ? StaffEstablishedPostRequestDao.GetCurrentRequestId(model.SEPId) : model.Id).OrderBy(x => x.ChargeName).ToList();
            

            return model;
        }
        /// <summary>
        /// Процедура сохранения новой заявки для штатной единицы.
        /// </summary>
        /// <param name="model">Модель заявки.</param>
        /// <param name="error">Сообщенио об ошибке.</param>
        /// <returns></returns>
        public bool SaveNewEstablishedPostRequest(StaffEstablishedPostRequestModel model, out string error)
        {
            error = string.Empty;
            StaffEstablishedPostRequest entity;
            User curUser = UserDao.Load(AuthenticationService.CurrentUser.Id);

            if (model.Id == 0)
            {

                entity = new StaffEstablishedPostRequest
                {
                    RequestType = StaffEstablishedPostRequestTypesDao.Load(model.RequestTypeId),
                    DateRequest = DateTime.Now,
                    StaffEstablishedPost = model.RequestTypeId == 1 ? null : StaffEstablishedPostDao.Get(model.SEPId),
                    Position = PositionDao.Get(model.PositionId),
                    Department = model.DepartmentId != 0 ? DepartmentDao.Get(model.DepartmentId) : null,
                    Schedule = model.ScheduleId != 0 ? ScheduleDao.Get(model.ScheduleId) : null,
                    WorkingCondition = model.WCId != 0 ? StaffWorkingConditionsDao.Get(model.WCId) : null,
                    Quantity = model.RequestTypeId != 3 ? model.Quantity : model.QuantityPrev,
                    Salary = model.RequestTypeId != 3 ? model.Salary : model.SalaryPrev,
                    BeginAccountDate = model.BeginAccountDate,
                    IsUsed = false,
                    IsDraft = true,
                    Reason = model.ReasonId.HasValue ? AppointmentReasonDao.Get(model.ReasonId.Value) : null,
                    Creator = curUser,
                    CreateDate = DateTime.Now
                };


                //надбавки
                entity.PostChargeLinks = new List<StaffEstablishedPostChargeLinks>();
                foreach (var item in model.PostChargeLinks.Where(x => x.Amount != 0 || x.IsUsed))
                {
                    entity.PostChargeLinks.Add(new StaffEstablishedPostChargeLinks
                    {
                        EstablishedPostRequest = entity,
                        EstablishedPost = entity.StaffEstablishedPost,
                        ExtraCharges = StaffExtraChargesDao.Get(item.ChargeId),
                        Amount = item.Amount,
                        ExtraChargeActions = StaffExtraChargeActionsDao.Get(item.ActionId),
                        IsUsed = item.IsUsed,
                        Creator = curUser,
                        CreateDate = DateTime.Now
                    });
                }

                //расстановка
                //при сокращении или вводе ременной вакансии ставим метки в расстановке
                if (entity.RequestType.Id == 2 || entity.RequestType.Id == 3 || (entity.RequestType.Id == 4 && model.Personnels.Count != 0))
                {
                    ////на случай добавления вакансии
                    //if (entity.RequestType.Id == 2 || (entity.RequestType.Id == 4 && model.Personnels.Count != 0))
                    //{
                    //    int CountLinks = entity.StaffEstablishedPost.EstablishedPostUserLinks != null ? entity.StaffEstablishedPost.EstablishedPostUserLinks.Count() : 0;
                    //    if (entity.Quantity > CountLinks)
                    //    {
                    //        CountLinks = entity.Quantity - CountLinks;
                    //        for (int i = 0; i < CountLinks; i++)
                    //        {
                    //            entity.StaffEstablishedPost.EstablishedPostUserLinks.Add(new StaffEstablishedPostUserLinks()
                    //            {
                    //                StaffEstablishedPost = entity.StaffEstablishedPost,
                    //                User = null,
                    //                IsUsed = false,
                    //                Creator = curUser,
                    //                CreateDate = DateTime.Now
                    //            });
                    //        }
                    //    }
                    //}

                    foreach (StaffEstablishedPostUserLinks ul in entity.StaffEstablishedPost.EstablishedPostUserLinks)
                    {
                        if (model.Personnels.Where(x => x.Id == ul.Id).Count() != 0)
                        {
                            StaffUserLinkDto item = model.Personnels.Where(x => x.Id == ul.Id).FirstOrDefault();
                            if (entity.RequestType.Id == 3 || entity.RequestType.Id == 4)
                            {
                                ul.IsDismissal = item.IsDismissal;//model.Personnels.Where(x => x.Id == ul.Id).Single().IsDismissal;
                                ul.DateDistribNote = item.DateDistribNote;
                                ul.DateReceivNote = item.DateReceivNote;
                            }
                            if (entity.RequestType.Id == 2 || entity.RequestType.Id == 4)
                            {
                                ul.IsTemporary = item.IsTemporary;
                                ul.DateTempBegin = item.DateTempBegin;
                                ul.DateTempEnd = item.DateTempEnd;
                            }
                            ul.Editor = curUser;
                            ul.EditDate = DateTime.Now;
                        }
                    }
                }


                try
                {
                    StaffEstablishedPostRequestDao.SaveAndFlush(entity);
                    model.Id = entity.Id;
                    return true;
                }
                catch (Exception ex)
                {
                    StaffEstablishedPostRequestDao.RollbackTran();
                    error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                    return false;
                }
            }
            //если не по той ветке пошли
            error = "Произошла ошибка при сохранении данных! Обратитесь к разработчикам!";
            return false;
        }
        /// <summary>
        /// Процедура сохранения существующей заявки для штатной единицы.
        /// </summary>
        /// <param name="model">Модель заявки.</param>
        /// <param name="error">Сообщенио об ошибке.</param>
        /// <returns></returns>
        public bool SaveEditEstablishedPostRequest(StaffEstablishedPostRequestModel model, out string error)
        {
            error = string.Empty;
            StaffEstablishedPostRequest entity = StaffEstablishedPostRequestDao.Get(model.Id);
            if (entity == null)
            {
                error = "Заявка не найдена! Обратитесь к разработчикам!";
                return false;
            }
            else
            {
                if (!model.IsDraft)
                {
                    bool IsEnabled = false;
                    //для подразделений уже давно существующих, смотрим наличие налоговых реквизитов
                    if (entity.Department.DepartmentTaxDetails.Count != 0)
                    {
                        if (!string.IsNullOrEmpty(entity.Department.DepartmentTaxDetails[0].TaxAdminCode) && !string.IsNullOrWhiteSpace(entity.Department.DepartmentTaxDetails[0].TaxAdminCode))
                            IsEnabled = true;
                    }

                    //для совсем свежих подразделений находим заявку и смотрим там соседа с налоговыми реквизитами
                    if (!IsEnabled)
                    {
                        StaffDepartmentRequest sdr = StaffDepartmentRequestDao.Get(StaffDepartmentRequestDao.GetCurrentRequestId(entity.Department != null ? entity.Department.Id : 0));

                        if (sdr != null)
                        {
                            if (sdr.DepNext != null && sdr.DepNext.DepartmentTaxDetails.Count != 0)
                            {
                                if (!string.IsNullOrEmpty(sdr.DepNext.DepartmentTaxDetails[0].TaxAdminCode) && !string.IsNullOrWhiteSpace(sdr.DepNext.DepartmentTaxDetails[0].TaxAdminCode))
                                    IsEnabled = true;
                            }
                        }
                    }

                    if (!IsEnabled)
                    {
                        error = "Нельзя создать/изменить/сократить штатную единицу, так как данное подразделение не стоит на налоговом учете!";
                        return false;
                    }

                    //if (entity.RequestType.Id == 3 && StaffEstablishedPostDao.GetEstablishedPostUsed(entity.StaffEstablishedPost != null ? entity.StaffEstablishedPost.Id : 0) != 0)
                    //{
                    //    error = "Нельзя сократить штатную единицу, так как она еще содержит работающих сотрудников!";
                    //    return false;
                    //}

                    //if (entity.Quantity < StaffEstablishedPostDao.GetEstablishedPostUsed(entity.StaffEstablishedPost != null ? entity.StaffEstablishedPost.Id : 0))
                    //{
                    //    error = "Нельзя сократить штатную единицу, так как она еще содержит работающих сотрудников больше, чем указанное количество в заявке!";
                    //    return false;
                    //}
                }
            }

            User curUser = UserDao.Load(AuthenticationService.CurrentUser.Id);

            entity.RequestType = StaffEstablishedPostRequestTypesDao.Load(model.RequestTypeId);
            entity.DateRequest = DateTime.Now;
            entity.StaffEstablishedPost = model.RequestTypeId == 1 ? null : StaffEstablishedPostDao.Get(model.SEPId);
            entity.Position = PositionDao.Get(model.PositionId);
            entity.Department = model.DepartmentId != 0 ? DepartmentDao.Get(model.DepartmentId) : null;
            entity.Schedule = model.ScheduleId != 0 ? ScheduleDao.Get(model.ScheduleId) : null;
            entity.WorkingCondition = model.WCId != 0 ? StaffWorkingConditionsDao.Get(model.WCId) : null;
            if (model.RequestTypeId != 3)
            {
                entity.Quantity = model.Quantity;
                entity.Salary = model.Salary;
            }
            entity.BeginAccountDate = model.BeginAccountDate;
            entity.Reason = model.ReasonId.HasValue ? AppointmentReasonDao.Get(model.ReasonId.Value) : null;
            entity.IsDraft = entity.IsUsed ? false : model.IsDraft; 
            entity.Editor = curUser;
            entity.EditDate = DateTime.Now;
            entity.BeginAccountDate = model.BeginAccountDate;

            //при сокращении или вводе временной вакансии ставим метки в расстановке (до начала согласования)
            if ((entity.RequestType.Id == 2 || entity.RequestType.Id == 3 || entity.RequestType.Id == 4) && !entity.DateSendToApprove.HasValue && entity.StaffEstablishedPost != null)
            {
                foreach (StaffEstablishedPostUserLinks ul in entity.StaffEstablishedPost.EstablishedPostUserLinks)
                {
                    if (model.Personnels.Where(x => x.Id == ul.Id && x.IsDismissal != ul.IsDismissal).Count() != 0)
                    {
                        StaffUserLinkDto item = model.Personnels.Where(x => x.Id == ul.Id && x.IsDismissal != ul.IsDismissal).FirstOrDefault();
                        //ul.IsDismissal = model.Personnels.Where(x => x.Id == ul.Id).Single().IsDismissal;
                        ul.IsDismissal = item.IsDismissal;
                        ul.DateDistribNote = item.DateDistribNote;
                        ul.DateReceivNote = item.DateReceivNote;
                        ul.ReserveType = (int)StaffReserveTypeEnum.Dismissal;
                        ul.DocId = entity.Id;
                        ul.Editor = curUser;
                        ul.EditDate = DateTime.Now;
                    }

                    if (model.Personnels.Where(x => x.Id == ul.Id && x.IsTemporary != ul.IsTemporary).Count() != 0)
                    {
                        StaffUserLinkDto item = model.Personnels.Where(x => x.Id == ul.Id && x.IsTemporary != ul.IsTemporary).FirstOrDefault();
                        ul.IsTemporary = item.IsTemporary;
                        ul.DateTempBegin = item.DateTempBegin;
                        ul.DateTempEnd = item.DateTempEnd;
                        ul.Editor = curUser;
                        ul.EditDate = DateTime.Now;
                    }
                }
            }


            


            //надбавки 
            //сохраняем только при открытии и изменении до отправки на согласование
            if (model.RequestTypeId != 3 && !entity.DateSendToApprove.HasValue)
            {
                if (entity.PostChargeLinks == null)
                    entity.PostChargeLinks = new List<StaffEstablishedPostChargeLinks>();

                foreach (var item in model.PostChargeLinks)
                {
                    StaffEstablishedPostChargeLinks pcl = new StaffEstablishedPostChargeLinks();

                    //если была запись и убрали значения, то удаляем
                    if ((item.Id != 0 && item.Amount == 0) || (item.Id != 0 && item.IsNeeded && !item.IsUsed))
                    {
                        pcl = entity.PostChargeLinks.Where(x => x.Id == item.Id).Single();
                        entity.PostChargeLinks.Remove(pcl);
                    }

                    //если не было записи и ввели значение, то добавляем
                    if ((item.Id == 0 && item.Amount != 0) || (item.Id == 0 && item.IsNeeded && item.IsUsed))
                    {
                        pcl.EstablishedPostRequest = entity;
                        pcl.EstablishedPost = entity.StaffEstablishedPost;
                        pcl.ExtraCharges = StaffExtraChargesDao.Get(item.ChargeId);
                        pcl.Amount = item.Amount;
                        pcl.IsUsed = item.IsUsed;
                        pcl.ExtraChargeActions = StaffExtraChargeActionsDao.Get(item.ActionId);
                        pcl.Creator = curUser;
                        pcl.CreateDate = DateTime.Now;

                        entity.PostChargeLinks.Add(pcl);
                    }

                    //запись была и есть код, то предпологаем, что это редактирование (только для надбавок с значением)
                    if (item.Id != 0 /*&& item.Amount != 0*/)
                    {
                        entity.PostChargeLinks.Where(x => x.Id == item.Id).Single().EstablishedPost = entity.StaffEstablishedPost;
                        entity.PostChargeLinks.Where(x => x.Id == item.Id).Single().Amount = item.Amount;
                        entity.PostChargeLinks.Where(x => x.Id == item.Id).Single().ExtraChargeActions = StaffExtraChargeActionsDao.Get(item.ActionId);
                        entity.PostChargeLinks.Where(x => x.Id == item.Id).Single().Editor = curUser;
                        entity.PostChargeLinks.Where(x => x.Id == item.Id).Single().EditDate = DateTime.Now;
                    }
                }
            }


            //создаем запись в справочнике штатных единиц.
            if (!model.IsDraft)
            {

                int Result = entity.RequestType.Id == 4 ? 0 : SaveStaffEstablishedPostApprovals(model, entity, curUser, out error);
                if (Result == -1) return false;

                if (Result == 0)
                {
                    //для первоначальных данных
                    if (entity.RequestType.Id == 4)
                    {
                        entity.DateSendToApprove = DateTime.Now;
                        entity.BeginAccountDate = DateTime.Now;
                        entity.DateAccept = DateTime.Now;
                    }

                    if (!SaveStaffEstablishedPostReference(entity, curUser, out error))
                    {
                        return false;
                    }

                    //если уже была заявка, то у нее убираем признак использования, это для изменения/удаления
                    if (entity.RequestType.Id != 1)
                    {
                        int OldRequestId = StaffEstablishedPostRequestDao.GetCurrentRequestId(entity.StaffEstablishedPost.Id);
                        if (OldRequestId != 0)
                        {
                            StaffEstablishedPostRequest OldEntity = StaffEstablishedPostRequestDao.Get(OldRequestId);
                            OldEntity.IsUsed = false;

                            try
                            {
                                StaffEstablishedPostRequestDao.SaveAndFlush(OldEntity);
                            }
                            catch (Exception ex)
                            {
                                StaffEstablishedPostRequestDao.RollbackTran();
                                error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                                return false;
                            }
                        }
                    }

                    entity.IsUsed = true;
                    error = "Заявка утверждена!";
                }
                
            }

            
            

            if (model.Id != 0)
            {
                try
                {
                    StaffEstablishedPostRequestDao.SaveAndFlush(entity);
                    model.Id = entity.Id;
                }
                catch (Exception ex)
                {
                    StaffEstablishedPostRequestDao.RollbackTran();
                    error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                    return false;
                }

                return true;
            }

            //если не по той ветке пошли
            error = "Произошла ошибка при сохранении данных! Обратитесь к разработчикам!";
            return false;
        }
        /// <summary>
        /// Сохраняем изменения в справочнике штатных единиц.
        /// </summary>
        /// <param name="entity">Текущая заявка.</param>
        /// <param name="curUser">текущий пользователь.</param>
        /// <param name="error">Сообщение об ошибке.</param>
        /// <returns></returns>
        protected bool SaveStaffEstablishedPostReference(StaffEstablishedPostRequest entity, User curUser, out string error)
        {
            error = string.Empty;
            StaffEstablishedPost sep = entity.StaffEstablishedPost != null ? StaffEstablishedPostDao.Get(entity.StaffEstablishedPost.Id) : new StaffEstablishedPost();
            //если заявка на создание, создаем новую запись и делаем в заявке на нее ссылку
            if (entity.RequestType.Id == 1 || entity.RequestType.Id == 4)
            {
                sep.Position = entity.Position;
                sep.Department = entity.Department;
                sep.Quantity = 0;// entity.Quantity;
                sep.Salary = entity.Salary;
                sep.IsUsed = true;
                sep.BeginAccountDate = entity.BeginAccountDate;
                sep.Creator = curUser;
                sep.CreateDate = DateTime.Now;
            }

            //изменилось количество изменяем количество связей
            if (!SaveStaffEstablishedPostChangeUserLinks(entity, sep, curUser, out error))
            {
                return false;
            }
            else
                sep.Quantity = entity.Quantity;

            //если заявка на редактирование/удаление, редактируем текущую запись в справочнике
            if (entity.RequestType.Id != 1 && entity.RequestType.Id != 4)
            {
                if (entity.RequestType.Id == 2)
                {
                    sep.Position = entity.Position;
                    //пока решили не менять количество в заявках наизменение
                    //sep.Quantity = entity.Quantity;
                    sep.Salary = entity.Salary;
                }
                else if (entity.RequestType.Id == 3)
                {
                    sep.IsUsed = false; //делаем неактивной текущую запись в справочнике
                }
                sep.BeginAccountDate = entity.BeginAccountDate;
                sep.Editor = curUser;
                sep.EditDate = DateTime.Now;
            }


            //для создания/изменения в надбавках делаем ссылку на штатную единицу
            if (entity.RequestType.Id != 3)
            {
                if (entity.PostChargeLinks.Count != 0)
                {
                    foreach (var item in entity.PostChargeLinks)
                    {
                        item.EstablishedPost = sep;
                        item.Editor = curUser;
                        item.EditDate = DateTime.Now;
                    }
                }
            }


            //архивируем изменения
            if (sep.EstablishedPostArchive == null)
                sep.EstablishedPostArchive = new List<StaffEstablishedPostArchive>();

            sep.EstablishedPostArchive.Add(new StaffEstablishedPostArchive { 
                StaffEstablishedPost = sep,
                Position = sep.Position,
                Department = sep.Department,
                Quantity = sep.Quantity,
                Salary = sep.Salary,
                IsUsed = sep.IsUsed,
                BeginAccountDate = sep.BeginAccountDate,
                Priority = sep.Priority,
                Creator = curUser,
                CreateDate = DateTime.Now
            });


            try
            {
                StaffEstablishedPostDao.SaveAndFlush(sep);

                if (entity.StaffEstablishedPost == null)
                    entity.StaffEstablishedPost = new StaffEstablishedPost();

                entity.StaffEstablishedPost = sep;
                entity.DateSendToApprove = !entity.DateSendToApprove.HasValue ? DateTime.Now : entity.DateSendToApprove.Value;//отправлено на согласование
                entity.DateAccept = DateTime.Now;//согласовано
                //если создается новая штатная единица и в ней нет сотрудников, то надо указать признак выгрузки в 1С, чтобы пустые заявки не попали в выгрузку кадровых перемещений.
                //или сокращаются пустые места в расстановке
                if (entity.StaffEstablishedPost.EstablishedPostUserLinks.Where(x => x.User != null).Count() == 0
                    && entity.StaffEstablishedPost.EstablishedPostUserLinks.Where(x => x.User != null && x.IsDismissal && x.IsUsed).Count() == 0)
                {
                    entity.SendTo1C = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                StaffEstablishedPostDao.RollbackTran();
                error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }

            return true;
        }
        /// <summary>
        /// Меняем количество связей штатной единицы с сотрудниками.
        /// </summary>
        /// <param name="entity">Заявка</param>
        /// <param name="sep">Штатная единица</param>
        /// <param name="curUser">Текущий пользователь</param>
        /// <param name="error">Для сообщений</param>
        /// <returns></returns>
        protected bool SaveStaffEstablishedPostChangeUserLinks(StaffEstablishedPostRequest entity, StaffEstablishedPost sep, User curUser, out string error)
        {
            error = string.Empty;
            int CountLinks = sep.EstablishedPostUserLinks != null ? sep.EstablishedPostUserLinks.Count() : 0;
            //если количество штатных единиц увеличилось, то нужно добавить необходимое количество записей для связей
            if (entity.Quantity > CountLinks)
            {
                CountLinks = entity.Quantity - CountLinks;

                if(sep.EstablishedPostUserLinks == null)
                    sep.EstablishedPostUserLinks = new List<StaffEstablishedPostUserLinks>();

                for (int i = 0; i < CountLinks; i++)
                {
                    sep.EstablishedPostUserLinks.Add(new StaffEstablishedPostUserLinks()
                    {
                        StaffEstablishedPost = sep,
                        User = null,
                        IsUsed = true,
                        Creator = curUser,
                        CreateDate = DateTime.Now
                    });
                }
            }


            ////редактирование временных вакансий
            //if ((entity.RequestType.Id == 1 || entity.RequestType.Id == 2 || entity.RequestType.Id == 4) && sep.EstablishedPostUserLinks.Where(x => !x.IsDismissal && x.IsTemporary).Count() != 0)
            //{
            //    //на случай добавления временной вакансии
            //    CountLinks = sep.EstablishedPostUserLinks != null ? sep.EstablishedPostUserLinks.Count() : 0;
            //    if (entity.Quantity > CountLinks)
            //    {
            //        CountLinks = entity.Quantity - CountLinks;
            //        for (int i = 0; i < CountLinks; i++)
            //        {
            //            sep.EstablishedPostUserLinks.Add(new StaffEstablishedPostUserLinks()
            //            {
            //                StaffEstablishedPost = sep,
            //                User = null,
            //                IsUsed = true,
            //                Creator = curUser,
            //                CreateDate = DateTime.Now
            //            });
            //        }
            //    }

            //    foreach (var item in sep.EstablishedPostUserLinks
            //        .Where(x => x.IsUsed && x.IsDismissal))
            //    {
            //        item.IsUsed = false;
            //        item.Editor = curUser;
            //        item.EditDate = DateTime.Now;
            //    }
            //}


            //сокращение
            if ((entity.RequestType.Id == 3 || entity.RequestType.Id == 4) && sep.EstablishedPostUserLinks.Where(x => x.IsUsed && x.IsDismissal).Count() != 0)
            {
                //если частичное сокращение в расстановке, то меняем количество единиц в заявке
                if (sep.EstablishedPostUserLinks.Where(x => x.IsUsed && x.IsDismissal).Count() != 0 && sep.EstablishedPostUserLinks.Where(x => x.IsUsed && !x.IsDismissal).Count() != 0)
                {
                    entity.Quantity = sep.EstablishedPostUserLinks.Where(x => x.IsUsed && !x.IsDismissal).Count();
                }

                foreach (var item in sep.EstablishedPostUserLinks
                    .Where(x => x.IsUsed && x.IsDismissal))
                {
                    item.IsUsed = false;
                    item.Editor = curUser;
                    item.EditDate = DateTime.Now;
                }
            }

            return true;
        }
        /// <summary>
        /// Сохраняем согласования.
        /// </summary>
        /// <param name="model">Модель</param>
        /// <param name="entity">Текущая заявка.</param>
        /// <param name="curUser">текущий пользователь.</param>
        /// <param name="error">Сообщение об ошибке.</param>
        /// <returns></returns>
        protected int SaveStaffEstablishedPostApprovals(StaffEstablishedPostRequestModel model, StaffEstablishedPostRequest entity, User curUser, out string error)
        {
            //возвращает значения от 1 до 4 - этап согласования прошел успешно, 0 - согласование завершено, заявка утверждена, -1 - ошибка при сохранении данных

            //только после утверждения заявки можно редактировать справочник подразделений.
            error = string.Empty;


            //в каждой ветке определяемся с участниками групповухи под кодовым названием согласование/утверждение заявки для подразделения
            //текущим пользователем может быть куратор ии кадровик, которые могут действовать за согласовантов
            User Assistant = AuthenticationService.CurrentUser.UserRole == UserRole.Inspector
                || AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantPersonnel
                || AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing ? curUser : null;//куратор/кадровик банка/консультант РК

            //выбираем из согласования не архивные записи.
            IList<DocumentApproval> DocApproval = DocumentApprovalDao.GetDocumentApproval(entity.Id, (int)ApprovalTypeEnum.StaffEstablishedPostRequest);
            DocumentApproval da = new DocumentApproval();

            da.ApprovalType = (int)ApprovalTypeEnum.StaffEstablishedPostRequest;
            da.DocId = entity.Id;
            da.IsArchive = false;
            da.CreateDate = DateTime.Now;

            switch (DocApproval.Count)
            {
                case 0:
                    //если иницатор не выбран, это значит, что инициатор действует сам
                    User Initiator = model.InitiatorId != 0 ? UserDao.Get(model.InitiatorId) : curUser;//инициатор

                    da.ApproveUser = Initiator;
                    da.AssistantUser = Assistant;
                    da.Number = 1;
                    error = "Заявка создана!";
                    break;
                case 1:
                    da.ApproveUser = curUser;
                    da.AssistantUser = null;
                    da.Number = 2;
                    error = "Заявка проверена куратором!";
                    break;
                case 2:
                    da.ApproveUser = curUser;
                    da.AssistantUser = null;
                    da.Number = 3;
                    error = "Заявка проверена кадровиком банка!";
                    break;
                case 3:
                    //если согласовант не выбран, это значит, что он действует сам
                    User TopManager = model.TopManagerId != 0 ? UserDao.Get(model.TopManagerId) : curUser;//высший руководитель

                    da.ApproveUser = TopManager;
                    da.AssistantUser = Assistant;
                    da.Number = 4;
                    error = "Заявка согласована!";
                    break;
                case 4:
                    //если утверждающий не выбран, это значит, что он действует сам
                    User BoardMember = model.BoardMemberId != 0 ? UserDao.Get(model.BoardMemberId) : curUser;//член правления

                    da.ApproveUser = BoardMember;
                    da.AssistantUser = Assistant;
                    da.Number = 5;
                    error = "Заявка утверждена!";
                    break;
            }

            try
            {
                DocumentApprovalDao.SaveAndFlush(da);
                
                if (da.Number == 1)
                {
                    entity.DateSendToApprove = DateTime.Now;
                    return da.Number;
                }

                if (da.Number > 1 && da.Number < 5)
                    return da.Number;

                if (da.Number == 5)
                {
                    //entity.BeginAccountDate = DateTime.Now;
                    entity.DateAccept = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                DocumentApprovalDao.RollbackTran();
                error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                return -1;
            }

            return 0;
        }
        /// <summary>
        /// Загрузка реквизитов инициатора и подразделения к заявкам для штатных единиц
        /// </summary>
        /// <param name="model">Заполняемая модель заявки.</param>
        /// <returns></returns>
        protected void GetDepRequestInfo(StaffEstablishedPostRequestModel model)
        {
            User curUser = UserDao.Load(model.UserId != 0 ? model.UserId : AuthenticationService.CurrentUser.Id);
            model.DepartmentName = curUser.Department != null ? curUser.Department.Name : string.Empty;
            model.RequestInitiator = curUser.Name + " - " + (curUser.Position != null ? curUser.Position.Name : string.Empty);

            if (model.DepartmentId != 0)
            {
                Department dep = DepartmentDao.Get(model.DepartmentId);
                model.DepartmentName = dep.Name;
                int DepId = StaffDepartmentRequestDao.GetCurrentRequestId(dep != null ? dep.Id : 0);
                if (DepId != 0)
                {
                    StaffDepartmentRequest DepEntity = StaffDepartmentRequestDao.Get(DepId);
                    //model.IsBack = DepEntity.IsBack;
                    if (DepEntity.LegalAddress != null)
                    {
                        model.LegalAddress = DepEntity.LegalAddress.Address;
                    }
                    model.IsTaxAdminAccount = DepEntity.IsTaxAdminAccount;
                    model.IsEmployeAvailable = DepEntity.IsEmployeAvailable;
                    model.AccessoryName = DepEntity.DepartmentAccessory != null ? DepEntity.DepartmentAccessory.Name : string.Empty;
                }

                //налоговые реквизиты
                //StaffDepartmentTaxDetails dt = StaffDepartmentTaxDetailsDao.Get(dep.Id);
                model.KPP = dep.DepartmentTaxDetails != null && dep.DepartmentTaxDetails.Count != 0 ? dep.DepartmentTaxDetails[0].KPP : string.Empty;
                model.OKTMO = dep.DepartmentTaxDetails != null && dep.DepartmentTaxDetails.Count != 0 ? dep.DepartmentTaxDetails[0].OKTMO : string.Empty;
                model.OKATO = dep.DepartmentTaxDetails != null && dep.DepartmentTaxDetails.Count != 0 ? dep.DepartmentTaxDetails[0].OKATO : string.Empty;
                model.RegionCode = dep.DepartmentTaxDetails != null && dep.DepartmentTaxDetails.Count != 0 ? dep.DepartmentTaxDetails[0].RegionCode : string.Empty;
                model.TaxAdminCode = dep.DepartmentTaxDetails != null && dep.DepartmentTaxDetails.Count != 0 ? dep.DepartmentTaxDetails[0].TaxAdminCode : string.Empty;
                model.TaxAdminName = dep.DepartmentTaxDetails != null && dep.DepartmentTaxDetails.Count != 0 ? dep.DepartmentTaxDetails[0].TaxAdminName : string.Empty;
                model.PostAddress = dep.DepartmentTaxDetails != null && dep.DepartmentTaxDetails.Count != 0 ? dep.DepartmentTaxDetails[0].PostAddress : string.Empty;
            }

        }
        /// <summary>
        /// Определяем состояние согласования заявки.
        /// </summary>
        /// <param name="model">Модель</param>
        /// <param name="entity">Данные заявки</param>
        protected void SetApprovalFlags(StaffEstablishedPostRequestModel model, StaffEstablishedPostRequest entity)
        {
            User curUser = UserDao.Get(AuthenticationService.CurrentUser.Id);//текущий пользователь

            //заполняем списки согласовантов
            if (entity.Creator != null)
                GetApprovalLists(model, entity);

            //за согласовантов могут отработать кураторы и кадровики банка.
            model.IsCurator = (AuthenticationService.CurrentUser.UserRole == UserRole.Inspector);
            model.IsPersonnelBank = (AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantPersonnel);
            model.IsConsultant = (AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing);

            //разбираемся с состоянием птиц
            //выбираем из согласования не архивные записи.
            IList<DocumentApproval> DocApproval = DocumentApprovalDao.GetDocumentApproval(entity.Id, (int)ApprovalTypeEnum.StaffEstablishedPostRequest);

            //для новых/автоматически сформированных заявок
            if (DocApproval == null || DocApproval.Count == 0)
            {
                model.IsInitiatorApproveAvailable = entity.IsUsed ? false : (model.Initiators.Count != 0 && (model.IsCurator || model.IsPersonnelBank || model.IsConsultant || model.Initiators.Where(x => x.Id == curUser.Id).Count() != 0) ? true : false);
                model.IsTopManagerApproveAvailable = false;
                model.IsBoardMemberApproveAvailable = false;
                model.IsAgreeButtonAvailable = model.IsInitiatorApproveAvailable;
            }
            else
            {
                //потом проводим слесарную обработку по состоянию согласования 
                foreach (DocumentApproval item in DocApproval.OrderBy(x => x.Number))
                {
                    switch (item.Number)
                    {
                        case 1://инициатор
                            model.IsInitiatorApprove = true;
                            model.IsInitiatorApproveAvailable = false;
                            model.InitiatorApproveName = "Заявка создана " + item.CreateDate.Value.ToShortDateString() + " " + (item.AssistantUser == null ? "Инициатор: " + item.ApproveUser.Name + " - " + item.ApproveUser.Position.Name
                                : "Автор заявки: " + item.AssistantUser.Name + "; Инициатор: " + item.ApproveUser.Name + " - " + item.ApproveUser.Position.Name);
                            model.InitiatorId = item.AssistantUser == null ? item.ApproveUser.Id : item.AssistantUser.Id;

                            //открываем согласование для следующего участника процесса
                            model.IsCuratorApproveAvailable = model.IsCurator || model.IsConsultant ? true : false;
                            model.IsAgreeButtonAvailable = model.IsCuratorApproveAvailable;

                            //открываем согласование для следующего участника процесса
                            if (entity.Department.DepartmentAccessory != null)
                            {
                                if (entity.Department.DepartmentAccessory.Id == 2 || entity.Department.DepartmentAccessory.Id == 6)//кураторы согласуют фронты и бэкфронты
                                {
                                    model.IsCuratorApproveAvailable = model.IsCurator || model.IsConsultant ? true : false;
                                    model.IsAgreeButtonAvailable = model.IsCuratorApproveAvailable;
                                }
                                else
                                {
                                    model.IsPersonnelBankApproveAvailable = model.IsPersonnelBank || model.IsConsultant ? true : false;
                                    model.IsAgreeButtonAvailable = model.IsPersonnelBankApproveAvailable;
                                }
                            }
                            break;
                        case 2://куратор
                            model.IsCuratorApprove = true;
                            model.IsCuratorApproveAvailable = false;
                            model.CuratorApproveName = "Заявка проверена " + item.CreateDate.Value.ToShortDateString() + " " + "Куратор: " + item.ApproveUser.Name;

                            //открываем согласование для следующего участника процесса
                            model.IsPersonnelBankApproveAvailable = model.IsPersonnelBank || model.IsConsultant ? true : false;
                            model.IsAgreeButtonAvailable = model.IsPersonnelBankApproveAvailable;
                            break;
                        case 3://кадровик
                            model.IsPersonnelBankApprove = true;
                            model.IsPersonnelBankApproveAvailable = false;
                            model.PersonnelBankApproveName = "Заявка проверена " + item.CreateDate.Value.ToShortDateString() + " " + "Кадровик банка: " + item.ApproveUser.Name;

                            //открываем согласование для следующего участника процесса
                            model.IsTopManagerApproveAvailable = model.TopManagers.Count != 0 && (model.IsCurator || model.IsPersonnelBank || model.IsConsultant || model.TopManagers.Where(x => x.Id == curUser.Id).Count() != 0) ? true : false;
                            model.IsAgreeButtonAvailable = model.IsTopManagerApproveAvailable;
                            break;
                        case 4://вышестоящий руководитель
                            model.IsTopManagerApprove = true;
                            model.IsTopManagerApproveAvailable = false;
                            model.TopManagerApproveName = "Заявка согласована " + item.CreateDate.Value.ToShortDateString() + " " + (item.AssistantUser == null ? "Согласовант: " + item.ApproveUser.Name + " - " + item.ApproveUser.Position.Name
                                : "Согласовал: " + item.AssistantUser.Name + "; Согласовант: " + item.ApproveUser.Name + " - " + item.ApproveUser.Position.Name);
                            model.TopManagerId = item.AssistantUser == null ? item.ApproveUser.Id : item.AssistantUser.Id;

                            //открываем согласование для следующего участника процесса
                            model.IsBoardMemberApproveAvailable = model.BoardMembers.Count != 0 && (model.IsCurator || model.IsPersonnelBank || model.IsConsultant || model.BoardMembers.Where(x => x.Id == curUser.Id).Count() != 0) ? true : false;
                            model.IsAgreeButtonAvailable = model.IsBoardMemberApproveAvailable;
                            break;
                        case 5://член правления
                            model.IsBoardMemberApprove = true;
                            model.IsBoardMemberApproveAvailable = false;
                            model.BoardMemberApproveName = "Заявка утверждена " + item.CreateDate.Value.ToShortDateString() + " " + (item.AssistantUser == null ? "Утвердил: " + item.ApproveUser.Name + " - Член правления банка"// + item.ApproveUser.Position.Name
                                : "Утвердил: " + item.AssistantUser.Name + "; Утверждающий: " + item.ApproveUser.Name + " - Член правления банка");
                            model.BoardMemberId = item.AssistantUser == null ? item.ApproveUser.Id : item.AssistantUser.Id;
                            model.IsAgreeButtonAvailable = false;
                            break;
                    }
                }
            }


            if (UserRole.Manager != AuthenticationService.CurrentUser.UserRole && UserRole.Inspector != AuthenticationService.CurrentUser.UserRole
                    && UserRole.ConsultantOutsourcing != AuthenticationService.CurrentUser.UserRole && UserRole.ConsultantPersonnel != AuthenticationService.CurrentUser.UserRole
                    && UserRole.Director != AuthenticationService.CurrentUser.UserRole)
            {
                model.IsAgreeButtonAvailable = false;
            }
        }
        /// <summary>
        /// Процедура заполняет списки согласовантов, на случай, если за них согласовывают кураторы или кадровики банка.
        /// </summary>
        /// <param name="model">Модель</param>
        /// <param name="entity">Данные заявки.</param>
        protected void GetApprovalLists(StaffEstablishedPostRequestModel model, StaffEstablishedPostRequest entity)
        {
            //определяем родительское подразделение
            Department Parentdep = DepartmentDao.GetByCode(entity.Department.ParentId.ToString());

            //относительно родительского ищем руководителей
            //помним, что начальника может не быть в родительском подразделении, по этому ищем вверх по ветке всех руководителей
            IList<User> Initiators = DepartmentDao.GetDepartmentManagers(Parentdep.Id, true)
                .OrderByDescending<User, int?>(manager => manager.Level)
                .ToList<User>();


            ////если инициатором является куратор или кадровик банка, то по ветке подразделения находим руководителей на уровень выше создаваемого подразделения
            if (entity.Creator.Id == AuthenticationService.CurrentUser.Id && AuthenticationService.CurrentUser.UserRole == UserRole.Manager)
            {
                model.Initiators = Initiators.Where(x => x.Id == AuthenticationService.CurrentUser.Id).ToList().ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name + " - " + x.Position.Name });
            }
            else
            {
                model.Initiators = Initiators.Where(x => x.Level >= 3).ToList().ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name + " - " + x.Position.Name });
                //если создатель заявки руководитель, то позиционируемся на нем
                if (entity.Creator.UserRole == UserRole.Manager)
                    model.InitiatorId = entity.Creator.Id;
            }


            //вышестоящее руководство
            model.TopManagers = Initiators.Where(x => x.Level == 3).ToList().ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name + " - " + x.Position.Name });


            //члены правления
            model.BoardMembers = UserDao.GetUsersWithRole(UserRole.Director)
                .Where(x => x.IsActive)
                .ToList()
                .ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name + " - Член правления банка" });

        }
        #endregion

        #region Справочник ПО.
        /// <summary>
        /// Загрузка модели справочника ПО.
        /// </summary>
        /// <param name="model">Обрабатываемая модель.</param>
        /// <returns></returns>
        public StaffDepartmentSoftReferenceModel GetSoftReference(StaffDepartmentSoftReferenceModel model)
        {
            //StaffDepartmentSoftReferenceModel model = new StaffDepartmentSoftReferenceModel();

            //model.GroupList = StaffDepartmentSoftGroupDao.GetSoftGroups();
            //model.SoftGroupLink = StaffDepartmentSoftGroupLinksDao.GetSoftGroupLinks(model.SoftGroupId != 0 ? model.SoftGroupId : (model.GroupList.Count == 0 ? 0 : model.GroupList[0].Id));
            //model.SoftList = StaffDepartmentInstallSoftDao.GetInstallSoft();

            return model;
        }


        /// <summary>
        /// Создание/Сохранение данных.
        /// </summary>
        /// <param name="model">Обрабатываемая модель.</param>
        /// <param name="error">Для сообщений.</param>
        /// <returns></returns>
        public bool SaveSoftReference(StaffDepartmentSoftReferenceModel model, out string error)
        {
            error = string.Empty;
            //User curUser = UserDao.Load(AuthenticationService.CurrentUser.Id);

            ////создание новой группы ПО
            //if (model.TabIndex == 0 && model.SwitchOperation == 1)
            //{
            //    if (StaffDepartmentSoftGroupDao.LoadAll().Where(x => x.Name == model.SoftGroupName).Count() != 0)
            //    {
            //        error = "В базе данных уже есть такое название группы ПО!";
            //        return false;
            //    }
            //    else
            //    {
            //        StaffDepartmentSoftGroup NewSoftGroup = new StaffDepartmentSoftGroup() { Name = model.SoftGroupName, Creator = curUser, CreateDate = DateTime.Now };
            //        try 
            //        {
            //            StaffDepartmentSoftGroupDao.SaveAndFlush(NewSoftGroup);
            //            return true;
            //        }
            //        catch (Exception ex)
            //        {
            //            StaffDepartmentSoftGroupDao.RollbackTran();
            //            error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
            //            return false;
            //        }
            //    }
            //}

            ////создание нового ПО
            //if (model.TabIndex == 2 && model.SwitchOperation == 4)
            //{
            //    if (StaffDepartmentInstallSoftDao.LoadAll().Where(x => x.Name == model.SoftName).Count() != 0)
            //    {
            //        error = "В базе данных уже есть такое название ПО!";
            //        return false;
            //    }
            //    else
            //    {
            //        StaffDepartmentInstallSoft NewSoft = new StaffDepartmentInstallSoft() { Name = model.SoftName, Creator = curUser, CreateDate = DateTime.Now };
            //        try
            //        {
            //            StaffDepartmentInstallSoftDao.SaveAndFlush(NewSoft);
            //            return true;
            //        }
            //        catch (Exception ex)
            //        {
            //            StaffDepartmentInstallSoftDao.RollbackTran();
            //            error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
            //            return false;
            //        }
            //    }
            //}


            ////редактирование названия группы ПО
            //if (model.TabIndex == 0 && model.SwitchOperation == 2)
            //{
            //    StaffDepartmentSoftGroup SoftGroup = StaffDepartmentSoftGroupDao.Get(model.GroupId.HasValue ? model.GroupId.Value : 0);
            //    if (SoftGroup == null)
            //    {
            //        error = "Нет записи для редактирования!";
            //        return false;
            //    }
            //    try
            //    {
            //        SoftGroup.Name = model.GroupList.Where(x => x.Id == model.GroupId.Value).Single().Name;
            //        SoftGroup.Editor = curUser;
            //        SoftGroup.EditDate = DateTime.Now;

            //        StaffDepartmentSoftGroupDao.SaveAndFlush(SoftGroup);
            //        return true;
            //    }
            //    catch (Exception ex)
            //    {
            //        StaffDepartmentSoftGroupDao.RollbackTran();
            //        error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
            //        return false;
            //    }
            //}


            ////редактирование названия ПО
            //if (model.TabIndex == 2 && model.SwitchOperation == 5)
            //{
            //    StaffDepartmentInstallSoft Soft = StaffDepartmentInstallSoftDao.Get(model.SoftId.HasValue ? model.SoftId.Value : 0);
            //    if (Soft == null)
            //    {
            //        error = "Нет записи для редактирования!";
            //        return false;
            //    }
            //    try
            //    {
            //        Soft.Name = model.SoftList.Where(x => x.Id == model.SoftId.Value).Single().Name;
            //        Soft.Editor = curUser;
            //        Soft.EditDate = DateTime.Now;

            //        StaffDepartmentInstallSoftDao.SaveAndFlush(Soft);
            //        return true;
            //    }
            //    catch (Exception ex)
            //    {
            //        StaffDepartmentInstallSoftDao.RollbackTran();
            //        error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
            //        return false;
            //    }
            //}

            ////Редактирование связей ПО с группами
            //if (model.TabIndex == 1)
            //{
            //    //цикл по записям связи для данной группы с клиента
            //    foreach(var row in model.SoftGroupLink)
            //    {
            //        StaffDepartmentSoftGroupLinks sgLink = StaffDepartmentSoftGroupLinksDao.Get(row.Id);

            //        //создаем новую запись/связь
            //        if (sgLink == null && row.IsUsed)
            //        {
            //            sgLink = new StaffDepartmentSoftGroupLinks() {
            //                InstallSoft = row.SoftId == 0 ? null : StaffDepartmentInstallSoftDao.Get(row.SoftId),
            //                SoftGroup = model.SoftGroupId == 0 ? null : StaffDepartmentSoftGroupDao.Get(model.SoftGroupId),
            //                IsUsed = true,
            //                Creator = curUser,
            //                CreateDate = DateTime.Now
            //            };
            //        }

            //        //редактируем связь
            //        if (sgLink != null)
            //        {
            //            if (sgLink.IsUsed != row.IsUsed)
            //            {
            //                sgLink.InstallSoft = row.SoftId == 0 ? null : StaffDepartmentInstallSoftDao.Get(row.SoftId);
            //                sgLink.SoftGroup = model.SoftGroupId == 0 ? null : StaffDepartmentSoftGroupDao.Get(model.SoftGroupId);
            //                sgLink.IsUsed = row.IsUsed;
            //                sgLink.Editor = curUser;
            //                sgLink.EditDate = DateTime.Now;
            //            }
            //        }

            //        try
            //        {
            //            if (sgLink != null)
            //                StaffDepartmentSoftGroupLinksDao.SaveAndFlush(sgLink);
            //            //return true;
            //        }
            //        catch (Exception ex)
            //        {
            //            StaffDepartmentSoftGroupLinksDao.RollbackTran();
            //            error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
            //            return false;
            //        }
            //    }
            //}

            return true;
        }

        #region Справочник групп ПО
        /// <summary>
        /// Загрузка справочника групп ПО.
        /// </summary>
        /// <param name="model">Обрабатываемая модель</param>
        /// <returns></returns>
        public StaffDepartmentSoftGroupModel GetStaffDepartmentSoftGroup(StaffDepartmentSoftGroupModel model)
        {
            model.GroupList = StaffDepartmentSoftGroupDao.GetSoftGroups();
            return model;
        }

        /// <summary>
        /// Сохраняем данные справочника групп ПО.
        /// </summary>
        /// <param name="itemToAddEdit"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool SaveStaffDepartmentSoftGroup(StaffDepartmentSoftGroupDto itemToAddEdit, out string error)
        {
            error = string.Empty;
            User curUser = UserDao.Load(AuthenticationService.CurrentUser.Id);

            StaffDepartmentSoftGroup entity = itemToAddEdit.gId == 0 ? null : StaffDepartmentSoftGroupDao.Load(itemToAddEdit.gId);
            if (entity == null)
            {
                entity = new StaffDepartmentSoftGroup()
                {
                    Name = itemToAddEdit.gName,
                    Creator = curUser,
                    CreateDate = DateTime.Now
                };
            }
            else
            {
                entity.Name = itemToAddEdit.gName;
                entity.Editor = curUser;
                entity.EditDate = DateTime.Now;
            }

            try
            {
                StaffDepartmentSoftGroupDao.SaveAndFlush(entity);
                error = "Данные сохранены!";
            }
            catch (Exception ex)
            {
                StaffDepartmentSoftGroupDao.RollbackTran();
                error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Проверка сохраняемой строки справочника групп ПО.
        /// </summary>
        /// <param name="Row">Строка.</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool ValidateDepartmentSoftGroupRow(StaffDepartmentSoftGroupDto Row, out string error)
        {
            //решил сделать все проврки здесь, чтобы все было в одном месте.
            error = string.Empty;

            //проверка на заполнение полей
            if (string.IsNullOrEmpty(Row.gName) || string.IsNullOrWhiteSpace(Row.gName))
            {
                error = "Полe 'Название группы ПО' должно быть заполнено!";
                return false;
            }

            //проверка на повтор полей
            IList<StaffDepartmentSoftGroup> db = StaffDepartmentSoftGroupDao.LoadAll();
            if (db != null && db.Count != 0)
            {
                if (db.Where(x => x.Name == Row.gName && x.Id != Row.gId).Count() > 0)
                {
                    error = "Строка с таким названием группы ПО уже существует!";
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region Справочник банковского ПО
        /// <summary>
        /// Загрузка справочника банковского ПО.
        /// </summary>
        /// <param name="model">Обрабатываемая модель</param>
        /// <returns></returns>
        public StaffDepartmentInstallSoftModel GetStaffDepartmentInstallSoft(StaffDepartmentInstallSoftModel model)
        {
            model.SoftList = StaffDepartmentInstallSoftDao.GetInstallSoft();
            return model;
        }

        /// <summary>
        /// Сохраняем данные справочника банковского ПО.
        /// </summary>
        /// <param name="itemToAddEdit"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool SaveStaffDepartmentInstallSoft(StaffDepartmentInstallSoftDto itemToAddEdit, out string error)
        {
            error = string.Empty;
            User curUser = UserDao.Load(AuthenticationService.CurrentUser.Id);

            StaffDepartmentInstallSoft entity = itemToAddEdit.sId == 0 ? null : StaffDepartmentInstallSoftDao.Load(itemToAddEdit.sId);
            if (entity == null)
            {
                entity = new StaffDepartmentInstallSoft()
                {
                    Name = itemToAddEdit.sName,
                    Creator = curUser,
                    CreateDate = DateTime.Now
                };
            }
            else
            {
                entity.Name = itemToAddEdit.sName;
                entity.Editor = curUser;
                entity.EditDate = DateTime.Now;
            }

            try
            {
                StaffDepartmentInstallSoftDao.SaveAndFlush(entity);
                error = "Данные сохранены!";
            }
            catch (Exception ex)
            {
                StaffDepartmentInstallSoftDao.RollbackTran();
                error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Проверка сохраняемой строки справочника банковского ПО.
        /// </summary>
        /// <param name="Row">Строка.</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool ValidateDepartmentInstallSoftRow(StaffDepartmentInstallSoftDto Row, out string error)
        {
            //решил сделать все проврки здесь, чтобы все было в одном месте.
            error = string.Empty;

            //проверка на заполнение полей
            if (string.IsNullOrEmpty(Row.sName) || string.IsNullOrWhiteSpace(Row.sName))
            {
                error = "Полe 'Название ПО' должно быть заполнено!";
                return false;
            }

            //проверка на повтор полей
            IList<StaffDepartmentInstallSoft> db = StaffDepartmentInstallSoftDao.LoadAll();
            if (db != null && db.Count != 0)
            {
                if (db.Where(x => x.Name == Row.sName && x.Id != Row.sId).Count() > 0)
                {
                    error = "Строка с таким названием ПО уже существует!";
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region Связи групп с ПО
        /// <summary>
        /// Загрузка связей.
        /// </summary>
        /// <param name="model">Обрабатываемая модель</param>
        /// <param name="SoftGroupId">Id группы ПО</param>
        /// <returns></returns>
        public StaffDepartmentSoftGroupLinksModel GetStaffDepartmentSoftGroupLinks(StaffDepartmentSoftGroupLinksModel model, int SoftGroupId)
        {
            model.GroupList = StaffDepartmentSoftGroupDao.GetSoftGroups();
            SoftGroupId = SoftGroupId != 0 ? SoftGroupId : (model.GroupList.Count != 0 ? model.GroupList[0].gId : 0);
            model.SoftGroupLink = StaffDepartmentSoftGroupLinksDao.GetSoftGroupLinks(SoftGroupId);
            return model;
        }

        /// <summary>
        /// Сохраняем данные связей ПО с группами.
        /// </summary>
        /// <param name="itemToAddEdit"></param>
        /// <param name="SoftGroupId">Id группы ПО</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool SaveStaffDepartmentSoftGroupLinks(IList<StaffDepartmentSoftGroupLinksDto> itemToAddEdit, int SoftGroupId, out string error)
        {
            error = string.Empty;
            User curUser = UserDao.Load(AuthenticationService.CurrentUser.Id);
            StaffDepartmentSoftGroupLinks entity = null;

            foreach (var item in itemToAddEdit)
            {
                //добавление
                if (item.Id == 0 && item.IsUsed)
                {
                    entity = new StaffDepartmentSoftGroupLinks()
                    {
                        InstallSoft = item.SoftId == 0 ? null : StaffDepartmentInstallSoftDao.Get(item.SoftId),
                        SoftGroup = SoftGroupId == 0 ? null : StaffDepartmentSoftGroupDao.Get(SoftGroupId),
                        IsUsed = item.IsUsed,
                        Creator = curUser,
                        CreateDate = DateTime.Now
                    };

                }

                //редакирование
                if (item.Id != 0)
                {
                    entity = StaffDepartmentSoftGroupLinksDao.Load(item.Id);
                    entity.IsUsed = item.IsUsed;
                    entity.Editor = curUser;
                    entity.EditDate = DateTime.Now;
                }

                if (entity != null)
                {
                    try
                    {
                        StaffDepartmentSoftGroupLinksDao.SaveAndFlush(entity);
                        error = "Данные сохранены!";
                    }
                    catch (Exception ex)
                    {
                        StaffDepartmentSoftGroupLinksDao.RollbackTran();
                        error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                        return false;
                    }
                }
            }



            return true;
        }
        #endregion
        #endregion

        #region Справочник кодировок
        #region Справочник филиалов
        /// <summary>
        /// Загрузка справочника кодировок филиалов.
        /// </summary>
        /// <param name="model">Обрабатываемая модель</param>
        /// <returns></returns>
        public StaffDepartmentBranchModel GetStaffDepartmentBranch(StaffDepartmentBranchModel model)
        {
            model.Branches = StaffDepartmentBranchDao.GetDepartmentBranches();
            model.TwoLevelDeps = DepartmentDao.LoadAll().Where(x => x.ItemLevel == 2).ToList();
            return model;
        }

        /// <summary>
        /// Сохраняем данные справочника кодировок филиалов.
        /// </summary>
        /// <param name="itemToAddEdit"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool SaveStaffDepartmentBranch(StaffDepartmentBranchDto itemToAddEdit, out string error)
        {
            error = string.Empty;
            User curUser = UserDao.Load(AuthenticationService.CurrentUser.Id);

            StaffDepartmentBranch entity = itemToAddEdit.Id == 0 ? null : StaffDepartmentBranchDao.Load(itemToAddEdit.Id);
            if (entity == null)
            {
                entity = new StaffDepartmentBranch()
                {
                    Code = itemToAddEdit.Code,
                    Name = itemToAddEdit.Name,
                    Department = itemToAddEdit.DepartmentId == 0 ? null : DepartmentDao.Load(itemToAddEdit.DepartmentId),
                    Creator = curUser,
                    CreateDate = DateTime.Now
                };
            }
            else
            {
                entity.Code = itemToAddEdit.Code;
                entity.Name = itemToAddEdit.Name;
                entity.Department = itemToAddEdit.DepartmentId == 0 ? null : DepartmentDao.Load(itemToAddEdit.DepartmentId);
                entity.Editor = curUser;
                entity.EditDate = DateTime.Now;
            }

            try
            {
                StaffDepartmentBranchDao.SaveAndFlush(entity);
                error = "Данные сохранены!";
            }
            catch (Exception ex)
            {
                StaffDepartmentBranchDao.RollbackTran();
                error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Удаляе строки в справочнике кодировок филиалов.
        /// </summary>
        /// <param name="Id">Id удаляемой строки</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool DeleteStaffDepartmentBranch(int Id, out string error)
        {
            error = string.Empty;

            StaffDepartmentBranch entity = StaffDepartmentBranchDao.Load(Id);
            if (entity != null)
            {
                if (!StaffDepartmentBranchDao.IsEnableDelete(Id))
                {
                    error = "Удаление данной строки невозможно!";
                    return false;
                }

                try
                {
                    StaffDepartmentBranchDao.DeleteAndFlush(entity);
                    error = "Запись удалена!";
                    return true;
                }
                catch (Exception ex)
                {
                    StaffDepartmentBranchDao.RollbackTran();
                    error = string.Format("Произошла ошибка при удалении данных! Исключение:{0}", ex.GetBaseException().Message);
                    return false;
                }    
            }

            error = "Запись не найдена!";
            return true;
        }

        /// <summary>
        /// Проверка сохраняемой строки справочника кодировок филиалов.
        /// </summary>
        /// <param name="Row">Строка.</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool ValidateDepartmentBranchRow(StaffDepartmentBranchDto Row, out string error)
        {
            //решил сделать все проврки здесь, чтобы все было в одном месте.
            error = string.Empty;

            //проверка на заполнение полей
            if (string.IsNullOrEmpty(Row.Name) || string.IsNullOrWhiteSpace(Row.Name) || string.IsNullOrEmpty(Row.Code) || string.IsNullOrWhiteSpace(Row.Code))
            {
                error = "Поля Название и Код филиала должны быть заполнены!";
                return false;
            }

            //проверка на правильное заполнение поля с кодом
            if (Row.Code.Trim().Length != 2)
            {
                error = "Код филиала должен состоять из двух символов!";
                return false;
            }

            //проверка на повтор полей
            IList<StaffDepartmentBranch> db = StaffDepartmentBranchDao.LoadAll();
            if (db != null && db.Count != 0)
            {
                if (db.Where(x => x.Name == Row.Name && x.Id != Row.Id).Count() > 0)
                {
                    error = "Строка с таким названием филиала уже существует!";
                    return false;
                }

                if (db.Where(x => x.Code == Row.Code && x.Id != Row.Id).Count() > 0)
                {
                    error = "Строка с таким кодом филиала уже существует!";
                    return false;
                }

                //проверка на вторичную привязку к подразделениям СКД
                if (db.Where(x => x.Department != null).Where(x => x.Department.Id == Row.DepartmentId && x.Id != Row.Id).Count() > 0)
                {
                    error = "Это подразделение из СКД уже привязано к другому филиалу Финграда!";
                    return false;
                }
            }
                        
            return true;
        }
        #endregion

        #region Справочник дирекций
        /// <summary>
        /// Загрузка справочника кодировок дирекций.
        /// </summary>
        /// <param name="model">Обрабатываемая модель</param>
        /// <returns></returns>
        public StaffDepartmentManagementModel GetStaffDepartmentManagement(StaffDepartmentManagementModel model)
        {
            model.Managements = StaffDepartmentManagementDao.GetDepartmentManagements(0);
            model.Branches = StaffDepartmentBranchDao.GetDepartmentBranches();
            model.ThreeLevelDeps = DepartmentDao.LoadAll().Where(x => x.ItemLevel == 3 && !x.Name.Contains("не исп") && !x.Name.Contains("ГПД")).OrderBy(x => x.Name).ToList();
            return model;
        }

        /// <summary>
        /// Сохраняем данные справочника кодировок дирекций.
        /// </summary>
        /// <param name="itemToAddEdit"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool SaveStaffDepartmentManagement(StaffDepartmentManagementDto itemToAddEdit, out string error)
        {
            error = string.Empty;
            User curUser = UserDao.Load(AuthenticationService.CurrentUser.Id);

            StaffDepartmentManagement entity = itemToAddEdit.mId == 0 ? null : StaffDepartmentManagementDao.Load(itemToAddEdit.mId);
            if (entity == null)
            {
                entity = new StaffDepartmentManagement()
                {
                    Code = itemToAddEdit.mCode,
                    Name = itemToAddEdit.mName,
                    DepartmentBranch = itemToAddEdit.BranchId == 0 ? null : StaffDepartmentBranchDao.Get(itemToAddEdit.BranchId),
                    Department = itemToAddEdit.mDepartmentId == 0 ? null : DepartmentDao.Load(itemToAddEdit.mDepartmentId),
                    Creator = curUser,
                    CreateDate = DateTime.Now
                };
            }
            else
            {
                entity.Code = itemToAddEdit.mCode;
                entity.Name = itemToAddEdit.mName;
                entity.DepartmentBranch = itemToAddEdit.BranchId == 0 ? null : StaffDepartmentBranchDao.Get(itemToAddEdit.BranchId);
                entity.Department = itemToAddEdit.mDepartmentId == 0 ? null : DepartmentDao.Load(itemToAddEdit.mDepartmentId);
                entity.Editor = curUser;
                entity.EditDate = DateTime.Now;
            }

            try
            {
                StaffDepartmentManagementDao.SaveAndFlush(entity);
                error = "Данные сохранены!";
            }
            catch (Exception ex)
            {
                StaffDepartmentManagementDao.RollbackTran();
                error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Удаляе строки в справочнике кодировок дирекций.
        /// </summary>
        /// <param name="Id">Id удаляемой строки</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool DeleteStaffDepartmentManagement(int Id, out string error)
        {
            error = string.Empty;

            StaffDepartmentManagement entity = StaffDepartmentManagementDao.Load(Id);
            if (entity != null)
            {
                if (!StaffDepartmentManagementDao.IsEnableDelete(Id))
                {
                    error = "Удаление данной строки невозможно!";
                    return false;
                }

                try
                {
                    StaffDepartmentManagementDao.DeleteAndFlush(entity);
                    error = "Запись удалена!";
                    return true;
                }
                catch (Exception ex)
                {
                    StaffDepartmentManagementDao.RollbackTran();
                    error = string.Format("Произошла ошибка при удалении данных! Исключение:{0}", ex.GetBaseException().Message);
                    return false;
                }
            }

            error = "Запись не найдена!";
            return true;
        }

        /// <summary>
        /// Проверка сохраняемой строки справочника кодировок дирекций.
        /// </summary>
        /// <param name="Row">Строка.</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool ValidateDepartmentManagementRow(StaffDepartmentManagementDto Row, out string error)
        {
            //решил сделать все проврки здесь, чтобы все было в одном месте.
            error = string.Empty;
            
            //проверка на заполнение полей
            if (string.IsNullOrEmpty(Row.mName) || string.IsNullOrWhiteSpace(Row.mName) || string.IsNullOrEmpty(Row.mCode) || string.IsNullOrWhiteSpace(Row.mCode))
            {
                error = "Поля Название и Код дирекции должны быть заполнены!";
                return false;
            }

            //проверка на правильное заполнение поля с кодом
            if (Row.mCode.Trim().Length != 3)
            {
                error = "Код дирекции должен состоять из трех символов!";
                return false;
            }

            //проверка на повтор полей
            IList<StaffDepartmentManagement> db = StaffDepartmentManagementDao.LoadAll();
            if (db != null && db.Count != 0)
            {
                if (db.Where(x => x.Name == Row.mName && x.Id != Row.mId).Count() > 0)
                {
                    error = "Строка с таким названием филиала уже существует!";
                    return false;
                }

                if (db.Where(x => x.Code == Row.mCode && x.Id != Row.mId).Count() > 0)
                {
                    error = "Строка с таким кодом филиала уже существует!";
                    return false;
                }

                //проверка на вторичную привязку к подразделениям СКД
                if (db.Where(x => x.Department != null)
                    .Where(x => x.Department.Id == Row.mDepartmentId && x.Id != Row.mId)
                    .Count() > 0)
                {
                    error = "Это подразделение из СКД уже привязано к другой дирекции Финграда!";
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region Справочник управлений
        /// <summary>
        /// Загрузка справочника кодировок управлений.
        /// </summary>
        /// <param name="model">Обрабатываемая модель</param>
        /// <param name="ManagementFilterId">Id дирекции</param>
        /// <param name="BranchFilterId">Id филиала</param>
        /// <returns></returns>
        public StaffDepartmentAdministrationModel GetStaffDepartmentAdministration(StaffDepartmentAdministrationModel model, int ManagementFilterId, int BranchFilterId)
        {
            model.Administrations = StaffDepartmentAdministrationDao.GetDepartmentAdministrations(ManagementFilterId, BranchFilterId);
            model.Administrations.Insert(0, new StaffDepartmentAdministrationDto() { aId = 0, aName = "" });

            model.Managements = StaffDepartmentManagementDao.GetDepartmentManagements(BranchFilterId);
            model.Managements.Insert(0, new StaffDepartmentManagementDto() { mId = 0, mName = "" });

            model.Branches = StaffDepartmentBranchDao.GetDepartmentBranches();
            model.Branches.Insert(0, new StaffDepartmentBranchDto() { Id = 0, Name = "" });

            model.FourLevelDeps = DepartmentDao.LoadAll().Where(x => x.ItemLevel == 4 && !x.Name.Contains("не исп") && !x.Name.Contains("ГПД")).OrderBy(x => x.Name).ToList();
            return model;
        }

        /// <summary>
        /// Сохраняем данные справочника кодировок управлений.
        /// </summary>
        /// <param name="itemToAddEdit"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool SaveStaffDepartmentAdministration(StaffDepartmentAdministrationDto itemToAddEdit, out string error)
        {
            error = string.Empty;
            User curUser = UserDao.Load(AuthenticationService.CurrentUser.Id);

            StaffDepartmentAdministration entity = itemToAddEdit.aId == 0 ? null : StaffDepartmentAdministrationDao.Load(itemToAddEdit.aId);
            if (entity == null)
            {
                entity = new StaffDepartmentAdministration()
                {
                    Code = itemToAddEdit.aCode,
                    Name = itemToAddEdit.aName,
                    DepartmentManagement = itemToAddEdit.ManagementId == 0 ? null : StaffDepartmentManagementDao.Get(itemToAddEdit.ManagementId),
                    Department = itemToAddEdit.aDepartmentId == 0 ? null : DepartmentDao.Load(itemToAddEdit.aDepartmentId),
                    Creator = curUser,
                    CreateDate = DateTime.Now
                };
            }
            else
            {
                entity.Code = itemToAddEdit.aCode;
                entity.Name = itemToAddEdit.aName;
                entity.DepartmentManagement = itemToAddEdit.ManagementId == 0 ? null : StaffDepartmentManagementDao.Get(itemToAddEdit.ManagementId);
                entity.Department = itemToAddEdit.aDepartmentId == 0 ? null : DepartmentDao.Load(itemToAddEdit.aDepartmentId);
                entity.Editor = curUser;
                entity.EditDate = DateTime.Now;
            }

            try
            {
                StaffDepartmentAdministrationDao.SaveAndFlush(entity);
                error = "Данные сохранены!";
            }
            catch (Exception ex)
            {
                StaffDepartmentAdministrationDao.RollbackTran();
                error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Удаляе строки в справочнике кодировок управлений.
        /// </summary>
        /// <param name="Id">Id удаляемой строки</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool DeleteStaffDepartmentAdministration(int Id, out string error)
        {
            error = string.Empty;

            StaffDepartmentAdministration entity = StaffDepartmentAdministrationDao.Load(Id);
            if (entity != null)
            {
                if (!StaffDepartmentAdministrationDao.IsEnableDelete(Id))
                {
                    error = "Удаление данной строки невозможно!";
                    return false;
                }

                try
                {
                    StaffDepartmentAdministrationDao.DeleteAndFlush(entity);
                    error = "Запись удалена!";
                    return true;
                }
                catch (Exception ex)
                {
                    StaffDepartmentAdministrationDao.RollbackTran();
                    error = string.Format("Произошла ошибка при удалении данных! Исключение:{0}", ex.GetBaseException().Message);
                    return false;
                }
            }

            error = "Запись не найдена!";
            return true;
        }

        /// <summary>
        /// Проверка сохраняемой строки справочника кодировок управлений.
        /// </summary>
        /// <param name="Row">Строка.</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool ValidateDepartmentAdministrationRow(StaffDepartmentAdministrationDto Row, out string error)
        {
            //решил сделать все проврки здесь, чтобы все было в одном месте.
            error = string.Empty;

            //проверка на заполнение полей
            if (string.IsNullOrEmpty(Row.aName) || string.IsNullOrWhiteSpace(Row.aName) || string.IsNullOrEmpty(Row.aCode) || string.IsNullOrWhiteSpace(Row.aCode))
            {
                error = "Поля Название и Код управления должны быть заполнены!";
                return false;
            }

            //проверка на правильное заполнение поля с кодом
            if (Row.aCode.Trim().Length != 7)
            {
                error = "Код управления должен состоять из 7 символов!";
                return false;
            }

            //проверка на повтор полей
            IList<StaffDepartmentAdministration> db = StaffDepartmentAdministrationDao.LoadAll();
            if (db != null && db.Count != 0)
            {
                if (db.Where(x => x.Name == Row.aName && x.Id != Row.aId).Count() > 0)
                {
                    error = "Строка с таким названием управления уже существует!";
                    return false;
                }

                if (db.Where(x => x.Code == Row.aCode && x.Id != Row.aId).Count() > 0)
                {
                    error = "Строка с таким кодом управления уже существует!";
                    return false;
                }

                //проверка на вторичную привязку к подразделениям СКД
                if (db.Where(x => x.Department != null)
                    .Where(x => x.Department.Id == Row.aDepartmentId && x.Id != Row.aId)
                    .Count() > 0)
                {
                    error = "Это подразделение из СКД уже привязано к другому управлению Финграда!";
                    return false;
                }
            }

            return true;
        }

        protected string CreateAdministrationCode()
        {
            return "";
        }
        #endregion

        #region Справочник бизнес-групп
        /// <summary>
        /// Загрузка справочника кодировок бизнес-групп.
        /// </summary>
        /// <param name="model">Обрабатываемая модель</param>
        /// <param name="AdminFilterId">Id управления.</param>
        /// <param name="ManagementFilterId">Id дирекции</param>
        /// <param name="BranchFilterId">Id филиала</param>
        /// <returns></returns>
        public StaffDepartmentBusinessGroupModel GetStaffDepartmentBusinessGroup(StaffDepartmentBusinessGroupModel model, int AdminFilterId, int ManagementFilterId, int BranchFilterId)
        {
            model.BusinessGroups = StaffDepartmentBusinessGroupDao.GetDepartmentBusinessGroups(AdminFilterId, ManagementFilterId, BranchFilterId);

            model.Administrations = StaffDepartmentAdministrationDao.GetDepartmentAdministrations(ManagementFilterId, BranchFilterId);
            model.Administrations.Insert(0, new StaffDepartmentAdministrationDto() { aId = 0, aName = "" });

            model.Managements = StaffDepartmentManagementDao.GetDepartmentManagements(BranchFilterId);
            model.Managements.Insert(0, new StaffDepartmentManagementDto() { mId = 0, mName = "" });

            model.Branches = StaffDepartmentBranchDao.GetDepartmentBranches();
            model.Branches.Insert(0, new StaffDepartmentBranchDto() { Id = 0, Name = "" });

            model.FiveLevelDeps = DepartmentDao.LoadAll().Where(x => x.ItemLevel == 5 && !x.Name.Contains("не исп") && !x.Name.Contains("ГПД")).OrderBy(x => x.Name).ToList();
            return model;
        }

        /// <summary>
        /// Сохраняем данные справочника кодировок бизнес-групп.
        /// </summary>
        /// <param name="itemToAddEdit"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool SaveStaffDepartmentBusinessGroup(StaffDepartmentBusinessGroupDto itemToAddEdit, out string error)
        {
            error = string.Empty;
            User curUser = UserDao.Load(AuthenticationService.CurrentUser.Id);

            StaffDepartmentBusinessGroup entity = itemToAddEdit.bId == 0 ? null : StaffDepartmentBusinessGroupDao.Load(itemToAddEdit.bId);
            if (entity == null)
            {
                entity = new StaffDepartmentBusinessGroup()
                {
                    Code = itemToAddEdit.bCode,
                    Name = itemToAddEdit.bName,
                    DepartmentAdministration = itemToAddEdit.AdminId == 0 ? null : StaffDepartmentAdministrationDao.Get(itemToAddEdit.AdminId),
                    Department = itemToAddEdit.bDepartmentId == 0 ? null : DepartmentDao.Load(itemToAddEdit.bDepartmentId),
                    Creator = curUser,
                    CreateDate = DateTime.Now
                };
            }
            else
            {
                entity.Code = itemToAddEdit.bCode;
                entity.Name = itemToAddEdit.bName;
                entity.DepartmentAdministration = itemToAddEdit.AdminId == 0 ? null : StaffDepartmentAdministrationDao.Get(itemToAddEdit.AdminId);
                entity.Department = itemToAddEdit.bDepartmentId == 0 ? null : DepartmentDao.Load(itemToAddEdit.bDepartmentId);
                entity.Editor = curUser;
                entity.EditDate = DateTime.Now;
            }

            try
            {
                StaffDepartmentBusinessGroupDao.SaveAndFlush(entity);
                error = "Данные сохранены!";
            }
            catch (Exception ex)
            {
                StaffDepartmentBusinessGroupDao.RollbackTran();
                error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Удаляе строки в справочнике кодировок бизнес-групп.
        /// </summary>
        /// <param name="Id">Id удаляемой строки</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool DeleteStaffDepartmentBusinessGroup(int Id, out string error)
        {
            error = string.Empty;

            StaffDepartmentBusinessGroup entity = StaffDepartmentBusinessGroupDao.Load(Id);
            
            if (entity != null)
            {
                if (!StaffDepartmentBusinessGroupDao.IsEnableDelete(Id))
                {
                    error = "Удаление данной строки невозможно!";
                    return false;
                }

                try
                {
                    StaffDepartmentBusinessGroupDao.DeleteAndFlush(entity);
                    error = "Запись удалена!";
                    return true;
                }
                catch (Exception ex)
                {
                    StaffDepartmentBusinessGroupDao.RollbackTran();
                    error = string.Format("Произошла ошибка при удалении данных! Исключение:{0}", ex.GetBaseException().Message);
                    return false;
                }
            }

            error = "Запись не найдена!";
            return true;
        }

        /// <summary>
        /// Проверка сохраняемой строки справочника кодировок бизнес-групп.
        /// </summary>
        /// <param name="Row">Строка.</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool ValidateDepartmentBusinessGroupRow(StaffDepartmentBusinessGroupDto Row, out string error)
        {
            //решил сделать все проврки здесь, чтобы все было в одном месте.
            error = string.Empty;

            //проверка на заполнение полей
            if (string.IsNullOrEmpty(Row.bName) || string.IsNullOrWhiteSpace(Row.bName) || string.IsNullOrEmpty(Row.bCode) || string.IsNullOrWhiteSpace(Row.bCode))
            {
                error = "Поля Название и Код бизнес-группы должны быть заполнены!";
                return false;
            }

            //проверка на правильное заполнение поля с кодом
            if (Row.bCode.Trim().Length != 11)
            {
                error = "Код бизнес-группы должен состоять из 11 символов!";
                return false;
            }

            //проверка на повтор полей
            IList<StaffDepartmentBusinessGroup> db = StaffDepartmentBusinessGroupDao.LoadAll();
            if (db != null && db.Count != 0)
            {
                if (db.Where(x => x.Name == Row.bName && x.Id != Row.bId).Count() > 0)
                {
                    error = "Строка с таким названием бизнес-группы уже существует!";
                    return false;
                }

                if (db.Where(x => x.Code == Row.bCode && x.Id != Row.bId).Count() > 0)
                {
                    error = "Строка с таким кодом бизнес-группы уже существует!";
                    return false;
                }

                //проверка на вторичную привязку к подразделениям СКД
                if (db.Where(x => x.Department != null)
                    .Where(x => x.Department.Id == Row.bDepartmentId && x.Id != Row.bId)
                    .Count() > 0)
                {
                    error = "Эта подразделение из СКД уже привязано к другой бизнес-группе Финграда!";
                    return false;
                }
            }

            return true;
        }

        protected string CreateBusinessGroupCode()
        {
            return "";
        }
        #endregion

        #region Справочник РП-привязок
        /// <summary>
        /// Загрузка справочника кодировок РП-привязок.
        /// </summary>
        /// <param name="model">Обрабатываемая модель</param>
        /// <param name="BGFilterId">Id бизнес-группы</param>
        /// <param name="AdminFilterId">Id управления.</param>
        /// <param name="ManagementFilterId">Id дирекции</param>
        /// <param name="BranchFilterId">Id филиала</param>
        /// <returns></returns>
        public StaffDepartmentRPLinkModel GetStaffDepartmentRPLink(StaffDepartmentRPLinkModel model, int BGFilterId, int AdminFilterId, int ManagementFilterId, int BranchFilterId)
        {
            model.RPLinks = StaffDepartmentRPLinkDao.GetDepartmentRPLinks(BGFilterId, AdminFilterId, ManagementFilterId, BranchFilterId);

            model.BusinessGroups = StaffDepartmentBusinessGroupDao.GetDepartmentBusinessGroups(AdminFilterId, ManagementFilterId, BranchFilterId);
            model.BusinessGroups.Insert(0, new StaffDepartmentBusinessGroupDto() { bId = 0, bName = "" });

            model.Administrations = StaffDepartmentAdministrationDao.GetDepartmentAdministrations(ManagementFilterId, BranchFilterId);
            model.Administrations.Insert(0, new StaffDepartmentAdministrationDto() { aId = 0, aName = "" });

            model.Managements = StaffDepartmentManagementDao.GetDepartmentManagements(BranchFilterId);
            model.Managements.Insert(0, new StaffDepartmentManagementDto() { mId = 0, mName = "" });

            model.Branches = StaffDepartmentBranchDao.GetDepartmentBranches();
            model.Branches.Insert(0, new StaffDepartmentBranchDto() { Id = 0, Name = "" });

            model.SixLevelDeps = DepartmentDao.LoadAll().Where(x => x.ItemLevel == 6 && !x.Name.Contains("не исп") && !x.Name.Contains("ГПД")).OrderBy(x => x.Name).ToList();
            return model;
        }

        /// <summary>
        /// Сохраняем данные справочника кодировок РП-привязок.
        /// </summary>
        /// <param name="itemToAddEdit"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool SaveStaffDepartmentRPLink(StaffDepartmentRPLinkDto itemToAddEdit, out string error)
        {
            error = string.Empty;
            User curUser = UserDao.Load(AuthenticationService.CurrentUser.Id);

            StaffDepartmentRPLink entity = itemToAddEdit.rId == 0 ? null : StaffDepartmentRPLinkDao.Load(itemToAddEdit.rId);
            if (entity == null)
            {
                entity = new StaffDepartmentRPLink()
                {
                    Code = itemToAddEdit.rCode,
                    Name = itemToAddEdit.rName,
                    DepartmentBG = itemToAddEdit.BGId == 0 ? null : StaffDepartmentBusinessGroupDao.Get(itemToAddEdit.BGId),
                    Department = itemToAddEdit.rDepartmentId == 0 ? null : DepartmentDao.Load(itemToAddEdit.rDepartmentId),
                    Creator = curUser,
                    CreateDate = DateTime.Now
                };
            }
            else
            {
                entity.Code = itemToAddEdit.rCode;
                entity.Name = itemToAddEdit.rName;
                entity.DepartmentBG = itemToAddEdit.BGId == 0 ? null : StaffDepartmentBusinessGroupDao.Get(itemToAddEdit.BGId);
                entity.Department = itemToAddEdit.rDepartmentId == 0 ? null : DepartmentDao.Load(itemToAddEdit.rDepartmentId);
                entity.Editor = curUser;
                entity.EditDate = DateTime.Now;
            }

            try
            {
                StaffDepartmentRPLinkDao.SaveAndFlush(entity);
                error = "Данные сохранены!";
            }
            catch (Exception ex)
            {
                StaffDepartmentRPLinkDao.RollbackTran();
                error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Удаляе строки в справочнике кодировок РП-привязок.
        /// </summary>
        /// <param name="Id">Id удаляемой строки</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool DeleteStaffDepartmentRPLink(int Id, out string error)
        {
            error = string.Empty;

            StaffDepartmentRPLink entity = StaffDepartmentRPLinkDao.Load(Id);
            if (entity != null)
            {
                if (!StaffDepartmentRPLinkDao.IsEnableDelete(Id))
                {
                    error = "Удаление данной строки невозможно!";
                    return false;
                }

                try
                {
                    StaffDepartmentRPLinkDao.DeleteAndFlush(entity);
                    error = "Запись удалена!";
                    return true;
                }
                catch (Exception ex)
                {
                    StaffDepartmentRPLinkDao.RollbackTran();
                    error = string.Format("Произошла ошибка при удалении данных! Исключение:{0}", ex.GetBaseException().Message);
                    return false;
                }
            }

            error = "Запись не найдена!";
            return true;
        }

        /// <summary>
        /// Проверка сохраняемой строки справочника кодировок РП-привязок.
        /// </summary>
        /// <param name="Row">Строка.</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool ValidateDepartmentRPLinkRow(StaffDepartmentRPLinkDto Row, out string error)
        {
            //решил сделать все проврки здесь, чтобы все было в одном месте.
            error = string.Empty;

            //проверка на заполнение полей
            if (string.IsNullOrEmpty(Row.rName) || string.IsNullOrWhiteSpace(Row.rName) || string.IsNullOrEmpty(Row.rCode) || string.IsNullOrWhiteSpace(Row.rCode))
            {
                error = "Поля Название и Код РП-привязки должны быть заполнены!";
                return false;
            }

            //проверка на правильное заполнение поля с кодом
            if (Row.rCode.Trim().Length != 12)
            {
                error = "Код РП-привязи должен состоять из 12 символов!";
                return false;
            }

            //проверка на повтор полей
            IList<StaffDepartmentRPLink> db = StaffDepartmentRPLinkDao.LoadAll();
            if (db != null && db.Count != 0)
            {
                if (db.Where(x => x.Name == Row.rName && x.Id != Row.rId).Count() > 0)
                {
                    error = "Строка с таким названием РП-привязки уже существует!";
                    return false;
                }

                if (db.Where(x => x.Code == Row.rCode && x.Id != Row.rId).Count() > 0)
                {
                    error = "Строка с таким кодом РП-привязки уже существует!";
                    return false;
                }

                //проверка на вторичную привязку к подразделениям СКД
                if (db.Where(x => x.Department != null)
                    .Where(x => x.Department.Id == Row.rDepartmentId && x.Id != Row.rId)
                    .Count() > 0)
                {
                    error = "Эта подразделение из СКД уже привязано к другой РП-привязке Финграда!";
                    return false;
                }
            }

            return true;
        }

        protected string CreateRPLinkCode()
        {
            return "";
        }
        #endregion
        #endregion

        #region Справочник операций подразделений

        #region Справочник групп операций
        /// <summary>
        /// Загрузка справочника групп операций.
        /// </summary>
        /// <param name="model">Обрабатываемая модель</param>
        /// <returns></returns>
        public StaffDepartmentOperationGroupsModel GetStaffDepartmentOperationGroups(StaffDepartmentOperationGroupsModel model)
        {
            model.OperationGroups = StaffDepartmentOperationGroupsDao.GetOperationGroups();
            return model;
        }

        /// <summary>
        /// Сохраняем данные справочника групп операций.
        /// </summary>
        /// <param name="itemToAddEdit"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool SaveStaffDepartmentOperationGroups(StaffDepartmentOperationGroupsDto itemToAddEdit, out string error)
        {
            error = string.Empty;
            User curUser = UserDao.Load(AuthenticationService.CurrentUser.Id);

            StaffDepartmentOperationGroups entity = itemToAddEdit.gId == 0 ? null : StaffDepartmentOperationGroupsDao.Load(itemToAddEdit.gId);
            if (entity == null)
            {
                entity = new StaffDepartmentOperationGroups()
                {
                    Name = itemToAddEdit.gName,
                    IsUsed = itemToAddEdit.gIsUsed,
                    Creator = curUser,
                    CreateDate = DateTime.Now
                };
            }
            else
            {
                entity.Name = itemToAddEdit.gName;
                entity.IsUsed = itemToAddEdit.gIsUsed;
                entity.Editor = curUser;
                entity.EditDate = DateTime.Now;
            }

            try
            {
                StaffDepartmentOperationGroupsDao.SaveAndFlush(entity);
                error = "Данные сохранены!";
            }
            catch (Exception ex)
            {
                StaffDepartmentOperationGroupsDao.RollbackTran();
                error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Проверка сохраняемой строки справочника групп операций.
        /// </summary>
        /// <param name="Row">Строка.</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool ValidateDepartmentOperationGroupsRow(StaffDepartmentOperationGroupsDto Row, out string error)
        {
            //решил сделать все проврки здесь, чтобы все было в одном месте.
            error = string.Empty;

            //проверка на заполнение полей
            if (string.IsNullOrEmpty(Row.gName) || string.IsNullOrWhiteSpace(Row.gName))
            {
                error = "Поле 'Название группы' должны быть заполнены!";
                return false;
            }


            //проверка на повтор полей
            IList<StaffDepartmentOperationGroups> db = StaffDepartmentOperationGroupsDao.LoadAll();
            if (db != null && db.Count != 0)
            {
                if (db.Where(x => x.Name == Row.gName && x.Id != Row.gId).Count() > 0)
                {
                    error = "Строка с таким названием группы операции уже существует!";
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region Справочник операций
        /// <summary>
        /// Загрузка справочника операций.
        /// </summary>
        /// <param name="model">Обрабатываемая модель</param>
        /// <returns></returns>
        public StaffDepartmentOperationsModel GetStaffDepartmentOperations(StaffDepartmentOperationsModel model)
        {
            model.Operations = StaffDepartmentOperationsDao.GetOperations();
            return model;
        }

        /// <summary>
        /// Сохраняем данные справочника операций.
        /// </summary>
        /// <param name="itemToAddEdit"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool SaveStaffDepartmentOperations(StaffDepartmentOperationsDto itemToAddEdit, out string error)
        {
            error = string.Empty;
            User curUser = UserDao.Load(AuthenticationService.CurrentUser.Id);

            StaffDepartmentOperations entity = itemToAddEdit.oId == 0 ? null : StaffDepartmentOperationsDao.Load(itemToAddEdit.oId);
            if (entity == null)
            {
                entity = new StaffDepartmentOperations()
                {
                    Name = itemToAddEdit.oName,
                    IsUsed = itemToAddEdit.oIsUsed,
                    Creator = curUser,
                    CreateDate = DateTime.Now
                };
            }
            else
            {
                entity.Name = itemToAddEdit.oName;
                entity.IsUsed = itemToAddEdit.oIsUsed;
                entity.Editor = curUser;
                entity.EditDate = DateTime.Now;
            }

            try
            {
                StaffDepartmentOperationsDao.SaveAndFlush(entity);
                error = "Данные сохранены!";
            }
            catch (Exception ex)
            {
                StaffDepartmentOperationsDao.RollbackTran();
                error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Проверка сохраняемой строки справочника операций.
        /// </summary>
        /// <param name="Row">Строка.</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool ValidateDepartmentOperationRow(StaffDepartmentOperationsDto Row, out string error)
        {
            //решил сделать все проврки здесь, чтобы все было в одном месте.
            error = string.Empty;

            //проверка на заполнение полей
            if (string.IsNullOrEmpty(Row.oName) || string.IsNullOrWhiteSpace(Row.oName))
            {
                error = "Поле 'Название операции' должно быть заполнено!";
                return false;
            }


            //проверка на повтор полей
            IList<StaffDepartmentOperations> db = StaffDepartmentOperationsDao.LoadAll();
            if (db != null && db.Count != 0)
            {
                if (db.Where(x => x.Name == Row.oName && x.Id != Row.oId).Count() > 0)
                {
                    error = "Строка с таким названием операции уже существует!";
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region Связи групп с операциями
        /// <summary>
        /// Загрузка связей.
        /// </summary>
        /// <param name="model">Обрабатываемая модель</param>
        /// <param name="OperationGroupId">Id группы операций</param>
        /// <returns></returns>
        public StaffDepartmentOperationLinksModel GetStaffDepartmentOperationLinks(StaffDepartmentOperationLinksModel model, int OperationGroupId)
        {
            model.OperationGroups = StaffDepartmentOperationGroupsDao.GetOperationGroups();
            OperationGroupId = OperationGroupId != 0 ? OperationGroupId : (model.OperationGroups.Count != 0 ? model.OperationGroups[0].gId : 0);
            model.OperationList = StaffDepartmentOperationLinksDao.GetOperationGroupLinks(OperationGroupId);
            return model;
        }

        /// <summary>
        /// Сохраняем данные связей операций с группами.
        /// </summary>
        /// <param name="itemToAddEdit"></param>
        /// <param name="OperationGroupId">Id группы операций</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool SaveStaffDepartmentOperationLinks(IList<StaffDepartmentOperationLinksDto> itemToAddEdit, int OperationGroupId, out string error)
        {
            error = string.Empty;
            User curUser = UserDao.Load(AuthenticationService.CurrentUser.Id);
            StaffDepartmentOperationLinks entity = null;

            foreach(var item in itemToAddEdit)
            {
                //добавление
                if (item.Id == 0 && item.IsLink)
                {
                    entity = new StaffDepartmentOperationLinks()
                    {
                        DepartmentOperation = item.OperationId == 0 ? null : StaffDepartmentOperationsDao.Get(item.OperationId),
                        DepartmentOperationGroup = OperationGroupId == 0 ? null : StaffDepartmentOperationGroupsDao.Get(OperationGroupId),
                        IsUsed = item.IsLink,
                        Creator = curUser,
                        CreateDate = DateTime.Now
                    };

                }

                //редакирование
                if (item.Id != 0)
                {
                    entity = StaffDepartmentOperationLinksDao.Load(item.Id);
                    entity.IsUsed = item.IsLink;
                    entity.Editor = curUser;
                    entity.EditDate = DateTime.Now;
                }

                if (entity != null)
                {
                    try
                    {
                        StaffDepartmentOperationLinksDao.SaveAndFlush(entity);
                        error = "Данные сохранены!";
                    }
                    catch (Exception ex)
                    {
                        StaffDepartmentOperationLinksDao.RollbackTran();
                        error = string.Format("Произошла ошибка при сохранении данных! Исключение:{0}", ex.GetBaseException().Message);
                        return false;
                    }
                }
            }

            

            return true;
        }
        #endregion

        #endregion

        #endregion

        #region Штатная расстановка.
        /// <summary>
        /// Загружаем структуру по заданному коду подразделения и штатную расстановку.
        /// </summary>
        /// <param name="DepId">Код родительского подразделения</param>
        /// <returns></returns>
        public StaffListArrangementModel GetDepartmentStructureWithStaffArrangement(string DepId)
        {
            StaffListArrangementModel model = new StaffListArrangementModel();

            //если не определены права ничего не грузим
            if (string.IsNullOrEmpty(DepId)) return model;

            Department dep = DepartmentDao.GetByCode(DepId);
            int DepartmentId = dep.Id;
            int itemLevel = dep.ItemLevel.Value;

            int PersonnelId = AuthenticationService.CurrentUser.UserRole == UserRole.PersonnelManager ? AuthenticationService.CurrentUser.Id : 0;

            //достаем уровень подразделений и сотрудников с должностями к ним
            //если на входе код подразделения 7 уровня, то надо достать должности и сотрудников
            if (itemLevel != 7)
            {
                model.EstablishedPosts = StaffEstablishedPostDao.GetStaffEstablishedArrangements(DepartmentId, PersonnelId);
                //уровень подразделений
                model.Departments = GetDepartmentListByParent(DepId, false).OrderBy(x => x.Priority).ToList();
            }
            else
            {
                model.EstablishedPosts = StaffEstablishedPostDao.GetStaffEstablishedArrangements(DepartmentId, PersonnelId);
            }

            return model;
        }
        /// <summary>
        /// Вызов формы для выбора типа заявки в штатном перемещении.
        /// </summary>
        /// <param name="UserId">Id сотрудника.</param>
        /// <returns></returns>
        public SelectMovementTypeModel GetSelectMovementTypeModel(int UserId)
        {
            SelectMovementTypeModel model = new SelectMovementTypeModel();
            model.UserId = UserId;
            model.RequestTypes = refStaffMovementsTypesDao.LoadAll();
            return model;
        }
        #endregion


        #region Загрузка словарей и справочников.
        /// <summary>
        /// Загрузка справочников модели для заявок к подразделениям.
        /// </summary>
        /// <param name="model">Модель заявки.</param>
        public void LoadDictionaries(StaffDepartmentRequestModel model)
        {
            //реквизиты инициатора
            model.DepRequestInfo = GetDepRequestInfo(model.Id, model.DateRequest, model.Id == 0 ? AuthenticationService.CurrentUser.Id : model.UserId);
            model.RequestTypes = StaffDepartmentRequestTypesDao.LoadAll();
            model.DepLandmarks = StaffDepartmentLandmarksDao.GetDepartmentLandmarks(model.DMDetailId);
            model.DepTypes = StaffDepartmentTypesDao.GetDepartmentTypes();
            model.ProgramCodes = StaffProgramCodesDao.GetProgramCodes(model.DMDetailId);

            model.OperationGroups = StaffDepartmentOperationGroupsDao.GetOperationGroups();
            model.OperationGroups.Insert(0, new StaffDepartmentOperationGroupsDto { gId = 0, gName = "" });

            model.OperationModes = StaffDepartmentOperationModesDao.GetDepartmentOperationModes(model.DMDetailId);
            model.Reasons = StaffDepartmentReasonsDao.GetDepartmentReasons();
            model.NetShopTypes = StaffNetShopIdentificationDao.GetNetShopTypes();
            model.CashDeskAvailables = StaffDepartmentCashDeskAvailableDao.GetCashDeskAvailable();
            model.RentPlace = StaffDepartmentRentPlaceDao.GetRentPlace();
            model.SKB_GE = StaffDepartmentSKB_GEDao.GetSKB_GE();

            model.SoftGroups = StaffDepartmentSoftGroupDao.GetSoftGroups();
            model.SoftGroups.Insert(0, new StaffDepartmentSoftGroupDto { gId = 0, gName = "" });

            model.Accessoryes = StaffDepartmentAccessoryDao.GetAccessoryes();
            model.Accessoryes.Insert(0, new IdNameDto { Id = 0, Name = "" });

            //согласование - расстановка флажков и т.д.
            StaffDepartmentRequest entity = StaffDepartmentRequestDao.Get(model.Id);
            if (entity != null)
                SetApprovalFlags(model, entity);
            else
            {
                model.IsCurator = (AuthenticationService.CurrentUser.UserRole == UserRole.Inspector);
                model.IsPersonnelBank = (AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantPersonnel);
                model.IsConsultant = (AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing);
            }

        }
        /// <summary>
        /// Загрузка справочников модели для заявок к штатным единицам.
        /// </summary>
        /// <param name="model">Модель заявки.</param>
        public void LoadDictionaries(StaffEstablishedPostRequestModel model)
        {
            //реквизиты инициатора
            model.RequestTypes = StaffEstablishedPostRequestTypesDao.LoadAll();
            model.Reasons = AppointmentReasonDao.LoadAll();
            model.Reasons.Insert(0, new AppointmentReason { Code = "", Id = 0, Name = "" });

            model.Schedules = ScheduleDao.LoadAll().Where(x => x.Id == 37 || x.Id == 45 || x.Id == 48).ToList().ConvertAll(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            model.WorkConditions = StaffWorkingConditionsDao.LoadAllSorted().ToList().ConvertAll(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).OrderBy(x => Int32.Parse(x.Value));


            model.PostChargeActions = StaffExtraChargeActionsDao.LoadAll().ToList().ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
            model.PostChargeActions.Insert(0, new IdNameDto { Id = 0, Name = "" });

            GetDepRequestInfo(model);


            

            //пытаемся показать правильную расстановку для штатной единицы
            StaffEstablishedPostRequest entity = StaffEstablishedPostRequestDao.Get(model.Id);
            if (entity != null)
            {
                //model.Personnels = StaffEstablishedPostDao.GetStaffEstablishedArrangements(model.DepartmentId).Where(x => x.SEPId == model.SEPId).ToList();
                if (entity.StaffEstablishedPost != null)
                {
                    //поля UserId, Surname, IsPregnant определяем так же, как в функции показывающей штатную расстановку
                    model.Personnels = entity.StaffEstablishedPost.EstablishedPostUserLinks
                        .Where(x => x.IsUsed || (!x.IsUsed && x.ReserveType == 3 && x.DocId == entity.Id)).ToList()
                        .ConvertAll(x => new StaffUserLinkDto
                        {
                            Id = x.Id
                            ,SEPId = x.StaffEstablishedPost.Id
                            //,UserId = x.User != null && (!x.User.IsPregnant.HasValue || !x.User.IsPregnant.Value) && x.User.ChildVacation.Where(z => z.SendTo1C.HasValue && !z.DeleteDate.HasValue && z.BeginDate <= DateTime.Now && z.EndDate >= DateTime.Now).Count() == 0 ? x.User.Id : 0
                            ,UserId = x.User != null ? x.User.Id : 0
                            ,Surname = x.User != null && (!x.User.IsPregnant.HasValue || !x.User.IsPregnant.Value) && x.User.ChildVacation.Where(z => z.SendTo1C.HasValue && !z.DeleteDate.HasValue && z.BeginDate <= DateTime.Now && z.EndDate >= DateTime.Now).Count() == 0 ? x.User.Name : ""
                            ,IsPregnant = x.User != null ? ((x.User.IsPregnant.HasValue && x.User.IsPregnant.Value) || x.User.ChildVacation.Where(z => z.SendTo1C.HasValue && !z.DeleteDate.HasValue && z.BeginDate <= DateTime.Now && z.EndDate >= DateTime.Now).Count() != 0 ? true : false) : false
                            ,IsUsed = x.IsUsed
                            ,ReserveType = x.ReserveType.HasValue ? x.ReserveType.Value : 0
                            ,DocId = x.DocId.HasValue ? x.DocId.Value : 0
                            ,IsDismissal = x.IsDismissal
                            ,DateDistribNote = x.DateDistribNote
                            ,DateReceivNote = x.DateReceivNote
                            ,IsTemporary = x.IsTemporary
                            ,DateTempBegin = x.DateTempBegin
                            ,DateTempEnd = x.DateTempEnd
                        }).OrderBy(x => x.Surname).ToList();
                }

                //согласование - расстановка флажков и т.д.
                SetApprovalFlags(model, entity);
            }
            else
            {
                model.IsCurator = (AuthenticationService.CurrentUser.UserRole == UserRole.Inspector);
                model.IsPersonnelBank = (AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantPersonnel);
                model.IsConsultant = (AuthenticationService.CurrentUser.UserRole == UserRole.ConsultantOutsourcing);
            }

            if (model.Personnels == null)
            {
                model.Personnels = StaffEstablishedPostDao.GetStaffEstablishedArrangements(model.DepartmentId).Where(x => x.SEPId == model.SEPId).ToList()
                        .ConvertAll(x => new StaffUserLinkDto
                        {
                            Id = x.Id
                            ,SEPId = x.SEPId
                            ,UserId = x.UserId
                            ,Surname = x.Surname
                            ,IsPregnant = x.IsPregnant
                            ,IsUsed = true
                            ,ReserveType = x.ReserveType
                            ,DocId = x.DocId
                            ,IsDismissal = x.IsDismissal
                            ,DateDistribNote = x.DateDistribNote
                            ,DateReceivNote = x.DateReceivNote
                            ,IsTemporary = x.IsTemporary
                            ,DateTempBegin = x.DateTempBegin
                            ,DateTempEnd = x.DateTempEnd
                        }).OrderBy(x => x.Surname).ToList();
            }
        }
        /// <summary>
        /// Заполняем список видов заявок для подразделений.
        /// </summary>
        /// <returns></returns>
        public IList<IdNameDto> GetDepRequestStatuses()
        {
            IList<IdNameDto> dto = new List<IdNameDto>();
            dto.Add(new IdNameDto { Id = 0, Name = "Все" });
            dto.Add(new IdNameDto { Id = 1, Name = "Черновик" });
            dto.Add(new IdNameDto { Id = 2, Name = "На согласовании" });
            dto.Add(new IdNameDto { Id = 3, Name = "Утверждено" });

            return dto;
        }
        /// <summary>
        /// В строке показываем список руководителей для инициатора текущей заявки.
        /// </summary>
        /// <param name="curUser">Инициатор.</param>
        /// <param name="ManagerLevel">Урвоень руководителей, список которых нужно показать.</param>
        /// <returns></returns>
        protected string GetManagers(User curUser, int ManagerLevel)
        {
            string ManagersStr = string.Empty;
            if (curUser.Level > ManagerLevel)
            {
                IList<User> managers = DepartmentDao.GetDepartmentManagers(curUser.Department.Id, true)
                            .Where<User>(x => x.Level == ManagerLevel && x.RoleId == (int)UserRole.Manager)
                            .OrderBy(x => x.IsMainManager)
                            .ThenBy(x => x.Name)
                            .ToList<User>();
                //список руководителей
                foreach (var item in managers)
                {
                    ManagersStr += (string.IsNullOrEmpty(ManagersStr) ? "" : ", ") + item.Name + "(" + item.Position.Name + ")";
                }
            }
            return ManagersStr;
        }
        /// <summary>
        /// Достаем для автозаполнения список должностей.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public IList<IdNameDto> GetPositionAutocomplete(string Name)
        {
            return PositionDao.GetOperatingPositions(Name);
        }
        #endregion

        #region Кладр - Почтовые адреса
        /// <summary>
        /// Загружаем модель для составления Российских адресов.
        /// </summary>
        /// <param name="model">Модель формы</param>
        /// <returns></returns>
        public AddressModel GetAddress(AddressModel model)
        {
            ////AddressModel model = new AddressModel();
            //if (model.Id == 0)    //загрузка без иденификатора
            //{
            //    model.Regions = KladrDao.GetKladr(1, null, null, null, null);
            //    model.Areas = KladrDao.GetKladr(2, null, null, null, null);
            //    model.Cityes = KladrDao.GetKladr(3, null, null, null, null);
            //    model.Settlements = KladrDao.GetKladr(4, null, null, null, null);
            //    model.Streets = KladrDao.GetKladr(5, null, null, null, null);
            //    model.HouseTypes = GetAddressDictionary(1);
            //    model.BuildTypes = GetAddressDictionary(2);
            //    model.FlatTypes = GetAddressDictionary(3);
            //}
            //else
            //{
                
            //}
            //тут по Id записи достаем строку с адресом и строим форму
            model.Regions = KladrDao.GetKladr(1, null, null, null, null);
            //model.RegionCode = "770000000000000";

            model.Areas = KladrDao.GetKladr(2, !string.IsNullOrEmpty(model.RegionCode) ? model.Regions.Where(x => x.Code == model.RegionCode).Single().RegionCode : null, null, null, null);
            //model.AreaCode = string.Empty; ;
            //string a2 = !string.IsNullOrEmpty(model.AreaCode) ? model.Areas.Where(x => x.Code == model.AreaCode).Single().AreaCode : null;
            model.Cityes = KladrDao.GetKladr(3, !string.IsNullOrEmpty(model.RegionCode) ? model.Regions.Where(x => x.Code == model.RegionCode).Single().RegionCode : null,
                !string.IsNullOrEmpty(model.AreaCode) ? model.Areas.Where(x => x.Code == model.AreaCode).Single().AreaCode : null, null, null);
            //model.CityCode = "770000020000000";

            //string a1 = !string.IsNullOrEmpty(model.RegionCode) ? model.Regions.Where(x => x.Code == model.RegionCode).Single().RegionCode : "";
            //string a2 = !string.IsNullOrEmpty(model.AreaCode) ? model.Areas.Where(x => x.Code == model.AreaCode).Single().AreaCode : null;
            //string a3 = !string.IsNullOrEmpty(model.CityCode) ? model.Cityes.Where(x => x.Code == model.CityCode).Single().CityCode : null;

            model.Settlements = KladrDao.GetKladr(4, !string.IsNullOrEmpty(model.RegionCode) ? model.Regions.Where(x => x.Code == model.RegionCode).Single().RegionCode : null,
                !string.IsNullOrEmpty(model.AreaCode) ? model.Areas.Where(x => x.Code == model.AreaCode).Single().AreaCode : null,
                !string.IsNullOrEmpty(model.CityCode) ? model.Cityes.Where(x => x.Code == model.CityCode).Single().CityCode : null, null);
            //model.SettlementCode = string.Empty;

            model.Streets = KladrDao.GetKladr(5, !string.IsNullOrEmpty(model.RegionCode) ? model.Regions.Where(x => x.Code == model.RegionCode).Single().RegionCode : null,
                !string.IsNullOrEmpty(model.AreaCode) ? model.Areas.Where(x => x.Code == model.AreaCode).Single().AreaCode : null,
                !string.IsNullOrEmpty(model.CityCode) ? model.Cityes.Where(x => x.Code == model.CityCode).Single().CityCode : null,
                !string.IsNullOrEmpty(model.SettlementCode) ? model.Settlements.Where(x => x.Code == model.SettlementCode).Single().SettlementCode : null);
            //model.StreetCode = "770000020004549";

            model.HouseTypes = GetAddressDictionary(1);
            //model.HouseType = 1;
            //model.HouseNumber = string.Empty;
            model.BuildTypes = GetAddressDictionary(2);
            //model.BuildType = 1;
            //model.BuildNumber = "1133";
            model.FlatTypes = GetAddressDictionary(3);
            //model.FlatType = 1;
            //model.FlatNumber = "7";
            //model.PostIndex = "124460";
            model.Address = GetAddressStr(model);
            return model;
        }
        /// <summary>
        /// По заполненной модели строим строку адреса.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected string GetAddressStr(AddressModel model)
        {
            string AddressStr = "";

            if (!string.IsNullOrEmpty(model.PostIndex))
                AddressStr = model.PostIndex + ", ";

            if (!string.IsNullOrEmpty(model.RegionCode))
            {
                AddressStr += model.Regions.Where(x => x.Code == model.RegionCode).Single().Name;
            }

            if (!string.IsNullOrEmpty(model.AreaCode))
            {
                AddressStr += ", " + model.Areas.Where(x => x.Code == model.AreaCode).Single().Name;
            }

            if (!string.IsNullOrEmpty(model.CityCode))
            {
                AddressStr += ", " + model.Cityes.Where(x => x.Code == model.CityCode).Single().Name;
            }

            if (!string.IsNullOrEmpty(model.SettlementCode))
            {
                AddressStr += ", " + model.Settlements.Where(x => x.Code == model.SettlementCode).Single().Name;
            }

            if (!string.IsNullOrEmpty(model.StreetCode))
            {
                AddressStr += ", " + model.Streets.Where(x => x.Code == model.StreetCode).Single().Name;
            }


            if (!string.IsNullOrEmpty(model.HouseNumber))
            {
                AddressStr += (AddressStr == "" ? "" : ", ") + model.HouseTypes.Where(x => x.Id == model.HouseType).Single().Name;
                AddressStr += (AddressStr == "" ? "" : " №") + model.HouseNumber;
            }


            if (!string.IsNullOrEmpty(model.BuildNumber))
            {
                AddressStr += (AddressStr == "" ? "" : ", ") + model.BuildTypes.Where(x => x.Id == model.BuildType).Single().Name;
                AddressStr += (AddressStr == "" ? "" : " ") + model.BuildNumber;
            }


            if (!string.IsNullOrEmpty(model.FlatNumber))
            {
                AddressStr += (AddressStr == "" ? "" : ", ") + model.FlatTypes.Where(x => x.Id == model.FlatType).Single().Name;
                AddressStr += (AddressStr == "" ? "" : " ") + model.FlatNumber;
            }

            return AddressStr;
        }
        /// <summary>
        /// Загружаем список объектов частей адресов.
        /// </summary>
        /// <param name="Code">Код объекта.</param>
        /// <param name="AddressType">Тип объекта.</param>
        /// <param name="RegionCode">Код региона.</param>
        /// <param name="AreaCode">Код района.</param>
        /// <param name="CityCode">Код города.</param>
        /// <param name="SettlementCode">Код населенного пункта.</param>
        /// <returns></returns>
        public KladrWithPostIndex GetKladr(string Code, int AddressType, string RegionCode, string AreaCode, string CityCode, string SettlementCode)
        {
            //по коду объекта адреса достаем запись и уже по даннм из
            KladrWithPostIndex k = new KladrWithPostIndex();
            if (string.IsNullOrEmpty(Code))
                k.Kladr = KladrDao.GetKladr(AddressType, RegionCode, AreaCode, CityCode, SettlementCode);
            else
            {
                //по коду выбранного обекта достаем строку и берем из нее параметры для поиска подчиненных списков объектов
                KladrDto row = KladrDao.GetKladrByCode(Code).Single();
                if (AddressType < 6)
                    k.Kladr = KladrDao.GetKladr(AddressType, row.RegionCode, row.AreaCode, row.CityCode, row.SettlementCode);
                k.PostIndex = row.Index;    //индекс берем из записи выбранного объекта
            }
            return k;
        }
        /// <summary>
        /// Заполняем выпадающие списки.
        /// </summary>
        /// <param name="FillType">Переключатель: 1 - дом/владение, 2 - корпус/строение, 3 - квартира/офис.</param>
        /// <returns></returns>
        public IList<IdNameDto> GetAddressDictionary(int FillType)
        {
            IList<IdNameDto> ht = new List<IdNameDto>();

            switch (FillType)
            {
                case 1:
                    //ht.Add(new IdNameDto { Id = 0, Name = "" });
                    ht.Add(new IdNameDto { Id = 1, Name = "дом" });
                    ht.Add(new IdNameDto { Id = 2, Name = "владение" });
                    break;
                case 2:
                    //ht.Add(new IdNameDto { Id = 0, Name = "" });
                    ht.Add(new IdNameDto { Id = 1, Name = "корпус" });
                    ht.Add(new IdNameDto { Id = 2, Name = "строение" });
                    break;
                case 3:
                    //ht.Add(new IdNameDto { Id = 0, Name = "" });
                    ht.Add(new IdNameDto { Id = 1, Name = "кв." });
                    ht.Add(new IdNameDto { Id = 2, Name = "офис" });
                    break;
            }


            return ht;
        }
        #endregion
    }
}

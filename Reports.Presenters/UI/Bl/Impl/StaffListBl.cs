using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Presenters.UI.ViewModel.StaffList;
using Reports.Core;
using Reports.Core.Domain;
using Reports.Core.Dao;
using Reports.Core.Dto;
using System.Web.Mvc;

namespace Reports.Presenters.UI.Bl.Impl
{
    public class StaffListBl : RequestBl, IStaffListBl
    {
        #region Dependencies
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
            
            
            //достаем уровень подразделений и штатных единиц к ним
            //если на входе код подразделения 7 уровня, то надо достать должности и сотрудников

            //все закомментаренное работало когда не было штатных единиц
            if (itemLevel != 7)
            {
                model.EstablishedPosts = StaffEstablishedPostDao.GetStaffEstablishedPosts(DepartmentId);

                
                ////руководство
                //IList<User> Users = UserDao.GetUsersForDepartment(DepartmentId).Where(x => x.IsActive == true && (x.RoleId & 4) > 0).OrderBy(x => x.IsMainManager)
                //    .ThenByDescending(x => x.Position.Name).ThenByDescending(x => x.Name).ToList();
                //IList<UsersListItemDto> ul = new List<UsersListItemDto>();
                //foreach (var item in Users)
                //{
                //    ul.Add(new UsersListItemDto(item.Id, item.Name, item.Department.Path, item.Department.Name, item.Position.Name, item.Login));
                //}
                //model.UserPositions = ul;

                //уровень подразделений
                model.Departments = GetDepartmentListByParent(DepId).OrderBy(x => x.Priority).ToList();
            }
            else
            {
                model.EstablishedPosts = StaffEstablishedPostDao.GetStaffEstablishedPosts(DepartmentId);
                ////нужно показать простых сотрудников, а показывать руководителей-сотрудников не нужно
                //IList<User> Users = UserDao.GetUsersForDepartment(DepartmentId).Where(x => x.IsActive == true && (x.RoleId & 2) > 0).OrderByDescending(x => x.Position.Name).ThenByDescending(x => x.Name).ToList();
                //IList<UsersListItemDto> ul = new List<UsersListItemDto>();
                //foreach (var item in Users)
                //{
                //    if (UserDao.FindByLogin(item.Login + "R") == null)//непоказываем начальников, потому что они видны уровнем выше
                //        ul.Add(new UsersListItemDto(item.Id, item.Name, item.Department.Path, item.Department.Name, item.Position.Name, item.Login));
                //}
                //model.UserPositions = ul;
            }

            return model;
        }

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
                model.DepartmentId, 
                model.Id.HasValue ? model.Id.Value : 0, 
                model.Creator, 
                model.DateBegin, 
                model.DateEnd, 
                model.StatusId,
                model.SortBy, 
                model.SortDescending);

            model.Statuses = GetDepRequestStatuses();

            return model;
        }
        /// <summary>
        /// Заполняем модель заявки для подразделения.
        /// </summary>
        /// <param name="model">Модель заявки.</param>
        /// <returns></returns>
        public StaffDepartmentRequestModel GetDepartmentRequest(StaffDepartmentRequestModel model)
        {
            if (model.Id == 0)
            {
                //Общие реквизиты
                model.DateState = null;
                model.DepartmentId = 0;
                model.ItemLevel = DepartmentDao.Load(model.ParentId.Value).ItemLevel + 1;
                model.Name = string.Empty;
                model.IsBack = false;
                model.OrderNumber = string.Empty;
                model.OrderDate = null;
                model.LegalAddressId = 0;
                model.LegalAddress = string.Empty;
                model.IsTaxAdminAccount = false;
                model.IsEmployeAvailable = false;
                model.DepNextId = 0;
                model.IsPlan = false;

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
                model.ATMCashInCount = 0;
                model.ATMCount = 0;
                model.DepCachinId = 0;
                model.DepATMId = 0;
                model.CashInStartedDate = null;
                model.ATMStartedDate = null;

                //Управленческие реквизиты
                model.NameShort = string.Empty;
                model.ReferenceReason = string.Empty;
                model.FactAddressId = 0;
                model.DepStatus = string.Empty;
                model.DepTypeId = 0;

                model.OpenDate = null;
                model.CloseDate = null;
                model.Reason = string.Empty;
                model.OperationMode = string.Empty;
                model.BeginIdleDate = null;
                model.EndIdleDate = null;
                model.IsRentPlace = false;
                model.AgreementDetails = string.Empty;
                model.DivisionArea = 0;
                model.AmountPayment = 0;
                model.Phone = string.Empty;

                model.IsBlocked = false;
                model.IsNetShop = false;
                model.IsAvailableCash = false;

                model.IsLegalEntity = false;

                model.PlanEPCount = 0;
                model.PlanSalaryFund = 0; model.Note = string.Empty;

                LoadDictionaries(model);
            }
            else
            {
                StaffDepartmentRequest entity = StaffDepartmentRequestDao.Get(model.Id);
                //для заявок на редактирование надо сделать поиск текущей заявки по Id подразделения, для того чтобы ее заполнить только данными, а Id самой заявки обнулить, чтобы запись добавилась, а не редактировалась
                if (entity == null) //если нет заявки с таким идентификатором, грузим новую заявку на создание подразделения
                {
                    model.Id = 0;
                    return GetDepartmentRequest(model);
                }
                //Общие реквизиты
                model.UserId = entity.Creator != null ? entity.Creator.Id : 0;
                model.DateState = entity.DateState;
                model.DepartmentId = entity.Department != null ? entity.Department.Id : 0;
                model.ItemLevel = entity.ItemLevel;
                model.Name = entity.Name;
                model.IsBack = entity.IsBack;
                model.OrderNumber = entity.OrderNumber;
                model.OrderDate = entity.OrderDate;
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
                model.IsPlan = entity.IsPlan;

                //налоговые реквизиты
                if (entity.Department != null)
                {
                    StaffDepartmentTaxDetails dt = StaffDepartmentTaxDetailsDao.Get(entity.Department.Id);
                    model.KPP = dt != null ? dt.KPP : string.Empty;
                    model.OKTMO = dt != null ? dt.OKTMO : string.Empty;
                    model.OKATO = dt != null ? dt.OKATO : string.Empty;
                    model.RegionCode = dt != null ? dt.RegionCode : string.Empty;
                    model.TaxAdminCode = dt != null ? dt.TaxAdminCode : string.Empty;
                    model.TaxAdminName = dt != null ? dt.TaxAdminName : string.Empty;
                    model.PostAddress = dt != null ? dt.PostAddress : string.Empty;
                }

                //ЦБ реквизиты
                StaffDepartmentCBDetails cbd = entity.DepartmentCBDetails.Where(x => x.DepRequest.Id == entity.Id).Single();
                model.ATMCountTotal = cbd.ATMCountTotal.HasValue ? cbd.ATMCountTotal.Value : 0;
                model.ATMCashInCount = cbd.ATMCashInCount.HasValue ? cbd.ATMCashInCount.Value : 0;
                model.ATMCount = cbd.ATMCount.HasValue ? cbd.ATMCount.Value : 0;
                model.DepCachinId = cbd.DepCashin != null ? cbd.DepCashin.Id : 0;
                model.DepATMId = cbd.DepATM != null ? cbd.DepATM.Id : 0;
                model.DepCachinName = cbd.DepCashin != null ? cbd.DepCashin.Name : string.Empty;
                model.DepATMName = cbd.DepATM != null ? cbd.DepATM.Name : string.Empty;
                model.CashInStartedDate = cbd.CashInStartedDate;
                model.ATMStartedDate = cbd.ATMStartedDate;

                //Управленческие реквизиты
                StaffDepartmentManagerDetails dmd = entity.DepartmentManagerDetails.Where(x => x.DepRequest.Id == entity.Id).Single();
                model.DMDetailId = dmd.Id;
                model.NameShort = dmd.NameShort;
                model.ReferenceReason = dmd.ReferenceReason;
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
                model.OpenDate = dmd.OpenDate;
                model.CloseDate = dmd.CloseDate;
                model.Reason = dmd.Reason;
                model.OperationMode = dmd.OperationMode;
                model.BeginIdleDate = dmd.BeginIdleDate;
                model.EndIdleDate = dmd.EndIdleDate;
                model.IsRentPlace = dmd.IsRentPlace;
                model.AgreementDetails = dmd.AgreementDetails;
                model.DivisionArea = dmd.DivisionArea;
                model.AmountPayment = dmd.AmountPayment;
                model.Phone = dmd.Phone;
                model.IsBlocked = dmd.IsBlocked;
                model.IsNetShop = dmd.IsNetShop;
                model.IsAvailableCash = dmd.IsAvailableCash;
                model.IsLegalEntity = dmd.IsLegalEntity;
                model.PlanEPCount = dmd.PlanEPCount;
                model.PlanSalaryFund = dmd.PlanSalaryFund;
                model.Note = dmd.Note;

                LoadDictionaries(model);
            }
            
            
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
                    ItemLevel = model.ItemLevel.Value,
                    Name = model.Name,
                    IsBack = model.IsBack,
                    OrderNumber = model.OrderNumber,
                    OrderDate = model.OrderDate,
                    IsTaxAdminAccount = model.IsTaxAdminAccount,
                    IsEmployeAvailable = model.IsEmployeAvailable,
                    IsPlan = model.IsPlan,
                    IsUsed = false,
                    IsDraft = true,
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


                entity.ParentDepartment = model.ParentId.Value == 0 ? null : DepartmentDao.Load(model.ParentId.Value);
                entity.DepNext = model.DepNextId == 0 ? null : DepartmentDao.Load(model.DepNextId);                
                

                //поля ЦБ реквизитов
                entity.DepartmentCBDetails = new List<StaffDepartmentCBDetails>();

                entity.DepartmentCBDetails.Add(new StaffDepartmentCBDetails
                {
                    DepRequest = entity,
                    ATMCountTotal = model.ATMCountTotal,
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
                dmd.ReferenceReason = model.ReferenceReason;

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
                dmd.OpenDate = model.OpenDate;
                dmd.CloseDate = model.CloseDate;
                dmd.Reason = model.Reason;
                dmd.OperationMode = model.OperationMode;
                dmd.BeginIdleDate = model.BeginIdleDate;
                dmd.EndIdleDate = model.EndIdleDate;
                dmd.IsRentPlace = model.IsRentPlace;
                dmd.AgreementDetails = model.AgreementDetails;
                dmd.DivisionArea = model.DivisionArea;
                dmd.AmountPayment = model.AmountPayment;
                dmd.Phone = model.Phone;
                dmd.IsBlocked = model.IsBlocked;
                dmd.IsNetShop = model.IsNetShop;
                dmd.IsAvailableCash = model.IsAvailableCash;
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

                //операции
                dmd.DepOperations = new List<StaffDepartmentOperationLinks>();
                foreach (var item in model.Operations.Where(x => x.IsUsed == true))
                {
                    dmd.DepOperations.Add(new StaffDepartmentOperationLinks { 
                        DepartmentManagerDetail = dmd, 
                        DepartmentOperation = StaffDepartmentOperationsDao.Load(item.OperationId), 
                        Creator = curUser, 
                        CreateDate = DateTime.Now 
                    });
                }

                //коды программ
                dmd.ProgramCodes = new List<StaffProgramCodes>();
                foreach (var item in model.ProgramCodes.Where(x => x.Code != null))
                {
                    dmd.ProgramCodes.Add(new StaffProgramCodes { 
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
                    dmd.DepartmentLandmarks.Add(new StaffDepartmentLandmarks { 
                        DepartmentManagerDetail = dmd, 
                        LandmarkTypes = StaffLandmarkTypesDao.Load(item.LandmarkId), 
                        Description = item.Description, 
                        Creator = curUser, 
                        CreateDate = DateTime.Now 
                    });
                }

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
            entity.IsBack = model.IsBack;
            entity.OrderNumber = model.OrderNumber;
            entity.OrderDate = model.OrderDate;
            entity.IsTaxAdminAccount = model.IsTaxAdminAccount;
            entity.IsEmployeAvailable = model.IsEmployeAvailable;
            entity.IsPlan = model.IsPlan;
            entity.IsDraft = true;
            entity.Editor = curUser;
            entity.EditDate = DateTime.Now;
            entity.ParentDepartment = model.ParentId.Value == 0 ? null : DepartmentDao.Load(model.ParentId.Value);
            entity.DepNext = model.DepNextId == 0 ? null : DepartmentDao.Load(model.DepNextId);

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
            if (entity.DepartmentCBDetails.Count == 0)//если в виду какого нить сбоя при первичном сохранении не добавлиась запись, то создаем ее
            {
                entity.DepartmentCBDetails = new List<StaffDepartmentCBDetails>();

                entity.DepartmentCBDetails.Add(new StaffDepartmentCBDetails
                {
                    DepRequest = entity,
                    ATMCountTotal = model.ATMCountTotal,
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
            if (entity.DepartmentManagerDetails == null) //если в виду какого нить сбоя при первичном сохранении не добавлиась управленческие реквизиты, то создаем их
            {
                entity.DepartmentManagerDetails = new List<StaffDepartmentManagerDetails>();
                StaffDepartmentManagerDetails dmd = new StaffDepartmentManagerDetails();
                dmd.DepRequest = entity;
                dmd.NameShort = model.NameShort;
                dmd.ReferenceReason = model.ReferenceReason;

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
                dmd.OpenDate = model.OpenDate;
                dmd.CloseDate = model.CloseDate;
                dmd.Reason = model.Reason;
                dmd.OperationMode = model.OperationMode;
                dmd.BeginIdleDate = model.BeginIdleDate;
                dmd.EndIdleDate = model.EndIdleDate;
                dmd.IsRentPlace = model.IsRentPlace;
                dmd.AgreementDetails = model.AgreementDetails;
                dmd.DivisionArea = model.DivisionArea;
                dmd.AmountPayment = model.AmountPayment;
                dmd.Phone = model.Phone;
                dmd.IsBlocked = model.IsBlocked;
                dmd.IsNetShop = model.IsNetShop;
                dmd.IsAvailableCash = model.IsAvailableCash;
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
                dmd.DepOperations = new List<StaffDepartmentOperationLinks>();
                foreach (var item in model.Operations.Where(x => x.IsUsed == true))
                {
                    dmd.DepOperations.Add(new StaffDepartmentOperationLinks
                    {
                        DepartmentManagerDetail = dmd,
                        DepartmentOperation = StaffDepartmentOperationsDao.Load(item.OperationId),
                        Creator = curUser,
                        CreateDate = DateTime.Now
                    });
                }

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

                entity.DepartmentManagerDetails.Add(dmd);
            }
            else
            {
                entity.DepartmentManagerDetails[0].NameShort = model.NameShort;
                entity.DepartmentManagerDetails[0].ReferenceReason = model.ReferenceReason;

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
                entity.DepartmentManagerDetails[0].DepartmentType = model.DepTypeId.Value == 0 ? null : StaffDepartmentTypesDao.Load(model.DepTypeId.Value);
                entity.DepartmentManagerDetails[0].OpenDate = model.OpenDate;
                entity.DepartmentManagerDetails[0].CloseDate = model.CloseDate;
                entity.DepartmentManagerDetails[0].Reason = model.Reason;
                entity.DepartmentManagerDetails[0].OperationMode = model.OperationMode;
                entity.DepartmentManagerDetails[0].BeginIdleDate = model.BeginIdleDate;
                entity.DepartmentManagerDetails[0].EndIdleDate = model.EndIdleDate;
                entity.DepartmentManagerDetails[0].IsRentPlace = model.IsRentPlace;
                entity.DepartmentManagerDetails[0].AgreementDetails = model.AgreementDetails;
                entity.DepartmentManagerDetails[0].DivisionArea = model.DivisionArea;
                entity.DepartmentManagerDetails[0].AmountPayment = model.AmountPayment;
                entity.DepartmentManagerDetails[0].Phone = model.Phone;
                entity.DepartmentManagerDetails[0].IsBlocked = model.IsBlocked;
                entity.DepartmentManagerDetails[0].IsNetShop = model.IsNetShop;
                entity.DepartmentManagerDetails[0].IsAvailableCash = model.IsAvailableCash;
                entity.DepartmentManagerDetails[0].IsLegalEntity = model.IsLegalEntity;
                entity.DepartmentManagerDetails[0].PlanEPCount = model.PlanEPCount;
                entity.DepartmentManagerDetails[0].PlanSalaryFund = model.PlanSalaryFund;
                entity.DepartmentManagerDetails[0].Note = model.Note;
                entity.DepartmentManagerDetails[0].Editor = curUser;
                entity.DepartmentManagerDetails[0].EditDate = DateTime.Now;


                //режим работы
                if (entity.DepartmentManagerDetails[0].DepOperationModes == null)
                    entity.DepartmentManagerDetails[0].DepOperationModes = new List<StaffDepartmentOperationModes>();

                foreach (var item in model.OperationModes)
                {
                    StaffDepartmentOperationModes dom = new StaffDepartmentOperationModes();
                    //если не было, добавляем
                    if (item.Id == 0)
                    {
                        dom.DepartmentManagerDetail = entity.DepartmentManagerDetails[0];
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

                //операции
                if (entity.DepartmentManagerDetails[0].DepOperations == null)
                    entity.DepartmentManagerDetails[0].DepOperations = new List<StaffDepartmentOperationLinks>();

                foreach (var item in model.Operations)// в отличие от первичного добавления делаем цикл по всем операциям
                {
                    StaffDepartmentOperationLinks oper = new StaffDepartmentOperationLinks();

                    //если была операция, но сняли птицу, то удаляем
                    if (item.Id != 0 && !item.IsUsed)
                    {
                        oper = entity.DepartmentManagerDetails[0].DepOperations.Where(x => x.Id == item.Id/* && item.IsUsed*/).Single();
                        entity.DepartmentManagerDetails[0].DepOperations.Remove(oper);
                    }
                    
                    //если не было и поставили птицу
                    if (item.Id == 0 && item.IsUsed)
                    {
                        oper.DepartmentManagerDetail = entity.DepartmentManagerDetails[0];
                        oper.DepartmentOperation = StaffDepartmentOperationsDao.Load(item.OperationId);
                        oper.Creator = curUser;
                        oper.CreateDate = DateTime.Now;
                        entity.DepartmentManagerDetails[0].DepOperations.Add(oper);
                    }
                }

                //коды программ
                if (entity.DepartmentManagerDetails[0].ProgramCodes == null)
                    entity.DepartmentManagerDetails[0].ProgramCodes = new List<StaffProgramCodes>();

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


                //ориентиры
                if (entity.DepartmentManagerDetails[0].DepartmentLandmarks == null)
                    entity.DepartmentManagerDetails[0].DepartmentLandmarks = new List<StaffDepartmentLandmarks>();

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



            if (model.Id != 0)
            {
                

                try
                {
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
        #endregion

        #endregion

        #region Загрузка словарей и справочников.
        /// <summary>
        /// Загрузка справочников модели для заявок к подразделениям.
        /// </summary>
        /// <param name="model">Модель заявки.</param>
        public void LoadDictionaries(StaffDepartmentRequestModel model)
        {
            //реквизиты инициатора
            model.DepRequestInfo = GetDepRequestInfo(model.Id, model.DateRequest, model.UserId);
            model.RequestTypes = StaffDepartmentRequestTypesDao.LoadAll();
            model.DepLandmarks = StaffDepartmentLandmarksDao.GetDepartmentLandmarks(model.DMDetailId);
            model.DepTypes = StaffDepartmentTypesDao.GetDepartmentTypes();
            model.ProgramCodes = StaffProgramCodesDao.GetProgramCodes(model.DMDetailId);
            model.Operations = StaffDepartmentOperationLinksDao.GetDepartmentOperationLinks(model.DMDetailId);
            model.OperationModes = StaffDepartmentOperationModesDao.GetDepartmentOperationModes(model.DMDetailId);
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

        #region Для тестов
        /// <summary>
        /// собираем полное дерево
        /// </summary>
        /// <returns></returns>
        public TreeViewModel GetDepartmentList()
        {
            User currentUser = UserDao.Load(AuthenticationService.CurrentUser.Id);
            TreeViewModel model = new TreeViewModel();
            //model.Departments = DepartmentDao.LoadAll().Where(x => x.Path.StartsWith(currentUser.Department.Path)).ToList();
            //model.Departments = DepartmentDao.LoadAll().Where(x => x.Id == currentUser.Department.Id).ToList();
            //model.ParentId = currentUser.Department.ParentId.ToString();
            //model.DepId = currentUser.Department.Code;
            model.Departments = DepartmentDao.LoadAll();
            return model;
        }
        /// <summary>
        /// Загружаем структуру по заданному коду подразделения
        /// </summary>
        /// <param name="DepId">Код родительского подразделения</param>
        /// <returns></returns>
        public TreeGridAjaxModel GetDepartmentStructure(string DepId)
        {
            TreeGridAjaxModel model = new TreeGridAjaxModel();
            Department dep =DepartmentDao.GetByCode(DepId);
            int DepartmentId = dep.Id;
            int itemLevel = dep.ItemLevel.Value;
            //этот вариант для выбранного подразделения достает с начала руководителей и замов, а уже потом подгружает уровень подчиненных подразделений
            //сотрудники с ролью руководителей есть во всех уровнях, кроме 7
            //сортировка сотрудников построена задом наперед, так как при построении дерева новые строки ставятся сразу после родительской
            //если на входе код подразделения 7 уровня, то надо достать должности и сотрудников
            if (itemLevel != 7)
            {
                //руководство
                IList<User> Users = UserDao.GetUsersForDepartment(DepartmentId).Where(x => x.IsActive == true && (x.RoleId & 4) > 0).OrderBy(x => x.IsMainManager)
                    .ThenByDescending(x => x.Position.Name).ThenByDescending(x => x.Name).ToList();
                IList<UsersListItemDto> ul = new List<UsersListItemDto>();
                foreach (var item in Users)
                {
                    ul.Add(new UsersListItemDto(item.Id, item.Name, item.Department.Path, item.Department.Name, item.Position.Name, item.Login));
                }
                model.UserPositions = ul;
                //уровень подразделений
                model.Departments = GetDepartmentListByParent(DepId).OrderBy(x => x.Priority).ToList();
            }
            else
            {
                //нужно показать простых сотрудников, а показывать руководителей-сотрудников не нужно
                IList<User> Users = UserDao.GetUsersForDepartment(DepartmentId).Where(x => x.IsActive == true && (x.RoleId & 2) > 0).OrderByDescending(x => x.Position.Name).ThenByDescending(x => x.Name).ToList();
                IList<UsersListItemDto> ul = new List<UsersListItemDto>();
                foreach (var item in Users)
                {
                    if (UserDao.FindByLogin(item.Login + "R") == null)
                        ul.Add(new UsersListItemDto(item.Id, item.Name, item.Department.Path, item.Department.Name, item.Position.Name, item.Login));
                }
                model.UserPositions = ul;
            }


            //кусок строит дерево структуры подразделений и подгружает сотрудников только в подразделения 7 уровня
            ////если на входе код подразделения 7 уровня, то надо достать должности и сотрудников
            //if (DepartmentDao.LoadAll().Where(x => x.Code1C == Convert.ToInt32(DepId)).Single().ItemLevel != 7)
            //    model.Departments = GetDepartmentListByParent(DepId);
            //else
            //{
            //    //таким способом сотрудники загружаются долго, если сделать функцию или представление, то скорость загрузки увеличится в разы
            //    IList<User> Users = UserDao.LoadAll().Where(x => x.Department != null && x.Department.Code1C == Convert.ToInt32(DepId) && x.IsActive == true && (x.RoleId & 2) > 0).ToList();
            //    IList<UsersListItemDto> ul = new List<UsersListItemDto>();
            //    foreach (var item in Users)
            //    {
            //        ul.Add(new UsersListItemDto(item.Id, item.Name, item.Department.Path, item.Department.Name, item.Position.Name, item.Login));
            //    }
            //    model.UserPositions = ul;
            //}
            return model;
        }
        /// <summary>
        /// подгружаем только подчиненые ветки на один уровень ниже
        /// </summary>
        /// <param name="DepId"></param>
        /// <returns></returns>
        public IList<StaffListDepartmentDto> GetDepartmentListByParent(string DepId)
        {
            //определяем подразделение по правам текущего пользователя для начальной загрузки страницы
            if (string.IsNullOrEmpty(DepId))
            {
                if (AuthenticationService.CurrentUser.UserRole == UserRole.OutsourcingManager || UserDao.Load(AuthenticationService.CurrentUser.Id).Level <= 2)
                {
                    DepId = "9900424";
                    //return DepartmentDao.LoadAll().Where(x => x.Code1C.ToString() == DepId).ToList();
                }
                else
                {
                    User cur = UserDao.Load(AuthenticationService.CurrentUser.Id);
                    DepId = (cur == null || cur.Department == null ? null : UserDao.Load(AuthenticationService.CurrentUser.Id).Department.Code1C.ToString());
                }

                return GetDepListWithSEPCount(DepartmentDao.LoadAll().Where(x => x.Code1C.ToString() == DepId).ToList());
            }

            return GetDepListWithSEPCount(DepartmentDao.LoadAll().Where(x => x.ParentId.ToString() == DepId).ToList());
        }
        /// <summary>
        /// Загружаем структуру по заданному коду подразделения с привязками к точкам Финграда
        /// </summary>
        /// <param name="DepId">Код родительского подразделения</param>
        /// <returns></returns>
        public DepStructureFingradPointsModel GetDepartmentStructureWithFingradPoins(string DepId)
        {
            DepStructureFingradPointsModel model = new DepStructureFingradPointsModel();
            Department dep = DepartmentDao.GetByCode(DepId);
            int DepartmentId = dep.Id;
            int itemLevel = dep.ItemLevel.Value;
            //этот вариант для выбранного подразделения достает с начала руководителей и замов, а уже потом подгружает уровень подчиненных подразделений
            //сотрудники с ролью руководителей есть во всех уровнях, кроме 7
            //сортировка сотрудников построена задом наперед, так как при построении дерева новые строки ставятся сразу после родительской
            //если на входе код подразделения 7 уровня, то надо достать должности и сотрудников
            if (itemLevel != 7)
            {
                //руководство
                IList<User> Users = UserDao.GetUsersForDepartment(DepartmentId).Where(x => x.IsActive == true && (x.RoleId & 4) > 0).OrderBy(x => x.IsMainManager)
                    .ThenByDescending(x => x.Position.Name).ThenByDescending(x => x.Name).ToList();
                IList<UsersListItemDto> ul = new List<UsersListItemDto>();
                foreach (var item in Users)
                {
                    ul.Add(new UsersListItemDto(item.Id, item.Name, item.Department.Path, item.Department.Name, item.Position.Name, item.Login));
                }
                model.UserPositions = ul;
                //уровень подразделений
                model.Departments = DepartmentDao.GetDepartmentWithFingradPoint(DepId).OrderBy(x => x.Priority).ToList();
            }
            else
            {
                //нужно показать простых сотрудников, а показывать руководителей-сотрудников не нужно
                IList<User> Users = UserDao.GetUsersForDepartment(DepartmentId).Where(x => x.IsActive == true && (x.RoleId & 2) > 0).OrderByDescending(x => x.Position.Name).ThenByDescending(x => x.Name).ToList();
                IList<UsersListItemDto> ul = new List<UsersListItemDto>();
                foreach (var item in Users)
                {
                    if (UserDao.FindByLogin(item.Login + "R") == null)
                        ul.Add(new UsersListItemDto(item.Id, item.Name, item.Department.Path, item.Department.Name, item.Position.Name, item.Login));
                }
                model.UserPositions = ul;
            }


            //кусок строит дерево структуры подразделений и подгружает сотрудников только в подразделения 7 уровня
            ////если на входе код подразделения 7 уровня, то надо достать должности и сотрудников
            //if (DepartmentDao.LoadAll().Where(x => x.Code1C == Convert.ToInt32(DepId)).Single().ItemLevel != 7)
            //    model.Departments = GetDepartmentListByParent(DepId);
            //else
            //{
            //    //таким способом сотрудники загружаются долго, если сделать функцию или представление, то скорость загрузки увеличится в разы
            //    IList<User> Users = UserDao.LoadAll().Where(x => x.Department != null && x.Department.Code1C == Convert.ToInt32(DepId) && x.IsActive == true && (x.RoleId & 2) > 0).ToList();
            //    IList<UsersListItemDto> ul = new List<UsersListItemDto>();
            //    foreach (var item in Users)
            //    {
            //        ul.Add(new UsersListItemDto(item.Id, item.Name, item.Department.Path, item.Department.Name, item.Position.Name, item.Login));
            //    }
            //    model.UserPositions = ul;
            //}
            return model;
        }
        /// <summary>
        /// Дополнительно к подразделениям делаем подсчет количества штатных единиц.
        /// </summary>
        /// <param name="deps"></param>
        /// <returns></returns>
        protected IList<StaffListDepartmentDto> GetDepListWithSEPCount(IList<Department> deps)
        {
            IList<StaffListDepartmentDto> Sdeps = new List<StaffListDepartmentDto>();
            foreach (var item in deps)
            {
                Sdeps.Add(new StaffListDepartmentDto
                {
                    Id = item.Id,
                    Code = item.Code,
                    Name = item.Name,
                    Code1C = item.Code1C,
                    ParentId = item.ParentId,
                    Path = item.Path,
                    ItemLevel = item.ItemLevel,
                    CodeSKD = item.CodeSKD,
                    Priority = item.Priority,
                    SEPCount = DepartmentDao.DepPositionCount(item.Id),
                    DepFingradName = string.IsNullOrEmpty(DepartmentDao.DepFingradName(item.Id)) ? "" : DepartmentDao.DepFingradName(item.Id)
                });
            }

            return Sdeps;
        }
        #endregion
    }
}

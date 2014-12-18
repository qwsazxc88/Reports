using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using Reports.Core;
using Reports.Core.Dao;
using Reports.Core.Dao.Impl;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Core.Services;
using Reports.Presenters.Services;
using Reports.Presenters.UI.ViewModel;
using System.Text;

namespace Reports.Presenters.UI.Bl.Impl
{
    public class GpdBl : BaseBl, IGpdBl
    {
        #region Справочник реквизитов
        public IGpdRefDetailDao gpdrefDetailDao;
        public IGpdRefDetailDao GpdRefDetailDao
        {
            get { return Validate.Dependency(gpdrefDetailDao); }
            set { gpdrefDetailDao = value; }
        }
        /// <summary>
        /// Заполняем модель справочника реквизитов для просмотра.
        /// </summary>
        /// <param name="model">Текущая модель.</param>
        /// <param name="hasError">Признак наличия ошибки.</param>
        public void SetGpdRefDetailView(GpdRefDetailModel model, bool hasError)
        {
            SetGpdRefDetailTypes(model);
        }
        /// <summary>
        /// Создаем список типов реквизитов для модели просмотра.
        /// </summary>
        /// <param name="model">Заполняемая модель</param>
        public void SetGpdRefDetailTypes(GpdRefDetailModel model)
        {
            UserRole role = CurrentUser.UserRole;
            model.DetailTypes = GpdRefDetailDao.GetDetailTypes(role,
                model.DTID,
                model.TypeName);
        }
        /// <summary>
        /// Создаем список типов реквизитов для модели редактирования.
        /// </summary>
        /// <param name="model">Заполняемая модель</param>
        public void SetGpdRefDetailTypes(GpdRefDetailEditModel model)
        {
            UserRole role = CurrentUser.UserRole;
            model.DetailTypes = GpdRefDetailDao.GetDetailTypes(role,
                model.DTID,
                model.TypeName);
        }
        /// <summary>
        /// Заполняем страницу с результатами поиска.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="hasError"></param>
        public void SetGpdRefDetailFind(GpdRefDetailModel model, bool hasError)
        {
            SetGpdRefDetailTypes(model);
            UserRole role = CurrentUser.UserRole;
            model.Documents = GpdRefDetailDao.GetRefDetail(role, model.Id, model.Name, model.DTID, model.INN, model.KPP, model.Account, model.BankName, model.BankBIK, model.CorrAccount, model.CreatorID, model.Code);
        }
        /// <summary>
        /// Проверяем правильность заполнения полей.
        /// </summary>
        /// <param name="model">Проверяемая модель.</param>
        /// <param name="ms">Словарь для сообщений об ошибках.</param>
        public void CheckFillFieldsForGpdRefDetail(GpdRefDetailEditModel model, System.Web.Mvc.ModelStateDictionary ms)
        {
            if (model.Name == null)
                ms.AddModelError("Name", "Заполните поле 'Наименование'");
            if (model.Name != null && model.Name.Trim().Length > 50)
                ms.AddModelError("Name", "Превышено допустимое количество символов!");

            if (model.INN == null)
                ms.AddModelError("INN", "Заполните поле 'ИНН'");
            if (model.INN != null && model.INN.Trim().Length > 12)
                ms.AddModelError("INN", "Превышено допустимое количество символов!");

            if (model.KPP == null)
                ms.AddModelError("KPP", "Заполните поле 'КПП'");
            if (model.KPP != null && model.KPP.Trim().Length > 9)
                ms.AddModelError("KPP", "Превышено допустимое количество символов!");

            if (model.Account == null)
                ms.AddModelError("Account", "Заполните поле 'Расчетный счет'");
            if (model.Account != null && model.Account.Trim().Length > 20)
                ms.AddModelError("Account", "Превышено допустимое количество символов!");

            if (model.BankName == null)
                ms.AddModelError("BankName", "Заполните поле 'Банк'");
            if (model.BankName != null && model.BankName.Trim().Length > 50)
                ms.AddModelError("BankName", "Превышено допустимое количество символов!");

            if (model.BankBIK == null)
                ms.AddModelError("BankBIK", "Заполните поле 'Банк БИК'");
            if (model.BankBIK != null && model.BankBIK.Trim().Length > 9)
                ms.AddModelError("BankBIK", "Превышено допустимое количество символов!");

            if (model.CorrAccount == null)
                ms.AddModelError("CorrAccount", "Заполните поле 'Банк кор/счет'");
            if (model.CorrAccount != null && model.CorrAccount.Trim().Length > 20)
                ms.AddModelError("Name", "Превышено допустимое количество символов!");

            if (model.DTID == 2)
            {
                if (model.Code == null)
                    ms.AddModelError("Code", "Заполните поле 'Код банка'");
                if (model.Code != null && model.Code.Trim().Length > 9)
                    ms.AddModelError("Name", "Превышено допустимое количество символов!");
            }
            else
                model.Code = null;

            UserRole role = CurrentUser.UserRole;
            model.DetailTypes = GpdRefDetailDao.GetDetailTypes(role,
                model.DTID,
                model.TypeName);
        }
        /// <summary>
        /// Процедура сохранения записи в базе данных.
        /// </summary>
        /// <param name="model">Текущая модель.</param>
        /// <param name="error">Переменная для возврата текста сообщения об ошибке.</param>
        public bool SaveGpdRefDetail(GpdRefDetailEditModel model, out string error)
        {
            error = string.Empty;
            //UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            IUser currentUseId = AuthenticationService.CurrentUser;

            try
            {
                GpdRefDetail gpdrefDetail = GpdRefDetailDao.Get(model.Id);

                if (gpdrefDetail == null)
                {
                    gpdrefDetail = new GpdRefDetail
                    {
                        //Id = model.Id,
                        Name = model.Name,
                        DTID = model.DTID,
                        INN = model.INN,
                        KPP = model.KPP,
                        Account = model.Account,
                        BankName = model.BankName,
                        BankBIK = model.BankBIK,
                        CorrAccount = model.CorrAccount,
                        CreatorID = currentUseId.Id,
                        Code = model.Code
                    };
                }
                else
                {
                    //gpdrefDetail.Id = model.Id;
                    gpdrefDetail.Name = model.Name;
                    gpdrefDetail.DTID = model.DTID;
                    gpdrefDetail.INN = model.INN;
                    gpdrefDetail.KPP = model.KPP;
                    gpdrefDetail.Account = model.Account;
                    gpdrefDetail.BankName = model.BankName;
                    gpdrefDetail.BankBIK = model.BankBIK;
                    gpdrefDetail.CorrAccount = model.CorrAccount;
                    gpdrefDetail.CreatorID = model.CreatorID;
                    gpdrefDetail.Code = model.Code;
                }
                GpdRefDetailDao.SaveAndFlush(gpdrefDetail);
                model.Id = gpdrefDetail.Id;
                return true;
            }
            catch (Exception ex)
            {
                GpdRefDetailDao.RollbackTran();
                Log.Error("Error on SaveMissionOrderEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                //SetUserInfoModel(user, model);
                //SetStaticFields(model, missionHotels);
                //LoadDictionaries(model);
                //SetHiddenFields(model);
            }
        }
        /// <summary>
        /// Заполняем модель для редактирования по указанному ID.
        /// </summary>
        /// <param name="id">Значение ID.</param>
        /// <param name="hasError">Признак ошибки.</param>
        /// <returns></returns>
        public GpdRefDetailEditModel SetRefDetailEditModel(int Id, bool hasError)
        {
            GpdRefDetailEditModel model = new GpdRefDetailEditModel();
            SetGpdRefDetailTypes(model);
            model.Id = Id;
            UserRole role = CurrentUser.UserRole;
            model.Documents = GpdRefDetailDao.GetRefDetail(role, model.Id, model.Name, model.DTID, model.INN, model.KPP, model.Account, model.BankName, model.BankBIK, model.CorrAccount, model.CreatorID, model.Code);
            if (model.Documents.Count > 0)
            {
                foreach (var doc in model.Documents)
                {
                    model.Id = doc.Id;
                    model.Name = doc.Name;
                    model.DTID = doc.DTID;
                    model.INN = doc.INN;
                    model.KPP = doc.KPP;
                    model.Account = doc.Account;
                    model.BankName = doc.BankName;
                    model.BankBIK = doc.BankBIK;
                    model.CorrAccount = doc.CorrAccount;
                    model.CreatorID = doc.CreatorID;
                    model.Code = doc.Code;
                }
            }


            return model;
        }
        #endregion

        #region Договора
        public IGpdContractDao gpdContractDao;
        public IGpdContractDao GpdContractDao
        {
            get { return Validate.Dependency(gpdContractDao); }
            set { gpdContractDao = value; }
        }
        /// <summary>
        /// Просмотр договоров.
        /// </summary>
        /// <param name="model">Обрабатываемая модель.</param>
        /// <param name="hasError">Флажок для ошибок.</param>
        public void SetGpdContractView(GpdContractModel model, bool hasError)
        {
            if (!model.IsFind)
            {
                DateTime today = DateTime.Today;
                model.DateBegin = new DateTime(today.Year, today.Month, 1);
                model.DateEnd = today;
            }

            SetGpdContractStatuses(model, hasError);
            SetGpdContractChargingTypes(model, hasError);
            if (model.IsFind)
                SetGpdContract(model);
        }
        /// <summary>
        /// Заполнение модели для создания/редактирования договора.
        /// </summary>
        /// <param name="Id">ID договора.</param>
        /// <param name="hasError">Флажок для ошибок.</param>
        /// <returns></returns>
        public GpdContractEditModel SetGpdContractEdit(int Id, bool hasError)
        {
            GpdContractEditModel model = new GpdContractEditModel();
            model.Id = Id;
            SetGpdContractPersons(model, hasError);
            SetGpdContractChargingTypes(model, hasError);
            SetGpdContractDetails(model, hasError);
            SetGpdContractStatuses(model, hasError);

            UserRole role = CurrentUser.UserRole;
            model.Contracts = GpdContractDao.GetContracts(role, 
                model.Id, 
                model.DepartmentId, 
                model.CTID, 
                null, null,
                model.Surname, 
                model.IsFind,
                0, null);

            if (model.Contracts.Count > 0)
            {
                foreach (var doc in model.Contracts)
                {
                    //model.Id = doc.Id;
                    model.CreatorID = doc.CreatorID;
                    model.DepartmentId = doc.DepartmentId;
                    model.DepartmentName = doc.DepartmentName;
                    model.PersonID = doc.PersonID;
                    model.CTID = doc.CTID;
                    model.StatusID = doc.StatusID;
                    model.NumContract = doc.NumContract;
                    model.NameContract = doc.NameContract;
                    model.DateBegin = doc.DateBegin;
                    model.DateEnd = doc.DateEnd;
                    model.DateP = doc.DateP;
                    model.DatePOld = doc.DatePOld;
                    model.PayeeID = doc.PayeeID;
                    model.PayerID = doc.PayerID;
                    model.GPDID = doc.GPDID;
                    model.PurposePayment = doc.PurposePayment;
                    if (doc.CreateDate == null)
                        model.Autor = doc.Autor;
                    else
                        model.Autor = doc.CreatorName + " Дата создания договора " + doc.CreateDate.ToShortDateString();
                    model.CreatorName = doc.CreatorName;
                    model.CreateDate = doc.CreateDate;
                    model.Surname = doc.Surname;
                    model.CTName = doc.CTName;
                    model.StatusName = doc.StatusName;
                }
            }
            else
                model.StatusID = 4;

            return model;
        }
        /// <summary>
        /// Создаем список статусов договоров для модели просмотра.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="hasError"></param>
        public void SetGpdContractStatuses(GpdContractModel model, bool hasError)
        {
            UserRole role = CurrentUser.UserRole;
            model.Statuses = GpdContractDao.GetStatuses(role, model.StatusID, model.StatusName);
        }
        /// <summary>
        /// Создаем список статусов договоров для модели редактирования.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="hasError"></param>
        public void SetGpdContractStatuses(GpdContractEditModel model, bool hasError)
        {
            UserRole role = CurrentUser.UserRole;
            model.Statuses = GpdContractDao.GetStatuses(role, model.StatusID, model.StatusName);
        }
        /// <summary>
        /// Создаем список видов начисления для модели просмотра.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="hasError"></param>
        public void SetGpdContractChargingTypes(GpdContractModel model, bool hasError)
        {
            UserRole role = CurrentUser.UserRole;
            model.ChargingTypes = GpdContractDao.GetChargingTypes(role, model.CTID, model.CTName);
        }
        /// <summary>
        /// Создаем список видов начисления для модели редактирования.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="hasError"></param>
        public void SetGpdContractChargingTypes(GpdContractEditModel model, bool hasError)
        {
            UserRole role = CurrentUser.UserRole;
            model.ChargingTypes = GpdContractDao.GetChargingTypes(role, model.CTID, model.CTName);
        }
        /// <summary>
        /// Создаем список физических лиц для модели редактирования.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="hasError"></param>
        public void SetGpdContractPersons(GpdContractEditModel model, bool hasError)
        {
            UserRole role = CurrentUser.UserRole;
            model.Persons = GpdContractDao.GetPersons(role, model.PersonID, model.Surname);
        }
        /// <summary>
        /// Заполняем списки получателей и плательщиков.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="hasError"></param>
        public void SetGpdContractDetails(GpdContractEditModel model, bool hasError)
        {
            UserRole role = CurrentUser.UserRole;
            //плательщики
            model.Payers = GpdContractDao.GetDetails(role, 2, model.PayerID, model.PayerName);
            //получатели
            model.Payeers = GpdContractDao.GetDetails(role, 1, model.PayeeID, model.PayeeName);
        }
        /// <summary>
        /// Достаем список договоров.
        /// </summary>
        /// <param name="model">Обрабатываемая модель.</param>
        public void SetGpdContract(GpdContractModel model)
        {
            UserRole role = CurrentUser.UserRole;
            model.Contracts = GpdContractDao.GetContracts(role,
                model.Id,
                model.DepartmentId,
                model.CTID,
                model.DateBegin,
                model.DateEnd,
                model.Surname,
                model.IsFind,
                model.SortBy,
                model.SortDescending);
        }
        /// <summary>
        /// Проверки при сохранении договора.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ms"></param>
        public void CheckFillFieldsForGpdContract(GpdContractEditModel model, System.Web.Mvc.ModelStateDictionary ms)
        {
            bool hasError = false;
            SetGpdContractPersons(model, hasError);
            SetGpdContractChargingTypes(model, hasError);
            SetGpdContractDetails(model, hasError);
            SetGpdContractStatuses(model, hasError);

            if (model.StatusID != 4 && model.Id != 0)
            {
                if (model.DateP != null)
                {
                    if (model.DateP <= model.DateEnd)
                        ms.AddModelError("DateP", "Дата пролонгации должна быть больше даты окончания действия договора!");
                }
            }
            else
            {
                if (model.DepartmentId == 0)
                    ms.AddModelError("DepartmentId", "Выберите подразделение!");
                else
                {
                    //проверяем уровень выбранного подразделения
                    if (GpdContractDao.GetDepLevel(model.DepartmentId) != 7)
                        ms.AddModelError("DepartmentId", "Нужно выбрать подразделение седьмого уровня!");
                }

                if (model.PersonID == 0)
                    ms.AddModelError("PersonID", "Выберите физическое лицо из списка!");

                if (model.CTID == 0)
                    ms.AddModelError("CTID", "Выберите вид начисления из списка!");

                if (model.NameContract == null)
                    ms.AddModelError("NameContract", "Заполните поле 'Наименование договора'!");

                if (model.NumContract == null)
                    ms.AddModelError("NumContract", "Заполните поле '№ договора'!");

                if (model.DateBegin == null)
                    ms.AddModelError("DateBegin", "Укажите дату начала действия договора!");

                if (model.DateBegin < DateTime.Today && model.DateBegin.Value.Month != DateTime.Today.Month)
                    ms.AddModelError("DateBegin", "Дата начала срока действия договора должна входить в текущий месяц!");

                if (model.DateEnd == null)
                    ms.AddModelError("DateEnd", "Укажите дату окончания действия договора!");

                if (model.DateEnd < model.DateBegin)
                    ms.AddModelError("DateEnd", "Дата окончания действия договора должна быть больше, чем дата начала договора!");

                if (model.DateP != null)
                {
                    if (model.DateP <= model.DateEnd)
                        ms.AddModelError("DateP", "Дата пролонгации должна быть больше даты окончания действия договора!");
                }

                if (model.PayerID == 0)
                    ms.AddModelError("PayerID", "Выберите плательщика из списка!");

                if (model.PayeeID == 0)
                    ms.AddModelError("PayeeID", "Выберите получателя из списка!");


                if (model.GPDID == null)
                    ms.AddModelError("GPDID", "Заполните поле 'ID ГПД в ЭССС'!");

                if (model.PurposePayment == null)
                    ms.AddModelError("PurposePayment", "Заполните поле 'Назначение платежа'!");
            }
        }
        /// <summary>
        /// Процедура сохранения договора в базе данных.
        /// </summary>
        /// <param name="model">Текущая модель.</param>
        /// <param name="error">Переменная для возврата текста сообщения об ошибке.</param>
        public bool SaveGpdContract(GpdContractEditModel model, out string error)
        {
            error = string.Empty;
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            IUser currentUseId = AuthenticationService.CurrentUser;

            try
            {
                GpdContract gpdContract;// = GpdContractDao.Get(model.Id);

                if (model.Id == 0)
                {
                    gpdContract = new GpdContract
                    {
                        //Id = model.Id,
                        CreatorID = currentUseId.Id,
                        DepartmentId = model.DepartmentId,
                        PersonID = model.PersonID,
                        CTID = model.CTID,
                        StatusID = model.StatusID,
                        NumContract = model.NumContract,
                        NameContract = model.NameContract,
                        DateBegin = model.DateBegin,
                        DateEnd = model.DateEnd,
                        PayeeID = model.PayeeID,
                        PayerID = model.PayerID,
                        GPDID = model.GPDID,
                        PurposePayment = model.PurposePayment,
                        DateP = model.DateP,                        
                        IsLong = model.DateP.HasValue ? true : false,
                        MagEntities = new List<GpdMagProlongation>()
                    };
                }
                else
                {
                    gpdContract = GpdContractDao.Get(model.Id);
                    if (gpdContract.StatusID != 4)
                    {
                        if (model.DateP.HasValue)
                        {
                            gpdContract.DateP = model.DateP.Value;
                            gpdContract.IsLong = model.DateP.HasValue ? true : false;
                        }
                    }
                    else
                    {
                        gpdContract.CreatorID = currentUseId.Id;
                        gpdContract.DepartmentId = model.DepartmentId;
                        gpdContract.PersonID = model.PersonID;
                        gpdContract.CTID = model.CTID;
                        gpdContract.StatusID = model.StatusID;
                        gpdContract.NumContract = model.NumContract;
                        gpdContract.NameContract = model.NameContract;
                        gpdContract.DateBegin = model.DateBegin.Value;
                        gpdContract.DateEnd = model.DateEnd.Value;
                        gpdContract.PayeeID = model.PayeeID;
                        gpdContract.PayerID = model.PayerID;
                        gpdContract.GPDID = model.GPDID;
                        gpdContract.PurposePayment = model.PurposePayment;
                        if (model.DateP.HasValue)
                        {
                            gpdContract.DateP = model.DateP.Value;
                            gpdContract.IsLong = true;
                        }
                    }
                }

                ChangeEntityProperties(gpdContract, model, currentUseId.Id);
                GpdContractDao.SaveAndFlush(gpdContract);
                model.Id = gpdContract.Id;
                return true;
            }
            catch (Exception ex)
            {
                GpdContractDao.RollbackTran();
                Log.Error("Error on SaveMissionOrderEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                //SetUserInfoModel(user, model);
                //SetStaticFields(model, missionHotels);
                //LoadDictionaries(model);
                //SetHiddenFields(model);
            }
        }
        /// <summary>
        /// Редактируем подчиненную строку (журнал дат пролонгации договоров)
        /// </summary>
        /// <param name="entity">Объект договора.</param>
        /// <param name="model">Модель.</param>
        /// <param name="UserID">ID пользователя.</param>
        protected void ChangeEntityProperties(GpdContract entity, GpdContractEditModel model, int UserID)
        {
            //if (model.Id == 0) return;
            if (model.DateP.HasValue && model.DatePOld != model.DateP)
            {
                //создаем строку для подчиненной таблицы
                GpdMagProlongation MagProlong = new GpdMagProlongation
                {
                    //GCID = entity.Id,
                    DateP = model.DateP,
                    CreatorID = UserID,
                    GpdContracts = entity
                };
                entity.MagEntities.Add(MagProlong);
            }
        }
        #endregion

        #region Акты
        public IGpdActDao gpdActDao;
        public IGpdActDao GpdActDao
        {
            get { return Validate.Dependency(gpdActDao); }
            set { gpdActDao = value; }
        }
        /// <summary>
        /// Заполняем модель для создания/редактирования акта по указанному.
        /// </summary>
        /// <param name="Id">Значение ID акт.</param>
        /// <param name="GCID">Занчение ID договора.</param>
        /// <param name="hasError">Признак ошибки.</param>
        /// <returns></returns>
        public GpdActEditModel SetActEditModel(int Id, int GCID, bool hasError)
        {
            GpdActEditModel model = new GpdActEditModel();
            UserRole role = CurrentUser.UserRole;
            IList<GpdActDto> document = null;
            //создание нового акта
            if (Id == 0)
                document = GpdActDao.GetNewAct(role, GCID);
            else //редактирование существующего
                document = GpdActDao.GetAct(role, Id, false, model.DateBegin, model.DateEnd, 0, null, 0, 0, false);

            if (document.Count > 0)
            {
                foreach (var doc in document)
                {
                    model.Id = doc.Id;
                    model.ActDate = doc.ActDate;
                    model.ActNumber = (Id == 0 ? doc.GCID.ToString() + "/" + doc.GCCount.ToString() : doc.ActNumber);
                    model.Surname = doc.Surname;
                    model.NameContract = doc.NameContract;
                    model.NumContract = doc.NumContract + (doc.ContractBeginDate.HasValue && doc.ContractEndDate.HasValue ? " с " + doc.ContractBeginDate.Value.ToShortDateString() + " по " + doc.ContractEndDate.Value.ToShortDateString() : "");
                    model.ContractBeginDate = doc.ContractBeginDate;
                    model.ContractEndDate = doc.ContractEndDate;
                    model.DepLevel3Name = doc.DepLevel3Name;
                    model.ChargingDate = doc.ChargingDate;
                    model.DateBegin = doc.DateBegin;
                    model.DateEnd = doc.DateEnd;
                    model.Amount = doc.Amount;
                    model.AmountPayment = doc.AmountPayment;
                    model.POrderDate = doc.POrderDate;
                    model.PurposePayment = doc.PurposePayment;
                    model.ESSSNum = doc.ESSSNum;
                    model.Autor = doc.CreatorName + (doc.CreateDate.HasValue ? " - " + doc.CreateDate.Value.ToShortDateString() : "");
                    model.StatusName = doc.StatusName;
                    model.StatusID = doc.StatusID;
                    model.GCID = doc.GCID;
                    model.CreatorID = doc.CreatorID;
                }
            }

            model.Comments = GpdActDao.GetComments(model.Id);

            return model;
        }
        /// <summary>
        /// Проверки при сохранении акта ГПД.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="ms">Словарь.</param>
        public void CheckFillFieldsForGpdAct(GpdActEditModel model, System.Web.Mvc.ModelStateDictionary ms)
        {
            if (model.ActDate == null)
                ms.AddModelError("ActDate", "Укажите дату акта!");
            else
            {
                if (model.ActDate < model.ContractBeginDate || model.ActDate > model.ContractEndDate)
                    ms.AddModelError("ActDate", "Дата акта должна входить в период действия договора!");
            }

            if (model.ChargingDate == null)
                ms.AddModelError("ChargingDate", "Укажите дату начисления!");

            if (model.DateBegin == null)
                ms.AddModelError("DateBegin", "Укажите начало периода оплаты!");

            if (model.DateEnd == null)
                ms.AddModelError("DateEnd", "Укажите конец периода оплаты!");

            if (model.DateBegin != null && model.DateEnd != null)
            {
                if (model.DateBegin < model.ContractBeginDate || model.DateBegin > model.ContractEndDate)
                    ms.AddModelError("DateBegin", "Дата начала срока оплаты должна входить в период действия договора!");

                if (model.DateEnd < model.ContractBeginDate || model.DateEnd > model.ContractEndDate)
                    ms.AddModelError("DateEnd", "Дата конца срока оплаты должна входить в период действия договора!");

                if (model.DateBegin > model.DateEnd)
                    ms.AddModelError("DateBegin", "Дата начала срока оплаты должна быть меньше даты конца срока оплаты!");
            }


            if (model.Amount == 0)
                ms.AddModelError("Amount", "Сумма не должна быть равна нулю!");

            if (model.PurposePayment == null)
                ms.AddModelError("PurposePayment", "Укажите Назначение договора!");

            if (model.ESSSNum == null)
                ms.AddModelError("ESSSNum", "Укажите № заявки ЭССС!");

        }
        /// <summary>
        /// Процедура сохранения акта в базе данных.
        /// </summary>
        /// <param name="model">Текущая модель.</param>
        /// <param name="error">Переменная для возврата текста сообщения об ошибке.</param>
        public bool SaveGpdAct(GpdActEditModel model, out string error)
        {
            error = string.Empty;
            UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            IUser currentUseId = AuthenticationService.CurrentUser;

            try
            {
                GpdAct gpdAct;// = GpdContractDao.Get(model.Id);

                if (model.Id == 0)
                {
                    gpdAct = new GpdAct
                    {
                        CreatorID = currentUseId.Id,
                        EditDate = DateTime.Now,
                        EditorID = currentUseId.Id,
                        ActNumber = model.ActNumber,
                        ActDate = model.ActDate,
                        GCID = model.GCID,
                        ChargingDate = model.ChargingDate,
                        DateBegin = model.DateBegin,
                        DateEnd = model.DateEnd,
                        Amount = model.Amount,
                        PurposePayment = model.PurposePayment,
                        ESSSNum = model.ESSSNum,
                        StatusID = model.StatusID
                    };
                }
                else
                {
                    gpdAct = GpdActDao.Get(model.Id);
                    gpdAct.CreatorID = model.CreatorID;
                    gpdAct.EditDate = DateTime.Now;
                    gpdAct.EditorID = currentUseId.Id;
                    gpdAct.ActNumber = model.ActNumber;
                    gpdAct.ActDate = model.ActDate;
                    gpdAct.GCID = model.GCID;
                    gpdAct.ChargingDate = model.ChargingDate;
                    gpdAct.DateBegin = model.DateBegin;
                    gpdAct.DateEnd = model.DateEnd;
                    gpdAct.Amount = model.Amount;
                    gpdAct.PurposePayment = model.PurposePayment;
                    gpdAct.ESSSNum = model.ESSSNum;
                    gpdAct.StatusID = model.StatusID;
                }

                AddComment(gpdAct, model);
                GpdActDao.SaveAndFlush(gpdAct);
                model.Id = gpdAct.Id;
                return true;
            }
            catch (Exception ex)
            {
                GpdContractDao.RollbackTran();
                Log.Error("Error on SaveMissionOrderEditModel:", ex);
                error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                return false;
            }
            finally
            {
                //SetUserInfoModel(user, model);
                //SetStaticFields(model, missionHotels);
                //LoadDictionaries(model);
                //SetHiddenFields(model);
            }
        }
        /// <summary>
        /// Заполняем модель просмора актов ГПД (запросная форма).
        /// </summary>
        /// <param name="model"></param>
        /// <param name="hasError"></param>
        public void SetGpdActFind(GpdActListModel model, bool hasError)
        {
            DateTime today = DateTime.Today;
            model.DateBegin = new DateTime(today.Year, today.Month, 1);
            model.DateEnd = today;
            model.Statuses = GpdActDao.GetStatuses();//список статусов
        }
        /// <summary>
        /// Заполняем модель просмора актов ГПД.
        /// </summary>
        /// <param name="model">Обрабатываемая модель.</param>
        /// <param name="hasError">Флажок для ошибок.</param>
        public void SetGpdActView(GpdActListModel model, bool hasError)
        {
            UserRole role = CurrentUser.UserRole;
            model.Statuses = GpdActDao.GetStatuses();//список статусов
            if (!model.IsFind)
            {
                DateTime today = DateTime.Today;
                model.DateBegin = new DateTime(today.Year, today.Month, 1);
                model.DateEnd = today;
            }

            model.Documents = GpdActDao.GetAct(role, model.Id, model.IsFind, model.DateBegin, model.DateEnd, model.DepartmentId, model.Surname, model.StatusID, model.SortBy, model.SortDescending);
            
        }
        /// <summary>
        /// Добавляем комментарий.
        /// </summary>
        /// <param name="entity">Редактируемый акт.</param>
        /// <param name="model">Модель редактируемого акта.</param>
        protected void AddComment(GpdAct entity, GpdActEditModel model)
        {
            if (model.CommentStr != null && model.CommentStr.Trim().Length != 0)
            {
                GpdActComment ActComment = new GpdActComment
                {
                    UserId = UserDao.Load(AuthenticationService.CurrentUser.Id),
                    Comment = model.CommentStr,
                    CreateDate = DateTime.Now,
                    GpdActs = entity
                };
                entity.Comments.Add(ActComment);
            }
        }
        #endregion
    }
}

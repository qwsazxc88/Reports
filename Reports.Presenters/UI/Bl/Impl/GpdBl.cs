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
                        Id = model.Id,
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
                    gpdrefDetail.Id = model.Id;
                    gpdrefDetail.Name = model.Name;
                    gpdrefDetail.DTID = model.DTID;
                    gpdrefDetail.INN = model.INN;
                    gpdrefDetail.KPP = model.KPP;
                    gpdrefDetail.Account = model.Account;
                    gpdrefDetail.BankName = model.BankName;
                    gpdrefDetail.BankBIK = model.BankBIK;
                    gpdrefDetail.CorrAccount = model.CorrAccount;
                    gpdrefDetail.CreatorID = currentUseId.Id;
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
        public GpdRefDetailEditModel GetMissionOrderEditModel(int Id, bool hasError)
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
                //model.CreatorID, 
                model.DepartmentId, 
                //model.DepartmentName,
                //model.PersonID, 
                model.CTID, 
                //model.StatusID, 
                //model.NumContract, 
                //model.NameContract, 
                null, null,
                //null, null,
                //model.PayeeID, 
                //model.PayerID, 
                //model.GPDID, 
                //model.PurposePayment, 
                model.IsDraft, 
                //model.CreatorName,
                //(DateTime)model.CreateDate,
                model.Surname, 
                //model.CTName, 
                //model.StatusName,
                //model.Autor,
                //null, null,
                model.IsFind,
                0, null);

            if (model.Contracts.Count > 0)
            {
                foreach (var doc in model.Contracts)
                {
                    model.Id = doc.Id;
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
                    model.IsDraft = doc.IsDraft;
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
                model.IsDraft = true;

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
                //model.CreatorID,
                model.DepartmentId,
                //model.DepartmentName,
                //model.PersonID,
                model.CTID,
                //model.StatusID,
                //model.NumContract,
                //model.NameContract,
                model.DateBegin,
                model.DateEnd,
                //model.DateP,
                //model.DatePOld,
                //model.PayeeID,
                //model.PayerID,
                //model.GPDID,
                //model.PurposePayment,
                model.IsDraft,
                //model.CreatorName,
                //model.CreateDate,
                model.Surname,
                //model.CTName,
                //model.StatusName,
                //model.Autor, 
                //model.DepLevel3Name,
                //model.DepLevel7Name,
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
                ms.AddModelError("STID", "Выберите вид начисления из списка!");

            if (model.NameContract == null)
                ms.AddModelError("NameContract", "Заполните поле 'Наименование договора'!");

            if (model.NumContract == null)
                ms.AddModelError("NumContract", "Заполните поле '№ договора'!");

            if (model.DateBegin == null)
                ms.AddModelError("DateBegin", "Укажите дату начала действия договора!");

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

            //if (ms.Count != 0)
            //    model.IsDraft = true;

            bool hasError = false;
            SetGpdContractPersons(model, hasError);
            SetGpdContractChargingTypes(model, hasError);
            SetGpdContractDetails(model, hasError);
            SetGpdContractStatuses(model, hasError);
        }
        /// <summary>
        /// Процедура сохранения договора в базе данных.
        /// </summary>
        /// <param name="model">Текущая модель.</param>
        /// <param name="error">Переменная для возврата текста сообщения об ошибке.</param>
        public bool SaveGpdContract(GpdContractEditModel model, out string error)
        {
            error = string.Empty;
            //UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            IUser currentUseId = AuthenticationService.CurrentUser;

            try
            {
                GpdContract gpdContract = GpdContractDao.Get(model.Id);

                if (gpdContract == null)
                {
                    gpdContract = new GpdContract
                    {
                        Id = model.Id,
                        CreatorID = currentUseId.Id,
                        DepartmentId = model.DepartmentId,
                        PersonID = model.PersonID,
                        CTID = model.CTID,
                        StatusID = model.IsDraft ? 4 : 2,
                        NumContract = model.NumContract,
                        NameContract = model.NameContract,
                        DateBegin = model.DateBegin,
                        DateEnd = model.DateEnd,
                        PayeeID = model.PayeeID,
                        PayerID = model.PayerID,
                        GPDID = model.GPDID,
                        PurposePayment = model.PurposePayment,
                        IsDraft = model.IsDraft,
                        DateP = model.DateP
                    };
                }
                else
                {
                    gpdContract.Id = model.Id;
                    gpdContract.CreatorID = currentUseId.Id;
                    gpdContract.DepartmentId = model.DepartmentId;
                    gpdContract.PersonID = model.PersonID;
                    gpdContract.CTID = model.CTID;
                    gpdContract.StatusID = model.StatusID;
                    gpdContract.NumContract = model.NumContract;
                    gpdContract.NameContract = model.NameContract;
                    gpdContract.DateBegin = model.DateBegin;
                    gpdContract.DateEnd = model.DateEnd;
                    gpdContract.PayeeID = model.PayeeID;
                    gpdContract.PayerID = model.PayerID;
                    gpdContract.GPDID = model.GPDID;
                    gpdContract.PurposePayment = model.PurposePayment;
                    gpdContract.IsDraft = model.IsDraft;
                    gpdContract.DateP = model.DateP;
                }
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
        #endregion
        
    }
}

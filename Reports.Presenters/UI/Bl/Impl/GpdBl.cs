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
        public IGpdDetailSetDao gpdDetailSetDao;
        public IGpdDetailSetDao GpdDetailSetDao
        {
            get { return Validate.Dependency(gpdDetailSetDao); }
            set { gpdDetailSetDao = value; }
        }
        /// <summary>
        /// Заполняем модель справочника реквизитов для просмотра.
        /// </summary>
        /// <param name="model">Текущая модель.</param>
        /// <param name="hasError">Признак наличия ошибки.</param>
        public void SetGpdRefDetailView(GpdRefDetailModel model, bool hasError)
        {
            GetPermission(model);
        }
        /// <summary>
        /// Определяем права роли текущего пользователя.
        /// </summary>
        /// <param name="model"></param>
        public void GetPermission(GpdRefDetailModel model)
        {
            UserRole role = CurrentUser.UserRole;
            model.Permissions = GpdRefDetailDao.GetPermission(role);

            //
            if (model.Permissions.Count == 0)
            {
                GpdPermissionDto perm = new GpdPermissionDto();
                perm.IsCancel = false;
                perm.IsComment = false;
                perm.IsCreate = false;
                perm.IsCreateAct = false;
                perm.IsDraft = false;
                perm.IsWrite = false;

                model.Permissions.Add(perm);
            }
        }
        /// <summary>
        /// Определяем права роли текущего пользователя.
        /// </summary>
        /// <param name="model"></param>
        public void GetPermission(GpdRefDetailEditModel model)
        {
            UserRole role = CurrentUser.UserRole;
            model.Permissions = GpdRefDetailDao.GetPermission(role);

            //
            if (model.Permissions.Count == 0)
            {
                GpdPermissionDto perm = new GpdPermissionDto();
                perm.IsCancel = false;
                perm.IsComment = false;
                perm.IsCreate = false;
                perm.IsCreateAct = false;
                perm.IsDraft = false;
                perm.IsWrite = false;

                model.Permissions.Add(perm);
            }
        }
        /// <summary>
        /// Создаем список типов реквизитов для модели просмотра.
        /// </summary>
        /// <param name="model">Заполняемая модель</param>
        public void SetGpdRefDetailTypes(GpdRefDetailModel model)
        {
            //UserRole role = CurrentUser.UserRole;
            //model.DetailTypes = GpdRefDetailDao.GetDetailTypes(role,
            //    model.DTID,
            //    model.TypeName);
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
            UserRole role = CurrentUser.UserRole;
            GetPermission(model);
            model.Documents = GpdRefDetailDao.GetDetailSetList(0, model.Name, model.Surname, model.PayerName, model.PayeeName, true, model.SortBy, model.SortDescending);
        }
        /// <summary>
        /// Проверяем правильность заполнения полей.
        /// </summary>
        /// <param name="model">Проверяемая модель.</param>
        /// <param name="ms">Словарь для сообщений об ошибках.</param>
        /// <param name="flgFromContract">Признак проверки модели заполняемой в договорах.</param>
        public void CheckFillFieldsForGpdRefDetail(GpdRefDetailEditModel model, System.Web.Mvc.ModelStateDictionary ms, bool flgFromContract)
        {
            string ModelName = flgFromContract ? "DetailEdit." : "";
            GetPermission(model);
            if (model.Operation == 1)
            {
                if (model.Name == null)
                    ms.AddModelError(ModelName + "Name", "Заполните поле 'Наименование'!");
                if (model.Name != null && model.Name.Trim().Length > 250)
                    ms.AddModelError(ModelName + "Name", "Превышено допустимое количество символов!");

                if (model.PersonID == 0)
                    ms.AddModelError(ModelName + "PersonID", "Выберите физическое лицо!");

                if (model.PayerID == 0)
                    ms.AddModelError(ModelName + "PayerID", "Укажите плательщика!");

                if (model.PayeeID == 0)
                    ms.AddModelError(ModelName + "PayeeID", "Укажите получателя!");

                if (model.Account == null)
                    ms.AddModelError(ModelName + "Account", "Укажите номер счета получателя!");
                if (model.Account != null && model.Account.Trim().Length > 20)
                    ms.AddModelError(ModelName + "Account", "Превышено допустимое количество символов!");
            }
            else
            {
                if (model.DetailName == null)
                    ms.AddModelError(ModelName + "DetailName", "Заполните поле 'Наименование'");
                if (model.DetailName != null && model.DetailName.Trim().Length > 150)
                    ms.AddModelError(ModelName + "DetailName", "Превышено допустимое количество символов!");

                if (model.INN == null)
                    ms.AddModelError(ModelName + "INN", "Заполните поле 'ИНН'");
                if (model.INN != null && model.INN.Trim().Length > 12)
                    ms.AddModelError(ModelName + "INN", "Превышено допустимое количество символов!");

                if (model.KPP == null)
                    ms.AddModelError(ModelName + "KPP", "Заполните поле 'КПП'");
                if (model.KPP != null && model.KPP.Trim().Length > 9)
                    ms.AddModelError(ModelName + "KPP", "Превышено допустимое количество символов!");

                if (model.DetailAccount == null)
                    ms.AddModelError(ModelName + "DetailAccount", "Заполните поле 'Расчетный счет'");
                if (model.DetailAccount != null && model.DetailAccount.Trim().Length > 20)
                    ms.AddModelError(ModelName + "DetailAccount", "Превышено допустимое количество символов!");

                if (model.BankName == null)
                    ms.AddModelError(ModelName + "BankName", "Заполните поле 'Банк'");
                if (model.BankName != null && model.BankName.Trim().Length > 100)
                    ms.AddModelError(ModelName + "BankName", "Превышено допустимое количество символов!");

                if (model.BankBIK == null)
                    ms.AddModelError(ModelName + "BankBIK", "Заполните поле 'Банк БИК'");
                if (model.BankBIK != null && model.BankBIK.Trim().Length > 9)
                    ms.AddModelError(ModelName + "BankBIK", "Превышено допустимое количество символов!");

                if (model.CorrAccount == null)
                    ms.AddModelError(ModelName + "CorrAccount", "Заполните поле 'Банк кор/счет'");
                if (model.CorrAccount != null && model.CorrAccount.Trim().Length > 20)
                    ms.AddModelError(ModelName + "Name", "Превышено допустимое количество символов!");
            }
            

            //if (model.DTID == 2)
            //{
            //    if (model.Code == null)
            //        ms.AddModelError(ModelName + "Code", "Заполните поле 'Код банка'");
            //    if (model.Code != null && model.Code.Trim().Length > 9)
            //        ms.AddModelError(ModelName + "Name", "Превышено допустимое количество символов!");
            //}
            //else
            //    model.Code = null;

            if (ms.Count != 0)
                model.StatusID = 4;

            UserRole role = CurrentUser.UserRole;
            model.DetailTypes = GpdRefDetailDao.GetDetailTypes(role,
                model.DTID,
                model.TypeName);
            //физики
            model.Persons = GpdRefDetailDao.GetPersons(0);
            //плательщики
            model.PayerInfo = GpdRefDetailDao.GetRefDetail(role, 0, 2);
            //получатели
            model.PayeerInfo = GpdRefDetailDao.GetRefDetail(role, 0, 1);
            //список реквизитов
            model.RefDetails = GpdRefDetailDao.GetRefDetail(role, 0, model.DTID);
        }
        /// <summary>
        /// Процедура сохранения записи в базе данных.
        /// </summary>
        /// <param name="model">Текущая модель.</param>
        /// <param name="error">Переменная для возврата текста сообщения об ошибке.</param>
        public bool SaveGpdRefDetail(GpdRefDetailEditModel model, out string error)
        {
            error = string.Empty;

            if (model.StatusID == 3)
                return true;

            //UserRole currentUserRole = AuthenticationService.CurrentUser.UserRole;
            IUser currentUseId = AuthenticationService.CurrentUser;

            //набор
            if (model.Operation == 1)
            {
                try
                {
                    GpdDetailSet gpdDetailSet = GpdDetailSetDao.Get(model.Id);

                    if (gpdDetailSet == null)
                    {
                        gpdDetailSet = new GpdDetailSet
                        {
                            Name = model.Name,
                            PersonID = model.PersonID,
                            PayerID = model.PayerID,
                            PayeeID = model.PayeeID,
                            Account = model.Account,
                            CreatorID = currentUseId.Id,
                        };
                    }
                    else
                    {
                        gpdDetailSet.Name = model.Name;
                        gpdDetailSet.PersonID = model.PersonID;
                        gpdDetailSet.PayerID = model.PayerID;
                        gpdDetailSet.PayeeID = model.PayeeID;
                        gpdDetailSet.Account = model.Account;
                        gpdDetailSet.CreatorID = model.CreatorID;
                        gpdDetailSet.EditorID = currentUseId.Id;
                        gpdDetailSet.EditDate = DateTime.Now;
                    }
                    GpdDetailSetDao.SaveAndFlush(gpdDetailSet);
                    model.Id = gpdDetailSet.Id;
                    model.StatusID = 2;
                    return true;
                }
                catch (Exception ex)
                {
                    GpdDetailSetDao.RollbackTran();
                    Log.Error("Error on SaveMissionOrderEditModel:", ex);
                    error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                    return false;
                }
            }
            else  //реквизиты
            {
                try
                {
                    GpdRefDetail gpdrefDetail = GpdRefDetailDao.Get(model.DetailId);

                    if (model.NewRow == 1 || gpdrefDetail == null)
                    {
                        gpdrefDetail = new GpdRefDetail
                        {
                            Name = model.DetailName,
                            DTID = model.DTID,
                            INN = model.INN,
                            KPP = model.KPP,
                            Account = model.DetailAccount,
                            BankName = model.BankName,
                            BankBIK = model.BankBIK,
                            CorrAccount = model.CorrAccount,
                            CreatorID = currentUseId.Id,
                        };
                    }
                    else
                    {
                        gpdrefDetail.Name = model.DetailName;
                        gpdrefDetail.DTID = model.DTID;
                        gpdrefDetail.INN = model.INN;
                        gpdrefDetail.KPP = model.KPP;
                        gpdrefDetail.Account = model.DetailAccount;
                        gpdrefDetail.BankName = model.BankName;
                        gpdrefDetail.BankBIK = model.BankBIK;
                        gpdrefDetail.CorrAccount = model.CorrAccount;
                        gpdrefDetail.CreatorID = model.DetailCreatorID;
                        gpdrefDetail.EditorID = currentUseId.Id;
                        gpdrefDetail.EditDate = DateTime.Now;
                    }
                    GpdRefDetailDao.SaveAndFlush(gpdrefDetail);
                    model.DetailId = gpdrefDetail.Id;
                    model.StatusID = 2;
                    return true;
                }
                catch (Exception ex)
                {
                    GpdRefDetailDao.RollbackTran();
                    Log.Error("Error on SaveMissionOrderEditModel:", ex);
                    error = string.Format("Исключение:{0}", ex.GetBaseException().Message);
                    return false;
                }
            }
        }
        /// <summary>
        /// Заполняем модель для редактирования по указанному ID.
        /// </summary>
        /// <param name="Id">Значение ID.</param>
        /// <param name="StatusID">Статус записи.</param>
        /// <param name="Operation">Операция: 1 - работа с набором, 2 - работа с реквизитом.</param>
        /// <param name="flgView">Признак просмотра списка.</param>
        /// <param name="DTID">ID типа реквизита</param>
        /// <param name="PayerID">ID плательщика</param>
        /// <param name="PayeeID">ID получателя</param>
        /// <param name="DetailId">ID реквизита</param>
        /// <returns></returns>
        public GpdRefDetailEditModel SetRefDetailEditModel(int Id, int StatusID, int Operation, bool flgView, int DTID, int PayerID, int PayeeID, int DetailId)
        {
            GpdRefDetailEditModel model = new GpdRefDetailEditModel();
            GetPermission(model);
            SetGpdRefDetailTypes(model);
            model.Id = Id;
            model.StatusID = StatusID == 3 ? 2 : StatusID;
            model.Operation = Operation;
            model.DTID = DTID;
            model.PayerID = PayerID;
            model.PayeeID = PayeeID;
            model.DetailId = DetailId;
            model.SetInfo = GpdRefDetailDao.GetDetailSetList(model.Id, null, null, null, null, flgView, 0, null);

            if (model.SetInfo.Count != 0)
            {
                foreach (var doc in model.SetInfo)
                {
                    model.Id = doc.Id;
                    model.Name = doc.Name;
                    model.PersonID = doc.PersonID;
                    model.PayerID = doc.PayerID;//PayerID != doc.PayerID ? PayerID : doc.PayerID;
                    model.PayeeID = doc.PayeeID;//PayeeID != doc.PayeeID ? PayeeID : doc.PayeeID;
                    model.Account = doc.Account;
                    model.CreatorID = doc.CreatorID;
                }
            }

            UserRole role = CurrentUser.UserRole;
            //физики
            model.Persons = GpdRefDetailDao.GetPersons(0);
            //плательщики
            model.PayerInfo = GpdRefDetailDao.GetRefDetail(role, 0, 2);
            
            if (model.PayerInfo.Count != 0)
            {
                //model.PayerID = PayerID == 0 ? model.PayerInfo[0].Id : model.PayerID;
                foreach (var doc in model.PayerInfo)
                {
                    if (doc.Id == model.PayerID)
                    {
                        model.PayerName = doc.Name;
                        model.PayerINN = doc.INN;
                        model.PayerKPP = doc.KPP;
                        model.PayerAccount = doc.Account;
                        model.PayerBankName = doc.BankName;
                        model.PayerBankBIK = doc.BankBIK;
                        model.PayerCorrAccount = doc.CorrAccount;
                        break;
                    }
                    else //если не указан плательщик, то берем первого из списка
                    {
                        if (model.PayerID == 0)
                        {
                            model.PayerName = doc.Name;
                            model.PayerINN = doc.INN;
                            model.PayerKPP = doc.KPP;
                            model.PayerAccount = doc.Account;
                            model.PayerBankName = doc.BankName;
                            model.PayerBankBIK = doc.BankBIK;
                            model.PayerCorrAccount = doc.CorrAccount;
                            break;
                        }
                    }
                }
            }

            //получатели
            model.PayeerInfo = GpdRefDetailDao.GetRefDetail(role, 0, 1);
            if (model.PayeerInfo.Count != 0)
            {
                //model.PayeeID = PayeeID == 0 ? model.PayeerInfo[0].Id : model.PayeeID;
                foreach (var doc in model.PayeerInfo)
                {
                    if (doc.Id == model.PayeeID)
                    {
                        model.PayeerName = doc.Name;
                        model.PayeerINN = doc.INN;
                        model.PayeerKPP = doc.KPP;
                        model.PayeerAccount = doc.Account;
                        model.PayeerBankName = doc.BankName;
                        model.PayeerBankBIK = doc.BankBIK;
                        model.PayeerCorrAccount = doc.CorrAccount;
                        break;
                    }
                    else //если не указан получатель, то берем первого из списка
                    {
                        if (model.PayeeID == 0)
                        {
                            model.PayeerName = doc.Name;
                            model.PayeerINN = doc.INN;
                            model.PayeerKPP = doc.KPP;
                            model.PayeerAccount = doc.Account;
                            model.PayeerBankName = doc.BankName;
                            model.PayeerBankBIK = doc.BankBIK;
                            model.PayeerCorrAccount = doc.CorrAccount;
                            break;
                        }
                    }
                }
            }

            //список реквизитов
            model.RefDetails = GpdRefDetailDao.GetRefDetail(role, 0, DTID);

            //если входим в режим редактирования реквизита
            //if (model.Operation == 2 && model.StatusID == 4)
            if (model.StatusID == 4 || model.StatusID == 2)
            {
                if (model.RefDetails.Count != 0)
                {
                    foreach (var doc in model.RefDetails)
                    {
                        if (doc.Id == model.DetailId || model.DetailId == 0 || model.RefDetails.Where(x => x.Id == model.DetailId).Count() == 0)
                        {
                            model.DTID = DTID;
                            model.DetailId = doc.Id;
                            model.DetailName = doc.Name;
                            model.INN = doc.INN;
                            model.KPP = doc.KPP;
                            model.DetailAccount = doc.Account;
                            model.BankName = doc.BankName;
                            model.BankBIK = doc.BankBIK;
                            model.CorrAccount = doc.CorrAccount;
                            break;
                        }
                        else
                        {
                        }
                    }
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
        /// Определяем права роли текущего пользователя.
        /// </summary>
        /// <param name="model"></param>
        public void GetPermission(GpdContractModel model)
        {
            UserRole role = CurrentUser.UserRole;
            model.Permissions = GpdContractDao.GetPermission(role);

            //
            if (model.Permissions.Count == 0)
            {
                GpdPermissionDto perm = new GpdPermissionDto();
                perm.IsCancel = false;
                perm.IsComment = false;
                perm.IsCreate = false;
                perm.IsCreateAct = false;
                perm.IsDraft = false;
                perm.IsWrite = false;

                model.Permissions.Add(perm);
            }
        }
        /// <summary>
        /// Определяем права роли текущего пользователя.
        /// </summary>
        /// <param name="model"></param>
        public void GetPermission(GpdContractEditModel model)
        {
            UserRole role = CurrentUser.UserRole;
            model.Permissions = GpdContractDao.GetPermission(role);

            //
            if (model.Permissions.Count == 0)
            {
                GpdPermissionDto perm = new GpdPermissionDto();
                perm.IsCancel = false;
                perm.IsComment = false;
                perm.IsCreate = false;
                perm.IsCreateAct = false;
                perm.IsDraft = false;
                perm.IsWrite = false;

                model.Permissions.Add(perm);
            }
        }
        /// <summary>
        /// Просмотр договоров.
        /// </summary>
        /// <param name="model">Обрабатываемая модель.</param>
        /// <param name="hasError">Флажок для ошибок.</param>
        public void SetGpdContractView(GpdContractModel model)
        {
            if (!model.IsFind)
            {
                DateTime today = DateTime.Today;
                //model.DateBegin = new DateTime(today.Year, today.Month, 1);
                model.DateBegin = new DateTime(2010, 1, 1);
                model.DateEnd = today;
            }
            GetPermission(model);
            SetGpdContractStatuses(model);
            SetGpdContractChargingTypes(model);
            if (model.IsFind)
                SetGpdContract(model);
        }
        /// <summary>
        /// Заполнение модели для создания/редактирования договора.
        /// </summary>
        /// <param name="Id">ID договора.</param>
        /// <param name="PersonID">ID физического лица</param>
        /// <param name="DepId">ID подразделения</param>
        /// <param name="DepName">Название подразделения</param>
        /// <returns></returns>
        public GpdContractEditModel SetGpdContractEdit(int Id, int PersonID, int DepId, string DepName)
        {
            GpdContractEditModel model = new GpdContractEditModel();
            model.Id = Id;

            UserRole role = CurrentUser.UserRole;
            model.Contracts = GpdContractDao.GetContracts(role, 
                model.Id, 
                model.DepartmentId, 
                model.CTID, 
                null, null,
                model.Surname,
                model.NumContract,
                model.IsFind,
                0, null);

            if (model.Contracts.Count > 0)
            {
                foreach (var doc in model.Contracts)
                {
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
                    model.PayeeName = doc.PayeeName;
                    model.PayerName = doc.PayerName;
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
                    model.PaymentPeriodID = doc.PaymentPeriodID;
                    model.Amount = doc.Amount;
                    model.BankName = doc.BankName;
                    model.Account = doc.Account;
                    model.DSID = doc.DSID;
                    model.PurposePaymentPart = doc.PurposePaymentPart;
                }
            }
            else
            {
                model.StatusID = 4;
                model.DepartmentId = DepId;
                model.DepartmentName = DepName;
            }


            SetGpdContractEditDropDowns(model);

            //заполняем поля после выбора фио в поле с автозаполнением
            //если договор только зоздается и еще не сохранен, на странице уже могут выбрать подразделение
            if (PersonID != 0 && Id == 0)
            {
                IList<GpdContractSurnameDto> Persons = GetPersonAutocomplete(null, PersonID);
                if (Persons.Count != 0)
                {
                    foreach (var doc in Persons)
                    {
                        model.DSID = doc.Id;
                        model.DepartmentId = DepId;
                        model.DepartmentName = DepName;
                        model.PersonID = doc.PersonID;
                        model.Surname = doc.LongName;
                        model.PayerName = doc.PayerName;
                        model.PayeeName = doc.PayeeName;
                        model.BankName = doc.BankName;
                        model.Account = doc.Account;
                        model.PurposePaymentPart = "Договор ГПХ # " + doc.Account + " ## " + doc.Name + " *";
                        //model.PurposePayment = model.PurposePaymentPart + " " + model.PurposePayment; 
                    }
                }
            }


            return model;
        }
        /// <summary>
        /// Заполняем модель.
        /// </summary>
        /// <param name="model">Обрабатываемая модель с частично заполненными полями</param>
        /// <returns></returns>
        public GpdContractEditModel SetGpdContractEdit(GpdContractEditModel model)
        {

            UserRole role = CurrentUser.UserRole;
            model.Contracts = GpdContractDao.GetContracts(role,
                model.Id,
                model.DepartmentId,
                model.CTID,
                null, null,
                model.Surname,
                model.NumContract,
                model.IsFind,
                0, null);

            if (model.Contracts.Count > 0)
            {
                foreach (var doc in model.Contracts)
                {
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
                    model.PayeeName = doc.PayeeName;
                    model.PayerName = doc.PayerName;
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
                    model.PaymentPeriodID = doc.PaymentPeriodID;
                    model.Amount = doc.Amount;
                    model.BankName = doc.BankName;
                    model.Account = doc.Account;
                    model.DSID = doc.DSID;
                    model.PurposePaymentPart = doc.PurposePaymentPart;
                }
            }
            else
            {
                model.StatusID = 4;
                //model.DepartmentId = DepId;
                //model.DepartmentName = DepName;
            }


            SetGpdContractEditDropDowns(model);

            //заполняем поля после выбора фио в поле с автозаполнением
            //если договор только зоздается и еще не сохранен, на странице уже могут выбрать подразделение
            if (model.PersonID != 0 && model.Id == 0)
            {
                IList<GpdContractSurnameDto> Persons = GetPersonAutocomplete(null, model.PersonID);
                if (Persons.Count != 0)
                {
                    foreach (var doc in Persons)
                    {
                        model.DSID = doc.Id;
                        //model.DepartmentId = DepId;
                        //model.DepartmentName = DepName;
                        model.PersonID = doc.PersonID;
                        model.Surname = doc.LongName;
                        model.PayerName = doc.PayerName;
                        model.PayeeName = doc.PayeeName;
                        model.BankName = doc.BankName;
                        model.Account = doc.Account;
                        model.PurposePaymentPart = "Договор ГПХ # " + doc.Account + " ## " + doc.Name + " *";
                        //model.PurposePayment = model.PurposePaymentPart + " " + model.PurposePayment; 
                    }
                }
            }


            return model;
        }
        /// <summary>
        /// Заполняем выпадающие списки для модели редактирования договоров ГПД.
        /// </summary>
        /// <param name="model"></param>
        public void SetGpdContractEditDropDowns(GpdContractEditModel model)
        {
            GetPermission(model);
            SetGpdContractChargingTypes(model);
            SetGpdContractStatuses(model);
            SetGpdPaymentPeriods(model);
        }
        /// <summary>
        /// Создаем список статусов договоров для модели просмотра.
        /// </summary>
        /// <param name="model"></param>
        public void SetGpdContractStatuses(GpdContractModel model)
        {
            UserRole role = CurrentUser.UserRole;
            model.Statuses = GpdContractDao.GetStatuses(role, model.StatusID, model.StatusName);
        }
        /// <summary>
        /// Создаем список статусов договоров для модели редактирования.
        /// </summary>
        /// <param name="model"></param>
        public void SetGpdContractStatuses(GpdContractEditModel model)
        {
            UserRole role = CurrentUser.UserRole;
            model.Statuses = GpdContractDao.GetStatuses(role, model.StatusID, model.StatusName);
        }
        /// <summary>
        /// Создаем список сроков оплаты.
        /// </summary>
        /// <param name="model"></param>
        public void SetGpdPaymentPeriods(GpdContractEditModel model)
        {
            UserRole role = CurrentUser.UserRole;
            model.PaymentPeriods = GpdContractDao.GetPaymentPeriods();
        }
        /// <summary>
        /// Создаем список видов начисления для модели просмотра.
        /// </summary>
        /// <param name="model"></param>
        public void SetGpdContractChargingTypes(GpdContractModel model)
        {
            UserRole role = CurrentUser.UserRole;
            model.ChargingTypes = GpdContractDao.GetChargingTypes(role, model.CTID, model.CTName);
        }
        /// <summary>
        /// Создаем список видов начисления для модели редактирования.
        /// </summary>
        /// <param name="model"></param>
        public void SetGpdContractChargingTypes(GpdContractEditModel model)
        {
            UserRole role = CurrentUser.UserRole;
            model.ChargingTypes = GpdContractDao.GetChargingTypes(role, model.CTID, model.CTName);
        }
        /// <summary>
        /// Создаем список физических лиц для модели редактирования.
        /// </summary>
        /// <param name="model"></param>
        public void SetGpdContractPersons(GpdContractEditModel model)
        {
            UserRole role = CurrentUser.UserRole;
            model.Persons = GpdContractDao.GetPersons(role, model.PersonID, model.Surname);
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
                model.NumContract,
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

                if (model.PaymentPeriodID == 0)
                    ms.AddModelError("PaymentPeriodID", "Укажите срок оплаты!");

                if (model.GPDID == null)
                    ms.AddModelError("GPDID", "Заполните поле 'ID ГПД в ЭССС'!");

                if (model.PurposePayment == null)
                    ms.AddModelError("PurposePayment", "Заполните поле 'Назначение платежа'!");

                if (model.Amount == 0)
                    ms.AddModelError("Amount", "Сумма не должна быть равна нулю!");
            }

            SetGpdContractEditDropDowns(model);
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
                        //PayeeID = model.PayeeID,
                        //PayerID = model.PayerID,
                        GPDID = model.GPDID,
                        PurposePayment = model.PurposePayment,
                        DateP = model.DateP,                        
                        IsLong = model.DateP.HasValue ? true : false,
                        PaymentPeriodID = model.PaymentPeriodID,
                        Amount = model.Amount,
                        DSID = model.DSID,
                        //EditDate = DateTime.Now,
                        //EditorID = currentUseId.Id,
                        MagEntities = new List<GpdMagProlongation>()
                    };
                }
                else
                {
                    gpdContract = GpdContractDao.Get(model.Id);
                    if (gpdContract.StatusID != 4)
                    {
                        gpdContract.StatusID = model.StatusID == 3 ? 4 : model.StatusID;

                        if (model.DateP.HasValue && model.DatePOld.Value != model.DateP.Value)
                        {
                            gpdContract.DateP = model.DateP.Value;
                            gpdContract.IsLong = model.DateP.HasValue ? true : false;
                            gpdContract.EditDate = DateTime.Now;
                            gpdContract.EditorID = currentUseId.Id;
                        }
                    }
                    else
                    {
                        gpdContract.CreatorID = model.CreatorID;
                        gpdContract.DepartmentId = model.DepartmentId;
                        gpdContract.PersonID = model.PersonID;
                        gpdContract.CTID = model.CTID;
                        gpdContract.StatusID = model.StatusID;
                        gpdContract.NumContract = model.NumContract;
                        gpdContract.NameContract = model.NameContract;
                        gpdContract.DateBegin = model.DateBegin.Value;
                        gpdContract.DateEnd = model.DateEnd.Value;
                        //gpdContract.PayeeID = model.PayeeID;
                        //gpdContract.PayerID = model.PayerID;
                        gpdContract.GPDID = model.GPDID;
                        gpdContract.PurposePayment = model.PurposePayment;
                        gpdContract.PaymentPeriodID = model.PaymentPeriodID;
                        gpdContract.Amount = model.Amount;
                        gpdContract.EditDate = DateTime.Now;
                        gpdContract.EditorID = currentUseId.Id;
                        if (model.DateP.HasValue)
                        {
                            gpdContract.DateP = model.DateP.Value;
                            gpdContract.IsLong = true;
                        }
                        else
                            gpdContract.IsLong = false;
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
        /// <summary>
        /// Автозаполнение наборов реквизитов физических лиц
        /// </summary>
        /// <param name="Name">ФИО физического лица</param>
        /// <param name="PersonID">ID физического лица</param>
        /// <returns></returns>
        public IList<GpdContractSurnameDto> GetPersonAutocomplete(string Name, int PersonID)
        {
            return GpdContractDao.GetAutocompletePersons(Name, PersonID);
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
        /// Определяем права роли текущего пользователя.
        /// </summary>
        /// <param name="model"></param>
        public void GetPermission(GpdActListModel model)
        {
            UserRole role = CurrentUser.UserRole;
            model.Permissions = GpdActDao.GetPermission(role);

            //
            if (model.Permissions.Count == 0)
            {
                GpdPermissionDto perm = new GpdPermissionDto();
                perm.IsCancel = false;
                perm.IsComment = false;
                perm.IsCreate = false;
                perm.IsCreateAct = false;
                perm.IsDraft = false;
                perm.IsWrite = false;

                model.Permissions.Add(perm);
            }
        }
        /// <summary>
        /// Определяем права роли текущего пользователя.
        /// </summary>
        /// <param name="model"></param>
        public void GetPermission(GpdActEditModel model)
        {
            UserRole role = CurrentUser.UserRole;
            model.Permissions = GpdActDao.GetPermission(role);

            //
            if (model.Permissions.Count == 0)
            {
                GpdPermissionDto perm = new GpdPermissionDto();
                perm.IsCancel = false;
                perm.IsComment = false;
                perm.IsCreate = false;
                perm.IsCreateAct = false;
                perm.IsDraft = false;
                perm.IsWrite = false;

                model.Permissions.Add(perm);
            }
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
            GetPermission(model);
            UserRole role = CurrentUser.UserRole;
            IList<GpdActDto> document = null;
            //создание нового акта
            if (Id == 0)
                document = GpdActDao.GetNewAct(role, GCID);
            else //редактирование существующего
                document = GpdActDao.GetAct(role, Id, false, model.DateBegin, model.DateEnd, 0, null, 0, null, 0, false);

            if (document.Count > 0)
            {
                foreach (var doc in document)
                {
                    model.Id = doc.Id;
                    model.ActDate = doc.ActDate;
                    model.ActNumber = (Id == 0 ? doc.NumContract.ToString() + "/" + doc.GCCount.ToString() : doc.ActNumber);
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
            GetPermission(model);

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


            if (model.Amount < 0)
                ms.AddModelError("Amount", "Сумма не должна быть меньше нуля!");

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
                        //EditDate = DateTime.Now,
                        //EditorID = currentUseId.Id,
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
                
                AddComment(gpdAct, model);  //добавление комментария
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
            GetPermission(model);
            model.Documents = GpdActDao.GetAct(role, model.Id, model.IsFind, model.DateBegin, model.DateEnd, model.DepartmentId, model.Surname, model.StatusID, model.ActNumber, model.SortBy, model.SortDescending);
            
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

        public static string FormatDateMY(DateTime? date)
        {
            return date.HasValue ? date.Value.Month.ToString() + "." + date.Value.Year.ToString() : string.Empty;
        }
        public static string FormatDateMY(DateTime date)
        {
            return date.Month.ToString() + "." + date.Year.ToString();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    public class StaffDepartmentRequestModel
    {
        #region Реквизиты инициатора
        public DepRequestInfoModel DepRequestInfo { get; set; }
        #endregion

        #region Общие реквизиты
        [Display(Name = "Дата утверждения заявки")]
        public DateTime? DateState { get; set; }

        [Display(Name = "Номер заявки")]
        public int? Id { get; set; }

        [Display(Name = "Вид заявки")]
        public int RequestType { get; set; }
        public IList<IdNameDto> RequestTypes { get; set; }

        [Display(Name = "Id подразделения")]
        public int? DepartmentId { get; set; }

        [Display(Name = "Id родительского подразделения")]
        public int? ParentId { get; set; }

        [Display(Name = "Уровень подразделения")]
        public int? ItemLevel { get; set; }

        [Display(Name = "Назание структурного подразделения")]
        public string Name { get; set; }

        [Display(Name = "Признак БЭК/ФРОНТ")]
        public bool IsBack { get; set; }

        [Display(Name = "Номер приказа")]
        public string OrderNumber { get; set; }

        [Display(Name = "Дата приказа")]
        public DateTime? OrderDate { get; set; }

        [Display(Name = "Юридический адрес")]
        public int? LegalAddressId { get; set; }
        public string LegalAddress { get; set; }

        [Display(Name = "Признак постановки на учет в ИФНС")]
        public bool IsTaxAdminAccount { get; set; }

        [Display(Name = "Признак наличия персонала в подразделении")]
        public bool IsEmployeAvailable { get; set; }

        [Display(Name = "Соседнее подразделение с налоговыми реквизитами")]
        public int? DepNextId { get; set; }
        public string DepNextName { get; set; }
        #endregion

        #region Налоговые реквизиты
        [Display(Name = "КПП")]
        public string KPP { get; set; }

        [Display(Name = "ОКТМО")]
        public string OKTMO { get; set; }

        [Display(Name = "ОКАТО")]
        public string OKATO { get; set; }

        [Display(Name = "Код региона")]
        public string RegionCode { get; set; }

        [Display(Name = "Код налоговой органа")]
        public string TaxAdminCode { get; set; }

        [Display(Name = "Название налогового органа")]
        public string TaxAdminName { get; set; }

        [Display(Name = "Почтовый адрес")]
        public string PostAddress { get; set; }
        #endregion

        #region ЦБ реквизиты
        [Display(Name = "Количество банкоматов всего")]
        public int ATMCountTotal { get; set; }

        [Display(Name = "Количество банкоматов с функцией кэшин")]
        public int ATMCashInCount { get; set; }

        [Display(Name = "Количество банкоматов с функцией ресайклинг")]
        public int ATMCount { get; set; }

        [Display(Name = "Подразделения инкассирующее кэшины")]
        public int DepCachintId { get; set; }

        [Display(Name = "Подразделения инкассирующее банкоматы")]
        public int DepATMId { get; set; }

        [Display(Name = "Дата запуска кэшина (первая)")]
        public DateTime? CashInStartedDate { get; set; }

        [Display(Name = "Дата запуска банкомата (первая)")]
        public DateTime? ATMStartedDate { get; set; }
        #endregion

        #region Управленческие реквизиты
        [Display(Name = "Краткое название подразделения")]
        public string NameShort { get; set; }

        [Display(Name = "Причина внесения в справочник")]
        public string ReferenceReason { get; set; }

        [Display(Name = "Фактический адрес")]
        public int? FactAddressId { get; set; }
        public string FactAddress { get; set; }

        [Display(Name = "Статус подразделения")]
        public string DepStatus { get; set; }

        [Display(Name = "Тип подразделения")]
        public int? DepTypeId { get; set; }
        public IList<IdNameDto> DepTypes { get; set; }

        [Display(Name = "Дата фактического открытия офиса")]
        public DateTime? OpenDate { get; set; }

        [Display(Name = "Дата закрытия офиса")]
        public DateTime? CloseDate { get; set; }

        [Display(Name = "Причина создания/изменения/удаления СП")]
        public string Reason { get; set; }

        [Display(Name = "Режим работы подразделения")]  //возможно потребуется переделать в справочник
        public string OperationMode { get; set; }

        [Display(Name = "Дата начала простоя")]
        public DateTime? BeginIdleDate { get; set; }

        [Display(Name = "Дата конца простоя")]
        public DateTime? EndIdleDate { get; set; }

        [Display(Name = "Признак арендованного помещения")]
        public bool IsRentPlace { get; set; }

        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }

        [Display(Name = "Коды совместимости программ")]
        IList<ProgramCodeDto> ProgramCodes { get; set; }

        [Display(Name = "Признак блокировки подразделения")]
        public bool IsBlocked { get; set; }

        [Display(Name = "Признак сетевого магазина")]
        public bool IsNetShop { get; set; }

        [Display(Name = "Признак наличия кассы")]
        public bool IsAvailableCash { get; set; }

        [Display(Name = "Операции")]
        public IList<DepOperationDto> Operations { get; set; }

        [Display(Name = "Признак обслуживания юридических лиц")]
        public bool IsLegalEntity { get; set; }

        [Display(Name = "Ориентиры")]
        IList<DepLandmarkDto> DepLandmarks { get; set; }

        [Display(Name = "Планируемое количество штатных единиц")]
        public int PlanEPCount { get; set; }

        [Display(Name = "Планируемый ФОТ")]
        public Decimal PlanSalaryFund { get; set; }

        [Display(Name = "Примечание")]
        public string Note { get; set; }
        #endregion
    }
}

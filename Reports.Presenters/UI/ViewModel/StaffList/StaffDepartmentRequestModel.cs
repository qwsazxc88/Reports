using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    /// <summary>
    /// Модель заявок для подразделений.
    /// </summary>
    public class StaffDepartmentRequestModel
    {
        #region Реквизиты инициатора
        /// <summary>
        /// Реквизиты инициатора.
        /// </summary>
        public DepRequestInfoModel DepRequestInfo { get; set; }
        #endregion

        #region Общие реквизиты
        [Display(Name = "Дата заявки")]
        public DateTime? DateRequest { get; set; }

        [Display(Name = "Дата утверждения заявки")]
        public DateTime? DateState { get; set; }

        [Display(Name = "Номер заявки")]
        public int Id { get; set; }

        [Display(Name = "Вид заявки")]
        public int RequestTypeId { get; set; }
        public IList<StaffDepartmentRequestTypes> RequestTypes { get; set; }

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
        //огород для кладра
        //при сохранении адреса в базе мы сохраняем не только строку адреса, но и коды его объектов, для заполнения кладра при вызове модальной формы
        //так как на странице происходит выбор двух разных адресов, а документ и адрес могут быть еще не сохранены в базе, то при повторном вызове модальной формы нужно построить кладр по параметрам
        public string LegalPostIndex { get; set; }
        public string LegalRegionCode { get; set; }
        public string LegalAreaCode { get; set; }
        public string LegalCityCode { get; set; }
        public string LegalSettlementCode { get; set; }
        public string LegalStreetCode { get; set; }
        public int LegalHouseType { get; set; }
        public string LegalHouseNumber { get; set; }
        public int LegalBuildType { get; set; }
        public string LegalBuildNumber { get; set; }
        public int LegalFlatType { get; set; }
        public string LegalFlatNumber { get; set; }

        [Display(Name = "Признак постановки на учет в ИФНС")]
        public bool IsTaxAdminAccount { get; set; }

        [Display(Name = "Признак наличия персонала в подразделении")]
        public bool IsEmployeAvailable { get; set; }

        [Display(Name = "Соседнее подразделение с налоговыми реквизитами")]
        public int DepNextId { get; set; }
        public string DepNextName { get; set; }

        [Display(Name = "Признак плановой операции")]
        public bool IsPlan { get; set; }

        [Display(Name = "Инициатор")]
        public int UserId { get; set; }
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

        [Display(Name = "Количество запущенных банкоматов с функцией кэшин")]
        public int ATMCashInStarted { get; set; }

        [Display(Name = "Количество банкоматов с функцией кэшин")]
        public int ATMCashInCount { get; set; }

        [Display(Name = "Количество банкоматов с функцией ресайклинг")]
        public int ATMCount { get; set; }

        [Display(Name = "Подразделения инкассирующее кэшины")]
        public int DepCachinId { get; set; }
        public string DepCachinName { get; set; }

        [Display(Name = "Подразделения инкассирующее банкоматы")]
        public int DepATMId { get; set; }
        public string DepATMName { get; set; }

        [Display(Name = "Дата запуска кэшина (первая)")]
        public DateTime? CashInStartedDate { get; set; }

        [Display(Name = "Дата запуска банкомата (первая)")]
        public DateTime? ATMStartedDate { get; set; }
        #endregion

        #region Управленческие реквизиты
        [Display(Name = "Id управленческих реквизитов")]
        public int DMDetailId { get; set; }
        //поля с кодами для Финграда есть в таблице, но сюда их пока не вносил
        [Display(Name = "Краткое название подразделения")]
        public string NameShort { get; set; }

        [Display(Name = "Причина внесения в справочник")]
        public int? ReasonId { get; set; }
        public IList<IdNameDto> Reasons { get; set; }

        [Display(Name = "Фактический адрес")]
        public int? FactAddressId { get; set; }
        public string FactAddress { get; set; }
        //огород для кладра
        public string FactPostIndex { get; set; }
        public string FactRegionCode { get; set; }
        public string FactAreaCode { get; set; }
        public string FactCityCode { get; set; }
        public string FactSettlementCode { get; set; }
        public string FactStreetCode { get; set; }
        public int FactHouseType { get; set; }
        public string FactHouseNumber { get; set; }
        public int FactBuildType { get; set; }
        public string FactBuildNumber { get; set; }
        public int FactFlatType { get; set; }
        public string FactFlatNumber { get; set; }

        [Display(Name = "Статус подразделения")]
        public string DepStatus { get; set; }

        [Display(Name = "Тип подразделения")]
        public int? DepTypeId { get; set; }
        public IList<IdNameDto> DepTypes { get; set; }

        [Display(Name = "Дата фактического открытия офиса")]
        public DateTime? OpenDate { get; set; }

        [Display(Name = "Дата закрытия офиса")]
        public DateTime? CloseDate { get; set; }

        [Display(Name = "Режим работы подразделения")]
        public IList<DepOperationModeDto> OperationModes { get; set; }

        [Display(Name = "Примечание")]
        public string OperationMode { get; set; }//к режиму работы подразделения

        [Display(Name = "Дата начала простоя")]
        public DateTime? BeginIdleDate { get; set; }

        [Display(Name = "Дата конца простоя")]
        public DateTime? EndIdleDate { get; set; }

        [Display(Name = "Признак арендованного помещения")]
        public bool IsRentPlace { get; set; }

        [Display(Name = "Реквизиты договора")]
        public string AgreementDetails { get; set; }

        [Display(Name = "Площадь подразделения")]
        public decimal? DivisionArea { get; set; }

        [Display(Name = "Сумма ежемесячного платежа")]
        public decimal? AmountPayment { get; set; }

        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }

        [Display(Name = "Коды совместимости программ")]
        public IList<ProgramCodeDto> ProgramCodes { get; set; }

        [Display(Name = "Признак блокировки подразделения")]
        public bool IsBlocked { get; set; }

        [Display(Name = "Признак сетевого магазина")]
        public int? NetShopId { get; set; }
        public IList<IdNameDto> NetShopTypes { get; set; }

        [Display(Name = "Признак наличия кассы")]
        public bool IsAvailableCash { get; set; }

        [Display(Name = "Операции")]
        public IList<DepOperationDto> Operations { get; set; }

        [Display(Name = "Признак обслуживания юридических лиц")]
        public bool IsLegalEntity { get; set; }

        [Display(Name = "Ориентиры")]
        public IList<DepLandmarkDto> DepLandmarks { get; set; }

        [Display(Name = "Планируемое количество штатных единиц")]
        public int PlanEPCount { get; set; }

        [Display(Name = "Планируемый ФОТ")]
        public Decimal PlanSalaryFund { get; set; }

        [Display(Name = "Примечание")]
        public string Note { get; set; }
        #endregion

        #region Служебные поля
        [Display(Name = "Признак черновика.")]
        public bool IsDraft { get; set; }
        [Display(Name = "Для сообщений")]
        public string MessageStr { get; set; }
        public bool IsDraftButtonAvailable { get; set; }    //доступна кнопка сохранения черновика
        public bool IsAgreeButtonAvailable { get; set; }    //доступна кнопка отправки на согласование
        #endregion
    }
}

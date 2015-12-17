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
        public int DepartmentId { get; set; }

        [Display(Name = "Родительское подразделение")]
        public int ParentId { get; set; }
        public string DepParentName { get; set; }

        [Display(Name = "Уровень подразделения")]
        public int? ItemLevel { get; set; }

        [Display(Name = "Название структурного подразделения")]
        public string Name { get; set; }

        [Display(Name = "Принадлежность подразделения")]
        public int BFGId { get; set; }
        public IList<IdNameDto> Accessoryes { get; set; }

        [Display(Name = "Номер приказа")]
        public string OrderNumber { get; set; }

        [Display(Name = "Дата приказа")]
        public DateTime? OrderDate { get; set; }

        [Display(Name = "Дата вступления приказа в силу")]
        public DateTime? BeginAccountDate { get; set; }

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

        [Display(Name = "Требуется постановка на учет в ИФНС")]
        public bool IsTaxAdminAccount { get; set; }

        [Display(Name = "Осуществлять прием сотрудников в подразделение")]
        public bool IsEmployeAvailable { get; set; }

        [Display(Name = "Подразделение с налоговыми реквизитами:")]
        public int DepNextId { get; set; }
        public string DepNextName { get; set; }

        [Display(Name = "Операция плановая")]
        public bool IsPlan { get; set; }

        [Display(Name = "Инициатор")]
        public int UserId { get; set; }

        [Display(Name = "Признак действующей заявки")]
        public bool IsUsed { get; set; }

        [Display(Name = "Депозитное подразделение:")]
        public int DepDepositId { get; set; }
        public string DepDepositName { get; set; }

        [Display(Name = "Дирекция")]
        public string ManagementName { get; set; }

        [Display(Name = "ID Дирекции")]
        public string ManagementCode { get; set; }

        [Display(Name = "Управление")]
        public string AdminName { get; set; }

        [Display(Name = "ID Управления")]
        public string AdminCode { get; set; }

        [Display(Name = "Бизнес-группа")]
        public string BGName { get; set; }

        [Display(Name = "ID Бизнес-группы")]
        public string BGCode { get; set; }

        [Display(Name = "РП-привязка")]
        public string RPLInkName { get; set; }

        [Display(Name = "ID РП-привязки")]
        public string RPLInkCode { get; set; }

        [Display(Name = "Форма С-09-3-1 отправлена")]
        public bool IsTaxRequest { get; set; }
        #endregion

        #region Налоговые реквизиты
        [Display(Name = "КПП")]
        public string KPP { get; set; }

        [Display(Name = "ОКТМО")]
        public string OKTMO { get; set; }

        [Display(Name = "ОКАТО")]
        public string OKATO { get; set; }

        [Display(Name = "ОКПО")]
        public string OKPO { get; set; }

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

        [Display(Name = "Кол-во запущенных кэшинов")]
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
        [Display(Name = "Сокращенное наименование")]
        public string NameShort { get; set; }

        [Display(Name = "Код подразделения")]
        public string DepCode { get; set; }

        [Display(Name = "Прежний код подразделения")]
        public string PrevDepCode { get; set; }

        [Display(Name = "Причина внесения в справочник")]
        public int? ReasonId { get; set; }
        public IList<IdNameDto> Reasons { get; set; }
        public int ReasonIdOld { get; set; }

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
        public int DepTypeIdOld { get; set; }

        [Display(Name = "Дата фактического открытия офиса")]
        public DateTime? OpenDate { get; set; }

        [Display(Name = "Дата закрытия офиса")]
        public DateTime? CloseDate { get; set; }

        [Display(Name = "Режим работы подразделения")]
        public IList<DepOperationModeDto> OperationModes { get; set; }

        [Display(Name = "Примечание")]
        public string OperationMode { get; set; }//к режиму работы подразделения

        [Display(Name = "Примечание")]
        public string OperationModeCash { get; set; }//к режиму работы кассы

        [Display(Name = "Примечание")]
        public string OperationModeATM { get; set; }//к режиму работы банкомата

        [Display(Name = "Примечание")]
        public string OperationModeCashIn { get; set; }//к режиму работы кэшина

        [Display(Name = "Дата начала простоя")]
        public DateTime? BeginIdleDate { get; set; }

        [Display(Name = "Дата конца простоя")]
        public DateTime? EndIdleDate { get; set; }

        [Display(Name = "Арендованное помещение")]
        public int? RentPlaceId { get; set; }
        public IList<IdNameDto> RentPlace { get; set; }

        [Display(Name = "Реквизиты договора")]
        public string AgreementDetails { get; set; }

        [Display(Name = "Площадь подразделения")]
        public decimal? DivisionArea { get; set; }

        [Display(Name = "Сумма ежемесячного платежа")]
        public decimal? AmountPayment { get; set; }

        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }

        [Display(Name = "Коды совместимых программ")]
        public IList<ProgramCodeDto> ProgramCodes { get; set; }

        [Display(Name = "Установленное ПО")]
        public int SoftGroupId { get; set; }
        public IList<StaffDepartmentSoftGroupDto> SoftGroups { get; set; }

        [Display(Name = "Наличие блокировки подразделения")]
        public bool IsBlocked { get; set; }

        [Display(Name = "Идентификация сетевого магазина")]
        public int? NetShopId { get; set; }
        public IList<IdNameDto> NetShopTypes { get; set; }

        [Display(Name = "Операции")]
        public int OperGroupId { get; set; }
        public IList<StaffDepartmentOperationGroupsDto> OperationGroups { get; set; }

        [Display(Name = "Обслуживание юридических лиц")]
        public bool IsLegalEntity { get; set; }

        [Display(Name = "Ориентиры")]
        public IList<DepLandmarkDto> DepLandmarks { get; set; }

        [Display(Name = "Планируемое количество штатных единиц")]
        public int PlanEPCount { get; set; }

        [Display(Name = "Планируемый ФОТ")]
        public Decimal PlanSalaryFund { get; set; }

        [Display(Name = "Примечание")]
        public string Note { get; set; }

        [Display(Name = "Наличие кассы")]
        public int? CDAvailableId { get; set; }
        public IList<IdNameDto> CashDeskAvailables { get; set; }

        [Display(Name = "СКБ/GE")]
        public int? SKB_GE_Id { get; set; }
        public IList<IdNameDto> SKB_GE { get; set; }
        #endregion

        #region Согласование
        [Display(Name = "№ задачи")]
        public string PyrusNumber { get; set; }

        [Display(Name = "Инициатор")]
        public bool IsInitiatorApprove { get; set; }    //1
        public bool IsInitiatorApproveAvailable { get; set; }
        [Display(Name = "ФИО инициатора")]
        public string InitiatorApproveName { get; set; }
        [Display(Name = "За")]
        public int InitiatorId { get; set; }
        public IList<IdNameDto> Initiators { get; set; }    //список инициаторов для куратора/кадровика банка
        public string InitiatorPyrusRef { get; set; }
        public string InitiatorPyrusName { get; set; }


        //кураторы и кадровки банка имеют одинаковые права
        [Display(Name = "Куратор")]
        public bool IsCuratorApprove { get; set; }    //2
        public bool IsCurator { get; set; }  //2
        public bool IsCuratorApproveAvailable { get; set; }
        [Display(Name = "ФИО куратора")]
        public string CuratorApproveName { get; set; }
        public string CuratorPyrusRef { get; set; }
        public string CuratorPyrusName { get; set; }


        [Display(Name = "Кадровик банка")]
        public bool IsPersonnelBankApprove { get; set; }  //3
        public bool IsPersonnelBank { get; set; }  //3
        public bool IsPersonnelBankApproveAvailable { get; set; }
        [Display(Name = "ФИО кадровика")]
        public string PersonnelBankApproveName { get; set; }
        public string PersonnelBankPyrusRef { get; set; }
        public string PersonnelBankPyrusName { get; set; }


        [Display(Name = "Консультант РК")]
        public bool IsConsultant { get; set; }  //


        [Display(Name = "Налоговик")]
        public bool IsTaxCollectorApprove { get; set; }   //4
        public bool IsTaxCollectorApproveAvailable { get; set; }    //кроме согласования налоговик должен указать подразделение с реквизитами
        [Display(Name = "ФИО налоговика")]
        public string TaxCollectorApproveName { get; set; }
        public bool IsTaxCollector { get; set; }            //налоговик, для него делаем доступ к выбору подразделения с налоговыми реквизитами и делаем проверку н наличие такового при согласовании

        

        [Display(Name = "Вышестоящий руководитель 3 ур.")]
        public bool IsTopManagerApprove { get; set; }   //5
        public bool IsTopManagerApproveAvailable { get; set; }
        [Display(Name = "ФИО вышестоящего руководителя")]
        public string TopManagerApproveName { get; set; }
        [Display(Name = "За")]
        public int TopManagerId { get; set; }
        public IList<IdNameDto> TopManagers { get; set; }    //список вышестоящих руководителей 3 уровня для куратора/кадровика банка
        public string TopManagerPyrusRef { get; set; }
        public string TopManagerPyrusName { get; set; }


        [Display(Name = "Член правления")]
        public bool IsBoardMemberApprove { get; set; }   //6
        public bool IsBoardMemberApproveAvailable { get; set; }
        [Display(Name = "ФИО члена правления")]
        public string BoardMemberApproveName { get; set; }
        [Display(Name = "За")]
        public int BoardMemberId { get; set; }
        public IList<IdNameDto> BoardMembers { get; set; }    //список членов правления для куратора/кадровика банка
        public string BoardMemberPyrusRef { get; set; }
        public string BoardMemberPyrusName { get; set; }

        //не участвует в согласовании, показывем только отметку
        [Display(Name = "Ответственный по приказам")]
        public bool IsOrderApprove { get; set; }   //7
        public bool IsOrderApproveAvailable { get; set; }
        [Display(Name = "ФИО ответственного по приказам")]
        public string OrderApproveName { get; set; }
        public bool IsOrder { get; set; }

        //не участвует в согласовании, сохраняет тольк коды
        [Display(Name = "Администратор ПО банка")]
        public bool IsSoftAdminApprove { get; set; }   //
        public bool IsSoftAdminApproveAvailable { get; set; }
        public bool IsSoftAdmin { get; set; }            

        #endregion

        #region Служебные поля
        [Display(Name = "Признак черновика.")]
        public bool IsDraft { get; set; }
        [Display(Name = "Для сообщений")]
        public string MessageStr { get; set; }
        public bool IsDraftButtonAvailable { get; set; }    //доступна кнопка сохранения черновика
        public bool IsAgreeButtonAvailable { get; set; }    //доступна кнопка отправки на согласование
        public bool IsImportance { get; set; }  //признак важности согласования

        
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Domain;
using System.Web.Mvc;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    /// <summary>
    /// Модель для штатных единиц.
    /// </summary>
    public class StaffEstablishedPostRequestModel
    {
        [Display(Name = "Дата заявки")]
        public DateTime? DateRequest { get; set; }

        [Display(Name = "Номер заявки")]
        public int Id { get; set; }

        [Display(Name = "Инициатор")]
        public string RequestInitiator { get; set; } //фио + должность
        public int UserId { get; set; }

        [Display(Name = "Id штатной единицы")]
        public int SEPId { get; set; }

        [Display(Name = "Вид заявки")]
        public int RequestTypeId { get; set; }
        public IList<StaffEstablishedPostRequestTypes> RequestTypes { get; set; }

        [Display(Name = "График работы")]
        public int ScheduleId { get; set; }
        public IEnumerable<SelectListItem> Schedules { get; set; }

        [Display(Name = "Условия труда")]
        public int WCId { get; set; }
        public IEnumerable<SelectListItem> WorkConditions { get; set; }

        [Display(Name = "Назание структурного подразделения")]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        [Display(Name = "Принадлежность подразделения")]
        public string AccessoryName { get; set; }

        [Display(Name = "Юридический адрес")]
        public string LegalAddress { get; set; }

        [Display(Name = "Признак постановки на учет в ИФНС")]
        public bool IsTaxAdminAccount { get; set; }

        [Display(Name = "Признак наличия персонала в подразделении")]
        public bool IsEmployeAvailable { get; set; }

        [Display(Name = "Должность")]
        public int PositionId { get; set; }
        public string PositionName { get; set; }

        [Display(Name = "Количество")]
        public int Quantity { get; set; }
        public int QuantityOld { get; set; }//для проверки в заявке на изменение

        [Display(Name = "Оклад")]
        public decimal Salary { get; set; }

        [Display(Name = "Причина создания ШЕ")]
        public int? ReasonId { get; set; }
        public IList<AppointmentReason> Reasons { get; set; }

        [Display(Name = "Надбавки")]
        public IList<StaffEstablishedPostChargeLinksDto> PostChargeLinks { get; set; }

        [Display(Name = "Действия к надбавкам")]
        public IList<IdNameDto> PostChargeActions { get; set; }

        [Display(Name = "Дата начала учета в системе")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BeginAccountDate { get; set; }

        [Display(Name = "Данные по ШЕ")]
        public string EPInfo { get; set; }

        #region Согласование
        [Display(Name = "Инициатор")]
        public bool IsInitiatorApprove { get; set; }    //1
        public bool IsInitiatorApproveAvailable { get; set; }

        [Display(Name = "ФИО инициатора")]
        public string InitiatorApproveName { get; set; }

        [Display(Name = "За")]
        public int InitiatorId { get; set; }
        public IList<IdNameDto> Initiators { get; set; }    //список инициаторов для куратора/кадровика банка


        //кураторы и кадровки банка имеют одинаковые права
        [Display(Name = "Куратор")]
        public bool IsCuratorApprove { get; set; }    //2
        public bool IsCurator { get; set; }  //2
        public bool IsCuratorApproveAvailable { get; set; }

        [Display(Name = "ФИО куратора")]
        public string CuratorApproveName { get; set; }


        [Display(Name = "Кадровик банка")]
        public bool IsPersonnelBankApprove { get; set; }  //3
        public bool IsPersonnelBank { get; set; }  //3
        public bool IsPersonnelBankApproveAvailable { get; set; }

        [Display(Name = "ФИО кадровика")]
        public string PersonnelBankApproveName { get; set; }

        [Display(Name = "Консультант РК")]
        public bool IsConsultant { get; set; }  //



        [Display(Name = "Вышестоящий руководитель 3 ур.")]
        public bool IsTopManagerApprove { get; set; }   //4
        public bool IsTopManagerApproveAvailable { get; set; }

        [Display(Name = "ФИО вышестоящего руководителя")]
        public string TopManagerApproveName { get; set; }

        [Display(Name = "За")]
        public int TopManagerId { get; set; }
        public IList<IdNameDto> TopManagers { get; set; }    //список вышестоящих руководителей 3 уровня для куратора/кадровика банка



        [Display(Name = "Член правления")]
        public bool IsBoardMemberApprove { get; set; }   //5
        public bool IsBoardMemberApproveAvailable { get; set; }

        [Display(Name = "ФИО члена правления")]
        public string BoardMemberApproveName { get; set; }

        [Display(Name = "За")]
        public int BoardMemberId { get; set; }
        public IList<IdNameDto> BoardMembers { get; set; }    //список членов правления для куратора/кадровика банка

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

        #region Служебные переменные
        //public bool IsNew { get; set; } //признак новой заявки
        public bool IsUsed { get; set; }    //признак использования
        public bool IsDraft { get; set; }   //черновик
        public string MessageStr { get; set; }  //для сообщений
        public bool IsDraftButtonAvailable { get; set; }    //доступна кнопка сохранения черновика
        public bool IsAgreeButtonAvailable { get; set; }    //доступна кнопка отправки на согласование
        #endregion
    }
}

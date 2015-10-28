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

        [Display(Name = "Оклад")]
        public decimal Salary { get; set; }

        [Display(Name = "Причина создания ШЕ")]
        public int? ReasonId { get; set; }
        public IList<AppointmentReason> Reasons { get; set; }

        [Display(Name = "Надбавки")]
        public IList<StaffEstablishedPostChargeLinksDto> PostChargeLinks { get; set; }

        [Display(Name = "Дата начала учета в системе")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BeginAccountDate { get; set; }

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
        public bool IsNew { get; set; } //признак новой заявки
        public bool IsUsed { get; set; }    //признак использования
        public bool IsDraft { get; set; }   //черновик
        public string MessageStr { get; set; }  //для сообщений
        public bool IsDraftButtonAvailable { get; set; }    //доступна кнопка сохранения черновика
        public bool IsAgreeButtonAvailable { get; set; }    //доступна кнопка отправки на согласование
        #endregion
    }
}

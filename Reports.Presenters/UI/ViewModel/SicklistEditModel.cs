using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    
    public interface IContainId
    {
        int Id { get; set; }
        int UserId { get; set; }
    }
    public class SicklistEditModel : UserInfoModel, ICheckBoxes, IAttachment, ICheckForEntity,IContainId
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public int UserId { get; set; }


        [Display(Name = "Вид больничного")]
        public int TypeId { get; set; }
        public int TypeIdHidden { get; set; }
        public IList<IdNameDto> Types;
        public bool IsTypeEditable { get; set; }
        public int SicklistTypeIdBabyMindingHidden { get; set; }

        public bool IsDatesEditable { get; set; }

        [Display(Name = "В случае ухода за ребенком")]
        public int? BabyMindingTypeId { get; set; }
        public int? BabyMindingTypeIdHidden { get; set; }
        public IList<IdNameDto> BabyMindingTypes;
        public bool IsBabyMindingTypeEditable { get; set; }

        [Display(Name = "Дата начала")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}",ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "SicklistEditModel_BeginDate_Required",
         ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("SicklistEditModel_BeginDate_Required", typeof(Resources))]
        public DateTime? BeginDate { get; set; }
        [Display(Name = "Дата окончания")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "SicklistEditModel_EndDate_Required",
        ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("SicklistEditModel_EndDate_Required", typeof(Resources))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Номер больничного листа")]
        [Required(ErrorMessageResourceName = "SicklistEditModel_SicklistNumber_Required",
         ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("SicklistEditModel_SicklistNumber_Required", typeof(Resources))]
        public string SicklistNumber { get; set; }


        public bool IsPersonnelFieldsEditable { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Назначить с даты")]
        public DateTime? PaymentBeginDate { get; set; }

        [Display(Name = "Количество календарных дней")]
        public int? DaysCount { get; set; }
        public int? DaysCountHidden { get; set; }


        [Display(Name = "Стаж")]
        public string ExperienceYears { get; set; }
        public string ExperienceMonthes { get; set; }

        [Display(Name = "Процент оплаты заработка")]
        public int PaymentPercentTypeId { get; set; }
        public int PaymentPercentTypeIdHidden { get; set; }
        public IList<IdNameDtoSort> PaymentPercentTypes;

        

        [Display(Name = "Ограничение заработка (пособия)")]
        public int PaymentRestrictTypeId { get; set; }
        public int PaymentRestrictTypeIdHidden { get; set; }
        public IList<IdNameDto> PaymentRestrictTypes;
        //public bool IsPaymentRestrictTypeEditable { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Снизить пособие за нарушение режима с")]
        public DateTime? PaymentDecreaseDate { get; set; }

        [Display(Name = "Учитывать заработок предыдущих страхователей")]
        public bool IsPreviousPaymentCounted { get; set; }
        public bool IsPreviousPaymentCountedHidden { get; set; }

        [Display(Name = "Больничный имеет продолжение")]
        public bool IsContinued { get; set; }
        public bool IsContinuedHidden { get; set; }

        //[Display(Name = "Рассчитывать заработок по правилам 2010 года")]
        //public bool Is2010Calculate { get; set; }
        //public bool Is2010CalculateHidden { get; set; }

        [Display(Name = "Доплачивать до полного среднего заработка")]
        public bool IsAddToFullPayment { get; set; }
        public bool IsAddToFullPaymentHidden { get; set; }


       [Display(Name = "Заполнение табеля")]
        public int TimesheetStatusId { get; set; }
        public int TimesheetStatusIdHidden { get; set; }
        public IList<IdNameDto> TimesheetStatuses;
        public bool IsTimesheetStatusEditable { get; set; }


        [Display(Name = "Сотрудник cогласен ")]
        public bool IsApprovedByUser { get; set; }
        public bool IsApprovedByUserHidden { get; set; }
        public bool IsApprovedByUserEnable { get; set; }
        //public bool IsApprovedByUserChecked { get; set; }
        [Display(Name = "Руководитель cогласен ")]
        public bool IsApprovedByManager { get; set; }
        public bool IsApprovedByManagerHidden { get; set; }
        public bool IsApprovedByManagerEnable { get; set; }
        public string ApprovedByManager { get; set; }
        [Display(Name = "Кадровик cогласен ")]
        public bool IsApprovedByPersonnelManager { get; set; }
        public bool IsApprovedByPersonnelManagerHidden { get; set; }
        public bool IsApprovedByPersonnelManagerEnable { get; set; }
        public string ApprovedByPersonnelManager { get; set; }
        [Display(Name = "Выгружен в 1C")]
        public bool IsPostedTo1C { get; set; }
        public bool IsPostedTo1CHidden { get; set; }
        public bool IsPostedTo1CEnable { get; set; }

        [Display(Name = "Автор")]
        public string CreatorLogin { get; set; }

        [Display(Name = "Скан документа")]
        public string Attachment { get; set; }
        public int AttachmentId { get; set; }
        //public int AttachmentTypeId { get; set; }

        public bool IsSaveAvailable { get; set; }
        public bool IsDeleteAvailable { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDeleted { get; set; }
        public RequestCommentsModel CommentsModel { get; set; }

        public bool ReloadPage { get; set; }

        public bool IsApproved { get; set; }
        public bool IsApprovedEnable { get; set; }

        public bool IsApprovedForAll { get; set; }
        public bool IsApprovedForAllEnable { get; set; }

        public bool? ExperienceIn1C { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Reports.Presenters.UI.ViewModel
{
    public class ChildVacationEditModel : UserInfoModel, ICheckBoxes, IAttachment, IOrderScanAttachment, ICheckForEntityBeginDate, IContainId
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public int UserId { get; set; }


        //[Display(Name = "Вид отпуска")]
        //public int VacationTypeId { get; set; }
        //public int VacationTypeIdHidden { get; set; }
        //public IList<IdNameDto> VacationTypes;
        //public bool IsVacationTypeEditable { get; set; }
        [Display(Name = "Дата начала")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "VacationEditModel_BeginDate_Required",
         ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("VacationEditModel_BeginDate_Required", typeof(Resources))]
        //[DataType("DateTimeDto")]
        public DateTime? BeginDate { get; set; }
        [Display(Name = "Дата окончания")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "VacationEditModel_EndDate_Required",
        ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("VacationEditModel_EndDate_Required", typeof(Resources))]
        public DateTime? EndDate { get; set; }
        public bool IsVacationDatesEditable { get; set; }

        public bool IsPersonnelFieldsEditable { get; set; }

        [Display(Name = "Освободить на период отпуска ставку в штатном расписании")]
        public bool IsFreeRate { get; set; }
        public bool IsFreeRateHidden { get; set; }
        //public bool IsFreeRateHidden { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Выплачивать по")]
        public DateTime? PaidToDate { get; set; }

        [Display(Name = "Учитывать заработок предыдущих страхователей")]
        public bool IsPreviousPaymentCounted { get; set; }
        public bool IsPreviousPaymentCountedHidden { get; set; }

        [Display(Name = "Количество детей")]
        public string ChildrenCount { get; set; }

        [Display(Name = "Среди детей есть первый ребенок")]
        public bool IsFirstChild { get; set; }
        public bool IsFirstChildHidden { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Выплачивать по")]
        public DateTime? PaidToDate1 { get; set; }
        

        //[Display(Name = "Длительность отпуска (календарных дней)")]
        //public int? DaysCount { get; set; }
        //public int? DaysCountHidden { get; set; }

        //[Display(Name = "Заполнение табеля")]
        //public int TimesheetStatusId { get; set; }
        //public int TimesheetStatusIdHidden { get; set; }
        //public IList<IdNameDto> TimesheetStatuses;
        //public bool IsTimesheetStatusEditable { get; set; }

        [Display(Name = "Сотрудник cогласен")]
        public bool IsApprovedByUser { get; set; }
        public bool IsApprovedByUserHidden { get; set; }
        public bool IsApprovedByUserEnable { get; set; }
        //public bool IsApprovedByUserChecked { get; set; }
        [Display(Name = "Руководитель cогласен")]
        public bool IsApprovedByManager { get; set; }
        public bool IsApprovedByManagerHidden { get; set; }
        public bool IsApprovedByManagerEnable { get; set; }
        [Display(Name = "Кадровик cогласен")]
        public bool IsApprovedByPersonnelManager { get; set; }
        public bool IsApprovedByPersonnelManagerHidden { get; set; }
        public bool IsApprovedByPersonnelManagerEnable { get; set; }
        [Display(Name = "Выгружен в 1C")]
        public bool IsPostedTo1C { get; set; }
        public bool IsPostedTo1CHidden { get; set; }
        public bool IsPostedTo1CEnable { get; set; }

        // Прикрепление скана подтверждающего документа
        public bool IsConfirmationAllowed { get; set; }

        [Display(Name = "Скан документа")]
        public string Attachment { get; set; }
        public int AttachmentId { get; set; }

        [Display(Name = "Скан подписанного приказа")]
        public string OrderScanAttachment { get; set; }
        public int OrderScanAttachmentId { get; set; }

        [Display(Name = "Автор")]
        public string CreatorLogin { get; set; }

        public bool IsSaveAvailable { get; set; }
        public bool IsDeleteAvailable { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPrintAvailable { get; set; }
        public RequestCommentsModel CommentsModel { get; set; }

        public bool ReloadPage { get; set; }

        public bool IsApproved { get; set; }
        public bool IsApprovedEnable { get; set; }

        public bool IsApprovedForAll { get; set; }
        public bool IsApprovedForAllEnable { get; set; }

        public bool IsApproveForAllByConsultant { get; set; }//для консультанта, чтобы мог согласовать за всех
        public bool IsApproveForAllByConsultantEnable { get; set; }//для консультанта, чтобы мог согласовать за всех
    }
}

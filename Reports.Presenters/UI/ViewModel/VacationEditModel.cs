﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class VacationEditModel : UserInfoModel, ICheckBoxes, IAttachment, IOrderScanAttachment, IUnsignedOrderScanAttachment, ICheckForEntityBeginDate, IContainId
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public int UserId { get; set; }


        [Display(Name = "Вид отпуска")]
        public int VacationTypeId { get; set; }
        public int VacationTypeIdHidden { get; set; }
        public IList<IdNameDto> VacationTypes;
        public bool IsVacationTypeEditable { get; set; }
        /*
        [Display(Name = "Включить дополнительный отпуск")]
        public bool IsAdditionalVacationPresent { get; set; }
        */
        [Display(Name = "Вид дополнительного отпуска")]
        public int AdditionalVacationTypeId { get; set; }
        public int AdditionalVacationTypeIdHidden { get; set; }
        public IList<IdNameDto> AdditionalVacationTypes;
        public bool IsAdditionalVacationTypeEditable { get; set; }

        [Display(Name = "Остаток дней основного отпуска")]
        public decimal? PrincipalVacationDaysLeft { get; set; }
        [Display(Name = "Остаток дней дополнительного отпуска")]
        public decimal? AdditionalVacationDaysLeft { get; set; }
        public bool IsDaysLeftEditable { get; set; }

        [Display(Name = "Дата начала отпуска")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}",ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "VacationEditModel_BeginDate_Required",
         ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("VacationEditModel_BeginDate_Required", typeof(Resources))]
        //[DataType("DateTimeDto")]
        public DateTime? BeginDate { get; set; }

        [Display(Name = "Дата окончания отпуска")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "VacationEditModel_EndDate_Required",
        ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("VacationEditModel_EndDate_Required", typeof(Resources))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Дата начала дополнительного отпуска")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AdditionalVacationBeginDate { get; set; }

        [Display(Name = "Длительность отпуска (календарных дней)")]
        public int? DaysCount { get; set; }
        public int? DaysCountHidden { get; set; }

        [Display(Name = "Длительность дополнительного отпуска (календарных дней)")]
        public int? AdditionalVacationDaysCount { get; set; }
        public int? AdditionalVacationDaysCountHidden { get; set; }

        [Display(Name = "Заполнение табеля")]
        public int TimesheetStatusId { get; set; }
        public int TimesheetStatusIdHidden { get; set; }
        public IList<IdNameDto> TimesheetStatuses;
        public bool IsTimesheetStatusEditable { get; set; }

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

        // Прикрепление скана подтверждающего документа на подпись
        public bool IsUnsignedConfirmationAllowed { get; set; }
        // Прикрепление скана подтверждающего документа
        public bool IsConfirmationAllowed { get; set; }

        [Display(Name = "Скан документа")]
        public string Attachment { get; set; }
        public int AttachmentId { get; set; }

        [Display(Name = "Скан приказа на подпись")]
        public string UnsignedOrderScanAttachment { get; set; }
        public int UnsignedOrderScanAttachmentId { get; set; }

        [Display(Name = "Скан подписанного приказа")]
        public string OrderScanAttachment { get; set; }
        public int OrderScanAttachmentId { get; set; }

        [Display(Name = "Автор")]
        public string CreatorLogin { get; set; }

        // Сообщение пользователю об ошибках
        public bool IsErrorNotificationAvailable { get; set; }

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

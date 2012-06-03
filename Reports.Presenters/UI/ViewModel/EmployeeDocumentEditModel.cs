using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class EmployeeDocumentEditModel : UserModel, IEmailDtoSupport
    {
        public int DocumentId { get; set; }
        public int Version { get; set; }
        [Display(Name = "Тип")]
        public int DocumentTypeId { get; set; }
        public IList<IdNameDto> DocumentTypes;
        public int DocumentSubTypeId { get; set; }
        public IList<IdNameDto> DocumentSubTypes;
        [Display(Name = "Дата приема")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        //[Required]
        [Required(ErrorMessageResourceName = "EmployeeDocumentEditModel_Comment_Required",
        ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("EmployeeDocumentEditModel_Comment_Required", typeof(Resources))]
        [Display(Name = "Текст")]
        public string EditComment { get; set; }
        [Display(Name = "Скан документа")]
        public string Attachment { get; set; }
        public int AttachmentId { get; set; }

        [Display(Name = "Согласен Руководитель")]
        public bool IsApprovedByManager { get; set; }
        public bool IsApprovedByManagerHidden { get; set; }
        [Display(Name = "Согласен Кадровик")]
        public bool IsApprovedByPersonnelManager { get; set; }
        public bool IsApprovedByPersonnelManagerHidden { get; set; }
        [Display(Name = "Согласен Бюджет")]
        public bool IsApprovedByBudgetManager { get; set; }
        public bool IsApprovedByBudgetManagerHidden { get; set; }
        [Display(Name = "Обработан")]
        public bool IsApprovedByOutsorsingManager { get; set; }
        public bool IsApprovedByOutsorsingManagerHidden { get; set; }

        public bool IsEditable { get; set; }
        public bool IsSaveAvailable { get; set; }
        public bool IsApprovedByManagerEnable { get; set; }
        public bool IsApprovedByPersonnelManagerEnable { get; set; }
        public bool IsApprovedByBudgetManagerEnable { get; set; }
        public bool IsApprovedByOutsorsingManagerEnable { get; set; }
        public bool SendEmailToBilling { get; set; }
        public bool IsSendToBillingAvailable { get; set; }

        public bool ReloadPage { get; set; }

        public DocumentCommentsModel CommentsModel { get; set; }

        public EmailDto EmailDto { get; set; }
        //public byte[] FileContext { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class DismissalEditModel : UserInfoModel,
        ICheckBoxes,
        IAttachment,
        IOrderScanAttachment,
        IUnsignedOrderScanAttachment,
        IT2ScanAttachment,
        IUnsignedT2ScanAttachment,
        IDismissalAgreementScanAttachment,
        IUnsignedDismissalAgreementScanAttachment,
        IF182NScanAttachment,
        IF2NDFLScanAttachment,
        IWorkbookRequestScanAttachment,
        ICheckForEntityEndDate,
        IContainId
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public int UserId { get; set; }


        [Display(Name = "Основание ст. ТК РФ")]
        public int TypeId { get; set; }
        public int TypeIdHidden { get; set; }
        public IList<IdNameDto> Types;
        public bool IsTypeEditable { get; set; }
       
        [Display(Name = "Дата окончания Т. Д.")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "DismissalEditModel_EndDate_Required",
        ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("DismissalEditModel_EndDate_Required", typeof(Resources))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Кол-во дней компенсации")]
        public string Compensation { get; set; }
        [Display(Name = "Кол-во дней удержания")]
        public string Reduction { get; set; }
        public bool IsPersonnelFieldsEditable { get; set; }

        /*[Required(ErrorMessageResourceName = "DismissalEditModel_Reason_Required",
        ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("DismissalEditModel_Reason_Required", typeof(Resources))]*/
        [Display(Name = "Основание документ")]
        public string Reason { get; set; }
        
        /*[Display(Name = "Заполнение табеля")]
        public int StatusId { get; set; }
        public int StatusIdHidden { get; set; }
        public IList<IdNameDto> Statuses;*/
        //public bool IsStatusEditable { get; set; }

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

        // Прикрепление скана Т2 на подпись
        public bool IsUnsignedT2Allowed { get; set; }
        // Прикрепление скана Т2
        public bool IsT2Allowed { get; set; }

        // Просмотр скана соглашения
        public bool IsViewDismissalAgreementAllowed { get; set; }
        // Прикрепление скана соглашения на подпись
        public bool IsUnsignedDismissalAgreementAllowed { get; set; }
        // Прикрепление скана соглашения
        public bool IsDismissalAgreementAllowed { get; set; }

        // Прикрепление скана заявления на выдачу ТК
        public bool IsWorkbookRequestAllowed { get; set; }

        // Просмотр скана 182-Н
        public bool IsViewF182NAllowed { get; set; }
        // Прикрепление скана 182-Н
        public bool IsF182NAllowed { get; set; }

        // Просмотр скана 2-НДФЛ
        public bool IsViewF2NDFLAllowed { get; set; }
        // Прикрепление скана 2-НДФЛ
        public bool IsF2NDFLAllowed { get; set; }

        [Display(Name = "Автор")]
        public string CreatorLogin { get; set; }

        [Display(Name = "Скан заявления")]
        public string Attachment { get; set; }
        public int AttachmentId { get; set; }

        [Display(Name = "Скан приказа на подпись")]
        public string UnsignedOrderScanAttachment { get; set; }
        public int UnsignedOrderScanAttachmentId { get; set; }

        [Display(Name = "Скан подписанного приказа")]
        public string OrderScanAttachment { get; set; }
        public int OrderScanAttachmentId { get; set; }

        [Display(Name = "Скан Т2 на подпись")]
        public string UnsignedT2ScanAttachment { get; set; }
        public int UnsignedT2ScanAttachmentId { get; set; }

        [Display(Name = "Скан Т2")]
        public string T2ScanAttachment { get; set; }
        public int T2ScanAttachmentId { get; set; }
        
        [Display(Name = "Скан соглашения на подпись")]
        public string UnsignedDismissalAgreementScanAttachment { get; set; }
        public int UnsignedDismissalAgreementScanAttachmentId { get; set; }

        [Display(Name = "Скан соглашения")]
        public string DismissalAgreementScanAttachment { get; set; }
        public int DismissalAgreementScanAttachmentId { get; set; }
        
        [Display(Name = "Скан 182-Н")]
        public string F182NScanAttachment { get; set; }
        public int F182NScanAttachmentId { get; set; }

        [Display(Name = "Скан 2-НДФЛ")]
        public string F2NDFLScanAttachment { get; set; }
        public int F2NDFLScanAttachmentId { get; set; }
                
        [Display(Name = "Скан заявления на выдачу трудовой книжки")]
        public string WorkbookRequestScanAttachment { get; set; }
        public int WorkbookRequestScanAttachmentId { get; set; }

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
    }
}

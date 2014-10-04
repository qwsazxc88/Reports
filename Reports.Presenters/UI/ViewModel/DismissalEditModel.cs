using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class DismissalEditModel : UserInfoModel, ICheckBoxes, IAttachment, IOrderScanAttachment, IUnsignedOrderScanAttachment, ICheckForEntityEndDate, IContainId
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

        [Display(Name = "Автор")]
        public string CreatorLogin { get; set; }

        [Display(Name = "Скан документа")]
        public string Attachment { get; set; }
        public int AttachmentId { get; set; }

        [Display(Name = "Скан приказа на подпись")]
        public string UnsignedOrderScanAttachment { get; set; }
        public int UnsignedOrderScanAttachmentId { get; set; }

        [Display(Name = "Скан подписанного приказа")]
        public string OrderScanAttachment { get; set; }
        public int OrderScanAttachmentId { get; set; }

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

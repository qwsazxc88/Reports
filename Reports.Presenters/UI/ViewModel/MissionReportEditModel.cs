using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel
{
    public class MissionReportEditModel : UserInfoModel, IContainId
    {
        public int Version { get; set; }
        #region IContainId Members

        public int Id { get; set; }
        public int UserId { get; set; }

        #endregion

        public string DocumentTitle { get; set; }
        [Display(Name = "Даты командировки из приказа")]
        public string OrderDates { get; set; }
        [Display(Name = "Даты командировки из изменения к приказу")]
        public string AdditionalOrderDates { get; set; }
        public string Costs { get; set; }

        [Display(Name = "Справочно.Проживал в гостинице")]
        public string Hotels { get; set; }


        //[Display(Name = "")]
        public string UserFio { get; set; }
        public bool IsUserApproved { get; set; }
        public bool IsUserApprovedAvailable { get; set; }
        public bool IsUserApprovedHidden { get; set; }
        public bool IsAttachmentsInvalid { get; set; }

        [Display(Name = "Задание сотрудника выполнено, производственные расходы согласованы")]
        public bool IsManagerApproved { get; set; }
        public bool IsManagerApproveAvailable { get; set; }
        public bool IsManagerApprovedHidden { get; set; }
        public bool IsManagerRejectAvailable { get; set; }
        public bool IsManagerReject { get; set; }
        public string ManagerFio { get; set; }

        [Display(Name = "Авансовый отчет проверен")]
        public bool IsAccountantApproved { get; set; }
        public bool IsAccountantApproveAvailable { get; set; }
        public bool IsAccountantApprovedHidden { get; set; }
        public bool IsAccountantRejectAvailable { get; set; }
        public bool IsAccountantReject { get; set; }
        public bool IsAccountantApprovedCancel { get; set; }//для отмены согласования бухгалтера
        public bool IsSend1C { get; set; }
        public bool IsSurchargeAvailable { get; set; }
        [Display(Name = "Бухгалтер")]
        public string AccountantFio { get; set; }
       
        //public string AccountantEmail { get; set; }

        public bool IsEditable { get; set; }
        public bool IsSaveAvailable { get; set; }

        public bool IsAccountantEditable { get; set; }
        public bool IsDeleted { get; set; }

        public bool IsDocumentsSaveToArchiveAvailable { get; set; }
        public bool IsPrintArchivistAddressAvailable { get; set; }

        public bool IsCreateAdditionalOrderAvailable { get; set; }

        public bool IsUserDismissal { get; set; }
        public int DeductionDocNumber { get; set; }
        [Display(Name = "Дата отправки в архив")]
        public string ArchiveDate { get; set; }
        [Display(Name = "Номер коробки (полки, места) в архиве")]
        public string ArchiveNumber { get; set; }
        [Display(Name = "ФИО архивариуса")]
        public string ArchivistFio { get; set; }
        public bool IsArchivistEditable { get; set; }

        public bool ReloadPage { get; set; }

        public RequestCommentsModel CommentsModel { get; set; }
        public RequestAttachmentsModel AttachmentsModel { get; set; }
    }
}
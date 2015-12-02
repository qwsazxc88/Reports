using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;
using System.Web;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class ScanOriginalDocumentsModel : AbstractEmploymentModel
    {
        //флажок для сотрудников Экспресс-Волги
        public bool IsVolga { get; set; }
        //состояние кандидата
        //public IList<CandidateStateDto> CandidateState { get; set; }
        public CandidateStateModel CandidateStateModel { get; set; }

        public string ErrorMessage { get; set; }
        [Display(Name = "Отправить на согласование")]
        public bool IsAgree { get; set; }
        public bool IsScanODDraft { get; set; }
        public bool IsCancelApproveAvailale { get; set; }
        public bool IsAgreeAvailable { get; set; }//доступность кнопки отправки на согласование

        public int DeleteAttachmentId { get; set; }//Id удаляемого скана
        public DateTime? SendTo1C { get; set; }//дата выгрузки в 1С

        [Display(Name = "Согласен на обработку своих персональных данных"), RequiredTrue(ErrorMessage = "Сохранение невозможно: отсутствует согласие на обработку персональных данных.")]
        public bool AgreedToPersonalDataProcessing { get; set; } //ok

        [Display(Name = "Подтверждаю что приложены все сканы имеющихся документов. Достоверность выложенных сканов документов подтверждаю.")]
        public bool IsScanFinal { get; set; } //ok

        public bool IsPrevApproveBySecurityAvailable { get; set; }

        [Display(Name = "Согласование")]
        public bool? PrevApprovalStatus { get; set; }
        public IEnumerable<SelectListItem> PrevApprovalStatuses { get; set; }
        public string PrevApproverName { get; set; }
        [Display(Name = "Дата согласования")]
        public DateTime? PrevApprovalDate { get; set; }

        //список сканов анкеты
        public IList<EmploymentAttachmentDto> AttachmentList { get; set; }

        //сканы общей информации
        [Display(Name = "Скан СНИЛС")]
        public HttpPostedFileBase SNILSScanFile { get; set; }
        public string SNILSScanAttachmentFilename { get; set; }
        public int SNILSScanAttachmentId { get; set; }

        [Display(Name = "Скан ИНН")]
        public HttpPostedFileBase INNScanFile { get; set; }
        public string INNScanAttachmentFilename { get; set; }
        public int INNScanAttachmentId { get; set; }

        [Display(Name = "Скан справки об инвалидности")]
        public HttpPostedFileBase DisabilityCertificateScanFile { get; set; }
        public string DisabilityCertificateScanAttachmentFilename { get; set; }
        public int DisabilityCertificateScanAttachmentId { get; set; }

        [Display(Name = "Скан паспорта")]
        public HttpPostedFileBase InternalPassportScanFile { get; set; }
        public string InternalPassportScanAttachmentFilename { get; set; }
        public int InternalPassportScanAttachmentId { get; set; }

        [Display(Name = "Скан документа об образовании")]
        public HttpPostedFileBase HigherEducationDiplomaScanFile { get; set; }
        public string HigherEducationDiplomaScanFileName { get; set; }
        public int HigherEducationDiplomaScanId { get; set; }

        [Display(Name = "Скан документа о послевузовском образовании")]
        public HttpPostedFileBase PostGraduateEducationDiplomaScanFile { get; set; }
        public string PostGraduateEducationDiplomaScanFileName { get; set; }
        public int PostGraduateEducationDiplomaScanId { get; set; }

        [Display(Name = "Скан документа о дополнительном образовании")]
        public HttpPostedFileBase CertificationScanFile { get; set; }
        public string CertificationScanFileName { get; set; }
        public int CertificationScanId { get; set; }

        [Display(Name = "Скан документа о повышении квалификации")]
        public HttpPostedFileBase TrainingScanFile { get; set; }
        public string TrainingScanFileName { get; set; }
        public int TrainingScanId { get; set; }

        [Display(Name = "Скан свидетельства о браке")]
        public HttpPostedFileBase MarriageCertificateScanFile { get; set; }
        public string MarriageCertificateScanAttachmentFilename { get; set; }
        public int MarriageCertificateScanAttachmentId { get; set; }

        [Display(Name = "Скан свидетельств о рождении детей")]
        public HttpPostedFileBase ChildBirthCertificateScanFile { get; set; }
        public string ChildBirthCertificateScanAttachmentFilename { get; set; }
        public int ChildBirthCertificateScanAttachmentId { get; set; }

        [Display(Name = "Скан военного билета")]
        public HttpPostedFileBase MilitaryCardScanFile { get; set; }
        public string MilitaryCardScanAttachmentFilename { get; set; }
        public int MilitaryCardScanAttachmentId { get; set; }

        [Display(Name = "Скан мобилизационного талона")]
        public HttpPostedFileBase MobilizationTicketScanFile { get; set; }
        public string MobilizationTicketScanAttachmentFilename { get; set; }
        public int MobilizationTicketScanAttachmentId { get; set; }

        [Display(Name = "Скан трудовой книжки")]
        public HttpPostedFileBase WorkBookScanFile { get; set; }
        public string WorkBookScanAttachmentFilename { get; set; }
        public int WorkBookScanAttachmentId { get; set; }

        [Display(Name = "Скан вкладыша в трудовую книжку")]
        public HttpPostedFileBase WorkBookSupplementScanFile { get; set; }
        public string WorkBookSupplementScanAttachmentFilename { get; set; }
        public int WorkBookSupplementScanAttachmentId { get; set; }

        [Display(Name = "Скан рукописного текста согласия на обработку персональных данных")]
        public HttpPostedFileBase PersonalDataProcessingScanFile { get; set; }
        public string PersonalDataProcessingScanAttachmentFilename { get; set; }
        public int PersonalDataProcessingScanAttachmentId { get; set; }

        [Display(Name = "Скан рукописного текста о достоверности сведений")]
        public HttpPostedFileBase InfoValidityScanFile { get; set; }
        public string InfoValidityScanAttachmentFilename { get; set; }
        public int InfoValidityScanAttachmentId { get; set; }
    }
}

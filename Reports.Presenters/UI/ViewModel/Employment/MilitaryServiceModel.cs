using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;
using Reports.Core.Dto;
using System.Web;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class MilitaryServiceModel : AbstractEmploymentModel
    {
        [Display(Name = "Военнообязанный")]
        public bool IsLiableForMilitaryService { get; set; } //ok

        [Display(Name = "Номер военного билета"),
            StringLength(20, ErrorMessage = "Не более 20 знаков.")]
        public string MilitaryCardNumber { get; set; } //ok

        [Display(Name = "Дата выдачи"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MilitaryCardDate { get; set; } //ok

        [Display(Name = "Категория запаса"),
            StringLength(20, ErrorMessage = "Не более 20 знаков.")]
        public string ReserveCategory { get; set; } //ok

        [Display(Name = "Воинское звание")]
        public int? RankId { get; set; } //ok
        public IList<MilitaryRanksDto> RankItems { get; set; }

        [Display(Name = "Состав (профиль)")]
        public int SpecialityCategoryId { get; set; } //ok
        public IList<IdNameDto> SpecialityCategoryes { get; set; }

        [Display(Name = "Полное кодовое обозначение ВУС"),
            StringLength(7, ErrorMessage = "Не более 7 знаков.")]
        public string MilitarySpecialityCode { get; set; } //ok

        [Display(Name = "Категория годности к военной службе")]
        public int MilitaryValidityCategoryId { get; set; } //ok
        public IList<MilitaryValidityCategoryDto> MilitaryValidityCategoryes { get; set; }

        [Display(Name = "Наименование ВК по месту жительства"),
            StringLength(100, ErrorMessage = "Не более 100 знаков.")]
        public string Commissariat { get; set; } //ok

        [Display(Name = "Отношение к воинскому учету")]
        public int MilitaryRelationAccountId { get; set; } //ok
        public IList<MilitaryRelationAccountDto> MilitaryRelationAccounts { get; set; }

        [Display(Name = "а) общем (номер команды, партии)"),
            StringLength(250, ErrorMessage = "Не более 250 знаков.")]
        public string CommonMilitaryServiceRegistrationInfo { get; set; }

        [Display(Name = "б) специальном"),
            StringLength(250, ErrorMessage = "Не более 250 знаков.")]
        public string SpecialMilitaryServiceRegistrationInfo { get; set; }

        [Display(Name = "Отношение к воинской обязанности")]
        public int? RegistrationExpiration { get; set; } //ok
        public IEnumerable<SelectListItem> RegistrationExpirationItems { get; set; }

        [Display(Name = "Бронирован")]
        public bool IsReserved { get; set; }

        [Display(Name = "Номер мобилизационного талона"),
            StringLength(20, ErrorMessage = "Не более 20 знаков.")]
        public string MobilizationTicketNumber { get; set; }

        [Display(Name = "Категория персонала")]
        public int? PersonnelCategory { get; set; } //ok
        public IEnumerable<SelectListItem> PersonnelCategoryItems { get; set; }

        [Display(Name = "Тип")]
        public int? PersonnelType { get; set; } //ok
        public IEnumerable<SelectListItem> PersonnelTypeItems { get; set; }

        [Display(Name = "Предписание")]
        public bool IsAssigned { get; set; } //ok

        [Display(Name = "Призыв на военную службу")]
        public int? ConscriptionStatus { get; set; } //ok
        public IEnumerable<SelectListItem> ConscriptionStatusItems { get; set; }

        [Display(Name = "Правильность предоставленных данных подтверждаю")]
        public bool IsValidate { get; set; } //ok

        public HttpPostedFileBase MilitaryCardScanFile { get; set; }
        public HttpPostedFileBase MobilizationTicketScanFile { get; set; }

        public string MilitaryCardScanAttachmentFilename { get; set; }
        public int MilitaryCardScanAttachmentId { get; set; }
        public string MobilizationTicketScanAttachmentFilename { get; set; }
        public int MobilizationTicketScanAttachmentId { get; set; }

        //состояние кандидата
        //public IList<CandidateStateDto> CandidateState { get; set; }
        public CandidateStateModel CandidateStateModel { get; set; }

        public MilitaryServiceModel()
        {
            this.Version = 0;
        }

    }
}

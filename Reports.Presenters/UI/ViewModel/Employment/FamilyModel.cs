using System;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class FamilyModel : AbstractEmploymentModel
    {
        [Display(Name = "Состояние в браке")]
        public bool IsMarried { get; set; } //ok?

        [Display(Name = "Муж/жена (гражданский муж/жена)")]
        public FamilyMemberDto Spouse { get; set; }

        [Display(Name = "Отец")]
        public FamilyMemberDto Father { get; set; }

        [Display(Name = "Мать")]
        public FamilyMemberDto Mother { get; set; }

        [Display(Name = "Дети")]
        public IList<FamilyMemberDto> Children { get; set; }

        [Display(Name = "ФИО совместно проживающих"),
            StringLength(250, ErrorMessage = "Не более 250 знаков.")]
        public string Cohabitants { get; set; } //ok

        [Display(Name = "Правильность предоставленных данных подтверждаю")]
        public bool IsValidate { get; set; } //ok

        [Display(Name = "Семейное положение"),
            Required(ErrorMessage = "*")]
        public int? FamilyStatusId { get; set; }
        public IList<FamilyStatusDto> FamillyStatuses { get; set; }

        public HttpPostedFileBase MarriageCertificateScanFile { get; set; }
        public string MarriageCertificateScanAttachmentFilename { get; set; }
        public int MarriageCertificateScanAttachmentId { get; set; }

        public HttpPostedFileBase ChildBirthCertificateScanFile { get; set; }
        public string ChildBirthCertificateScanAttachmentFilename { get; set; }
        public int ChildBirthCertificateScanAttachmentId { get; set; }
        

        public int RowID { get; set; }

        //состояние кандидата
        //public IList<CandidateStateDto> CandidateState { get; set; }
        public CandidateStateModel CandidateStateModel { get; set; }

        public FamilyModel()
        {
            this.Version = 0;
            this.Children = new List<FamilyMemberDto>();
            this.Father = new FamilyMemberDto();
            this.Mother = new FamilyMemberDto();
            this.Spouse = new FamilyMemberDto();
        }               
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;
using System.Web;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class EducationModel : AbstractEmploymentModel
    {
        [Display(Name = "Сведения об образовании (высшее, неполное высшее, начальное профессиональное, среднее профессиональное)")]
        public IList<HigherEducationDiplomaDto> HigherEducationDiplomas { get; set; } //ok

        [Display(Name = "Послевузовское образование")]
        public IList<PostGraduateEducationDiplomaDto> PostGraduateEducationDiplomas { get; set; } //ok

        [Display(Name = "Аттестация")]
        public IList<CertificationDto> Certifications { get; set; } //ok

        [Display(Name = "Повышение квалификации")]
        public IList<TrainingDto> Training { get; set; } //ok

        //для удаления записей на странице
        public int Operation { get; set; }
        public int RowID { get; set; }

        //состояние кандидата
        public IList<CandidateStateDto> CandidateState { get; set; }

        public HttpPostedFileBase HigherEducationDiplomaScanFile { get; set; }
        public int HigherEducationDiplomaScanId { get; set; }
        public string HigherEducationDiplomaScanFileName { get; set; }

        public HttpPostedFileBase PostGraduateEducationDiplomaScanFile { get; set; }
        public int PostGraduateEducationDiplomaScanId { get; set; }
        public string PostGraduateEducationDiplomaScanFileName { get; set; }

        public HttpPostedFileBase CertificationScanFile { get; set; }
        public int CertificationScanId { get; set; }
        public string CertificationScanFileName { get; set; }

        public HttpPostedFileBase TrainingScanFile { get; set; }
        public int TrainingScanId { get; set; }
        public string TrainingScanFileName { get; set; }

        public EducationModel()
        {
            this.Version = 0;
            this.HigherEducationDiplomas = new List<HigherEducationDiplomaDto>();
            this.PostGraduateEducationDiplomas = new List<PostGraduateEducationDiplomaDto>();
            this.Certifications = new List<CertificationDto>();
            this.Training = new List<TrainingDto>();
        }
    }
}

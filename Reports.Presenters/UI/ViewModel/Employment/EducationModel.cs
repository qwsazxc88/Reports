using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;
using Reports.Core.Domain;
using System.Web;
using System.Web.Mvc;


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

        [Display(Name = "Правильность предоставленных данных подтверждаю")]
        public bool IsValidate { get; set; } //ok

        //для удаления записей на странице
        public int Operation { get; set; }
        public int RowID { get; set; }

        //состояние кандидата
        //public IList<CandidateStateDto> CandidateState { get; set; }
        public CandidateStateModel CandidateStateModel { get; set; }

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

        //для проверок модальных форм 
        [Display(Name = "Вид образования"),
            Required(ErrorMessage = "*")]
        public int EducationTypeId { get; set; }
        public IList<EmploymentEducationType> EducationTypes { get; set; }

        [Display(Name = "Наименование образовательного учреждения"),
            StringLength(100, ErrorMessage = "Не более 100 знаков."),
            Required(ErrorMessage = "*")]
        public string CertificateIssuedBy { get; set; }

        [Display(Name = "Наименование образовательного учреждения"),
            StringLength(100, ErrorMessage = "Не более 100 знаков."),
            Required(ErrorMessage = "*")]
        public string IssuedBy { get; set; }

        [Display(Name = "Серия"),
            StringLength(20, ErrorMessage = "Не более 20 знаков."),
            Required(ErrorMessage = "*")]
        public string Series { get; set; }
        
        [Display(Name = "Номер"),  
            StringLength(20, ErrorMessage = "Не более 20 знаков."),
            Required(ErrorMessage = "*")]
        public string Number { get; set; }

        [Display(Name = "Год поступления"),
            RegularExpression(@"^\d{4}$", ErrorMessage = "Требуется 4 цифры"),
            Required(ErrorMessage = "*")]
        public string AdmissionYear { get; set; }

        [Display(Name = "Год окончания"),
            RegularExpression(@"^\d{4}$", ErrorMessage = "Требуется 4 цифры"),
            Required(ErrorMessage = "*")]
        public string GraduationYear { get; set; }

        [Display(Name = "Квалификация по диплому"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "*")]
        public string Qualification { get; set; }

        [Display(Name = "Специальность по диплому"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "*")]
        public string Speciality { get; set; }

        [Display(Name = "Профессия основная"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "*")]
        public string Profession { get; set; }

        [Display(Name = "Факультет/отделение"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "*")]
        public string Department { get; set; }

        [Display(Name = " с "),
            Required(ErrorMessage = "*")]
        public DateTime? BeginningDate { get; set; }

        [Display(Name = " по "),
            Required(ErrorMessage = "*")]
        public DateTime? EndDate { get; set; }



        public bool IsHigherEducationNotValid { get; set; }//для проверок заполнения сведения об образовании
        public bool IsPostGraduateEducationNotValid { get; set; }//для проверок заполнения послевузовского образования
        public bool IsEducationTrainingNotValid { get; set; }//для проверок заполнения повышения квалификации

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

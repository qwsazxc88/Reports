using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

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

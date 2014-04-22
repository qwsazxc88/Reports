using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment;

namespace Reports.Presenters.UI.ViewModel.Employment
{
    public class EducationModel
    {
        [Display(Name = "Сведения об образовании (высшее, неполное высшее)")]
        public IList<HigherEducationDiplomaDto> HigherEducationDiplomas { get; set; }

        [Display(Name = "Послевузовское образование")]
        public IList<PostGraduateEducationDiplomaDto> PostGraduateEducationDiplomas { get; set; }

        [Display(Name = "Аттестация")]
        public IList<CertificationDto> Certifications { get; set; }

        [Display(Name = "Повышение квалификации")]
        public IList<TrainingDto> Training { get; set; }
    }
}

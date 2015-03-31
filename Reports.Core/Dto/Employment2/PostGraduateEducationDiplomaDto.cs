using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Reports.Core.Dto.Employment2
{
    public class PostGraduateEducationDiplomaDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Display(Name = "Наименование образовательного учреждения"),
            StringLength(100, ErrorMessage = "Не более 100 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string IssuedBy { get; set; }

        [Display(Name = "Серия"),
            StringLength(20, ErrorMessage = "Не более 20 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string Series { get; set; }

        [Display(Name = "Номер"),
            StringLength(20, ErrorMessage = "Не более 20 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string Number { get; set; }

        [Display(Name = "Год поступления"),
            RegularExpression(@"^\d{4}$", ErrorMessage = "Требуется 4 цифры"),
            Required(ErrorMessage = "Обязательное поле")]
        public string AdmissionYear { get; set; }

        [Display(Name = "Год окончания"),
            RegularExpression(@"^\d{4}$", ErrorMessage = "Требуется 4 цифры"),
            Required(ErrorMessage = "Обязательное поле")]
        public string GraduationYear { get; set; }

        [Display(Name = "Специальность по диплому"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string Speciality { get; set; }

        [Display(Name = "Место нахождения учебного заведения"),
            StringLength(150, ErrorMessage = "Не более 150 знаков."),
            Required(ErrorMessage = "*")]
        public string LocationEI { get; set; }

        public PostGraduateEducationDiplomaDto()
        {
            this.IssuedBy = "ДГТУ";
            this.Series = "AF";
            this.Number = "324234234";
            this.AdmissionYear = "2000";
            this.GraduationYear = "2005";
            this.Speciality = "Veterinarian";
        }
    }
}
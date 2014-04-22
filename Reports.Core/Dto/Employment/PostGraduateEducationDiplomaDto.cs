using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Reports.Core.Dto.Employment
{
    public class PostGraduateEducationDiplomaDto
    {
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
            StringLength(4, ErrorMessage = "4 знака"),
            RegularExpression(@"^\d{4}$", ErrorMessage = "Требуется 4 цифры"),
            Required(ErrorMessage = "Обязательное поле")]
        public DateTime AdmissionYear { get; set; }

        [Display(Name = "Год окончания"),
            StringLength(4, ErrorMessage = "4 знака"),
            RegularExpression(@"^\d{4}$", ErrorMessage = "Требуется 4 цифры"),
            Required(ErrorMessage = "Обязательное поле")]
        public DateTime GraduationYear { get; set; }

        [Display(Name = "Специальность по диплому"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string Speciality { get; set; }
    }
}
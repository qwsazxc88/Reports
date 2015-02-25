using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Reports.Core.Dto.Employment2
{
    public class HigherEducationDiplomaDto
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

        [Display(Name = "Квалификация по диплому"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string Qualification { get; set; }

        [Display(Name = "Специальность по диплому"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string Speciality { get; set; }

        [Display(Name = "Профессия основная"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string Profession { get; set; }

        [Display(Name = "Факультет/отделение"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string Department { get; set; }

        public HigherEducationDiplomaDto()
        {
            IssuedBy = "МГУ";
            Series = "AB";
            Number = "1234567";
            AdmissionYear = "1997";
            GraduationYear = "2002";
            Qualification = "xxx";
            Speciality = "yyy";
            Profession = "zzz";
            Department = "Finance";
        }
    }
}
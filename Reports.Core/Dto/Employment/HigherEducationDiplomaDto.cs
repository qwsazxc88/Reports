using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Reports.Core.Dto.Employment
{
    public class HigherEducationDiplomaDto
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
    }
}
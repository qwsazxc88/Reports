using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Reports.Core.Dto.Employment2
{
    public class ExperienceItemDto
    {
        [Display(Name = "Начало работы"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true),
            Required(ErrorMessage = "Обязательное поле")]
        public DateTime BeginningDate { get; set; }

        [Display(Name = "Окончание работы"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true),
            Required(ErrorMessage = "Обязательное поле")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Организация"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string Company { get; set; }

        [Display(Name = "Должность"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string Position { get; set; }

        [Display(Name = "Адрес организации, телефон отдела кадров, секретаря"),
            StringLength(250, ErrorMessage = "Не более 250 знаков.")]
        public string CompanyContacts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Reports.Core.Dto.Employment2
{
    public class NameChangeDto
    {
        [Display(Name = "Предыдущие ФИО"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string PreviousName { get; set; }
        [Display(Name = "Дата изменения"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true),
            Required(ErrorMessage = "Обязательное поле")]
        public DateTime? Date { get; set; }
        [Display(Name = "Место"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string Place { get; set; }
        [Display(Name = "Причина"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string Reason { get; set; }

        public NameChangeDto()
        {
            this.PreviousName = "John";            
            this.Date = new DateTime();
            this.Place = "NV";
            this.Reason = "Marriage";
        }
    }
}

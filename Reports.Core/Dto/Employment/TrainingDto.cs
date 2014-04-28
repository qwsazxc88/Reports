using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Reports.Core.Dto.Employment
{
    public class TrainingDto
    {
        [Display(Name = "Наименование образовательного учреждения"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string CertificateIssuedBy { get; set; }

        [Display(Name = "Серия"),
            StringLength(10, ErrorMessage = "Не более 10 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string Series { get; set; }

        [Display(Name = "Номер"),
            StringLength(10, ErrorMessage = "Не более 10 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string Number { get; set; }

        [Display(Name = "Начало обучения"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true),
            Required(ErrorMessage = "Обязательное поле")]
        public DateTime BeginningDate { get; set; }

        [Display(Name = "Конец обучения"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true),
            Required(ErrorMessage = "Обязательное поле")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Специальность/квалификация"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string Speciality { get; set; }

        public TrainingDto()
        {
            this.CertificateIssuedBy = "РЭА";
            this.Series = "AG";
            this.Number = "6565465456";
            this.BeginningDate = new DateTime();
            this.EndDate = new DateTime();
            this.Speciality = "Accountant";
        }
    }
}
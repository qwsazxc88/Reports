using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Reports.Core.Dto.Employment2
{
    public class CertificationDto
    {
        public int Id { get; set; }

        [Display(Name = "Дата аттестации"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true),
            Required(ErrorMessage = "Обязательное поле")]
        public DateTime? CertificationDate { get; set; }

        [Display(Name = "Номер документа"),
            StringLength(20, ErrorMessage = "Не более 20 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string CertificateNumber { get; set; }

        [Display(Name = "Дата документа"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true),
            Required(ErrorMessage = "Обязательное поле")]
        public DateTime? CertificateDateOfIssue { get; set; }

        [Display(Name = "Основание"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string InitiatingOrder { get; set; }

        public CertificationDto()
        {
            this.CertificationDate = new DateTime();
            this.CertificateNumber = string.Empty;
            this.CertificateDateOfIssue = new DateTime();
            this.InitiatingOrder = string.Empty;
        }
    }
}
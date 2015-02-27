using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Enum;

namespace Reports.Core.Dto.Employment2
{
    public class FamilyMemberDto
    {
        public int Id { get; set; }

        [Display(Name = "ФИО"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string Name { get; set; }

        [Display(Name = "Паспортные данные"),
            StringLength(500, ErrorMessage = "Не более 500 знаков.")]
        public string PassportData { get; set; }

        [Display(Name = "Дата рождения"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Место рождения"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string PlaceOfBirth { get; set; }

        [Display(Name = "Место работы, должность, (рабочий, мобильный телефон)"),
            StringLength(250, ErrorMessage = "Не более 250 знаков.")]
        public string WorksAt { get; set; }

        [Display(Name = "Адрес места жительства, (домашний, мобильный телефон)"),
            StringLength(250, ErrorMessage = "Не более 250 знаков.")]
        public string Contacts { get; set; }

        public FamilyMemberDto()
        {
            this.Name = String.Empty;
            this.PassportData = String.Empty;
            this.PlaceOfBirth = String.Empty;
            this.WorksAt = String.Empty;
            this.Contacts = String.Empty;
        }
    }
}

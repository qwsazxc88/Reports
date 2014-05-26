using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Reports.Core.Dto.Employment2
{
    public class FamilyMemberDto
    {
        public string Relation { get; set; } // TODO: EMPL Dropdown

        [Display(Name = "ФИО"),
            StringLength(50, ErrorMessage = "Не более 50 знаков.")]
        public string Name { get; set; }

        [Display(Name = "Паспортные данные"),
            StringLength(500, ErrorMessage = "Не более 500 знаков.")]
        public string PassportData { get; set; }

        [Display(Name = "Дата рождения"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

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
            this.Relation = "Ребенок";
            this.Name = "Петров П.П.";
            this.PassportData = "345v3g443t5f5";
            this.DateOfBirth = new DateTime();
            this.PlaceOfBirth = "Ростов-на-Дону";
            this.WorksAt = "Детский сад";
            this.Contacts = "???";
        }
    }
}

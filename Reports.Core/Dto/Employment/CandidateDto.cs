﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Reports.Core.Dto.Employment
{
    public class CandidateDto
    {
        public string Name { get; set; }

        // Дирекция

        public string WorkCity { get; set; }

        // Структурное подразделение

        public string Position { get; set; }

        public DateTime EmploymentDate { get; set; }

        public string EmploymentOrderNumber { get; set; }

        public DateTime EmploymentOrderDate { get; set; }

        public string ContractNumber { get; set; }

        public DateTime ContractDate { get; set; }

        public bool IsFulltime { get; set; }

        public bool IsTemporary { get; set; }

        public string ProbationaryPeriod { get; set; }

        public string Timetable { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Disabilities { get; set; }

        public string Grade { get; set; }

        public string Status { get; set; }

        /*
        [Display(Name = "Паспортные данные"),
            StringLength(500, ErrorMessage = "Не более 500 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string PassportData { get; set; }

        [Display(Name = "Дата рождения"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true),
            Required(ErrorMessage = "Обязательное поле")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Место рождения"),
            StringLength(50, ErrorMessage = "Не более 50 знаков."),
            Required(ErrorMessage = "Обязательное поле")]
        public string PlaceOfBirth { get; set; }

        [Display(Name = "Место работы, должность, (рабочий, мобильный телефон)"),
            StringLength(250, ErrorMessage = "Не более 250 знаков.")]
        public string WorksAt { get; set; }

        [Display(Name = "Адрес места жительства, (домашний, мобильный телефон)"),
            StringLength(250, ErrorMessage = "Не более 250 знаков.")]
        public string WorksAt { get; set; }
        */
    }
}

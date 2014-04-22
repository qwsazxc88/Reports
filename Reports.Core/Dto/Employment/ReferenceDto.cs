using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Reports.Core.Dto.Employment
{
    public class ReferenceDto
    {
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Место работы")]
        public string WorksAt { get; set; }

        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Display(Name = "Телефон (рабочий, мобильный)")]
        public string Patronymic { get; set; }

        [Display(Name = "Откуда Вас знает рекомендующий")]
        public string Relation { get; set; }
    }
}

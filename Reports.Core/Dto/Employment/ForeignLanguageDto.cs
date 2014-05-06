using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Reports.Core.Dto.Employment
{
    public class ForeignLanguageDto
    {
        [Display(Name = "Язык")]
        public string LanguageName { get; set; }

        [Display(Name = "Уровень")]
        public string Level { get; set; }
    }
}

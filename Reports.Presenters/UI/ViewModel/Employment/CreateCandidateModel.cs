using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;
using System.Web.Mvc;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class CreateCandidateModel
    {
        [Display(Name = "Паспортные данные (серия, номер)", Prompt = "#### ######"),
            StringLength(11, ErrorMessage = "Требуется 11 знаков."),
            RegularExpression(@"^\d{4} \d{6}$", ErrorMessage = "Требуется формат #### ######")]
        public string PassportData { get; set; }

        [Display(Name = "СНИЛС №", Prompt = "###-###-###-##"),
            RegularExpression(@"^(\d{3}-){3}\d{2}$", ErrorMessage = "Требуется формат ###-###-###-##")]
        public string SNILS { get; set; }

        [Display(Name = "Дата рождения"),
            DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }
                
        public int DepartmentId { get; set; }
        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }

        public int? OnBehalfOfManagerId { get; set; }
        [Display(Name = "За руководителя")]
        public string OnBehalfOfManagerName { get; set; }

        // Id временного пользователя
        public int? UserId { get; set; }

        public bool IsOnBehalfOfManagerAvailable { get; set; }
    }
}
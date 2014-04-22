using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment;

namespace Reports.Presenters.UI.ViewModel.Employment
{
    public class FamilyModel
    {
        [Display(Name = "Состояние в браке")]
        public bool MaritalStatus { get; set; }

        [Display(Name = "Муж/жена (гражданский муж/жена)")]
        public FamilyMemberDto Spouse { get; set; }

        [Display(Name = "Отец")]
        public FamilyMemberDto Father { get; set; }

        [Display(Name = "Мать")]
        public FamilyMemberDto Mother { get; set; }

        [Display(Name = "Дети")]
        public IList<FamilyMemberDto> Children { get; set; }

        [Display(Name = "ФИО совместно проживающих"),
            StringLength(250, ErrorMessage = "Не более 250 знаков.")]
        public string Cohabitants { get; set; }
    }
}

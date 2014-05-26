using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class FamilyModel
    {
        [Display(Name = "Состояние в браке")]
        public bool IsMarried { get; set; } //ok?

        [Display(Name = "Муж/жена (гражданский муж/жена)")]
        public FamilyMemberDto Spouse { get; set; } //ok?

        [Display(Name = "Отец")]
        public FamilyMemberDto Father { get; set; } //ok?

        [Display(Name = "Мать")]
        public FamilyMemberDto Mother { get; set; } //ok?

        [Display(Name = "Дети")]
        public IList<FamilyMemberDto> Children { get; set; } //ok?

        [Display(Name = "ФИО совместно проживающих"),
            StringLength(250, ErrorMessage = "Не более 250 знаков.")]
        public string Cohabitants { get; set; } //ok

        public FamilyModel()
        {
            this.Children = new List<FamilyMemberDto>();
        }
    }
}
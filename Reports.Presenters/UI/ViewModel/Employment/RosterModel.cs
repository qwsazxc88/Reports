using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class RosterModel
    {
        [Display(Name = "Реестр по приему"),
            StringLength(200, ErrorMessage = "Не более 200 знаков.")]
        public IList<CandidateDto> Roster { get; set; }

        public RosterModel()
        {
            this.Roster = new List<CandidateDto>();
        }
    }
}

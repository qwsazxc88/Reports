using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class RosterInputModel
    {
        [Display(Name = "Реестр по приему")]
        public IList<CandidateApprovalDto> Roster { get; set; }
        /*
        public RosterInputModel()
        {
            this.Roster = new List<CandidateApprovalDto>();
        }*/
    }
}

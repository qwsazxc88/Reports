using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class PersonnelInfoModel
    {
        public int CandidateID { get; set; }
        public bool IsCandidateInfoAvailable { get; set; }
        public bool IsBackgroundCheckAvailable { get; set; }
        public bool IsManagersAvailable { get; set; }
        public bool IsPersonalManagersAvailable { get; set; }
        public int TabIndex { get; set; }
    }
}

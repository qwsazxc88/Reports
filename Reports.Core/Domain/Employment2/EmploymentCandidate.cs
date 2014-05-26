using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Dto.Employment2;

namespace Reports.Core.Domain
{
    public class EmploymentCandidate : AbstractEntityWithVersion
    {
        public virtual User User { get; set; }
        public virtual GeneralInfo GeneralInfo { get; set; }
        public virtual Passport Passport { get; set; }
        public virtual Education Education { get; set; }
        public virtual Family Family { get; set; }
        public virtual MilitaryService MilitaryService { get; set; }
        public virtual Experience Experience { get; set; }
        public virtual Contacts Contacts { get; set; }
        public virtual BackgroundCheck BackgroundCheck { get; set; }
        public virtual OnsiteTraining OnsiteTraining { get; set; }
        public virtual Managers Managers { get; set; }
        public virtual PersonnelManagers PersonnelManagers { get; set; }
    }
}
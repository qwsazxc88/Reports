using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain.Employment
{
    public class MilitaryService : AbstractEntity
    {
        public virtual User User { get; set; }
        public bool IsLiableForMilitaryService { get; set; }
        public string MilitaryCardNumber { get; set; }
        public DateTime MilitaryCardDate { get; set; }
        
        public string MilitarySpecialityCode { get; set; }
        public string CombatFitness { get; set; }
        public string Commissariat { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain.Employment
{
    public class Family : AbstractEntity
    {
        public virtual User User { get; set; }
        public bool MaritalStatus { get; set; }
        //public FamilyMemberDto Spouse { get; set; }
        //public FamilyMemberDto Father { get; set; }
        //public FamilyMemberDto Mother { get; set; }
        public IList<FamilyMember> FamilyMembers { get; set; }
        public string Cohabitants { get; set; }
    }
}

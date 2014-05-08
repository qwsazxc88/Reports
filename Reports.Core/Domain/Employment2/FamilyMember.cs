using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain.Employment2
{
    public class FamilyMember : AbstractEntity
    {
        public virtual User User { get; set; }
        public virtual int Relation { get; set; }
        public virtual string Name { get; set; }
        public virtual string PassportData { get; set; }
        public virtual  DateTime DateOfBirth { get; set; }
        public virtual string PlaceOfBirth { get; set; }
        public virtual string WorksAt { get; set; }
        public virtual string Contacts { get; set; }
    }
}
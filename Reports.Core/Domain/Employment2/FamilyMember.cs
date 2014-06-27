﻿using System;

namespace Reports.Core.Domain
{
    public class FamilyMember : AbstractEntityWithVersion
    {
        public virtual FamilyRelationship Relationship { get; set; }
        public virtual string Name { get; set; }
        public virtual string PassportData { get; set; }
        public virtual DateTime? DateOfBirth { get; set; }
        public virtual string PlaceOfBirth { get; set; }
        public virtual string WorksAt { get; set; }
        public virtual string Contacts { get; set; }
    }
}
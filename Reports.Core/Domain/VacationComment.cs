using System;

namespace Reports.Core.Domain
{
    public class DocumentComment : AbstractEntityWithVersion
    {
        public virtual User User { get; set; }
        public virtual Document Document { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime DateCreated { get; set; }
    }
}
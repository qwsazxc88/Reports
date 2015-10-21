using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Архив справочника подразделений.
    /// </summary>
    public class DepartmentArchive : AbstractEntity
    {
        public virtual Department Department { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual int? Code1C { get; set; }
        public virtual int? ParentId { get; set; }
        public virtual string Path { get; set; }
        public virtual int? ItemLevel { get; set; }
        public virtual int? Priority { get; set; }
        public virtual bool IsUsed { get; set; }
        public virtual User Creator { get; set; }
        public virtual DateTime? CreateDate { get; set; } 
    }
}

using System;

namespace Reports.Core.Domain
{
    public class ExportImportAction : AbstractEntityWithVersion
    {
        public virtual int Type { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual DateTime? Month { get; set; }
    }
}
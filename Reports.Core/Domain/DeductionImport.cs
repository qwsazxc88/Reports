using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class DeductionImport : AbstractEntity
    {
        public virtual string InputFile { get; set; }
        public virtual string ReportFile { get; set; }
        public virtual User Creator { get; set; }
        public virtual DateTime ImportDate { get; set; }
        public virtual string InputFileHash { get; set; }
    }
}

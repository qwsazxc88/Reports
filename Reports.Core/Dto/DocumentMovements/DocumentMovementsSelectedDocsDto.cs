using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto.DocumentMovements
{
    public class DocumentMovementsSelectedDocsDto
    {
        public int Type { get; set; }
        public bool SenderCheck { get; set; }
        public DateTime? SenderCheckDate { get; set; }
        public bool RecieverCheck { get; set; }
        public DateTime? RecieverCheckDate { get; set; }
    }
}

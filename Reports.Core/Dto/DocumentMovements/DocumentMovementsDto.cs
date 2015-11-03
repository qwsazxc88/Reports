using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class DocumentMovementsDto
    {
        public StandartUserDto User { get; set; }
        public StandartUserDto Sender { get; set; }
        public StandartUserDto Receiver { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime? SendDate { get; set; }
        public virtual string Descript { get; set; }
        //public virtual DocumentMovements_SelectedDocs Docs { get; set; }
    }
}

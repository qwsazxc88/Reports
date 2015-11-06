using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
namespace Reports.Core.Dto
{
    public class DocumentMovementsSelectedDocsDto
    {
        public int Type { get; set; }
        public string TypeName { get; set; }
        public bool SenderCheck { get; set; }
        public DateTime? SenderCheckDate { get; set; }
        public bool RecieverCheck { get; set; }
        public DateTime? RecieverCheckDate { get; set; }
        public bool IsEditable { get; set; }
        public DocumentMovementsSelectedDocsDto()
        {
            this.IsEditable = true;
        }
        public DocumentMovementsSelectedDocsDto FromDomain(DocumentMovements_SelectedDocs x)
        {
            this.Type = x.DocType.Id;
            this.TypeName = x.DocType.Name;
            this.SenderCheck = x.SenderCheck;
            this.SenderCheckDate = x.SenderCheckDate;
            this.RecieverCheck = x.RecieverCheck;
            this.RecieverCheckDate = x.RecieverCheckDate;
            return this;
        }
    }
}

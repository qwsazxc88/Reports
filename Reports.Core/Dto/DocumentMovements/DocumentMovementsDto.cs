using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
namespace Reports.Core.Dto
{
    public class DocumentMovementsDto
    {
        public DocumentMovementsDto()
        {
            this.User = new StandartUserDto();
            this.Sender = new StandartUserDto();
            this.Receiver = new StandartUserDto();
            this.SelectedDocs = new List<DocumentMovementsSelectedDocsDto>();
        }
        public StandartUserDto User { get; set; }
        public StandartUserDto Sender { get; set; }
        public StandartUserDto Receiver { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? SendDate { get; set; }
        public string Descript { get; set; }
        public List<DocumentMovementsSelectedDocsDto> SelectedDocs { get; set; }
        //public virtual DocumentMovements_SelectedDocs Docs { get; set; }
        public DocumentMovementsDto FromDomain(DocumentMovements x)
        {
                CreateDate = x.CreateDate;
                Descript = x.Descript;
                Receiver = x.Receiver != null ? new StandartUserDto { Id = x.Receiver.Id, Name = x.Receiver.Name, Dep7Name = x.Receiver.Department != null ? x.Receiver.Department.Name : "", Dep3Name = x.Receiver.Department != null && x.Receiver.Department.Dep3 != null && x.Receiver.Department.Dep3.Any() ? x.Receiver.Department.Dep3.First().Name : "" } : new StandartUserDto { };
                Sender = x.Sender != null ? new StandartUserDto { Id = x.Sender.Id, Name = x.Sender.Name, Dep7Name = x.Sender.Department != null ? x.Sender.Department.Name : "", Dep3Name = x.Sender.Department != null && x.Sender.Department.Dep3 != null && x.Sender.Department.Dep3.Any() ? x.Sender.Department.Dep3.First().Name : "" } : new StandartUserDto { };
                User = x.User != null ? new StandartUserDto { Id = x.User.Id, Name = x.User.Name, Dep7Name = x.User.Department != null ? x.User.Department.Name : "", Dep3Name = x.User.Department != null && x.User.Department.Dep3 != null && x.User.Department.Dep3.Any() ? x.User.Department.Dep3.First().Name : "" } : new StandartUserDto { };
                SendDate = x.SendDate;
                SelectedDocs = x.Docs.Select<DocumentMovements_SelectedDocs,DocumentMovementsSelectedDocsDto>(t => new DocumentMovementsSelectedDocsDto().FromDomain(t)).ToList();
                return this;
        }
    }
}

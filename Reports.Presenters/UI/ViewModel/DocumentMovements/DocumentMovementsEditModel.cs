using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Dao;
using Reports.Core.Dto;
using Reports.Core.Domain;
using System.ComponentModel.DataAnnotations;
namespace Reports.Presenters.UI.ViewModel
{
    public class DocumentMovementsEditModel
    {
        public DocumentMovementsEditModel()
        {
            this.User = new StandartUserDto();
            this.Receiver = new StandartUserDto();
            this.Sender = new StandartUserDto();
            this.SelectedDocs = new List<DocumentMovementsSelectedDocsDto>();
        }
        [Display(Name="Номер заявки")]
        public int Id { get; set; }
        public StandartUserDto User { get; set; }
        public StandartUserDto Sender { get; set; }
        public StandartUserDto Receiver { get; set; }
        
        public List<DocumentMovementsSelectedDocsDto> SelectedDocs { get; set; }
        [Display(Name="Описание")]
        public string Descript { get; set; }
        public DateTime CreateDate { get; set; }
        [Display(Name="Дата отправки")]
        public DateTime? SendDate { get; set; }
        public bool IsUserSender { get; set; }
        public bool IsUserReceiver { get; set; }
        public bool SenderAccept { get; set; }
        public bool ReceiverAccept { get; set; }
    }
}

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
            this.User.Label = "Сотрудник";
            this.Receiver = new StandartUserDto();
            this.Receiver.Label = "Получатель";
            this.Sender = new StandartUserDto();
            this.Sender.Label = "Отправитель";
            this.SelectedDocs = new List<DocumentMovementsSelectedDocsDto>();
        }
        [Display(Name="Номер заявки")]
        public int Id { get; set; }
        public int SenderRuscount { get; set; }
        public StandartUserDto User { get; set; }
        public StandartUserDto Sender { get; set; }
        public StandartUserDto Receiver { get; set; }
        public List<IdNameDto> RuscountUsers { get; set; }
        public List<DocumentMovementsSelectedDocsDto> SelectedDocs { get; set; }
        [Display(Name="Описание")]
        public string Descript { get; set; }
        [Display(Name="Дата создания")]
        [DisplayFormat(DataFormatString="{0:dd.MM.yyyy}")]
        public DateTime CreateDate { get; set; }
        [Display(Name="Дата отправки")]
        public DateTime? SendDate { get; set; }
        public bool IsUserSender { get; set; }
        public bool IsUserReceiver { get; set; }
        [Display(Name="Согласовано отправителем")]
        public bool SenderAccept { get; set; }
        [Display(Name = "Согласовано получателем")]
        public bool ReceiverAccept { get; set; }
        public bool IsUserFromBank { get; set; }
    }
}

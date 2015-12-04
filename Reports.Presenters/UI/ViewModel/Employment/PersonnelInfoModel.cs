using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Dto;
using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class PersonnelInfoModel
    {
        public int CandidateID { get; set; }
        public bool IsCandidateInfoAvailable { get; set; }
        public bool IsBackgroundCheckAvailable { get; set; }
        public bool IsManagersAvailable { get; set; }
        public bool IsPersonalManagersAvailable { get; set; }
        public int TabIndex { get; set; }
        public string CandidateName { get; set; }
        //список участников процесса оформления
        [Display(Name = "Кому")]
        public int ToUserId { get; set; }
        public IList<IdNameDto> UsersTo { get; set; }
        [Display(Name = "Тема")]
        public string Subject { get; set; }
        [Display(Name = "Сообщение")]
        public string EmailMessage { get; set; }
    }
}

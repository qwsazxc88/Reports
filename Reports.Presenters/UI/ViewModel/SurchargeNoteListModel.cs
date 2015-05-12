using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Reports.Core.Dto;
using System.ComponentModel.DataAnnotations;
namespace Reports.Presenters.UI.ViewModel
{
    public class SurchargeNoteListModel
    {
        public int NoteType { get; set; }
        public int DepartmentId { get; set; }
        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }
        
        [Display(Name = "Статус заявки")]
        public int StatusId { get; set; }
        public IList<IdNameDto> Statuses;

        [Display(Name = "Номер заявки")]
        public string Number { get; set; }
        [Display(Name = "ФИО сотрудника")]
        public string UserName { get; set; }

        [Display(Name = "Период с")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BeginDate { get; set; }

        [Display(Name = "по")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
    }
}

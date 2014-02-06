using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class MissionPbEditRecordModel
    {
        public int RecordId { get; set; }
        public int DocumentId { get; set; }

        [Display(Name = "Сотрудник")]
        public int RecordUserId { get; set; }

        public List<IdNameDto> Users { get; set; }

        [Display(Name = "Авансовый отчет")]
        public int ReportId { get; set; }
        public List<IdNameDto> Reports { get; set; }

        [Display(Name = "Вид расхода")]
        public int CostTypeId { get; set; }

        public List<IdNameDto> CostTypes { get; set; }

        [Display(Name = "Сумма расходов")]
        public decimal? Sum { get; set; }

        [Display(Name = "НДС")]
        public decimal? SumNds { get; set; }

        [Display(Name = "Сумма всего")]
        public decimal? AllSum { get; set; }

        [Display(Name = "Номер заявки в ЭССЗ")]
        public string RequestNumber { get; set; }
    }
}
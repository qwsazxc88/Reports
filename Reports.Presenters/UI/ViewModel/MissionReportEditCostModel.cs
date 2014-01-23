using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class MissionReportEditCostModel
    {
        public int CostId { get; set; }
        [Display(Name = "Вид расхода")]
        public int CostTypeId { get; set; }
        public List<IdNameDto> CostTypes { get; set; }
        [Display(Name = "Количество")]
        public int? Count { get; set; }
        [Display(Name = "Норма по грейду")]
        public decimal? GradeSum { get; set; }
        [Display(Name = "Оплачено фактически")]
        public decimal? UserSum { get; set; }
        [Display(Name = "Оплачено организацией")]
        public decimal? PurchaseBookSum { get; set; }
        [Display(Name = "Принято к учету")]
        public decimal? AccountantSum { get; set; }
    }
}
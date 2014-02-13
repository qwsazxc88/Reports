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
        //[Display(Name = "Количество")]
        //public int? Count { get; set; }
        [Display(Name = "Норма по грейду")]
        public decimal? GradeSum { get; set; }
        [Display(Name = "Оплачено фактически")]
        public decimal? UserSum { get; set; }
        [Display(Name = "Оплачено организацией")]
        public decimal? PurchaseBookSum { get; set; }
        [Display(Name = "Принято к учету")]
        public decimal? AccountantSum { get; set; }
    }
    public class MissionReportEditTranModel
    {
        public int CostId { get; set; }
        public int TranId { get; set; }
        [Display(Name = "Дебет")]
        public int DebitAccountId { get; set; }
        public List<IdNameDto> DebitAccounts { get; set; }
        [Display(Name = "Кредит")]
        public int CreditAccountId { get; set; }
        public List<IdNameDto> CreditAccounts { get; set; }
        [Display(Name = "Сумма")]
        public decimal Sum { get; set; }
    }
}
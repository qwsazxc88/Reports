using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class ConstantListModel
    {
        [Display(Name = "Год")]
        public int Year { get; set; }
        public IList<IdNameDto> Years { get; set; }
        public List<MonthConstModel> Months { get; set; }
        public int FirtsAvailableAddMonth { get; set; }
    }

    public class MonthConstModel
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string MonthName { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
    }
}
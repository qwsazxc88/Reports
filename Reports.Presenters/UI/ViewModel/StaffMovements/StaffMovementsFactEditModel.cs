using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using System.Web;
namespace Reports.Presenters.UI.ViewModel
{
    public class StaffMovementsFactEditModel : StandartEditModel
    {
        public StaffMovementsFactEditModel()
        {
            this.User = new StandartUserDto();
            this.StatusId = 1;
        }
        public IList<AdditionsDto> ActiveAdditions { get; set; }
        public bool IsAgreementAvailable { get; set; }
        public bool IsAgreementAdditionAvailable { get; set; }
        public bool IsOrderAvailable { get; set; }
        public decimal RegionCoefficient {get;set;}
            public decimal Salary {get;set;}
            public decimal Casing { get; set; }
    }
}

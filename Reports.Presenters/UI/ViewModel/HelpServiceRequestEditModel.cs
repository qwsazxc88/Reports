using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class HelpServiceRequestEditModel:HelpUserInfoModel,IContainId
    {
        #region IContainId Members
        public int Id { get; set; }
        public int UserId { get; set; }
        #endregion
        public int Version { get; set; }

        [Display(Name = "Вид услуги")]
        public int TypeId { get; set; }
        public int TypeIdHidden { get; set; }
        public IList<IdNameDto> Types;

        [Display(Name = "Требования к справке")]
        public string Requirements { get; set; }

        [Display(Name = "Период")]
        public int PeriodId { get; set; }
        public int PeriodIdHidden { get; set; }
        public IList<IdNameDto> Periods;

        [Display(Name = "Срок изготовления")]
        public int ProductionTimeTypeId { get; set; }
        public int ProductionTimeTypeIdHidden { get; set; }
        public IList<IdNameDto> ProductionTimeTypes;

        [Display(Name = "Способ передачи")]
        public int TransferMethodTypeId { get; set; }
        public int TransferMethodTypeIdHidden { get; set; }
        public IList<IdNameDto> TransferMethodTypes;
    }
}

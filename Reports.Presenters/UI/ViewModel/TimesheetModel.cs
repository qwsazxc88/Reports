using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class TimesheetModel
    {
        [DataType("TimesheetDto")]
        public TimesheetDto TimesheetDto { get; set; }
        [DataType("TimesheetDtoList")]
        public IList<TimesheetDto> TimesheetDtos { get; set; }
    }
}
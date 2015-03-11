using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
namespace Reports.Presenters.UI.ViewModel
{
    public class AnalyticalStatementDetailsModel : UserInfoModel
    {
        public int SortBy { get; set; }
        public bool? SortDescending { get; set; }

        public IList<AnalyticalStatementDetailsDto> Documents { get; set; }
    }
}

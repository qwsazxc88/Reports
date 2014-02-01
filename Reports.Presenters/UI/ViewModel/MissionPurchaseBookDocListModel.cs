using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class MissionPurchaseBookDocListModel : BeginEndCreateDate
    {
        public int UserId { get; set; }

        [Display(Name = "Период с")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BeginDate { get; set; }

        [Display(Name = "по")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        public int SortBy { get; set; }
        public bool? SortDescending { get; set; }

        public bool HasErrors { get; set; }
        public bool IsAddAvailable { get; set; }

        public IList<MissionPurchaseBookDocDto> Documents { get; set; }
    }
}
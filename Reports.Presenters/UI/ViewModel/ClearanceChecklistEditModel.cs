﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class ClearanceChecklistEditModel
    {
        public int UserId { get; set; }

        public int Id { get; set; }
        public IList<ClearanceChecklistApprovalDto> ClearanceChecklistApprovals { get; set; }
        public DateTime EndDate { get; set; }

        public int SortBy { get; set; }
        public bool? SortDescending { get; set; }
    }
}

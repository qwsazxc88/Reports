using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class PrintRegisterPersonalRecordModel : AbstractEmploymentModel
    {
        public string EmployeeName { get; set; }
        public IList<AttachmentListDto> Attachments { get; set; }

    }
}

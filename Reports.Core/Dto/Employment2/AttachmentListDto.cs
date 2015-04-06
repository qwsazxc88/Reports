using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Reports.Core.Dto.Employment2
{
    public class AttachmentListDto
    {
        public int RowNumber { get; set; }
        public string AttachmentTypeName { get; set; }
        public string AtachmentAvalable { get; set; }
    }
}

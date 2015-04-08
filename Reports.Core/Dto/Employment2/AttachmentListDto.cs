using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Reports.Core.Dto.Employment2
{   
    /// <summary>
    /// Список сканов
    /// </summary>
    public class AttachmentListDto
    {
        public int RowNumber { get; set; }
        public string AttachmentTypeName { get; set; }
        public string AtachmentAvalable { get; set; }
    }
    /// <summary>
    /// Список документов на прием
    /// </summary>
    public class AttachmentNeedListDto
    {
        public int DocTypeId { get; set; }
        public bool IsNeeded { get; set; }
    }
}

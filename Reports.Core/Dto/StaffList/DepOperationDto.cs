using System;
using System.Collections.Generic;


namespace Reports.Core.Dto
{
    public class DepOperationDto
    {
        public int Id { get; set; }
        public int OperationId { get; set; }
        public string OperationName { get; set; }
        public bool IsUsed { get; set; }
    }
}

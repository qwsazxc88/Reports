using System;
using System.Collections.Generic;


namespace Reports.Core.Dto
{
    public class ProgramCodeDto
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public string Code { get; set; }
        public string UserName { get; set; }
    }
}

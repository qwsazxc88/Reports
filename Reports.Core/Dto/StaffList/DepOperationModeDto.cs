using System;
using System.Collections.Generic;


namespace Reports.Core.Dto
{
    public class DepOperationModeDto
    {
        public int Id { get; set; }
        public int DMDetailId { get; set; }
        public int WeekDay { get; set; }
        public string WorkBegin { get; set; }
        public string WorkEnd { get; set; }
        public string BreakBegin { get; set; }
        public string BreakEnd { get; set; }
        public bool IsWorkDay { get; set; }
    }
}

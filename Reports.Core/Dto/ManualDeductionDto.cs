﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class ManualDeductionDto
    {
        public int NPP {get;set;}
        public int UserId {get;set;}
        public string UserName { get; set; }
        public decimal AllSum { get; set; }
        public string DeductionDate { get; set; }
        public string SendTo1C { get; set; }
        public string DeleteDate { get; set; }
        public string Dep3Name { get; set; }
        public string Dep7Name { get; set; }
        public int MissionReportNumber { get; set; }
        public int MissionReportId { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class AccessGroupListDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PositionName { get; set; }
        public string AccessGroupCode { get; set; }
        public string AccessGroupName { get; set; }
        public string Dep7Name { get; set; }
        public string Dep3Name { get; set; }
    }
}
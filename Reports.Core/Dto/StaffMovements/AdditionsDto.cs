using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class AdditionsDto
    {
        public IdNameDto Type { get; set; }
        public decimal Value { get; set; }
        public int Action { get; set; }
    }
}

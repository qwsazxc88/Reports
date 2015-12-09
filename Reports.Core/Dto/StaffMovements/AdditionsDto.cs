using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class AdditionsDto
    {
        public AdditionsDto()
        {
            IsValueEditable = true;
        }
        public IdNameDto Type { get; set; }
        public string guid { get; set; }
        public decimal Value { get; set; }
        public int Action { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEditable { get; set; }
        public bool IsValueEditable { get; set; }
    }
}

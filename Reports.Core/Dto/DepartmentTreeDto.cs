using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class DepartmentTreeDto
    {
        public DepartmentTreeDto() { this.children = new List<DepartmentTreeDto>(); }
        public int id { get; set; }
        public string text { get; set; }
        public int ParentId { get; set; }
        public IList<DepartmentTreeDto> children { get; set; }
    }
}

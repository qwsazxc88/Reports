using System.Collections.Generic;

namespace Reports.Core.Dto
{
    public class DepartmentChildrenDto
    {
        public List<IdNameDto> Children;
        public string Error { get; set; }
    }
}
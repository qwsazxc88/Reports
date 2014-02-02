using System.Collections.Generic;

namespace Reports.Core.Dto
{
    public class DepartmentChildrenDto
    {
        public List<IdNameDto> Children { get; set; }
        public string Error { get; set; }
    }
    public class TerraPointChildrenDto : DepartmentChildrenDto
    {
        public List<IdNameDto> Level3Children { get; set; }
        public string ShortName { get; set; }
    }
    public class TerraPointShortNameDto 
    {
        public string ShortName { get; set; }
        public string Error { get; set; }
    }
    public class ContractorAccountDto
    {
        public string Account { get; set; }
        public string Error { get; set; }
    }
}
using Reports.Core.Domain;
namespace Reports.Core.Dto
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int? ItemLevel { get; set; }
    }

    public class DepartmentWithFigradPointsDto : Department
    {
        //public string CodeSKD { get; set; }
        public string AvailableEmployees { get; set; }
        public string FinDepNameShort { get; set; }
        public string FinDepPointCode { get; set; }
        public string FinDepName { get; set; }
        public string FinDepCode { get; set; }
        public string FinAdminCode { get; set; }
        public string FinBGCode { get; set; }
        public string RPLinkCode { get; set; }
        public string ComplianceSign { get; set; }
    }
}
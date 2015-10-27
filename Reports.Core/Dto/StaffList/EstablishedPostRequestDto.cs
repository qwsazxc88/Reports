using System;
using System.Collections.Generic;

namespace Reports.Core.Dto
{
    public class EstablishedPostRequestDto
    {
        public int Id { get; set; }
        public int SEPId { get; set; }
        public DateTime? DateRequest { get; set; }
        public string RequestTypeName { get; set; }
        public int RequestTypeId { get; set; }
        public int DepartmentId { get; set; }
        public string Dep2Name { get; set; }
        public string Dep3Name { get; set; }
        public string Dep4Name { get; set; }
        public string Dep5Name { get; set; }
        public string Dep6Name { get; set; }
        public string Dep7Name { get; set; }
        public int PersonId { get; set; }
        public string Surname { get; set; }
        public string PositionName { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
    }
}

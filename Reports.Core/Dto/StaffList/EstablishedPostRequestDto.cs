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
        public string Dep3Name { get; set; }
        public string Dep7Name { get; set; }
        public DateTime? BeginAccountDate { get; set; }
        public string DepartmentAccessoryName { get; set; }
        public string TaxAvailable { get; set; }
        public string PositionName { get; set; }
        public int CountNotVacation { get; set; }
        public int CountVacation { get; set; }
        public decimal? SalaryPrev { get; set; }
        public decimal? Salary { get; set; }
        public decimal? RegionalRatePrev { get; set; }
        public decimal? RegionalRate { get; set; }
        public int PersonId { get; set; }
        public string Surname { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
    }
}

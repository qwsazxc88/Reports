using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Dto
{
    public class StaffMovementsDto
    {
        public int NPP { get; set; }
        public DateTime CreateDate { get; set; }
        public int TypeId { get; set; }
        public int Number { get; set; }
        public string Creator { get; set; }
        public string UserName { get; set; }
        public string Position { get; set; }
        public string ManagerName { get; set; }
        public string Dep3Name { get; set; }
        public string Dep7Name { get; set; }
        public string PositionCurrent { get; set; }
        public int PositionCurrentId { get; set; }
        public string PositionTarget { get; set; }
        public int PositionTargetId { get; set; }
        public string SourceManagerName { get; set; }
        public int SourceManagerId { get; set; }
        public string TargetManagerName { get; set; }
        public int TargetManagerId { get; set; }
        public string TargetDep3Name { get; set; }
        public string TargetDep7Name { get; set; }
        public DateTime? MoveDate { get; set; }
        public decimal Salary { get; set; }
        public bool Conjunction { get; set; }
        public string DocsStatus { get; set; }
        public string Status { get; set; }
    }
}

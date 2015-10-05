using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Dto;
using System.ComponentModel.DataAnnotations;
namespace Reports.Presenters.UI.ViewModel
{
    public class StaffMovementsCreateRequestModel
    {
        [Display(Name="Сотрудник")]
        public int UserId { get; set; }
        public string Surname { get; set; }
        public int SourceDepartmentId { get; set; }
        public int TargetDepartmentId { get; set; }
        public string SourceDepartmentName { get; set; }
        public string TargetDepartmentName { get; set; }
        public string TargetPositionName { get; set; }
        public string SourcePositionName { get; set; }
        public int TargetPositionId { get; set; }
        public int SourcePositionId { get; set; }
        public int RequestType { get; set; }
        
        public IList<IdNameDto> TargetPositions { get; set; }
        public IList<IdNameDto> Users { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Domain;


namespace Reports.Presenters.UI.ViewModel.StaffList
{
    public class SelectMovementTypeModel
    {
        [Display(Name = "Id сотрудника")]
        public int UserId { get; set; }

        [Display(Name = "Вид заявки")]
        public int RequestTypeId { get; set; }
        public IList<refStaffMovementsTypes> RequestTypes { get; set; }
    }
}

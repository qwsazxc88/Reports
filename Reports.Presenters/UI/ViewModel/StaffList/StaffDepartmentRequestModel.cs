using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    public class StaffDepartmentRequestModel
    {
        #region Общие реквизиты
        [Display(Name = "Дата заявки")]
        public DateTime? DateRequest { get; set; }

        [Display(Name = "Номер заявки")]
        public int? Id { get; set; }

        [Display(Name = "Вид заявки")]
        public int RequestType { get; set; }
        public IList<IdNameDto> RequestTypes { get; set; }

        [Display(Name = "Id подразделения")]
        public int? DepartmentId { get; set; }
        #endregion
        
    }
}

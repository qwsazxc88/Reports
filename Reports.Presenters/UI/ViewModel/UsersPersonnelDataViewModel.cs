using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Dto;
using System.ComponentModel.DataAnnotations;
namespace Reports.Presenters.UI.ViewModel
{
    public class UsersPersonnelDataViewModel
    {
        [Display(Name="ФИО сотрудника")]
        public string FIO { get; set; }
        [Display(Name="Подразделение")]
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }

        public IList<UserPersonnelDataDto> Documents { get; set; }

        public UsersPersonnelDataViewModel()
        {
            this.Documents=new List<UserPersonnelDataDto>();
        }
    }
}

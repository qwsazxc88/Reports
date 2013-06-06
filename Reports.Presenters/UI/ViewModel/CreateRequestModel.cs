using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class CreateRequestModel
    {
        [Display(Name = "Тип заявки")]
        public int RequestTypeId { get; set; }
        public IList<IdNameDto> RequestTypes;

        public bool IsUserVisible { get; set; }

        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public bool DepartmentReadOnly { get; set; }


        [Display(Name = "За пользователя")]
        public int UserId { get; set; }
        public IList<IdNameDto> Users;

        public CreateRequestModel()
        {
            RequestTypes = new List<IdNameDto>();
            Users = new List<IdNameDto>();
        }
    }
}

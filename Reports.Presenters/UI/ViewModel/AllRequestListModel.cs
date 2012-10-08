using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class AllRequestListModel : BeginEndCreateDate
    {
        public int UserId { get; set; }

        //[Display(Name = "Структурное подразделение")]
        //public int DepartmentId { get; set; }
        //public IList<IdNameDto> Departments;
        /*[Display(Name = "Должность")]
        public int PositionId { get; set; }
        public IList<IdNameDto> Positions;
        [Display(Name = "Вид приема")]
        public int TypeId { get; set; }
        public IList<IdNameDto> Types;

        [Display(Name = "График работы")]
        public int GraphicTypeId { get; set; }
        public IList<IdNameDto> GraphicTypes;*/

        [Display(Name = "Дата создания заявки с")]
        public DateTime? BeginDate { get; set; }
        [Display(Name = "по")]
        public DateTime? EndDate { get; set; }
        //[Display(Name = "Дата составления")]
        //public DateTime? CreateDate { get; set; }
        //[Display(Name = "Дата начала Т Д")]
        //public DateTime? BeginDate { get; set; }

        [Display(Name = "Статус заявки")]
        public int StatusId { get; set; }
        public IList<IdNameDto> Statuses;

        //[Display(Name = "Документы")]
        public IList<AllRequestDto> Documents { get; set; }

    }

    
}
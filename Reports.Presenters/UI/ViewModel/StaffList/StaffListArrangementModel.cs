using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Presenters.UI.ViewModel.StaffList
{
    public class StaffListArrangementModel
    {
        //public IList<DepartmentWithFigradPointsDto> Departments { get; set; }
        public IList<StaffListDepartmentDto> Departments { get; set; }
        //public IList<UsersListItemDto> UserPositions { get; set; }    // должности сотрудников
        public IList<StaffEstablishedPostDto> EstablishedPosts { get; set; } //штатные единицы
        public string DepId { get; set; }
        public int PositionCount { get; set; }
        //public StandartUserDto UserId { get; set; }

        [Display(Name = "Место в штатной расстановке")]
        public int UserLinkId { get; set; }

        [Display(Name = "Отсутствующий сотрудник")]
        public int ReplacedId { get; set; }

        [Display(Name = "Начало периода")]
        public DateTime? TempDateBegin { get; set; }
        public DateTime? DateBegin { get; set; }

        [Display(Name = "Конец периода")]
        public DateTime? TempDateEnd { get; set; }
        public DateTime? DateEnd { get; set; }

        [Display(Name = "Вид длительного отсутствия")]
        public int TempAbsencesTypeId { get; set; }
        public int AbsencesTypeId { get; set; }
        public IList<IdNameDto> AbsencesTypes { get; set; }

        [Display(Name = "Примечание")]
        public string TempNote { get; set; }
        public string Note { get; set; }
       
    }
}

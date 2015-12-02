using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    /// <summary>
    /// Модель для просмотра актов ГПД.
    /// </summary>
    public class GpdActListModel
    {
        [Display(Name = "Поиск по номеру заявки (web)")]
        public int? Id { get; set; }

        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }

        [Display(Name = "Статус акта")]
        public int StatusID { get; set; }
        public IList<GpdActStatusesDto> Statuses { get; set; }

        [Display(Name = "Период с")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? DateBegin { get; set; }

        [Display(Name = " по ")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? DateEnd { get; set; }

        [Display(Name = "Месяц")]
        public int ChargingDateMonth { get; set; }
        public List<IdNameDto> Monthes { get; set; }
        [Display(Name = "Год")]        
        public int? ChargingDateYear { get; set; }
        [Display(Name = "Поиск по ФИО")]
        public string Surname { get; set; }

        [Display(Name = "Поиск по номеру акта в ЭССД")]
        public string ActNumber { get; set; }

        public IList<GpdActDto> Documents { get; set; }
        [Display(Name="По всем видам выплат")]
        public bool GroupAll { get; set; }
        public int CTtype { get; set; }
        [Display(Name="Вид начисления")]
        public IList<IdNameDto> CTTypes { get; set; }
        [Display(Name="Номер карточки")]
        public string CardNumber { get; set; }
        public string errorMessage { get; set; }
        public bool hasErrors { get; set; }
        public bool IsFind { get; set; }
        public int SortBy { get; set; }
        public bool SortDescending { get; set; }

        //права
        public IList<GpdPermissionDto> Permissions { get; set; }
    }
}

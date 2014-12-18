using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    /// <summary>
    /// Модель просмотра договоров ГПД.
    /// </summary>
    public class GpdContractModel
    {
        public int Id { get; set; }
        public int CreatorID { get; set; }
        public string errorMessage { get; set; }

        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }

        [Display(Name = "Статус договора")]
        public int StatusID { get; set; }
        public IList<GpdContractStatusesDto> Statuses { get; set; }

        [Display(Name = "Состояние")]
        public string StatusName { get; set; }

        [Display(Name = "Вид начисления по договору")]
        public int CTID { get; set; }
        public IList<GpdContractChargingTypesDto> ChargingTypes { get; set; }

        [Display(Name = "Вид начисления по договору")]
        public string CTName { get; set; }

        [Display(Name = "Период с")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime DateBegin { get; set; }

        [Display(Name = " по ")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime DateEnd { get; set; }

        [Display(Name = "Поиск по ФИО")]
        public string Surname { get; set; }

        public IList<GpdContractDto> Contracts { get; set; }
        public int PersonID { get; set; }
        public string NumContract { get; set; }
        public string NameContract { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime DateP { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime DatePOld { get; set; }
        public int PayeeID { get; set; }
        public int PayerID { get; set; }
        public string GPDID { get; set;}
        public string PurposePayment { get; set; }
        public string CreatorName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime CreateDate { get; set; }
        public string Autor { get; set; }
        public bool IsFind { get; set; }
        public string DepLevel3Name { get; set; }
        public string DepLevel7Name { get; set; }

        public int SortBy { get; set; }
        public bool? SortDescending { get; set; }


        //права
        public IList<GpdPermissionDto> Permissions { get; set; }
    }
}

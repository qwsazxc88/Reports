using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    /// <summary>
    /// Модель создания/редактирования договоров.
    /// </summary>
    public class GpdContractEditModel
    {
        [Display(Name = "№ документа")]
        public int Id { get; set; }
        public int CreatorID { get; set; }
        [Display(Name = "Ошибка")]
        public string errorMessage { get; set; }

        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }

        [Display(Name = "ФИО")]
        public int PersonID { get; set; }
        public string Surname { get; set; }
        public IList<GpdContractSurnameDto> Persons { get; set; }

        [Display(Name = "Вид начисления по договору")]
        public int CTID { get; set; }
        public IList<GpdContractChargingTypesDto> ChargingTypes { get; set; }

        [Display(Name = "Вид начисления по договору")]
        public string CTName { get; set; }

        [Display(Name = "Наименование договора")]
        public string NameContract { get; set; }

        [Display(Name = "№ договора")]
        public string NumContract { get; set; }

        [Display(Name = "Срок действия договора с")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? DateBegin { get; set; }

        [Display(Name = " по ")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? DateEnd { get; set; }

        [Display(Name = "Дата пролонгации")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? DateP { get; set; }
        public System.DateTime? DatePOld { get; set; }

        [Display(Name = "Плательщик")]
        public int PayerID { get; set; }
        public string PayerName { get; set; }
        public IList<GpdContractDetailDto> Payers { get; set; }

        [Display(Name = "Получатель")]
        public int PayeeID { get; set; }
        public string PayeeName { get; set; }
        public IList<GpdContractDetailDto> Payeers { get; set; }

        [Display(Name = "ID ГПД в ЭССС")]
        public string GPDID { get; set; }

        [Display(Name = "Назначение платежа")]
        public string PurposePayment { get; set; }

        [Display(Name = "Автор")]
        public string Autor { get; set; }

        [Display(Name = "Статус договора")]
        public int StatusID { get; set; }
        public IList<GpdContractStatusesDto> Statuses { get; set; }

        [Display(Name = "Состояние")]
        public string StatusName { get; set; }

        public IList<GpdContractDto> Contracts { get; set; }
        public bool hasErrors { get; set; }
        public string CreatorName { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool IsFind { get; set; }
        public bool IsLong { get; set; }

        //права
        public IList<GpdPermissionDto> Permissions { get; set; }
    }
}

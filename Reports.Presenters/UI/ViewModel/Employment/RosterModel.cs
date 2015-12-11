using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;
using Reports.Core;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class RosterModel: SortableModel
    {
        public bool IsCandidateInfoAvailable { get; set; }
        public bool IsBackgroundCheckAvailable { get; set; }
        public bool IsManagersAvailable { get; set; }
        public bool IsPersonalManagersAvailable { get; set; }

        [Display(Name = "Реестр по приему")]
        public IList<CandidateDto> Roster { get; set; }

        // Фильтры
        [Display(Name = "Структурное подразделение")]
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }

        [Display(Name = "Статус")]
        public int? StatusId { get; set; } //ok
        public IEnumerable<SelectListItem> Statuses { get; set; }

        [Display(Name = "ФИО сотрудника/кандидата")]
        public string UserName { get; set; }

        [Display(Name = "Период с")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BeginDate { get; set; }
        [Display(Name = "по")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Период приема с")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EmploymentDateBegin { get; set; }
        [Display(Name = "по")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EmploymentDateEnd { get; set; }


        [Display(Name = "Номер заявки")]
        public int? CandidateId { get; set; }

        [Display(Name = "Номер документа из 1С")]
        public string ContractNumber1C { get; set; }

        [Display(Name = "Дата создания кандидата")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CompleteDate { get; set; }

        [Display(Name = "Номер отчета из подбора персонала")]
        public string AppointmentReportNumber { get; set; }

        [Display(Name = "Номер заявки из подбора персонала")]
        public int? AppointmentNumber { get; set; }

        [Display(Name = "Поиск по кадровику")]
        public int PersonnelId { get; set; }
        public IList<CandidatePersonnelDto> Personnels { get; set; }

        [Display(Name = "Наличие персональных надбавок")]
        public int? AdditionId { get; set; }
        public IEnumerable<SelectListItem> Additions { get; set; }

        // Перевод списком на бессрочный ТД
        public bool IsBulkChangeContractToIndefiniteAvailable { get; set; }

        // Согласование списком для руководителя
        public bool IsBulkApproveByManagerAvailable { get; set; }

        // Согласование списком для вышестоящего руководителя
        public bool IsBulkApproveByHigherManagerAvailable { get; set; }
        //Метки для оригиналов документов
        public bool IsMarkDocOriginal { get; set; }
        
        
        public RosterModel()
        {
            this.Roster = new List<CandidateDto>();
        }
    }
}

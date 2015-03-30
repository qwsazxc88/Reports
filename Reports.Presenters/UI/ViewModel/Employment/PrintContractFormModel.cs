using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class PrintContractFormModel : AbstractEmploymentModel
    {
        //public string EmployerRepresentativeTemplate { get; set; }
        public string EmployerRepresentativeNameShortened { get; set; }
        public string EmployerRepresentativeName { get; set; }
        public string EmployerRepresentativePosition { get; set; }
        public string EmployerRepresentativePreamblePartyTemplate { get; set; }
        //public string Number { get; set; }
        // public string City { get; set; }
        public DateTime? ContractDate { get; set; }
        //public DateTime? ContractEndDate { get; set; }
        public string ContractNumber { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeNameShortened { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        //public DateTime? EmploymentDate { get; set; }
        public bool IsSecondaryJob { get; set; }
        //public string WorkCity { get; set; }
        public string ProbationaryPeriod { get; set; }
        public decimal? SalaryBasis { get; set; }
        public decimal? AreaAddition { get; set; }
        //public string Schedule { get; set; }
        // Реквизиты подразделения
        public string PassportSeriesNumber { get; set; }
        public string PassportIssuedBy { get; set; }
        public DateTime? PassportDateOfIssue { get; set; }
        public string EmployeeAddress { get; set; }
        //public bool? IsFixedTermContract { get; set; }


        //пункты трудового договора
        //для пукта 1.5
        [Display(Name = "Пункт 1.5 трудового договора")]
        public int? ContractPoint_1_Id { get; set; } //ok

        //для пункта 1.6
        [Display(Name = "Пункт 1.6 трудового договора")]
        public int? ContractPoint_2_Id { get; set; } //ok

        //для пункта 5.1
        [Display(Name = "Пункт 5.1 трудового договора")]
        public int? ContractPoint_3_Id { get; set; } //ok

        //поля вводимые по результатам выбора вариантов пуктов меню
        [StringLength(20, ErrorMessage = "Не более 100 знаков.")]
        public string ContractPointsFio { get; set; }
        [StringLength(20, ErrorMessage = "Не более 150 знаков.")]
        public string ContractPointsAddress { get; set; }

        public PrintContractFormModel()
        {            
        }
    }
}

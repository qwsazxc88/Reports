using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class PrintEmploymentOrderModel : AbstractEmploymentModel
    {
        public string OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? EmploymentDate { get; set; }        
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string Conditions { get; set; }
        public decimal? SalaryBasis { get; set; }
        public string UserCode { get; set; }//табельный номер

        // Надбавки, вводимые руководителем
        [Display(Name = "Персональная надбавка")]
        public decimal? PersonalAddition { get; set; }
        [Display(Name = "Должностная надбавка")]
        public decimal? PositionAddition { get; set; }
        // Надбавки, вводимые кадровиком
        [Display(Name = "Северная надбавка")]
        public decimal? NorthernAreaAddition { get; set; }
        [Display(Name = "Территориальная надбавка")]
        public decimal? AreaAddition { get; set; }
        [Display(Name = "Надбавка за разъездной характер работы")]
        public decimal? TravelRelatedAddition { get; set; }
        [Display(Name = "Надбавка за квалификацию")]
        public decimal? CompetenceAddition { get; set; }
        [Display(Name = "Надбавка за стаж работы специалистом фронт-офиса")]
        public decimal? FrontOfficeExperienceAddition { get; set; }

        public string ProbationaryPeriod { get; set; }
        public bool IsSecondaryJob { get; set; } 
        public string ContractNumber { get; set; }
        public DateTime? ContractDate { get; set; }
        public DateTime? ContractEndDate { get; set; }

        public string EmployerRepresentativeNameShortened { get; set; }
        public string EmployerRepresentativePosition { get; set; }        
        
        public PrintEmploymentOrderModel()
        {            
        }
    }
}

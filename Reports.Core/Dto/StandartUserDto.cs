using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Reports.Core.Dto
{
    public class StandartUserDto
    {
        public string Label { get; set; }
        [Display(Name = "ФИО сотрудника")]
        public string Name { get; set; }
        [Display(Name="Организация")]
        public string Organization { get; set; }
        public int Id { get; set; }
        [Display(Name = "Дирекция 3 ур.")]
        public string Dep3Name { get; set; }
        public int Dep3Id { get; set; }
        [Display(Name = "Подразделение 7 ур.")]
        public string Dep7Name { get; set; }
        public int Dep7Id { get; set; }
        [Display(Name="Должность сотрудника")]
        public string PositionName { get; set; }
        public int PositionId { get; set; }
        public int StaffEstablishedPostId { get; set; }
        [Display(Name = "Ставка")]
        public decimal Salary { get; set; }
        [Display(Name = "Вид расчёта оклада")]
        public int SalaryType { get; set; }
        [Display(Name = "Оклад")]
        public decimal Casing { get; set; }
        /*[Display(Name = "Надбавки")]
        public IList<StaffEstablishedPostChargeLinksDto> Charges { get; set; }*/
        [Display(Name = "Вышестоящие руководители")]
        public IList<string> Chiefs { get; set; }
        [Display(Name = "Кадровики")]
        public IList<string> Personnels { get; set; }
    }
}

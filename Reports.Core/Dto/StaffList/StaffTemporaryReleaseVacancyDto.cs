using System;
using System.Collections.Generic;

namespace Reports.Core.Dto
{
    /// <summary>
    /// Реестр заявок на создание временных вакансий.
    /// </summary>
    public class StaffTemporaryReleaseVacancyDto
    {
        public int Id { get; set; }     //Id заявки
        public int SEPId { get; set; }  //Id штатной единицы
        public int UserLinkId { get; set; } //Id расстановки
        public int ReplacedId { get; set; } //Id отсутствующео сотрудника
        public string Surname { get; set; } //ФИО отсутствующео сотрудника
        public int DepartmentId { get; set; }
        public string Dep3Name { get; set; }
        public string Dep4Name { get; set; }
        public string Dep5Name { get; set; }
        public string Dep6Name { get; set; }
        public string Dep7Name { get; set; }
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public int AbsencesTypeId { get; set; }
        public string AbsencesTypeName { get; set; }
        public DateTime? DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool IsUsed { get; set; }
    }
}

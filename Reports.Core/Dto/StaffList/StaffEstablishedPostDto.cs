using System;
using System.Collections.Generic;

namespace Reports.Core.Dto
{
    /// <summary>
    /// Для списка штатных единиц в пределах подразделения.
    /// </summary>
    public class StaffEstablishedPostDto
    {
        public int Id { get; set; }     //Id строки связи шаттной единицы и пользователя
        public int SEPId { get; set; }  //Id штатной единицы
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public int DepartmentId { get; set; }
        public int Quantity { get; set; }
        public decimal? Salary { get; set; }
        public string Path { get; set; }    //поле из подразделения, используется в дереве
        public int RequestId { get; set; }  //Id действующей заявки
        public int UserId { get; set; }     //Id сотрудника
        public string Surname { get; set; } //ФИО сотрудника (данный класс используется для штатных единиц и расстановки)
        public decimal? Rate { get; set; }  //ставка
        public int ReplacedId { get; set; }     //Id заменяемого сотрудника
        public string ReplacedName { get; set; } //ФИО заменяемого сотрудника + период отпуска
        public int ReserveType { get; set; }        //Тип бронирования вакансии
        public string Reserve { get; set; } //расшифровка бронирования
        public int DocId { get; set; }      //Id документа/заявки
        public bool IsReserve { get; set; } //Признак бронирования 
        public bool? IsPregnant { get; set; }     //признак беременности
        public bool IsVacation { get; set; }    //признак вакансии
        public bool IsSTD { get; set; }     //признак срочного договора
        public bool IsDismiss { get; set; } //есть заявка на увольнение
        public bool IsDismissal { get; set; } //есть признак сокращения в расстановке
        public decimal? SalaryPersonnel { get; set; }
        public decimal? Regional { get; set; }
        public decimal? Personnel { get; set; }
        public decimal? Territory { get; set; }
        public decimal? Front { get; set; }
        public decimal? Drive { get; set; }
        public decimal? North { get; set; }
        public decimal? Qualification { get; set; }
        public decimal? TotalSalary { get; set; }
        public DateTime? DateDistribNote { get; set; }
        public DateTime? DateReceivNote { get; set; }
        public bool IsTemporary { get; set; }
        public DateTime? DateTempBegin { get; set; }
        public DateTime? DateTempEnd { get; set; }
        public string BasicUser { get; set; }   //ФИО постоянного сотрудника
        public string TemporaryMovementUsers { get; set; }   //ФИО временно перемещенных
        public string LongAbsencesUsers { get; set; }   //ФИО длительно отсутствующих
    }

    public class StaffUserLinkDto
    {
        public int Id { get; set; }
        public int SEPId { get; set; }
        public int UserId { get; set; }
        public string Surname { get; set; }
        public bool? IsPregnant { get; set; }
        public bool IsUsed { get; set; }
        public int ReserveType { get; set; }
        public int DocId { get; set; }
        public bool IsDismissal { get; set; }
        public DateTime? DateDistribNote { get; set; }
        public DateTime? DateReceivNote { get; set; }
    }
}

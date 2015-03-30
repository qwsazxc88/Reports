using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reports.Core.Dto.Employment2;
using Reports.Core.Domain;
using Reports.Core.Dao;

namespace Reports.Presenters.UI.ViewModel.Employment2
{
    public class PrintT2Model : AbstractEmploymentModel
    {
        //шапка
        public string Code { get; set; }//табельный номер
        public string Inn { get; set; }//ИНН
        public string SNILS { get; set; }//СНИЛС
        public bool IsSecondaryJob { get; set; }//false - постоянно, true - временно
        public bool IsMale { get; set; }//пол
        public string LastName { get; set; }//фамилия
        public string FirstName { get; set; }//имя
        public string Patronymic { get; set; }//отчество
        public DateTime? ContractDate { get; set; }//дата ТД
        public string ContractNumber { get; set; }//номер ТД
        public DateTime? DateOfBirth { get; set; }//дата рождения
        public string RegionOfBirth { get; set; }//район рождения
        public string CityOfBirth { get; set; }//город рождения
        public string CitizenshipName { get; set; }//название страны (гражданство)
        public IList<ForeignLanguage> ForeignLanguages { get; set; }//иностранные языки
        public IList<HigherEducationDiplomaDto> HigherEducations { get; set; }//сведения об образовании
        public IList<PostGraduateEducationDiploma> PostGraduateEducations { get; set; }//послевузовское образование
        //общий стаж
        public int OverallExperienceYears { get; set; } //год
        public int OverallExperienceMonths { get; set; } //месяц
        public int OverallExperienceDays { get; set; } //день
        //страховой стаж
        public int InsurableExperienceYears { get; set; } //год
        public int InsurableExperienceMonths { get; set; } //месяц
        public int InsurableExperienceDays { get; set; } //день
        //семья
        public string FamilyStatusName { get; set; }//семейное положение
        public IList<FamilyMember> FamilyMembers { get; set; }//состав семьи
    }
}

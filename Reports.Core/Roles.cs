using System;
using System.Collections.Generic;

namespace Reports.Core
{
    // same is id in database
     [Flags]
    public enum UserRole
    {
         /// <summary>
         /// Без роли
         /// </summary>
        NoRole = 0,
         /// <summary>
         /// Администратор
         /// </summary>
        Admin = 1,
         /// <summary>
         /// Сотрудник
         /// </summary>
        Employee = 2, 
         /// <summary>
         /// Руководитель
         /// </summary>
        Manager = 4,
         /// <summary>
         /// Кадровик
         /// </summary>
        PersonnelManager = 8,
         /// <summary>
         /// Бюджет
         /// </summary>
        BudgetManager = 16,
         /// <summary>
         /// Просмотр
         /// </summary>
        OutsourcingManager = 32,
         /// <summary>
         /// Куратор
         /// </summary>
        Inspector = 64,
         /// <summary>
         /// Начальник
         /// </summary>
        Chief = 128,
         /// <summary>
         /// Бухгалтер
         /// </summary>
        Accountant = 256,
         /// <summary>
         /// Член правления
         /// </summary>
        Director = 512,
         /// <summary>
         /// Секретарь
         /// </summary>
        Secretary = 1024,
         /// <summary>
         /// Сотрудник фин.департамента
         /// </summary>
        Findep = 2048,
         /// <summary>
         /// Специалист по подбору персонала
         /// </summary>
        StaffManager = 4096,
         /// <summary>
         /// Архивариус
         /// </summary>
        Archivist = 8192,
         /// <summary>
         /// Кандидат
         /// </summary>
        Candidate = 16384,
         /// <summary>
         /// Сотрудник службы безопасности
         /// </summary>
        Security = 32768,
         /// <summary>
         /// Тренер
         /// </summary>
        Trainer = 65536,
         /// <summary>
         /// Консультант
         /// </summary>
        ConsultantOutsourcing = 131072,
         /// <summary>
         /// Кадровик Банк
         /// </summary>
        ConsultantPersonnel = 262144,
         /// <summary>
        /// Администратор ПО банка
         /// </summary>
        SoftAdmin = 524288,
         /// <summary>
         /// Ответственный за приказы штатного расписания
         /// </summary>
        StaffListOrder = 1048576,
         /// <summary>
         /// Уволеный сотрудник
         /// </summary>
        DismissedEmployee = 2097152,
          /// <summary>
         /// Расчетчик (биллинг) 
         /// </summary>
        Estimator = 4194304,
         /// <summary>
         /// Налоговик
         /// </summary>
        TaxCollector = 8388608
    }

    public enum UserManualRole
    {
        // Согласует приказы на командировку
        ApprovesMissionOrders = 1,
        // Согласует табель
        ApprovesCommonRequests = 2,
        //утверждает прием
        ApprovesEmployment = 3
    }
    
    public class ReportRoleConstants
    {
        public const string Admin = "1";
        public const string Employee = "2";
        public const string Manager = "4";
        public const string PersonnelManager = "8";
        public const string OutsourcingManager = "32";
        public const string Inspector = "64";
        public const string Chief = "128";
        public const string Accountant = "256";
        public const string Director = "512";
        public const string Secretary = "1024";
        public const string Findep = "2048";
        public const string StaffManager = "4096";
        public const string Archivist = "8192";
        public const string Candidate = "16384";
        public const string Security = "32768";
        public const string Trainer = "65536";
        public const string ConsultantOutsourcing = "131072";
        public const string ConsultantPersonnel = "262144";
        public const string SoftAdmin = "524288";
        public const string StaffListOrder = "1048576";
        public const string DismissedEmployee = "2097152";
        public const string Estimator = "4194304";
        //public const string RegisterDoctor = "4";
        //public const string RegisterAdminHosp = "5";
        //public const string GuestAdmin = "7";
        public const string TaxCollector = "8388608";

        public static Dictionary<UserRole, string> Mapper { get; private set; }
        public static Dictionary<int, string> RoleName = new Dictionary<int, string>() 
            {
                {(int)UserRole.NoRole,"Нет роли"},
                {(int)UserRole.Admin,"Администратор"},
                {(int)UserRole.Employee,"Сотрудник"},
                {(int)UserRole.Manager,"Руководитель"},
                {(int)UserRole.PersonnelManager,"Кадровик"},
                {(int)UserRole.BudgetManager,"Бюджет"},
                {(int)UserRole.OutsourcingManager,"Просмотр"},
                {(int)UserRole.Inspector,"Куратор"},
                {(int)UserRole.Chief,"Начальник"},
                {(int)UserRole.Accountant,"Бухгалтер"},
                {(int)UserRole.Director,"Член правления"},
                {(int)UserRole.Secretary,"Секретарь"},
                {(int)UserRole.Findep,"Сотрудник фин.департамента"},
                {(int)UserRole.StaffManager,"Специалист по подбору персонала"},
                {(int)UserRole.Archivist,"Архивариус"},
                {(int)UserRole.Candidate,"Кандидат"},
                {(int)UserRole.Security,"Сотрудник службы безопасности"},
                {(int)UserRole.Trainer,"Тренер"},
                {(int)UserRole.ConsultantOutsourcing,"Консультант"},
                {(int)UserRole.ConsultantPersonnel,"Кадровик Банк"},
                {(int)UserRole.SoftAdmin,"Администратор ПО банка"},
                {(int)UserRole.StaffListOrder,"Ответственный за приказы штатного расписания"},
                {(int)UserRole.DismissedEmployee,"Уволенный сотрудник"},
                {(int)UserRole.Estimator,"Расчётчик"},
                {(int)UserRole.TaxCollector,"Налоговик"}
            };
        static ReportRoleConstants()
        {
            Mapper = new Dictionary<UserRole, string>
                         {
                             {UserRole.Admin, Admin},
                             {UserRole.Employee, Employee},
                             {UserRole.Manager, Manager},
                             {UserRole.PersonnelManager, PersonnelManager},
                             {UserRole.OutsourcingManager, OutsourcingManager},
                             {UserRole.Inspector, Inspector},
                             {UserRole.Chief, Chief},
                             {UserRole.Accountant, Accountant},
                             {UserRole.Director, Director},
                             {UserRole.Secretary, Secretary},
                             {UserRole.Findep, Findep},
                             {UserRole.StaffManager, StaffManager},
                             {UserRole.Archivist, Archivist},
                             {UserRole.Candidate, Candidate},
                             {UserRole.Security, Security},
                             {UserRole.Trainer, Trainer},
                             {UserRole.ConsultantOutsourcing, ConsultantOutsourcing},
                             {UserRole.ConsultantPersonnel, ConsultantPersonnel},
                             {UserRole.SoftAdmin, SoftAdmin},
                             {UserRole.StaffListOrder, StaffListOrder}, 
                             {UserRole.DismissedEmployee, DismissedEmployee},
                             {UserRole.Estimator, Estimator},
                             {UserRole.TaxCollector, TaxCollector}
                             //{SafetyZoneRoles.RegisterAdminHosp, RegisterAdminHosp},
                             //{SafetyZoneRoles.RegisterDoctor, RegisterDoctor},
                             //{SafetyZoneRoles.GuestAdmin, GuestAdmin}
                         };
        }
    }
}

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
         /// Контролер
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
         /// Консультант Банк
         /// </summary>
        ConsultantPersonnel = 262144,
         /// <summary>
         /// Консультант Бухгалтер Банк
         /// </summary>
        ConsultantAccountant = 524288,
         /// <summary>
         /// Консультант ОК
         /// </summary>
        ConsultantOutsorsingManager = 1048576,
         /// <summary>
         /// Уволеный сотрудник
         /// </summary>
        DismissedEmployee = 2097152,
          /// <summary>
         /// Расчетчик (биллинг) 
         /// </summary>
        Estimator = 4194304
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
        public const string ConsultantAccountant = "524288";
        public const string ConsultantOutsorsingManager = "1048576";
        public const string DismissedEmployee = "2097152";
        public const string Estimator = "4194304";
        //public const string RegisterDoctor = "4";
        //public const string RegisterAdminHosp = "5";
        //public const string GuestAdmin = "7";

        public static Dictionary<UserRole, string> Mapper { get; private set; }

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
                             {UserRole.ConsultantAccountant, ConsultantAccountant},
                             {UserRole.ConsultantOutsorsingManager, ConsultantOutsorsingManager},
                             {UserRole.DismissedEmployee, DismissedEmployee},
                             {UserRole.Estimator, Estimator}
                             //{SafetyZoneRoles.RegisterAdminHosp, RegisterAdminHosp},
                             //{SafetyZoneRoles.RegisterDoctor, RegisterDoctor},
                             //{SafetyZoneRoles.GuestAdmin, GuestAdmin}
                         };
        }
    }
}

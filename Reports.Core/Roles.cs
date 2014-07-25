using System;
using System.Collections.Generic;

namespace Reports.Core
{
    // same is id in database
     [Flags]
    public enum UserRole
    {
        NoRole = 0,
        Admin = 1,
        Employee = 2, 
        Manager = 4,
        PersonnelManager = 8,
        BudgetManager = 16,
        OutsourcingManager = 32,
        Inspector = 64,
        Chief = 128,
        Accountant = 256,
        Director = 512,
        Secretary = 1024,
        Findep = 2048,
        StaffManager = 4096,
        Archivist = 8192,
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
                             //{SafetyZoneRoles.RegisterAdminHosp, RegisterAdminHosp},
                             //{SafetyZoneRoles.RegisterDoctor, RegisterDoctor},
                             //{SafetyZoneRoles.GuestAdmin, GuestAdmin}
                         };
        }
    }
}

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
    }
    public class ReportRoleConstants
    {
        public const string Admin = "1";
        public const string Employee = "2";
        public const string Manager = "3";
        public const string PersonnelManager = "4";
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
                             //{SafetyZoneRoles.RegisterAdminHosp, RegisterAdminHosp},
                             //{SafetyZoneRoles.RegisterDoctor, RegisterDoctor},
                             //{SafetyZoneRoles.GuestAdmin, GuestAdmin}
                         };
        }
    }
}

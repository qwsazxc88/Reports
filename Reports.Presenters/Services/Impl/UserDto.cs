﻿using System;
using System.Reflection;
using System.Security.Principal;
using log4net;
using Reports.Core;
using Reports.Core.Domain;

namespace Reports.Presenters.Services.Impl
{

    public class ReportPrincipal:GenericPrincipal
    {
        private readonly IUser info;
        public ReportPrincipal(IIdentity identity, IUser info)
            : base(identity, new []{((int)info.UserRole).ToString()})
        {
            this.info = info;
        }
        public UserRole UserRole
        {
            get
            {
                if (IsInRole(ReportRoleConstants.Admin))
                    return UserRole.Admin;
                if (IsInRole(ReportRoleConstants.Employee))
                    return UserRole.Employee;
                if (IsInRole(ReportRoleConstants.Manager))
                    return UserRole.Manager;
                if (IsInRole(ReportRoleConstants.Inspector))
                    return UserRole.Inspector;
                if (IsInRole(ReportRoleConstants.Chief))
                    return UserRole.Chief;
                //if (IsInRole(ReportRoleConstants.Doctor))
                //    return SafetyZoneRoles.Doctor;
                //if (IsInRole(SafetyZoneRoleConstants.RegisterAdminHosp))
                //    return SafetyZoneRoles.RegisterAdminHosp;
                //if (IsInRole(SafetyZoneRoleConstants.RegisterDoctor))
                //    return SafetyZoneRoles.RegisterDoctor;
                return IsInRole(ReportRoleConstants.PersonnelManager) ?
                       UserRole.PersonnelManager :
                       UserRole.NoRole;
            }
         }
    }

   
    [Serializable]
    public class UserDto : IUser
    {
        protected static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

       
        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public UserRole UserRole { get; set; }
        public bool IsAdministrator { get; set; }

        public static IUser CreateUser(User user )
        {
            return new UserDto
                       {
                           Id = user.Id,
                           Login = user.Login,
                           Name = user.FullName,
                           UserRole = user.UserRole,
                           IsAdministrator = user.IsAdministrator
                           
                       };
        }
        public string Serialize()
        {
            return Id+"$"+Login+"$"+Name+"$"+(int)UserRole+"$"+IsAdministrator;
        }
        public static IUser Deserialize(string data)
        {
            if(string.IsNullOrEmpty(data))
                throw new ArgumentException("Ошибка определения пользователя.");
            string[] parts = data.Split('$');
            if (parts.Length != 5)
            {
                Log.ErrorFormat("Ошибка определения пользователя: {0}",data);
                throw new ArgumentException("Ошибка определения пользователя.");
            }
            try
            {
                return new UserDto
                {
                    Id = int.Parse(parts[0]),
                    Login = parts[1],
                    Name = parts[2],
                    UserRole = (UserRole)int.Parse(parts[3]),
                    IsAdministrator = bool.Parse(parts[4]),
                };

            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Ошибка определения пользователя: {0}", data), ex);
                throw new ArgumentException("Ошибка определения пользователя.");
            }
        }
    }
}
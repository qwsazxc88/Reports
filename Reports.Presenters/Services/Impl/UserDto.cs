using System;
using System.Reflection;
using System.Security.Principal;
using log4net;
using Reports.Core;
using Reports.Core.Domain;
using Reports.Presenters.UI.Bl;


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
                if (IsInRole(ReportRoleConstants.OutsourcingManager))
                    return UserRole.OutsourcingManager;
                if (IsInRole(ReportRoleConstants.Accountant))
                    return UserRole.Accountant;
                if (IsInRole(ReportRoleConstants.Director))
                    return UserRole.Director;
                if (IsInRole(ReportRoleConstants.Secretary))
                    return UserRole.Secretary;
                if (IsInRole(ReportRoleConstants.Findep))
                    return UserRole.Findep;
                if (IsInRole(ReportRoleConstants.ConsultantOutsourcing))
                    return UserRole.ConsultantOutsourcing;
                if (IsInRole(ReportRoleConstants.ConsultantPersonnel))
                    return UserRole.ConsultantPersonnel;
                if (IsInRole(ReportRoleConstants.ConsultantAccountant))
                    return UserRole.ConsultantAccountant;
                //if (IsInRole(ReportRoleConstants.Doctor))
                //    return SafetyZoneRoles.Doctor;
                //if (IsInRole(SafetyZoneRoleConstants.RegisterAdminHosp))
                //    return SafetyZoneRoles.RegisterAdminHosp;
                //if (IsInRole(SafetyZoneRoleConstants.RegisterDoctor))
                //    return SafetyZoneRoles.RegisterDoctor;
                if (IsInRole(ReportRoleConstants.ConsultantOutsorsingManager))
                    return UserRole.ConsultantOutsorsingManager;
                if (IsInRole(ReportRoleConstants.DismissedEmployee))
                    return UserRole.DismissedEmployee;
                if (IsInRole(ReportRoleConstants.Estimator))
                    return UserRole.Estimator;
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

        public static IUser CreateUser(User user,UserRole roleId)
        {
            return new UserDto
                       {
                           Id = user.Id,
                           Login = user.Login,
                           Name = user.FullName,
                           UserRole = roleId,
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
        public static bool IsHelpServiceAvailable(IUser dto)
        {
            return (dto.UserRole & UserRole.Manager) > 0
                   || (dto.UserRole & UserRole.Employee) > 0
                   || (dto.UserRole & UserRole.Admin) > 0
                   || (dto.UserRole & UserRole.ConsultantOutsourcing) > 0
                   || (dto.UserRole & UserRole.OutsourcingManager) > 0
                   || ((dto.UserRole & UserRole.PersonnelManager) > 0 && dto.Id == 10)//для инфоуслуг от роли кадровики нужны только наши расчетчики
                   || (dto.UserRole & UserRole.ConsultantOutsorsingManager) > 0
                   || (dto.UserRole & UserRole.DismissedEmployee) > 0;
        }
        public static bool IsHelpQuestionAvailable(IUser dto)
        {
            return (dto.UserRole & UserRole.Manager) > 0
                   || (dto.UserRole & UserRole.Employee) > 0
                   || (dto.UserRole & UserRole.Admin) > 0
                   || (dto.UserRole & UserRole.ConsultantOutsourcing) > 0
                   || (dto.UserRole & UserRole.ConsultantPersonnel) > 0
                   || (dto.UserRole & UserRole.ConsultantAccountant) > 0
                   || (dto.UserRole & UserRole.OutsourcingManager) > 0
                   //|| (dto.UserRole & UserRole.PersonnelManager) > 0
                   || (dto.UserRole & UserRole.ConsultantOutsorsingManager) > 0
                   || (dto.UserRole & UserRole.DismissedEmployee) > 0;
        }
        public static bool IsHelpPersonnelBillingAvailable(IUser dto)
        {
            return (dto.UserRole & (UserRole.Estimator | UserRole.ConsultantOutsorsingManager | UserRole.OutsourcingManager | UserRole.ConsultantOutsourcing)) > 0 ||
                ((dto.UserRole & UserRole.PersonnelManager) > 0 && dto.Id == 10);
        }
        public static bool IsHelpTemplateAvailable(IUser dto)
        {
            return (dto.UserRole & (UserRole.Candidate | UserRole.Security | UserRole.Trainer)) == 0;
        }
        public static string GetUserRole(IUser dto,out bool isLinkAvailable)
        {
            ILoginBl bl = Ioc.Resolve<ILoginBl>();
            return bl.GetUserRole(dto, out isLinkAvailable);
            //if(dto.UserRole == 0)
            //    return "Нет роли";
            //if((dto.UserRole & UserRole.Accountant) > 0)
            //    return "Бухгалтер";
            //if ((dto.UserRole & UserRole.Admin) > 0)
            //    return "Администратор";
            //if ((dto.UserRole & UserRole.BudgetManager) > 0)
            //    return "Бюджет";
            //if ((dto.UserRole & UserRole.Chief) > 0)
            //    return "Начальник";
            //if ((dto.UserRole & UserRole.Employee) > 0)
            //    return "Сотрудник";
            //if ((dto.UserRole & UserRole.Inspector) > 0)
            //    return "Контролер";
            //if ((dto.UserRole & UserRole.Manager) > 0)
            //    return "Руководитель";
            //if ((dto.UserRole & UserRole.OutsourcingManager) > 0)
            //    return "Аутсорсинг";
            //if  ((dto.UserRole & UserRole.PersonnelManager) > 0)
            //    return "Кадровик";
            //throw new ValidationException(string.Format("Неизвестная роль {0}",(int)dto.UserRole));
        }
    }
}
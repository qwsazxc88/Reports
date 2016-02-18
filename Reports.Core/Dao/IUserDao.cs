using System;
using System.Collections;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IUserDao : IDao<User>
    {
        User FindByLogin(string login);
        void UpdateUserInn(int persid, string inn);
        IList<IdNameDto> GetUsersByTerm(string term);
        string GetUserInn(int persId);
        IList<UserAllRolesDto> GetAllUserRoles(int userId);
        IList<User> GetUsersWhoMailNeeded();
        IList<UserDocsDto> GetAllUserDocs(int userId, int roleId);
        bool CheckUserDismissal(int userid);
        IList<User> FindByCnilc(string cnilc);
        IList<User> FindByEmail(string email);
        bool IsLoginWithOtherIdExists(string login, int id);
        IList<IdNameDto> GetUsersForManagerNotDismissed(int managerId, UserRole managerRole,
            int departmentId, string DismDate);
        IList<User> GetStaffList();
        IList<User> GetUsersWithRole(UserRole role);
        IList<User> LoadUserByCodes(List<string> codes);
        IList<IdNameDto> GetUsersForManager(int managerId, UserRole managerRole,int departmentId);
        IList<IdNameDtoWithDates> GetUsersForManagerWithDate(int userId, UserRole managerRole);
        IList<IdNameDtoWithDates> GetUsersForManagerWithDatePaged(int managerId, UserRole managerRole,
                           DateTime beginDate, DateTime endDate, int departmentId, string userName);
        IList<User> GetUsersForPersonnel(int persId);
        IList<IdNameDto> GetUsersForConsultantBank();
        IList<User> GetUserWithEmailAndRole(UserRole role, string email, string departmentPath, List<int> levelList);
		//User FindByEmail(string email);
		//User FindByCustomerId(string masterCustomerId, string subCustomerId);
        //IList<User> FindByFilter(UserListFilter filter, out int count);
		//IList<User> GetUsersInRole(string roleName);
		//IList<User> GetUsersInDcmRole(ContentManagementRole role, int baseCaseId);
		//ContentManagementRole GetUserRolesForCase(int userId, int baseCaseId);
		//string[] GetUsersLoginInRole(string roleName);
        int Count();
		//ISet<Institution> GetLinkedInstitutions(int userId);
		//IList<User> GetUsersInDcmRoles(ContentManagementRole roles, int maxResults, int firstResult,
		//    string sortExpression, bool sortAscending, out int count);
		//IList<SelectUserDto> LoadDtoListForInstitution(IList<User> list, int institutionId);
        IList<User> LoadForIdsList(ArrayList ids);
        //IList<UsersListItemDto> FindByFilter(UserListFilter filter, out int count);
        //Iesi.Collections.Generic.ISet<IdNameDto> GetAllEntitiesForDictionary(string fieldName);
        IList<UserPersonnelDataDto> GetUserPersonnelData(int CurrentUserId, UserRole role, string fio, int DepId);
        string ConstFKExistsViewName { get;}
        IList<UserDto> GetUsersForManager(string userName, int managerId,
            UserRole managerRole, int? role,ref int currentPage,
            out int numberOfPages);
        IList<User> GetUsersForAdmin(string userName, int role,
                                     ref int currentPage, out int numberOfPages);

        IList<User> GetUsersForPersonnel(string userName, int personnelId, ref int currentPage, out int numberOfPages);

        IList<AcceptRequestDateDto> GetAcceptDatesForManager(int userId, UserRole managerRole,
                                                             DateTime beginDate, DateTime endDate);

        int DeleteEmployees(DateTime date);
        IList<IdNameDto> GetUserListForDeduction(string Name, int UserId);

        IList<IdNameDtoWithDates> GetUsersForManagerWithDatePagedForGraphics(int managerId, UserRole managerRole,
                                                                             DateTime beginDate, DateTime endDate,
                                                                             int departmentId, string userName);
        IList<IdNameDtoWithDates> GetUsersForManagerWithDatePagedForGraphics_NEW(int managerId, UserRole managerRole,
            DateTime beginDate, DateTime endDate, int departmentId, string userName);

        IList<User> LoadForIdsList(List<int> userIds);

        IList<IdNameDto> GetManagersAndEmployeesForCreateMissionOrder(string departmentPath, int level);

        User GetManagerForEmployee(string login);
        IList<IdNameDto> GetMainManagersForLevelDepartment(int level, string departmentPath);

        IList<IdNameDto> GetUsersWithPurchaseBookReportCosts();
        IList<IdNameAddressDto> GetArchivistAddresses();

        IList<IdNameDto> GetManagersWithDepartments();
        IList<IdNameDto> GetEmployeesForCreateHelpServiceRequest(List<int> departments, string Surname);
        /// <summary>
        /// Для автозаполнения.
        /// </summary>
        /// <param name="Surname">Фио сотрудника.</param>
        /// <param name="UserId">Id кадровика</param>
        /// <returns></returns>
        IList<IdNameDto> GetEmployeesForCreateHelpServiceRequestOK(string Surname, int UserId);
        IList<IdNameDto> GetUsersWithRole(UserRole role, bool? isActive);
        /// <summary>
        /// Список всех сотрудников по заданному подразделению
        /// </summary>
        /// <param name="DepartmentId">Id подразделения</param>
        /// <returns></returns>
        IList<User> GetUsersForDepartment(int DepartmentId);
    }
}


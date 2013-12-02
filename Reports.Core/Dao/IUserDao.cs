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
        IList<User> FindByEmail(string email);
        bool IsLoginWithOtherIdExists(string login, int id);
        IList<User> GetUsersWithRole(UserRole role);
        IList<User> LoadUserByCodes(List<string> codes);
        IList<IdNameDto> GetUsersForManager(int managerId, UserRole managerRole,int departmentId);
        IList<IdNameDtoWithDates> GetUsersForManagerWithDate(int userId, UserRole managerRole);
        IList<IdNameDtoWithDates> GetUsersForManagerWithDatePaged(int managerId, UserRole managerRole,
                           DateTime beginDate, DateTime endDate, int departmentId, string userName);

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
        IList<IdNameDto> GetUserListForDeduction();

        IList<IdNameDtoWithDates> GetUsersForManagerWithDatePagedForGraphics(int managerId, UserRole managerRole,
                                                                             DateTime beginDate, DateTime endDate,
                                                                             int departmentId, string userName);

        IList<User> LoadForIdsList(List<int> userIds);
        IList<IdNameDto> GetUsersForCreateMissionOrder(string departmentPath, List<int> levelList, int level);

        IList<IdNameDto> GetManagersAndEmployeesForCreateMissionOrder(string departmentPath, List<int> levelList,
                                                                      int level);

        User GetManagerForEmployee(string login);
    }
}


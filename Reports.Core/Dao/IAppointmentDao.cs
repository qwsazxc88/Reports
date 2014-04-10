using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IAppointmentDao : IDao<Appointment>
    {
        IList<DepartmentDto> GetDepartmentsForManager23(int managerId, int level,bool dep3only);
        IList<int> GetManager3ForManager2(int managerId);
        IList<int> GetChildrenManager2ForManager2(int parentId);
        DepartmentDto GetDepartmentForPathAndLevel(string path, int level);
        User GetParentForManager2(int childId);
        User GetParentForManager3(int childId);
        List<User> GetParentForManager4Department(int departmentId);
        List<User> GetParentForManagerDepartment(int departmentId);

        IList<AppointmentDto> GetDocuments(int userId,
                                           UserRole role,
                                           int departmentId,
                                           int statusId,
                                           DateTime? beginDate,
                                           DateTime? endDate,
                                           string userName,
                                           int sortBy,
                                           bool? sortDescending);
    }
}
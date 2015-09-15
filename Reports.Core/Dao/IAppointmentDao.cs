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
        List<IdNameDto> GetParentForManager2(int childId);
        List<IdNameDto> GetParentForManager3(int childId);
        List<IdNameDto> GetParentForManager4Department(int departmentId);
        List<IdNameDto> GetParentForManagerDepartment(int departmentId);
        List<IdNameDto> GetForManagersParentDepartment(int departmentId);
        IList<AppointmentDto> GetDocuments(int userId,
                                           UserRole role,
                                           int departmentId,
                                           int reasonId,
                                           int statusId,
                                           string number,
                                           DateTime? beginDate,
                                           DateTime? endDate,
                                           string userName,
                                           string RecruterFio,
                                           int sortBy,
                                           bool? sortDescending);
        IList<AppointmentDto> GetReportDocuments(int userId,
                                           UserRole role,
                                           int departmentId,
                                           int statusId,
                                            string number,
                                           DateTime? beginDate,
                                           DateTime? endDate,
                                           string userName,
                                           string CandidateFio,
                                           string RecruterFio,
                                           int sortBy,
                                           bool? sortDescending);
        List<Appointment> GetAppointmentForReasonPosition(int userId);
    }
}
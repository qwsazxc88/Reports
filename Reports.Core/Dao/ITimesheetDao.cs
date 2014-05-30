using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface ITimesheetDao : IDao<Timesheet>
    {
        void CheckAndCreateTimesheetsForMonth(int managerId, DateTime month);
        IList<TimesheetDaysDto> LoadTimesheetsForMonth(int managerId, DateTime month, UserRole role);
        IList<DateTime> GetTimesheetDatesForManager(int managerId, UserRole role);
        void UpdateTimesheetDays(IEnumerable<int> timesheetIds, int statusId,
                                 float hours, int number);
        IList<Timesheet> GetTimesheetListForOwner(int ownerId);
        IList<Timesheet> LoadTimesheetsForMonth(DateTime date);
        IList<DateTime> GetTimesheetDates();

        //IList<RequestDto> GetRequests(DateTime beginDate, DateTime endDate, RequestTypeEnum type,
        //                              int userId, UserRole userRole);
        IList<DayRequestsDto> GetRequestsForMonth(int month, int year, int managerId, UserRole managerRole,
            IList<DayRequestsDto> dtoList, IList<IdNameDtoWithDates> users, IList<WorkingCalendar> workDays,
            IList<TerraGraphicDbDto> tgList,bool setEmptyDays);

        IList<DayRequestsDto> GetRequestsForYear(DateTime beginDate, DateTime year, int managerId,
                                                        UserRole managerRole,
                                                        IList<DayRequestsDto> dtoList, IList<IdNameDtoWithDates> users,
                                                        IList<WorkingCalendar> workDays, IList<TerraGraphicDbDto> tgList);
    }
}
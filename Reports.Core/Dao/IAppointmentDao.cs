using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IAppointmentDao : IDao<Appointment>
    {
        IList<DepartmentDto> GetDepartmentsForManager23(int managerId, int level);
        IList<int> GetManager3ForManager2(int managerId);
    }
}
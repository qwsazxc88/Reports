using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
namespace Reports.Core.Dao
{
    public interface IStaffMovementsDao:IDao<StaffMovements>
    {
        decimal GetUserSalary(int UserId);
        decimal GetUserRegionCoeff(int UserId);
        decimal GetUserTotalSalary(int UserId);
        IList<StaffMovements> GetDocuments(int UserId, UserRole role ,int DepartmentId, string UserName, int Number, int Status, int TypeId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
namespace Reports.Core.Dao
{
    public interface IStaffMovementsFactDao: IDao<StaffMovementsFact>
    {
        IList<StaffMovementsFact> GetDocuments(int UserId, UserRole role, int Number, int SEPReqId, int SMId, int DepartmentId, string userName);
    }
}

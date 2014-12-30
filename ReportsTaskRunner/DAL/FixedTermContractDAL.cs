using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportsTaskRunner.DAL
{
    class FixedTermContractDAL
    {
        public static IList<EmploymentCandidate> GetEmployeesWithExpiringContracts()
        {
            return MainDAL.db.EmploymentCandidate
                .Where(c => c.PersonnelManagers.ContractEndDate.HasValue
                    && (c.PersonnelManagers.ContractEndDate.Value - DateTime.Now).Days == 6
                    && (c.CandidateUser.RoleId & 2) == 2)
                .ToList<EmploymentCandidate>();
        }
    }
}

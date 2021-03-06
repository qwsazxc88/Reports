﻿using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentGeneralInfoDao : DefaultDao<GeneralInfo>, IEmploymentGeneralInfoDao
    {
        public EmploymentGeneralInfoDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }

    public class InsuredPersonTypeDao : DefaultDao<InsuredPersonType>, IInsuredPersonTypeDao
    {
        public InsuredPersonTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
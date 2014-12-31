using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class GpdDetailSetDao : DefaultDao<GpdDetailSet>, IGpdDetailSetDao
    {
        public GpdDetailSetDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

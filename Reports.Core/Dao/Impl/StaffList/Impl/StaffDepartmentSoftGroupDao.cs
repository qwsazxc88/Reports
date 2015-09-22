﻿using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;
using Reports.Core.Services;
using NHibernate.Transform;
using NHibernate;
using NHibernate.Criterion;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Справочник групп банковского ПО.
    /// </summary>
    public class StaffDepartmentSoftGroupDao : DefaultDao<StaffDepartmentSoftGroup>, IStaffDepartmentSoftGroupDao
    {
        public StaffDepartmentSoftGroupDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// <summary>
        /// Список признаков арендованных помещений.
        /// </summary>
        /// <returns></returns>
        public IList<IdNameWithOldNameDto> GetSoftGroups()
        {
            IQuery query = Session.CreateSQLQuery("SELECT Id, Name, Name as OldName FROM StaffDepartmentSoftGroup")
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("Name", NHibernateUtil.String)
                .AddScalar("OldName", NHibernateUtil.String);

            return query.SetResultTransformer(Transformers.AliasToBean<IdNameWithOldNameDto>()).List<IdNameWithOldNameDto>();
        }
    }
}

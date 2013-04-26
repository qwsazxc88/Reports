﻿using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class HolidayWorkDao : DefaultDao<HolidayWork>, IHolidayWorkDao
    {
        public HolidayWorkDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<VacationDto> GetDocuments(
              int userId, 
              UserRole role,
              int departmentId,
              int positionId,
              int typeId,
              int statusId,
            DateTime? beginDate,
            DateTime? endDate
           )
        {
            string sqlQuery =
                string.Format(@"select v.Id as Id,
                         u.Id as UserId,
                         'Оплата праздничных и выходных дней  '+ u.Name + case when [DeleteDate] is not null then N' ({0})' else '' end as Name,
                         v.[CreateDate] as Date    
            from [dbo].[HolidayWork] v
            inner join [dbo].[Users] u on u.Id = v.UserId",DeleteRequestText);

            return GetDefaultDocuments(userId, role, departmentId,
                positionId, typeId,
                statusId, beginDate, endDate, sqlQuery,0,null);
            
            //string whereString = GetWhereForUserRole(role, userId);
            //whereString = GetTypeWhere(whereString, typeId);
            //whereString = GetStatusWhere(whereString, statusId);
            //whereString = GetDatesWhere(whereString, beginDate, endDate);
            //whereString = GetPositionWhere(whereString, positionId);
            //whereString = GetDepartmentWhere(whereString, departmentId);
            //sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString);

            //IQuery query = CreateQuery(sqlQuery);
            //AddDatesToQuery(query, beginDate, endDate);
            //return query.SetResultTransformer(Transformers.AliasToBean(typeof(VacationDto))).List<VacationDto>();
        }
    }
}
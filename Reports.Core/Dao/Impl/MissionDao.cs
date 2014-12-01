using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class MissionDao : DefaultDao<Mission>, IMissionDao
    {
        protected const string sqlMissionSelectForList =
                               @"select v.Id as Id,
                                u.Id as UserId,
                                '{3}' as Name,
                                {2} as Date,  
                                {5} as BeginDate,  
                                {6} as EndDate,  
                                v.Number as Number,
                                u.Name as UserName,
                                t.Name as RequestType,
                                case when v.DeleteDate is not null then '{0}'
                                     when v.SendTo1C is not null then 'Выгружено в 1с' 
                                     when v.PersonnelManagerDateAccept is not null 
                                          and v.ManagerDateAccept is not null 
                                          and v.UserDateAccept is not null 
                                          then 'Согласовано кадровиком'
                                    when  v.PersonnelManagerDateAccept is null 
                                          and v.ManagerDateAccept is not null 
                                          and v.UserDateAccept is not null 
                                          then 'Отправлено кадровику'    
                                    when  -- v.PersonnelManagerDateAccept is null and 
                                          v.ManagerDateAccept is null 
                                          and v.UserDateAccept is not null 
                                          then 'Отправлено руководителю'    
                                    when  v.PersonnelManagerDateAccept is null 
                                          and v.ManagerDateAccept is null 
                                          and v.UserDateAccept is null 
                                          then 'Черновик сотрудника'    
                                    else ''
                                end as RequestStatus,
                                case when v.IsAdditionalOrderExists = 1 then N'Да' else N'Нет' end as IsAdditionalOrderExists,
                                case when ((v.DeleteDate is not null) or (v.IsAdditionalOrderExists = 0)) then null
                                     when AdditionalOrderRecalculateDate is not null then 0
                                     else 1 end as Flag 
                                from {4} v
                                left join {1} t on v.TypeId = t.Id
                                inner join [dbo].[Users] u on u.Id = v.UserId";
        public MissionDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<MissionDto> GetDocuments(
               int userId, 
               UserRole role,
               int departmentId,
               int positionId,
               int typeId,
               int requestStatusId,
               DateTime? beginDate,
               DateTime? endDate,
               string userName, 
               int sortedBy,
               bool? sortDescending)
        {
            string sqlQuery = string.Format(sqlMissionSelectForList,
                                            DeleteRequestText,
                                            "dbo.MissionType",
                                            "v.[CreateDate]",
                                            "Командировка",
                                            "[dbo].[Mission]",
                                            "v.[BeginDate]",
                                            "v.[EndDate]");

            return GetMissionDocuments(userId, role, departmentId,
                positionId, typeId,
                requestStatusId, beginDate, endDate, userName,
                sqlQuery, sortedBy, sortDescending);

        }
        public virtual IList<MissionDto> GetMissionDocuments(
                               int userId,
                               UserRole role,
                               int departmentId,
                               int positionId,
                               int typeId,
                               int statusId,
                               DateTime? beginDate,
                               DateTime? endDate,
                               string userName,
                               string sqlQuery,
                               int sortedBy,
                               bool? sortDescending
           )
        {
            string whereString = GetWhereForUserRole(role, userId, ref sqlQuery);
            whereString = GetTypeWhere(whereString, typeId);
            whereString = GetStatusWhere(whereString, statusId);
            whereString = GetDatesWhere(whereString, beginDate, endDate);
            whereString = GetPositionWhere(whereString, positionId);
            whereString = GetDepartmentWhere(whereString, departmentId);
            whereString = GetUserNameWhere(whereString, userName);
            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString, sortedBy, sortDescending);

            IQuery query = CreateQuery(sqlQuery);
            AddDatesToQuery(query, beginDate, endDate, userName);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(MissionDto))).List<MissionDto>();
        }
        public override IQuery CreateQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("UserId", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("Date", NHibernateUtil.DateTime).
                AddScalar("BeginDate", NHibernateUtil.DateTime).
                AddScalar("EndDate", NHibernateUtil.DateTime).
                AddScalar("Number", NHibernateUtil.Int32).
                AddScalar("UserName", NHibernateUtil.String).
                AddScalar("RequestType", NHibernateUtil.String).
                AddScalar("RequestStatus", NHibernateUtil.String).
                AddScalar("IsAdditionalOrderExists", NHibernateUtil.String).
                AddScalar("Flag", NHibernateUtil.Boolean);
        }

        public override string GetSqlQueryOrdered(string sqlQuery, string whereString,
            int sortedBy,
            bool? sortDescending)
        {
            if (!string.IsNullOrEmpty(whereString))
                sqlQuery += @" where " + whereString;
            if (!sortDescending.HasValue)
                return sqlQuery;
            switch (sortedBy)
            {
                case 0:
                    return sqlQuery;
                case 1:
                    sqlQuery += @" order by Name";
                    break;
                case 2:
                    sqlQuery += @" order by UserName";
                    break;
                case 3:
                    sqlQuery += @" order by Date";
                    break;
                case 4:
                    sqlQuery += @" order by RequestType";
                    break;
                case 5:
                    sqlQuery += @" order by RequestStatus";
                    break;
                case 6:
                    sqlQuery += @" order by Number";
                    break;
                case 7:
                    sqlQuery += @" order by BeginDate";
                    break;
                case 8:
                    sqlQuery += @" order by IsAdditionalOrderExists";
                    break;
                case 9:
                    sqlQuery += @" order by Flag";
                    break;
                /*case 8:
                    sqlQuery += @" order by EndDate";
                    break;
                case 10:
                    sqlQuery += @" order by IsOriginalReceived";
                    break;
                case 11:
                    sqlQuery += @" order by IsPersonnelFileSentToArchive";
                    break;*/
            }
            if (sortDescending.Value)
                sqlQuery += " DESC ";
            else
                sqlQuery += " ASC ";
            //sqlQuery += @" order by Date DESC,Name ";
            return sqlQuery;
        }

        public virtual int SetRecalculateDate(List<int> idsForApprove)
        {
            return Session.CreateSQLQuery
                (@"update [dbo].[Mission]  set AdditionalOrderRecalculateDate=GetDate() where Id in (:entitiesList)").
                SetParameterList("entitiesList", idsForApprove).
                ExecuteUpdate();
        }
    }
}
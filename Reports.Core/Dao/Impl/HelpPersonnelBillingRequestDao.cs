using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class HelpPersonnelBillingRequestDao : DefaultDao<HelpPersonnelBillingRequest>,
                                                  IHelpPersonnelBillingRequestDao
    {
        public const string sqlSelectForRn = @";with res as
                                ({0})
                                select {1} as Number,* from res order by Number ";
        protected const string sqlSelectForPbList =
                                @"select 
                                    v.id
                                    ,v.CreatorId as UserId
                                    ,t.Name as Title
                                    ,un.Name as Urgency
                                    ,v.CreateDate
                                    ,v.UserName as ForUserName
                                    ,dep3.Name as Dep3Name
                                    --,dep7.Name as Dep7Name
                                    ,v.Number as RequestNumber
                                    ,case 
                                    when (u.RoleId & 8 >0 OR u.RoleId & 4194304 >0) then u.Name
                                    else '' end as CreatorName
                                    ,case 
                                    when (u.RoleId & 8 =0 AND u.RoleId & 4194304 =0) then u.Name
                                    else '' end as CreatorName_Bank
                                    --,case when RecipientId = -1 then N'Все расчетчики'
	                                      --when RecipientId = -2 then N'Все консультанты ОК'
	                                      --when uRep.Id is  null then N''
	                                      --else uRep.Name end as RepicientName
                                    ,case when v.[SendDate] is null then 1
                                                                         when v.[SendDate] is not null and v.[BeginWorkDate] is null and v.[EndWorkDate] is null then 2 
                                                                         when v.[SendDate] is not null and v.[BeginWorkDate] is not null and v.[EndWorkDate] is null then 3 
                                                                         when v.[EndWorkDate] is not null then 4
                                                                        else 0
                                                                    end as StatusNumber
                                    ,case when v.[SendDate] is null then N'Черновик'
                                                                         when v.[SendDate] is not null and v.[BeginWorkDate] is null and v.[EndWorkDate] is null then N'Запрос отправлен' 
                                                                         when v.[SendDate] is not null and v.[BeginWorkDate] is not null and v.[EndWorkDate] is null then  N'Запрос прочитан' 
                                                                         when v.[EndWorkDate] is not null then  N'Запрос обработан' 
                                                                        else  N'' 
                                                                    end as Status
                                    ,dbo.fnGetBillingTaskExecutorNames(v.Id) as RepicientName
                                    ,dbo.fnGetBillingTaskExecutorNames_Bank(v.Id) as RepicientName_Bank
                                    from [dbo].[HelpPersonnelBillingRequest] v
                                    inner join [dbo].[HelpBillingTitle] t on t.Id = v.TitleId
                                    inner join [dbo].[HelpBillingUrgency] un on un.Id = v.UrgencyId
                                    inner join [dbo].[Users] u on u.Id = v.CreatorId
                                    left join [dbo].[Department] dep7 on v.DepartmentId = dep7.Id 
                                    left join  [dbo].[Department] dep3 on dep7.Path like dep3.Path+N'%' and dep3.ItemLevel = 3
                                    
                                    ";
       
        public HelpPersonnelBillingRequestDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public override IQuery CreateQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("UserId", NHibernateUtil.Int32).
                AddScalar("Title", NHibernateUtil.String).
                AddScalar("Urgency", NHibernateUtil.String).
                AddScalar("CreateDate", NHibernateUtil.DateTime).
                AddScalar("ForUserName", NHibernateUtil.String).
                AddScalar("Dep3Name", NHibernateUtil.String).
                //AddScalar("Dep7Name", NHibernateUtil.String).
                AddScalar("RequestNumber", NHibernateUtil.Int32).
                AddScalar("CreatorName", NHibernateUtil.String).
                AddScalar("CreatorName_Bank", NHibernateUtil.String).
                AddScalar("StatusNumber", NHibernateUtil.Int32).
                AddScalar("Status", NHibernateUtil.String).
                AddScalar("Number", NHibernateUtil.Int32).
                AddScalar("RepicientName", NHibernateUtil.String).
                AddScalar("RepicientName_Bank", NHibernateUtil.String);
        }
        public List<HelpPersonnelBillingRequestDto> GetDocuments(int userId,
               UserRole role,
               int departmentId,
               int statusId,
               DateTime? beginDate,
               DateTime? endDate,
               string initiatorUserName,
               string workerUserName,
               string  number,
               int titleId,
               int urgencyId,
               int sortBy,
               bool? sortDescending
           )
        {
            string sqlQuery = sqlSelectForPbList;

            string whereString = GetWhereForUserRole(role, userId, ref sqlQuery);
            whereString = GetStatusWhere(whereString, statusId);
            whereString = GetDatesWhere(whereString, beginDate, endDate);
            whereString = GetDepartmentWhere(whereString, departmentId);
            whereString = GetUserNameWhere(whereString, initiatorUserName);
            whereString = GetRecipientNameWhere(whereString, workerUserName);
            whereString = GetNumberWhere(whereString, number);
            whereString = GetTitleAndUrgencyWhere(whereString, titleId,urgencyId);

            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString, sortBy, sortDescending);

            IQuery query = CreateQuery(sqlQuery);
            AddDatesToQuery(query, beginDate, endDate, initiatorUserName);
            if (!string.IsNullOrEmpty(workerUserName))
                query.SetString("recName", "%" + workerUserName.ToLower() + "%");
            if (!string.IsNullOrEmpty(number))
                query.SetString("number", number);
            //query.SetInt32("userId", userId);
            List<HelpPersonnelBillingRequestDto> documentList = query
                .SetResultTransformer(Transformers.AliasToBean(typeof(HelpPersonnelBillingRequestDto)))
                .List<HelpPersonnelBillingRequestDto>().ToList();
            return documentList;
        }
        public override string GetWhereForUserRole(UserRole role, int userId, ref string sqlQuery)
        {
            switch (role)
            {
                case UserRole.ConsultantOutsorsingManager:
                case UserRole.Estimator:
                case UserRole.ConsultantPersonnel:
                case UserRole.PersonnelManager:
                    sqlQuery = string.Format(sqlQuery + " {0} ", @"INNER JOIN (SELECT DISTINCT * 
												                   FROM (SELECT Id AS HelpBillingId, CreatorId as UserId FROM HelpPersonnelBillingRequest
															             UNION ALL
															             SELECT HelpBillingId, UserId FROM HelpBillingExecutorTasks) as tbl) as UB ON UB.HelpBillingId = v.id and UB.UserId = " + userId.ToString());
                    return string.Empty;
                case UserRole.OutsourcingManager:
                case UserRole.ConsultantOutsourcing:
                    return string.Empty;
                default:
                    throw new ArgumentException(string.Format("Invalid user role {0}", role));
            }
        }
        public override string GetSqlQueryOrdered(string sqlQuery, string whereString,
                    int sortedBy,
                    bool? sortDescending)
        {
            string orderBy = string.Empty;
            if (!string.IsNullOrEmpty(whereString))
                sqlQuery += @" where " + whereString;
            if (!sortDescending.HasValue)
            {
                orderBy = " ORDER BY CreateDate DESC,CreatorName";
                return string.Format(sqlSelectForRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy));
            }
            switch (sortedBy)
            {
                case 0:
                    orderBy = " ORDER BY CreateDate DESC,CreatorName";
                    return string.Format(sqlSelectForRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy));
                case 1:
                    orderBy = @" order by Title";
                    break;
                case 2:
                    orderBy = @" order by Urgency";
                    break;
                case 3:
                    orderBy += @" order by CreateDate";
                    break;
                case 4:
                    orderBy = @" order by ForUserName";
                    break;
                case 5:
                    orderBy = @" order by Dep3Name";
                    break;
                case 6:
                    orderBy = @" order by Dep7Name";
                    break;
                case 7:
                    orderBy = @" order by RequestNumber";
                    break;
                case 8:
                    orderBy = @" order by CreatorName";
                    break;
                case 9:
                    orderBy = @" order by RepicientName";
                    break;
                case 10:
                    orderBy = @" order by Status";
                    break;
                case 11:
                    orderBy = @" order by RepicientName_Bank";
                    break;
                case 12:
                    orderBy = @" order by CreatorName_Bank";
                    break; 
                //case 11:
                //    orderBy = @" order by Dep3Name";
                //    break;
                //case 12:
                //    orderBy = @" order by ProdTimeName";
                //    break;
                //case 13:
                //    orderBy = @" order by FiredUserSurname";
                //    break;
                //case 14:
                //    orderBy = @" order by NoteName";
                //    break;
            }
            if (sortDescending.Value)
                orderBy += " DESC ";
            else
                orderBy += " ASC ";
            return string.Format(sqlSelectForRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy));
            //sqlQuery += @" order by Date DESC,Name ";
            //return sqlQuery;
        }
        public override void AddDatesToQuery(IQuery query, DateTime? beginDate,
            DateTime? endDate, string userName)
        {
            if (beginDate.HasValue)
                query.SetDateTime("beginDate", beginDate.Value);
            if (endDate.HasValue)
                query.SetDateTime("endDate", endDate.Value.AddDays(1));
            if (!string.IsNullOrEmpty(userName))
                query.SetString("userName", "%" + userName.ToLower() + "%");
        }
        public virtual string GetTitleAndUrgencyWhere(string whereString, int titleId, int urgencyId)
        {
            if (titleId != 0)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += string.Format("v.[TitleId] = {0} ", titleId);
            }
            if (urgencyId != 0)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += string.Format("v.[UrgencyId] = {0} ", urgencyId);
            }
            return whereString;
        }
        public virtual string GetRecipientNameWhere(string whereString, string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @"LOWER(
                                case when RecipientId = -1 then N'Все расчетчики'
	                                      when RecipientId = -2 then N'Все консультанты ОК'
	                                      else uRep.Name end 
                                ) like :recName";
            }
            return whereString;
        }
        public override string GetStatusWhere(string whereString, int statusId)
        {
            if (statusId != 0)
            {
                string statusWhere;
                switch (statusId)
                {
                    case 1:
                        statusWhere = @"v.[SendDate] is null ";
                        break;
                    case 2:
                        statusWhere = @"v.[SendDate] is not null and v.[BeginWorkDate] is null ";
                        break;
                    case 3:
                        statusWhere = @"v.[BeginWorkDate] is not null and v.[EndWorkDate] is null ";
                        break;
                    case 4:
                        statusWhere = @"v.[EndWorkDate] is not null ";
                        break;
                    default:
                        throw new ArgumentException("Неправильный статус запроса");
                }
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @" " + statusWhere;
                //return whereString;
            }
            return whereString;
        }
        public override string GetDepartmentWhere(string whereString, int departmentId)
        {
            if (departmentId != 0)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += string.Format(@"exists 
                    (select d.ID from dbo.Department d
                     where dep7.Path like d.Path+N'%' and d.Id = {0}) "
                    , departmentId);
            }
            return whereString;
        }
       
    }
}
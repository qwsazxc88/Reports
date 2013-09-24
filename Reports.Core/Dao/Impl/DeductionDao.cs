using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class DeductionDao : DefaultDao<Deduction>, IDeductionDao
    {
        protected const string sqlSelectForDeduction =
                               @"select 
                                v.Id as Id,
                                u.Id as UserId,
                                v.Number,
                                v.EditDate,
                                dep3.Name as Dep3Name,
                                v.Sum,
                                v.DeductionDate
                                u.Name as UserName,
                                p.Name as Position,
                                k.Name as Kind,
                                dep.Name as  Dep7Name,
                                v.DismissalDate,
                                case when v.DeleteDate is not null then N'Отклонена'
                                     when v.SendTo1C is not null then N'Выгружена в 1С' 
                                     else N'Записана'
                                end as Status,
                                case when ((IsFastDismissal is null) or (IsFastDismissal = 0)) then N'Нет'
                                else N'Да'
                                end as IsFastDismissal
                                from dbo.Deduction v
                                -- inner join dbo.DeductionType t on v.TypeId = t.Id
                                inner join dbo.DeductionKind k on v.KindId = k.Id
                                inner join [dbo].[Users] u on u.Id = v.UserId
                                left join dbo.Position p on p.Id = u.PositionId
                                left join dbo.Department dep on u.DepartmentId = dep.Id
                                left join dbo.Department dep3 on dep.[Path] like dep3.[Path]+N'%' and dep3.ItemLevel = 3 ";
        public DeductionDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public virtual IList<VacationDto> GetDocuments(
                                int userId,
                                UserRole role,
                                int departmentId,
                                //int positionId,
                                int typeId,
                                int statusId,
                                DateTime? beginDate,
                                DateTime? endDate,
                                string userName,
                                //string sqlQuery,
                                int sortedBy,
                                bool? sortDescending
            )
        {
            string whereString = GetWhereForUserRole(role, userId);
            whereString = GetTypeWhere(whereString, typeId);
            whereString = GetStatusWhere(whereString, statusId);
            whereString = GetDatesWhere(whereString, beginDate, endDate);
            //whereString = GetPositionWhere(whereString, positionId);
            whereString = GetDepartmentWhere(whereString, departmentId);
            whereString = GetUserNameWhere(whereString, userName);
            string sqlQuery = GetSqlQueryOrdered(sqlSelectForDeduction, whereString, sortedBy, sortDescending);

            IQuery query = CreateQuery(sqlQuery);
            AddDatesToQuery(query, beginDate, endDate, userName);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(VacationDto))).List<VacationDto>();
        }
        public override string GetDatesWhere(string whereString, DateTime? beginDate,
            DateTime? endDate)
        {
            if (beginDate.HasValue)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @"v.[DeductionDate] >= :beginDate ";
            }
            if (endDate.HasValue)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @"v.[DeductionDate] < :endDate ";
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
                    case 1://1, "Записана"
                        statusWhere = @"SendTo1C is null and DeleteDate is null";
                        break;
                    case 2://2, "Выгружена в 1С"
                        statusWhere = @"SendTo1C is not null and DeleteDate is null";
                        break;
                    case 3://3, "Отклонена"
                        statusWhere = @"[DeleteDate] is not null";
                        break;
                   
                    default:
                        throw new ArgumentException("Неправильный статус заявки");
                }
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @" " + statusWhere;
                return whereString;
            }
            return whereString;
        }
        public override string GetWhereForUserRole(UserRole role, int userId)
        {
            switch (role)
            {
                case UserRole.Accountant:
                case UserRole.OutsourcingManager:
                    return string.Empty;
                default:
                    throw new ArgumentException(string.Format("Invalid user role {0}", role));
            }
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
                    sqlQuery += @" order by Number";
                    break;
                //case 2:
                //    sqlQuery += @" order by EditDate";
                //    break;
                case 3:
                    sqlQuery += @" order by EditDate";
                    break;
                case 4:
                    sqlQuery += @" order by Dep3Name";
                    break;
                case 5:
                    sqlQuery += @" order by Sum";
                    break;
                case 6:
                    sqlQuery += @" order by DeductionDate";
                    break;
                case 7:
                    sqlQuery += @" order by UserName";
                    break;
                case 8:
                    sqlQuery += @" order by Position";
                    break;
                case 9:
                    sqlQuery += @" order by Kind";
                    break;
                case 10:
                    sqlQuery += @" order by Dep7Name";
                    break;
                case 11:
                    sqlQuery += @" order by DismissalDate";
                    break;
                case 12:
                    sqlQuery += @" order by Status";
                    break;
                case 13:
                    sqlQuery += @" order by IsFastDismissal";
                    break;
            }
            if (sortDescending.Value)
                sqlQuery += " DESC ";
            else
                sqlQuery += " ASC ";
            //sqlQuery += @" order by Date DESC,Name ";
            return sqlQuery;
        }
    }
}
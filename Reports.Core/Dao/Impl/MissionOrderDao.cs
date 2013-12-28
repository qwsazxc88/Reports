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
    public class MissionOrderDao : DefaultDao<MissionOrder>, IMissionOrderDao
    {
        protected IUserDao userDao;
        public IUserDao UserDao
        {
            get { return Validate.Dependency(userDao); }
            set { userDao = value; }
        }

        protected const string sqlSelectForMissionOrderRn = @";with res as
                                ({0})
                                select {1} as Number,* from res order by Number ";
        public override IQuery CreateQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("UserId", NHibernateUtil.Int32).
                AddScalar("UserName", NHibernateUtil.String).
                AddScalar("Dep7Name", NHibernateUtil.String).
                AddScalar("OrderNumber", NHibernateUtil.Int32).
                AddScalar("EditDate", NHibernateUtil.DateTime).
                AddScalar("MissionType", NHibernateUtil.String).
                AddScalar("Target", NHibernateUtil.String).
                AddScalar("Grade", NHibernateUtil.Int32).
                AddScalar("GradeSum", NHibernateUtil.Decimal).
                AddScalar("GradeIncrease", NHibernateUtil.Decimal).
                AddScalar("UserSum", NHibernateUtil.Decimal).
                AddScalar("HasMission", NHibernateUtil.String).
                AddScalar("NeedSecretary", NHibernateUtil.String).
                AddScalar("State", NHibernateUtil.String).
                AddScalar("BeginDate", NHibernateUtil.DateTime).
                AddScalar("EndDate", NHibernateUtil.DateTime).
                AddScalar("Flag", NHibernateUtil.Boolean).
                AddScalar("Number", NHibernateUtil.Int32);
        }
        protected const string sqlSelectForMoList =
                                @"select v.Id as Id,
                                u.Id as UserId,
                                u.Name as UserName,
                                dep.Name as Dep7Name,
                                v.Number as OrderNumber,
                                EditDate as EditDate,
                                t.Name as MissionType,  
                                [dbo].[fnGetMissionOrderTargetsCities](v.Id) as Target,
                                u.Grade as Grade,
                                v.AllSum as GradeSum,
                                v.AllSum - v.UserAllSum   
                                - case when IsResidencePaid = 1 then isnull(SumResidence,0) else 0 end
                                - case when IsAirTicketsPaid = 1 then isnull(SumAir,0) else 0 end
                                - case when IsTrainTicketsPaid = 1 then isnull(SumTrain,0) else 0 end
                                as GradeIncrease,
                                v.UserAllSum as UserSum,
                                case when v.MissionId is null then N'Нет' else N'Да' end as HasMission, 
                                case when ((NeedToAcceptByChief = 1 and v.ChiefDateAccept is not null) or
                                          (NeedToAcceptByChief = 0 and v.UserDateAccept is not null))
                                           and v.DeleteDate is null and v.SendTo1C is null
                                           and 
                                           ( (IsResidencePaid = 1 and ResidenceRequestNumber is null) or
                                             (IsAirTicketsPaid = 1 and AirTicketsRequestNumber is null) or
                                             (IsTrainTicketsPaid = 1 and TrainTicketsRequestNumber is null))
                                    then N'Заказ' else N'' end  as NeedSecretary,
                                case when v.DeleteDate is not null then N'Отклонен'
                                     when v.SendTo1C is not null then N'Выгружен в 1с' 
                                     when v.ChiefDateAccept is not null 
                                          -- and v.ManagerDateAccept is not null 
                                          -- and v.UserDateAccept is not null 
                                          then N'Согласован'
                                    when  NeedToAcceptByChief = 0
                                          and v.ManagerDateAccept is not null 
                                          and v.UserDateAccept is not null 
                                          then N'Согласован'    
                                    when  v.ChiefDateAccept is null 
                                          and NeedToAcceptByChief = 1
                                          and v.ManagerDateAccept is not null 
                                          and v.UserDateAccept is not null 
                                          then N'Отправлен члену правления'    
                                    when  v.ManagerDateAccept is null 
                                          and v.UserDateAccept is not null 
                                          then N'Отправлен руководителю'    
                                    when  v.UserDateAccept is null 
                                          then N'Черновик сотрудника'    
                                    else N''
                                end as State,
                                v.BeginDate as BeginDate,  
                                v.EndDate as EndDate,
                                {0}  
                                from dbo.MissionOrder v
                                left join dbo.MissionType t on v.TypeId = t.Id
                                inner join [dbo].[Users] u on u.Id = v.UserId
                                inner join dbo.Department dep on u.DepartmentId = dep.Id
                                {1}";
        public MissionOrderDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<MissionOrderDto> GetDocuments(int userId,
                UserRole role,
                int departmentId,
                int statusId,
                DateTime? beginDate,
                DateTime? endDate,
                string userName,
                int sortBy,
                bool? sortDescending)
        {
            string sqlQuery = sqlSelectForMoList;
            string whereString = GetWhereForUserRole(role, userId,ref sqlQuery);
            //whereString = GetTypeWhere(whereString, typeId);
            whereString = GetStatusWhere(whereString, statusId);
            whereString = GetDatesWhere(whereString, beginDate, endDate);
            //whereString = GetPositionWhere(whereString, positionId);
            whereString = GetDepartmentWhere(whereString, departmentId);
            whereString = GetUserNameWhere(whereString, userName);
            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString, sortBy, sortDescending);

            IQuery query = CreateQuery(sqlQuery);
            AddDatesToQuery(query, beginDate, endDate, userName);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(MissionOrderDto))).List<MissionOrderDto>();
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
                orderBy = " ORDER BY UserName,EditDate DESC";
                return string.Format(sqlSelectForMissionOrderRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy));
            }
            switch (sortedBy)
            {
                case 0:
                    orderBy = " ORDER BY UserName,EditDate DESC";
                    return string.Format(sqlSelectForMissionOrderRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy));
                case 1:
                    orderBy = @" order by UserName";
                    break;
                case 2:
                    orderBy += @" order by Dep7Name";
                    break;
                case 3:
                    orderBy = @" order by OrderNumber";
                    break;
                case 4:
                    orderBy = @" order by EditDate";
                    break;
                case 5:
                    orderBy = @" order by MissionType";
                    break;
                case 6:
                    orderBy = @" order by Target";
                    break;
                case 7:
                    orderBy = @" order by Grade";
                    break;
                case 8:
                    orderBy = @" order by GradeSum";
                    break;
                case 9:
                    orderBy = @" order by GradeIncrease";
                    break;
                case 10:
                    orderBy = @" order by UserSum";
                    break;
                case 11:
                    orderBy = @" order by HasMission";
                    break;
                case 12:
                    orderBy = @" order by State";
                    break;
                case 13:
                    orderBy = @" order by BeginDate,EndDate";
                    break;
                case 14:
                    orderBy = @" order by NeedSecretary";
                    break;
            }
            if (sortDescending.Value)
                orderBy += " DESC ";
            else
                orderBy += " ASC ";
            return string.Format(sqlSelectForMissionOrderRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy));
            //sqlQuery += @" order by Date DESC,Name ";
            //return sqlQuery;
        }

        public virtual string GetWhereForUserRole(UserRole role, int userId, ref string sqlQuery)
        {
            switch (role)
            {
                case UserRole.Employee:
                    sqlQuery = string.Format(sqlQuery, @" 0 as Flag", string.Empty);
                    return string.Format(" u.Id = {0} ", userId);
                case UserRole.Manager:
                    User currentUser = UserDao.Load(userId);
                    if(currentUser == null)
                        throw new ArgumentException(string.Format("Не могу загрузить пользователя {0} из базы даннных",userId));

                    string sqlQueryPartTemplate =
                        @" u.Id in (        select distinct emp.Id from dbo.Users emp
                                            inner join dbo.Users manU on manU.Login = emp.Login+N'R' and manU.RoleId = 4 
                                             inner join dbo.Department dManU on manU.DepartmentId = dManU.Id and
                                             ((manU.[level] in ({0})) or ((manU.[level] = {1}) and (manU.IsMainManager = 0)))
                                             inner join dbo.Department dMan on dManU.Path like dMan.Path+N'%'
                                             inner join dbo.Users man on man.DepartmentId = dMan.Id and man.Id = {2}";
                    string sqlQueryPart = string.Empty;
                    string sqlFlag = string.Empty;     
                    switch (currentUser.Level)
                    {
                        case 2:
                            sqlQueryPart = string.Format(sqlQueryPartTemplate, "3", "2",currentUser.Id);
                            sqlFlag = @"case when v.UserDateAccept is not null 
                                        and  v.ManagerDateAccept is null then 1 else 0 end as Flag";
                            break;
                        case 3:
                            sqlFlag = @"case when v.UserDateAccept is not null 
                                        and v.ManagerDateAccept is null then 1 else 0 end as Flag";
                            sqlQueryPart = string.Format(sqlQueryPartTemplate, "4,5", "3", currentUser.Id);
                            break;
                        case 4:
                            sqlQueryPart = string.Format(sqlQueryPartTemplate, "5", "4", currentUser.Id);
                            sqlFlag = "0 as Flag";
                            break;
                        case 5:
                        case 6:
                            sqlQueryPartTemplate += @" union 
                             select distinct emp1.Id from dbo.Users emp1
                             inner join dbo.Department dEmp1 on emp1.DepartmentId = dEmp1.Id 
                             and  ((emp1.RoleId & 2) > 0) 
                             inner join dbo.Department dMan on dEmp1.Path like dMan.Path+N'%'
                             inner join dbo.Users man on man.DepartmentId = dMan.Id and man.Id = {2}
                             where not exists (select Id from dbo.Users empMan1 where
                                empMan1.RoleId = 4 and empMan1.Login = emp1.Login+N'R')";
                            if (currentUser.Level == 5)
                            {
                                sqlQueryPart = string.Format(sqlQueryPartTemplate, "6", "5", currentUser.Id);
                                sqlFlag = @"case when v.UserDateAccept is not null 
                                            and  v.ManagerDateAccept is null then 1 else 0 end as Flag";
                            }
                            else
                            {
                                sqlQueryPart = string.Format(sqlQueryPartTemplate, "-1", "6", currentUser.Id);
                                sqlFlag = "0 as Flag";
                            }
                            break;
                    }
                    sqlQuery = string.Format(sqlQuery, sqlFlag, string.Empty);
                    return sqlQueryPart+" ) ";
                case UserRole.Director:
                        //User currUser = UserDao.Load(userId);
                        //if(currUser == null)
                        //    throw new ArgumentException(string.Format("Не могу загрузить пользователя {0} из базы даннных",userId));
                        const string sqlFlagD = @"case when (
                                            (v.UserDateAccept is not null 
                                            and  v.ManagerDateAccept is not null 
                                            and  v.[ChiefDateAccept] is null 
                                            and  v.[NeedToAcceptByChief] = 1) 
                                            or
                                            (v.UserDateAccept is not null 
                                             and  v.ManagerDateAccept is null 
                                             and v.[NeedToAcceptByChiefAsManager] = 1)
                                            )
                                            then 1 else 0 end as Flag";
                        sqlQuery = string.Format(sqlQuery, sqlFlagD, string.Empty);
                        return @"   -- ((u.Id in ( select distinct emp.Id from dbo.Users emp
                                    --        inner join dbo.Users manU on manU.Login = emp.Login+N'R' and manU.RoleId = 4 
                                    --        and manU.[level] = 2 and manU.IsMainManager = 1 )
                                             -- inner join dbo.Department dManU on manU.DepartmentId = dManU.Id and
                                             -- ((manU.[level] in ({0})) or ((manU.[level] = {1}) and (manU.IsMainManager = 0)))
                                             -- inner join dbo.Department dMan on dManU.Path like dMan.Path+N'%'
                                             -- inner join dbo.Users man on man.DepartmentId = dMan.Id and man.Id = {2} 
                                    --  )
                                    --  or 
                                       ((v.[NeedToAcceptByChief] = 1) or (v.[NeedToAcceptByChiefAsManager] = 1))  ";
                case UserRole.Accountant:
                case UserRole.OutsourcingManager:
                case UserRole.Secretary:
                case UserRole.Findep:
                    sqlQuery = string.Format(sqlQuery, @" 0 as Flag", string.Empty);
                    return string.Empty;
                default:
                    throw new ArgumentException(string.Format("Invalid user role {0}", role));
            }
        }
        public override string GetDatesWhere(string whereString, DateTime? beginDate,
            DateTime? endDate)
        {
            if (beginDate.HasValue)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @"v.[EditDate] >= :beginDate ";
            }
            if (endDate.HasValue)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @"v.[EditDate] < :endDate ";
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
                    //case 1:
                    //    statusWhere =
                    //        @"UserDateAccept is null and ManagerDateAccept is null and PersonnelManagerDateAccept is null and SendTo1C is null";
                    //    break;
                    case 1:
                        statusWhere = @"UserDateAccept is not null";
                        break;
                    case 2:
                        statusWhere = @"UserDateAccept is null";
                        break;
                    case 3:
                        statusWhere = @"ManagerDateAccept is not null and NeedToAcceptByChiefAsManager = 0";
                        break;
                    case 4:
                        statusWhere = @"ManagerDateAccept is null and NeedToAcceptByChiefAsManager = 0";
                        break;
                    case 5:
                        statusWhere = @"(ChiefDateAccept is not null and NeedToAcceptByChief = 1) or
                                        (ManagerDateAccept is not null and NeedToAcceptByChiefAsManager = 1)";
                        break;
                    case 6:
                        statusWhere = @"(ChiefDateAccept is null  and NeedToAcceptByChief = 1) or
                                        (ManagerDateAccept is null and NeedToAcceptByChiefAsManager = 1)";
                        break;
                    case 7:
                        statusWhere = @"UserDateAccept is not null and ManagerDateAccept is null
                                        and NeedToAcceptByChiefAsManager = 0";
                        break;
                    case 8:
                        statusWhere = @"UserDateAccept is not null and ((ManagerDateAccept is null and NeedToAcceptByChiefAsManager = 1) 
                                        or (ManagerDateAccept is not null and ChiefDateAccept is null and NeedToAcceptByChief = 1))";
                        break;
                    //case 8:
                    //    statusWhere =
                    //        @"UserDateAccept is not null and ManagerDateAccept is not null and PersonnelManagerDateAccept is not null";
                    //    break;
                    //case 10:
                    //    statusWhere = @"[DeleteDate] is not null";
                    //    break;
                    //case 9:
                    //    statusWhere = @"SendTo1C is not null";
                    //    break;
                    default:
                        throw new ArgumentException("Неправильный статус заявки");
                }
                //if (statusId != 10)
                //    statusWhere += " and DeleteDate is null ";
                //if (statusId != 9 && statusId != 10)
                //    statusWhere += " and SendTo1C is null ";
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @" " + statusWhere;


                return whereString;
            }
            return whereString;
        }

        public IList<MissionOrder> LoadForIdsList(List<int> ids)
        {
            if (ids.Count == 0)
                return new List<MissionOrder>();
            ICriteria criteria = Session.CreateCriteria(typeof(MissionOrder));
            criteria.Add(Restrictions.In("Id", ids));
            return criteria.List<MissionOrder>();
        }
      
    }
}
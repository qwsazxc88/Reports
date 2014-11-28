using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;
using System.Linq;

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

        public const string StrInvalidManagerLevel = "Неверный уровень руководителя (id {0}) {1} в базе даннных.";

        public const string sqlSelectForMissionOrderRn = @";with res as
                                ({0})
                                select {1} as Number,* from res order by Number ";
        public override IQuery CreateQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("UserId", NHibernateUtil.Int32).
                AddScalar("UserName", NHibernateUtil.String).
                AddScalar("Position", NHibernateUtil.String).
                AddScalar("Dep7Name", NHibernateUtil.String).
                AddScalar("OrderNumber", NHibernateUtil.Int32).
                AddScalar("EditDate", NHibernateUtil.DateTime).
                AddScalar("IsRecalculated", NHibernateUtil.Boolean).
                AddScalar("AdditionalOrderId", NHibernateUtil.Int32).
                AddScalar("AdditionalOrderNumber", NHibernateUtil.String).
                AddScalar("AdditionalOrderEditDate", NHibernateUtil.DateTime).
                AddScalar("MissionGoal", NHibernateUtil.String).
                AddScalar("MissionType", NHibernateUtil.String).
                AddScalar("MissionKind", NHibernateUtil.String).
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
                AddScalar("AdditionalOrderState", NHibernateUtil.String).
                AddScalar("AdditionalOrderBeginDate", NHibernateUtil.DateTime).
                AddScalar("AdditionalOrderEndDate", NHibernateUtil.DateTime).
                AddScalar("Flag", NHibernateUtil.Boolean).
                AddScalar("Number", NHibernateUtil.Int32).
                AddScalar("AirTicketType", NHibernateUtil.String).
                AddScalar("TrainTicketType", NHibernateUtil.String);
        }
        protected const string sqlSelectForMoList =
                                @"select v.Id as Id,
                                u.Id as UserId,
                                u.Name as UserName,
                                up.Name as Position,
                                dep.Name as Dep7Name,
                                v.Number as OrderNumber,
                                v.EditDate as EditDate,
                                v.IsRecalculated as IsRecalculated,
                                ao.Id as AdditionalOrderId,
                                case when ao.Id is null then null 
                                     else cast(ao.Number as nvarchar(10))+N'-изм' end as AdditionalOrderNumber,
                                ao.EditDate as AdditionalOrderEditDate,
                                mg.Name as MissionGoal,
                                t.Name as MissionType,  
                                case when v.Kind = 1 then  N' Внутренняя'
                                     when v.Kind = 2 then  N' Внешняя'
                                     else N'' end
                                as MissionKind,  
                                [dbo].[fnGetMissionOrderTargetsCities](v.Id) as Target,
                                u.Grade as Grade,
                                v.AllSum as GradeSum,
                                case when (v.UserAllSum - v.AllSum 
                                    + case when  v.IsResidencePaid = 1 then isnull( v.SumResidence,0) else 0 end
                                    + case when  v.IsAirTicketsPaid = 1 then isnull( v.SumAir,0) else 0 end
                                    + case when  v.IsTrainTicketsPaid = 1 then isnull( v.SumTrain,0) else 0 end) > 0
                                    then v.UserAllSum - v.AllSum 
                                    + case when  v.IsResidencePaid = 1 then isnull( v.SumResidence,0) else 0 end
                                    + case when  v.IsAirTicketsPaid = 1 then isnull( v.SumAir,0) else 0 end
                                    + case when  v.IsTrainTicketsPaid = 1 then isnull( v.SumTrain,0) else 0 end
                                    else null end
                                as GradeIncrease,
                                v.UserAllSum as UserSum,
                                case when v.MissionId is null then N'Нет' else N'Да' end as HasMission, 
                                case when (( v.NeedToAcceptByChief = 1 and v.ChiefDateAccept is not null) or
                                          ( v.NeedToAcceptByChief = 0 and v.UserDateAccept is not null))
                                           and v.DeleteDate is null /*and v.SendTo1C is null*/
                                           and 
                                           ( ( v.IsResidencePaid = 1 and  v.ResidenceRequestNumber is null) or
                                             ( v.IsAirTicketsPaid = 1 and  v.AirTicketsRequestNumber is null) or
                                             ( v.IsTrainTicketsPaid = 1 and  v.TrainTicketsRequestNumber is null))
                                    then N'Заказ' else N'' end  as NeedSecretary,
                                case when v.DeleteDate is not null then N'Отклонен'
                                     when v.SendTo1C is not null then N'Выгружен в 1с' 
                                     when v.ChiefDateAccept is not null 
                                          -- and v.ManagerDateAccept is not null 
                                          -- and v.UserDateAccept is not null 
                                          then N'Согласован'
                                    when  v.NeedToAcceptByChief = 0
                                          and v.ManagerDateAccept is not null 
                                          and v.UserDateAccept is not null 
                                          then N'Согласован'    
                                    when  v.ChiefDateAccept is null 
                                          and v.NeedToAcceptByChief = 1
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
                                case when ao.Id is null then null
                                     when ao.DeleteDate is not null then N'Отклонен'
                                     when ao.SendTo1C is not null then N'Выгружен в 1с' 
                                     when ao.ChiefDateAccept is not null 
                                          -- and v.ManagerDateAccept is not null 
                                          -- and v.UserDateAccept is not null 
                                          then N'Согласован'
                                    when  ao.NeedToAcceptByChief = 0
                                          and ao.ManagerDateAccept is not null 
                                          and ao.UserDateAccept is not null 
                                          then N'Согласован'    
                                    when  ao.ChiefDateAccept is null 
                                          and ao.NeedToAcceptByChief = 1
                                          and ao.ManagerDateAccept is not null 
                                          and ao.UserDateAccept is not null 
                                          then N'Отправлен члену правления'    
                                    when  ao.ManagerDateAccept is null 
                                          and ao.UserDateAccept is not null 
                                          then N'Отправлен руководителю'    
                                    when  ao.UserDateAccept is null 
                                          then N'Черновик сотрудника'    
                                    else N''
                                end as AdditionalOrderState,
                                case when ao.Id is null then null else ao.BeginDate end as AdditionalOrderBeginDate,  
                                case when ao.Id is null then null else ao.EndDate end as AdditionalOrderEndDate,
                                case when v.AirTicketType = 1 then N'Бизнес'
                                     when v.AirTicketType = 2 then N'Эконом'
                                     else N'' end as AirTicketType,
                                case when v.TrainTicketType = 1 then N'Купе'
                                     when v.TrainTicketType = 2 then N'СВ'
                                     else N'' end as TrainTicketType,
                                {0}  
                                from dbo.MissionOrder v
                                left join dbo.MissionType t on v.TypeId = t.Id
                                left join dbo.MissionOrder ao on v.Id = ao.MainOrderId
                                inner join [dbo].[Users] u on u.Id = v.UserId
                                left join [dbo].[Position]  up on up.Id = u.PositionId
                                left join [dbo].[MissionGoal]  mg on mg.Id = v.MissionGoalId
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
                string number,
                int sortBy,
                bool? sortDescending)
        {
            string sqlQuery = sqlSelectForMoList;
            string whereString = GetWhereForUserRole(role, userId,ref sqlQuery);
            if (whereString.Length > 0)
                whereString += @" and ";
            if(role != UserRole.Director)
                whereString += @" v.IsAdditional = 0 ";
            else
                whereString += @" ((v.IsAdditional = 0) or (ao.NeedToAcceptByChief = 1)) ";
            //whereString = GetTypeWhere(whereString, typeId);
            whereString = GetStatusWhere(whereString, statusId);
            whereString = GetDatesWhere(whereString, beginDate, endDate);
            //whereString = GetPositionWhere(whereString, positionId);
            whereString = GetDepartmentWhere(whereString, departmentId);
            whereString = GetUserNameWhere(whereString, userName);
            whereString = GetNumberWhere(whereString, number);
            //
            // whereString += String.Format(" or u.Id in (select morr.TargetUserId from [dbo].[MissionOrderRoleRecord] morr where morr.UserId = {0})", userId);
            // whereString += String.Format(" or u.DepartmentId in (select morr.TargetDepartmentId from [dbo].[MissionOrderRoleRecord] morr where morr.UserId = {0})", userId);
            //
            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString, sortBy, sortDescending);

            IQuery query = CreateQuery(sqlQuery);
            AddDatesToQuery(query, beginDate, endDate, userName);
            if (!string.IsNullOrEmpty(number))
                query.SetString("number", number);
            IList<MissionOrderDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(MissionOrderDto))).List<MissionOrderDto>();
            #region Deleted
            /*
            sqlQuery = sqlSelectForMoList;
            whereString = GetWhereForUserRole(role, userId, ref sqlQuery);
            whereString += String.Format(" and u.Id in (select morr.TargetUserId from [dbo].[MissionOrderRoleRecord] morr where morr.UserId = {0})", userId);
            whereString = GetStatusWhere(whereString, statusId);
            whereString = GetDatesWhere(whereString, beginDate, endDate);
            whereString = GetDepartmentWhere(whereString, departmentId);
            whereString = GetUserNameWhere(whereString, userName);
            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString, sortBy, sortDescending);
            query = CreateQuery(sqlQuery);
            AddDatesToQuery(query, beginDate, endDate, userName);
            foreach (var document in query.SetResultTransformer(Transformers.AliasToBean(typeof(MissionOrderDto))).List<MissionOrderDto>())
            {
                documentList.Add(document);
            }
            */
            #endregion
            return documentList;
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
                case 15:
                    orderBy = @" order by MissionKind";
                    break;
                case 16:
                    orderBy = @" order by AirTicketType";
                    break;
                case 17:
                    orderBy = @" order by TrainTicketType";
                    break;
                case 18:
                    orderBy = @" order by AdditionalOrderNumber";
                    break;
                case 19:
                    orderBy = @" order by AdditionalOrderEditDate";
                    break;
                case 20:
                    orderBy = @" order by AdditionalOrderState";
                    break;
                case 21:
                    orderBy = @" order by AdditionalOrderBeginDate,AdditionalOrderEndDate";
                    break;
                case 22:
                    orderBy = @" order by Position";
                    break;
                case 23:
                    orderBy = @" order by MissionGoal";
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
                        @" select distinct emp.Id from dbo.Users emp
                                            inner join dbo.Users manU on manU.Login = emp.Login+N'R' and manU.RoleId = 4 and manU.IsActive = 1
                                             inner join dbo.Department dManU on manU.DepartmentId = dManU.Id and
                                             ((manU.[level] in ({0})) or ((manU.[level] = {1}) and (manU.IsMainManager = 0)))
                                             inner join dbo.Department dMan on dManU.Path like dMan.Path+N'%'
                                             inner join dbo.Users man on man.DepartmentId = dMan.Id and man.Id = {2}";
                    string sqlQueryPart = string.Empty;
                    string sqlFlag = string.Empty;

                    switch (currentUser.Level)
                    {
                        case 2:
                            sqlQueryPart = string.Format(sqlQueryPartTemplate, "3", "2", currentUser.Id);
                            sqlFlag = @"case when v.UserDateAccept is not null 
                                        and  v.ManagerDateAccept is null then 1 else 0 end as Flag";
                            break;
                        case 3:
                        case 4:
                            sqlQueryPartTemplate += @" union 
                                select distinct emp.id from Users emp
                                    inner join dbo.Department dept
	                                    on emp.DepartmentId = dept.id
                                where
	                                (emp.RoleId & 2) > 0
	                                and
	                                emp.departmentid in
		                                (select Department.id from Department where Department.Path like
			                                (select department.path+'%' from department where id =
				                                (select departmentid from users where id={2})
			                                )
		                                )
                                    and
                                    not (select Login from dbo.Users where Id={2}) = emp.Login + N'R'";
                            sqlFlag = @"case when v.UserDateAccept is not null 
                                        and v.ManagerDateAccept is null then 1 else 0 end as Flag";
                            if (currentUser.Level == 3)
                            {
                                sqlQueryPart = string.Format(sqlQueryPartTemplate, "4,5,6", "3", currentUser.Id);
                            }
                            else
                            {
                                sqlQueryPart = string.Format(sqlQueryPartTemplate, "5,6", "4", currentUser.Id);
                            }
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
                                empMan1.RoleId = 4 and empMan1.Login = emp1.Login+N'R' and empMan1.IsActive = 1)";
                            if (currentUser.Level == 5)
                            {
                                sqlQueryPart = string.Format(sqlQueryPartTemplate, "6", "5", currentUser.Id);                                
                            }
                            else
                            {
                                sqlQueryPart = string.Format(sqlQueryPartTemplate, "-1", "6", currentUser.Id);
                            }
                            sqlFlag = @"case when v.UserDateAccept is not null 
                                            and  v.ManagerDateAccept is null then 1 else 0 end as Flag";
                            break;
                        default:
                            throw new ArgumentException(string.Format(StrInvalidManagerLevel,userId,currentUser.Level));
                    }

                    sqlQueryPart = string.Format(@"u.Id in ( {0} )", sqlQueryPart);

                    sqlQuery = string.Format(sqlQuery, sqlFlag, string.Empty);
                    // Автороль должна действовать только для уровней ниже третьего
                    sqlQueryPart = string.Format(" ((u.Level>3 or u.Level IS NULL) and {0} ) ", sqlQueryPart);
                    // Ручные привязки человек-человек и человек-подразделение из MissionOrderRoleRecord
                    sqlQueryPart += string.Format(" or u.Id in (select morr.TargetUserId from [dbo].[MissionOrderRoleRecord] morr where morr.UserId = {0})", userId);
                    sqlQueryPart += string.Format(" or u.DepartmentId in (select morr.TargetDepartmentId from [dbo].[MissionOrderRoleRecord] morr where morr.UserId = {0})", userId);
                    sqlQueryPart = string.Format(@"({0})", sqlQueryPart);
                    return sqlQueryPart;
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
                        return @" ((v.[NeedToAcceptByChief] = 1) or (v.[NeedToAcceptByChiefAsManager] = 1) or (ao.NeedToAcceptByChief = 1))  ";
                case UserRole.Accountant:
                case UserRole.OutsourcingManager:
                case UserRole.Secretary:
                case UserRole.PersonnelManager:
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
                        statusWhere = @"v.UserDateAccept is not null";
                        break;
                    case 2:
                        statusWhere = @"v.UserDateAccept is null";
                        break;
                    case 3:
                        statusWhere = @"v.ManagerDateAccept is not null and v.NeedToAcceptByChiefAsManager = 0";
                        break;
                    case 4:
                        statusWhere = @"v.ManagerDateAccept is null and v.NeedToAcceptByChiefAsManager = 0";
                        break;
                    case 5:
                        statusWhere = @"(v.ChiefDateAccept is not null and v.NeedToAcceptByChief = 1) or
                                        (v.ManagerDateAccept is not null and v.NeedToAcceptByChiefAsManager = 1)";
                        break;
                    case 6:
                        statusWhere = @"(v.ChiefDateAccept is null  and v.NeedToAcceptByChief = 1) or
                                        (v.ManagerDateAccept is null and v.NeedToAcceptByChiefAsManager = 1) ";
                        break;
                    case 7:
                        statusWhere = @"v.UserDateAccept is not null and v.ManagerDateAccept is null
                                        and v.NeedToAcceptByChiefAsManager = 0";
                        break;
                    case 8:
                        statusWhere = @"v.UserDateAccept is not null and ((v.ManagerDateAccept is null and v.NeedToAcceptByChiefAsManager = 1) 
                                        or (v.ManagerDateAccept is not null and v.ChiefDateAccept is null and v.NeedToAcceptByChief = 1)
                                        or (ao.ManagerDateAccept is not null and ao.ChiefDateAccept is null and ao.NeedToAcceptByChief = 1))";
                        break;
                    //case 8:
                    //    statusWhere =
                    //        @"UserDateAccept is not null and ManagerDateAccept is not null and PersonnelManagerDateAccept is not null";
                    //    break;
                    //case 10:
                    //    statusWhere = @"[DeleteDate] is not null";
                    //    break;
                    case 9:
                        statusWhere = @"v.SendTo1C is not null";
                        break;
                    default:
                        throw new ArgumentException("Неправильный статус заявки");
                }
                if (statusId != 10)
                    statusWhere += " and v.DeleteDate is null ";
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


        public IList<MissionUserDeptsListDto> GetUserDeptsDocuments(int userId,
               UserRole role,
               int departmentId,
               int statusId,
               DateTime? beginDate,
               DateTime? endDate,
               string userName,
               int sortBy,
               bool? sortDescending,
               bool showDepts)
        {
            string sqlQuery = @"select 
                            v.Id,
                            u.Name as UserName,
                            cast(v.Number as nvarchar(10)) as ReportNumber,
                            v.EditDate as ReportDate,
                            v.AccountantAllSum - v.PurchaseBookAllSum - v.UserSumReceived as DiffSum,
                            v.AccountantAllSum as AccountantSum,
                            v.UserAllSum as UserSum,
                            case when v.SendTo1C is null then N'Не выгружено в 1С'
	                             else N'Выгружено в 1С' end as [Status]
                            from dbo.MissionReport v
                            inner join dbo.Users u on v.UserId = u.id and v.[AccountantDateAccept] is not null
                            and v.[DeleteDate] is null";
            //string whereString = GetWhereForUserRole(role, userId, ref sqlQuery);
            //whereString = GetTypeWhere(whereString, typeId);
            string whereString = @" (v.AccountantAllSum - v.PurchaseBookAllSum - v.UserSumReceived)" +
                                 (showDepts ? " < 0 " : " > 0 ");
            whereString = GetUdStatusWhere(whereString, statusId);
            whereString = GetDatesWhere(whereString, beginDate, endDate);
            //whereString = GetPositionWhere(whereString, positionId);
            whereString = GetDepartmentWhere(whereString, departmentId);
            whereString = GetUserNameWhere(whereString, userName);
            sqlQuery = GetUdSqlQueryOrdered(sqlQuery, whereString, sortBy, sortDescending);

            IQuery query = CreateUdQuery(sqlQuery);
            AddDatesToQuery(query, beginDate, endDate, userName);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(MissionUserDeptsListDto)))
                .List<MissionUserDeptsListDto>();
        }
        public virtual IQuery CreateUdQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Number", NHibernateUtil.Int32).
                AddScalar("UserName", NHibernateUtil.String).
                AddScalar("ReportNumber", NHibernateUtil.String).
                AddScalar("ReportDate", NHibernateUtil.DateTime).
                AddScalar("DiffSum", NHibernateUtil.Decimal).
                AddScalar("AccountantSum", NHibernateUtil.Decimal).
                AddScalar("UserSum", NHibernateUtil.Decimal).
                AddScalar("Status", NHibernateUtil.String);
        }
        public virtual string GetUdStatusWhere(string whereString, int statusId)
        {
            if (statusId != 0)
            {
                string statusWhere;
                switch (statusId)
                {
                    case 1:
                        statusWhere = @"SendTo1C is not null";
                        break;
                    case 2:
                        statusWhere = @"SendTo1C is null";
                        break;
                    default:
                        throw new ArgumentException("Неправильный статус отчета");
                }
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @" " + statusWhere;
                return whereString;
            }
            return whereString;
        }
        public virtual string GetUdSqlQueryOrdered(string sqlQuery, string whereString,
                    int sortedBy,
                    bool? sortDescending)
        {
            string orderBy = string.Empty;
            if (!string.IsNullOrEmpty(whereString))
                sqlQuery += @" where " + whereString;
            if (!sortDescending.HasValue)
            {
                orderBy = " ORDER BY UserName,ReportDate DESC";
                return string.Format(sqlSelectForMissionOrderRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy));
            }
            switch (sortedBy)
            {
                case 0:
                    orderBy = " ORDER BY UserName,ReportDate DESC";
                    return string.Format(sqlSelectForMissionOrderRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy));
                case 1:
                    orderBy = @" order by UserName";
                    break;
                case 2:
                    orderBy += @" order by ReportNumber";
                    break;
                case 3:
                    orderBy = @" order by ReportDate";
                    break;
                case 4:
                    orderBy = @" order by DiffSum";
                    break;
                case 5:
                    orderBy = @" order by AccountantSum";
                    break;
                case 6:
                    orderBy = @" order by UserSum";
                    break;
                case 7:
                    orderBy = @" order by Status";
                    break;
                //case 8:
                //    orderBy = @" order by GradeSum";
                //    break;
                //case 9:
                //    orderBy = @" order by GradeIncrease";
                //    break;
                //case 10:
                //    orderBy = @" order by UserSum";
                //    break;
                //case 11:
                //    orderBy = @" order by HasMission";
                //    break;
                //case 12:
                //    orderBy = @" order by State";
                //    break;
                //case 13:
                //    orderBy = @" order by BeginDate,EndDate";
                //    break;
                //case 14:
                //    orderBy = @" order by NeedSecretary";
                //    break;
                //case 15:
                //    orderBy = @" order by MissionKind";
                //    break;
                //case 16:
                //    orderBy = @" order by AirTicketType";
                //    break;
                //case 17:
                //    orderBy = @" order by TrainTicketType";
                //    break;
            }
            if (sortDescending.Value)
                orderBy += " DESC ";
            else
                orderBy += " ASC ";
            return string.Format(sqlSelectForMissionOrderRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy));
            //sqlQuery += @" order by Date DESC,Name ";
            //return sqlQuery;
        }

        public virtual bool CheckOtherOrdersExists(int id,int userId,DateTime beginDate,DateTime endDate)
        {
            const string sqlQuery = @" select count(Id) from [dbo].[MissionOrder]
                                    where [Id] != :id
                                    and [UserId] = :userId
                                    and ([BeginDate] between :beginDate and :endDate
                                    or [EndDate] between :beginDate and :endDate
                                    or :beginDate between [BeginDate] and  [EndDate]
                                    or :endDate between [BeginDate] and  [EndDate])
                                    and [DeleteDate] is null
                                    and (([NeedToAcceptByChief] = 1 and [ChiefDateAccept] is not null)
	                                    or ([NeedToAcceptByChief] = 0 and [ManagerDateAccept] is not null))";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                SetInt32("id", id).
                SetDateTime("beginDate", beginDate).
                SetDateTime("endDate", endDate).
                SetInt32("userId", userId);
            return query.UniqueResult<int>() > 0;
        }
        public virtual bool CheckAnyOtherOrdersExists(int id, int userId, DateTime beginDate, DateTime endDate)
        {
            const string sqlQuery = @" select count(Id) from [dbo].[MissionOrder]
                                    where [Id] != :id
                                    and [UserId] = :userId
                                    and ([BeginDate] between :beginDate and :endDate
                                    or [EndDate] between :beginDate and :endDate
                                    or :beginDate between [BeginDate] and  [EndDate]
                                    or :endDate between [BeginDate] and  [EndDate])
                                    and [DeleteDate] is null";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                SetInt32("id", id).
                SetDateTime("beginDate", beginDate).
                SetDateTime("endDate", endDate).
                SetInt32("userId", userId);
            return query.UniqueResult<int>() > 0;
        }
        public virtual bool CheckAdditionalOrderExists(int id)
        {
            const string sqlQuery = @" select count(Id) from [dbo].[MissionOrder]
                                    where [MainOrderId] = :id and [NeedToAcceptByChief] = 1
                                    ";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                SetInt32("id", id);
            return query.UniqueResult<int>() > 0;
        }
        public virtual bool CheckAnyAdditionalOrdersExists(int id)
        {
            const string sqlQuery = @" select count(Id) from [dbo].[MissionOrder]
                                    where [MainOrderId] = :id";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                SetInt32("id", id);
            return query.UniqueResult<int>() > 0;
        }

    }
}
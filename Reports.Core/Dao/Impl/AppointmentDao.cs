using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;
using NHibernate.Linq;
namespace Reports.Core.Dao.Impl
{
    public class AppointmentDao : DefaultDao<Appointment>, IAppointmentDao
    {
        protected IDepartmentDao departmentDao;
        public IDepartmentDao DepartmentDao
        {
            get { return Validate.Dependency(departmentDao); }
            set { departmentDao = value; }
        }
        #region Selects for documents list
        protected const string sqlSelectForAppointmentRn = @";with res as
                                ({0})
                                select {1} as Number,* from res {2} order by Number ";

        protected const string sqlSelectForAppointmentReportList =
            @"select 
                v.Number as AppNumber,
                r.CreateDate,
                -- N'' as ReportNumber,
                v.Id as Id,
                v.EditDate as EditDate,
                -- u.Id as UserId,
                u.Name as UserName,
                pos.Name as PositionName,
                mapDep7.Name as ManDep7Name,
                mapDep3.Name as ManDep3Name,
                -- aPos.Name as CanPosition, 
                v.PositionName as CanPosition, 
                dep3.Name as Dep3Name,
                dep7.Name as Dep7Name,
                -- v.Period as Period,
                v.Schedule as Schedule,
                v.Salary+v.Bonus as Salary,
                v.DesirableBeginDate as DesirableBeginDate,
                v.Recruter,
                ar.Id as ReasonId,
                ar.Name as Reason,
                r.[Id] as RId,
                r.[Number] as RNumber,
                case when r.[Id] is null then N''
                        when r.[StaffDateAccept] is null then N'Нет' 
                        else N'Да' end as RStaffAccept,
                r.Name as RName,
                r.SecondNumber as SecondNumber,
                r.[Phone] as Phone,
                r.[Email] as Email,
                case when r.[Id] is null then  N'' 
                        when r.[DateAccept] is null and r.[DeleteDate] is null then N''
                        when r.[DateAccept] is null then N'Нет' 
                        else N'Да' end as RApprove,
                case when r.[Id] is null then  N''
                        when r.[DateAccept] is null and r.[DeleteDate] is null then N'' 
                        when r.[DeleteDate] is null then N'' 
                        else r.[RejectReason] end as RReject,
                ur.Name as StaffName,
                case
                        when r.CandidateRejectDate is not null then N'Кандидат отказался от вакансии'
                        when  r.StaffDateAccept is null then N'Черновик'
                        when  r.StaffDateAccept is not null and (r.IsColloquyPassed=0 or (r.TestingResult<=2 and r.TestingResult>0))  and r.IsEducationExists is null then N'Отказано'
                        
                        when r.ColloquyDate is not null and r.IsColloquyPassed is null  and v.AppointmentEducationTypeId=1 and v.Recruter=1 then N'Собеседование назначено'
                        when r.IsColloquyPassed=1 and r.TestingResult is null  and v.AppointmentEducationTypeId=1 and v.Recruter=1 then N'Входное тестирование'
                        when r.IsColloquyPassed=1 and r.TestingResult>2 and r.IsEducationExists is null and v.AppointmentEducationTypeId=1 and v.Recruter=1  then N'Welcome курс'
                        when r.IsColloquyPassed=1 then N'Собеседование пройдено'
                        when r.StaffDateAccept is not null and r.IsEducationExists is null and LessonDate is not null then N'Обучение назначено'
                        when  r.StaffDateAccept is not null and r.IsEducationExists =0 then N'Обучение не пройдено'
                        when  r.StaffDateAccept is not null and r.IsEducationExists =1 then N'Обучение пройдено'
                        when r.Id in (select AppointmentReportId from EmploymentCandidate where AppointmentReportId=r.id ) then 'Кандидат выгружен в приём'
                        when r.StaffDateAccept is not null and r.IsColloquyPassed is null then 'Отправлена руководителю'
                         else N''
                        end as Status,
                        v.BankAccountantAccept as BankAccountantAccept,
                        V.BankAccountantAcceptCount as BankAccountantAcceptCount,
                case
                    when EC.Status is null then -2
                    else                    
                    EC.Status 
                end as EmploymentStatus ,
                case
                    when OnsTr.[IsComplete]=0 then N'Обучение не пройдено'
                    when OnsTr.[IsComplete]=1 then N'Обучение пройдено'
                    else N''
                end  as EducationStatus
                        , 
                        (SELECT AUSR.Name FROM AppointmentRecruter AR inner join Users AUSR ON AR.RecruterId=AUSR.Id WHERE AR.AppointmentId = v.Id ORDER BY AR.RecruterId OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY) as RECRUTER1,
                        (SELECT AUSR.Name FROM AppointmentRecruter AR inner join Users AUSR ON AR.RecruterId=AUSR.Id WHERE AR.AppointmentId = v.Id ORDER BY AR.RecruterId OFFSET 1 ROWS FETCH NEXT 1 ROWS ONLY) as RECRUTER2,
                        (SELECT AUSR.Name FROM AppointmentRecruter AR inner join Users AUSR ON AR.RecruterId=AUSR.Id WHERE AR.AppointmentId = v.Id ORDER BY AR.RecruterId OFFSET 2 ROWS FETCH NEXT 1 ROWS ONLY) as RECRUTER3

                from dbo.Appointment v
                inner join  dbo.AppointmentReport r on r.[AppointmentId] = v.Id
                left join [dbo].[Users] ur on ur.Id = r.CreatorId
                inner join dbo.AppointmentReason ar on ar.Id = v.ReasonId
                -- inner join dbo.Position aPos on v.PositionId = aPos.Id
                inner join [dbo].[Users] u on u.Id = v.CreatorId
                left join dbo.Position pos on u.PositionId = pos.Id
                inner join dbo.Department dep on v.DepartmentId = dep.Id
                inner join dbo.Department crDep on u.DepartmentId = crDep.Id
                inner join dbo.Department dep3 on dep.[Path] like dep3.[Path]+N'%' and dep3.ItemLevel = 3 
                inner join dbo.Department dep7 on dep.[Path] like dep7.[Path]+N'%' and dep7.ItemLevel = 7
                left join [dbo].[Users] uEmp on uEmp.Login +
                    case when u.RoleId & 512 > 0 then N'H' else N'R' end  
                    = u.Login and uEmp.RoleId = 2 
                left join dbo.Department mapDep7 on mapDep7.Id = uEmp.DepartmentId 
                left join dbo.Department mapDep3 on mapDep7.Path like  mapDep3.Path+N'%' and mapDep3.ItemLevel = 3
                Left join EmploymentCandidate EC ON r.id=EC.AppointmentReportId
                left join OnsiteTraining OnsTr On OnsTr.id=EC.OnsiteTrainingId
                ";
        #endregion
        //{1}";
        #region Select for list
        protected const string sqlSelectForAppointmentList =
            @"select 
                v.Number as AppNumber,
                v.CreateDate,
                -- N'' as ReportNumber,
                v.Id as Id,
                v.EditDate as EditDate,
                -- u.Id as UserId,
                u.Name as UserName,
                st.Name as StaffCreator,
                pos.Name as PositionName,
                mapDep7.Name as ManDep7Name,
                mapDep3.Name as ManDep3Name,
                -- aPos.Name as CanPosition, 
                v.PositionName as CanPosition, 
                dep3.Name as Dep3Name,
                dep7.Name as Dep7Name,
                -- v.Period as Period,
                v.Schedule as Schedule,
                v.Salary+v.Bonus as Salary,
                v.DesirableBeginDate as DesirableBeginDate,
                ar.Id as ReasonId,
                ar.Name as Reason,
                case
                        when v.NonActual =1 then N'Заявка не актуальна'
                        when v.ManagerDateAccept is null then N'Черновик'
                        when v.ManagerDateAccept is not null and v.ChiefDateAccept is null and (v.BankAccountantAccept is null or v.BankAccountantAccept=0) and (v.IsStoped=0 or v.IsStoped is null) then N'Отправлена на согласование Специалисту УКДиУ'
                        when (v.BankAccountantAccept is null or v.BankAccountantAccept=0) and v.IsStoped=1 then N'Специалистом УКДиУ приостановлено согласование'
                        
                        when v.ManagerDateAccept is not null and v.ChiefDateAccept is null and v.BankAccountantAccept=1 and v.IsVacationExists=0 then N'Нет вакансий'
                        when v.ManagerDateAccept is not null and v.ChiefDateAccept is null and v.BankAccountantAccept=1 and v.BankAccountantAcceptCount<v.VacationCount then N'Не хватает свободных вакансий. Отправлена на согласование вышестоящему руководителю.'
                        when v.ManagerDateAccept is not null and v.ChiefDateAccept is null and v.BankAccountantAccept=1 and v.BankAccountantAcceptCount>=v.VacationCount then N'Отправлена на согласование вышестоящему руководителю'
                        when v.ChiefDateAccept is not null and v.Recruter!=1 then N'Согласована вышестоящим руководителем. Поиск сотрудника не требуется.'
                        when v.ChiefDateAccept is not null and v.StaffDateAccept is null then N'Согласована вышестоящим руководителем'
                        when v.StaffDateAccept is not null then N'Принята в работу'                        
                        when v.DeleteDate is not null then N'Отменена'
                        else N''
                        end as Status,
                        v.BankAccountantAccept as BankAccountantAccept,
                        V.BankAccountantAcceptCount as BankAccountantAcceptCount,
                        v.Recruter,
                        v.FIO as CandidateFIO
                        , 
                        (SELECT AUSR.Name FROM AppointmentRecruter AR inner join Users AUSR ON AR.RecruterId=AUSR.Id WHERE AR.AppointmentId = v.Id ORDER BY AR.RecruterId OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY) as RECRUTER1,
                        (SELECT AUSR.Name FROM AppointmentRecruter AR inner join Users AUSR ON AR.RecruterId=AUSR.Id WHERE AR.AppointmentId = v.Id ORDER BY AR.RecruterId OFFSET 1 ROWS FETCH NEXT 1 ROWS ONLY) as RECRUTER2,
                        (SELECT AUSR.Name FROM AppointmentRecruter AR inner join Users AUSR ON AR.RecruterId=AUSR.Id WHERE AR.AppointmentId = v.Id ORDER BY AR.RecruterId OFFSET 2 ROWS FETCH NEXT 1 ROWS ONLY) as RECRUTER3

                from dbo.Appointment v
                inner join dbo.AppointmentReason ar on ar.Id = v.ReasonId
                -- inner join dbo.Position aPos on v.PositionId = aPos.Id
                inner join [dbo].[Users] u on u.Id = v.CreatorId
                Left join [dbo].[Users] st on v.StaffCreatorId=st.id
                left join dbo.Position pos on u.PositionId = pos.Id
                inner join dbo.Department dep on v.DepartmentId = dep.Id
                inner join dbo.Department crDep on u.DepartmentId = crDep.Id
                inner join dbo.Department dep3 on dep.[Path] like dep3.[Path]+N'%' and dep3.ItemLevel = 3 
                inner join dbo.Department dep7 on dep.[Path] like dep7.[Path]+N'%' and dep7.ItemLevel = 7
                left join [dbo].[Users] uEmp on uEmp.Login +
                    case when u.RoleId & 512 > 0 then N'H' else N'R' end  
                    = u.Login and uEmp.RoleId = 2 
                left join dbo.Department mapDep7 on mapDep7.Id = uEmp.DepartmentId 
                left join dbo.Department mapDep3 on mapDep7.Path like  mapDep3.Path+N'%' and mapDep3.ItemLevel = 3
                ";
        #endregion
        public override IQuery CreateQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("AppNumber", NHibernateUtil.Int32).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("EditDate", NHibernateUtil.DateTime).
                //AddScalar("UserId", NHibernateUtil.Int32).
                AddScalar("UserName", NHibernateUtil.String).
                AddScalar("PositionName", NHibernateUtil.String).
                AddScalar("ManDep7Name", NHibernateUtil.String).
                AddScalar("ManDep3Name", NHibernateUtil.String).
                AddScalar("CanPosition", NHibernateUtil.String).
                AddScalar("Dep3Name", NHibernateUtil.String).
                AddScalar("Dep7Name", NHibernateUtil.String).
                //AddScalar("Period", NHibernateUtil.String).
                AddScalar("Schedule", NHibernateUtil.String).
                AddScalar("Salary", NHibernateUtil.Decimal).
                AddScalar("DesirableBeginDate", NHibernateUtil.DateTime).
                AddScalar("Reason", NHibernateUtil.String).
                AddScalar("Number", NHibernateUtil.Int32).
                AddScalar("Status", NHibernateUtil.String).
                AddScalar("BankAccountantAccept",NHibernateUtil.Boolean).
                AddScalar("BankAccountantAcceptCount",NHibernateUtil.Int32).
                AddScalar("CreateDate", NHibernateUtil.DateTime).
                AddScalar("Recruter",NHibernateUtil.Int32).
                AddScalar("CandidateFIO",NHibernateUtil.String).
                AddScalar("ReasonId",NHibernateUtil.Int32).
                AddScalar("StaffCreator", NHibernateUtil.String).
                AddScalar("Recruter1", NHibernateUtil.String).
                AddScalar("Recruter2", NHibernateUtil.String).
                AddScalar("Recruter3", NHibernateUtil.String)
                ;
        }
        public  IQuery CreateReportQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("AppNumber", NHibernateUtil.Int32).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("EditDate", NHibernateUtil.DateTime).
                //AddScalar("UserId", NHibernateUtil.Int32).
                AddScalar("UserName", NHibernateUtil.String).
                AddScalar("PositionName", NHibernateUtil.String).
                AddScalar("ManDep7Name", NHibernateUtil.String).
                AddScalar("CanPosition", NHibernateUtil.String).
                AddScalar("Dep3Name", NHibernateUtil.String).
                AddScalar("Dep7Name", NHibernateUtil.String).
                //AddScalar("Period", NHibernateUtil.String).
                AddScalar("Schedule", NHibernateUtil.String).
                AddScalar("Salary", NHibernateUtil.Decimal).
                AddScalar("DesirableBeginDate", NHibernateUtil.DateTime).
                AddScalar("Reason", NHibernateUtil.String).
                AddScalar("RId", NHibernateUtil.Int32).
                AddScalar("RNumber", NHibernateUtil.Int32).
                AddScalar("RStaffAccept", NHibernateUtil.String).
                AddScalar("RName", NHibernateUtil.String).
                AddScalar("Phone", NHibernateUtil.String).
                AddScalar("Email", NHibernateUtil.String).
                AddScalar("RApprove", NHibernateUtil.String).
                AddScalar("RReject", NHibernateUtil.String).
                AddScalar("StaffName", NHibernateUtil.String).
                AddScalar("Number", NHibernateUtil.Int32).
                AddScalar("Status", NHibernateUtil.String).
                AddScalar("BankAccountantAccept", NHibernateUtil.Boolean).
                AddScalar("BankAccountantAcceptCount", NHibernateUtil.Int32).
                AddScalar("SecondNumber",NHibernateUtil.Int32).
                AddScalar("CreateDate",NHibernateUtil.DateTime).
                AddScalar("EmploymentStatus",NHibernateUtil.Int32).
                AddScalar("ManDep3Name", NHibernateUtil.String).
                AddScalar("Recruter", NHibernateUtil.Int32).
                AddScalar("EducationStatus", NHibernateUtil.String).
                AddScalar("Recruter1", NHibernateUtil.String).
                AddScalar("Recruter2", NHibernateUtil.String).
                AddScalar("Recruter3", NHibernateUtil.String).
                AddScalar("ReasonId", NHibernateUtil.Int32);
        }
        public AppointmentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public virtual IList<DepartmentDto> GetDepartmentsForManager23(int managerId, int level,bool dep3only)
        {
            string sqlQuery = string.Format(@" select d.Id,d.Name,d.Path,d.ItemLevel from [dbo].[AppointmentManager23ToDepartment3] mtod
                                        inner join [dbo].[Department] d on d.Id = mtod.[DepartmentId]
                                        where mtod.managerId = {0}",managerId);
            if(level == 2 && !dep3only)
                sqlQuery += string.Format(@" union
                            select d.Id,d.Name,d.Path,d.ItemLevel from [dbo].[AppointmentCreateManager2ToDepartment2] mctod
                                        inner join [dbo].[Department] d on d.Id = mctod.[DepartmentId]
                                        where mctod.managerId = {0}", managerId);

            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("Path", NHibernateUtil.String).
                AddScalar("ItemLevel", NHibernateUtil.Int32);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(DepartmentDto))).List<DepartmentDto>();
        }
        public virtual DepartmentDto GetDepartmentForPathAndLevel(string path,int level)
        {
            const string sqlQuery = @"select d.Id,d.Name,d.Path,d.ItemLevel from [dbo].[Department] d
                                            where :path like Path+N'%' and ItemLevel = :level";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("Path", NHibernateUtil.String).
                AddScalar("ItemLevel", NHibernateUtil.Int32).
                SetString("path", path).
                SetInt32("level", level);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(DepartmentDto))).UniqueResult<DepartmentDto>();
        }
        public virtual IList<int> GetManager3ForManager2(int managerId)
        {
            string sqlQuery = string.Format(@" select [Manager3Id]  as Id from [dbo].[AppointmentManager2ToManager3]
                                            where [Manager2Id] = {0}", managerId);
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32);
            return query.List<int>();
        }
        public virtual IList<int> GetChildrenManager2ForManager2(int parentId)
        {
            string sqlQuery = string.Format(@" select [ChildId]  as Id from [dbo].[AppointmentManager2ParentToManager2Child]
                                            where [ParentId] = {0}", parentId);
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32);
            return query.List<int>();
        }
        public virtual List<IdNameDto> GetParentForManager2(int childId)
        {
            string sqlQuery = string.Format(@" select u.Id,u.email as Name from users u
                    inner join dbo.AppointmentManager2ParentToManager2Child mptomc on mptomc.ParentId = u.id
                    where mptomc.ChildId = {0}",childId);
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                    AddScalar("Id", NHibernateUtil.Int32).
                    AddScalar("Name", NHibernateUtil.String);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDto))).List<IdNameDto>().ToList();
        }
        public virtual List<IdNameDto> GetParentForManager3(int childId)
        {
            string sqlQuery = string.Format(@"select u.Id,u.email as Name from users u
                        inner join dbo.AppointmentManager2ToManager3 mptomc on mptomc.Manager2Id = u.id
                        where mptomc.Manager3Id = {0}", childId);
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                    AddScalar("Id", NHibernateUtil.Int32).
                    AddScalar("Name", NHibernateUtil.String);;
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDto))).List<IdNameDto>().ToList();
        }
        public virtual List<IdNameDto> GetParentForManager4Department(int departmentId)
        {
            string sqlQuery = string.Format(@"select distinct  u.Id,u.email as Name from users u
                            inner join  dbo.AppointmentManager23ToDepartment3 mtod on mtod.ManagerId = u.Id
                            inner join dbo.Department dParent on dParent.Id = mtod.DepartmentId
                            inner join dbo.Department dChild on dChild.Path like dParent.Path +N'%' 
                            and dChild.ItemLevel = dParent.ItemLevel + 1
                            where dChild.Id = {0}", departmentId);
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                    AddScalar("Id", NHibernateUtil.Int32).
                    AddScalar("Name", NHibernateUtil.String); ;
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDto))).List<IdNameDto>().ToList();
        }
        public virtual List<IdNameDto> GetForManagersParentDepartment(int departmentId)
        {
            string sqlQuery = string.Format(@"select top(1) u.Id,u.email as Name from users u
                        inner join dbo.Department dParent on dParent.Id = u.DepartmentId
                        inner join dbo.Department dChild on dChild.Path like dParent.Path +N'%' 
                        where u.IsActive=1 and u.Level is not null and  dChild.Id = {0} and dParent.ItemLevel<dChild.ItemLevel
						order by Level desc, IsMainManager desc", departmentId);
                        
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                    AddScalar("Id", NHibernateUtil.Int32).
                    AddScalar("Name", NHibernateUtil.String); ;
            var result= query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDto))).List<IdNameDto>().ToList();
            
            return result;
            
        }
        public virtual List<IdNameDto> GetParentForManagerDepartment(int departmentId)
        {
            string sqlQuery = string.Format(@"select distinct u.Id,u.email as Name from users u
                        inner join dbo.Department dParent on dParent.Id = u.DepartmentId
                        inner join dbo.Department dChild on dChild.Path like dParent.Path +N'%' 
                        and dChild.ItemLevel = dParent.ItemLevel + 1
                        where dChild.Id = {0}", departmentId);
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                    AddScalar("Id", NHibernateUtil.Int32).
                    AddScalar("Name", NHibernateUtil.String); ;
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDto))).List<IdNameDto>().ToList();
        }
        #region Search documents for list
        public IList<AppointmentDto> GetDocuments(int userId,
                UserRole role,
                int departmentId,
                int reasonId,
                int statusId,
                string number,
                DateTime? beginDate,
                DateTime? endDate,
                string userName,
                string RecruterFio,
                int sortBy,
                bool? sortDescending)
        {
            string sqlQuery = sqlSelectForAppointmentList;
            string whereString = GetWhereForUserRole(role, userId);
            //string whereString = GetWhereForUserRole(role, userId, ref sqlQuery);
            //whereString = GetTypeWhere(whereString, typeId);
            whereString = GetStatusWhere(whereString, statusId);
            whereString = GetDatesWhere(whereString, beginDate, endDate);
            if (!String.IsNullOrWhiteSpace(number))
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += String.Format(@" v.Number like '{0}%' ", number);
            }
            if (reasonId > 0)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += String.Format(@" ar.id={0} ", reasonId);
            }
            string RecruterStr="";
            if (!String.IsNullOrWhiteSpace(RecruterFio))
            {
                RecruterStr= String.Format(@" where (Recruter1 like '{0}%' or Recruter2 like '{0}%' or Recruter3 like '{0}%') ", RecruterFio.Trim());
            }
            //whereString = GetPositionWhere(whereString, positionId);
            whereString = GetDepartmentWhere(whereString, departmentId);
            whereString = GetUserNameWhere(whereString, userName);
            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString , RecruterStr , sortBy, sortDescending);

            IQuery query = CreateQuery(sqlQuery);
            AddDatesToQuery(query, beginDate, endDate, userName);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(AppointmentDto))).List<AppointmentDto>();
        }
        public IList<AppointmentDto> GetReportDocuments(int userId,
                UserRole role,
                int departmentId,
                int statusId,
                string number,
                DateTime? beginDate,
                DateTime? endDate,
                string userName,
                string CandidateFio,
                string RecruterFio,
                int sortBy,
                bool? sortDescending)
        {
            string sqlQuery = sqlSelectForAppointmentReportList;
            string whereString = GetWhereForUserRole(role, userId);
            //string whereString = GetWhereForUserRole(role, userId, ref sqlQuery);
            //whereString = GetTypeWhere(whereString, typeId);
            whereString = GetReportStatusWhere(whereString, statusId);
            whereString = GetDatesWhere(whereString, beginDate, endDate);
            if (!String.IsNullOrWhiteSpace(number))
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString +=String.Format(@" (CAST(v.Number as varchar)+'/'+CAST(r.SecondNumber as varchar)) like '{0}%' ", number.Trim());
            }
            if (!String.IsNullOrWhiteSpace(CandidateFio))
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += String.Format(@" r.name like '{0}%' ", CandidateFio.Trim());
            }
            string RecruterStr = "";
            if (!String.IsNullOrWhiteSpace(RecruterFio))
            {
                RecruterStr = String.Format(@" where (Recruter1 like '{0}%' or Recruter2 like '{0}%' or Recruter3 like '{0}%') ", RecruterFio.Trim());
            }
            //whereString = GetPositionWhere(whereString, positionId);
            whereString = GetDepartmentWhere(whereString, departmentId);
            whereString = GetUserNameWhere(whereString, userName);
            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString, RecruterStr, sortBy, sortDescending);

            IQuery query = CreateReportQuery(sqlQuery);
            AddDatesToQuery(query, beginDate, endDate, userName);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(AppointmentDto))).List<AppointmentDto>();
        }
        public string GetSqlQueryOrdered(string sqlQuery, string whereString, string RecruterStr,
                    int sortedBy,
                    bool? sortDescending)
        {
            string orderBy = string.Empty;
            if (!string.IsNullOrEmpty(whereString))
                sqlQuery += @" where " + whereString;
            if (!sortDescending.HasValue)
            {
                orderBy = " ORDER BY EditDate DESC,UserName";
                return string.Format(sqlSelectForAppointmentRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy),RecruterStr);
            }
            switch (sortedBy)
            {
                case 0:
                    orderBy = " ORDER BY EditDate DESC,UserName";
                    return string.Format(sqlSelectForAppointmentRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy), RecruterStr);
                case 1:
                    orderBy = @" order by AppNumber";
                    break;
                case 2:
                    orderBy += @" order by AppNumber,RNumber,SecondNumber";
                    break;
                case 3:
                    orderBy = @" order by UserName";
                    break;
                case 4:
                    orderBy = @" order by PositionName";
                    break;
                case 20:
                    orderBy = @" order by ManDep7Name";
                    break;
                case 7:
                    orderBy = @" order by CanPosition";
                    break;
                case 5:
                    orderBy = @" order by Dep3Name";
                    break;
                case 6:
                    orderBy = @" order by Dep7Name";
                    break;
                case 8:
                    orderBy = @" order by Period";
                    break;
                case 9:
                    orderBy = @" order by Schedule";
                    break;
                case 10:
                    orderBy = @" order by Salary";
                    break;
                case 11:
                    orderBy = @" order by DesirableBeginDate";
                    break;
                case 12:
                    orderBy = @" order by Reason";
                    break;
                case 13:
                    orderBy = @" order by RStaffAccept";
                    break;
                case 14:
                    orderBy = @" order by RName";
                    break;
                case 15:
                    orderBy = @" order by Phone";
                    break;
                case 16:
                    orderBy = @" order by Email";
                    break;
                case 17:
                    orderBy = @" order by RApprove";
                    break;
                case 18:
                    orderBy = @" order by RReject";
                    break;
                case 19:
                    orderBy = @" order by StaffName";
                    break;
                case 21:
                    orderBy = @" order by Status";
                    break;
                case 22:
                    if (sqlQuery.Contains("AppointmentReport"))
                         orderBy = @" order by CreateDate";
                    else orderBy = @" order by CreateDate";
                    break;
                case 23:
                    orderBy = @" order by Recruter";
                    break;
                case 24:
                    orderBy = @" order by CandidateFIO";
                    break;
                case 25:
                    orderBy = @" order by BankAccountantAcceptCount";
                    break;
                case 26:
                    orderBy = @" order by StaffCreator";
                    break;
                case 27:
                    orderBy = @" order by StaffCreator";
                    break;
                case 28:
                    orderBy = @" order by Recruter1";
                    break;
                case 29:
                    orderBy = @" order by Recruter2";
                    break;
                case 30:
                    orderBy = @" order by Recruter3";
                    break;
            }
            if (sortDescending.Value)
                orderBy += " DESC ";
            else
                orderBy += " ASC ";
            return string.Format(sqlSelectForAppointmentRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy), RecruterStr);
            //sqlQuery += @" order by Date DESC,Name ";
            //return sqlQuery;
        }
        public string GetReportStatusWhere(string whereString, int statusId)
        {
            if (statusId != 0)
            {
                string statusWhere;
                switch (statusId)
                {
                    case 1://1, "Черновик"
                        statusWhere = @" r.CandidateRejectDate is null and r.StaffDateAccept is null ";
                        break;
                    case 2://2, "Кандидату отказано"
                        statusWhere = @" r.CandidateRejectDate is null and r.StaffDateAccept is not null and (r.IsColloquyPassed=0 or (r.TestingResult<=2 and r.TestingResult>0)) ";
                        break;
                    case 3://3, "кандидат принят"
                        statusWhere = @" r.CandidateRejectDate is null and r.Id in (select AppointmentReportId from EmploymentCandidate where AppointmentReportId=r.id ) ";
                        break;
                    case 4://4, "Отправлено руководителю"
                        statusWhere = @" r.CandidateRejectDate is null and r.StaffDateAccept is not null and r.IsColloquyPassed is null ";
                        break;
                    case 5://5, "Собеседование пройдено"
                        statusWhere = @" r.CandidateRejectDate is null and r.StaffDateAccept is not null and r.IsColloquyPassed=1 and r.IsEducationExists is null ";
                        break;
                    case 6://6, "Обучение пройдено"
                        statusWhere = @" r.CandidateRejectDate is null and r.StaffDateAccept is not null and r.IsEducationExists=1 ";
                        break;
                    case 7://7, "Обучение не пройдено"
                        statusWhere = @" r.CandidateRejectDate is null and r.StaffDateAccept is not null and r.IsEducationExists=0 ";
                        break;
                    case 8://Welcome 
                        statusWhere = @" r.CandidateRejectDate is null and r.IsColloquyPassed=1 and r.TestingResult>2   and v.AppointmentEducationTypeId=2 and v.Recruter=1 ";
                        break;
                    case 9://собеседование назначено
                        statusWhere = @" r.CandidateRejectDate is null and r.ColloquyDate is not null and r.IsColloquyPassed is null  and v.AppointmentEducationTypeId=2 and v.Recruter=1 ";
                        break;
                    case 10://входное тестирование
                        statusWhere = @" r.CandidateRejectDate is null and r.IsColloquyPassed=1 and r.TestingResult is null  and v.AppointmentEducationTypeId=2 and v.Recruter=1 ";
                        break;
                    case 11:
                        statusWhere = @" r.CandidateRejectDate is not null ";
                        break;
                    default:
                        throw new ArgumentException("Неправильный статус заявки");
                }
                if (statusId != 5)
                    statusWhere += " and v.DeleteDate is null ";
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @" " + statusWhere;
                return whereString;
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
                    case 1://1, "Черновик"
                        statusWhere = @" v.DeleteDate is null and v.ManagerDateAccept is null AND is v.NonActual!=1";
                        break;
                    case 2://2, "Отправлена на согласование вышестоящему руководителю"
                        statusWhere = @"  v.DeleteDate is null and v.NonActual!=1 and v.ManagerDateAccept is not null and v.ChiefDateAccept is null and v.BankAccountantAccept is not null and v.IsVacationExists=1";
                        break;
                    case 3://3, "Согласована вышестоящим руководителем"
                        statusWhere = @" v.DeleteDate is null and v.NonActual!=1 and v.ChiefDateAccept is not null and v.StaffDateAccept is null and recruter<2";
                        break;
                    case 4://4, "Принята в работу"
                        statusWhere = @" v.DeleteDate is null and v.NonActual!=1 and v.StaffDateAccept is not null ";
                        break;
                    case 5://5, "Отменена"
                        statusWhere = @" v.DeleteDate is not null";
                        break;
                    case 6://Нет подходящих вакансий
                        statusWhere = @" v.DeleteDate is null and v.NonActual!=1 and  v.ManagerDateAccept is not null and v.ChiefDateAccept is null and v.BankAccountantAccept=1 and v.IsVacationExists=0 ";
                            break;
                    case 7://Отправлена на согласование в кадровую службу
                            statusWhere = @" v.DeleteDate is null and  v.NonActual!=1 and  v.ManagerDateAccept is not null and v.ChiefDateAccept is null and v.BankAccountantAccept is null ";
                            break;
                    case 8://Согласование приостановленно
                            statusWhere = @" v.DeleteDate is null and  v.NonActual!=1 and v.BankAccountantAccept is null and v.IsStoped=1 ";
                            break;
                    case 9: //Не хватает вакансий
                            statusWhere = @" v.DeleteDate is null and  v.NonActual!=1 and v.ManagerDateAccept is not null and v.ChiefDateAccept is null and v.BankAccountantAccept=1 and v.BankAccountantAcceptCount<v.VacationCount and v.BankAccountantAcceptCount>0"; 
                        break;
                    case 10://3, "Согласована вышестоящим руководителем. Поиск не требуется"
                        statusWhere = @"  v.DeleteDate is null and  v.NonActual!=1 and v.ChiefDateAccept is not null and v.StaffDateAccept is null and recruter=2";
                        break;
                    case 11://Заявка не актуальна
                        statusWhere = @" v.NonActual=1 ";
                        break;
                    default:
                        throw new ArgumentException("Неправильный статус заявки");
                }
                if( statusId != 5)
                    statusWhere += " and v.DeleteDate is null ";
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @" " + statusWhere;
                return whereString;
            }
            return whereString;
        }
        public override string GetDepartmentWhere(string whereString, int departmentId)
        {
            if (departmentId != 0)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                Department dep = DepartmentDao.Load(departmentId);
                whereString += string.Format(@" mapDep7.Path  like '{0}' and mapDep7.ItemLevel = {1}", dep.Path + "%", 7);
            }
            return whereString;
        }
        public override string GetWhereForUserRole(UserRole role, int userId)
        {
            User currentUser = UserDao.Load(userId);
            if (currentUser == null)
                throw new ArgumentException(string.Format("Не могу загрузить пользователя {0} из базы даннных", userId));
            switch (role)
            {
                case UserRole.Manager:
                   
                    const string sqlQueryPartTemplate = @" ((u.Id = {0}) or ({1})) ";
                    string sqlDepQueryPart;
                    switch (currentUser.Level)
                    {
                        case 2:
                            // todo manager2 to department ???
                            sqlDepQueryPart = string.Format(
                            @" exists 
                                ( 
                                    select uC.Id from dbo.Users uC
                                    inner join  dbo.AppointmentManager2ToManager3 dmtom on  dmtom.Manager2Id = uC.[Id]
                                    where uC.Id = {0} and dmtom.Manager3Id = u.Id
                                )
                                or
                                exists 
                                ( 
                                    select uC.Id from dbo.Users uC
                                    inner join  dbo.AppointmentManager23ToDepartment3 dmtod on  dmtod.ManagerId = uC.[Id]
                                    inner join dbo.Department dc on dc.Id = dmtod.DepartmentId
                                    where uC.Id = {0}
                                    and crDep.Path like dC.Path + N'%' and dC.ItemLevel + 1 = crDep.ItemLevel
                                )
                                or
                                exists 
                                ( 
                                    select uC.Id from dbo.Users uC
                                    inner join  dbo.AppointmentManager2ParentToManager2Child dmtom on  dmtom.ParentId = uC.[Id]
                                    where uC.Id = {0} and dmtom.ChildId = u.Id
                                )
                                or
                                exists 
                                ( 
                                    select uC.Id from dbo.Users uC
                                    inner join [dbo].[Department] dC on  dC.Id = uC.[DepartmentId]
                                    where uC.Id = {0}
                                    and crDep.Path like dC.Path + N'%' and dC.ItemLevel < crDep.ItemLevel
                                )
                                ", currentUser.Id);
                            break;
                        case 3:
                            sqlDepQueryPart = string.Format(
                                @" exists 
                                ( 
                                    select uC.Id from dbo.Users uC
                                    inner join  dbo.ManualRoleRecord mrr on  (mrr.UserId = uC.[Id] and mrr.TargetDepartmentId > 0)
                                    inner join dbo.Department dc on dc.Id = mrr.TargetDepartmentId
                                    where uC.Id = {0}
                                    and crDep.Path like dC.Path + N'%' and dc.ItemLevel < crDep.ItemLevel
                                )", currentUser.Id);
                            break;
                        case 4:
                        case 5:
                        case 6:
                            sqlDepQueryPart = string.Format(
                                @" exists 
                                ( 
                                    select uC.Id from dbo.Users uC
                                    inner join [dbo].[Department] dC on  dC.Id = uC.[DepartmentId]
                                    where uC.Id = {0}
                                    and crDep.Path like dC.Path + N'%' and dC.ItemLevel <= crDep.ItemLevel
                                )", currentUser.Id);
                            break;
                        default:
                            throw new ArgumentException(string.Format(MissionOrderDao.StrInvalidManagerLevel, 
                                userId, currentUser.Level));
                    }
                    string sqlQueryPart = string.Format(sqlQueryPartTemplate,currentUser.Id,sqlDepQueryPart);
                    return sqlQueryPart;
                //case UserRole.Director:
                //    sqlDepQueryPart = string.Format(@" ((u.Level = 2) or (u.RoleId = {0} and u.Id != {1})) ",
                //                                    (int)UserRole.Director,userId);
                //    sqlQueryPart = string.Format(sqlQueryPartTemplate,currentUser.Id,sqlDepQueryPart);
                //    return sqlQueryPart;
                case UserRole.PersonnelManager:
                case UserRole.OutsourcingManager:
                case UserRole.Estimator:
                case UserRole.ConsultantPersonnel:
                case UserRole.StaffManager:
                case UserRole.Trainer:
                case UserRole.Security:
                    return string.Empty;
                default:
                    throw new ArgumentException(string.Format("Invalid user role {0}", role));
            }
        }
        #endregion

        public List<Appointment> GetAppointmentForReasonPosition(int userId)
        {
            var res=Session.Query<Appointment>().Where(x => x.ReasonPositionUser.Id == userId && x.isNotifyNeeded);
            if (res != null && res.Any()) return res.ToList();
            else return null;
        }
    }
}
using System;
using Reports.Core.Domain;
using Reports.Core.Services;
using Reports.Core.Dto;
using System.Collections.Generic;
using NHibernate.Transform;
using NHibernate;
using NHibernate.Criterion;
using System.Linq;
using NHibernate.Linq;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Заявки на изменение структуры подразделений.
    /// </summary>
    public class StaffDepartmentRequestDao : DefaultDao<StaffDepartmentRequest>, IStaffDepartmentRequestDao
    {
        public StaffDepartmentRequestDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Dependencies
        protected IDepartmentDao departmentDao;
        public IDepartmentDao DepartmentDao
        {
            get { return Validate.Dependency(departmentDao); }
            set { departmentDao = value; }
        }

        #endregion

        /// <summary>
        /// Список заявок для подразделений.
        /// </summary>
        /// <param name="curUser">Текущий пользователь.</param>
        /// <param name="role">Роль текущего пользователя.</param>
        /// <param name="DepartmentId">Id подразделения.</param>
        /// <param name="Id">Номер заявки</param>
        /// <param name="Surname">ФИО инициатора</param>
        /// <param name="DateBegin">Дата начала периода создания заявки</param>
        /// <param name="DateEnd">Дата конца периода создания заявки</param>
        /// <param name="StatusId">Id статуса заявки.</param>
        /// <param name="SortBy">Номер колонки для сортировки</param>
        /// <param name="SortDescending">Признак направления сортировки.</param>
        /// <param name="RequestTypeId">Id вида заявки</param>
        /// <returns></returns>
        public IList<DepartmentRequestListDto> GetDepartmentRequestList(User curUser, UserRole role, int DepartmentId, int Id, string Surname, DateTime? DateBegin, DateTime? DateEnd, int StatusId, int SortBy, bool? SortDescending, int RequestTypeId)
        {
            string SqlQuery = @"SELECT A.Id, 
                                       A.DateRequest, 
                                       B.Name as RequestTypeName, 
                                       A.RequestTypeId,
                                       A.DepartmentId,
                                       A.ParentId,
                                       A.BFGId,
                                       F.Name as AccessoryName,
                                       isnull(Dep2.Name, case when A.ItemLevel = 2 then A.Name else null end) as Dep2Name, 
                                       isnull(Dep3.Name, case when A.ItemLevel = 3 then A.Name else null end) as Dep3Name, 
                                       isnull(Dep4.Name, case when A.ItemLevel = 4 then A.Name else null end) as Dep4Name, 
                                       isnull(Dep5.Name, case when A.ItemLevel = 5 then A.Name else null end) as Dep5Name, 
                                       isnull(Dep6.Name, case when A.ItemLevel = 6 then A.Name else null end) as Dep6Name, 
                                       case when A.ItemLevel = 7 then A.Name else null end as Dep7Name, 
                                       Dep2.Path as Dep2Path, 
                                       Dep3.Path as Dep3Path, 
                                       Dep4.Path as Dep4Path, 
                                       Dep5.Path as Dep5Path, 
                                       Dep6.Path as Dep6Path, 
                                       A.OrderNumber, 
                                       A.OrderDate, 
                                       D.Id as PersonId, 
                                       D.Name as Surname, 
                                       E.Name as PositionName,
                                       case when A.DeleteDate is not null then 'Отклонено'
                                            when A.DateSendToApprove is null then 'Черновик'
						                    when A.DateSendToApprove is not null and A.DateState is null and A.DeleteDate is null then 'На согласовании'
						                    when A.DateState is not null and A.DeleteDate is null then 'Утверждено' end as Status,
			                           case when A.DeleteDate is not null then 4
                                            when A.DateSendToApprove is null then 1
						                    when A.DateSendToApprove is not null and A.DateState is null and A.DeleteDate is null then 2
						                    when A.DateState is not null and A.DeleteDate is null then 3 end as StatusId
                                FROM StaffDepartmentRequest as A
                                INNER JOIN StaffDepartmentRequestTypes as B ON B.Id = A.RequestTypeId
                                LEFT JOIN Department as C ON C.Id = A.ParentId
                                LEFT JOIN Department as Dep2 ON C.Path like Dep2.Path + N'%' and Dep2.ItemLevel = 2
                                LEFT JOIN Department as Dep3 ON C.Path like Dep3.Path + N'%' and Dep3.ItemLevel = 3
                                LEFT JOIN Department as Dep4 ON C.Path like Dep4.Path + N'%' and Dep4.ItemLevel = 4
                                LEFT JOIN Department as Dep5 ON C.Path like Dep5.Path + N'%' and Dep5.ItemLevel = 5
                                LEFT JOIN Department as Dep6 ON C.Path like Dep6.Path + N'%' and Dep6.ItemLevel = 6
                                LEFT JOIN Users as D ON D.Id = A.CreatorID
                                LEFT JOIN Position as E ON E.Id = D.PositionId
                                LEFT JOIN StaffDepartmentAccessory as F ON F.Id = A.BFGId";

            

            SqlQuery = string.Format(@"SELECT * FROM ({0}) as A", SqlQuery);
            SqlQuery += @" INNER JOIN Department as B ON B.Id = isnull(A.DepartmentId, A.ParentId)";


            SqlQuery += GetWhereForUserRole(curUser, role);

            string sqlWhere = string.Empty;
            sqlWhere = GetWhereForParameters(DepartmentId, Id, Surname, DateBegin, DateEnd, StatusId, ref sqlWhere, RequestTypeId);
            sqlWhere = (string.IsNullOrEmpty(sqlWhere) ? "" : " WHERE " + sqlWhere);
            SqlQuery += sqlWhere + GetOrderByForSqlQuery(SortBy, SortDescending);

            IQuery query = Session.CreateSQLQuery(SqlQuery)
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("DateRequest", NHibernateUtil.Date)
                .AddScalar("RequestTypeName", NHibernateUtil.String)
                .AddScalar("RequestTypeId", NHibernateUtil.Int32)
                .AddScalar("DepartmentId", NHibernateUtil.Int32)
                .AddScalar("ParentId", NHibernateUtil.Int32)
                .AddScalar("AccessoryName", NHibernateUtil.String)
                .AddScalar("Dep2Name", NHibernateUtil.String)
                .AddScalar("Dep3Name", NHibernateUtil.String)
                .AddScalar("Dep4Name", NHibernateUtil.String)
                .AddScalar("Dep5Name", NHibernateUtil.String)
                .AddScalar("Dep6Name", NHibernateUtil.String)
                .AddScalar("Dep7Name", NHibernateUtil.String)
                .AddScalar("OrderNumber", NHibernateUtil.String)
                .AddScalar("OrderDate", NHibernateUtil.Date)
                .AddScalar("PersonId", NHibernateUtil.Int32)
                .AddScalar("Surname", NHibernateUtil.String)
                .AddScalar("PositionName", NHibernateUtil.String)
                .AddScalar("Status", NHibernateUtil.String)
                .AddScalar("StatusId", NHibernateUtil.Int32);

            if (SqlQuery.Contains(":userId")) query.SetInt32("userId", curUser.Id);
            if (SqlQuery.Contains(":DepartmentId")) query.SetInt32("DepartmentId", DepartmentId);
            if (SqlQuery.Contains(":Id")) query.SetInt32("Id", Id);
            if (SqlQuery.Contains(":DateBegin")) query.SetDateTime("DateBegin", DateBegin.Value);
            if (SqlQuery.Contains(":DateEnd")) query.SetDateTime("DateEnd", DateEnd.Value.AddDays(1));
            if (SqlQuery.Contains(":StatusId")) query.SetInt32("StatusId", StatusId);
            if (SqlQuery.Contains(":RequestTypeId")) query.SetInt32("RequestTypeId", RequestTypeId);

            return query.SetResultTransformer(Transformers.AliasToBean<DepartmentRequestListDto>()).List<DepartmentRequestListDto>();
        }
        /// <summary>
        /// Определяем критерий поиска для роли текущего пользователя.
        /// </summary>
        /// <param name="curUser">Текущий пользователь.</param>
        /// <param name="role">Роль текущего пользователя.</param>
        /// <returns></returns>
        protected string GetWhereForUserRole(User curUser, UserRole role)
        {
            string sqlWhere = string.Empty;
            switch (role)
            {
                case UserRole.Manager://автоматические права + ручные привязки
                    sqlWhere = @"
                                 INNER JOIN (SELECT distinct * 
                                 FROM (SELECT C.*
                                        FROM Users as A
                                        INNER JOIN Department as B ON B.Id = A.DepartmentId
                                        INNER JOIN Department as C ON C.Path like B.Path + N'%' --and C.ItemLevel <> B.ItemLevel
						                WHERE A.Id = :userId
                                        UNION ALL
										SELECT C.*
										FROM ManualRoleRecord as A
                                        INNER JOIN Department as B ON B.Id = A.TargetDepartmentId
                                        INNER JOIN Department as C ON C.Path like B.Path + N'%' --and C.ItemLevel <> B.ItemLevel
                                        INNER JOIN ManualRole as D ON D.Id = A.RoleId and D.Id in (5, 6, 7)
						                WHERE A.UserId = :userId
                                              and ((isnull(C.BFGId, 0) = isnull(case when A.RoleId = 5 then 1 when A.RoleId = 6 then 2 when A.RoleId = 7 then 6 else null end, 0) and C.ItemLevel = 7) or (C.BFGId is null and C.ItemLevel <> 7))
                                                ) as A ) as F ON F.Id = isnull(A.DepartmentId, A.ParentId) and isnull(F.BFGId, 0) = isnull(B.BFGId, 0) ";
                    break;
                case UserRole.Inspector:
                    //кураторам показываем фронты и бэкфронты
                    sqlWhere = @" INNER JOIN Department as F ON F.Id = isnull(A.DepartmentId, A.ParentId) and isnull(F.BFGId, 2) in (2, 6)";
                    break;
                case UserRole.Director:
                    //членам правления 
                    sqlWhere = @" INNER JOIN DirectorsRight as F ON F.UserId = " + curUser.Id.ToString() + " and F.DepartmentAccessoryId = isnull(A.BFGId, F.DepartmentAccessoryId)";
                    break;
            }
            return sqlWhere;
        }
        /// <summary>
        /// Составляем строку условия для входных параметров.
        /// </summary>
        /// <param name="DepartmentId">Id подразделения.</param>
        /// <param name="Id">Id заявки</param>
        /// <param name="Surname">ФИО инициатора</param>
        /// <param name="DateBegin">Начало периода</param>
        /// <param name="DateEnd">Конец периода</param>
        /// <returns></returns>
        protected string GetWhereForParameters(int DepartmentId, int Id, string Surname, DateTime? DateBegin, DateTime? DateEnd, int StatusId, ref string sqlWhere, int RequestTypeId)
        {
            //string SqlWhere = string.Empty;
            if (DepartmentId != 0)
            {
                
                Department department = DepartmentDao.Load(DepartmentId);
                //sqlWhere += string.Format(@" B.Path  like '{0}' and B.ItemLevel = {1}", department.Path + "%", 7);
                sqlWhere += string.Format(@" B.Path  like '{0}' ", department.Path + "%");
                //sqlWhere += (!string.IsNullOrEmpty(sqlWhere) ? " and " : "") + "A.ParentId = :DepartmentId";
            }
            
            if (Id != 0)
                sqlWhere += (!string.IsNullOrEmpty(sqlWhere) ? " and " : "") + "A.Id = :Id";

            if (!string.IsNullOrEmpty(Surname))
                sqlWhere += (!string.IsNullOrEmpty(sqlWhere) ? " and " : "") + "Surname like '" + Surname + "%'";


            if (DateBegin.HasValue && DateEnd.HasValue)
                sqlWhere += (!string.IsNullOrEmpty(sqlWhere) ? " and " : "") + "A.DateRequest between :DateBegin and :DateEnd";
            else
            {
                if (DateBegin.HasValue)
                    sqlWhere += (!string.IsNullOrEmpty(sqlWhere) ? " and " : "") + "A.DateRequest >= :DateBegin";

                if (DateEnd.HasValue)
                    sqlWhere += (!string.IsNullOrEmpty(sqlWhere) ? " and " : "") + "A.DateRequest < :DateEnd";
            }

            if (StatusId != 0)
                sqlWhere += (!string.IsNullOrEmpty(sqlWhere) ? " and " : "") + "StatusId = :StatusId";

            if (RequestTypeId != 0)
                sqlWhere += (!string.IsNullOrEmpty(sqlWhere) ? " and " : "") + "A.RequestTypeId = :RequestTypeId";
            
            return sqlWhere;
        }
        /// <summary>
        /// Определяем сортировку записей.
        /// </summary>
        /// <param name="SortBy">Номер колонки</param>
        /// <param name="SortDescending">Направление сортировки.</param>
        /// <returns></returns>
        protected string GetOrderByForSqlQuery(int SortBy, bool? SortDescending)
        {
            string SqlOrderBy = " ORDER BY ";
            switch (SortBy)
            {
                case 1:
                    SqlOrderBy += "A.Id";
                    break;
                case 2:
                    SqlOrderBy += "DateRequest";
                    break;
                case 3:
                    SqlOrderBy += "RequestTypeName";
                    break;
                case 4:
                    SqlOrderBy += "Dep2Name";
                    break;
                case 5:
                    SqlOrderBy += "Dep3Name";
                    break;
                case 6:
                    SqlOrderBy += "Dep4Name";
                    break;
                case 7:
                    SqlOrderBy += "Dep5Name";
                    break;
                case 8:
                    SqlOrderBy += "Dep6Name";
                    break;
                case 9:
                    SqlOrderBy += "Dep7Name";
                    break;
                case 10:
                    SqlOrderBy += "OrderNumber";
                    break;
                case 11:
                    SqlOrderBy += "OrderDate";
                    break;
                case 12:
                    SqlOrderBy += "Surname";
                    break;
                case 13:
                    SqlOrderBy += "PositionName";
                    break;
                case 14:
                    SqlOrderBy += "Status";
                    break;
                case 15:
                    SqlOrderBy += "AccessoryName";
                    break;
                default:
                    SqlOrderBy += "A.Id";
                    break;
            }
            return SqlOrderBy += (SortDescending.HasValue && !SortDescending.Value ? "" : " desc");
        }

        /// <summary>
        /// Достаем Id действующей заявки для данного подразделения.
        /// </summary>
        /// <param name="Id">Id подразделения</param>
        /// <returns></returns>
        public int GetCurrentRequestId(int DepartmentId)
        {
            return Session.CreateSQLQuery(@"SELECT A.Id
                                            FROM StaffDepartmentRequest as A
                                            WHERE A.IsUsed = 1 and A.DepartmentId = :DepartmentId")
                .AddScalar("Id", NHibernateUtil.Int32)
                .SetInt32("DepartmentId", DepartmentId)
                .UniqueResult<int>();
        }

        /// <summary>
        /// Проверка на возможность создать код Финграда для создаваемого подразделения.
        /// </summary>
        /// <param name="Id">Id родительского подразделения</param>
        /// <returns></returns>
        public bool IsEnableCreateCode(int Id)
        {
            //от родительского подразделения и вверх по ветке до филиала смотрим чтобы справочник кодировок имел коды к веткам
            IQuery query = Session.CreateSQLQuery(@"SELECT cast(case when count(*) = 0 then 1 else 0 end as bit) IsExists
                                                    FROM (SELECT B.Id, 
						                                         case when B.ItemLevel = 6 then (SELECT Code FROM StaffDepartmentRPLink WHERE DepartmentId = B.Id) 
									                                        when B.ItemLevel = 5 then (SELECT Code FROM StaffDepartmentBusinessGroup WHERE DepartmentId = B.Id)
									                                        when B.ItemLevel = 4 then (SELECT Code FROM StaffDepartmentAdministration WHERE DepartmentId = B.Id)
									                                        when B.ItemLevel = 3 then (SELECT Code FROM StaffDepartmentManagement WHERE DepartmentId = B.Id)
									                                        when B.ItemLevel = 2 then (SELECT Code FROM StaffDepartmentBranch WHERE DepartmentId = B.Id)
						                                         else B.FingradCode end as FingradCode
			                                              FROM Department as A
			                                              INNER JOIN Department as B ON B.ItemLevel <= A.ItemLevel and B.ItemLevel not in (1, 7) and A.Path like B.Path + '%'
			                                              WHERE A.Id = " + Id.ToString() + @") as A
                                                    WHERE A.FingradCode is null")
            .AddScalar("IsExists", NHibernateUtil.Boolean);
            return query.UniqueResult<bool>();
        }

        /// <summary>
        /// Формируем новый код для подразделения 7 уровня.
        /// </summary>
        /// <param name="br">Филиал</param>
        /// <param name="mn">Дирекция</param>
        /// <param name="rp">РП-привязка</param>
        /// <returns></returns>
        public string GetNewFinDepCode(StaffDepartmentBranch br, StaffDepartmentManagement mn, StaffDepartmentRPLink rp)
        {
            IList<Department> dep = Session.Query<Department>().Where(x => x.ItemLevel == 7 && x.ParentId == rp.Department.Code1C && x.FingradCode != null).ToList();


            string Code = dep.Count == 0 || dep.Where(x => x.FingradCode.StartsWith(br.Code + "-" + mn.Code.Substring(1) + "-" + rp.Code.Substring(6, 2))).Count() == 0 ? "0" :
                dep.Where(x => x.FingradCode.StartsWith(br.Code + "-" + mn.Code.Substring(1) + "-" + rp.Code.Substring(6, 2)))
                .OrderByDescending(x => x.FingradCode.Substring(9))
                .FirstOrDefault().FingradCode.Substring(9);

            //предпологаем, что код содержит только цифры с разделителями, увеличиваем на 1
            Code = (Convert.ToInt32(Code) + 1).ToString();
            Code = (Code.Length == 1 ? "00" : (Code.Length == 2 ? "0" : "")) + Code;

            Code = br.Code + "-" + mn.Code.Substring(1) + "-" + rp.Code.Substring(6, 2) + "-" + Code;

            return Code;
        }

        /// <summary>
        /// Проверка на возможность закрыть подразделение.
        /// </summary>
        /// <param name="Id">Id подразделения</param>
        /// <returns></returns>
        public bool IsEnableCloseDepartment(int Id)
        {
            //от текущего подразделения смотрим всю ветку вниз и ищем сотрудников в подразделениях
            IQuery query = Session.CreateSQLQuery(@"SELECT cast(case when count(*) = 0 then 1 else 0 end as bit) IsExists
                                                    FROM Department as A
                                                    INNER JOIN Department as B ON B.Path like A.Path + N'%'
                                                    INNER JOIN Users as C ON C.DepartmentId = B.Id and (C.RoleId & 2 > 0) and C.IsActive = 1
                                                    WHERE A.Id = " + Id.ToString())
            .AddScalar("IsExists", NHibernateUtil.Boolean);
            return query.UniqueResult<bool>();
        }
    }
}

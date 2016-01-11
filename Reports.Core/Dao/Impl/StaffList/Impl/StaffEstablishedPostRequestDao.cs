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
    /// Заявки по штатным единицам.
    /// </summary>
    public class StaffEstablishedPostRequestDao : DefaultDao<StaffEstablishedPostRequest>, IStaffEstablishedPostRequestDao
    {
        public StaffEstablishedPostRequestDao(ISessionManager sessionManager)
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
        /// Список заявок для штатных единиц.
        /// </summary>
        /// <param name="curUser">Текущий пользователь.</param>
        /// <param name="role">Роль пользователя (из-за байт-кода дополнительный параметр).</param>
        /// <param name="DepartmentId">Id подразделения.</param>
        /// <param name="Id">Номер заявки</param>
        /// <param name="Surname">ФИО инициатора</param>
        /// <param name="DateBegin">Дата начала периода создания заявки</param>
        /// <param name="DateEnd">Дата конца периода создания заявки</param>
        /// <param name="StatusId">Id статуса заявки.</param>
        /// <param name="SortBy">Номер колонки для сортировки</param>
        /// <param name="SortDescending">Признак направления сортировки.</param>
        /// <param name="RequestTypeId">Вид заявки</param>
        /// <returns></returns>
        public IList<EstablishedPostRequestDto> GetEstablishedPostRequestList(User curUser, UserRole role, int DepartmentId, int Id, string Surname, DateTime? DateBegin, DateTime? DateEnd, int StatusId, int SortBy, bool? SortDescending, int RequestTypeId)
        {
            string SqlQuery = @"SELECT A.Id, 
                                       A.SEPId,
                                       A.DateRequest, 
                                       B.Name as RequestTypeName, 
                                       A.RequestTypeId,
                                       A.DepartmentId,
                                       C.ParentId,
                                       Dep2.Name as Dep2Name, 
                                       Dep3.Name as Dep3Name, 
                                       Dep4.Name as Dep4Name, 
                                       Dep5.Name as Dep5Name, 
                                       Dep6.Name as Dep6Name, 
                                       C.Name as Dep7Name, 
                                       Dep2.Path as Dep2Path, 
                                       Dep3.Path as Dep3Path, 
                                       Dep4.Path as Dep4Path, 
                                       Dep5.Path as Dep5Path, 
                                       Dep6.Path as Dep6Path, 
                                       D.Id as PersonId, 
                                       D.Name as Surname, 
                                       E.Name as PositionName,
                                       case when A.DateSendToApprove is null then 'Черновик'
						                    when A.DateSendToApprove is not null and A.DateAccept is null then 'На согласовании'
						                    when A.DateAccept is not null then 'Утверждено' end as Status,
			                           case when A.DateSendToApprove is null then 1
						                    when A.DateSendToApprove is not null and A.DateAccept is null then 2
						                    when A.DateAccept is not null then 3 end as StatusId
                                FROM StaffEstablishedPostRequest as A
                                INNER JOIN StaffEstablishedPostRequestTypes as B ON B.Id = A.RequestTypeId
                                INNER JOIN Department as C ON C.Id = A.DepartmentId
                                LEFT JOIN Department as Dep2 ON C.Path like Dep2.Path + N'%' and Dep2.ItemLevel = 2
                                LEFT JOIN Department as Dep3 ON C.Path like Dep3.Path + N'%' and Dep3.ItemLevel = 3
                                LEFT JOIN Department as Dep4 ON C.Path like Dep4.Path + N'%' and Dep4.ItemLevel = 4
                                LEFT JOIN Department as Dep5 ON C.Path like Dep5.Path + N'%' and Dep5.ItemLevel = 5
                                LEFT JOIN Department as Dep6 ON C.Path like Dep6.Path + N'%' and Dep6.ItemLevel = 6
                                LEFT JOIN Users as D ON D.Id = A.CreatorID
                                LEFT JOIN Position as E ON E.Id = D.PositionId";

            SqlQuery = string.Format(@"SELECT * FROM ({0}) as A", SqlQuery);
            SqlQuery += @" INNER JOIN Department as B ON B.Id = A.DepartmentId";


            SqlQuery += GetWhereForUserRole(curUser, role);
            SqlQuery += GetWhereForParameters(DepartmentId, Id, Surname, DateBegin, DateEnd, StatusId, RequestTypeId);
            SqlQuery += GetOrderByForSqlQuery(SortBy, SortDescending);

            IQuery query = Session.CreateSQLQuery(SqlQuery)
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("SEPId", NHibernateUtil.Int32)
                .AddScalar("DateRequest", NHibernateUtil.Date)
                .AddScalar("RequestTypeName", NHibernateUtil.String)
                .AddScalar("RequestTypeId", NHibernateUtil.Int32)
                .AddScalar("DepartmentId", NHibernateUtil.Int32)
                .AddScalar("Dep2Name", NHibernateUtil.String)
                .AddScalar("Dep3Name", NHibernateUtil.String)
                .AddScalar("Dep4Name", NHibernateUtil.String)
                .AddScalar("Dep5Name", NHibernateUtil.String)
                .AddScalar("Dep6Name", NHibernateUtil.String)
                .AddScalar("Dep7Name", NHibernateUtil.String)
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

            return query.SetResultTransformer(Transformers.AliasToBean<EstablishedPostRequestDto>()).List<EstablishedPostRequestDto>();
        }
        /// <summary>
        /// Определяем критерий поиска для роли текущего пользователя.
        /// </summary>
        /// <param name="curUser">Текущий пользователь.</param>
        /// <returns></returns>
        protected string GetWhereForUserRole(User curUser, UserRole role)
        {
            string sqlWhere = string.Empty;
            switch (role)
            {
                case UserRole.Manager:
                    sqlWhere = @"
                                 INNER JOIN (SELECT C.*
                                             FROM Users as A
                                             INNER JOIN Department as B ON B.Id = A.DepartmentId
                                             INNER JOIN Department as C ON C.Path like B.Path + N'%' and C.ItemLevel <> B.ItemLevel
						                     WHERE A.Id = :userId) as F ON F.Id = isnull(A.DepartmentId, A.ParentId)";
                    break;
                case UserRole.PersonnelManager:
                    sqlWhere = @" INNER JOIN vwDepartmentToPersonnels as F ON F.DepartmentId = isnull(A.DepartmentId, A.ParentId) and F.PersonnelId = :userId";
                    break;
                case UserRole.Inspector:
                    //кураторам показываем фронты и бэкфронты
                    sqlWhere = @" INNER JOIN Department as F ON F.Id = isnull(A.DepartmentId, A.ParentId) and isnull(F.BFGId, 2) in (2, 6)";
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
        protected string GetWhereForParameters(int DepartmentId, int Id, string Surname, DateTime? DateBegin, DateTime? DateEnd, int StatusId, int RequestTypeId)
        {
            string SqlWhere = string.Empty;
            if (DepartmentId != 0)
            {
                Department department = DepartmentDao.Load(DepartmentId);
                SqlWhere += string.Format(@" B.Path  like '{0}' and B.ItemLevel = {1}", department.Path + "%", 7);
                //SqlWhere += "A.ParentId = :DepartmentId";
            }

            if (Id != 0)
                SqlWhere += (!string.IsNullOrEmpty(SqlWhere) ? " and " : "") + "A.Id = :Id";

            if (!string.IsNullOrEmpty(Surname))
                SqlWhere += (!string.IsNullOrEmpty(SqlWhere) ? " and " : "") + "Surname like '" + Surname + "%'";


            if (DateBegin.HasValue && DateEnd.HasValue)
                SqlWhere += (!string.IsNullOrEmpty(SqlWhere) ? " and " : "") + "A.DateRequest between :DateBegin and :DateEnd";
            else
            {
                if (DateBegin.HasValue)
                    SqlWhere += (!string.IsNullOrEmpty(SqlWhere) ? " and " : "") + "A.DateRequest >= :DateBegin";

                if (DateEnd.HasValue)
                    SqlWhere += (!string.IsNullOrEmpty(SqlWhere) ? " and " : "") + "A.DateRequest < :DateEnd";
            }

            if (StatusId != 0)
                SqlWhere += (!string.IsNullOrEmpty(SqlWhere) ? " and " : "") + "StatusId = :StatusId";

            if (RequestTypeId != 0)
                SqlWhere += (!string.IsNullOrEmpty(SqlWhere) ? " and " : "") + "A.RequestTypeId = :RequestTypeId";
            
            return (string.IsNullOrEmpty(SqlWhere) ? "" : " WHERE " + SqlWhere);
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
                    SqlOrderBy += "Surname";
                    break;
                case 11:
                    SqlOrderBy += "PositionName";
                    break;
                case 12:
                    SqlOrderBy += "Status";
                    break;
                default:
                    SqlOrderBy += "A.Id";
                    break;
            }
            return SqlOrderBy += (SortDescending.HasValue && !SortDescending.Value ? "" : " desc");
        }
        /// <summary>
        /// Достаем Id действующей заявки для данной штатной единицы.
        /// </summary>
        /// <param name="SEPId">Id штатной единицы.</param>
        /// <returns></returns>
        public int GetCurrentRequestId(int SEPId)
        {
            return Session.CreateSQLQuery(@"SELECT A.Id
                                            FROM StaffEstablishedPostRequest as A
                                            WHERE A.IsUsed = 1 and A.SEPId = :SEPId")
                .AddScalar("Id", NHibernateUtil.Int32)
                .SetInt32("SEPId", SEPId)
                .UniqueResult<int>();
        }
        /// <summary>
        /// Достаем предыдущую утвержденную заявку для данной штатной единицы.
        /// </summary>
        /// <param name="SEPId">Id штатной единицы.</param>
        /// <param name="Id">Id заявки, для которой ищем предыдущее состояние.</param>
        /// <returns></returns>
        public StaffEstablishedPostRequest GetPrevEstablishedPostRequest(int SEPId, int Id)
        {
            return Session.Query<StaffEstablishedPostRequest>()
                .Where(x => x.StaffEstablishedPost.Id == SEPId && /*x.Id != Id &&*/ x.DateAccept.HasValue).ToList()
                .OrderByDescending(x => x.DateAccept).FirstOrDefault();
        }
    }
}

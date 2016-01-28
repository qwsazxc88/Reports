using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Services;
using Reports.Core.Dto;
using System.Linq;
using NHibernate.Linq;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Заявки на временно освобождение вакансии.
    /// </summary>
    public class StaffTemporaryReleaseVacancyRequestDao : DefaultDao<StaffTemporaryReleaseVacancyRequest>, IStaffTemporaryReleaseVacancyRequestDao
    {
        public StaffTemporaryReleaseVacancyRequestDao(ISessionManager sessionManager)
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
        /// Список заявок на создание временных вакансий.
        /// </summary>
        /// <param name="curUser">Текущий пользователь.</param>
        /// <param name="role">Роль пользователя (из-за байт-кода дополнительный параметр).</param>
        /// <param name="DepartmentId">Id подразделения.</param>
        /// <param name="Id">Номер заявки</param>
        /// <param name="SEPId">Id штатной единицы</param>
        /// <param name="Surname">ФИО инициатора</param>
        /// <param name="DateBegin">Дата начала периода действия заявки</param>
        /// <param name="DateEnd">Дата конца периода действия заявки</param>
        /// <param name="AbsencesTypeId">Id вида отсутствия.</param>
        /// <param name="SortBy">Номер колонки для сортировки</param>
        /// <param name="SortDescending">Признак направления сортировки.</param>
        /// <returns></returns>
        public IList<StaffTemporaryReleaseVacancyDto> GetTemporaryReleaseVacancyList(User curUser, UserRole role, int DepartmentId, int Id, int SEPId, string Surname, DateTime? DateBegin, DateTime? DateEnd, int AbsencesTypeId, int SortBy, bool? SortDescending)
        {
            string SqlQuery = @"SELECT A.Id
			                            ,B.SEPId
			                            ,C.DepartmentId
                                        ,A.UserLinkId
			                            ,Dep3.Name as Dep3Name
			                            ,Dep4.Name as Dep4Name
			                            ,Dep5.Name as Dep5Name
			                            ,Dep6.Name as Dep6Name
			                            ,Dep7.Name as Dep7Name
                                        ,C.PositionId
			                            ,D.Name as PositionName
			                            ,A.ReplacedId
			                            ,E.Name as Surname
			                            ,A.AbsencesTypeId
			                            ,F.Name as AbsencesTypeName
			                            ,A.DateBegin
			                            ,A.DateEnd
			                            ,A.IsUsed
                                        ,A.CreateDate
                                FROM StaffTemporaryReleaseVacancyRequest as A
                                INNER JOIN StaffEstablishedPostUserLinks as B ON B.Id = A.UserLinkId and B.IsUsed = 1
                                INNER JOIN StaffEstablishedPost as C ON C.Id = B.SEPId
                                INNER JOIN Position as D ON D.Id = C.PositionId
                                INNER JOIN Department as Dep7 ON Dep7.Id = C.DepartmentId
                                INNER JOIN Department as Dep6 ON Dep6.Id = Dep7.ParentId
                                INNER JOIN Department as Dep5 ON Dep5.Id = Dep6.ParentId
                                INNER JOIN Department as Dep4 ON Dep4.Id = Dep5.ParentId
                                INNER JOIN Department as Dep3 ON Dep3.Id = Dep4.ParentId
                                INNER JOIN Users as E ON E.Id = A.ReplacedId
                                INNER JOIN StaffLongAbsencesTypes as F ON F.Id = A.AbsencesTypeId";

            SqlQuery = string.Format(@"SELECT * FROM ({0}) as A", SqlQuery);
            SqlQuery += @" INNER JOIN Department as B ON B.Id = A.DepartmentId";


            SqlQuery += GetWhereForUserRole(curUser, role);
            SqlQuery += GetWhereForParameters(DepartmentId, Id, SEPId, Surname, DateBegin, DateEnd, AbsencesTypeId);
            SqlQuery += GetOrderByForSqlQuery(SortBy, SortDescending);

            IQuery query = Session.CreateSQLQuery(SqlQuery)
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("SEPId", NHibernateUtil.Int32)
                .AddScalar("UserLinkId", NHibernateUtil.Int32)
                .AddScalar("SEPId", NHibernateUtil.Int32)
                .AddScalar("ReplacedId", NHibernateUtil.Int32)
                .AddScalar("Surname", NHibernateUtil.String)
                .AddScalar("DepartmentId", NHibernateUtil.Int32)
                .AddScalar("Dep3Name", NHibernateUtil.String)
                .AddScalar("Dep4Name", NHibernateUtil.String)
                .AddScalar("Dep5Name", NHibernateUtil.String)
                .AddScalar("Dep6Name", NHibernateUtil.String)
                .AddScalar("Dep7Name", NHibernateUtil.String)
                .AddScalar("PositionId", NHibernateUtil.Int32)
                .AddScalar("PositionName", NHibernateUtil.String)
                .AddScalar("AbsencesTypeId", NHibernateUtil.Int32)
                .AddScalar("AbsencesTypeName", NHibernateUtil.String)
                .AddScalar("DateBegin", NHibernateUtil.DateTime)
                .AddScalar("DateEnd", NHibernateUtil.DateTime)
                .AddScalar("CreateDate", NHibernateUtil.DateTime)
                .AddScalar("IsUsed", NHibernateUtil.Boolean);

            if (SqlQuery.Contains(":userId")) query.SetInt32("userId", curUser.Id);
            if (SqlQuery.Contains(":DepartmentId")) query.SetInt32("DepartmentId", DepartmentId);
            if (SqlQuery.Contains(":Id")) query.SetInt32("Id", Id);
            if (SqlQuery.Contains(":SEPId")) query.SetInt32("SEPId", SEPId);
            if (SqlQuery.Contains(":DateBegin")) query.SetDateTime("DateBegin", DateBegin.Value);
            if (SqlQuery.Contains(":DateEnd")) query.SetDateTime("DateEnd", DateEnd.Value.AddDays(1));
            if (SqlQuery.Contains(":AbsencesTypeId")) query.SetInt32("AbsencesTypeId", AbsencesTypeId);

            return query.SetResultTransformer(Transformers.AliasToBean<StaffTemporaryReleaseVacancyDto>()).List<StaffTemporaryReleaseVacancyDto>();
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
						                WHERE A.UserId = :userId) as A ) as F ON F.Id = isnull(A.DepartmentId, A.ParentId) and isnull(F.BFGId, 0) = isnull(B.BFGId, 0) ";
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
        protected string GetWhereForParameters(int DepartmentId, int Id, int SEPId, string Surname, DateTime? DateBegin, DateTime? DateEnd, int AbsencesTypeId)
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

            if (SEPId != 0)
                SqlWhere += (!string.IsNullOrEmpty(SqlWhere) ? " and " : "") + "A.SEPId = :SEPId";

            if (!string.IsNullOrEmpty(Surname))
                SqlWhere += (!string.IsNullOrEmpty(SqlWhere) ? " and " : "") + "Surname like '" + Surname + "%'";


            if (DateBegin.HasValue && DateEnd.HasValue)
                SqlWhere += (!string.IsNullOrEmpty(SqlWhere) ? " and " : "") + "A.CreateDate between :DateBegin and :DateEnd";
            else
            {
                if (DateBegin.HasValue)
                    SqlWhere += (!string.IsNullOrEmpty(SqlWhere) ? " and " : "") + "A.CreateDate >= :DateBegin";

                if (DateEnd.HasValue)
                    SqlWhere += (!string.IsNullOrEmpty(SqlWhere) ? " and " : "") + "A.CreateDate < :DateEnd";
            }

            if (AbsencesTypeId != 0)
                SqlWhere += (!string.IsNullOrEmpty(SqlWhere) ? " and " : "") + "AbsencesTypeId = :AbsencesTypeId";


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
                    SqlOrderBy += "A.CreateDate";
                    break;
                case 3:
                    SqlOrderBy += "Dep3Name";
                    break;
                case 4:
                    SqlOrderBy += "Dep4Name";
                    break;
                case 5:
                    SqlOrderBy += "Dep5Name";
                    break;
                case 6:
                    SqlOrderBy += "Dep6Name";
                    break;
                case 7:
                    SqlOrderBy += "Dep7Name";
                    break;
                case 8:
                    SqlOrderBy += "A.SEPId";
                    break;
                case 9:
                    SqlOrderBy += "A.PositionName";
                    break;
                case 10:
                    SqlOrderBy += "A.Surname";
                    break;
                case 11:
                    SqlOrderBy += "A.AbsencesTypeName";
                    break;
                case 12:
                    SqlOrderBy += "A.DateBegin";
                    break;
                case 13:
                    SqlOrderBy += "A.DateEnd";
                    break;
                case 14:
                    SqlOrderBy += "A.IsUsed";
                    break;
                
                default:
                    SqlOrderBy += "A.Id";
                    break;
            }
            return SqlOrderBy += (SortDescending.HasValue && !SortDescending.Value ? "" : " desc");
        }
    }
}

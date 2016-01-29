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
        /// <param name="SEPId">Id штатной единицы</param>
        /// <param name="Surname">ФИО инициатора</param>
        /// <param name="DateBegin">Дата начала периода создания заявки</param>
        /// <param name="DateEnd">Дата конца периода создания заявки</param>
        /// <param name="StatusId">Id статуса заявки.</param>
        /// <param name="SortBy">Номер колонки для сортировки</param>
        /// <param name="SortDescending">Признак направления сортировки.</param>
        /// <param name="RequestTypeId">Вид заявки</param>
        /// <param name="BFGId">Id принадлежности подразделения.</param>
        /// <returns></returns>
        public IList<EstablishedPostRequestDto> GetEstablishedPostRequestList(User curUser, UserRole role, int DepartmentId, int Id, int SEPId, string Surname, DateTime? DateBegin, DateTime? DateEnd, int StatusId, int SortBy, bool? SortDescending
                                                                                , int RequestTypeId, int BFGId)
        {
            string SqlQuery = @"SELECT  A.Id 
				                        ,A.SEPId
				                        ,A.DateRequest
				                        ,B.Name as RequestTypeName 
				                        ,A.RequestTypeId
				                        ,A.DepartmentId
				                        ,Dep3.Name as Dep3Name 
				                        ,C.Name as Dep7Name 
                                        ,C.BFGId
				                        ,A.BeginAccountDate
				                        ,F.Name as DepartmentAccessoryName
				                        ,case when len(isnull(J.TaxAdminCode, isnull(JJ.TaxAdminCode, ''))) = 0 then N'Нет' else N'Да' end as TaxAvailable
				                        ,E.Name as PositionName
				                        ,isnull(G.CountNotVacation, 0) as CountNotVacation
				                        ,isnull(G.CountVacation, 0) as CountVacation
				                        --по дате утверждения заявки смотрю в архив
				                        ,(SELECT top 1 Salary FROM StaffEstablishedPostArchive
					                        WHERE SEPId = A.SepId and Id < (SELECT Id FROM StaffEstablishedPostArchive 
																					                        WHERE SEPId = A.SepId and CreateDate = A.DateAccept)
																					                        ORDER BY Id desc) as SalaryPrev
				                        ,A.Salary
				                        --относительно текущей заявки по дате утверждения нахожу предыдущую, а потом уже смотрю районнй коэффициент
				                        ,(SELECT top 1 case when ActionId = 4 then 0 else Amount end FROM StaffEstablishedPostChargeLinks
					                        WHERE SEPId = A.SepId and StaffExtraChargeId = 6
					                        and SEPRequestId = (SELECT top 1 Id FROM StaffEstablishedPostRequest 
															                        WHERE SEPId = A.SepId and DateAccept is not null and DateAccept < isnull(A.DateAccept, GETDATE()) ORDER BY Id desc)
															                        ORDER BY Id desc) as RegionalRatePrev
				                        ,case when H.ActionId = 4 then 0 else H.Amount end as RegionalRate
				                        ,D.Id as PersonId
				                        ,D.Name as Surname
				                        ,case when A.DeleteDate is not null then 'Отклонено'
							                        when A.DateSendToApprove is null and K.DocId is null then 'Черновик'
							                        when A.DateSendToApprove is not null and A.DateAccept is null and A.DeleteDate is null and isnull(K.Number, 0) = 1 then 'Заявка создана'
																			when A.DateSendToApprove is not null and A.DateAccept is null and A.DeleteDate is null and isnull(K.Number, 0) = 2 then 'Заявка проверена куратором'
																			when A.DateSendToApprove is not null and A.DateAccept is null and A.DeleteDate is null and isnull(K.Number, 0) = 3 then 'Заявка проверена кадровиком'
																			when A.DateSendToApprove is not null and A.DateAccept is null and A.DeleteDate is null and isnull(K.Number, 0) = 4 then 'Заявка согласована высшим руководителем'
							                        when A.DateAccept is not null and A.DeleteDate is null then 'Заявка утверждена' end as Status
				                        ,case when A.DeleteDate is not null then 7
							                        when A.DateSendToApprove is null and K.DocId is null then 1
							                        when A.DateSendToApprove is not null and A.DateAccept is null and A.DeleteDate is null and isnull(K.Number, 0) = 1 then 2
																			when A.DateSendToApprove is not null and A.DateAccept is null and A.DeleteDate is null and isnull(K.Number, 0) = 2 then 3
																			when A.DateSendToApprove is not null and A.DateAccept is null and A.DeleteDate is null and isnull(K.Number, 0) = 3 then 4
																			when A.DateSendToApprove is not null and A.DateAccept is null and A.DeleteDate is null and isnull(K.Number, 0) = 4 then 5
							                        when A.DateAccept is not null and A.DeleteDate is null then 6 end as StatusId
                        FROM StaffEstablishedPostRequest as A
                        INNER JOIN StaffEstablishedPostRequestTypes as B ON B.Id = A.RequestTypeId
                        INNER JOIN Department as C ON C.Id = A.DepartmentId
                        LEFT JOIN Department as Dep3 ON C.Path like Dep3.Path + N'%' and Dep3.ItemLevel = 3
                        LEFT JOIN Users as D ON D.Id = A.CreatorID
                        LEFT JOIN Position as E ON E.Id = D.PositionId
                        LEFT JOIN StaffDepartmentAccessory as F ON F.Id = C.BFGId
                        LEFT JOIN (SELECT SEPId
                        ,sum(case when UserId is null then 1 else 0 end) as CountVacation 
                        ,sum(case when UserId is not null then 1 else 0 end) as CountNotVacation 
                        FROM StaffEstablishedPostUserLinks WHERE IsUsed = 1
                        GROUP BY SEPId) as G ON G.SEPId = A.SEPId
                        LEFT JOIN StaffEstablishedPostChargeLinks as H ON H.SEPRequestId = A.Id and H.SEPId = A.SEPId and H.StaffExtraChargeId = 6
                        LEFT JOIN StaffDepartmentRequest as I ON I.DepartmentId = A.DepartmentId and I.IsUsed = 1
                        LEFT JOIN StaffDepartmentTaxDetails as J ON J.DepartmentId = I.DepartmentId
                        LEFT JOIN StaffDepartmentTaxDetails as JJ ON JJ.DepartmentId = I.DepNextId
                        LEFT JOIN (SELECT DocId, MAX(Number) as Number FROM DocumentApproval 
						    	   WHERE ApprovalType = 2 and Number <= 4 GROUP BY DocId) as K ON K.DocId = A.Id";

            SqlQuery = string.Format(@"SELECT * FROM ({0}) as A", SqlQuery);
            SqlQuery += @" INNER JOIN Department as B ON B.Id = A.DepartmentId";


            SqlQuery += GetWhereForUserRole(curUser, role);
            SqlQuery += GetWhereForParameters(DepartmentId, Id, SEPId, Surname, DateBegin, DateEnd, StatusId, RequestTypeId, BFGId);
            SqlQuery += GetOrderByForSqlQuery(SortBy, SortDescending);

            IQuery query = Session.CreateSQLQuery(SqlQuery)
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("SEPId", NHibernateUtil.Int32)
                .AddScalar("DateRequest", NHibernateUtil.Date)
                .AddScalar("RequestTypeName", NHibernateUtil.String)
                .AddScalar("RequestTypeId", NHibernateUtil.Int32)
                .AddScalar("DepartmentId", NHibernateUtil.Int32)
                .AddScalar("Dep3Name", NHibernateUtil.String)
                .AddScalar("Dep7Name", NHibernateUtil.String)
                .AddScalar("BeginAccountDate", NHibernateUtil.Date)
                .AddScalar("DepartmentAccessoryName", NHibernateUtil.String)
                .AddScalar("TaxAvailable", NHibernateUtil.String)
                .AddScalar("PositionName", NHibernateUtil.String)
                .AddScalar("CountNotVacation", NHibernateUtil.Int32)
                .AddScalar("CountVacation", NHibernateUtil.Int32)
                .AddScalar("SalaryPrev", NHibernateUtil.Decimal)
                .AddScalar("Salary", NHibernateUtil.Decimal)
                .AddScalar("RegionalRatePrev", NHibernateUtil.Decimal)
                .AddScalar("RegionalRate", NHibernateUtil.Decimal)
                .AddScalar("PersonId", NHibernateUtil.Int32)
                .AddScalar("Surname", NHibernateUtil.String)
                .AddScalar("Status", NHibernateUtil.String)
                .AddScalar("StatusId", NHibernateUtil.Int32);

            if (SqlQuery.Contains(":userId")) query.SetInt32("userId", curUser.Id);
            if (SqlQuery.Contains(":DepartmentId")) query.SetInt32("DepartmentId", DepartmentId);
            if (SqlQuery.Contains(":Id")) query.SetInt32("Id", Id);
            if (SqlQuery.Contains(":SEPId")) query.SetInt32("SEPId", SEPId);
            if (SqlQuery.Contains(":DateBegin")) query.SetDateTime("DateBegin", DateBegin.Value);
            if (SqlQuery.Contains(":DateEnd")) query.SetDateTime("DateEnd", DateEnd.Value.AddDays(1));
            if (SqlQuery.Contains(":StatusId")) query.SetInt32("StatusId", StatusId);
            if (SqlQuery.Contains(":RequestTypeId")) query.SetInt32("RequestTypeId", RequestTypeId);
            if (SqlQuery.Contains(":BFGId")) query.SetInt32("BFGId", BFGId);

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
						                WHERE A.UserId = :userId) as A ) as F ON F.Id = isnull(A.DepartmentId, B.ParentId) and isnull(F.BFGId, 0) = isnull(B.BFGId, 0) ";
                    break;
                case UserRole.PersonnelManager:
                    sqlWhere = @" INNER JOIN vwDepartmentToPersonnels as F ON F.DepartmentId = isnull(A.DepartmentId, B.ParentId) and F.PersonnelId = :userId";
                    break;
                case UserRole.Inspector:
                    //кураторам показываем фронты и бэкфронты
                    sqlWhere = @" INNER JOIN Department as F ON F.Id = isnull(A.DepartmentId, B.ParentId) and isnull(F.BFGId, 2) in (2, 6)";
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
        protected string GetWhereForParameters(int DepartmentId, int Id, int SEPId, string Surname, DateTime? DateBegin, DateTime? DateEnd, int StatusId, int RequestTypeId, int BFGId)
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

            if (BFGId != 0)
                SqlWhere += (!string.IsNullOrEmpty(SqlWhere) ? " and " : "") + "A.BFGId = :BFGId";
            
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
                    SqlOrderBy += "A.SEPId";
                    break;
                case 3:
                    SqlOrderBy += "A.DateRequest";
                    break;
                case 4:
                    SqlOrderBy += "A.RequestTypeName";
                    break;
                case 5:
                    SqlOrderBy += "A.Dep3Name";
                    break;
                case 6:
                    SqlOrderBy += "A.Dep7Name";
                    break;
                case 7:
                    SqlOrderBy += "A.BeginAccountDate";
                    break;
                case 8:
                    SqlOrderBy += "A.TaxAvailable";
                    break;
                case 9:
                    SqlOrderBy += "A.CountNotVacation";
                    break;
                case 10:
                    SqlOrderBy += "A.CountVacation";
                    break;
                case 11:
                    SqlOrderBy += "A.SalaryPrev";
                    break;
                case 12:
                    SqlOrderBy += "A.Salary";
                    break;
                case 13:
                    SqlOrderBy += "A.RegionalRatePrev";
                    break;
                case 14:
                    SqlOrderBy += "A.RegionalRate";
                    break;
                case 15:
                    SqlOrderBy += "A.PositionName";
                    break;
                case 16:
                    SqlOrderBy += "A.Surname";
                    break;
                case 17:
                    SqlOrderBy += "A.Status";
                    break;
                case 18:
                    SqlOrderBy += "A.DepartmentAccessoryName";
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
